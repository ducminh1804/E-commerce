using E_Commerce_Backend.Models;

namespace E_Commerce_Backend.Services
{
    public interface IVnPayService
    {
        //tao url thanh toan
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);

        //luu lai thong tin khi thanh toan thanh cong
        PaymentResponseModel PaymentExecute(IQueryCollection collections);

    }
}
