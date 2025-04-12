using SerialPortLib;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class SerialControllers : BaseModel
  {
    public string Name { get; set; }
    public string Serial { get; set; }
    public string COM { get; set; }
    public int Baud { get; set; }
    public Parity Parity { get; set; }
    public DataBits Databits { get; set; }
    public StopBits Stopbits { get; set; }
  }
}
