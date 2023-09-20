using Microsoft.AspNetCore.Mvc;

namespace JobManagerSystem.Controllers
{
    public class AccountController : BaseController
    {
        //
        // GET: /Account/

        public ActionResult Login()
        {
            return View();
        }

    }
}
