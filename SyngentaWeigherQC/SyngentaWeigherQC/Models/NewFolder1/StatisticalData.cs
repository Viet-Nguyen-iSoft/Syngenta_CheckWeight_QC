using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Responsitory
{
  public class StatisticalData
  {
    public int Shift { get; set; }
    public double Stdev { get; set; }
    public double Average { get; set; }
    public double Target { get; set; }
    public string Result { get; set; }
    public int TotalSample { get; set; }
    public int NumberSampleOver { get; set; }
    public int NumberSampleLower { get; set; }
    public double RateError { get; set; }
    public double RateLoss { get; set; }
  }
}
