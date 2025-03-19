using E_Commerce_Backend.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using E_Commerce_Backend.ViewModel;
namespace E_Commerce_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly EcommerceContext db;

        public UserController(EcommerceContext db)
        {
            this.db = db;
        }

        [HttpGet(Name = "test")]
        public async Task<ActionResult<IEnumerable<KhachHang>>> GetCustomers()
        {
            var khachHangs = await db.KhachHangs.ToListAsync();
            return Ok(khachHangs);
        }
        public static string GenerateRandomChars()
        {
            Random random = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 5; i++)
            {
                sb.Append((char)random.Next(33, 127));
            }

            return sb.ToString();
        }

        [HttpPost("register")]
        public async Task<ActionResult<bool>> Register([FromBody] UserDTO userDTO)
        {
            var kh = new KhachHang
            {
                MaKh = GenerateRandomChars(),
                Email = userDTO.email,
                MatKhau = userDTO.matkhau,
            };

            db.Add(kh);
            await db.SaveChangesAsync();
            return Ok(true);
        }

        [HttpPost("login")]
        public async Task<ActionResult<bool>> Login([FromBody] UserDTO userDTO)
        {
            var kh = db.KhachHangs.Where(p => p.Email == userDTO.email).FirstOrDefault();
            if (kh.MatKhau == userDTO.matkhau)
            {
                return Ok(true);
            }
            return Ok(false);
        }
    }
}
