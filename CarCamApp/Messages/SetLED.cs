using System;
namespace CarCamApp.Messages
{
    public class SetLED : Message
    {
        public int State { get; set; }

        public SetLED(int state)
        {
            this.Type = MsgType.SET_LED;
            this.State = state;
        }
    }
}
