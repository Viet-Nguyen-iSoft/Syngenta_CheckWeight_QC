using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.enumSoftware;

namespace SynCheckWeigherLoggerApp.DashboardViews
{
  public partial class ProductionDataInfor : UserControl
  {
    //private eDataFormat _eDataFormat = eDataFormat.Packsize;


    public ProductionDataInfor()
    {
      InitializeComponent();
    }

    public string SetTitle
    {
      set
      {
        this.label1.Text = value;
      }
    }

    public string SetValue
    {
      set
      {
        this.label2.Text = value;
      }
    }



    public void SetData(eDataFormat eDataFormat)
    {
      if (eDataFormat == eDataFormat.Packsize)
      {
        this.label1.Text = "Packsize";
      }
      else if (eDataFormat == eDataFormat.Standard)
      {
        this.label1.Text = "Chuẩn";
      }
      else if (eDataFormat == eDataFormat.Upperlimit)
      {
        this.label1.Text = "Cận trên";
      }
      else if (eDataFormat == eDataFormat.Lowerlmit)
      {
        this.label1.Text = "Cận dưới";
      }
      else if (eDataFormat == eDataFormat.TareTotalSamples)
      {
        this.label1.Text = "Số mẫu (số vòi)";
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 40.00F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      }
      else if (eDataFormat == eDataFormat.TareStandard)
      {
        this.label1.Text = "Tare chuẩn (g)";
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 40.00F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      }
      else if (eDataFormat == eDataFormat.TareUpperlimit)
      {
        this.label1.Text = "Tare cận trên (g)";
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 40.00F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      }
      else if (eDataFormat == eDataFormat.TareLowerlmit)
      {
        this.label1.Text = "Tare cận dưới (g)";
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 40.00F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      }
    }

    public void SetText(string text)
    {
      this.label2.Text = text;
    }
  }
}
