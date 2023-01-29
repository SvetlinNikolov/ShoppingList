using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingList.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
