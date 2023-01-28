using Microsoft.AspNetCore.Mvc;

namespace ShoppingList.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
