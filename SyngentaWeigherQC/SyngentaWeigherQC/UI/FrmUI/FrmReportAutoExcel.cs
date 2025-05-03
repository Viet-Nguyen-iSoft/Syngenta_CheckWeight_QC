using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.enumSoftware;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmReportAutoExcel : Form
  {
    private DateTime _dt_DB = DateTime.Now;
    private string _stationName = "";
    public FrmReportAutoExcel()
    {
      InitializeComponent();
    }

    public FrmReportAutoExcel(DateTime dateTime)
    {
      InitializeComponent();
      _dt_DB = dateTime;
    }


    private string fileNameReportLocal = "";
    private string fileNameReportOneDrive = "";
    private void FrmReportAutoExcel_Load(object sender, EventArgs e)
    {
      ////Load tên file excel
      //fileNameReportLocal = AppCore.Ins._stationCurrent.PathReportLocal + $"\\Dailys\\ReportDailys_{_stationName}_{_dt_DB.ToString("yyyy_MM_dd")}.xlsx";
      //fileNameReportOneDrive = AppCore.Ins._stationCurrent.PathReportOneDrive + $"\\Dailys\\ReportDailys_{_stationName}_{_dt_DB.ToString("yyyy_MM_dd")}.xlsx";

      //if (!File.Exists(fileNameReportLocal))
      //{
      //  LoadDataReport();
      //}
      //else
      //{
      //  this.Close();
      //}  
    }

    private  ExportManual exportManual = new ExportManual();
    private async void LoadDataReport()
    {
      string fileDB = Application.StartupPath + $"\\Database\\{_dt_DB.ToString("yyyyMMdd")}_data_log.sqlite";
      if (File.Exists(fileDB))
      {
        List<Sample> samples = await AppCore.Ins.GetSampleReport(_dt_DB);
        List<DatalogWeight> datalogs = await AppCore.Ins.GetDatalogReport(_dt_DB);

        if (datalogs.Count > 0)
        {
          exportManual = new ExportManual()
          {
            DateTime = _dt_DB,
            Datalogs = datalogs,
            Samples = samples,
          };

          ReportExcel(exportManual);

          this.Close();
        }
        else
        {
          this.Close();
        }
      }
    }


   

   
    private void ReportExcel(ExportManual report)
    {
      
      //string templatePath = $@"{Application.StartupPath}\Template\AutoReportV2.xlsx";
      //string passExxcel = AppCore.Ins._stationCurrent.PassReport;

      //XLWorkbook workbook = new XLWorkbook(templatePath);
      //DateTime dateTime = report.DateTime;
      //List<DatalogWeight> datalogsAll = report.Datalogs;
      //List<Sample> samplesAll = report.Samples;

      ////Tách thành các sản phẩm khác nhau
      //var groupedDatalogByProduct = datalogsAll.GroupBy(x => x.ProductId).ToList();

      //int sheetId = 1;
      ////double averageAllShift = 0;
      //double averageShift = 0;
      //foreach (var item in groupedDatalogByProduct)
      //{
      //  //Fill từng sản phẩm vào các sheet
      //  IXLWorksheet worksheet = workbook.Worksheet($"Page{sheetId}");
      //  worksheet.Protect(passExxcel);

      //  //Datalog từng sản phẩm
      //  List<DatalogWeight> datalogsByProduct = item.ToList();
      //  if (datalogsByProduct.Count==0) return;

      //  DatalogWeight datalogLast = datalogsByProduct.LastOrDefault();

      //  //Phần 1: Fill thông tin sản phẩm
      //  string nameLine = AppCore.Ins._listInforLine?.Where(x => x.Id == datalogLast.StationId).FirstOrDefault()?.Name;
      //  string modeTare = (datalogLast.IsTareWithLabel == eModeTare.TareWithLabel) ? "Tare có nhãn" : "Tare không nhãn";

      //  Production productions = AppCore.Ins._listAllProductsContainIsDelete?.Where(x => x.Id == datalogLast.ProductId).FirstOrDefault();
      //  string nameProduct = (productions != null) ? productions.Name : "";
      //  double packSize = (productions != null) ? productions.PackSize : 0;
      //  double standard = (productions != null) ? productions.StandardFinal : 0;
      //  double upper = (productions != null) ? productions.UpperLimitFinal : 0;
      //  double lower = (productions != null) ? productions.LowerLimitFinal : 0;

      //  int idUser = datalogLast.UserId;
      //  string nameShiftLeader = (idUser > 0) ? AppCore.Ins._listShiftLeader?.Where(x => x.Id == idUser).FirstOrDefault()?.UserName : "";

      //  int rowStartUnit1 = 3;
      //  worksheet.Cell($"I{rowStartUnit1++}").Value = nameLine;
      //  worksheet.Cell($"I{rowStartUnit1++}").Value = modeTare;
      //  worksheet.Cell($"I{rowStartUnit1++}").Value = nameProduct;
      //  worksheet.Cell($"I{rowStartUnit1++}").Value = packSize;
      //  worksheet.Cell($"I{rowStartUnit1++}").Value = standard;
      //  worksheet.Cell($"I{rowStartUnit1++}").Value = upper;
      //  worksheet.Cell($"I{rowStartUnit1++}").Value = lower;
      //  worksheet.Cell($"I{rowStartUnit1++}").Value = nameShiftLeader;

      //  worksheet.Cell($"B1").Value = $"BÁO CÁO SẢN PHẨM - {dateTime.ToString("dd/MM/yyyy")}: {nameProduct}";

      //  //worksheet.Cell("A3").FormulaA1 = "(A1 + A2) * B1";

      //  //Phần 2: Fill table
      //  int rowStart = 70;
      //  int shiftId = 0;
      //  string nameShift = "";
      //  double target = 0;
      //  var groupDatalogsByShift = datalogsByProduct.GroupBy(x => x.ShiftId).ToList();
      //  if (groupDatalogsByShift != null)
      //  {
      //    if (groupDatalogsByShift.Count <= 0) return;
      //    int indexRowAverageByShift = 70;
      //    int noAll = 1;
      //    foreach (var data in groupDatalogsByShift)
      //    {
      //      List<DatalogWeight> datalog_by_shift = data.ToList();
      //      List<int> datalogId = datalog_by_shift.Select(x => x.Id).ToList();

      //      List<Sample> sample_by_shift = GetDataSampleByListIdDatalog(samplesAll, datalogId);
      //      List<Sample> sample_by_shift_remove_valueZero = sample_by_shift.Where(x => x.isHasValue == true && x.isEnable == true).ToList();
      //      List<double> listSampleValue = sample_by_shift_remove_valueZero.Select(x => x.Value).ToList();

      //      //Trung bình Sample theo ca
      //      averageShift = Math.Round(listSampleValue.Average(), 2);

      //      //FillTable
      //      int stt = 1;
      //      for (int i = 0; i < datalog_by_shift.Count; i++)
      //      {
      //        shiftId = datalog_by_shift[i].ShiftId;
      //        nameShift = AppCore.Ins._listShift?.Where(x => x.CodeShift == shiftId).FirstOrDefault()?.Name;

      //        worksheet.Cell($"E{rowStart}").Value = datalog_by_shift[i].CreatedAt;
      //        worksheet.Cell($"AC{rowStart}").Value = datalog_by_shift[i].Id;
      //        worksheet.Cell($"B{rowStart}").Value = noAll++;
      //        worksheet.Cell($"C{rowStart}").Value = nameShift;
      //        worksheet.Cell($"W{rowStart}").FormulaA1 = $"AVERAGE(V{indexRowAverageByShift} : V{indexRowAverageByShift + datalog_by_shift.Count - 1})";
      //        worksheet.Cell($"Z{rowStart}").FormulaA1 = $"STDEV(G{indexRowAverageByShift} : U{indexRowAverageByShift + datalog_by_shift.Count - 1})";
      //        worksheet.Cell($"D{rowStart}").Value = stt++;


      //        //Sample cho từng Datalog
      //        List<Sample> sampleSingle = sample_by_shift?.Where(x => x.DatalogId == datalog_by_shift[i].Id).ToList();

      //        AppCore.Ins.SetColor(worksheet, sampleSingle[0].Value, lower, upper, $"G{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[1].Value, lower, upper, $"H{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[2].Value, lower, upper, $"I{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[3].Value, lower, upper, $"J{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[4].Value, lower, upper, $"K{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[5].Value, lower, upper, $"L{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[6].Value, lower, upper, $"M{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[7].Value, lower, upper, $"N{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[8].Value, lower, upper, $"O{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[9].Value, lower, upper, $"P{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[10].Value, lower, upper, $"Q{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[11].Value, lower, upper, $"R{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[12].Value, lower, upper, $"S{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[13].Value, lower, upper, $"T{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[14].Value, lower, upper, $"U{rowStart}");

      //        AppCore.Ins.SetColor(worksheet, sampleSingle[0], $"AL{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[1], $"AM{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[2], $"AN{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[3], $"AQ{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[4], $"AP{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[5], $"AQ{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[6], $"AR{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[7], $"AS{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[8], $"AT{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[9], $"AU{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[10], $"AV{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[11], $"AW{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[12], $"AX{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[13], $"AY{rowStart}");
      //        AppCore.Ins.SetColor(worksheet, sampleSingle[14], $"AZ{rowStart}");

      //        rowStart++;
      //      }


      //      double stdev = AppCore.Ins.Stdev(listSampleValue);
      //      target = productions.StandardFinal;

      //      //double minValue = listSampleValue.Min();
      //      //double maxValue = listSampleValue.Max();

      //      //double Cp = (stdev != 0) ? ((maxValue - minValue) / (6 * stdev)) : 0;
      //      //double Cpk_H = (stdev != 0) ? ((maxValue - averageShift) / (3 * stdev)) : 0;
      //      //double Cpk_L = (stdev != 0) ? ((averageShift - minValue) / (3 * stdev)) : 0;
      //      //double Cpk = Math.Min(Cpk_H, Cpk_L);

      //      //int totalSamples = sample_by_shift_remove_valueZero.Where(x => x.isHasValue && x.isEnable).Count();
      //      //int numberSamplesOver = (sample_by_shift_remove_valueZero != null) ? sample_by_shift_remove_valueZero.Where(x => x.Value > productions.UpperLimitFinal).Count() : 0;
      //      //int numberSamplesLower = (sample_by_shift_remove_valueZero != null) ? sample_by_shift_remove_valueZero.Where(x => x.Value < productions.LowerLimitFinal).Count() : 0;

      //      int index = 13;
      //      if (shiftId == 2 || shiftId == 5) index = 14;
      //      else if (shiftId == 3) index = 15;

      //      worksheet.Cell($"B{index}").Value = nameShift;
      //      worksheet.Cell($"E{index}").FormulaA1 = $"ROUND(STDEV(G{indexRowAverageByShift} : U{indexRowAverageByShift + datalog_by_shift.Count - 1}),3)";
      //      worksheet.Cell($"G{index}").FormulaA1 = $"ROUND(AVERAGE(V{indexRowAverageByShift} : V{indexRowAverageByShift + datalog_by_shift.Count - 1}),3)";

      //      worksheet.Cell($"M{index}").FormulaA1 = $"ROUND((G{index} - I9)/(3*E{index}),3)";
      //      worksheet.Cell($"K{index}").FormulaA1 = $"ROUND((I8-G{index})/(3*E{index}),3)";

      //      //worksheet.Cell($"K{index}").FormulaA1 = $"ROUND((MAX(G{indexRowAverageByShift} : U{indexRowAverageByShift + datalog_by_shift.Count - 1})-G{index})/(3*E{index}),3)";
      //      //worksheet.Cell($"M{index}").FormulaA1 = $"ROUND((G{index} - MIN(G{indexRowAverageByShift} : U{indexRowAverageByShift + datalog_by_shift.Count - 1}))/(3*E{index}),3)";
      //      worksheet.Cell($"O{index}").FormulaA1 = $"MIN(M{index},K{index})";
      //      worksheet.Cell($"S{index}").FormulaA1 = $"COUNTA(G{indexRowAverageByShift} : U{indexRowAverageByShift + datalog_by_shift.Count - 1})";
      //      worksheet.Cell($"V{index}").FormulaA1 = $"COUNTIF(G{indexRowAverageByShift} : U{indexRowAverageByShift + datalog_by_shift.Count - 1}, \"<\" & I9)";
      //      worksheet.Cell($"X{index}").FormulaA1 = $"COUNTIF(G{indexRowAverageByShift} : U{indexRowAverageByShift + datalog_by_shift.Count - 1}, \">\" & I8)";
      //      indexRowAverageByShift += datalog_by_shift.Count();
      //    } 
      //  }

      //  for (int i = 328; i >= rowStart; i--)
      //  {
      //    worksheet.Row(i).Hide();
      //  }

      ////  sheetId++;
      //}


      //for (int i = groupedDatalogByProduct.Count() + 1; i <= 10; i++)
      //{
      //  workbook.Worksheets.Delete($"Page{i}");
      //}

      //try
      //{
      //  if (!File.Exists(fileNameReportLocal))
      //  {
      //    workbook.SaveAs(fileNameReportLocal);
      //  }

      //  if (!File.Exists(fileNameReportOneDrive))
      //  {
      //    workbook.SaveAs(fileNameReportOneDrive);
      //  }
      //}
      //catch (Exception ex)
      //{
      //  AppCore.Ins.LogErrorToFileLog(ex.ToString());
      //  MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      //}
    }


    private List<Sample> GetDataSampleByListIdDatalog(List<Sample> listSamples, List<int> listIdDatalogs)
    {
      List<Sample> rs = new List<Sample>();

      foreach (var id in listIdDatalogs)
      {
        List<Sample> samples = listSamples?.Where(x => x.DatalogId == id).ToList();
        rs.AddRange(samples);
      }
      return rs;
    }



  }
}
