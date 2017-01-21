#include <Servo.h>

const int TIGGER_PIN = 6;
const int ECHO_PIN = 7;
const int SERVO_PIN = 9;
const int TRIGGER_DIST = 10;
float cm;
Servo coinServo;
int servoPos = 0;

void setup() {
  Serial.begin(9600);

  coinServo.attach(SERVO_PIN);
  coinServo.write(160);
  
  pinMode(TIGGER_PIN, OUTPUT);
  pinMode(ECHO_PIN, INPUT);
}

void loop() {
  sendPulse();
  readPulse();
  
  delay(1.0);
}

void sendPulse() {
  digitalWrite(TIGGER_PIN, LOW);
  delayMicroseconds(2);
  digitalWrite(TIGGER_PIN, HIGH);
  delayMicroseconds(10);
  digitalWrite(TIGGER_PIN, LOW);
}

void readPulse() {
  cm = pulseIn(ECHO_PIN, HIGH) / 58.0;

  if (cm < TRIGGER_DIST) {
    Serial.println("TIGGER");
    thankYou();
  }
}

void thankYou() {
  coinServo.write(30);
  delay(5000);
  coinServo.write(160);
}

