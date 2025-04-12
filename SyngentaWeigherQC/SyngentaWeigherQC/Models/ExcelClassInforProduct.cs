using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class ExcelClassInforProduct
  {
    public string NameLine { get; set; }
    public string ModeTare { get; set; }
    public string ProductName { get; set; }
    public double PackSize { get; set; }
    public double Standard { get; set; }
    public double Upper { get; set; }
    public double Lower { get; set; }
    public string NameShiftLeader { get; set; }
  }
}
