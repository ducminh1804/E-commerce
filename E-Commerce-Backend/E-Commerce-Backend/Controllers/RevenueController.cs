using E_Commerce_Backend.Data;
using E_Commerce_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueController : ControllerBase
    {
        public readonly EcommerceContext db;

        public RevenueController(EcommerceContext db)
        {
            this.db = db;
        }
        [HttpGet("revenues")]
        public async Task<List<RevenueDTO>> Revenues(int x, int y, int z)
        {
            var sql = @"select MONTH(hd.NgayDat) as Thang, YEAR(hd.NgayDat) as Nam, 
                sum(DonGia * SoLuong * (1 - GiamGia)) as Total
                from HoaDon hd 
                join ChiTietHD ct on hd.MaHD = ct.MaHD 
                where @p0 <= MONTH(hd.NgayDat) and MONTH(hd.NgayDat) <= @p1 and YEAR(hd.NgayDat) = @p2 
                group by MONTH(hd.NgayDat), YEAR(hd.NgayDat) 
                order by MONTH(hd.NgayDat) asc";
            var revenues = await db.Database.SqlQueryRaw<RevenueDTO>(sql, x, y, z).ToListAsync();

            return revenues;
        }
    }
}
