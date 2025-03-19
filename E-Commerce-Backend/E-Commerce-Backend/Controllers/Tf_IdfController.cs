using E_Commerce_Backend.Data;
using E_Commerce_Backend.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Transforms.Text;
using MathNet.Numerics.LinearAlgebra;

using Microsoft.Build.Execution;
namespace E_Commerce_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tf_IdfController : ControllerBase
    {
        public readonly MLContext mlContext;
        public readonly EcommerceContext db;

        public Tf_IdfController(EcommerceContext db)
        {
            this.mlContext = new MLContext();
            this.db = db;
        }

        public static double CalculateCosineSimilarity(Vector<float> vector1, Vector<float> vector2)
        {
            double dotProduct = vector1.DotProduct(vector2);
            double norm1 = vector1.L2Norm();
            double norm2 = vector2.L2Norm();

            if (norm1 == 0 || norm2 == 0)
            {
                return 0;
            }

            return dotProduct / (norm1 * norm2);
        }
       
        [HttpGet("Recommend")]
        public async Task<IActionResult> TrainModel(int? Id_Hh)
        {
            var data = db.HangHoas.Select(p => new InputData { MaHh = p.MaHh, TenHh = p.TenHh, Hinh = p.Hinh, DonGia = (double)p.DonGia }).ToList();
            var dataView = mlContext.Data.LoadFromEnumerable(data);
            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(InputData.TenHh));
            var model = pipeline.Fit(dataView);
            var transformedData = model.Transform(dataView);
            var features = mlContext.Data.CreateEnumerable<TransformedData>(transformedData, reuseRowObject: false).ToList();

            //int Id_Hh = 1001;
            int index_Hh = data.FindIndex(item => item.MaHh == Id_Hh);
            Console.WriteLine(index_Hh);

            var oriItem = features[index_Hh];
            var oriVector = Vector<float>.Build.DenseOfArray(features[index_Hh].Features);
            List<Score> inputDatas = new List<Score>();

            foreach (var item in features)
            {
                if (item.MaHh != oriItem.MaHh)
                {
                    var itemVector = Vector<float>.Build.DenseOfArray(item.Features);
                    double cosineSimilarity = CalculateCosineSimilarity(oriVector, itemVector);
                    inputDatas.Add(new Score(item.MaHh, item.TenHh, cosineSimilarity, item.Hinh,item.DonGia));
                }
            }
            inputDatas.Sort(new ScoreComparer());
            List<Score> result = new List<Score>();
            for (int i = 0; i < 5; i++)
            {
                result.Add(inputDatas[i]);
            }

            return Ok(result);
        }
        [HttpGet("test1")]
        public async Task<ActionResult<List<string>>> method()
        {
            var data = db.HangHoas.Select(p => p.TenHh).ToList();
            return Ok(data);
        }
    }
}

