using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SyngentaWeigherQC.eNum.enumSoftware;

namespace SyngentaWeigherQC.Models
{
  public class Tare : BaseModel
  {
    public double ValueAgv { get; set; }
    public eModeTare eModeTare { get; set; }

  }
}
