using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Helper
{
  public class MathHelper
  {
    public static double Stdev(List<double> data, int number_digits = 2)
    {
      if (data.Count > 0)
      {
        double mean = data.Average();
        double sumOfSquares = data.Sum(x => Math.Pow(x - mean, 2));
        double variance = (data.Count() > 1) ? sumOfSquares / (data.Count() - 1) : 1;
        return Math.Round(Math.Sqrt(variance), number_digits);
      }
      return 0;
    }
  }
}
