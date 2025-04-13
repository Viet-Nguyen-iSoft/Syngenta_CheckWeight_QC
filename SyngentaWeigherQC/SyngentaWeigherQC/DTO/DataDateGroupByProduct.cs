using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class DataDateGroupByProduct
  {
    public int ProductId { get; set; }
    public List<DatalogWeight> Datalogs { get; set; }
    public List<Sample> Samples { get; set; }
  }
}
