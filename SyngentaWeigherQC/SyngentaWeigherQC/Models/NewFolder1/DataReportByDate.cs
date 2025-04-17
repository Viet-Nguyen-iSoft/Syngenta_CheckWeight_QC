using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class DataReportByDate
  {
    public DateTime DateTime { get; set; }
    public List<DataDateGroupByProduct> DataDateGroupByProducts { get; set; }
  }
}
