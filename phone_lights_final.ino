#include <SoftwareSerial.h>
#include <FastLED.h>

SoftwareSerial mySerial(7, 8); //RX, TX

#define num_leds 44
#define data_pin 6

CRGB leds[num_leds];

char character; 
boolean flag = false;

String data0 = "0",
       data1 = "1",
       data2 = "2",
       data3 = "3",
       data4 = "4",
       data5 = "5",
       data6 = "6",
       data7 = "7",
       data8 = "8",
       data9 = "9";
       
int power_pin = 9;

String colorData = "";

int r = 0,
    g = 0,
    b = 0;

void setup() {
  mySerial.begin(9600);
  Serial.begin(6900);

  FastLED.addLeds<NEOPIXEL, data_pin>(leds, num_leds);

  pinMode(data_pin, OUTPUT);
  pinMode(power_pin, OUTPUT);

  mySerial.print("Send data from phone");
}

void turn_off() {
  for (int i = 0; i <= num_leds; i++) {
    leds[i] = CRGB::Black;
  }
  FastLED.show();
}

void turn_white() {
  for (int i = 0; i <= num_leds; i++) {
    leds[i] = CRGB::White;
  }
  FastLED.show();
}

void turn_blue() {
  for (int i = 0; i <= num_leds; i++) {
    leds[i] = CRGB::Blue;
  }
  FastLED.show();
}

void turn_red() {
  for (int i = 0; i <= num_leds; i++) {
    leds[i] = CRGB::Red;
  }
  FastLED.show();
}

void turn_purple() {
  for (int i = 0; i <= num_leds; i++) {
    leds[i] = CRGB::Purple;
  }
  FastLED.show();
}

void turn_green() {
  for (int i = 0; i <= num_leds; i++) {
    leds[i] = CRGB::Green;
  }
  FastLED.show();
}

void turn_yellow() {
  for (int i = 0; i <= num_leds; i++) {
    leds[i] = CRGB::Yellow;
  }
  FastLED.show();
}

void turn_orange() {
  for (int i = 0; i <= num_leds; i++) {
    leds[i] = CRGB::Orange;
  }
  FastLED.show();
}

void turn_pink() {
  for (int i = 0; i <= num_leds; i++) {
    leds[i] = CRGB::Pink;
  }
  FastLED.show();
}

void turn_cyan() {
  for (int i = 0; i <= num_leds; i++) {
    leds[i] = CRGB::Cyan;
  }
  FastLED.show();
}

void loop() {
  digitalWrite(power_pin, HIGH);
  
  String Data = "";
  flag = false;

  while(mySerial.available()) {
    character = mySerial.read();
    colorData = String(character);
    Data.concat(character);
    flag = true;
    delay(100);
  }

  if (flag) {
    Data.trim();
    Serial.println(Data);

    if(colorData[0] == "r") {
      r = colorData.substring(1, colorData.length()).toInt();
    }
    else if (colorData[0] == "g") {
      g = colorData.substring(1, colorData.length()).toInt();
    }
    else if (colorData[0] == "b") {
      b = colorData.substring(1, colorData.length()).toInt();
    }

    for (int i = 0; i <= num_leds; i++) {
      leds[i] = CRGB(r, g, b);
    }
    FastLED.show();

    

    if(Data.equals(data0)) {
      turn_off();
    }
    if(Data.equals(data1)) {
      turn_white();
    }
    if(Data.equals(data2)) {
      turn_blue();
    }
    if(Data.equals(data3)) {
      turn_red();
    }
    if(Data.equals(data4)) {
      turn_purple();
    }
    if(Data.equals(data5)) {
      turn_green();
    }
    if(Data.equals(data6)) {
      turn_yellow();
    }
    if(Data.equals(data7)) {
      turn_orange();
    }
    if(Data.equals(data8)) {
      turn_pink();
    }
    if(Data.equals(data9)) {
      turn_cyan();
    }
  }

}




















