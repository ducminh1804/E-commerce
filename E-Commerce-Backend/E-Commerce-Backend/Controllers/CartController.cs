using E_Commerce_Backend.Data;
using E_Commerce_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public readonly EcommerceContext db;

        public CartController(EcommerceContext db)
        {
            this.db = db;
        }

        [HttpGet("all-cart")]
        public async Task<List<AllCartDTO>> createHoaDon(string email)
        {
            var sql = @"select hd.NgayDat,ct.MaHH,hh.TenHH,hh.Hinh,hh.DonGia,ct.SoLuong from hoadon hd 
                    join ChiTietHD ct 
                    on hd.MaHD = ct.MaHD
                    join KhachHang kh 
                    on kh.MaKH = hd.MaKH
                    join HangHoa hh on hh.MaHH = ct.MaHH
                    where kh.Email = @p0";
            var result = await db.Database.SqlQueryRaw<AllCartDTO>(sql,email).ToListAsync();    
            return result;
        }
    }
}
