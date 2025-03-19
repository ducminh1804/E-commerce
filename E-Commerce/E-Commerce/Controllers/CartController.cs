using E_Commerce.Data;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Helpers;
namespace E_Commerce.Controllers
{
    public class CartController : Controller
    {
        public readonly EcommerceContext db;

        public CartController(EcommerceContext db)
        {
            this.db = db;
        }

        List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>("carts") ?? new List<CartItem>();

        public IActionResult Index()
        {
            return View(Cart);
        }

        public IActionResult AddToCart(int? mahh, int? quantity)
        {
            var gioHang = Cart;
            var item = Cart.SingleOrDefault(p => p.MaHH == mahh);
            if (item == null)
            {
                var hangHoa = db.HangHoas.SingleOrDefault(p => p.MaHh == mahh);
                item = new CartItem
                {
                    MaHH = hangHoa.MaHh,
                    TenHH = hangHoa.TenHh,
                    DonGia = (double)hangHoa.DonGia,
                    Img = hangHoa.Hinh,
                    SoLuong = quantity ?? 0,
                    Total = (double)(quantity ?? 0 * hangHoa.DonGia),
                };
                gioHang.Add(item);
            }
            else
            {
                item.SoLuong += quantity ?? 0;
            }
            HttpContext.Session.Set("carts", gioHang);
            return RedirectToAction("Index");
        }

    }
}
