using E_Commerce_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using static E_Commerce.Controllers.PaymentController;

namespace E_Commerce.Controllers
{

    public class PaymentController:Controller
    {
        HttpClient _httpClient;

        public PaymentController()
        {
            _httpClient = new HttpClient();
        }
        public class PaymentResponse
        {
            public string paymentUrl { get; set; }
        }

        public async Task<IActionResult> GetPaymentUrlAsync(double totalSub)
        {
            var url = "http://localhost:5159/api/Payment/create-payment-url";
            PaymentInformationModel model = new PaymentInformationModel { OrderType = "VnPay", Amount = totalSub * 1000, OrderDescription = "Banking", Name = "ECommerce" };
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            //bien dich json traa ve theo dto PaymentResponse
            var responseObject = JsonConvert.DeserializeObject<PaymentResponse>(responseString);
            string urlTarget = responseObject.paymentUrl;
            return Redirect(urlTarget);
        }

      

    }
}
