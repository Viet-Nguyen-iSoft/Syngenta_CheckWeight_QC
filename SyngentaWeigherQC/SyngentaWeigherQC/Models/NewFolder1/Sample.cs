using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class Sample : BaseModel
  {
    public int DatalogId { get; set; }
    public int GroupId { get; set; }
    public int LocalId { get; set; }
    public double Value { get; set; }
    public double PreviouValue { get; set; }
    public bool isHasValue { get; set; }

    public bool isEnable { get; set; }

    public bool isEdited { get; set; }
  }
}
