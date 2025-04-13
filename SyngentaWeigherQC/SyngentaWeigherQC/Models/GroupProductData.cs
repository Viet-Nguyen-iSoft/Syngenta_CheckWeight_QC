using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class GroupProductData
  {
    public int ProductId { get; set; }
    public string NameProduct { get; set; }
    public List<DatalogWeight> Datalogs { get; set; }
    public List<Sample> Samples { get; set; }
    public int NumberSample { get; set; }
    public int NumberSampleValueLower { get; set; }
    public int NumberSampleValueUpper { get; set; }
    public double RateError { get; set; }
    public double RateLoss { get; set; }
    public double Cpk { get; set; }
    public double Stdev { get; set; }
  }
}
