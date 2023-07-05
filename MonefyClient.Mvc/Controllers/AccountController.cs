using Microsoft.AspNetCore.Mvc;

namespace MonefyClient.Mvc.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult Account()
        {
            return View();
        }

        public IActionResult Income()
        {
            return View();
        }

        public IActionResult Expense()
        {
            return View();
        }
    }
}
