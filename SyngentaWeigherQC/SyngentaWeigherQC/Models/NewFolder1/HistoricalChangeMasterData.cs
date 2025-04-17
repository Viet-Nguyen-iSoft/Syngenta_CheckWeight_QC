using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class HistoricalChangeMasterData : BaseModel
  {
    public string Reason { get; set; }
    public int UpdateBy { get; set; }
  }
}
