using System;
using System.Collections.Generic;
using CarCamApp.Models;
namespace CarCamApp.Messages
{
    public class Ack : Message
    {
        public int? UserID { get; set; }
        public Car[] Cars { get; set; }
        public Dictionary<string, string> Networks { get; set; }

        public Ack()
        {
            this.Type = MsgType.ACK;
        }
    }
}
