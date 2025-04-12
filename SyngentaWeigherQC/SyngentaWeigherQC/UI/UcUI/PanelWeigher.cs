using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using Irony.Parsing;
using SyngentaWeigherQC.Models;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace SyngentaWeigherQC.UI.UcUI
{
  public partial class PanelWeigher : UserControl
  {
    public PanelWeigher()
    {
      InitializeComponent();

      this.ucProductionDataInforPacksize.SetTitle = "PackSize";
      this.ucProductionDataInforTarget.SetTitle = "Chuẩn";
      this.ucProductionDataInforMax.SetTitle = "Cận trên";
      this.ucProductionDataInforMin.SetTitle = "Cận dưới";
    }

    public void SetDataInforProduct(double PackSize, double Standard, double Upper, double Lower)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataInforProduct(PackSize, Standard, Upper, Lower);
        }));
        return;
      }

      this.ucProductionDataInforPacksize.SetValue = PackSize.ToString();
      this.ucProductionDataInforTarget.SetValue = Standard.ToString();
      this.ucProductionDataInforMax.SetValue = Upper.ToString();
      this.ucProductionDataInforMin.SetValue = Lower.ToString();
    }

    public void SetValueWeigherRealTime(double value, eValuate eValuate)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetValueWeigherRealTime(value, eValuate);
        }));
        return;
      }

      lblPasFail.Visible = ((value >= 10));
      this.lblWeigherData.Text = value.ToString();

      switch (eValuate)
      {
        case eValuate.UNKNOWN:
          this.lblPasFail.Text = "CHƯA CHỌN SẢN PHẨM";
          this.lblPasFail.BackColor = Color.Orange;
          this.lblPasFail.ForeColor = Color.White;
          break;
        case eValuate.FAIL:
          this.lblPasFail.Text = "TRỌNG LƯỢNG TRUNG BÌNH KHÔNG ĐẠT";
          this.lblPasFail.BackColor = Color.Red;
          this.lblPasFail.ForeColor = Color.White;
          break;
        case eValuate.PASS:
          this.lblPasFail.Text = "TRỌNG LƯỢNG TRUNG BÌNH ĐẠT";
          this.lblPasFail.BackColor = Color.Green;
          this.lblPasFail.ForeColor = Color.White;
          break;
        case eValuate.OVER:
          this.lblPasFail.Text = "TRỌNG LƯỢNG TRUNG BÌNH CAO";
          this.lblPasFail.BackColor = Color.Yellow;
          this.lblPasFail.ForeColor = Color.Black;
          break;
      }
    }

    public void SetValueTare(double avg, double standard, double upper, double lower, int isLabel, DateTime dateTime)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetValueTare(avg, standard, upper, lower, isLabel, dateTime);
        }));
        return;
      }

      this.lblTareAvg.Text = avg.ToString() ;
      this.lblTareUpperLimit.Text = $" : {upper}";
      this.lblTareLowerLimit.Text = $" : {lower}";
      this.lblTareStandard.Text = $" : {standard}";
      this.btTareWithLabel.Text = (isLabel == 1) ? "Tare chai có nhãn" : "Tare chai không nhãn";
      this.lblTareLastUpdated.Text = $"Last updated: {dateTime.ToString("yyyy/MM/dd HH:mm:ss")}";
    }


    public void SetSatutusConnectSerialWeigher(bool isConnect)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetSatutusConnectSerialWeigher(isConnect);
        }));
        return;
      }

      this.lblWeigherStatus.Text = (isConnect) ? "KẾT NỐI CÂN" : "MẤT KẾT NỐI CÂN";
      this.lblWeigherStatus.BackColor = (isConnect) ? Color.Green : Color.Red;
    }


  }
}
