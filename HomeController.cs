using System.Web.Mvc;
using IoCWebApp.Classes;

namespace IoCWebApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(Message message, ContactInfo info)
        {
            _info = info;
            _message = message;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = _message.GetMessage();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = _info.GetInfo();

            return View();
        }

        #region Fields

        private readonly ContactInfo _info;
        private readonly Message _message;

        #endregion

    }
}