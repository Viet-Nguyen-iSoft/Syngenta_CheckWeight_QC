using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class Shift : BaseModel
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public int StartHour { get; set; }
    public int StartMinute { get; set; }
    public int StartSecond { get; set; }
    public int EndHour { get; set; }
    public int EndMinute { get; set; }
    public int EndSecond { get; set; }
    public int Hours { get; set; }
    public int CodeShift { get; set; }
    public int ShiftTypeId { get; set; }
  }
}
