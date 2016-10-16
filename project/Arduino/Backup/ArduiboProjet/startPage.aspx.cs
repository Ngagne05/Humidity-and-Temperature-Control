using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace ArduiboProjet
{
    public partial class startPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
           
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string name = "";
            name = new Query().isUserExit(username.Text, password.Text);

            if (name != "")
            {
                Query.nomUser= name;
                Response.Redirect("Home.aspx"); 
            }
            else
            {
                lblErreurMessage.Visible = true;
                lblErreurMessage.Text = "Connection failed. Login or passord maybe wrong";
            }
        }

       
    }
}