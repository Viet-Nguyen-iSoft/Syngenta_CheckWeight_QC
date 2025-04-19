using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Helper
{
  public class WeightHelper
  {
    public static (string Name, double Weight) ParseDataWeight(string input)
    {
      input = input.Trim('\u0002', '\u0003');

      var match = Regex.Match(input, @"(\d+\.\d+)g");

      if (match.Success)
      {
        string name = match.Groups[1].Value.Trim();

        double weight = 0;
        double.TryParse(name, out weight);

        return (name, weight);
      }

      throw new ArgumentException("Format Data Weight Fail");
    }
  }
}
