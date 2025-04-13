using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SyngentaWeigherQC.eNum.eUI;

namespace SyngentaWeigherQC.Models
{
  public class DatalogWeight : BaseModel
  {
    public int ShiftId { get; set; }
  
    public double Value { get; set; }
    public double ValuePrevious { get; set; }


    [Browsable(false)]
    public int? ProductionId { get; set; }

    [Browsable(false)]
    public Production Production { get; set; }


    [Browsable(false)]
    public int? DatalogTareId { get; set; }

    [Browsable(false)]
    public DatalogTare DatalogTare { get; set; }


    [Browsable(false)]
    public int? InforLineId { get; set; }

    [Browsable(false)]
    public InforLine InforLine { get; set; }
  }
}
