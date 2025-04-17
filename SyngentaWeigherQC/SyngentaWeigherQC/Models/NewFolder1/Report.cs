using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class Report
  {
    public DateTime DateTime { get; set; }
    public List< GroupProductData> GroupProductDatas { get; set; }
  }
}
