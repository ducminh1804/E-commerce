namespace E_Commerce_Backend.ViewModel
{
    public class Score : InputData
    {
        public double Value { get; set; }
        public string Hinh { get; set; }
        public Score(int maHh, string tenHh, double value, string hinh,double dongia)
            : base(maHh, tenHh, hinh,dongia)
        {
            Value = value;
            Hinh = hinh;
        }

    }
}
