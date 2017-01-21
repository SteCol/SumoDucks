 const unsigned X_AXIS_1_PIN = 2;
const unsigned Y_AXIS_1_PIN = 1;
const unsigned Z_AXIS_1_PIN = 0;
const int STATUS_LED_1 = 3;
const int STATUS_BTN_1 = 2;
int STATUS_1 = 0;
int STATUS_BTN_VALUE_1 = 0;

const unsigned X_AXIS_2_PIN = 5;
const unsigned Y_AXIS_2_PIN = 4;
const unsigned Z_AXIS_2_PIN = 3;
const int STATUS_LED_2 = 6;
const int STATUS_BTN_2 = 5;
int STATUS_2 = 0;
int STATUS_BTN_VALUE_2 = 0;

void setup() {
  Serial.begin(9600);

  pinMode(STATUS_LED_1, OUTPUT);
  pinMode(STATUS_BTN_1, INPUT);
  
  pinMode(STATUS_LED_2, OUTPUT);
  pinMode(STATUS_BTN_2, INPUT);
}

void loop() {
  checkButtonStatus();
  
  if (STATUS_1 == HIGH) {
    readSensor1Values();
  }

  if (STATUS_2 == HIGH) {
    readSensor2Values();
  }

  delay(100);
}

void readSensor1Values() {
  int x_axis_1 = analogRead(X_AXIS_1_PIN);
  int y_axis_1 = analogRead(Y_AXIS_1_PIN);
  int z_axis_1 = analogRead(Z_AXIS_1_PIN);
  Serial.println("X1:" + (String)x_axis_1 + ";Y1:" + (String)y_axis_1 + ";Z1:" + (String)z_axis_1);
}

void readSensor2Values() {
  int x_axis_2 = analogRead(X_AXIS_2_PIN);
  int y_axis_2 = analogRead(Y_AXIS_2_PIN);
  int z_axis_2 = analogRead(Z_AXIS_2_PIN);
  Serial.println("X2:" + (String)x_axis_2 + ";Y2:" + (String)y_axis_2 + ";Z2:" + (String)z_axis_2);
}

void checkButtonStatus() {
  STATUS_BTN_VALUE_1 = digitalRead(STATUS_BTN_1);
  if (STATUS_BTN_VALUE_1 == HIGH) {
    delay(1000);
    if (STATUS_1 == HIGH) {
      STATUS_1 = LOW;
      digitalWrite(STATUS_LED_1, LOW);
    } else {
      STATUS_1 = HIGH;
      digitalWrite(STATUS_LED_1, HIGH);
    }
  }

  STATUS_BTN_VALUE_2 = digitalRead(STATUS_BTN_2);
  if (STATUS_BTN_VALUE_2 == HIGH) {
    delay(1000);
    if (STATUS_2 == HIGH) {
      STATUS_2 = LOW;
      digitalWrite(STATUS_LED_2, LOW);
    } else {
      STATUS_2 = HIGH;
      digitalWrite(STATUS_LED_2, HIGH);
    }
  }
}

