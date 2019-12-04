using System;
namespace CarCamApp.Messages
{
    public class RegUser : Message
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public RegUser(string name, string password)
        {
            this.Type = MsgType.REG_USER;
            this.Name = name;
            this.Password = password;
        }
    }
}
