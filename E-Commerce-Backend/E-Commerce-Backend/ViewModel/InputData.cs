using Microsoft.ML.Data;

namespace E_Commerce_Backend.ViewModel
{
    public class InputData
    {
        public int MaHh { get; set; }

        [LoadColumn(0)]
        public string TenHh { get; set; }
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public InputData(int maHh, string tenHh, string hinh, double dongia)
        {
            MaHh = maHh;
            TenHh = tenHh;
            Hinh = hinh;
            DonGia = dongia;
        }

        public InputData()
        {
        }

    }
}
