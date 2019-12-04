using System;
using CarCamApp.Models;
namespace CarCamApp.Messages
{
    public class Ack : Message
    {
        public int? UserID { get; set; }
        public Car[] Cars { get; set; }
        public string[] Networks { get; set; }

        public Ack()
        {
            this.Type = MsgType.ACK;
        }
    }
}
