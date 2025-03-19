using E_Commerce.Data;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using E_Commerce.Helpers;

namespace E_Commerce.Controllers
{
    public class UserController : Controller
    {
        HttpClient _httpClient;

        public UserController()
        {
            _httpClient = new HttpClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Register(string email, string matkhau)
        {
            var userDto = new UserDTO
            {
                Email = email,
                MatKhau = matkhau
            };
            string url = "http://localhost:5159/api/User/register";
            var json = JsonConvert.SerializeObject(userDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(url, content);

            //List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>("carts") ?? new List<CartItem>();

            HttpContext.Session.SetString("email_user", email);

            return View("Login");
        }


        public async Task<IActionResult> Login(string email, string matkhau)
        {
            var userDto = new UserDTO
            {
                Email = email,
                MatKhau = matkhau
            };
            string url = "http://localhost:5159/api/User/login";
            var json = JsonConvert.SerializeObject(userDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            var response = await _httpClient.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseString);
            return RedirectToAction("Index", "HangHoa");
        }

    }
}
