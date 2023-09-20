using Microsoft.AspNetCore.Mvc;

namespace JobManagerSystem.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }

    }
}
