using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.UI.UcUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmChangeLine : Form
  {
    public delegate void SendConfirmChangeLine(object sender, int stationID);
    public event SendConfirmChangeLine OnSendConfirmChangeLine;
    public FrmChangeLine()
    {
      InitializeComponent();
    }

    private void FrmChangeLine_Load(object sender, EventArgs e)
    {
      AddLines();
    }

    #region Singleton parttern
    private static FrmChangeLine _Instance = null;
    public static FrmChangeLine Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new FrmChangeLine();
        }
        return _Instance;
      }
    }


    #endregion

    private void AddLines()
    {
      List<InforLine> stations = AppCore.Ins._listInforLine;

      if (stations.Count > 0 )
      {
        for (int i = 0; i < stations.Count; i++)
        {
          InforLine station = stations[i];
          UcLine line = new UcLine();
          line.BorderStyle = System.Windows.Forms.BorderStyle.None;
          line.Location = new System.Drawing.Point(12, 12);
          line.Name = $"{station.Name}";
          line.Size = new System.Drawing.Size(188, 191);
          line.TabIndex = 0;
          line.Tag = station.Id;
          line.StationName = line.Name;
          line.OnSendActiveClicked += Line_OnSendActiveClicked;
          line.SetActive(station.IsEnable == true);

          this.flowLayoutPanel1.Controls.Add(line);
        }
      }
    }


    private int stationID = 0;
    private void Line_OnSendActiveClicked(object sender, bool isConfirm = true)
    {
      if (sender is UcLine)
      {
        UcLine station = (UcLine)sender;
        stationID = (int)station.Tag;

        for (int i = 0; i < this.flowLayoutPanel1.Controls.Count; i++)
        {
          if (this.flowLayoutPanel1.Controls[i] is UcLine)
          {
            UcLine single_line = (UcLine)(this.flowLayoutPanel1.Controls[i]);
            single_line.SetActive(false);
          }
        }

        station.SetActive(true);
      }  
    }

    private void btnConfirm_Click(object sender, EventArgs e)
    {
      OnSendConfirmChangeLine?.Invoke(this, stationID);
      this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void FrmChangeLine_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        this.btnConfirm.PerformClick();
      }
      else if (e.KeyCode == Keys.Escape)
      {
        this.btnCancel.PerformClick();
      }
    }
  }
}
