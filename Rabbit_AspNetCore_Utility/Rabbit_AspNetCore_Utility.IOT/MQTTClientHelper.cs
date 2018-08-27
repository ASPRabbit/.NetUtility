using MQTTnet;
using MQTTnet.Client;
using System;

namespace Rabbit_AspNetCore_Utility.IOT
{
    public class MQTTClientHelper
    {
        private static IMqttClient mqttClient;
        public MQTTClientHelper()
        {

        }

        public static void CreateMQTTClient()
        {
            mqttClient = new MqttFactory().CreateMqttClient();
            var options = new MqttClientOptionsBuilder()
                            .WithCleanSession()
                            .WithProtocolVersion(MQTTnet.Serializer.MqttProtocolVersion.V311)
                            .WithClientId("Client1")
                            .WithTcpServer("broker.hivemq.com", 1884)
                            .WithCredentials("uid", "pwd")
                            .Build();
            //处理传入消息
            mqttClient.ApplicationMessageReceived += (s, e) => {
                Console.WriteLine(e.ApplicationMessage.Topic + "," + e.ApplicationMessage.Payload);
            };
            mqttClient.Connected += (sc,ec)=> {
                Console.WriteLine("已连接服务器");
            };
            mqttClient.Disconnected += (dc, de) => {
                Console.WriteLine("已断开连接");
            };
        }

        public static void PublishMessage()
        {
            if (mqttClient != null)
            {
                if (mqttClient.IsConnected)
                {
                    mqttClient.PublishAsync("topic", "payload");
                }
            }
        }

        public static void Subscribe()
        {
            if (mqttClient != null)
            {
                if (mqttClient.IsConnected)
                {
                    /// AtMostOnce 即发即忘 无法保证客户端是否收到
                    /// AtLeastOnce 保证消息至少一次传递给客户端
                    /// ExactlyOnce保证消息仅有订阅客户端接收一次
                    mqttClient.SubscribeAsync("topic", MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce);
                }
            }
        }
    }
}
