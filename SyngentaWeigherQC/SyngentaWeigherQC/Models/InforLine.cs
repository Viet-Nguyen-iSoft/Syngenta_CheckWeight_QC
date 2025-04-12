using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SyngentaWeigherQC.eNum.eUI;

namespace SyngentaWeigherQC.Models
{
  public class InforLine : BaseModel
  {
    public string Name { get; set; }
    public string Code { get; set; }
    public string PathReportOneDrive { get; set; }
    public string PathReportLocal { get; set; }
    public string PassReport { get; set; }
    public bool ActionPassReport { get; set; } = false;
    public bool IsEnable { get; set; } = false;
    public bool IsDelete { get; set; } = false;

    public eModeTare eModeTare { get; set; }
    public bool RequestTare { get; set; } = true;







    [Browsable(false)]
    public int? ShiftTypesId { get; set; }

    [Browsable(false)]
    public ShiftType ShiftTypes { get; set; }



    [Browsable(false)]
    public int? ShiftLeaderId { get; set; }

    [Browsable(false)]
    public ShiftLeader ShiftLeader { get; set; }



    [Browsable(false)]
    public ICollection<DatalogTare> DatalogTares { get; set; }


    [Browsable(false)]
    public ICollection<Production> Productions { get; set; }


  }
}
