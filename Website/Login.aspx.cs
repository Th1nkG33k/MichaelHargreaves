using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    private string _pwd = "Password1";
    private string _uname = "admin";

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string UName = txtUsername.Text;
        string PWord = txtPassword.Text;

        if (UName != _uname && PWord != _pwd)
            lblError.Text = "Username or Password incorrect.  Please re-enter and try again.";
        else
        {
            Session["PWord"] = PWord;
            Session["Uname"] = UName;
            Response.Redirect("Imports.aspx");
        }
            
    }
}