using MQTTnet;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System;
using System.Text;

namespace Rabbit_AspNetCore_Utility.IOT
{
    public static class MQTTHelper
    {
        private static IMqttServer mqttServer;
        public static void CreateMQTTServer()
        {
            mqttServer = new MqttFactory().CreateMqttServer();
            var option = new MqttServerOptionsBuilder()
                //客户端验证
                .WithConnectionValidator(c => {
                    if (c.ClientId.Length < 10)
                    {
                        c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedIdentifierRejected;
                        return;
                    }
                    if (c.Username != "mySecretUser")
                    {
                        c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                        return;
                    }
                    if (c.Password != "mySecretPassword")
                    {
                        c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                        return;
                    }
                    c.ReturnCode = MqttConnectReturnCode.ConnectionAccepted;
                })
                //拦截应用消息
                .WithApplicationMessageInterceptor((c)=> {
                    if (MqttTopicFilterComparer.IsMatch(c.ApplicationMessage.Topic, "/myTopic/WithTimes"))
                    {
                        c.ApplicationMessage.Payload = Encoding.UTF8.GetBytes(DateTime.Now.ToString("O"));
                    }
                })
                //拦截订阅
                .WithSubscriptionInterceptor((c) => {
                    if (c.TopicFilter.Topic.StartsWith("admin/foo/bar") && c.ClientId != "theAdmin")
                    {
                        c.AcceptSubscription = false;
                    }
                    if (c.TopicFilter.Topic.StartsWith("the/secret/stuff") && c.ClientId != "Imperator")
                    {
                        c.AcceptSubscription = false;
                        c.CloseConnection = true;
                    }
                })
                .WithDefaultEndpointPort(1884)
                .WithDefaultEndpointBoundIPAddress(new System.Net.IPAddress(Encoding.Default.GetBytes("127.0.0.1")))
                .WithDefaultCommunicationTimeout(new TimeSpan(0,10,0))
                .WithConnectionBacklog(100).Build();

            option.ClientMessageQueueInterceptor = c => {
                Console.WriteLine(c.ReceiverClientId + "," + c.ApplicationMessage.Topic + "," + c.ApplicationMessage.Payload + "," + c.SenderClientId);
            };
            mqttServer.StartAsync(option);
        }

        public static void Publish()
        {
            mqttServer.PublishAsync("topic", "payload");
        }
    }
}
