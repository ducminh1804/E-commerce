using E_Commerce_Backend.Data;
using E_Commerce_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using System.Collections;

namespace E_Commerce_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        public readonly EcommerceContext db;

        public HangHoaController(EcommerceContext db)
        {
            this.db = db;
        }
        [HttpGet("get-all-hanghoa")]
        public async Task<ActionResult<IEnumerable<HangHoa>>> getAll()
        {
            var hangHoas =await db.HangHoas.Select(p => new HangHoaDTO
            {
                MaHH = p.MaHh,
                TenHH = p.TenHh,
                Hinh = p.Hinh,
                DonGia = (double)p.DonGia
            }).ToListAsync();

            return Ok(hangHoas);
        }

        [HttpPost("add-hanghoa")]
        public async Task<ActionResult<HangHoa>> addHangHoa([FromBody] HangHoa hangHoa)
        {
            db.HangHoas.Add(hangHoa);
            await db.SaveChangesAsync();
            return hangHoa;
        }
        [HttpDelete("delete-hanghoa/{id}")]
        public async Task<IActionResult> DeleteHangHoa(int id)
        {
            // Tìm sản phẩm theo MaHh
            var hangHoa = await db.HangHoas.FindAsync(id);
            if (hangHoa == null)
            {
                return NotFound("Không tìm thấy sản phẩm.");
            }
            // Xóa sản phẩm
            db.HangHoas.Remove(hangHoa);
            await db.SaveChangesAsync();
            return NoContent(); // Trả về 204 nếu xóa thành công
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHangHoa(int id, [FromBody] HangHoa updatedHangHoa)
        {
            // Kiểm tra nếu id trong route không khớp với id trong dữ liệu
            if (id != updatedHangHoa.MaHh)
            {
                return BadRequest("Mã hàng hóa không khớp.");
            }

            // Kiểm tra nếu sản phẩm không tồn tại
            var hangHoa = await db.HangHoas.FindAsync(id);
            if (hangHoa == null)
            {
                return NotFound("Không tìm thấy sản phẩm.");
            }

            // Cập nhật thông tin sản phẩm
            hangHoa.TenHh = updatedHangHoa.TenHh;
            hangHoa.Hinh = updatedHangHoa.Hinh;
            hangHoa.DonGia = updatedHangHoa.DonGia;
            // Cập nhật thêm các thuộc tính khác nếu có

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!db.HangHoas.Any(e => e.MaHh == id))
                {
                    return NotFound("Không tìm thấy sản phẩm để cập nhật.");
                }
                else
                {
                    throw;
                }
            }
            return NoContent(); // Trả về 204 nếu cập nhật thành công
        }
    }
}
