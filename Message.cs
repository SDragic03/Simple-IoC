using IoCWebApp.Intefaces;

namespace IoCWebApp.Classes
{
    public class Message : IMessage
    {
        public Message()
        {
            _message = "This is an injected message";
        }

        #region Methods

        public void SetMessage(string message)
        {
            _message = message;
        }

        public string GetMessage()
        {
            return _message;
        }

        #endregion

        private string _message;
    }
}