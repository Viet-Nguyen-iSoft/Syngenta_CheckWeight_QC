using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class ExportManual
  {
    public DateTime DateTime { get; set; }
    public List<Datalog> Datalogs { get; set; }
    public List<Sample> Samples { get; set; }
  }
}
