using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class DatalogDgv
  {
    [DisplayName("Ca")]
    public string Shift { get; set; }
    [DisplayName("STT")]
    public int No { get; set; }
    [DisplayName("Thời gian")]
    public string DateTime { get; set; }
    [DisplayName("Mẫu 1")]
    public double Sample1 { get; set; }
    [DisplayName("Mẫu 2")]
    public double Sample2 { get; set; }
    [DisplayName("Mẫu 3")]
    public double Sample3 { get; set; }
    [DisplayName("Mẫu 4")]
    public double Sample4 { get; set; }
    [DisplayName("Mẫu 5")]
    public double Sample5 { get; set; }
    [DisplayName("Mẫu 6")]
    public double Sample6 { get; set; }
    [DisplayName("Mẫu 7")]
    public double Sample7 { get; set; }
    [DisplayName("Mẫu 8")]
    public double Sample8 { get; set; }
    [DisplayName("Mẫu 9")]
    public double Sample9 { get; set; }
    [DisplayName("Mẫu 10")]
    public double Sample10 { get; set; }
    [DisplayName("Mẫu 11")]
    public double Sample11 { get; set; }
    [DisplayName("Mẫu 12")]
    public double Sample12 { get; set; }
    [DisplayName("Mẫu 13")]
    public double Sample13 { get; set; }
    [DisplayName("Mẫu 14")]
    public double Sample14 { get; set; }
    [DisplayName("Mẫu 15")]
    public double Sample15 { get; set; }
    [DisplayName("Đánh giá")]
    public string Evaluate { get; set; }
    [DisplayName("TB (Đo)")]
    public double AvgRaw { get; set; }
    [DisplayName("TB (Ca)")]
    public double AvgTotal { get; set; }
    [DisplayName("Tiêu chuẩn")]
    public double Standard { get; set; }
    [DisplayName("Stb")]
    public double Stb { get; set; }
    [DisplayName("Giới hạn trên")]
    public double Max { get; set; }
    [DisplayName("Giới hạn dưới")]
    public double Min { get; set; }
  }
}
