using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Helper
{
  public class CvtClassDTO
  {
    public static List<ProductionDTO> ConvertToProductionExcelList(List<Production> productions)
    {
      int no = 1;
      return productions
          .Select((p) => ConvertToProductionExcel(p, no++))
          .ToList();
    }

    public static ProductionDTO ConvertToProductionExcel(Production production, int no)
    {
      if (production == null) return null;

      return new ProductionDTO
      {
        No = no,
        Name = production.Name,
        LineCode = production.LineCode,
        PackSize = production.PackSize,
        Density = production.Density,

        // Tare không nhãn
        Tare_no_label_lowerlimit = production.Tare_no_label_lowerlimit,
        Tare_no_label_standard = production.Tare_no_label_standard,
        Tare_no_label_upperlimit = production.Tare_no_label_upperlimit,

        // Tare có nhãn
        Tare_with_label_lowerlimit = production.Tare_with_label_lowerlimit,
        Tare_with_label_standard = production.Tare_with_label_standard,
        Tare_with_label_upperlimit = production.Tare_with_label_upperlimit,

        // Final
        LowerLimitFinal = production.LowerLimitFinal,
        StandardFinal = production.StandardFinal,
        UpperLimitFinal = production.UpperLimitFinal,

        // Trường thêm
        Id = production.Id,
        InforLineId = production.InforLineId,
        IsNew = false
      };
    }


    public static List<Production> ConvertToProductionList(List<ProductionDTO> productions)
    {
      return productions
          .Select((p) => ConvertToProduction(p))
          .ToList();
    }
    public static Production ConvertToProduction(ProductionDTO productionDTO)
    {
      if (productionDTO == null) return null;

      return new Production
      {
        Name = productionDTO.Name,
        LineCode = productionDTO.LineCode,
        PackSize = productionDTO.PackSize,
        Density = productionDTO.Density,

        // Tare không nhãn
        Tare_no_label_lowerlimit = productionDTO.Tare_no_label_lowerlimit,
        Tare_no_label_standard = productionDTO.Tare_no_label_standard,
        Tare_no_label_upperlimit = productionDTO.Tare_no_label_upperlimit,

        // Tare có nhãn
        Tare_with_label_lowerlimit = productionDTO.Tare_with_label_lowerlimit,
        Tare_with_label_standard = productionDTO.Tare_with_label_standard,
        Tare_with_label_upperlimit = productionDTO.Tare_with_label_upperlimit,

        // Final
        LowerLimitFinal = productionDTO.LowerLimitFinal,
        StandardFinal = productionDTO.StandardFinal,
        UpperLimitFinal = productionDTO.UpperLimitFinal,

        // Trường thêm
        InforLineId = productionDTO.InforLineId,
      };
    }
  }
}
