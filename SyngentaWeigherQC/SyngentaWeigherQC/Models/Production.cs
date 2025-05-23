using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class Production : BaseModel
  {
    [DisplayName("Tên sản phẩm")]
    public string Name { get; set; }

    [DisplayName("Dây chuyền")]
    public string LineCode { get; set; }

    [DisplayName("Packsize")]
    public double PackSize { get; set; }

    [DisplayName("Tỷ trọng")]
    public double Density { get; set; }


    //Tare Không nhãn
    [DisplayName("TL Tare không nhãn - Giới hạn dưới (g)")]
    public double Tare_no_label_lowerlimit { get; set; }
    [DisplayName("TL Tare không nhãn - Chuẩn (g)")]
    public double Tare_no_label_standard { get; set; }
    [DisplayName("TL Tare không nhãn - Giới hạn trên (g)")]
    public double Tare_no_label_upperlimit { get; set; }


    //Tare Có nhãn
    [DisplayName("TL Tare có nhãn - Giới hạn dưới (g)")]
    public double Tare_with_label_lowerlimit { get; set; }
    [DisplayName("TL Tare có nhãn - Chuẩn (g)")]
    public double Tare_with_label_standard { get; set; }

    [DisplayName("TL Tare có nhãn - Giới hạn trên (g)")]
    public double Tare_with_label_upperlimit { get; set; }
    

    //Khối lượng Final
   
    [DisplayName("Giới hạn dưới (g)")]
    public double LowerLimitFinal { get; set; }
    [DisplayName("Chuẩn (g)")]
    public double StandardFinal { get; set; }
    [DisplayName("Giới hạn trên (g)")]
    public double UpperLimitFinal { get; set; }



    [Browsable(false)]
    public bool IsDelete { get; set; }
    [Browsable(false)]
    public bool IsEnable { get; set; }




    [Browsable(false)]
    public int? InforLineId { get; set; }

    [Browsable(false)]
    public InforLine InforLine { get; set; }


    [Browsable(false)]
    public ICollection<DatalogWeight> DatalogWeights { get; set; }

    [Browsable(false)]
    public ICollection<DatalogTare> DatalogTares { get; set; }

  }



  public class ProductionDTO
  {
    [DisplayName("Stt")]
    public int No { get; set; }
    [DisplayName("Tên sản phẩm")]
    public string Name { get; set; }

    [DisplayName("Chuyền")]
    public string LineCode { get; set; }

    [DisplayName("Packsize")]
    public double PackSize { get; set; }

    [DisplayName("Tỷ trọng")]
    public double Density { get; set; }


    //Tare Không nhãn
    [DisplayName("TL Tare không nhãn - Giới hạn dưới (g)")]
    public double Tare_no_label_lowerlimit { get; set; }
    [DisplayName("TL Tare không nhãn - Chuẩn (g)")]
    public double Tare_no_label_standard { get; set; }
    [DisplayName("TL Tare không nhãn - Giới hạn trên (g)")]
    public double Tare_no_label_upperlimit { get; set; }


    //Tare Có nhãn
    [DisplayName("TL Tare có nhãn - Giới hạn dưới (g)")]
    public double Tare_with_label_lowerlimit { get; set; }
    [DisplayName("TL Tare có nhãn - Chuẩn (g)")]
    public double Tare_with_label_standard { get; set; }

    [DisplayName("TL Tare có nhãn - Giới hạn trên (g)")]
    public double Tare_with_label_upperlimit { get; set; }


    //Khối lượng Final

    [DisplayName("Giới hạn dưới (g)")]
    public double LowerLimitFinal { get; set; }
    [DisplayName("Chuẩn (g)")]
    public double StandardFinal { get; set; }
    [DisplayName("Giới hạn trên (g)")]
    public double UpperLimitFinal { get; set; }
    [Browsable(false)]
    public bool IsNew { get; set; }
    [Browsable(false)]
    public int? InforLineId { get; set; }
    [Browsable(false)]
    public int? Id { get; set; }
  }
}
