using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Threading;

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
                string alarmDuration = param[5];

                txtMaxTemp.Text = param[0];
                txtMinTemp.Text = param[1];
                txtMaxHumd.Text = param[2];
                txtMinHumd.Text = param[3];
                txtMail.Text = param[6];
                DropDlListeTime.Text = (int.Parse(alarmDuration) / 60).ToString();

                btnApply.CssClass = "btn btn-info";
                HyperLink1.Text = "WELCOME " + Query.nomUser;
            }
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {


            //Control of the state of fields.
            //if there is a empty field or one field contains string then the button turn to red
            if (txtMaxTemp.Text != "" && txtMinTemp.Text != "" && txtMaxHumd.Text != "" && txtMinHumd.Text != "")
            {
                long number1 = 0;

                int alarmeDuration = Int32.Parse(DropDlListeTime.Text) * 60;
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
                                if (!txtMail.Text.Contains("@")||(!txtMail.Text.Contains(".")))
                                {
                                    txtMail.Text = "";
                                    txtMail.Focus();
                                    btnApply.CssClass = "btn btn-danger";
                                } 
                                 else
                                    {
                                    //Data are saved in database et sent to arduino

                                    new Query().updateSetting(txtMaxTemp.Text, txtMinTemp.Text, txtMaxHumd.Text, txtMinHumd.Text, alarmeDuration.ToString(),txtMail.Text);
                             
                                 //sendData();
                                    btnApply.CssClass = "btn btn-success";
                                }
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

    }
}