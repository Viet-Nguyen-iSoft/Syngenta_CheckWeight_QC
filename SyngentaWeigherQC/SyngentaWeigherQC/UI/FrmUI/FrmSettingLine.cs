using SyngentaWeigherQC.Control;
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
using static SyngentaWeigherQC.eNum.enumSoftware;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmSettingLine : Form
  {
    public delegate void SendChangeLine();
    public event SendChangeLine OnSendChangeLine;
    public FrmSettingLine()
    {
      InitializeComponent();
    }

    #region Singleton parttern
    private static FrmSettingLine _Instance = null;
    public static FrmSettingLine Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new FrmSettingLine();
        }
        return _Instance;
      }
    }
    #endregion

    private void FrmSettingLine_Load(object sender, EventArgs e)
    {
      var data = AppCore.Ins._listInforLine?.Where(x => x.IsDelete == false).OrderBy(x => x.Id).ToList();
      var data_total = $"{data?.Count(x=>x.IsEnable==true)} / {data?.Count()}";
      LoadListStation(data, data_total);

      dataGridView1.CellClick += DataGridView1_CellClick;
    }

    private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
        return;

      if (dataGridView1.Columns[e.ColumnIndex].Name == "col6")
      {
        if (!AppCore.Ins.CheckRole(ePermit.SettingInformationLine))
        {
          new FrmNotification().ShowMessage("Tài khoản không có quyền !", eMsgType.Warning);
          return;
        }

        var data_line = dataGridView1.Rows[e.RowIndex].Tag as InforLine;
        FrmChangeSettingLine frm = new FrmChangeSettingLine(data_line);
        frm.OnSendSaveChange += Frm_OnSendSaveChange;
        frm.ShowDialog();
      }
    }

    private async void Frm_OnSendSaveChange()
    {
      //Load DB
      await AppCore.Ins.ReloadInforLine();

      //Show UI
      var data = AppCore.Ins._listInforLine?.Where(x => x.IsDelete == false).OrderBy(x=>x.Id).ToList();
      var data_total = $"{data?.Count(x => x.IsEnable == true)} / {data?.Count()}";
      LoadListStation(data, data_total);

      //Event
      OnSendChangeLine?.Invoke();
    }

    private void LoadListStation(List<InforLine> inforLines, string total)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LoadListStation(inforLines, total);
        }));
        return;
      }


      dataGridView1.Rows.Clear();

      if (inforLines.Count() > 0)
      {
        for (int i = 0; i < inforLines.Count(); i++)
        {
          int row = dataGridView1.Rows.Add();
          dataGridView1.Rows[row].Cells["col1"].Value = i + 1;
          dataGridView1.Rows[row].Cells["col2"].Value = inforLines[i].Name;
          dataGridView1.Rows[row].Cells["col7"].Value = inforLines[i].Code;
          dataGridView1.Rows[row].Cells["col3"].Value = inforLines[i].PathReportLocal;
          dataGridView1.Rows[row].Cells["col4"].Value = inforLines[i].PathReportOneDrive;
          dataGridView1.Rows[row].Cells["col5"].Value = inforLines[i].IsEnable == true? Properties.Resources.check: Properties.Resources.no_check;
          dataGridView1.Rows[row].Cells["col6"].Value = "Thay đổi";
          
          dataGridView1.Rows[row].Tag = inforLines[i];
        }
      }

      this.lbTotal.Text = total;
    }


  }
}
