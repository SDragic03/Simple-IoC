using IoCWebApp.Intefaces;

namespace IoCWebApp.Classes
{
    public class ContactInfo : IContactInfo
    {
        public string GetInfo()
        {
            return "Call 555 - My Info...";
        }
    }
}