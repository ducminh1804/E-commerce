using E_Commerce.Data;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.ViewComponents
{
    public class LoaiViewComponent : ViewComponent
    {
        private readonly EcommerceContext db;

        public LoaiViewComponent(EcommerceContext context) => db = context;
        //GET:
        public IViewComponentResult Invoke()
        {
            var datas = db.Loais
                .Select(l => new LoaiDto
                {
                    MaLoai = l.MaLoai,
                    TenLoai = l.TenLoai,
                    SoLuong = l.HangHoas.Count
                })
                .ToList();

            return View("Default", datas);
        }
    }
}
