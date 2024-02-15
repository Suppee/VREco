// Wifi Refs
#include <WiFiNINA.h>
#include <WiFiUdp.h>

// RFID Refs
#include "SPI.h"
#include "MFRC522.h"

// RFID Variables
#define SS_PIN 10
#define RST_PIN 9

bool card = false;

MFRC522 rfid(SS_PIN, RST_PIN);

MFRC522::MIFARE_Key key;

//the initial wifi status
int status = WL_IDLE_STATUS;

//your network name (SSID) and password (WPA)
char ssid[] = "VRECO";            
char pass[] = "12345678VR"; 

//WiFiUDP object used for the communication
WiFiUDP Udp;

//local port to listen on
int localPort = 3002;                               

//IP and port for the server
IPAddress serverIPAddress (34, 88, 170, 131);
int serverPort = 3001;       

//potentiometer
int potPin = A0;
int potValue;

//LED
int LEDPin = 3;

//setup: runs only once
void setup() {
  
  
  //begin serial and await serial
  Serial.begin(9600);
  while (!Serial);

  SPI.begin();
  rfid.PCD_Init();
  Serial.println("I am waiting for card...");
  card = false;

  //check the WiFi module
  if (WiFi.status() == WL_NO_MODULE) {
    Serial.println("Communication with WiFi module failed!");
    
    //don't continue
    while (true);
  }

  //attempt to connect to WiFi network
  while (status != WL_CONNECTED) {

    //connect to WPA/WPA2 network
    Serial.print("Attempting to connect to SSID: ");
    Serial.println(ssid);
    status = WiFi.begin(ssid, pass);

    //wait 10 seconds for connection
    delay(10000);
  }
  
  Serial.println("Connected to WiFi");
  
  //if you get a connection, report back via serial:
  Udp.begin(localPort);

  //set pinModes
  pinMode(potPin, INPUT);
  pinMode(LEDPin, OUTPUT);

 
  SPI.begin();
  rfid.PCD_Init();
  Serial.println("I am waiting for card...");
  card = false;
}

//loop: runs forever
void loop() {


  delay(100);
  //Serial.println(card);
  //Serial.println(rfid.PICC_IsNewCardPresent());
  //Serial.println(rfid.PICC_ReadCardSerial());
  // put your main code here, to run repeatedly:

  if (!rfid.PICC_IsNewCardPresent() )
  {
    if (card == true) Serial.println("No Card");
    //Serial.println("No Card");
    card = false;
    return;
  }

  if (!rfid.PICC_ReadCardSerial())
  {
    //Serial.println("No Card");
    return;
  }


  // PICC TYPE INFO

  //Serial.print(F("PICC type: "));
  MFRC522::PICC_Type piccType = rfid.PICC_GetType(rfid.uid.sak);
  //Serial.println(rfid.PICC_GetTypeName(piccType));

  // Check if the PICC of Classic MIFARE type
  // if (piccType != MFRC522::PICC_TYPE_MIFARE_MINI &&
  //     piccType != MFRC522::PICC_TYPE_MIFARE_1K &&
  //     piccType != MFRC522::PICC_TYPE_MIFARE_4K) {
  //   Serial.println(F("Your tag is not of type MIFARE Classic."));
  //   return;
  // }


  String strID = "";
  for (byte i = 0; i < 4; i++) {
    strID +=
      (rfid.uid.uidByte[i] < 0x10 ? "0" : "") +
      String(rfid.uid.uidByte[i], HEX) +
      (i != 3 ? ":" : "");
  }

  
  strID.toUpperCase();
    Serial.println(strID);
    delay(500);
    card = true;
    sendUDPMessage(serverIPAddress, serverPort, String(strID));

  
   // readPotentiometer();
}


//sends a message to the server (UDP)
void sendUDPMessage(IPAddress remoteIPAddress, int remoteport, String message) {
  Serial.println("sendUDPMessageToServer");

  //get message string length (+1 to store a null value indicating the end of the message)
  int messageLength = message.length() + 1;
  
  //create char array 
  char messageBuffer[messageLength];

  //copy string message to char array
  message.toCharArray(messageBuffer, messageLength);

  //send the packet to the server
  Udp.beginPacket(remoteIPAddress, remoteport);
  Udp.write(messageBuffer);
  Udp.endPacket(); 
}

void readPotentiometer() {
  int currentPotValue = analogRead(potPin);
  
  if(potValue+1 < currentPotValue || potValue-1 > currentPotValue) {
    potValue = currentPotValue;
    
    Serial.print("\npot val : ");
    Serial.println(String(potValue));
    
    sendUDPMessage(serverIPAddress, serverPort, String(potValue));
  }
}

void setLEDTo(int LEDValue) {
  Serial.print("set LED to: ");
  Serial.println(LEDValue);
  digitalWrite(LEDPin, LEDValue);
}
