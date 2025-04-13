using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SyngentaWeigherQC.eNum.eUI;

namespace SyngentaWeigherQC.Models
{
  public class DatalogTare:BaseModel
  {
    public double Value { get; set; }
    public eModeTare eModeTare { get; set; }


    [Browsable(false)]
    public int? InforLineId { get; set; }

    [Browsable(false)]
    public InforLine InforLine { get; set; }


    [Browsable(false)]
    public ICollection<DatalogWeight> DatalogWeights { get; set; }
  }
}
