using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class DataChartReport
  {
    public Dictionary<string, double> RateError { get; set; }
    public Dictionary<string, double> RateLoss { get; set; }
    public Dictionary<string, double> Cpk { get; set; }
    public Dictionary<string, double> Stdev { get; set; }
    public int NumberSampleTotal { get; set; }
    public int NumberSampleUpper { get; set; }
    public int NumberSampleLower { get; set; }
  }
}
