using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class ConfigSoftware:BaseModel
  {
    public string NameStation { get; set; }
    public string IpTcp { get; set; }
    public int PortTcp { get; set; }
    public int Spare1 { get; set; } //Timeout
    public string Spare2 { get; set; }
    public string Spare3 { get; set; }
  }
}
