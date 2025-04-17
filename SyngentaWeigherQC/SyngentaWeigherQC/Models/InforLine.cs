using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
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
    
    public int LastTareId { get; set; } = 0;


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
    public ICollection<DatalogWeight> DatalogWeights { get; set; }

    [Browsable(false)]
    public ICollection<Production> Productions { get; set; }


    [NotMapped]
    public Production ProductionCurrent { get; set; }
    [NotMapped]
    public DatalogTare DatalogTareCurrent { get; set; }
    [NotMapped]
    public eStatusConnectWeight eStatusConnectWeight { get; set; } = eStatusConnectWeight.Disconnnect;
  }
}
