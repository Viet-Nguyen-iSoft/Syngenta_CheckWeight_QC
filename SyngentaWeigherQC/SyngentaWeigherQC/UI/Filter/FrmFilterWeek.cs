using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.UI.FrmUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.enumSoftware;
using static SyngentaWeigherQC.UI.FrmUI.FrmFilterMonth;

namespace SyngentaWeigherQC.UI.Filter
{
  public partial class FrmFilterWeek : Form
  {
    public delegate void SendWeekChoose(int week);
    public event SendWeekChoose OnSendWeekChoose;

    public FrmFilterWeek()
    {
      InitializeComponent();
    }

    private void FrmFilterWeek_Load(object sender, EventArgs e)
    {
      Dictionary<int, List<DateTime>>  weekInYears = DatetimeHelper.GetDaysInWeeks(DateTime.Now.Year);
      CreateAllCheckBox(weekInYears);
    }
    private void CreateAllCheckBox(Dictionary<int, List<DateTime>> listWeek)
    {
      for (int i = 0; i < listWeek.Count; i++)
      {
        var dataDates = listWeek.Values.ToList()[i];

        string dayStart = dataDates.FirstOrDefault().ToString("dd/MM/yyyy");
        string dayEnd = dataDates.LastOrDefault().ToString("dd/MM/yyyy");

        CheckBox checkbox = new CheckBox();
        checkbox.Text = $"Tuần {(i+1).ToString("00")}: Từ {dayStart} đến {dayEnd}";
        checkbox.ForeColor = Color.Black;
        checkbox.Font = new Font(Font.FontFamily, 16);
        checkbox.AutoSize = true;
        checkbox.Tag = i + 1;
        checkbox.CheckedChanged += AllCheckBox_CheckedChanged;
        flowLayoutPanel1.Controls.Add(checkbox);
      }
    }

    private int GetIso8601WeekOfYear(DateTime time)
    {
      int daysToAdd = DayOfWeek.Monday - time.DayOfWeek;
      if (daysToAdd > 0) daysToAdd -= 7;
      DateTime mondayOfYear = time.AddDays(daysToAdd);
      int week = (time.DayOfYear - mondayOfYear.DayOfYear) / 7 + 1;
      if (week < 1)
      {
        return GetIso8601WeekOfYear(mondayOfYear.AddDays(-7));
      }
      return week;
    }

    private int week = 0;
    private void AllCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      CheckBox changedCheckBox = sender as CheckBox;

      if (changedCheckBox.Checked)
      {
        foreach (var control in flowLayoutPanel1.Controls)
        {
          if (control is CheckBox cb && cb != changedCheckBox)
          {
            cb.Checked = false;
          }
        }

        week = (int)changedCheckBox.Tag;
      }
      else
      {
        week = 0;
      }
    }


    private void btnOK_Click(object sender, EventArgs e)
    {
      if (week > 0)
      {
        OnSendWeekChoose?.Invoke(week);
        this.Close();
      }
      else
      {
        new FrmNotification().ShowMessage("Vui lòng chọn tuần cần xem !", eMsgType.Warning);
      }
    }
  }
}
