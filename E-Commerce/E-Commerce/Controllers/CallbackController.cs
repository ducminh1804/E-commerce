using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    //public class CallbackController : Controller
    //{
    //    HttpClient _httpClient;

    //    public CallbackController()
    //    {
    //        _httpClient = new HttpClient();
    //    }
    //    //public IActionResult Index()
    //    //{
    //    //    return View();
    //    //}

    //    public IActionResult Index()
    //    {
    //        Console.WriteLine("Dang trong callback");
    //        return View();
    //    }
    //}

    [Route("api/[controller]")]
    [ApiController]
    public class CallbackController : Controller
    {
        HttpClient _httpClient;

        public CallbackController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet("payment-callback")]
        public IActionResult Index()
        {

            Console.WriteLine("Dang trong callback");
            return View();
        }
    }

}
