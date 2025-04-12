using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class Tare : BaseModel
  {
    public int StationId { get; set; }
    public int GroupId { get; set; }
    public int ProductId { get; set; }
    public int ShiftId { get; set; }
    public double Value { get; set; }
    public double ValueAvg { get; set; }
    public double Stdev { get; set; }
    public double ValueUpper_nolabel { get; set; }
    public double ValueStandard_nolabel { get; set; }
    public double ValueLower_nolabel { get; set; }
    public double ValueUpper_withlabel { get; set; }
    public double ValueStandard_withlabel { get; set; }
    public double ValueLower_withlabel { get; set; }
    public int isWithLabel { get; set; }

  }
}
