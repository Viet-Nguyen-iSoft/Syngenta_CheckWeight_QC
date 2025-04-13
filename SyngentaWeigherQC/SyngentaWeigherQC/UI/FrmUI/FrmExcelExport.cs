using Aspose.Cells;
using Aspose.Cells.Timelines;
using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Irony.Parsing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SynCheckWeigherLoggerApp.DashboardViews;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.Responsitory;
using SyngentaWeigherQC.UI.UcUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static ClosedXML.Excel.XLPredefinedFormat;
using static SyngentaWeigherQC.eNum.eUI;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Application = System.Windows.Forms.Application;
using Color = System.Drawing.Color;
using DateTime = System.DateTime;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmExcelExport : Form
  {
    public FrmExcelExport()
    {
      InitializeComponent();
    }
    #region Singleton parttern
    private static FrmExcelExport _Instance = null;
    public static FrmExcelExport Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new FrmExcelExport();
        }
        return _Instance;
      }
    }
    #endregion

    private void FrmExcelExport_Load(object sender, EventArgs e)
    {
      this.cbShift.SelectedIndex = 0;
      this.flowLayoutPanelProduct.Visible = false;
      this.flowLayoutPanelProduct.WrapContents = false;
      this.flowLayoutPanelProduct.FlowDirection = FlowDirection.LeftToRight;
      dataGridView1.CellClick += DataGridView1_CellMouseEnter;
    }

   

    private DateTime fromDate = DateTime.Now;
    private DateTime toDate = DateTime.Now;
    private int shiftIndex = 0;
    private void btnPreview_Click(object sender, EventArgs e)
    {
      if (!backgroundWorkerLoadData.IsBusy)
      {
        backgroundWorkerLoadData.RunWorkerAsync();
      }
    }

    private void AddListDataExcel(List<DataReportByDate> dataReportByDates)
    {
      if (dataReportByDates.Count <= 0) return;

      this.flowLayoutPanelProduct.Controls.Clear();
      //ĐI từng ngày
      int cnt = 0;
      foreach (var item in dataReportByDates)
      {
        cnt++;
        //Các sản phẩm ngày hôm đó
        DateTime dt_Report = item.DateTime;
        List<DataDateGroupByProduct> dataReportByDate = item.DataDateGroupByProducts.ToList();

        if (dataReportByDate.Count > 0)
        {
          //Đi từng sản phẩm
          foreach (var dataDateGroupByProduct in dataReportByDate)
          {
            //List<Datalog> datalogs = dataDateGroupByProduct.Datalogs.ToList();
            //List<Sample> samples = dataDateGroupByProduct.Samples.ToList();
            int idProduct = dataDateGroupByProduct.ProductId;
            string nameProduct = AppCore.Ins._listAllProductsContainIsDelete?.Where(x => x.Id == idProduct)?.Select(x => x.Name).FirstOrDefault();
            this.flowLayoutPanelProduct.Visible = true;
            string textButton = dt_Report.ToString($"dd/MM/yyyy - {nameProduct}");
            CreateButtonWithDataProduction(textButton, dataDateGroupByProduct);
          }

          if (flowLayoutPanelProduct.Controls.Count > 0 && flowLayoutPanelProduct.Controls[0] is Button)
          {
            Button firstButton = (Button)flowLayoutPanelProduct.Controls[0];
            firstButton.PerformClick();
          }
        }

        Progress((cnt * 100 / dataReportByDates.Count()));
      }
    }

    


    private void ClearPage()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          ClearPage();
        }));
        return;
      }

      this.flowLayoutPanelProduct.Controls.Clear();
      this.flowLayoutPanelProduct.Visible = false;
      this.dataGridView1.Rows.Clear();

      this.ucTemplateExcel1.UpdateInforProductUI(null);
      this.ucTemplateExcel1.ClearStatistical();
      this.ucChartHistogram1.SetDataChart(null, null);
      this.ucChartLine1.SetDataChart(null);
    }


    private void CreateButtonWithDataProduction(string text, DataDateGroupByProduct dataDateGroupByProduct)
    {
      //string title = $"{date.ToString("dd/MM/yyyy")}-"

      Button button = new System.Windows.Forms.Button();
      button.BackColor = Color.FromArgb(72, 61, 139);
      button.FlatAppearance.BorderColor = Color.FromArgb(102, 102, 153);
      button.FlatAppearance.BorderSize = 3;
      button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
      button.ForeColor = System.Drawing.Color.White;
      button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      button.Location = new System.Drawing.Point(3, 3);
      button.Name = text;
      button.Size = new System.Drawing.Size(300, 40);
      button.TabIndex = 16;
      button.Text = text;
      button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      button.UseVisualStyleBackColor = false;
      button.Tag = dataDateGroupByProduct;
      button.Click += EvenButton_Click;
      this.flowLayoutPanelProduct.Controls.Add(button);
    }

    private void EvenButton_Click(object sender, EventArgs e)
    {
      
    }


  
    private void DataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
    {
      

    
    }

    private ChartLineData ProcessingDataChartLine(List<DatalogWeight> datalogs, List<Sample> samples, Models.Production productions)
    {
      List<double> averageRaw = new List<double>();
      double averageTotal = Math.Round(samples.Average(x => x.Value), 2);
      datalogs = datalogs.OrderBy(x=>x.Id).ToList();
      foreach (DatalogWeight datalog in datalogs)
      {
        double average = samples.Where(x => x.DatalogId == datalog.Id && x.isHasValue==true).Average(x => x.Value);
        averageRaw.Add(Math.Round(average, 2));
      }
      ChartLineData chartLineData = new ChartLineData()
      {
        Min = productions.LowerLimitFinal,
        Max = productions.UpperLimitFinal,
        Target = productions.StandardFinal,
        Average = averageTotal,
        AverageRaw = averageRaw,
      };
      return chartLineData;
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



    private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
    {
      fromDate = dateTimePickerFrom.Value;
    }

    private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
    {
      toDate = dateTimePickerTo.Value;
    }



    private List<DataReportByDate> dataReportByDates = new List<DataReportByDate>();
    private async void backgroundWorkerLoadData_DoWork(object sender, DoWorkEventArgs e)
    {
      
    }

    private void backgroundWorkerLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (dataReportByDates.Count > 0)
      {
        AddListDataExcel(dataReportByDates);
      }
      else
      {
        ClearPage();
        new FrmNotification().ShowMessage("Không có dữ liệu !", eMsgType.Info);
      }
    }


    private string nameFolder = "";

    private void btnExport_Click(object sender, EventArgs e)
    {
      
      
    }

    private void Progress(int value)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          Progress(value);
        }));
        return;
      }

      value = value > 100 ? 100 : value;
      this.progressBar1.Value = value;
    }

    private void FrmConfirm_OnSendOKClicked(object sender)
    {
      Process.Start("explorer.exe", nameFolder);
      //Process.Start(fileNameExcel);
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


    
    

    private void cbShift_SelectedIndexChanged(object sender, EventArgs e)
    {
      shiftIndex = cbShift.SelectedIndex; 
    }
  }
}
