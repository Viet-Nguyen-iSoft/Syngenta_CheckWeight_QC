using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyngentaWeigherQC.UI.UcUI
{
  public partial class UcLine : UserControl
  {
    public delegate void SendActiveClicked(object sender, bool isConfirm=true);
    public event SendActiveClicked OnSendActiveClicked;
    public UcLine()
    {
      InitializeComponent();
    }

    public string StationName
    {
      get { return lbStationName.Text; }
      set {this.lbStationName.Text = value; }
    }

    private void lbStationName_Click(object sender, EventArgs e)
    {
      OnSendActiveClicked?.Invoke(this);
    }

    private void picStation_Click(object sender, EventArgs e)
    {
      OnSendActiveClicked?.Invoke(this);
    }

    public void SetActive(bool value)
    {
      this.picStation.Image = (value) ? Properties.Resources.WeigherActive : Properties.Resources.Weigher;
      this.lbStationName.BackColor = (value) ? Color.Green : Color.Silver;
      this.lbStationName.ForeColor = (value) ? Color.White : Color.Black;
    }
  }
}
