using E_Commerce.Data;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace E_Commerce.Controllers
{
    public class HangHoaController : Controller
    {
        HttpClient _httpClient;

        private readonly EcommerceContext db;

        public HangHoaController(EcommerceContext db)
        {
            this.db = db;
            _httpClient = new HttpClient();
        }

        public IActionResult Index(int? MaLoai)
        {
            Console.WriteLine("dang trong hang hoa index");
            //khi muon su dung cac ham sql len list, colleciton=> chuyen no quau Iqueryable = asqueryable
            var hangHoas = db.HangHoas.AsQueryable();
            if (MaLoai.HasValue)
            {
                hangHoas = db.HangHoas
                    .Where(item => item.MaLoai == MaLoai);
            }
            var result = hangHoas.Select(h => new HangHoaDTO
            {
                MaHH = h.MaHh,
                TenHH = h.TenHh,
                MaLoai = h.MaLoai,
                MoTa = h.MoTa,
                DonGia = (double)h.DonGia,
                GiamGia = (int)h.GiamGia,
                SoLanXem = h.SoLanXem,
                MaNCC = h.MaNcc,
                TenLoai = h.MaLoaiNavigation.TenLoai,
                Img = h.Hinh
            });
            return View(result);
        }

        public IActionResult Search(string? query)
        {
            var hanghoas = db.HangHoas.AsQueryable();
            hanghoas = hanghoas.Where(h => h.TenHh.Contains(query));
            var result = hanghoas.Select(h => new HangHoaDTO
            {
                MaHH = h.MaHh,
                TenHH = h.TenHh,
                MaLoai = h.MaLoai,
                MoTa = h.MoTa,
                DonGia = (double)h.DonGia,
                GiamGia = (int)h.GiamGia,
                SoLanXem = h.SoLanXem,
                MaNCC = h.MaNcc,
                TenLoai = h.MaLoaiNavigation.TenLoai,
                Img = h.Hinh
            });
            return View(result);
        }

        public async Task<IActionResult> Detail(int? mahh)
        {
            var hh = db.HangHoas.AsQueryable();
            hh = hh.Where(h => h.MaHh == mahh);
            var result = hh.Select(h => new HangHoaDTO
            {
                MaHH = h.MaHh,
                TenHH = h.TenHh,
                MaLoai = h.MaLoai,
                MoTa = h.MoTa,
                DonGia = (double)h.DonGia,
                GiamGia = (int)h.GiamGia,
                SoLanXem = h.SoLanXem,
                MaNCC = h.MaNcc,
                TenLoai = h.MaLoaiNavigation.TenLoai,
                Img = h.Hinh
            }).FirstOrDefault();
            var recommends = await Recommend(mahh);
            ViewBag.Recommends = recommends;
            return View(result);
        }

        public async Task<List<HangHoa>> Recommend(int? mahh)
        {
            string url = $"http://localhost:5159/api/Tf_Idf/Recommend?Id_Hh={mahh}";
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            List<HangHoa> hangHoaList = await response.Content.ReadFromJsonAsync<List<HangHoa>>();
            foreach (var item in hangHoaList)
            {
                Console.WriteLine(item.TenHh);
            }
            return hangHoaList;
        }
    }
}
