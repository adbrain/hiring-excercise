using System.Web.Mvc;

namespace AdBrainTask.Controllers
{
    public class WebAppController : Controller
    {
        // GET: WebApp
        public ActionResult Index()
        {
            ViewBag.Title = "AdBrain task homepage";
            return View();
        }
    }
}