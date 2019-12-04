using System;
namespace CarCamApp.Messages
{
    public class Link : Message
    {
        public int UserID { get; set; }
        public int CarID { get; set; }

        public Link(int carID, int userID)
        {
            this.Type = MsgType.LINK;
            this.CarID = carID;
            this.UserID = userID;
        }
    }
}
