using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using System.Net.Mail;

namespace MichaelHargreaves
{
    class Program
    {
        private const string FILE = "c:\\data.txt";
        private const string COUNTRY = "united kingdom";
        private const string DB_CONN = "Data Source=MICHAEL-HP\\SQLEXPRESS;Initial Catalog=RewardsForRacing;Integrated Security=True";
        static void Main(string[] args)
        {
            DataTable _imports = new DataTable("PeopleTable");

            try
            {
                _imports = GetData();
                AddToDataBase(_imports);
                Console.ReadKey();
            }
            catch (Exception pex)
            {
                Console.WriteLine(string.Format("Error running program: {0}", pex.Message));
            }
        }

        private static DataTable GetData()
        {
            DataTable _imports = new DataTable("PeopleTable");

            _imports.Columns.Add("id", typeof(Int32));
            _imports.Columns.Add("first_name", typeof(string));
            _imports.Columns.Add("last_name", typeof(string));
            _imports.Columns.Add("email", typeof(string));
            _imports.Columns.Add("country", typeof(string));

            if (File.Exists(FILE))
            {
                var fileStream = new FileStream(FILE, FileMode.Open, FileAccess.Read);

                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string newLine;
                    while ((newLine = streamReader.ReadLine()) != null)
                    {
                        string[] newPerson = newLine.Split(',');

                        if (newPerson.Length == 5)
                        {
                            if (newPerson[4].ToLower() == COUNTRY && CheckEmail(newPerson[1], newPerson[2], newPerson[3]))
                            {
                                try
                                {
                                    DataRow newRow = _imports.NewRow();

                                    newRow["id"] = Convert.ToInt32(newPerson[0]);
                                    newRow["first_name"] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(newPerson[1]);
                                    newRow["last_name"] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(newPerson[2]);
                                    newRow["email"] = newPerson[3].ToString().ToLower();
                                    newRow["country"] = newPerson[4];

                                    _imports.Rows.Add(newRow);

                                    Console.WriteLine(string.Format("{0} {1} successfully added to datatable.",
                                                                    CultureInfo.CurrentCulture.TextInfo.ToTitleCase(newPerson[1]),
                                                                    CultureInfo.CurrentCulture.TextInfo.ToTitleCase(newPerson[2])));
                                }
                                catch (Exception ex)
                                {
                                    //Log failed import
                                    Console.WriteLine(string.Format("Error adding {0} {1} into datatable: {2}", 
                                                                    CultureInfo.CurrentCulture.TextInfo.ToTitleCase(newPerson[1]), 
                                                                    CultureInfo.CurrentCulture.TextInfo.ToTitleCase(newPerson[2]),
                                                                    ex.Message));
                                }
                            }
                        }
                    }
                }
                return _imports;
            }
            else
            {
                Console.WriteLine(string.Format("{0} Does Not Exist, Please check file and run program again.", FILE));
                return _imports;
            }
                
        }

        private static bool CheckEmail(string Forename, string Surname, string Email)
        {
            try
            {
                return Regex.IsMatch(Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            }
            catch 
            {
                Console.WriteLine(string.Format("Email invalid for {0} {1}.", Forename, Surname));
                return false;
            }
        }

        private static bool AddToDataBase(DataTable Dt)
        {
            SqlConnection MyConn = new SqlConnection(DB_CONN);
            SqlCommand MyInsert = new SqlCommand("INSERT INTO Imports(id, first_name, last_name, email, country) VALUES(@id, @first_name, @last_name, @email, @country)", MyConn);
            SqlDataAdapter DA = new SqlDataAdapter();
            SqlParameter[] InsertParams = new SqlParameter[5];

            InsertParams[0] = new SqlParameter("@id", SqlDbType.Int, 4, "id");
            InsertParams[1] = new SqlParameter("@first_name", SqlDbType.VarChar, 255, "first_name");
            InsertParams[2] = new SqlParameter("@last_name", SqlDbType.VarChar, 255, "last_name");
            InsertParams[3] = new SqlParameter("@email", SqlDbType.VarChar, 255, "email");
            InsertParams[4] = new SqlParameter("@country", SqlDbType.VarChar, 255, "country");

            foreach (SqlParameter p in InsertParams)
                MyInsert.Parameters.Add(p);

            DA.InsertCommand = MyInsert;

            try
            {
                if (MyConn.State != ConnectionState.Open)
                    MyConn.Open();

                DA.Update(Dt);

                Console.WriteLine("Data imported successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error importing Data: {0}.", ex.Message));
                return false;
            }
			finally
			{
				MyConn.Close();
			}
        }
    }
}
