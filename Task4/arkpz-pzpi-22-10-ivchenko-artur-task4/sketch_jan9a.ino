#include <ESP8266WiFi.h> 
#include <ArduinoJson.h> 
#include <TinyGPS++.h>   

#define RX_PIN 3  // RX ESP8266
#define TX_PIN 1  // TX ESP8266

// Налаштування Wi-Fi
const char* ssid = "Space Cowboy";      
const char* password = "bambaleila";  

// Параметри гонки та датчика (заглушки)
int raceId = 1;
int sensorId = 42;

// Лінія старт-фінішу (заглушки)
double startFinishLat1 = 50.4501; 
double startFinishLon1 = 30.5234; 
double startFinishLat2 = 50.4505; 
double startFinishLon2 = 30.5238; 

// Поточний стан
int lapNumber = 0;
unsigned long lapStartTime = 0; 
float speed = 0.0;              

// Точки гальмування та прискорення
struct Point {
    double  latitude;
    double  longitude;
};
Point brakingPoints[1000]; 
Point accelerationPoints[1000];
int brakingCount = 0;
int accelerationCount = 0;

WiFiClient client;

TinyGPSPlus gps; 
HardwareSerial gpsSerial(1);

void updateSpeed() {
    if (gps.speed.isUpdated()) {
        speed = gps.speed.kmph(); 
    }
}

// Перевірка перетину лінії старт-фінішу
bool isCrossingStartFinish(float lat, float lon) {
    // Простий спосіб: перевірити, чи координати близькі до лінії
    return (lat >= startFinishLat1 && lat <= startFinishLat2) &&
        (lon >= startFinishLon1 && lon <= startFinishLon2);
}

// Відправка даних на сервер
void sendDataToServer(unsigned long lapTime) {
    // Формуємо JSON
    DynamicJsonDocument doc(1024);
    doc["raceId"] = raceId;
    doc["sensorId"] = sensorId;
    doc["lapTime"] = lapTime;
    doc["lapNumber"] = lapNumber;

    JsonArray brakingArray = doc.createNestedArray("brakingPoints");
    for (int i = 0; i < brakingCount; i++) {
        JsonObject point = brakingArray.createNestedObject();
        point["latitude"] = brakingPoints[i].latitude;
        point["longitude"] = brakingPoints[i].longitude;
    }

    JsonArray accelerationArray = doc.createNestedArray("accelerationPoints");
    for (int i = 0; i < accelerationCount; i++) {
        JsonObject point = accelerationArray.createNestedObject();
        point["latitude"] = accelerationPoints[i].latitude;
        point["longitude"] = accelerationPoints[i].longitude;
    }

    // Серіалізація JSON
    String jsonData;
    serializeJson(doc, jsonData);

    // Відправка через HTTP
    if (client.connect("localhost", 5000)) { 
        client.println("POST /api/v1/Lap HTTP/1.1");
        client.println("Host: localhost");
        client.println("Content-Type: application/json");
        client.print("Content-Length: ");
        client.println(jsonData.length());
        client.println(); 
        client.println(jsonData); 

        while (client.available()) {
            String response = client.readString();
            Serial.println("Відповідь сервера: " + response);
        }
        client.stop();
        Serial.println("Succed");
    }
    else {
        Serial.println("Error HTTP");
    }
}

void setup() {
    Serial.begin(115200);
    gpsSerial.begin(9600); 
    WiFi.begin(ssid, password);

    while (WiFi.status() != WL_CONNECTED) {
        delay(1000);
        Serial.print(".");
    }
    Serial.println("\nWi-Fi connected");
}

void loop() {
    while (gpsSerial.available() > 0) {
        gps.encode(gpsSerial.read()); 
    }

    if (gps.location.isUpdated()) {
        double  latitude = gps.location.lat();
        double  longitude = gps.location.lng();

        static float prevSpeed = 0.0;
        updateSpeed();
        if (speed < prevSpeed) {
            brakingPoints[brakingCount++] = { latitude, longitude };
        }
        else if (speed > prevSpeed) {
            accelerationPoints[accelerationCount++] = { latitude, longitude };
        }
        prevSpeed = speed;

        if (isCrossingStartFinish(latitude, longitude)) {
            if (lapNumber == 0) { // Перше коло
                lapStartTime = millis();
            }
            else {
                unsigned long lapTime = millis() - lapStartTime;
                lapStartTime = millis();

                sendDataToServer(lapTime);

                lapNumber++;
                brakingCount = 0;
                accelerationCount = 0;
            }
        }
    }
    delay(100);
}