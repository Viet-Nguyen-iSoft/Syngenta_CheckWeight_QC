using Aspose.Cells;
using ClosedXML.Excel;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.DTO;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.UI.FrmUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.enumSoftware;
using Worksheet = Aspose.Cells.Worksheet;

namespace SyngentaWeigherQC.Helper
{
  public class ExcelHelper
  {
    private static string[] ChartColTable = { "G", "H", "I", "J", "K", "L", "M", "N", "O", "P" };
    private static string[] ChartColTableIsChange = { "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP" };
    public static bool ReportExcel(DataReportExcel dataReportExcel, string[] pathFolderSave)
    {
      try
      {
        string templatePath = $@"{Application.StartupPath}\Template\TemplateReportV1.xlsx";
        string passExxcel = "1";
        string nameLine = "";

        XLWorkbook workbook = new XLWorkbook(templatePath);

        DateTime dateTime = dataReportExcel.DateTime;
        List<DataByShiftType> DataByDates = dataReportExcel.DataByDates;

        int sheetId = 1;
        //Đi từng Loại Ca
        foreach (var data_by_shift in DataByDates)
        {
          //Đi từng sp
          foreach (var data_by_product in data_by_shift.DataByProducts)
          {
            //Xuất report tungừ Sheet
            //Fill từng sản phẩm vào các sheet
            IXLWorksheet worksheet = workbook.Worksheet($"Page{sheetId}");
            worksheet.Protect(passExxcel);


            //Fill Thông tin SP
            Production production = data_by_product.Production;
            List<DataByProductionByShift> dataByProductionByShifts = data_by_product.DataByProductionByShifts;

            nameLine = production.InforLine.Name;
            DatalogWeight datalogWeight = data_by_product.DataByProductionByShifts?.FirstOrDefault()?.DatalogWeights?.FirstOrDefault();

            int rowStartUnit1 = 3;
            worksheet.Cell($"I{rowStartUnit1++}").Value = nameLine;
            worksheet.Cell($"I{rowStartUnit1++}").Value = eNumHelper.GetDescription(datalogWeight.DatalogTare.eModeTare);
            worksheet.Cell($"I{rowStartUnit1++}").Value = production?.Name;
            worksheet.Cell($"I{rowStartUnit1++}").Value = production?.PackSize;
            worksheet.Cell($"I{rowStartUnit1++}").Value = production?.StandardFinal;
            worksheet.Cell($"I{rowStartUnit1++}").Value = production?.UpperLimitFinal;
            worksheet.Cell($"I{rowStartUnit1++}").Value = production?.LowerLimitFinal;
            worksheet.Cell($"I{rowStartUnit1++}").Value = datalogWeight?.ShiftLeader.UserName;

            worksheet.Cell($"B1").Value = $"BÁO CÁO SẢN PHẨM - {dateTime.ToString("dd/MM/yyyy")}: {production.Name}";


            //Fill Table
            int rowStart = 70;
            int stt_total = 1;
            int stt_by_shift = 1;
            int indexRowAverageByShiftFrom = 70;
            int indexRowAverageByShiftEnd = 70;
            if (dataByProductionByShifts.Count > 0)
            {
              dataByProductionByShifts = dataByProductionByShifts.OrderBy(x => x.Shift.Id).ToList();
            }

            foreach (var item in dataByProductionByShifts)
            {
              stt_by_shift = 1;
              Shift shift = item.Shift;
              var data_table = AppCore.Ins.ConvertToDTOList(item.DatalogWeights);

              indexRowAverageByShiftEnd = indexRowAverageByShiftFrom + data_table.Count - 1;

              foreach (var raw in data_table)
              {
                worksheet.Cell($"B{rowStart}").Value = stt_total++;
                worksheet.Cell($"C{rowStart}").Value = shift.Name;
                worksheet.Cell($"D{rowStart}").Value = stt_by_shift++;
                worksheet.Cell($"E{rowStart}").Value = raw.DateTime;
                //worksheet.Cell($"AC{rowStart}").Value = datalog_by_shift[i].Id;

                worksheet.Cell($"R{rowStart}").FormulaA1 = $"ROUND(AVERAGE(Q{indexRowAverageByShiftFrom}:Q{indexRowAverageByShiftEnd}), 3)";// $"AVERAGE(Q{indexRowAverageByShiftFrom} : Q{indexRowAverageByShiftEnd})";
                worksheet.Cell($"U{rowStart}").FormulaA1 = $"ROUND(STDEV(G{indexRowAverageByShiftFrom} : P{indexRowAverageByShiftEnd}),3)";

                for (var i = 0; i < 10; i++)
                {
                  if (i < raw.DatalogWeights.Count)
                  {
                    SetColor(worksheet, raw.DatalogWeights[i], production, $"{ChartColTable[i]}{rowStart}");
                    if (raw.DatalogWeights[i].IsChange)
                    {
                      SetColor(worksheet, raw.DatalogWeights[i], production, $"{ChartColTableIsChange[i]}{rowStart}", false);
                    }
                  }
                  else
                  {
                    SetColor(worksheet, null, null, $"{ChartColTable[i]}{rowStart}");
                  }
                }
                rowStart++;
              }


              //Fill thống kê 3 Ca
              int index = 13;
              if (shift.Id == 2 || shift.Id == 5) index = 14;
              else if (shift.Id == 3) index = 15;

              worksheet.Cell($"B{index}").Value = shift.Name;
              worksheet.Cell($"D{index}").FormulaA1 = $"ROUND(STDEV(G{indexRowAverageByShiftFrom} : P{indexRowAverageByShiftEnd}),3)";
              worksheet.Cell($"E{index}").FormulaA1 = $"ROUND(AVERAGE(Q{indexRowAverageByShiftFrom}:Q{indexRowAverageByShiftEnd}), 3)";

              worksheet.Cell($"G{index}").FormulaA1 = $"ROUND((I8-E{index})/(3*D{index}),3)";
              worksheet.Cell($"I{index}").FormulaA1 = $"ROUND((E{index} - I9)/(3*D{index}),3)";

              worksheet.Cell($"K{index}").FormulaA1 = $"MIN(G{index},I{index})";

              worksheet.Cell($"O{index}").FormulaA1 = $"COUNTA(G{indexRowAverageByShiftFrom} : P{indexRowAverageByShiftEnd})";
              worksheet.Cell($"Q{index}").FormulaA1 = $"COUNTIF(G{indexRowAverageByShiftFrom} : P{indexRowAverageByShiftEnd}, \"<\" & I9)";
              worksheet.Cell($"S{index}").FormulaA1 = $"COUNTIF(G{indexRowAverageByShiftFrom} : P{indexRowAverageByShiftEnd}, \">\" & I8)";

              indexRowAverageByShiftFrom = indexRowAverageByShiftEnd + 1;
            }






            for (int i = 328; i >= rowStart; i--)
            {
              worksheet.Row(i).Hide();
            }

            //Tăng Shift Id
            sheetId++;
          }
        }

        for (int i = sheetId; i <= 10; i++)
        {
          workbook.Worksheets.Delete($"Page{i}");
        }

        for (int i = 0; i < pathFolderSave.Count(); i++)
        {
          if (Directory.Exists(pathFolderSave[i]))
          {
            string fileName = pathFolderSave[i] + $"\\Report_{nameLine}_{dateTime.ToString("yyyy_MM_dd")}.xlsx";
            workbook.SaveAs(fileName);
          }
        }

        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }













    private static void SetColor(IXLWorksheet worksheet, DatalogWeight datalogWeight, Production production, string location, bool DataCurrent = true)
    {
      if (DataCurrent)
      {
        if (datalogWeight != null)
        {
          worksheet.Cell(location).Value = datalogWeight.Value;
          if (datalogWeight.IsChange)
          {
            worksheet.Cell(location).Style.Fill.BackgroundColor = XLColor.DarkViolet;
            worksheet.Cell(location).Style.Font.FontColor = XLColor.White;
            return;
          }
          if (datalogWeight.Value > production.UpperLimitFinal)
            worksheet.Cell(location).Style.Fill.BackgroundColor = XLColor.Orange;
          else if (datalogWeight.Value < production.LowerLimitFinal)
          {
            worksheet.Cell(location).Style.Font.FontColor = XLColor.White;
            worksheet.Cell(location).Style.Fill.BackgroundColor = XLColor.Red;
          }
        }
        else
        {
          worksheet.Cell(location).Style.Fill.BackgroundColor = XLColor.DarkGray;
        }
      }
      else
      {
        worksheet.Cell(location).Value = datalogWeight.ValuePrevious;
      }
    }


   
  }
}
