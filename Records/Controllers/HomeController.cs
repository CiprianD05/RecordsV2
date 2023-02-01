using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;

namespace Record.Controllers
{
    //[Authorize(Roles = "User")]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            
            return View();
        }
    }
}
