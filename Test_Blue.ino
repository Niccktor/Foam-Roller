#include <SoftwareSerial.h>

SoftwareSerial bluetooth(2, 3); // RX TX 

int ECG = 60;
int EMG = 10;
int str;
int i;
char payload; 

void setup() {
  Serial.begin(9600);
  bluetooth.begin(9600);
}

void loop() {
  if (bluetooth.available() > 0) 
  {
    str = bluetooth.read();
    Serial.print((char)str);
    str = 0;
  }
  payload = '\0';
  if (Serial.available() > 0)         // Test si il y a une information sur le port serie
  {
    payload = Serial.read();          // lit le port serie et :'inscrit dans payload
  }
  if (payload != '\0')                // Permet de ne pas flood le canal de communication
  {
          bluetooth.print(payload);      // Ecrit sur le port serie du ZigBee pour pouvoir communiquer.
          Serial.print(payload);            // Ecrire sur le port serie pour l'afficher a l'utilisateur
  }
  
  
  /*
  if (ECG < 70)
  {
      ECG += 10;
  }
  else
  {
    ECG += random(-10, 10);
  }
    if (EMG < 70)
  {
      EMG += 10;
  }
  else
  {
    EMG += random(-10, 10);
  }*/
  //str = ("FC: " + String(ECG) + " EMG: " + String(EMG) + "\n");
  //i = str.length();
  //bluetooth.println(str);
  //Serial.print("Sizeof : " + String(i));
 // Serial.println(" FC: " + String(ECG) + " EMG: " + String(EMG));
  
  //delay(500);
}
