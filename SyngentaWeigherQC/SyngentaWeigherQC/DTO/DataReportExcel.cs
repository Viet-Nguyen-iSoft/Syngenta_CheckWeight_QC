using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.DTO
{
  public class DataReportExcel
  {
    public DateTime DateTime { get; set; }
    public List<DataByDate> DataByDates { get; set; }
  }

  public class DataByDate
  {
    public Shift Shift { get; set; }
    public List<DataByProduct> DataByProducts { get; set; }
  }

  public class DataByProduct
  {
    public Production Production { get; set; }
    public List<DatalogWeight> DatalogWeights { get; set; }
  }


}
