#include "Arduino.h"

int old_value = LOW;

void setup() {
  pinMode(7, INPUT);
  Serial.begin(9600);
}

void loop() {
  int value = digitalRead(7);
  if (old_value == LOW && value == HIGH) {
    Serial.write(value);
    old_value = value;
  }
  delay(100);
}
