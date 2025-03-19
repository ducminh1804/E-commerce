using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class PurchaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
