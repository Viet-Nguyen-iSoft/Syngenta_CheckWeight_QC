using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class SampleByProductId
  {
    public int ProductId { get; set; }
    public List<Sample> Samples { get; set; }
  }
}
