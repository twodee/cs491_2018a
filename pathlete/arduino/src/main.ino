#include "Arduino.h"

int old_x = -5;
int old_y = -5;

void setup() {
  pinMode(A0, INPUT);
  pinMode(A1, INPUT);
  Serial.begin(9600);
}

int sign(int value) {
  if (value > 20) {
    return 1;
  } else if (value < -20) {
    return -1;
  } else {
    return 0;
  }
}

void loop() {
  int x = analogRead(A0) - 512;
  int y = analogRead(A1) - 512;

  if (abs(x) > abs(y)) {
    x = sign(x);
    y = 0;
  } else {
    x = 0;
    y = sign(y);
  }

#define println write

  if ((x != 0 || y != 0) && (old_x != x || old_y != y)) {
    Serial.println(x + 1);
    Serial.println(y + 1);

    old_x = x;
    old_y = y;
  }
}
