using Microsoft.OpenApi.Validations;

namespace API.Classes
{
    public class DeviationRecounter
    {

        public List<decimal> Mathfun(List<decimal> results)
        {
            List<decimal> result = new List<decimal>();

            result.Add(0);
            result.Add(0);

            result[0] = results.Average();

            results.ToList().ForEach(x => result[1] += (decimal)Math.Pow((double)(x - result[0]), 2));

            result[1] = (decimal)Math.Sqrt((double)(result[1] / results.Count));

            return result;
        }
    }
}
