using Aspose.Cells;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using SQLitePCL;
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
using static SyngentaWeigherQC.eNum.eUI;
using Color = System.Drawing.Color;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmSettingGeneral : Form
  {
    public delegate void SendSettingNumberRaw(int numberRaw);
    public event SendSettingNumberRaw OnSendSettingNumberRaw;

    public delegate void SendModeOnOffApp(bool isOn);
    public event SendModeOnOffApp OnSendModeOnOffApp;

    public delegate void SendSettingNumberTimeOut(int timerNumber);
    public event SendSettingNumberTimeOut OnSendSettingNumberTimeOut;

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
    private bool isEnableChangeRecord= false;
    private void FrmSettingLine_Load(object sender, EventArgs e)
    {
      listShift = AppCore.Ins._listShift;

      LoadSettingDefault(AppCore.Ins._stationCurrent);

      Instance_OnSendChangeRole();
      FrmMain.Instance.OnSendChangeLogin += Instance_OnSendChangeRole;
      FrmDecentralization.Instance.OnSendChangeDecentralization += Instance_OnSendChangeRole;

      this.btn3Ca.PerformClick();
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

      dataGridView1.Columns[3].Visible = AppCore.Ins.CheckRole(ePermit.role_ShiftLineSetting);
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

      bool isRoleOK = AppCore.Ins.CheckRole(ePermit.role_ShiftLineSetting);

      this.dataGridView1.Columns[3].Visible = isRoleOK;
      this.btnSaveTimeOut.Visible = isRoleOK;
    }

  

    private void LoadSettingDefault(InforLine stations)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LoadSettingDefault(stations);
        }));
        return;
      }

      //if (stations != null)
      //{
      //  txtPathFolderLocal.Texts = stations.PathReportLocal;
      //  txtPathFolderOnedrive.Texts = stations.PathReportOneDrive;
      //  txtPassExcel.Texts = stations.PassReport;

      //  timeOut.Value = (stations.TimeCheckTimeOutModeWeigher > 0) ? stations.TimeCheckTimeOutModeWeigher : 1;
      //  btnEnableTimeOutApp.Image = (stations.IsEnableOffApp) ?
      //                              Properties.Resources.switch_on :
      //                              Properties.Resources.switch_off;
      //}  

      //numbersRaw.Value = Properties.Settings.Default.numberRaw;

      ////Setting cho phép cài đặt thay đổi record
      //numericUpDownChangeRecord.Value = Properties.Settings.Default.NumberChangeRecordForShift;
      //isEnableChangeRecord = Properties.Settings.Default.isEnableLimitChangeRecord;
      //btnEnableNumberChangeRecord.Image = isEnableChangeRecord ?
      //                              Properties.Resources.switch_on :
      //                              Properties.Resources.switch_off;
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

   

    private async void btnSaveTimeOut_Click(object sender, EventArgs e)
    {
      //try
      //{
      //  AppCore.Ins._stationCurrent.TimeCheckTimeOutModeWeigher = (int)timeOut.Value;
      //  await AppCore.Ins.Update(AppCore.Ins._stationCurrent);
      //  OnSendSettingNumberTimeOut?.Invoke((int)timeOut.Value);
      //  new FrmNotification().ShowMessage("Lưu thành công.", eMsgType.Info);
      //}
      //catch (Exception)
      //{
      //  new FrmNotification().ShowMessage("Lưu thất bại !", eMsgType.Warning);
      //}
    }


   
    private async void btnEnableTimeOutApp_Click(object sender, EventArgs e)
    {
      //try
      //{
      //  if (AppCore.Ins.CheckRole(ePermit.role_ShiftLineSetting) || AppCore.Ins._isPermitDev)
      //  {
      //    if (AppCore.Ins._stationCurrent.IsEnableOffApp)
      //    {
      //      AppCore.Ins._stationCurrent.IsEnableOffApp = false;
      //      btnEnableTimeOutApp.Image = Properties.Resources.switch_off;
      //    }
      //    else
      //    {
      //      AppCore.Ins._stationCurrent.IsEnableOffApp = true;
      //      btnEnableTimeOutApp.Image = Properties.Resources.switch_on;
      //    }
      //    await AppCore.Ins.Update(AppCore.Ins._stationCurrent);
      //    OnSendModeOnOffApp?.Invoke(AppCore.Ins._stationCurrent.IsEnableOffApp);

      //  }
      //  else
      //  {
      //    new FrmNotification().ShowMessage("Tài khoản không có quyền thay đổi chế độ này", eMsgType.Warning);
      //  }
      //}
      //catch (Exception ex)
      //{
      //  AppCore.Ins.LogErrorToFileLog(ex.Message + "&&" + ex.StackTrace);
      //}
      
    }



    private void btnSaveNumberRaw_Click(object sender, EventArgs e)
    {
      if (AppCore.Ins.CheckRole(ePermit.role_ShiftLineSetting) || AppCore.Ins._isPermitDev)
      {
        try
        {
          Properties.Settings.Default.numberRaw = (int)numbersRaw.Value;
          Properties.Settings.Default.Save();

          OnSendSettingNumberRaw?.Invoke((int)numbersRaw.Value);

          new FrmNotification().ShowMessage("Lưu thành công.", eMsgType.Info);
        }
        catch (Exception)
        {
          new FrmNotification().ShowMessage("Lưu thất bại !", eMsgType.Warning);
        }
      }
      else
      {
        new FrmNotification().ShowMessage("Tài khoản không có quyền thay đổi chế độ này", eMsgType.Warning);
      }
    }

    private void btnSaveChangeRecord_Click(object sender, EventArgs e)
    {
      if (AppCore.Ins.CheckRole(ePermit.role_ShiftLineSetting) || AppCore.Ins._isPermitDev)
      {
        try
        {
          Properties.Settings.Default.NumberChangeRecordForShift = (int)numericUpDownChangeRecord.Value;
          Properties.Settings.Default.Save();

          new FrmNotification().ShowMessage("Lưu thành công.", eMsgType.Info);
        }
        catch (Exception)
        {
          new FrmNotification().ShowMessage("Lưu thất bại !", eMsgType.Warning);
        }
      }
      else
      {
        new FrmNotification().ShowMessage("Tài khoản không có quyền thay đổi chế độ này", eMsgType.Warning);
      }
    }

    private void btnEnableNumberChange_Click(object sender, EventArgs e)
    {
      if (AppCore.Ins.CheckRole(ePermit.role_ShiftLineSetting) || AppCore.Ins._isPermitDev)
      {
        isEnableChangeRecord = !isEnableChangeRecord;

        btnEnableNumberChangeRecord.Image = isEnableChangeRecord ? Properties.Resources.switch_on : Properties.Resources.switch_off;
        Properties.Settings.Default.isEnableLimitChangeRecord = isEnableChangeRecord;
        Properties.Settings.Default.Save();
      }
      else
      {
        new FrmNotification().ShowMessage("Tài khoản không có quyền thay đổi chế độ này", eMsgType.Warning);
      }
    }
  }
}
