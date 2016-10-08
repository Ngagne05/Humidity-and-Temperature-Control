using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Net;
using System.Threading;

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
        public static Thread readThread;
        //Constructor
        public Query()
        {
            Initialize();
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
            string[] result = new string[4];
            string selectTempQuery = "SELECT tmax,tmin,hmax,hmin FROM param";
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
                }
                CloseConnection();
            }

            return result;
        }

        public void updateSetting(string pTpmax, string pTpmin, string pHmax, string pHmin)
        {
            string query = "UPDATE param SET tmax = '" + pTpmax + "', tmin ='" + pTpmin + "', hmax ='" + pHmax + "', hmin ='" + pHmin + "' WHERE ID = 1";

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