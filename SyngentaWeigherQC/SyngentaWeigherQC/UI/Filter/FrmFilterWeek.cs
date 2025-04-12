using SyngentaWeigherQC.Control;
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

namespace SyngentaWeigherQC.UI.Filter
{
  public partial class FrmFilterWeek : Form
  {
    public delegate void SendWeekChoose(List<DateTime> listDate);
    public event SendWeekChoose OnSendWeekChoose;

    public FrmFilterWeek()
    {
      InitializeComponent();
    }

    private Dictionary<int, List<DateTime>> weekInYears = new Dictionary<int, List<DateTime>>();
    private void FrmFilterWeek_Load(object sender, EventArgs e)
    {
      weekInYears = AppCore.Ins.GetDaysInWeeks(DateTime.Now.Year);
      CreateAllCheckBox(weekInYears);
    }

    public CheckBox checkbox { get; private set; }
    private void CreateAllCheckBox(Dictionary<int, List<DateTime>> listWeek)
    {
      for (int i = 0; i < listWeek.Count; i++)
      {
        var dataDates = listWeek.Values.ToList()[i];

        string dayStart = dataDates.FirstOrDefault().ToString("dd/MM/yyyy");
        string dayEnd = dataDates.LastOrDefault().ToString("dd/MM/yyyy");

        checkbox = new CheckBox();
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

    private List<int> week = new List<int>();
    private void AllCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      week = new List<int>();
      int cnt = 1;
      foreach (System.Windows.Forms.Control control in flowLayoutPanel1.Controls)
      {
        if (control is CheckBox checkBox)
        {
          if (checkBox.Checked)
          {
            week.Add(cnt);
          }
          cnt++;
        }
      }
    }


    

    private void btnOK_Click(object sender, EventArgs e)
    {
      List<DateTime> listDateTime = new List<DateTime>();
      foreach (var item in week)
      {
        listDateTime.AddRange(weekInYears[item]);
      }
      OnSendWeekChoose?.Invoke(listDateTime);
      this.Close();
    }
  }
}
