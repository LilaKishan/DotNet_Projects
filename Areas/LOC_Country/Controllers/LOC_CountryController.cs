using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Areas.LOC_Country.Controllers
{
    [Area("LOC_Country")]
    [Route("LOC_Country/[controller]/[action]")]
    public class LOC_CountryController : Controller
    {
       
       
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LOC_CountryList()
        {
            return View("LOC_CountryList");
        }
        public IActionResult Add_Country() { return View(); }
    }
}
