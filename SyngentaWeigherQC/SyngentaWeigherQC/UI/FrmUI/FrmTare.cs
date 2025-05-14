using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.enumSoftware;
using Production = SyngentaWeigherQC.Models.Production;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmTare : Form
  {
    public delegate void SendCloseFrmTare();
    public event SendCloseFrmTare OnSendCloseFrmTare;

    public delegate void SendChangeTypeTare();
    public event SendChangeTypeTare OnSendChangeTypeTare;

    public delegate void SendConfirmValueTare(DatalogTare datalogTare);
    public event SendConfirmValueTare OnSendConfirmValueTare;

    private InforLine _inforLine { get; set; }
    private Production _production { get; set; }

    private double minTareValue = 0;
    private double maxTareValue = 0;
    private double averageValue = 0;
    private double stdevValue = 0;

    private Dictionary<DateTime, double> listTare = new Dictionary<DateTime, double>();

    public FrmTare()
    {
      InitializeComponent();
    }


    public FrmTare(InforLine inforLine) : this()
    {
      this._inforLine = inforLine;
      this._production = inforLine.Productions.FirstOrDefault(x => x.IsEnable == true && x.IsDelete == false);
    }


    private void FrmTare_Load(object sender, EventArgs e)
    {
      this.productionDataInforPacksize.SetTitle = "Packsize";
      this.productionDataInforTarget.SetTitle = "Tare chuẩn (g)";
      this.productionDataInforUpper.SetTitle = "Tare cận trên (g)";
      this.productionDataInforLower.SetTitle = "Tare cận dưới (g)";

      SetInforProduct();

      //Loại Tare
      SetColorBtnModeTare(_inforLine.eModeTare);
      this.btnTareNoLabel.Click += BtTareNoLabel_Click;
      this.btnTareWithLabel.Click += BtTareWithLabel_Click;

      AppCore.Ins.OnSendValueTare += Ins_OnSendValueTare;
    }

    private void Ins_OnSendValueTare(double value)
    {
      SetDataTareRealTime(value);
    }

    private void FrmTare_FormClosed(object sender, FormClosedEventArgs e)
    {
      OnSendCloseFrmTare?.Invoke();
    }
    private void FrmTare_FormClosing(object sender, FormClosingEventArgs e)
    {
      AppCore.Ins.OnSendValueTare -= Ins_OnSendValueTare;
    }

    private void SetInforProduct()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetInforProduct();
        }));
        return;
      }

      this.lblProductionName.Text = _production?.Name ?? string.Empty;
      this.productionDataInforPacksize.SetValue = _production?.PackSize.ToString() ?? string.Empty;

      if (this._inforLine.eModeTare == eModeTare.TareNoLabel)
      {
        this.productionDataInforTarget.SetValue = _production.Tare_no_label_standard.ToString();
        this.productionDataInforUpper.SetValue = _production.Tare_no_label_upperlimit.ToString();
        this.productionDataInforLower.SetValue = _production.Tare_no_label_lowerlimit.ToString();

        this.minTareValue = _production.Tare_no_label_lowerlimit;
        this.maxTareValue = _production.Tare_no_label_upperlimit;
      }
      else
      {
        this.productionDataInforTarget.SetValue = _production.Tare_with_label_standard.ToString();
        this.productionDataInforUpper.SetValue = _production.Tare_with_label_upperlimit.ToString();
        this.productionDataInforLower.SetValue = _production.Tare_with_label_lowerlimit.ToString();

        this.minTareValue = _production.Tare_with_label_lowerlimit;
        this.maxTareValue = _production.Tare_with_label_upperlimit;
      }
    }

    private void BtTareWithLabel_Click(object sender, EventArgs e)
    {
      if (_inforLine.eModeTare == eModeTare.TareNoLabel)
      {
        FrmConfirm frmConfirm = new FrmConfirm("Vui lòng xác nhận thay đổi \r\nTare có nhãn ?", eMsgType.Question);
        frmConfirm.OnSendOKClicked += FrmConfirm_OnSendOKClicked;
        frmConfirm.ShowDialog();
      }
    }

    private void BtTareNoLabel_Click(object sender, EventArgs e)
    {
      if (_inforLine.eModeTare == eModeTare.TareWithLabel)
      {
        FrmConfirm frmConfirm = new FrmConfirm("Vui lòng xác nhận thay đổi \r\nTare không nhãn ?", eMsgType.Question);
        frmConfirm.OnSendOKClicked += FrmConfirm_OnSendOKClicked;
        frmConfirm.ShowDialog();
      }
    }

    private void FrmConfirm_OnSendOKClicked()
    {
      _inforLine.eModeTare = _inforLine.eModeTare == eModeTare.TareWithLabel ?
                                                     eModeTare.TareNoLabel :
                                                     eModeTare.TareWithLabel;
      SetColorBtnModeTare(_inforLine.eModeTare);

      OnSendChangeTypeTare?.Invoke();
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

    private void SetDataTareRealTime(double value)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataTareRealTime(value);
        }));
        return;
      }

      try
      {
        this.lblWeigherData.Text = value.ToString();
        if (value > minTareValue / 2 && value < maxTareValue * 2)
        {
          this.lblPasFail.Text = (value >= minTareValue && value <= maxTareValue) ? "TARE ĐẠT" : "TARE KHÔNG ĐẠT";
          this.lblPasFail.BackColor = (value >= minTareValue && value <= maxTareValue) ? Color.Green : Color.Red;
        }
        else
        {
          this.lblPasFail.Text = "GIÁ TRỊ NGOÀI PHẠM VI SO SÁNH";
          this.lblPasFail.BackColor = Color.Red;
        }

        listTare.Add(DateTime.Now, value);

        stdevValue = (listTare.Count > 1) ? MathHelper.Stdev(listTare.Select(x=>x.Value).ToList()) : 0;
        averageValue = (listTare.Count > 0) ? Math.Round(listTare.Average(x => x.Value), 3) : 0;
        SetDataUI(listTare, averageValue, stdevValue);
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
      }
    }


    private void SetDataUI(Dictionary<DateTime, double> dataTare, double avg, double stdev)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataUI(dataTare, avg, stdev);
        }));
        return;
      }

      try
      {
        dataGridView1.Rows.Clear();
        if (dataTare.Count > 0)
        {
          dataTare.Reverse();
          int stt = 1;

          foreach (var item in dataTare)
          {
            int row = dataGridView1.Rows.Add();
            dataGridView1.Rows[row].Cells["Col1"].Value = stt++;
            dataGridView1.Rows[row].Cells["Col2"].Value = item.Key;
            dataGridView1.Rows[row].Cells["Col3"].Value = item.Value;

            if (minTareValue == maxTareValue)//Dạng Gói
            {
              dataGridView1.Rows[row].Cells["Col3"].Style.BackColor = Color.White;
              dataGridView1.Rows[row].Cells["Col3"].Style.ForeColor = Color.Black;
            }
            else
            {
              if (item.Value > maxTareValue)
              {
                dataGridView1.Rows[row].Cells["Col3"].Style.BackColor = Color.DarkOrange;
                dataGridView1.Rows[row].Cells["Col3"].Style.ForeColor = Color.White;
              }
              else if (item.Value < minTareValue)
              {
                dataGridView1.Rows[row].Cells["Col3"].Style.BackColor = Color.Red;
                dataGridView1.Rows[row].Cells["Col3"].Style.ForeColor = Color.White;
              }
            }
          }
        }

        this.lblAvg.Text = avg.ToString() + " g";
        this.lblStb.Text = stdev.ToString();
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
      }
    }

    private void btnComfirmTareDone_Click(object sender, EventArgs e)
    {
      if (averageValue <= 0)
      {
        new FrmNotification().ShowMessage("Giá trị Tare nhỏ hơn hoặc bằng 0. Vui lòng Tare lại !", eMsgType.Warning);
      }
      else
      {
        FrmConfirm frmConfirm = new FrmConfirm($"Xác nhận lấy giá trị tare = {averageValue} g ?", eMsgType.Question);
        frmConfirm.OnSendOKClicked += FrmConfirm_OnSendOKClicked1;
        frmConfirm.ShowDialog();
      }
    }

    private async void FrmConfirm_OnSendOKClicked1()
    {
      try
      {
        DatalogTare datalogTare = new DatalogTare();
        datalogTare.Value = averageValue;
        datalogTare.eModeTare = _inforLine.eModeTare;
        datalogTare.InforLineId = _inforLine.Id;
        datalogTare.ProductionId = _production.Id;

        var rs = await AppCore.Ins.Add(datalogTare);

        //Cập nhật Line
        this._inforLine.RequestTare=false;
        this._inforLine.LastTareId= rs.Id;
        await AppCore.Ins.Update(this._inforLine);

        OnSendConfirmValueTare?.Invoke(rs);
        //Close
        this.Close();
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
        new FrmNotification().ShowMessage("Lỗi xác nhận !", eMsgType.Error);
      }
    }

    private void btnCreateNew_Click(object sender, EventArgs e)
    {
      averageValue = 0;
      stdevValue = 0;
      listTare = new Dictionary<DateTime, double>();
      SetDataUI(listTare, stdevValue, averageValue);
    }

    private void btCancel_Click_1(object sender, EventArgs e)
    {
      this.Close();
    }







    private void btLoadHistoricalTare_Click(object sender, EventArgs e)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          btLoadHistoricalTare_Click(sender, e);
        }));
        return;
      }

     
    }

    private void FrmTare_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        this.btnComfirmTareDone.PerformClick();
      }
      if (e.KeyCode == Keys.Escape)
      {
        this.btCancel.PerformClick();
      }
    }




  }
}
