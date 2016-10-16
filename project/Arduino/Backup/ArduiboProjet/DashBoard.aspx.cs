using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;

namespace ArduiboProjet
{
    public partial class DashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                printValue();

                Timer1.Enabled = true;
                HyperLink1.Text = "WELCOME " + Query.nomUser;
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            printValue();
        }

        //Print the value of température humidity
        protected void printValue()
        {
            string[] param = new Query().getParam();
            string lastTemp = new Query().getTempFromDb();
            string lastHumid = new Query().getHUMDFromDb();

            if (lastHumid == "")
            {
                lastHumid = "0";
            }

            if (lastTemp == "")
            {
                lastTemp = "0";
            }


            lblTemp.Text = lastTemp + "°C";
            lblHumid.Text = lastHumid + "%";

            if (float.Parse(param[0], CultureInfo.InvariantCulture.NumberFormat) < float.Parse(lastTemp, CultureInfo.InvariantCulture.NumberFormat))
            {
                lblTemp.ForeColor = System.Drawing.Color.DarkRed;
            }
            else
            {
                lblTemp.ForeColor = System.Drawing.Color.DarkGreen;
            }

            if (float.Parse(param[2], CultureInfo.InvariantCulture.NumberFormat) < float.Parse(lastHumid, CultureInfo.InvariantCulture.NumberFormat))
            {
                lblHumid.ForeColor = System.Drawing.Color.DarkRed;
            }
            else
            {
                lblHumid.ForeColor = System.Drawing.Color.DarkGreen;
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            new Query().updateAlarm("1");
            
        }
    }
}