using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SyngentaWeigherQC.eNum.eUI;

namespace SyngentaWeigherQC.DTO
{
  public class TableDatalogDTO
  {
    public string Shift { get; set; }
    public int No { get; set; }
    public string DateTime { get; set; }
    public Dictionary<int, double> Samples { get; set; }
    public double AvgRaw { get; set; }
    public double AvgTotal { get; set; }
    public eEvaluate eEvaluate { get; set; }
  }
}
