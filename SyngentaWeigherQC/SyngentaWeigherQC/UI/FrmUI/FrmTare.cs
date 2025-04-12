using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Newtonsoft.Json.Linq;
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
using static SyngentaWeigherQC.Control.AppCore;
using static SyngentaWeigherQC.eNum.eUI;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmTare : Form
  {
    public delegate void SendCloseFrmTare();
    public event SendCloseFrmTare OnSendCloseFrmTare;

    public FrmTare()
    {
      InitializeComponent();
    }

    private Production _production = new Production();
    private double minTare = 0;
    private double maxTare = 0;
    private double average = 0;
    private double stdev = 0;
    private List<Tare> listTare = new List<Tare>();
    private DateTime dt_fileDB;
    public FrmTare(Production production, eModeTare eModeTare)
    {
      InitializeComponent();

      _production = production;
      StartFormModeTare(eModeTare);
      SetColorBtnModeTare(eModeTare);
      SetDataInforTare(_production, eModeTare);
    }

    private void FrmTare_Load(object sender, EventArgs e)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          FrmTare_Load(sender, e);
        }));
        return;
      }
      dt_fileDB = AppCore.Ins.GetDataTimeFileDB(DateTime.Now);
      this.dtpDateHistoricalTare.Value = dt_fileDB;

      this.btnTareNoLabel.Click += BtTareNoLabel_Click;
      this.btnTareWithLabel.Click += BtTareWithLabel_Click;

      this.productionDataInforNumberSample.SetTitle = "Số mẫu (Số vòi)";
      this.productionDataInforTarget.SetTitle = "Tare chuẩn (g)";
      this.productionDataInforUpper.SetTitle = "Tare cận trên (g)";
      this.productionDataInforLower.SetTitle = "Tare cận dưới (g)";
      this.lblProductionName.Text = _production?.Name ?? string.Empty;

      AppCore.Ins.OnSendDataRealTimeWeigherTare += Ins_OnSendDataWeigherTare;

      this.btLoadHistoricalTare.PerformClick();
    }

    private void BtTareWithLabel_Click(object sender, EventArgs e)
    {
      //FrmConfirm frmConfirm = new FrmConfirm("Vui lòng xác nhận thay đổi kiểu Tare sản phẩm", eModeTare.TareWithLabel);
      //frmConfirm.OnSendOKClicked += FrmConfirm_OnSendOKClicked;
      //frmConfirm.ShowDialog();
    }

    private void BtTareNoLabel_Click(object sender, EventArgs e)
    {
      //FrmConfirm frmConfirm = new FrmConfirm("Vui lòng xác nhận thay đổi kiểu Tare sản phẩm", eModeTare.TareNoLabel);
      //frmConfirm.OnSendOKClicked += FrmConfirm_OnSendOKClicked;
      //frmConfirm.ShowDialog();
    }

    private void StartFormModeTare(eModeTare eModeTare)
    {
      if (eModeTare==eModeTare.TareWithLabel) 
        this.btnTareWithLabel.PerformClick();
      else 
        this.btnTareNoLabel.PerformClick();
    }



    private void FrmConfirm_OnSendOKClicked(object sender, eModeTare eModeTare)
    {
      AppCore.Ins._modeTare = eModeTare;
      SetColorBtnModeTare(eModeTare);

      AppCore.Ins._inforValueSettingStation.ModeTare = (int)eModeTare;
      //await AppCore.Ins.UpdateInforShift(AppCore.Ins._inforValueSettingStation);

      SetDataInforTare(_production, eModeTare);
      ClearDataTare();
    }

    private void SetColorBtnModeTare(eModeTare eModeTare)
    {
      if (eModeTare == eModeTare.TareWithLabel)
      {
        this.btnTareWithLabel.BackColor = Color.FromArgb(255, 128, 0);
        this.btnTareNoLabel.BackColor = Color.Silver;
      }
      else if (eModeTare == eModeTare.TareNoLabel)
      {
        this.btnTareNoLabel.BackColor = Color.FromArgb(255, 128, 0);
        this.btnTareWithLabel.BackColor = Color.Silver;
      }
    }

    private void ClearDataTare()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          ClearDataTare();
        }));
        return;
      }
      average = 0;
      stdev = 0;

      listTare = new List<Tare>();
      this.dataGridView1.Rows.Clear();
      listValue.Clear();
      
      this.lblAvg.Text = average.ToString() + " g";
      this.lblStb.Text = stdev.ToString();
    }

    
    private void Ins_OnSendDataWeigherTareCalDone()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          Ins_OnSendDataWeigherTareCalDone();
        }));
        return;
      }
      listTare = AppCore.Ins._listTares;
      //SetDgv(listTare);

      average = (listTare != null && listTare.Count > 0) ? Math.Round(listTare[0].ValueAvg, 3) : 0;
      stdev = (listTare != null && listTare.Count > 0) ? listTare[0].Stdev : 0;

      this.lblAvg.Text = average.ToString() + " g";
      this.lblStb.Text = stdev.ToString();
    }


    
    private void CalValueAvgStdTare(List<Tare> listTares)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          CalValueAvgStdTare(listTares);
        }));
        return;
      }

      if (listTares.Count>0)
      {
        List<Double> listValueTare = listTares?.Select(x => x.Value).ToList();
        average = Math.Round(listValueTare.Average(), 3);
        stdev = AppCore.Ins.Stdev(listValueTare);
      }  
    }

    private List<Double> listValue = new List<double>();
    private void Ins_OnSendDataWeigherTare(double value)
    {
      SetDataWeigherRealTime(value);
      FilterDataWeigherTare(value);
    }

    private void SetDataWeigherRealTime(double value)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataWeigherRealTime(value);
        }));
        return;
      }

      try
      {
        this.lblPasFail.Visible = (value > 0.5);
        this.lblWeigherData.Text = value.ToString();
        if (value > minTare / 2 && value < maxTare * 2)
        {
          this.lblPasFail.Text = (value >= minTare && value<=maxTare ) ? "TARE ĐẠT" : "TARE KHÔNG ĐẠT";
          this.lblPasFail.BackColor = (value >= minTare && value <= maxTare) ? Color.Green : Color.Red;
        } 
        else
        {
          this.lblPasFail.Text = "GIÁ TRỊ NGOÀI PHẠM VI SO SÁNH";
          this.lblPasFail.BackColor = Color.Red;
        }  
        
      }
      catch (Exception ex)
      {
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
      }
    }


    private void SetDataInforTare(Production productions, eModeTare eMode)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataInforTare(productions, eMode);
        }));
        return;
      }

      if (productions != null)
      {
        if (eMode == eModeTare.TareNoLabel)
        {
          this.productionDataInforTarget.SetValue = productions.Tare_no_label_standard.ToString();
          this.productionDataInforUpper.SetValue = productions.Tare_no_label_upperlimit.ToString();
          this.productionDataInforLower.SetValue = productions.Tare_no_label_lowerlimit.ToString();
          minTare = productions.Tare_no_label_lowerlimit;
          maxTare = productions.Tare_no_label_upperlimit;
        }
        else if (eMode == eModeTare.TareWithLabel)
        {
          this.productionDataInforTarget.SetValue = productions.Tare_with_label_standard.ToString();
          this.productionDataInforUpper.SetValue = productions.Tare_with_label_upperlimit.ToString();
          this.productionDataInforLower.SetValue = productions.Tare_with_label_lowerlimit.ToString();
          minTare = productions.Tare_with_label_lowerlimit;
          maxTare = productions.Tare_with_label_upperlimit;
        }
      }
    }

    private void FrmTare_FormClosed(object sender, FormClosedEventArgs e)
    {
      OnSendCloseFrmTare?.Invoke();
    }

    private void btCancel_Click_1(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnCreateNew_Click(object sender, EventArgs e)
    {
      //FrmConfirmV2 frmConfirm = new FrmConfirmV2("Bạn muốn tạo giá trị Tare mới ?", eMsgType.Question);
      //frmConfirm.OnSendOKClicked += FrmConfirm_OnSendOKClicked2;
      //frmConfirm.ShowDialog();
    }

    private void FrmConfirm_OnSendOKClicked2(object sender)
    {
      listTare = new List<Tare>();
      ClearDataTare();
      AppCore.Ins._readyReceidDataTare = eReadyReceidDataTare.Yes;
    }

    private void btnComfirmTareDone_Click(object sender, EventArgs e)
    {
      if (average<=0)
      {
        new FrmNotification().ShowMessage("Giá trị Tare nhỏ hơn hoặc bằng 0. Vui lòng Tare lại !", eMsgType.Warning);
      }
      else
      {
        //FrmConfirmV2 frmConfirm = new FrmConfirmV2($"Xác nhận sử dụng giá trị trung bình Tare :\r\n {average} g .", eMsgType.Question);
        //frmConfirm.OnSendOKClicked += FrmConfirm_OnSendOKClicked1;
        //frmConfirm.ShowDialog();
      }  
      
      
    }

    private async void FrmConfirm_OnSendOKClicked1(object sender)
    {
      await AppCore.Ins.ConfirmTareDone(listTare);
      timer1.Start();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      timer1.Stop();
      this.Close();
    }

    private void SetDataUI(List<Tare> listTare, double averageValue, double stdevValue)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataUI(listTare, averageValue, stdevValue);
        }));
        return;
      }

      try
      {
        listTare.Reverse();
        if (listTare.Count>0)
        {
          dataGridView1.Rows.Clear();
          int stt = listTare.Count();
          foreach (var datalog in listTare)
          {
            int row = dataGridView1.Rows.Add();
            dataGridView1.Rows[row].Cells["Col1"].Value = stt--;
            dataGridView1.Rows[row].Cells["Col2"].Value = datalog.CreatedAt;
            dataGridView1.Rows[row].Cells["Col3"].Value = datalog.Value;

            if (minTare==maxTare)//Dạng Gói
            {
              //dataGridView1.Rows[row].Cells["Col3"].Style.BackColor = Color.White;
              //dataGridView1.Rows[row].Cells["Col3"].Style.ForeColor = Color.Black;
            }  
            else
            {
              if (datalog.Value > maxTare)
              {
                dataGridView1.Rows[row].Cells["Col3"].Style.BackColor = Color.DarkOrange;
                dataGridView1.Rows[row].Cells["Col3"].Style.ForeColor = Color.White;
              }
              else if (datalog.Value < minTare)
              {
                dataGridView1.Rows[row].Cells["Col3"].Style.BackColor = Color.Red;
                dataGridView1.Rows[row].Cells["Col3"].Style.ForeColor = Color.White;
              }
            }  
            
          }
        }

        this.lblAvg.Text = averageValue.ToString() + " g";
        this.lblStb.Text = stdevValue.ToString();
      }
      catch (Exception ex)
      {
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
      }
    }

    private double[] dataWeighers = new double[1000];
    private int cnt = 0;
    private void FilterDataWeigherTare(double current_weigher_value)
    {
      GetDataToList(current_weigher_value);

      ////if (current_weigher_value > AppCore.Ins._currentProduct.Tare_no_label_lowerlimit / 2 && current_weigher_value < AppCore.Ins._currentProduct.Tare_no_label_upperlimit * 2)
      ////{
      ////  dataWeighers[cnt] = current_weigher_value;
      ////  cnt++;
      ////  if (cnt >= 1000) cnt = 0;
      ////}
      //if (current_weigher_value >minTare / 2 && current_weigher_value < maxTare * 2)
      //{
      //  dataWeighers[cnt] = current_weigher_value;
      //  cnt++;
      //  if (cnt >= 1000) cnt = 0;
      //}
      //else if (current_weigher_value <= 0.1)
      //{
      //  // Sử dụng Dictionary để đếm tần suất xuất hiện của từng giá trị
      //  Dictionary<double, int> frequencyCounter = new Dictionary<double, int>();

      //  foreach (double value in dataWeighers)
      //  {
      //    if (value > 0.1)
      //    {
      //      if (frequencyCounter.ContainsKey(value))
      //      {
      //        frequencyCounter[value]++;
      //      }
      //      else
      //      {
      //        frequencyCounter.Add(value, 1);
      //      }
      //    }

      //  }

      //  double valueFilter = 0;
      //  double valueFre = 0;
      //  if (frequencyCounter.Count > 0)
      //  {
      //    valueFilter = frequencyCounter.Where(x => x.Key > 0.1).OrderByDescending(x => x.Value).First().Key;
      //    valueFre = frequencyCounter.Where(x => x.Key > 0.1).OrderByDescending(x => x.Value).First().Value;
      //  }

      //  if (valueFilter > 0.1 && valueFre>=2)
      //  {
      //    GetDataToList(valueFilter);
      //  }

      //  cnt = 0;
      //  dataWeighers = new double[1000];
      //}
    }


    private void GetDataToList(double value)
    {
      // List lại 
      listValue.Add(value);
      average = Math.Round(listValue.Average(), 3);
      stdev = (listValue.Count > 1) ? AppCore.Ins.Stdev(listValue) : 0;

      Tare tareSave = new Tare()
      {
        StationId = (AppCore.Ins._stationCurrent != null) ? AppCore.Ins._stationCurrent.Id : -1,
        GroupId = AppCore.Ins._groupIdCurrentTare +1,
        ProductId = (AppCore.Ins._currentProduct != null) ? AppCore.Ins._currentProduct.Id : -1,
        ShiftId = 1,
        Value = value,
        ValueUpper_nolabel = (_production != null) ? _production.Tare_no_label_upperlimit : 0,
        ValueStandard_nolabel = (_production != null) ? _production.Tare_no_label_standard : 0,
        ValueLower_nolabel = (_production != null) ? _production.Tare_no_label_lowerlimit : 0,
        ValueUpper_withlabel = (_production != null) ? _production.Tare_with_label_upperlimit : 0,
        ValueStandard_withlabel = (_production != null) ? _production.Tare_with_label_standard : 0,
        ValueLower_withlabel = (_production != null) ? _production.Tare_with_label_lowerlimit : 0,
        ValueAvg = average,
        Stdev = stdev,
        isWithLabel = (int)AppCore.Ins._modeTare,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now,
      };

      listTare.Add(tareSave);
      foreach (var item in listTare)
      {
        item.ValueAvg = average;
        item.Stdev = stdev;
        item.UpdatedAt = DateTime.Now;
      }

      SetDataUI(listTare, average, stdev);
    }

    private async void btLoadHistoricalTare_Click(object sender, EventArgs e)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          btLoadHistoricalTare_Click(sender, e);
        }));
        return;
      }

      dataGridView2.Rows.Clear();

      string pathDB = Application.StartupPath + $"\\Database\\{dtpDateHistoricalTare.Value.ToString("yyyyMMdd")}_data_log.sqlite";
      if (File.Exists(pathDB))
      {
        List<Tare> historicalTare = await AppCore.Ins.LoadAllTare(dtpDateHistoricalTare.Value);
        if (historicalTare.Count > 0)
        {
          Dictionary<int, List<Tare>> groupSamples = historicalTare
              .GroupBy(sample => sample.GroupId)
              .ToDictionary(group => group.Key, group => group.ToList());

          int stt = 1;
          foreach (var taregroup in groupSamples)
          {
            int row = dataGridView2.Rows.Add();
            dataGridView2.Rows[row].Cells["C1"].Value = stt++;
            dataGridView2.Rows[row].Cells["C2"].Value = taregroup.Value[0].CreatedAt;
            dataGridView2.Rows[row].Cells["C3"].Value = taregroup.Value[0].ValueAvg;
          }
        }
      }  
      
    }

    private void FrmTare_KeyUp(object sender, KeyEventArgs e)
    {
      //if (e.KeyCode == Keys.Enter)
      //{
      //  this.btConfirm.PerformClick();
      //}
      if (e.KeyCode == Keys.Escape)
      {
        this.btCancel.PerformClick();
      }
    }

    private void FrmTare_FormClosing(object sender, FormClosingEventArgs e)
    {
      AppCore.Ins._eWeigherMode = eWeigherMode.Normal;
    }


  }
}
