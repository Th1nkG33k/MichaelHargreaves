using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class imports : System.Web.UI.Page
{
    private const string DB_CONN = "Data Source=MICHAEL-HP\\SQLEXPRESS;Initial Catalog=RewardsForRacing;Integrated Security=True";
    private DataTable Dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Uname"] == null || Session["PWord"] == null)
            Response.Redirect("Login.aspx");

        if (Session["Uname"].ToString() != "admin" && Session["PWord"].ToString() != "Password1")
            Response.Redirect("Login.aspx");

        if (!Page.IsPostBack)
        {
            GetData();
            Session["Dt"] = Dt;
        }
        else
            Dt = (DataTable)Session["Dt"];

        rptImports.DataSource = Dt;
        rptImports.DataBind();
    }

    protected void rptImports_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView dr = (DataRowView)e.Item.DataItem;

            Literal ID = (Literal)e.Item.FindControl("litID");
            Literal FName = (Literal)e.Item.FindControl("litFName");
            Literal LName = (Literal)e.Item.FindControl("litLName");
            Literal Email = (Literal)e.Item.FindControl("litEmail");
            Literal Country = (Literal)e.Item.FindControl("litCountry");
            LinkButton Del = (LinkButton)e.Item.FindControl("btnCommand");

            ID.Text = dr["id"].ToString();
            FName.Text = dr["first_name"].ToString();
            LName.Text = dr["last_name"].ToString();
            Email.Text = string.Format("<a href='mailto:{0}'>{0}</a>", dr["email"].ToString());
            Country.Text = dr["country"].ToString();

            Del.CommandArgument = e.Item.ItemIndex.ToString();
        }
        
    }

    protected void rptImports_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Dt = (DataTable)Session["Dt"];

        if (e.CommandName == "delete")
        {
            if (e.CommandArgument != null)
            {
                int idx = Convert.ToInt32(e.CommandArgument);
                
                Dt.Rows[idx].Delete();

                Session["Dt"] = Dt;

                rptImports.DataSource = Dt;
                rptImports.DataBind();
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("imports.aspx");
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        try
        {
            CommitChanges();

            Session["Dt"] = Dt;

            rptImports.DataSource = Dt;
            rptImports.DataBind();

            lblMessage.Text = "Changes commited to database successfully.";
            lblMessage.Style["color"] = "green";
        }
        catch (Exception ex)
        {
            lblMessage.Text = string.Format("Error commiting changes to database: {0}", ex.Message);
            lblMessage.Style["color"] = "red";

            Dt = (DataTable)Session["Dt"];

            rptImports.DataSource = Dt;
            rptImports.DataBind();
        }
    }

    private void GetData()
    {
        Dt = new DataTable("Imports");

        SqlConnection MyConn = new SqlConnection(DB_CONN);
        SqlCommand MySelect = new SqlCommand("SELECT id, first_name, last_name, email, country FROM Imports", MyConn);
        SqlDataAdapter DA = new SqlDataAdapter();

        DA.SelectCommand = MySelect;

        try
        {
            if (MyConn.State != ConnectionState.Open)
                MyConn.Open();

            DA.Fill(Dt);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            MyConn.Close();
        }
    }

    private void CommitChanges()
    {
        Dt = (DataTable)Session["Dt"];

        SqlConnection MyConn = new SqlConnection(DB_CONN);
        SqlDataAdapter DA = new SqlDataAdapter();

        //SqlCommand MyUpdate = new SqlCommand("UPDATE Imports Set first_name = @first_name, last_name = @last_name, email = @email, country = @country WHERE id = @id", MyConn);
        SqlCommand MyDelete = new SqlCommand("DELETE FROM Imports WHERE id = @id", MyConn);
        
        //SqlParameter[] UpdateParams = new SqlParameter[5];
        SqlParameter DeleteParam = new SqlParameter();

        //UpdateParams[0] = new SqlParameter("@id", SqlDbType.Int, 4, "id");
        //UpdateParams[1] = new SqlParameter("@first_name", SqlDbType.VarChar, 255, "first_name");
        //UpdateParams[2] = new SqlParameter("@last_name", SqlDbType.VarChar, 255, "last_name");
        //UpdateParams[3] = new SqlParameter("@email", SqlDbType.VarChar, 255, "email");
        //UpdateParams[4] = new SqlParameter("@country", SqlDbType.VarChar, 255, "country");
        
        DeleteParam = new SqlParameter("@id", SqlDbType.Int, 4, "id");

        //foreach (SqlParameter p in UpdateParams)
        //    MyUpdate.Parameters.Add(p);

        MyDelete.Parameters.Add(DeleteParam);

        //DA.UpdateCommand = MyUpdate;
        DA.DeleteCommand = MyDelete;

        try
        {
            if (MyConn.State != ConnectionState.Open)
                MyConn.Open();

            DA.Update(Dt);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            MyConn.Close();
        }
    }
}