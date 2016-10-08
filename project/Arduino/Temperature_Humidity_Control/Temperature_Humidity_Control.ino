#include <Wire.h>
#include <AM2321.h>

float maxTemp = 35; //Variable of maximum temperature
float maxHum = 20; //variable of maximum humidity
float minTemp = 5; //variable of minimum temperature
float minHum = 1; //variable of minimun humidity
bool stopAlarm = false; 
String valueToRead; //Value get from serialPort
int chrono = 0; //use to start alarm after few minutes

void setup() {

  Serial.begin(9600);
  pinMode(9, OUTPUT);
}

void loop() {

    float temp; //Variable of temperature value
    float humid; //variable of humidity value
    
    AM2321 am2321;
    am2321.read();

    temp = am2321.temperature/10.0;
    humid = am2321.humidity/10.0;
    
    int dataToread = Serial.available(); //Reading of caracters number in the buffer
    

    if(dataToread > 0) //if the buffer is not empty
    {
         valueToRead = Serial.readString();// get value from serial port
    }

    if (valueToRead.length() !=0)
    {
      if (valueToRead.length() > 1) //parameters values are sent
      { 
        // We get the values of parameters
        String tempMaxRead = String(valueToRead.charAt(0));
        tempMaxRead.concat(String(valueToRead.charAt(1)));
        String tempMinRead = String(valueToRead.charAt(3));
        tempMinRead.concat(String(valueToRead.charAt(4)));
        String humidMaxRead = String(valueToRead.charAt(6));
        humidMaxRead.concat(String(valueToRead.charAt(7)));
        String humidMinRead = String(valueToRead.charAt(9));
        humidMinRead.concat(String(valueToRead.charAt(10)));

        maxTemp = tempMaxRead.toInt();
        minTemp = tempMinRead.toInt();
        maxHum = humidMaxRead.toInt();
        minHum = humidMinRead.toInt();
        
      }
      else // alarm is stopped
      {
        stopAlarm = true;
      }
    }


if (stopAlarm == true)
{
  noTone(9);
  chrono++; // Begining of the chrono to start alarm again if the temperature or the humidity is not normal
}
else
{

  if ((temp<=maxTemp and temp>=minTemp) and (humid<=maxHum and humid>=minHum))
  {
    noTone(9);// No ring when the temperature and the humidity not exced 30 and 50
   }
  else
  {
    if ((temp>maxTemp or temp < minTemp) and (humid>maxHum or humid < minHum))
    {
      tone(9,400);
    }
    else
    {
      if (temp>maxTemp or temp<minTemp)
      {
        noTone(9);
        tone(9,400,600);
      }
      if (humid>maxHum or humid<minHum)
      {
        noTone(9);
        tone(9,200,100);
      }
    }
  }
}

if (chrono==360) // After 30 minutes if the temperature or humidity not normal then alarm is turn on
{
  stopAlarm = false;
  chrono = 0;
}

Serial.print(temp);
Serial.print(";");
Serial.print(humid);
Serial.println();
delay(5000);
}

