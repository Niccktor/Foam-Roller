#include <SoftwareSerial.h>

SoftwareSerial bluetooth(2, 3); // TX RX  

int ECG = 60;
int EMG = 10;
char cmd[10];
int i;

void setup() {
  Serial.begin(9600);
  bluetooth.begin(9600);  
  Serial.println("Je suis pret :");
}

void loop() {
  
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
  }
  bluetooth.println("{FC: " + String(ECG) + "}        {EMG: " + String(EMG)+ "}");
  Serial.println("FC: "+ String(ECG) + " EMG: " + String(EMG));
  delay(500);
  /*char c;
  int i;
  while (bluetooth.available() > 0)
  {
    c = bluetooth.read();
    if (c == '{')
    {
      i = bluetooth.readBytesUntil('}', cmd, 10);
      cmd[i] = '\0';
      if (strcmp(cmd, "FC") == 0)
      {
        ECG = random(60, 160);
        bluetooth.println("{FC" + String(ECG)+ "}");
        Serial.println("{FC" + String(ECG)+ "}");
      }
      else if (strcmp(cmd, "EMG") == 0)
      {
        EMG = random(20, 90);
        bluetooth.println("{EMG" + String(EMG)+ "}");
        Serial.println("{EMG" + String(EMG)+ "}");
      }
    }
  }*/
}
