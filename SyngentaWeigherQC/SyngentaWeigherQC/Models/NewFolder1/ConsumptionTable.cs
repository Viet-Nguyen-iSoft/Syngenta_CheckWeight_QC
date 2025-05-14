using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class ConsumptionTable
  {
    public string Shift { get; set; }
    public string DateTime { get; set; }
    public string ProductionName { get; set; }

    public double AverageMeasure { get; set; }
    public double MinMeasure { get; set; }
    public double MaxMeasure { get; set; }

    public double Target { get; set; }
    public double UpperProduct { get; set; }
    public double LowerProduct { get; set; }

    public string Evaluate { get; set; }
  }
}
