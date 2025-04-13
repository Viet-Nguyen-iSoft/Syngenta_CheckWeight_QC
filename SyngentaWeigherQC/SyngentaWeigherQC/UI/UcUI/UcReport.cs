using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.Responsitory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.eUI;

namespace SyngentaWeigherQC.UI.UcUI
{
  public partial class UcReport : UserControl
  {
    public UcReport()
    {
      InitializeComponent();
    }

    public void SetDataInfor (ExcelClassInforProduct excelClassInforProduct, string titleReport)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataInfor(excelClassInforProduct, titleReport);
        }));
        return;
      }

      this.lbTitle.Text = titleReport;
      this.ucTemplateExcel1.UpdateInforProductUI(excelClassInforProduct);
    }

    public void StatisticalProductUI(StatisticalData statisticalDatas)
    {
      this.ucTemplateExcel1.UpdateSynthetic(statisticalDatas, statisticalDatas.Shift);
    }

    public void StatisticalProductUI(List<StatisticalData> statisticalDatas)
    {
      this.ucTemplateExcel1.UpdateSynthetic(statisticalDatas);
    }

    public void SetChartHistogram(List<Sample> listSample, Production productions)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetChartHistogram(listSample, productions);
        }));
        return;
      }

      this.ucChartHistogram1.SetDataChart(listSample, productions);
    }

    public void SetChartLine(ChartLineData chartLineData)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetChartLine(chartLineData);
        }));
        return;
      }
      this.ucChartLine1.SetDataChart(chartLineData);
    }

    public void SetResultFinal(bool isPass)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetResultFinal(isPass);
        }));
        return;
      }

      this.ucTemplateExcel1.SetResultFinal(isPass);
    }

    public void SetDataTable(List<DatalogWeight> datalogs, List<Sample> samples)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataTable(datalogs, samples);
        }));
        return;
      }

      try
      {
        dataGridView1.Rows.Clear();

        if (datalogs == null) return;
        if (datalogs.Count() <= 0) return;

        DatalogWeight datalogLast = datalogs.LastOrDefault();
        int idProduct = 0;
        Production productions = AppCore.Ins._listAllProductsContainIsDelete.Where(x => x.Id == idProduct).FirstOrDefault();


        //Chia các shift 
        var listDatalogsByShift = datalogs.GroupBy(x=>x.ShiftId).ToList();

        foreach (var item in listDatalogsByShift)
        {
          List<DatalogWeight> listDatalogByShift = item.ToList();
          List<int> listIdDatalog = listDatalogByShift.Select(x=>x.Id).ToList();

          List<Sample> samplesByShift = GetDataSampleByListIdDatalog(samples, listIdDatalog);

          List<double> listValueSample = samplesByShift.Where(x => x.isEnable == true && x.isHasValue == true).Select(x => x.Value).ToList();
          double averageShift = Math.Round(listValueSample.Average(), 2);
          double stdev = Math.Round(AppCore.Ins.Stdev(listValueSample), 3);

          int stt = 1;
          foreach (var datalog in listDatalogByShift)
          {
            int row = dataGridView1.Rows.Add();
            string shift = (datalog.ShiftId != -1) ? AppCore.Ins._listShift.Where(x => x.CodeShift == datalog.ShiftId).Select(x => x.Name).FirstOrDefault() : "";

            dataGridView1.Rows[row].Cells["Column1"].Value = shift;
            dataGridView1.Rows[row].Cells["Column2"].Value = stt++;
            dataGridView1.Rows[row].Cells["Column3"].Value = datalog.CreatedAt;

            List<Sample> sampleChid = samples.Where(x => x.DatalogId == datalog.Id).ToList();
            double averageRaw = Math.Round(sampleChid.Where(x => x.isHasValue == true).Average(x => x.Value), 2);

            //Sample 
            var samp1 = sampleChid?.Where(x => x.LocalId == 1).FirstOrDefault();
            var samp2 = sampleChid?.Where(x => x.LocalId == 2).FirstOrDefault();
            var samp3 = sampleChid?.Where(x => x.LocalId == 3).FirstOrDefault();
            var samp4 = sampleChid?.Where(x => x.LocalId == 4).FirstOrDefault();
            var samp5 = sampleChid?.Where(x => x.LocalId == 5).FirstOrDefault();
            var samp6 = sampleChid?.Where(x => x.LocalId == 6).FirstOrDefault();
            var samp7 = sampleChid?.Where(x => x.LocalId == 7).FirstOrDefault();
            var samp8 = sampleChid?.Where(x => x.LocalId == 8).FirstOrDefault();
            var samp9 = sampleChid?.Where(x => x.LocalId == 9).FirstOrDefault();
            var samp10 = sampleChid?.Where(x => x.LocalId == 10).FirstOrDefault();
            var samp11 = sampleChid?.Where(x => x.LocalId == 11).FirstOrDefault();
            var samp12 = sampleChid?.Where(x => x.LocalId == 12).FirstOrDefault();
            var samp13 = sampleChid?.Where(x => x.LocalId == 13).FirstOrDefault();
            var samp14 = sampleChid?.Where(x => x.LocalId == 14).FirstOrDefault();
            var samp15 = sampleChid?.Where(x => x.LocalId == 15).FirstOrDefault();


            //Sample 
            double samp1_Value = (samp1 != null) ? samp1.Value : 0;
            double samp2_Value = (samp2 != null) ? samp2.Value : 0;
            double samp3_Value = (samp3 != null) ? samp3.Value : 0;
            double samp4_Value = (samp4 != null) ? samp4.Value : 0;
            double samp5_Value = (samp5 != null) ? samp5.Value : 0;
            double samp6_Value = (samp6 != null) ? samp6.Value : 0;
            double samp7_Value = (samp7 != null) ? samp7.Value : 0;
            double samp8_Value = (samp8 != null) ? samp8.Value : 0;
            double samp9_Value = (samp9 != null) ? samp9.Value : 0;
            double samp10_Value = (samp10 != null) ? samp10.Value : 0;
            double samp11_Value = (samp11 != null) ? samp11.Value : 0;
            double samp12_Value = (samp12 != null) ? samp12.Value : 0;
            double samp13_Value = (samp13 != null) ? samp13.Value : 0;
            double samp14_Value = (samp14 != null) ? samp14.Value : 0;
            double samp15_Value = (samp15 != null) ? samp15.Value : 0;


            //Tô màu
            double max = productions.UpperLimitFinal;
            double min = productions.LowerLimitFinal;

            //Check Cân lại
            //Sample 
            var samp1Edit = (samp1 != null) ? samp1.isEdited : false;
            var samp2Edit = (samp2 != null) ? samp2.isEdited : false;
            var samp3Edit = (samp3 != null) ? samp3.isEdited : false;
            var samp4Edit = (samp4 != null) ? samp4.isEdited : false;
            var samp5Edit = (samp5 != null) ? samp5.isEdited : false;
            var samp6Edit = (samp6 != null) ? samp6.isEdited : false;
            var samp7Edit = (samp7 != null) ? samp7.isEdited : false;
            var samp8Edit = (samp8 != null) ? samp8.isEdited : false;
            var samp9Edit = (samp9 != null) ? samp9.isEdited : false;
            var samp10Edit = (samp10 != null) ? samp10.isEdited : false;
            var samp11Edit = (samp11 != null) ? samp11.isEdited : false;
            var samp12Edit = (samp12 != null) ? samp12.isEdited : false;
            var samp13Edit = (samp13 != null) ? samp13.isEdited : false;
            var samp14Edit = (samp14 != null) ? samp14.isEdited : false;
            var samp15Edit = (samp15 != null) ? samp15.isEdited : false;


            SetColorDgv(row, "Column4", (double)samp1_Value, min, max, samp1Edit);
            SetColorDgv(row, "Column5", (double)samp2_Value, min, max, samp2Edit);
            SetColorDgv(row, "Column6", (double)samp3_Value, min, max, samp3Edit);
            SetColorDgv(row, "Column7", (double)samp4_Value, min, max, samp4Edit);
            SetColorDgv(row, "Column8", (double)samp5_Value, min, max, samp5Edit);
            SetColorDgv(row, "Column9", (double)samp6_Value, min, max, samp6Edit);
            SetColorDgv(row, "Column10", (double)samp7_Value, min, max, samp7Edit);
            SetColorDgv(row, "Column11", (double)samp8_Value, min, max, samp8Edit);
            SetColorDgv(row, "Column20", (double)samp9_Value, min, max, samp9Edit);
            SetColorDgv(row, "Column21", (double)samp10_Value, min, max, samp10Edit);
            SetColorDgv(row, "Column22", (double)samp11_Value, min, max, samp11Edit);
            SetColorDgv(row, "Column23", (double)samp12_Value, min, max, samp12Edit);
            SetColorDgv(row, "Column24", (double)samp10_Value, min, max, samp10Edit);
            SetColorDgv(row, "Column25", (double)samp11_Value, min, max, samp11Edit);
            SetColorDgv(row, "Column26", (double)samp12_Value, min, max, samp12Edit);

            if (samp1_Value != 0)
              dataGridView1.Rows[row].Cells["Column4"].Value = samp1_Value;
            else
              dataGridView1.Rows[row].Cells["Column4"].Style.BackColor = Color.DimGray;

            if (samp2_Value != 0)
              dataGridView1.Rows[row].Cells["Column5"].Value = samp2_Value;
            else
              dataGridView1.Rows[row].Cells["Column5"].Style.BackColor = Color.DimGray;

            if (samp3_Value != 0)
              dataGridView1.Rows[row].Cells["Column6"].Value = samp3_Value;
            else
              dataGridView1.Rows[row].Cells["Column6"].Style.BackColor = Color.DimGray;

            if (samp4_Value != 0)
              dataGridView1.Rows[row].Cells["Column7"].Value = samp4_Value;
            else
              dataGridView1.Rows[row].Cells["Column7"].Style.BackColor = Color.DimGray;

            if (samp5_Value != 0)
              dataGridView1.Rows[row].Cells["Column8"].Value = samp5_Value;
            else
              dataGridView1.Rows[row].Cells["Column8"].Style.BackColor = Color.DimGray;

            if (samp6_Value != 0)
              dataGridView1.Rows[row].Cells["Column9"].Value = samp6_Value;
            else
              dataGridView1.Rows[row].Cells["Column9"].Style.BackColor = Color.DimGray;

            if (samp7_Value != 0)
              dataGridView1.Rows[row].Cells["Column10"].Value = samp7_Value;
            else
              dataGridView1.Rows[row].Cells["Column10"].Style.BackColor = Color.DimGray;

            if (samp8_Value != 0)
              dataGridView1.Rows[row].Cells["Column11"].Value = samp8_Value;
            else
              dataGridView1.Rows[row].Cells["Column11"].Style.BackColor = Color.DimGray;
            if (samp9_Value != 0)
              dataGridView1.Rows[row].Cells["Column20"].Value = samp9_Value;
            else
              dataGridView1.Rows[row].Cells["Column20"].Style.BackColor = Color.DimGray;

            if (samp10_Value != 0)
              dataGridView1.Rows[row].Cells["Column21"].Value = samp10_Value;
            else
              dataGridView1.Rows[row].Cells["Column21"].Style.BackColor = Color.DimGray;
            if (samp11_Value != 0)
              dataGridView1.Rows[row].Cells["Column22"].Value = samp11_Value;
            else
              dataGridView1.Rows[row].Cells["Column22"].Style.BackColor = Color.DimGray;
            if (samp12_Value != 0)
              dataGridView1.Rows[row].Cells["Column23"].Value = samp12_Value;
            else
              dataGridView1.Rows[row].Cells["Column23"].Style.BackColor = Color.DimGray;
            if (samp13_Value != 0)
              dataGridView1.Rows[row].Cells["Column24"].Value = samp13_Value;
            else
              dataGridView1.Rows[row].Cells["Column24"].Style.BackColor = Color.DimGray;
            if (samp14_Value != 0)
              dataGridView1.Rows[row].Cells["Column25"].Value = samp14_Value;
            else
              dataGridView1.Rows[row].Cells["Column25"].Style.BackColor = Color.DimGray;
            if (samp15_Value != 0)
              dataGridView1.Rows[row].Cells["Column26"].Value = samp15_Value;
            else
              dataGridView1.Rows[row].Cells["Column26"].Style.BackColor = Color.DimGray;


            //Tô màu
            double maxSample = sampleChid.Min(x => x.Value);
            double minSample = sampleChid.Max(x => x.Value);

            dataGridView1.Rows[row].Cells["Column12"].Value = averageRaw;
            dataGridView1.Rows[row].Cells["Column13"].Value = averageShift;
            dataGridView1.Rows[row].Cells["Column14"].Value = productions.StandardFinal;
            dataGridView1.Rows[row].Cells["Column15"].Value = (averageRaw >= productions.StandardFinal) ? "Đạt" : "Không đạt";
            dataGridView1.Rows[row].Cells["Column16"].Value = stdev;
            dataGridView1.Rows[row].Cells["Column17"].Value = minSample;
            dataGridView1.Rows[row].Cells["Column18"].Value = maxSample;

            //Đánh giá
            dataGridView1.Rows[row].Cells["Column15"].Style.BackColor = (averageRaw >= productions.StandardFinal) ? Color.Green : Color.Red;
            dataGridView1.Rows[row].Cells["Column15"].Style.ForeColor = Color.White;

            dataGridView1.Columns["Column17"].Visible = false;
            dataGridView1.Columns["Column18"].Visible = false;
          }
        }

      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex);
      }
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

    private void SetColorDgv(int rowIndex, string nameColumn, double value, double min, double max, bool isReWeigher = false)
    {
      if (isReWeigher == true)
      {
        dataGridView1.Rows[rowIndex].Cells[nameColumn].Style.BackColor = Color.Purple;
        dataGridView1.Rows[rowIndex].Cells[nameColumn].Style.ForeColor = Color.White;
        return;
      }
      if (value < min)
      {
        dataGridView1.Rows[rowIndex].Cells[nameColumn].Style.BackColor = Color.Red;
        dataGridView1.Rows[rowIndex].Cells[nameColumn].Style.ForeColor = Color.White;
      }
      else if (value > max)
      {
        dataGridView1.Rows[rowIndex].Cells[nameColumn].Style.BackColor = Color.DarkOrange;
        dataGridView1.Rows[rowIndex].Cells[nameColumn].Style.ForeColor = Color.White;
      }
    }


    


  }
}
