using System;
using CarCamApp.Messages;
using System.Text.RegularExpressions;

namespace CarCamApp.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        private string Password { get; set; }

        public User(string name, string password)
        {
            this.Name = name;
            this.Password = password;
        }

        private bool CheckInformation()
        {
            Regex invalidChar = new Regex(@"[^-_!@\w]");

            return this.Name != null && this.Password != null && !invalidChar.IsMatch(this.Password);
        }

        public void Register()
        {
            if (this.CheckInformation())
            {
                SocketClient client;

                try
                {
                    client = new SocketClient(Constants.ServerIP, Constants.ServerPort);
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException("Unable to create UDP client: " + e.Message);
                }

                client.SetRecvTimeout(Constants.TIMEOUT);
                int attempts = 0;
                while (attempts < Constants.MAX_ATTEMPTS)
                {
                    client.Send(new RegUser(this.Name, this.Password));

                    Message resp = client.Receive();

                    if (resp is Error error)
                    {
                        client.Close();
                        throw new ArgumentException(error.ErrorMsg);
                    }

                    if (resp is Ack ack && ack.UserID.HasValue)
                    {
                        this.ID = ack.UserID.Value;
                        client.Close();
                        return;
                    }

                    attempts++;
                }

                client.Close();
                throw new ServerUnreachableException();
            }

            throw new ArgumentException("Invalid username or password");
        }

        public void Login()
        {
            if (this.CheckInformation())
            {
                SocketClient client;

                try
                {
                    client = new SocketClient(Constants.ServerIP, Constants.ServerPort);
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException("Unable to create UDP client: " + e.Message);
                }

                client.SetRecvTimeout(Constants.TIMEOUT);
                int attempts = 0;
                while (attempts < Constants.MAX_ATTEMPTS)
                {
                    client.Send(new Login(this.Name, this.Password));

                    Message resp = client.Receive();

                    if (resp is Error error)
                    {
                        client.Close();
                        throw new ArgumentException(error.ErrorMsg);
                    }

                    if (resp is Ack ack && ack.UserID.HasValue)
                    {
                        this.ID = ack.UserID.Value;
                        client.Close();
                        return;
                    }

                    attempts++;
                }

                client.Close();
                throw new ServerUnreachableException();
            }

            throw new ArgumentException("Invalid username or password");
        }
    }
}
