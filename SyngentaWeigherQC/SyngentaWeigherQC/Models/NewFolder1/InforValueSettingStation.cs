using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class InforValueSettingStation:BaseModel
  {
    public int StationId { get; set; }
    public double ValueAvgTare { get; set; }
    public double Min { get; set; }
    public double Max { get; set; }
    public double Target { get; set; }
    public int ModeTare { get; set; } //1-Label______2- No Label

    public bool IsChangeProductNoTare { get; set; }

    public int GroupId_Datalog { get; set; }
    public int STT_Datalog { get; set; }
    public int GroupId_Sample { get; set; }
    public int DatalogId_Sample { get; set; }
    
    public string Spare2 { get; set; }
    public string Spare3 { get; set; }
  }
}
