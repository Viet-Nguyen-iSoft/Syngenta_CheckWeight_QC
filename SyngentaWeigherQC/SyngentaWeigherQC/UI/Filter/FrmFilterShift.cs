using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyngentaWeigherQC.UI.Filter
{
  public partial class FrmFilterShift : Form
  {
    public delegate void SendShiftChoose(List<string> listDate);
    public event SendShiftChoose OnSendShiftChoose;

    public FrmFilterShift()
    {
      InitializeComponent();
    }

    private List<string> _listShift = new List<string>(); 
    private List<string> listShiftChoose = new List<string>();
    public FrmFilterShift(List<string> listShift)
    {
      InitializeComponent();
      _listShift = listShift;
    }

    private void FrmFilterShift_Load(object sender, EventArgs e)
    {
      CreateAllCheckBox(_listShift);
    }



    public CheckBox checkbox { get; private set; }
    private void CreateAllCheckBox(List<string> listShift)
    {
      for (int i = 0; i < listShift.Count; i++)
      {
        checkbox = new CheckBox();
        checkbox.Text = $"{listShift[i]}";
        checkbox.ForeColor = Color.Black;
        checkbox.Font = new Font(Font.FontFamily, 16);
        checkbox.AutoSize = true;
        checkbox.Tag = listShift[i];
        checkbox.CheckedChanged += AllCheckBox_CheckedChanged;
        flowLayoutPanel1.Controls.Add(checkbox);
      }

      foreach (System.Windows.Forms.Control control in flowLayoutPanel1.Controls)
      {
        if (control is CheckBox checkBox)
        {
          checkBox.Checked = true;
        }
      }

    }

    //List<int> listDate = new List<DateTime>();
    private void AllCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      listShiftChoose = new List<string>();
      int cnt = 1;
      foreach (System.Windows.Forms.Control control in flowLayoutPanel1.Controls)
      {
        if (control is CheckBox checkBox)
        {
          if (!checkBox.Checked)
          {
            listShiftChoose.Add((string)checkBox.Tag);
          }
          cnt++;
        }
      }
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      OnSendShiftChoose?.Invoke(listShiftChoose);
      this.Close();
    }
  }
}
