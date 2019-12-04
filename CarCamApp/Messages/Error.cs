namespace CarCamApp.Messages
{
    public class Error : Message
    {
        public enum ErrorTypes
        {
            BAD_REQ = 0,
            UNAUTHORIZED = 1,
            SERVER_ERR = 2
        }

        public ErrorTypes ErrorType;
        public string ErrorMsg;

        public Error(ErrorTypes errorType, string errorMsg)
        {
            this.Type = MsgType.ERROR;
            this.ErrorType = errorType;
            this.ErrorMsg = errorMsg;
        }
    }
}
