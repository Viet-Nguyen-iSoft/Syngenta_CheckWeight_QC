using SyngentaWeigherQC.Control;
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

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmFilterMonth : Form
  {
    public delegate void SendMonthChoose(List<DateTime> listDate);
    public event SendMonthChoose OnSendMonthChoose;

    public FrmFilterMonth()
    {
      InitializeComponent();
    }


    private Dictionary<int, List<DateTime>> listMonths = new Dictionary<int, List<DateTime>>();
    private void FrmYear_Load(object sender, EventArgs e)
    {
      listMonths = GetDaysInMonths(DateTime.Now.Year);
      CreateAllCheckBox(listMonths);
    }

    public CheckBox checkBox { get; private set; }
    private void CreateAllCheckBox(Dictionary<int, List<DateTime>> listMonths)
    {
      for(int i = 0;i < listMonths.Count; i++)
      {
        checkBox = new CheckBox();
        checkBox.Text = $"Tháng: {(i+1).ToString("00")}";
        checkBox.ForeColor = Color.Black;
        checkBox.Font = new Font(Font.FontFamily, 16);
        checkBox.AutoSize = true;
        checkBox.Tag = i + 1;
        checkBox.CheckedChanged += AllCheckBox_CheckedChanged;
        flowLayoutPanel1.Controls.Add(checkBox);
      }
    }

    private List<int> months = new List<int>();
    private void AllCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      months = new List<int>();
      int cnt = 1;
      foreach (System.Windows.Forms.Control control in flowLayoutPanel1.Controls)
      {
        if (control is CheckBox checkBox)
        {
          if (checkBox.Checked)
          {
            months.Add(cnt);
          }
          cnt++;
        }
      }
    }


    private Dictionary<int, List<DateTime>> GetDaysInMonths(int year)
    {
      Dictionary<int, List<DateTime>> daysInMonths = new Dictionary<int, List<DateTime>>();

      for (int month = 1; month <= 12; month++)
      {
        int daysInMonth = DateTime.DaysInMonth(year, month);
        List<DateTime> days = new List<DateTime>();
        for (int day = 1; day <= daysInMonth; day++)
        {
          DateTime date = new DateTime(year, month, day);
          days.Add(date);
        }
        daysInMonths.Add(month, days);
      }
      return daysInMonths;
    }


    private void btnOK_Click(object sender, EventArgs e)
    {
      List<DateTime> days = new List<DateTime>();
      foreach (var item in months)
      {
        days.AddRange(AppCore.Ins.GetAllDaysInMonth(DateTime.Now.Year, item));
      }
      OnSendMonthChoose?.Invoke(days);
      this.Close();
    }


  }
}
