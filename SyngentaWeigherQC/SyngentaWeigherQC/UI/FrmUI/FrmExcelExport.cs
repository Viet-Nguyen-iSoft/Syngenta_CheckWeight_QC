using Irony.Parsing;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.DTO;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.Responsitory;
using SyngentaWeigherQC.UI.UcUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.eUI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Color = System.Drawing.Color;
using DateTime = System.DateTime;
using Production = SyngentaWeigherQC.Models.Production;

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


    #region Get Set
    private int ShiftId
    {
      get { return cbShift.SelectedIndex; }
    }
    private DateTime From
    {
      get { return dateTimePickerFrom.Value; }
    }
    private DateTime To
    {
      get { return dateTimePickerTo.Value; }
    }

    public List<InforLine> CbbLines
    {
      set
      {
        this.cbbLine.DataSource = value;
        this.cbbLine.DisplayMember = "Name";
      }
    }
    #endregion


    private int _colShift = 0;
    private int _colNo = 1;
    private int _colDatetime = 2;
    private int _colDataWeight = 3; //.....
    private int _colAvgRaw = 13;
    private int _colAvgTotal = 14;
    private int _colEvaluate = 15;

    private void FrmExcelExport_Load(object sender, EventArgs e)
    {
      this.cbShift.SelectedIndex = 0;
      CbbLines = AppCore.Ins._listInforLine?.Where(x => x.IsEnable == true).ToList();
    }

    private async void btnPreview_Click(object sender, EventArgs e)
    {
      var lineChoose = cbbLine.SelectedItem as InforLine;
      int lineId = (lineChoose != null) ? lineChoose.Id : 0;
      var datalogs = await AppCore.Ins.LoadAllDatalogWeight(lineId, From, To, ShiftId);

      var dataReportExcels = AppCore.Ins.GenerateDataReport(datalogs);

      ShowResult(dataReportExcels);
    }


    private void ShowResult(List<DataReportExcel> dataReportExcels)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          ShowResult(dataReportExcels);
        }));
        return;
      }

      this.flowLayoutPanelProduct.Controls.Clear();
      //Đi từng ngày
      foreach (var dataReportExcel in dataReportExcels)
      {
        //Đi từng Ca
        foreach (var data_by_shift in dataReportExcel.DataByDates)
        {
          foreach (var data_by_product in data_by_shift.DataByProducts)
          {
            string textButton = dataReportExcel.DateTime.ToString("yyyy/MM/dd") + " - " +
                                data_by_shift.Shift.Name + " - " +
                                data_by_product.Production.Name;

            CreateButtonWithDataProduction(textButton, data_by_product.DatalogWeights);
          }
        }
      }
    }

    private void CreateButtonWithDataProduction(string text, List<DatalogWeight> datalogWeights)
    {
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
      button.Tag = datalogWeights;
      button.Click += EventButton_Click;
      this.flowLayoutPanelProduct.Controls.Add(button);
    }

    private void EventButton_Click(object sender, EventArgs e)
    {
      if (sender is Button)
      {
        for (int i = 0; i < this.flowLayoutPanelProduct.Controls.Count; i++)
        {
          if (this.flowLayoutPanelProduct.Controls[i] is Button)
          {
            Button localButton = (Button)(this.flowLayoutPanelProduct.Controls[i]);
            if (localButton != null)
            {
              localButton.BackColor = Color.FromArgb(72, 61, 139);
            }
          }
        }


        Button button = (Button) sender;
        button.BackColor = Color.FromArgb(204, 102, 255);

        //Xử lý data
        var datalogWeights = (List<DatalogWeight>)button.Tag;

        Production production = datalogWeights.FirstOrDefault().Production;
        eModeTare eModeTare = datalogWeights.FirstOrDefault().DatalogTare.eModeTare;

        //Thông tin report
        ExcelClassInforProduct excelClassInforProduct= new ExcelClassInforProduct();
        excelClassInforProduct.NameLine = production.InforLine.Name;
        excelClassInforProduct.ModeTare = eNumHelper.GetDescription(production.InforLine.eModeTare);
        excelClassInforProduct.ProductName = production.Name;
        excelClassInforProduct.PackSize = production.PackSize;
        excelClassInforProduct.Standard = production.StandardFinal;
        excelClassInforProduct.Upper = production.UpperLimitFinal;
        excelClassInforProduct.Lower = production.LowerLimitFinal;

        ucTemplateExcel1.UpdateInforProductUI(excelClassInforProduct);

        //Table
        var data_table = AppCore.Ins.ConvertToDTOList(datalogWeights);
        SetDataTable(data_table, production);

        //Histogram
        ucChartHistogram1.SetDataChart(datalogWeights, production);

        //ChartLine
        var dataChartLine = AppCore.Ins.CvtDatalogWeightToChartLine(datalogWeights, production);
        ucChartLine1.SetDataChart(dataChartLine);

        //Thống kê 3 ca
        LoadSumaryByShift(datalogWeights, production);
      }
    }

    private void LoadSumaryByShift(List<DatalogWeight> listDatalogByLine, Production production)
    {
      ucTemplateExcel1.ClearSumary();
      var rs = AppCore.Ins.SumaryDTO(listDatalogByLine);
      foreach (var item in rs)
      {
        ucTemplateExcel1.SetValueSumary(item);
      }
    }

    public void SetDataTable(List<TableDatalogDTO> tableDatalogDTOs, Production production)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataTable(tableDatalogDTOs, production);
        }));
        return;
      }

      dataGridView1.Rows.Clear();
      if (tableDatalogDTOs.Count() > 0)
      {
        tableDatalogDTOs.Reverse();
        foreach (var item in tableDatalogDTOs)
        {
          dataGridView1.Rows.Insert(0);
          int cnt_number_raw= item.DatalogWeights.Count();

          dataGridView1.Rows[0].Cells[_colShift].Value = item.Shift;
          dataGridView1.Rows[0].Cells[_colNo].Value = item.No;
          dataGridView1.Rows[0].Cells[_colDatetime].Value = item.DateTime;

          for (int i = 1; i <= cnt_number_raw; i++)
          {
            dataGridView1.Rows[0].Cells[_colDataWeight - 1 + i].Value = item.DatalogWeights.ElementAtOrDefault(i - 1).Value;
            dataGridView1.Rows[0].Cells[_colDataWeight - 1 + i].Tag = item.DatalogWeights.ElementAtOrDefault(i - 1);

            var color = AppCore.Ins.EvaluateRetureColor(item.DatalogWeights.ElementAtOrDefault(i - 1), production);
            dataGridView1.Rows[0].Cells[_colDataWeight - 1 + i].Style.BackColor = color.Item1;
            dataGridView1.Rows[0].Cells[_colDataWeight - 1 + i].Style.ForeColor = color.Item2;
          }

          dataGridView1.Rows[0].Cells[_colAvgRaw].Value = item.AvgRaw;
          dataGridView1.Rows[0].Cells[_colAvgTotal].Value = item.AvgTotal;
          dataGridView1.Rows[0].Cells[_colEvaluate].Value = eNumHelper.GetDescription(item.eEvaluate);

          dataGridView1.Rows[0].Cells[_colEvaluate].Style.BackColor = item.eEvaluate == eEvaluate.Pass ? Color.Green : Color.Red;
          dataGridView1.Rows[0].Cells[_colEvaluate].Style.ForeColor = Color.White;
        }
      }
    }







    private void btnExport_Click(object sender, EventArgs e)
    {


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
            //CreateButtonWithDataProduction(textButton, dataDateGroupByProduct);
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








    private string nameFolder = "";



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






  }
}
