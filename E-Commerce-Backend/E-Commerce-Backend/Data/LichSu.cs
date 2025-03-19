using System;
using System.Collections.Generic;

namespace E_Commerce_Backend.Data;

public partial class LichSu
{
    public string MaKh { get; set; } = null!;

    public string LichSu1 { get; set; } = null!;

    public virtual KhachHang MaKhNavigation { get; set; } = null!;
}
