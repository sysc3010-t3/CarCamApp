using System;
namespace CarCamApp.Messages
{
    public class Move : Message
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Move(int X, int Y)
        {
            this.Type = MsgType.MOVE;
            this.X = X;
            this.Y = Y;
        }
    }
}
