using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Net;
using System.Threading;
using System.Net.Mail;


namespace ArduiboProjet
{
    public class Query
    {
        public MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        public static bool stopGettingData { get; set; }
        public static int mailSendTime = 0;
        public static Thread readThread;
        public static string nomUser { get; set; }
        //Constructor
        public Query()
        {
            Initialize();
        }

        private  void sendEmail(string temperature,string humidity)
        {
            string MailReceiver = getParam()[6];
            MailMessage email = new MailMessage();
            email.From = new System.Net.Mail.MailAddress("dembacoco@yahoo.com");
            string to = MailReceiver;
            email.To.Add(new MailAddress(to));
            email.IsBodyHtml = true;
            email.Subject = "Temperature[" + temperature + "] Humidity[" + humidity + "]";
            email.Body = "Temperature[" + temperature + "] : Humidity[" + humidity + "]";

            SmtpClient smtpClient = new SmtpClient("smtp.mail.yahoo.fr", 587);

            smtpClient.Credentials = new System.Net.NetworkCredential("dembacoco", "demba-coco");
            smtpClient.EnableSsl = true;
            try
            {
                smtpClient.Send(email);

                //   Console.WriteLine("email envoyé");
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void getTimeTosendMail()
        {
            
            string temp; 
            string hum;
            string tempmax;
            string humMax;
            string[] param = getParam();

            if (getTempFromDb().Length<4)
            {
                temp = getTempFromDb().Substring(0,1);

            }
            else{
                temp = getTempFromDb().Substring(0,2);
            }

            if (getHUMDFromDb().Length < 4)
            {
                hum = getHUMDFromDb().Substring(0, 1);

            }
            else
            {
                hum = getHUMDFromDb().Substring(0, 2);
            }

            if (param[0].Length < 1)
            {
                tempmax = param[0].Substring(0, 1);

            }
            else
            {
                tempmax = param[0].Substring(0, 2);
            }

            if (param[2].Length < 1)
            {
                humMax = param[2].Substring(0, 1);

            }
            else
            {
                humMax = param[2].Substring(0, 2);
            }

            if (((int.Parse(temp) > int.Parse(param[0])) || (int.Parse(hum) > int.Parse(param[2]))) || ((int.Parse(temp) > int.Parse(param[0])) && (int.Parse(hum) > int.Parse(param[2]))))
            {

                if (mailSendTime == 0)
                {
                    sendEmail(temp, hum);
                }
                mailSendTime++;
                if (mailSendTime == 36000)
                {
                    mailSendTime = 0;
                }
            }
            else
            {
                mailSendTime = 0;
            }
           
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "arduinoprojet";
            uid = "admin";
            password = "admin";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:

                        break;

                    case 1045:

                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        //Insert statement
        public void Insert(string pTemp, string pHumd)
        {
            DateTime thisDay = DateTime.Today;
            string query = "Insert into temphumd(HUMD,TEMP,DATE) values ('" + pHumd + "','" + pTemp + "','" + thisDay + "')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update()
        {
        }

        //Delete statement
        public void Delete()
        {
        }

        //Backup
        public void Backup()
        {
        }

        //Restore
        public void Restore()
        {
        }

        // Get the last data of temperature in the database
        public string getTempFromDb()
        {
            string result = string.Empty;
            string selectTempQuery = "SELECT TEMP FROM temphumd WHERE ID=(SELECT MAX(ID) FROM temphumd)";
            MySqlCommand sqlCommand;
            MySqlDataReader SqlReader;

            if (this.OpenConnection() == true)
            {
                sqlCommand = new MySqlCommand(selectTempQuery, connection);
                SqlReader = sqlCommand.ExecuteReader();

                while (SqlReader.Read())
                {
                    result = SqlReader[0].ToString();
                }
                CloseConnection();
            }

            return result;
        }

        //Get the parameters values
        public string[] getParam()
        {
            string[] result = new string[7];
            string selectTempQuery = "SELECT tmax,tmin,hmax,hmin,StopAlarme,Alarmduration,Mail FROM param";
            MySqlCommand sqlCommand;
            MySqlDataReader SqlReader;


            if (this.OpenConnection() == true)
            {
                sqlCommand = new MySqlCommand(selectTempQuery, connection);
                SqlReader = sqlCommand.ExecuteReader();

                while (SqlReader.Read())
                {
                    result[0] = SqlReader["tmax"].ToString();
                    result[1] = SqlReader["tmin"].ToString();
                    result[2] = SqlReader["hmax"].ToString();
                    result[3] = SqlReader["hmin"].ToString();
                    result[4] = SqlReader["StopAlarme"].ToString();
                    result[5] = SqlReader["Alarmduration"].ToString();
                    result[6] = SqlReader["Mail"].ToString();
                }
                CloseConnection();
            }

            return result;
        }

        public void updateAlarm(string pAlarmeState)
        {
            string query = "UPDATE param SET StopAlarme = '" + pAlarmeState + "' WHERE ID = 1";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        public void updateSetting(string pTpmax, string pTpmin, string pHmax, string pHmin, string alarmeduration,string mail)
        {
            string query = "UPDATE param SET tmax = '" + pTpmax + "', tmin ='" + pTpmin + "', hmax ='" + pHmax + "', hmin ='" + pHmin + "', Alarmduration='" + alarmeduration + "',Mail='"+ mail +"' WHERE ID = 1";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        // Get the name of the opened ports
        public static string[] getPortName()
        {
            string[] portName = System.IO.Ports.SerialPort.GetPortNames();
            return portName;
        }

        //Get the humidity from the database
        public string getHUMDFromDb()
        {
            string result = string.Empty;
            string selectTempQuery = "SELECT HUMD FROM temphumd WHERE ID=(SELECT MAX(ID) FROM temphumd)";
            MySqlCommand sqlCommand;
            MySqlDataReader SqlReader;

            if (this.OpenConnection() == true)
            {
                sqlCommand = new MySqlCommand(selectTempQuery, connection);
                SqlReader = sqlCommand.ExecuteReader();

                while (SqlReader.Read())
                {
                    result = SqlReader[0].ToString();
                }
                CloseConnection();
            }

            return result;
        }


        //Send data to arduino
        public string sendData()
        {
            string[] param = getParam();
            string tempMax, tempMin, humidMax, humidMin, dataTosend, alarmState, alarmduration;


            if (param[0].Length == 1)
            {
                tempMax = "0" + param[0];
            }
            else
            {
                tempMax = param[0];
            }

            if (param[1].Length == 1)
            {
                tempMin = "0" + param[1];
            }
            else
            {
                tempMin = param[1];
            }

            if (param[2].Length == 1)
            {
                humidMax = "0" + param[2];
            }
            else
            {
                humidMax = param[2];
            }

            if (param[3].Length == 1)
            {
                humidMin = "0" + param[3];
            }
            else
            {
                humidMin = param[3];
            }

            alarmState = param[4];

            alarmduration = param[5];

            dataTosend = tempMax + "|" + tempMin + "|" + humidMax + "|" + humidMin + "|" + alarmState + "|" + alarmduration;

            return dataTosend;

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
                    string alarmState = string.Empty;
                    string dataToArduino = new Query().sendData();//Contain the data we will send to arduino
                    

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
                            alarmState = valueTempHumd.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)[2];

                            if (alarmState == "1")
                            {
                                new Query().updateAlarm("0");//Saving alarmestate
                            }

                            Query dataManager = new Query();
                            dataManager.thingSpeak(temp, humd);// Using thingSpeak api
                            dataManager.Insert(temp, humd);//Insert data in database
                            getTimeTosendMail();

                        }
                        com1.WriteLine(dataToArduino);
                        com1.Close();
                    }
                    catch (TimeoutException ex)
                    {
                        
                    }
                }
                else
                {

                }

            }


        }

        // Return the name of the user if the login and password is ok
        public string isUserExit(string login,string password)
        {
            string result = string.Empty;
            string selectTempQuery = "SELECT Name FROM user WHERE Login='" + login + "' and Password = '" + password + "'";
            MySqlCommand sqlCommand;
            MySqlDataReader SqlReader;

            if (this.OpenConnection() == true)
            {
                sqlCommand = new MySqlCommand(selectTempQuery, connection);
                SqlReader = sqlCommand.ExecuteReader();

                while (SqlReader.Read())
                {
                    result = SqlReader[0].ToString();
                }
                CloseConnection();
            }

            return result;
        }


        //Using of thingSpeak api to send data online
        public void thingSpeak(string temp, string humid)
        {
            try
            {

                const string WRITEKEY = "IE12SVCQ9S9E1N97";
                string strUpdateBase = "http://api.thingspeak.com/update";
                string strUpdateURI = strUpdateBase + "?key=" + WRITEKEY;
                string strField1 = temp;
                string strField2 = humid;
                HttpWebRequest ThingsSpeakReq;
                HttpWebResponse ThingsSpeakResp;

                strUpdateURI += "&field1=" + strField1;
                strUpdateURI += "&field2=" + strField2;

                ThingsSpeakReq = (HttpWebRequest)WebRequest.Create(strUpdateURI);

                ThingsSpeakResp = (HttpWebResponse)ThingsSpeakReq.GetResponse();

                if (!(string.Equals(ThingsSpeakResp.StatusDescription, "OK")))
                {
                    Exception exData = new Exception(ThingsSpeakResp.StatusDescription);
                    throw exData;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}