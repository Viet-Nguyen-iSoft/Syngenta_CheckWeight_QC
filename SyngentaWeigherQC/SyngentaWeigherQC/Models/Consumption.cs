using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class Consumption
  {
    public int ProductId { get; set; }
    public string Shift { get; set; }
    public int STT { get; set; }
    public string DateTime { get; set; }
    public string Production { get; set; }
    public double AverageMeasure { get; set; }
    public double MinMeasure { get; set; }
    public double MaxMeasure { get; set; }
    public double Target { get; set; }
    public double UpperProduct { get; set; }
    public double LowerProduct { get; set; }
    public string Evaluate { get; set; }

    //public List<Datalog> Datalogs { get; set; }
    public List<double> SamplesValue { get; set; }
  }
}
