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


    private List<DataReportExcel> dataReportExcels = new List<DataReportExcel>();
    private async void btnPreview_Click(object sender, EventArgs e)
    {
      if (AppCore.Ins.CheckRole(ePermit.Role_Excel) || AppCore.Ins._roleCurrent.Name == "iSOFT")
      {
        var lineChoose = cbbLine.SelectedItem as InforLine;
        int lineId = (lineChoose != null) ? lineChoose.Id : 0;
        var datalogs = await AppCore.Ins.LoadAllDatalogWeight(lineId, From, To, ShiftId);

        dataReportExcels = AppCore.Ins.GenerateDataReport(datalogs);

        ShowResult(dataReportExcels);
      }
      else
      {
        new FrmNotification().ShowMessage("Tài khoản không có quyền !", eMsgType.Warning);
      }
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

      if (dataReportExcels.Count()>0)
      {
        //Đi từng ngày
        foreach (var dataReportExcel in dataReportExcels)
        {
          //Đi từng Loại Ca
          foreach (var data_by_shift in dataReportExcel.DataByDates)
          {
            //Đi từng sp
            foreach (var data_by_product in data_by_shift.DataByProducts)
            {
              string textButton = dataReportExcel.DateTime.ToString("yyyy/MM/dd") + " - " +
                                  data_by_shift.ShiftType.Name + " - " +
                                  data_by_product.Production.Name;

              CreateButtonWithDataProduction(textButton, data_by_product);
            }
          }
        }
      }
      else
      {
        //Bảng thông tin SP
        ucTemplateExcel1.UpdateInforProductUI(null);

        //Thống kê 3 ca
        LoadSumaryByShift(null, null);

        //Table
        this.dataGridView1.Rows.Clear();

        //Histogram
        this.ucChartHistogram1.SetDataChart(null, null);

        //ChartLine
        this.ucChartLine1.SetDataChart(null);
      }


      if (flowLayoutPanelProduct.Controls.Count > 0 && flowLayoutPanelProduct.Controls[0] is Button)
      {
        Button firstButton = (Button)flowLayoutPanelProduct.Controls[0];
        firstButton.PerformClick();
      }
    }

    private void CreateButtonWithDataProduction(string text, DataByProduction datalogWeights)
    {
      Button button = new Button();
      button.BackColor = Color.FromArgb(72, 61, 139);
      button.FlatAppearance.BorderColor = Color.FromArgb(102, 102, 153);
      button.FlatAppearance.BorderSize = 3;
      button.FlatStyle = FlatStyle.Flat;
      button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
      button.ForeColor = Color.White;
      button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      button.Location = new System.Drawing.Point(3, 3);
      button.Name = text;
      button.Size = new System.Drawing.Size(400, 40);
      button.TabIndex = 16;
      button.Text = text;
      button.TextImageRelation = TextImageRelation.ImageBeforeText;
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
        var datalogWeights = (DataByProduction)button.Tag;

        var production = datalogWeights.Production;
        var listWeightByShift = datalogWeights.DataByProductionByShifts;

        DatalogWeight datalogWeight = listWeightByShift.FirstOrDefault().DatalogWeights.FirstOrDefault();
        //Thông tin report
        ExcelClassInforProduct excelClassInforProduct = new ExcelClassInforProduct();
        excelClassInforProduct.NameLine = production.InforLine.Name;
        excelClassInforProduct.ModeTare = eNumHelper.GetDescription(datalogWeight.DatalogTare.eModeTare);
        excelClassInforProduct.ProductName = production.Name;
        excelClassInforProduct.PackSize = production.PackSize;
        excelClassInforProduct.Standard = production.StandardFinal;
        excelClassInforProduct.Upper = production.UpperLimitFinal;
        excelClassInforProduct.Lower = production.LowerLimitFinal;
        excelClassInforProduct.NameShiftLeader = datalogWeight.ShiftLeader.UserName;
        ucTemplateExcel1.UpdateInforProductUI(excelClassInforProduct);


        ////Table
        this.dataGridView1.Rows.Clear();
        foreach ( var item in listWeightByShift)
        {
          var data_table = AppCore.Ins.ConvertToDTOList(item.DatalogWeights);
          SetDataTable(data_table, production);
        }

        ///Gộp data Weight
        List<DatalogWeight> allDatalogWeights = listWeightByShift.SelectMany(d => d.DatalogWeights).ToList();


        ////Histogram
        ucChartHistogram1.SetDataChart(allDatalogWeights, production);

        ////ChartLine
        var dataChartLine = AppCore.Ins.CvtDatalogWeightToChartLine(allDatalogWeights, production);
        ucChartLine1.SetDataChart(dataChartLine);

        ////Thống kê 3 ca
        LoadSumaryByShift(listWeightByShift, production);
      }
    }

    private void LoadSumaryByShift(List<DataByProductionByShift> dataByProductionByShifts, Production production)
    {
      ucTemplateExcel1.ClearSumary();
      eEvaluate eEvaluate = eEvaluate.None;

      if (dataByProductionByShifts?.Count>0)
      {
        eEvaluate = eEvaluate.Pass;

        foreach (var item in dataByProductionByShifts)
        {
          var rs = AppCore.Ins.SumaryDTO(item.DatalogWeights);
          foreach (var item2 in rs)
          {
            ucTemplateExcel1.SetValueSumary(item2);

            if (item2.eEvaluate == eEvaluate.Fail)
            {
              eEvaluate = eEvaluate.Fail;
            }
          }
        }
      }  

      //Kết quả
      this.ucTemplateExcel1.SetResultFinal(eEvaluate);
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

    private string nameFolder =  string.Empty;
    private void btnExport_Click(object sender, EventArgs e)
    {
      try
      {
        if (!(AppCore.Ins.CheckRole(ePermit.Role_Excel) || AppCore.Ins._roleCurrent.Name == "iSOFT"))
        {
          new FrmNotification().ShowMessage("Tài khoản không có quyền !", eMsgType.Warning);
          return;
        }

        if (dataReportExcels != null)
        {
          if (dataReportExcels.Count > 0)
          {
            //Chọn folder để lưu
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
              folderDialog.Description = "Chọn thư mục lưu";
              DialogResult result = folderDialog.ShowDialog();
              if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
              {
                nameFolder = folderDialog.SelectedPath;
                string[] path = new string[1];
                path[0] = nameFolder;
                foreach (var item in dataReportExcels)
                {
                  ExcelHelper.ReportExcel(item, path);
                }

                FrmConfirm frmConfirm = new FrmConfirm("Xuất report thành công !\n Bạn có muốn mở file bây giờ ?", eMsgType.Question);
                frmConfirm.OnSendOKClicked += FrmConfirm_OnSendOKClicked;
                frmConfirm.ShowDialog();
              }
            }
          }
          else
          {
            new FrmNotification().ShowMessage("Không có dữ liệu xuất Report !", eMsgType.Info);
          }
        }
        else
        {
          new FrmNotification().ShowMessage("Không có dữ liệu xuất Report !", eMsgType.Info);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message + "---" + ex.StackTrace);
      }

    }

    private void FrmConfirm_OnSendOKClicked()
    {
      Process.Start("explorer.exe", nameFolder);
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




  }
}
