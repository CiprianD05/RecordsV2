using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;

namespace Record.Controllers
{
    //[Authorize(Roles = "User")]
    public class HomeController : Controller
    {
        private readonly RecordsRepositories.Interfaces.ICitizenRepo _repo;
        public HomeController(RecordsRepositories.Interfaces.ICitizenRepo repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            
            return View();
        }
    }
}
