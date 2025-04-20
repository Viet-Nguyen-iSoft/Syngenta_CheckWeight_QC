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
    public List<DataByShiftType> DataByDates { get; set; }
  }

  public class DataByShiftType
  {
    public ShiftType ShiftType { get; set; }
    public List<DataByProduction> DataByProducts { get; set; }
  }

  public class DataByProduction
  {
    public Production Production { get; set; }
    public List<DataByProductionByShift> DataByProductionByShifts { get; set; }
  }

  public class DataByProductionByShift
  {
    public Shift Shift { get; set; }
    public List<DatalogWeight> DatalogWeights { get; set; }
  }

  //public class DataReportExcel
  //{
  //  public DateTime DateTime { get; set; }
  //  public List<DataByDate> DataByDates { get; set; }
  //}

  //public class DataByDate
  //{
  //  public Shift Shift { get; set; }
  //  public List<DataByProduct> DataByProducts { get; set; }
  //}

  //public class DataByProduct
  //{
  //  public Production Production { get; set; }
  //  public List<DatalogWeight> DatalogWeights { get; set; }
  //}


}
