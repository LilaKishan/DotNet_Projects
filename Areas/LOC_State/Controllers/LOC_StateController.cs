using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Areas.LOC_State.Controllers
{
    [Area("LOC_State")]
    [Route("LOC_State/[controller]/[action]")]
    public class LOC_StateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult State_List() { return View(); }
        public IActionResult Add_State() { return View(); }
    }
}
