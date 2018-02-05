#include "Arduino.h"
#define THRESHOLD 5

int old_reading = 0;

void setup() {
  Serial.begin(9600);
}

void loop() {
  int reading = analogRead(A5);
  if (abs(old_reading - reading) > THRESHOLD) {
    int mapped = map(reading, 0, 1023, 0, 255);
    Serial.write(mapped);
    old_reading = reading;
  }
}
