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
  public partial class FrmFilterDate : Form
  {
    public delegate void SendDateChoose(List<DateTime> listDate);
    public event SendDateChoose OnSendDateChoose;

    private int _month = 0;
    public FrmFilterDate(int month)
    {
      InitializeComponent();
      _month = month;
    }

    private List<DateTime> listDates = new List<DateTime>();
    private void FrmFilterDate_Load(object sender, EventArgs e)
    {
      listDates = GetDaysInMonth(DateTime.Now.Year, _month);
      CreateAllCheckBox(listDates);
    }

    public CheckBox checkbox { get; private set; }
    private void CreateAllCheckBox(List<DateTime> listMonths)
    {
      for (int i = 0; i < listMonths.Count; i++)
      {
        checkbox = new CheckBox();
        checkbox.Text = $"Ngày: {listMonths[i].ToString("dd/MM/yyyy")}";
        checkbox.ForeColor = Color.Black;
        checkbox.Font = new Font(Font.FontFamily, 16);
        checkbox.AutoSize = true;
        checkbox.CheckedChanged += AllCheckBox_CheckedChanged;
        flowLayoutPanel1.Controls.Add(checkbox);
      }

      //foreach (System.Windows.Forms.Control control in flowLayoutPanel1.Controls)
      //{
      //  if (control is CheckBox checkBox)
      //  {
      //    checkBox.Checked = true;
      //  }
      //}

    }

    private List<DateTime> listDateTicks = new List<DateTime>();
    private void AllCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      listDateTicks = new List<DateTime>();
      int cnt = 0;
      foreach (System.Windows.Forms.Control control in flowLayoutPanel1.Controls)
      {
        if (control is CheckBox checkBox)
        {
          if (checkBox.Checked)
          {
            listDateTicks.Add(listDates[cnt]);
          }
          cnt++;
        }
      }
    }


    private List<DateTime> GetDaysInMonth(int year, int month)
    {
      List<DateTime> daysInMonth = new List<DateTime>();

      // Kiểm tra xem tháng và năm có hợp lệ không
      if (month < 1 || month > 12 || year < 1)
      {
        throw new ArgumentException("Tháng hoặc năm không hợp lệ.");
      }

      // Tính số ngày trong tháng
      int daysInThisMonth = DateTime.DaysInMonth(year, month);

      // Tạo danh sách các ngày trong tháng
      for (int day = 1; day <= daysInThisMonth; day++)
      {
        daysInMonth.Add(new DateTime(year, month, day));
      }

      return daysInMonth;
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      OnSendDateChoose?.Invoke(listDateTicks);
      this.Close();
    }
  }
}
