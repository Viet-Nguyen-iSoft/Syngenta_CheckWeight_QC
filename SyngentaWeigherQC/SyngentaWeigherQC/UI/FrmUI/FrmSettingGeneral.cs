using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.enumSoftware;
using Color = System.Drawing.Color;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmSettingGeneral : Form
  {
    public delegate void SendChangeNameStation(ConfigSoftware configSoftware);
    public event SendChangeNameStation OnSendChangeNameStation;
    public FrmSettingGeneral()
    {
      InitializeComponent();
    }
    #region Singleton parttern
    private static FrmSettingGeneral _Instance = null;
    public static FrmSettingGeneral Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new FrmSettingGeneral();
        }
        return _Instance;
      }
    }
    #endregion

    private Color colorSelect = Color.MediumSlateBlue;
    private Color colorNoSelect = Color.DarkSlateBlue;
    private List<Shift> listShift = new List<Shift>();
    private eShiftTypes eShiftTypesCurrent = new eShiftTypes();

    private void FrmSettingLine_Load(object sender, EventArgs e)
    {
      LoadData(AppCore.Ins._configSoftware);

      numericUpDownH.Value = Properties.Settings.Default.H;
      numericUpDownW.Value = Properties.Settings.Default.W;

      listShift = AppCore.Ins._listShift;

      Instance_OnSendChangeRole();
      FrmSettingDecentralization.Instance.OnSendChangeDecentralization += Instance_OnSendChangeRole;

      this.btn3Ca.PerformClick();

      FormMain.Instance.OnSendLogInChange += Instance_OnSendLogInChange;
    }

    private void Instance_OnSendLogInChange(Roles account)
    {
      dataGridView1.Columns[3].Visible = AppCore.Ins.CheckRole(ePermit.SettingGeneral);
    }

    private void LoadData(ConfigSoftware configSoftware)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LoadData(configSoftware);
        }));
        return;
      }

      try
      {
        if (configSoftware != null)
        {
          numericUpDownTimeout.Value = configSoftware.Spare1;
          txtNameStation.Texts = configSoftware.NameStation.ToString();
        }
        else
        {
          txtNameStation.Texts = string.Empty;
        }
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
      }
    }



    private void UpdateDataShiftUI(eShiftTypes eShiftTypes)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          UpdateDataShiftUI(eShiftTypes);
        }));
        return;
      }

      List<Shift> shiftList = listShift?.Where(x => x.ShiftTypeId == (int)eShiftTypes).ToList();

      if (shiftList.Count() > 0)
      {
        shiftList = shiftList.OrderBy(x => x.CodeShift).ToList();
        dataGridView1.Rows.Clear();
        for (int i = 0; i < shiftList.Count(); i++)
        {
          int row = dataGridView1.Rows.Add();
          dataGridView1.Rows[row].Cells["Column1"].Value = shiftList[i].Name;
          dataGridView1.Rows[row].Cells["Column2"].Value = shiftList[i].StartHour.ToString("00") + ":" +
                                                           shiftList[i].StartMinute.ToString("00") + ":" +
                                                           shiftList[i].StartSecond.ToString("00");

          dataGridView1.Rows[row].Cells["Column3"].Value = shiftList[i].EndHour.ToString("00") + ":" +
                                                           shiftList[i].EndMinute.ToString("00") + ":" +
                                                           shiftList[i].EndSecond.ToString("00");

          dataGridView1.Rows[row].Cells["Column4"].Value = "Chỉnh sửa";
        }
      }

      dataGridView1.Columns[3].Visible = AppCore.Ins.CheckRole(ePermit.SettingGeneral);
    }


    private void Instance_OnSendChangeRole()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          Instance_OnSendChangeRole();
        }));
        return;
      }

      bool isRoleOK = AppCore.Ins.CheckRole(ePermit.SettingGeneral);

      this.dataGridView1.Columns[3].Visible = isRoleOK;
    }


    private void btn3Ca_Click(object sender, EventArgs e)
    {
      UpdateDataShiftUI(eShiftTypes.BaCa);
      eShiftTypesCurrent = eShiftTypes.BaCa;
      ActiveColor(eShiftTypesCurrent);
    }

    private void btnGianCa_Click(object sender, EventArgs e)
    {
      UpdateDataShiftUI(eShiftTypes.GianCa);
      eShiftTypesCurrent = eShiftTypes.GianCa;
      ActiveColor(eShiftTypesCurrent);
    }

    private void btnHanhChinh_Click(object sender, EventArgs e)
    {
      UpdateDataShiftUI(eShiftTypes.HanhChinh);
      eShiftTypesCurrent = eShiftTypes.HanhChinh;
      ActiveColor(eShiftTypesCurrent);
    }

    private void ActiveColor(eShiftTypes eShiftTypes)
    {
      switch (eShiftTypes)
      {
        case eShiftTypes.BaCa:
          this.btn3Ca.BackColor = colorSelect;
          this.btnGianCa.BackColor = colorNoSelect;
          this.btnHanhChinh.BackColor = colorNoSelect;
          break;
        case eShiftTypes.GianCa:
          this.btn3Ca.BackColor = colorNoSelect;
          this.btnGianCa.BackColor = colorSelect;
          this.btnHanhChinh.BackColor = colorNoSelect;
          break;
        case eShiftTypes.HanhChinh:
          this.btn3Ca.BackColor = colorNoSelect;
          this.btnGianCa.BackColor = colorNoSelect;
          this.btnHanhChinh.BackColor = colorSelect;
          break;

      }
    }

    private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      int rowIndex = e.RowIndex;
      int colIndex = e.ColumnIndex;

      if (rowIndex == -1) return;
      if (colIndex == 3)
      {
        var data = dataGridView1.Rows[rowIndex];
        string nameShift = data.Cells[0].Value.ToString();
        Shift shiftChange = listShift?.Where(x => x.Name == nameShift).FirstOrDefault();
        FrmChangeTimeShift frmChangeTimeShift = new FrmChangeTimeShift(shiftChange);
        frmChangeTimeShift.OnSendSendChangeClicked += FrmChangeTimeShift_OnSendSendChangeClicked;
        frmChangeTimeShift.ShowDialog();
      }
    }

    private async void FrmChangeTimeShift_OnSendSendChangeClicked(Shift shiftChange)
    {
      await AppCore.Ins.UpdateInforShift(shiftChange);
      await AppCore.Ins.ReloadShift();

      UpdateDataShiftUI(eShiftTypesCurrent);
    }

    private async void btnSaveTimeout_Click(object sender, EventArgs e)
    {
      if (!AppCore.Ins.CheckRole(ePermit.SettingGeneral))
      {
        new FrmNotification().ShowMessage("Tài khoản không có quyền !", eMsgType.Warning);
        return;
      }

      int value = (int)numericUpDownTimeout.Value;
      if (value < 10)
      {
        new FrmNotification().ShowMessage("Thời gian tối thiểu cài đặt là 60 giây !", eMsgType.Warning);
        return;
      }
      AppCore.Ins._configSoftware.Spare1 = value;
      await AppCore.Ins.Update(AppCore.Ins._configSoftware);
    }

    private async void btnSaveStationName_Click(object sender, EventArgs e)
    {
      if (!AppCore.Ins.CheckRole(ePermit.SettingGeneral))
      {
        new FrmNotification().ShowMessage("Tài khoản không có quyền !", eMsgType.Warning);
        return;
      }

      try
      {
        AppCore.Ins._configSoftware.NameStation = txtNameStation.Texts.Trim();
        await AppCore.Ins.Update(AppCore.Ins._configSoftware);

        new FrmNotification().ShowMessage("Lưu thành công.", eMsgType.Info);
        OnSendChangeNameStation?.Invoke(AppCore.Ins._configSoftware);
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
        new FrmNotification().ShowMessage("Lưu thất bại.", eMsgType.Warning);
      }
    }

    private void btnSaveChangeNumberChange_Click(object sender, EventArgs e)
    {
      if (!AppCore.Ins.CheckRole(ePermit.SettingGeneral))
      {
        new FrmNotification().ShowMessage("Tài khoản không có quyền !", eMsgType.Warning);
        return;
      }



    }

    private void btnSettingWH_Click(object sender, EventArgs e)
    {
      Properties.Settings.Default.H = (int)numericUpDownH.Value;
      Properties.Settings.Default.W = (int)numericUpDownW.Value;
      Properties.Settings.Default.Save();
    }
  }
}
