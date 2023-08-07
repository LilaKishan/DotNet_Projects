using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Areas.LOC_City.Controllers
{
    [Area("LOC_City")]
    [Route("LOC_City/[controller]/[action]")]
    public class LOC_CityController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult City_List()
        { 
            return View(); 
        }
        public IActionResult Add_City() { return View(); }
    }
}
