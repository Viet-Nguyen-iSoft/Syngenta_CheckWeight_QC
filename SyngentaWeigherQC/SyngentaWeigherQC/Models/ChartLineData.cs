using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class ChartLineData
  {
    public double Min { get; set; }
    public double Max { get; set; }
    public double Target { get; set; }
    public double Average { get; set; }
    public List<double> AverageRaw { get; set; }
  }
}
