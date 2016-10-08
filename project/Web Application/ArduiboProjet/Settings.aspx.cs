using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace ArduiboProjet
{
    public partial class Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                // Parameters data getting
                string[] param = new Query().getParam();
                txtMaxTemp.Text = param[0];
                txtMinTemp.Text = param[1];
                txtMaxHumd.Text = param[2];
                txtMinHumd.Text = param[3];
                btnApply.CssClass = "btn btn-info";
            }
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            //Control of the state of fields.
            //if there is a empty field or one field contains string then the button turn to red
            if (txtMaxTemp.Text != "" && txtMinTemp.Text != "" && txtMaxHumd.Text != "" && txtMinHumd.Text != "")
            {
                long number1 = 0;
                bool canConvert1 = long.TryParse(txtMaxTemp.Text, out number1);
                bool canConvert2 = long.TryParse(txtMinTemp.Text, out number1);
                bool canConvert3 = long.TryParse(txtMaxHumd.Text, out number1);
                bool canConvert4 = long.TryParse(txtMinHumd.Text, out number1);
                if ((canConvert1 == false) || (txtMaxTemp.Text == ""))
                {
                    txtMaxTemp.Text = "";
                    txtMaxTemp.Focus();
                    btnApply.CssClass = "btn btn-danger";
                }
                else
                {
                    if ((canConvert2 == false) || (txtMinTemp.Text == ""))
                    {
                        txtMinTemp.Text = "";
                        txtMinTemp.Focus();
                        btnApply.CssClass = "btn btn-danger";
                    }
                    else
                    {
                        if ((canConvert3 == false) || (txtMaxHumd.Text == ""))
                        {
                            txtMaxHumd.Text = "";
                            txtMaxHumd.Focus();
                            btnApply.CssClass = "btn btn-danger";
                        }
                        else
                        {
                            if ((canConvert4 == false) || (txtMinHumd.Text == ""))
                            {
                                txtMinHumd.Text = "";
                                txtMinHumd.Focus();
                                btnApply.CssClass = "btn btn-danger";
                            }
                            else
                            {
                                //Data are saved in database et sent to arduino
                                new Query().updateSetting(txtMaxTemp.Text, txtMinTemp.Text, txtMaxHumd.Text, txtMinHumd.Text);
                                //sendData(txtMaxTemp.Text + "|" + txtMinTemp.Text + "|" + txtMaxHumd.Text + "|" + txtMinHumd.Text);
                                btnApply.CssClass = "btn btn-success";
                            }
                        }
                    }
                }

            }
            else
            {
                btnApply.CssClass = "btn btn-danger";
            }
        }


        //Send data to arduino
        //protected void sendData(string dataSend)
        //{
        //    s

        //    // Control if there is opened port
        //    if (portNames.Length > 0)
        //    {
        //        System.IO.Ports.SerialPort com1 = new System.IO.Ports.SerialPort(portNames[0]);
        //        string valueTempHumd = String.Empty;
        //        string temp = string.Empty;
        //        string humd = string.Empty;

        //        try
        //        {
        //            com1.Open();
        //            com1.BaudRate = 9600;

        //            com1.WriteLine(dataSend);
        //            com1.Close();
        //        }
        //        catch (TimeoutException ex)
        //        {
        //            lblErrorMessage.Text = "Operation failed. Port is closed";
        //        }
        //    }
        //    else
        //    {
        //        lblErrorMessage.Text = "No ports found.";
        //    }
        //}
    }
}