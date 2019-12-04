using System;
namespace CarCamApp.Messages
{
    public class Login : Message
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public Login(string name, string password)
        {
            this.Type = MsgType.LOGIN;
            this.Name = name;
            this.Password = password;
        }
    }
}
