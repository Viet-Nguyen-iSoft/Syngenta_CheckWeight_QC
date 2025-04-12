using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SyngentaWeigherQC.eNum.eUI;

namespace SyngentaWeigherQC.Models
{
  public class Datalog : BaseModel
  {
    public int StationId { get; set; }
    public int GroupId { get; set; }
    public int ShiftId { get; set; }
    public int No { get; set; }
    //public double AvgRaw { get; set; }
    //public double AvgTotal { get; set; }
    //public double Standard { get; set; }
    //public int Evaluate { get; set; }
    //public double UpperLimit { get; set; }
    //public double LowerLimit { get; set; }
    //public double Stdev { get; set; }
    //public double StdevAgv { get; set; }
    public double TareValue { get; set; }
    public eModeTare IsTareWithLabel { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    //public double Cp { get; set; }
    //public double CpkL { get; set; }
    //public double CpkH { get; set; }
    //public double Cpk { get; set; }
    //public double HightDefects { get; set; }
    //public double LowDefects { get; set; }
    //public double Z_HightDefects { get; set; }
    //public double Z_LowDefects { get; set; }
    //public double CpkPass { get; set; }


    //[Browsable(false)]
    //public int numberIsUpdate { get; set; }
  }
}
