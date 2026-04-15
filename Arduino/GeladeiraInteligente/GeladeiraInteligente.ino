#include <SPI.h>
#include <Ethernet.h>
#include <DHT.h>

#define DHTTYPE DHT11

#define DHTPIN1 A0
#define DHTPIN2 A1
#define DHTPIN3 A2

DHT dht1(DHTPIN1, DHTTYPE);
DHT dht2(DHTPIN2, DHTTYPE);
DHT dht3(DHTPIN3, DHTTYPE);

byte mac[] = { 0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED };

IPAddress server(192, 168, 0, 100);  // IP do servidor da API

EthernetClient client;

int portaSensor = 7;  // sensor magnético

void setup() {
  Serial.begin(9600);

  pinMode(portaSensor, INPUT);

  Ethernet.begin(mac);

  delay(1000);

  Serial.print("IP Arduino: ");
  Serial.println(Ethernet.localIP());

  dht1.begin();
  dht2.begin();
  dht3.begin();
}

void loop() {
  float t1 = dht1.readTemperature();
  float t2 = dht2.readTemperature();
  float t3 = dht3.readTemperature();

  bool porta = digitalRead(portaSensor);

  String json = "{";
  json += "\"temp_sensor_1\":" + String(t1) + ",";
  json += "\"temp_sensor_2\":" + String(t2) + ",";
  json += "\"temp_sensor_externo\":" + String(t3) + ",";
  json += "\"porta_aberta\":" + String(porta ? "true" : "false");
  json += "}";

  Serial.println("JSON enviado:");
  Serial.println(json);

  if (client.connect(server, 80)) {
    Serial.println("Conectado na API");

    client.println("POST /api_tcc/api/geladeira/dados HTTP/1.1");
    client.println("Host: 192.168.0.100");
    client.println("Content-Type: application/json");
    client.print("Content-Length: ");
    client.println(json.length());
    client.println();
    client.println(json);

  } else {
    Serial.println("ERR001 : Falha ao conectar no servidor");
  }

  client.stop();

  delay(10000);
}