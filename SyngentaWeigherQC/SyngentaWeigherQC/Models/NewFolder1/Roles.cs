using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class Roles : BaseModel
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public string Permission { get; set; }
    public string Passwords { get; set; }
    public bool isEnable { get; set; }
  }
}
