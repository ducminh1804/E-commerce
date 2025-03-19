using Microsoft.ML.Data;

namespace E_Commerce_Backend.ViewModel
{
    public class TransformedData:InputData
    {
        [VectorType]
        public float[] Features { get; set; }
    }
}
