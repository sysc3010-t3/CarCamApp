using System;
namespace CarCamApp.Messages
{
    public class GetCars : Message
    {
        public int UserID { get; set; }

        public GetCars(int userID)
        {
            this.Type = MsgType.GET_CARS;
            this.UserID = userID;
        }
    }
}
