# Humidity-and-Temperature-Control

 

I.	PROJECT DESCRIPTION

The project's goal is to protect objects such as archeological objects, contained in a room, keeping them within a range of temperature and humidity to prevent their degradation.
For this, a data acquisition system will be established (temperature and humidity sensor) to detect the humidity and the temperature of the room.
If the temperature or humidity of the room reaches a certain maximum or minimum threshold then an alarm is triggered and a message is sent to the room manager. It will also be possible to follow the evolution of the temperature and humidity of the room through a web application.

II.	Hardware

     1. Piezo buzzer :
          We use for this project, a piezoelectric audio signaling device: Piezo buzzer.
          It will make different sound depend on the value of the temperature and the humidity.


     2. AM2321 Temperature and humidity sensor:
          AM2321 capacitive humidity sensing digital temperature and humidity sensor is a temperature and humidity own calibration digital signal output composite sensor. 
          The sensor includes a capacitive sensor wet components and a high-precision integrated temperature measurement devices, and connected with a high-performance microprocessor.



          Connexion:
                  AM2321 Connexion:
                                 Gray -> VCC
                                 Blue -> GND
                                 Purple -> SDA
                                 Green -> SCL

                  Piezo Buzzer:
                              GND 
                               9v

III.  Software

We need for this project

•	Arduino

     	     detect the humidity and the temperature of the room
	     an alarm is triggered
	     
•	C#/ ASP.NET for the web application

	     	C#/ ASP.NET for the web application,
     		Create the web App.
     		A message is sent to the room manager
     		possibility to follow the evolution of the temperature and humidity through a web application
     		settings
		
•	Mysql Server as database. 

    		 DataBase name: arduinoproject
     
          Table Param
               ID	               Identification
               tmax	               The maximal temperature value
               tmin 	          The minimal temperature value
               hmax	               The maximal humidity value
               hmin	               The minimum humidity value
               StopAlarm	          Turn to 1 if the alarm is switched off and 0 if not
               Alarm duration	     Contain the duration on which alarm will be switched off.
               Mail	               Contain the mail of the room’s manager

          Table temphumd
               ID	               Identification
               temp	               temperature value
               humd	               humidity value
               Date	               Date of date saving

          Table user
               ID	               Identification
               Name	               Name of the user
               password	          User password


•	IoT ThingSpeak.
	
     		Permit to send the data(temperature and humidity’s values ) to the phone 
     		Phone rings
     		Draw the graph of the Humidity and the temperature over the time
	
IV. Functionalities

This application consist on web application and mobile application, to show the evolution of the temperature and the humidity (through the graphs), and trigger an alarm when they have reached a specified threshold.
When the temperature and humidity have reached a specified threshold then:
	
An alarm is trigged. 

There are different kind of sound: Sound for temperature and sound for humidity.

	A mail is sending to the room’s manager to show him the temperature and the humidity values

	And his phone is ringing to warn him of the temperature or the humidity level. 

The room’s manager can set up the minimum and maximum values of temperature and humidity. 

In case the alarm is trigged, he can stop it fix the problem by increasing or decreasing the temperature or the humidity.
If he stop the Alarm and doesn’t fix the problem, the alarm will be trigged again after 30min.











