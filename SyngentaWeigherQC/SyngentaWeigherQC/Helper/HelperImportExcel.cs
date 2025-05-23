using Aspose.Cells;
using Aspose.Cells.Drawing;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.UI.FrmUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SyngentaWeigherQC.eNum.enumSoftware;

namespace SyngentaWeigherQC.Helper
{
  public class HelperImportExcel
  {
    //Import User Shift Leader
    public static List<ShiftLeader> PareXlsxByAspose_ShiftLeader(string file_path)
    {
      try
      {
        FileInfo dest_file_info = new FileInfo(file_path);
        Workbook wb = new Workbook(dest_file_info.FullName);
        WorksheetCollection collection = wb.Worksheets;
        int max_rows = 0; int max_cols = 0;

        List<ShiftLeader> users = new List<ShiftLeader>();
        for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
        {
          Worksheet worksheet = collection[worksheetIndex];
          if (worksheet.Name == "ShiftLeader")
          {
            max_rows = worksheet.Cells.MaxDataRow;
            max_cols = worksheet.Cells.MaxDataColumn;

            for (int row = 1; row <= max_rows; row++)
            {
              ShiftLeader user = new ShiftLeader();
              user.UserName = GetText(worksheet, row, 1);
              user.RoleId = 4;
              if (!string.IsNullOrEmpty(user.UserName)) users.Add(user);
            }
          }
        }

        return users;
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
        new FrmNotification().ShowMessage("File excel load thất bại !", eMsgType.Warning);
        return null;
      }
    }

    public static List<ProductionDTO> PareXlsxByAspose_Production(string file_path, List<InforLine> inforLines)
    {
      List<ProductionDTO> productionExcels = new List<ProductionDTO>();

      FileInfo dest_file_info = new FileInfo(file_path);
      Workbook wb = new Workbook(dest_file_info.FullName);
      WorksheetCollection collection = wb.Worksheets;
      int max_rows = 0; int max_cols = 0;
      for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
      {
        Worksheet worksheet = collection[worksheetIndex];
        if (worksheet.Name.Contains("Data"))
        {
          max_rows = worksheet.Cells.MaxDataRow;
          max_cols = worksheet.Cells.MaxDataColumn;

          for (int row = 2; row <= max_rows; row++)
          {
            ProductionDTO production_data = new ProductionDTO();

            int col = 0;
            production_data.Name = HelperImportExcel.GetTextNameProduct(worksheet, row, col++);
            production_data.LineCode = HelperImportExcel.GetText(worksheet, row, col++);
            production_data.Name = HelperImportExcel.GetText(worksheet, row, col++);
            production_data.PackSize = HelperImportExcel.GetDouble(worksheet, row, col++);
            production_data.Density = HelperImportExcel.GetDouble(worksheet, row, col++);

            col = 6;
            production_data.Tare_no_label_lowerlimit = HelperImportExcel.GetDouble(worksheet, row, col++);
            production_data.Tare_no_label_standard = HelperImportExcel.GetDouble(worksheet, row, col++);
            production_data.Tare_no_label_upperlimit = HelperImportExcel.GetDouble(worksheet, row, col++);

            col = 10;
            production_data.Tare_with_label_lowerlimit = HelperImportExcel.GetDouble(worksheet, row, col++);
            production_data.Tare_with_label_standard = HelperImportExcel.GetDouble(worksheet, row, col++);
            production_data.Tare_with_label_upperlimit = HelperImportExcel.GetDouble(worksheet, row, col++);

            col = 14;
            production_data.LowerLimitFinal = HelperImportExcel.GetDouble(worksheet, row, col++);
            production_data.StandardFinal = HelperImportExcel.GetDouble(worksheet, row, col++);
            production_data.UpperLimitFinal = HelperImportExcel.GetDouble(worksheet, row, col++);

            //Check xem data có phải thuộc station hiện tại không
            var data_line = inforLines?.FirstOrDefault(x => x.Code == production_data.LineCode);
            if (data_line == null)
            {
              continue;
            }

            //Check Line
            production_data.InforLineId = data_line.Id;
            productionExcels.Add(production_data);
          }
        }
      }
      return productionExcels;
    }


    public static string GetText(Worksheet worksheet, int row, int column)
    {
      string ret = "";
      try
      {
        object textObj = worksheet.Cells[row, column].Value;
        if (textObj != null)
        {
          ret = textObj.ToString().Trim();
        }
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
      }
      return ret;
    }
    public static string GetTextNameProduct(Worksheet worksheet, int row, int column)
    {
      string ret = "";
      try
      {
        object textObj = worksheet.Cells[row, column].Value;
        if (textObj != null)
        {
          ret = textObj.ToString().Trim();
          ret = RemoveTextInQuotes(ret);
        }

      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
      }
      return ret;
    }

    private static string RemoveTextInQuotes(string input)
    {
      int startIndex = input.IndexOf('<');
      int endIndex = input.LastIndexOf('>');
      if (startIndex >= 0 && endIndex >= 0 && endIndex > startIndex)
      {
        return input.Remove(startIndex, endIndex - startIndex + 1);
      }
      return input;
    }


    public static Double GetDouble(Worksheet worksheet, int row, int column)
    {
      double value = 0;
      try
      {
        object textObj = worksheet.Cells[row, column].Value;
        if (textObj != null)
        {
          Double.TryParse(textObj.ToString(), out value);
        }
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
      }
      return Math.Round(value, 3);
    }
  }
}
