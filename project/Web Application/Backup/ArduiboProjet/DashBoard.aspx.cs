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


        public void saveTemperatureHumidity()
        {
            while (Query.stopGettingData == false)
            {
                if (Query.getPortName().Length > 0)
                {
                    System.IO.Ports.SerialPort com1 = new System.IO.Ports.SerialPort(Query.getPortName()[0]);
                    string valueTempHumd = String.Empty;
                    string temp = string.Empty;
                    string humd = string.Empty;
                    //string[] valueTsend = getParam();

                    try
                    {
                        com1.Open();
                        com1.BaudRate = 9600;
                        valueTempHumd = (String)com1.ReadLine();
                        if ((valueTempHumd != "") && (valueTempHumd.Contains(";")))
                        {
                            char[] delimiters = new char[] { ';' };
                            temp = valueTempHumd.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)[0];
                            humd = valueTempHumd.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)[1];

                            Query dataManager = new Query();
                            dataManager.thingSpeak(temp, humd);// Using thingSpeak api
                            dataManager.Insert(temp, humd);//Insert data in database
                        }
                        com1.Close();
                    }
                    catch (TimeoutException ex)
                    {
                        lblStartInfo.Visible = true;
                        lblStartInfo.Text = "Failure. Check Ports";
                    }
                }
                else
                {

                }

            }


        }

        protected void ImgStart_Click(object sender, ImageClickEventArgs e)
        {
            Query.stopGettingData = false;
            Query.readThread = new Thread(saveTemperatureHumidity);
            Query.readThread.Start();
            lblStartInfo.Visible = false;
        }

        protected void ImgStop_Click(object sender, ImageClickEventArgs e)
        {
            Query.stopGettingData = true;
            if (Query.readThread.IsAlive)
            {
                Query.readThread.Join();
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string[] portNames = Query.getPortName();

            Query.readThread.Join();

            // Control if there is opened port
            if (portNames.Length > 0)
            {
                System.IO.Ports.SerialPort com1 = new System.IO.Ports.SerialPort(portNames[0]);
                string valueTempHumd = String.Empty;
                string temp = string.Empty;
                string humd = string.Empty;

                try
                {
                    com1.Open();
                    com1.BaudRate = 9600;

                    com1.WriteLine("1");
                    com1.Close();
                }
                catch (TimeoutException ex)
                {
                    lblPush.Text = "Operation failed. Port is closed";
                }
            }
            else
            {
                lblPush.Text = "No ports found.";
            }
        }
    }
}