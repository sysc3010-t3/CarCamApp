using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using CarCamApp.Messages;

namespace CarCamApp.Models
{
    public class SocketClient
    {
        private UdpClient Client { get; set; }

        private const int CLIENT_PORT = 8080;

        private IPEndPoint iep;

        public SocketClient(string destIP, int destPort)
        {
            try
            {
                this.Client = new UdpClient(CLIENT_PORT);
                this.iep = new IPEndPoint(IPAddress.Parse(destIP), destPort);
            }
            catch (Exception e)
            {
                throw new System.InvalidOperationException(e.Message);
            }
        }

        public void Send(Message msg)
        {
            byte[] msgBytes = Encoding.UTF8.GetBytes(msg.ToString());
            try
            {
                Client.Send(msgBytes, msgBytes.Length, this.iep);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        public Message Receive()
        {
            Message obj;

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };

            byte[] msgBytes;

            try
            {
                msgBytes = Client.Receive(ref this.iep);
            }
            catch (SocketException)
            {
                return null;
            }

            string msgStr = Encoding.UTF8.GetString(msgBytes);

            try
            {
                obj = JsonConvert.DeserializeObject<Message>(msgStr, settings);

                switch (obj.Type)
                {
                    case Message.MsgType.ACK:
                        return JsonConvert.DeserializeObject<Ack>(msgStr, settings);
                    case Message.MsgType.ERROR:
                        return JsonConvert.DeserializeObject<Error>(msgStr, settings);
                    default:
                        this.Send(new Error(
                            Error.ErrorTypes.BAD_REQ,
                            "invalid request type: "+obj.Type
                        ));
                        break;
                }
            }
            catch (JsonException e)
            {
                this.Send(new Error(
                    Error.ErrorTypes.BAD_REQ,
                    "invalid JSON: " + e.Message
                ));
            }

            return null;
        }

        public void Close()
        {
            this.Client.Close();
        }

        public void SetRecvTimeout(int milliseconds)
        {
            Client.Client.ReceiveTimeout = milliseconds;
        }
    }
}
