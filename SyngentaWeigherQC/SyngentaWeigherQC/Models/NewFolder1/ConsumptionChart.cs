using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models.NewFolder1
{
  public class ConsumptionChart
  {
    public Production Production { get; set; }
    public List<DetailConsumption> DetailConsumptions { get; set; } = new List<DetailConsumption>();
  }

  public class DetailConsumption
  {
    public double Error { get; set; }
    public double Loss { get; set; }
    public double Cpk { get; set; }
    public double Stdev { get; set; }
    public double Sigma { get; set; }

    public double TotalSample { get; set; }
    public double TotalOver { get; set; }
    public double TotalLower { get; set; }
  }
}
