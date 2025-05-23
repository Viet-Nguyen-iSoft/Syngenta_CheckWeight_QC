using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SyngentaWeigherQC.eNum.enumSoftware;

namespace SyngentaWeigherQC.Models
{
  public class ShiftLeader : BaseModel
  {
    public string UserName { get; set; }
    public string Passwords { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public bool IsEnable { get; set; } = false;
    public bool IsDelete { get; set; } = false;

    [Browsable(false)]
    public ICollection<DatalogWeight> DatalogWeights { get; set; }
  }
}
