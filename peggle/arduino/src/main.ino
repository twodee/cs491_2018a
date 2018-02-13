#include "Arduino.h"
#define THRESHOLD 5

/* #define write println */

int old_potentiometer;
int old_button = LOW;

void setup() {
  Serial.begin(9600);
  pinMode(7, INPUT);

  emitPotentiometer(analogRead(A5));
}

void emitPotentiometer(int potentiometer) {
  int byte = map(potentiometer, 0, 1023, 0, 255);
  Serial.write(0);
  Serial.write(byte);
  old_potentiometer = potentiometer;
}

void loop() {
  int potentiometer = analogRead(A5);
  if (abs(old_potentiometer - potentiometer) > THRESHOLD) {
    emitPotentiometer(potentiometer);
  }

  int button = digitalRead(7);
  if (button != old_button) {
    if (button == LOW) {
      Serial.write(1);
      Serial.write(1);
    }
    old_button = button;
    delay(10);
  }
}
