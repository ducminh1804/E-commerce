namespace E_Commerce_Backend.ViewModel
{
    public class ScoreComparer : IComparer<Score>
    {
        public int Compare(Score? x, Score? y)
        {
            return y.Value.CompareTo(x.Value);
        }
    }
}
