using System;
namespace CarCamApp.Messages
{
    public class GetSSID : Message
    {
        public GetSSID()
        {
            this.Type = MsgType.GET_SSID;
        }
    }
}
