using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class ShiftType : BaseModel
  {
    public string Name { get; set; }
    public int Code { get; set; }
    public bool isEnable { get; set; }
  }
}
