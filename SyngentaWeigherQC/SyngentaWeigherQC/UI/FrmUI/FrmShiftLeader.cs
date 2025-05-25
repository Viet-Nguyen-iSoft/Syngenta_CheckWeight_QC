using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.enumSoftware;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmShiftLeader : Form
  {
    public FrmShiftLeader()
    {
      InitializeComponent();
    }
    #region Singleton parttern
    private static FrmShiftLeader _Instance = null;
    public static FrmShiftLeader Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new FrmShiftLeader();
        }
        return _Instance;
      }
    }


    #endregion

    private string file_name = "";
    private void FrmUser_Load(object sender, EventArgs e)
    {
      UpdateDataUI(AppCore.Ins._listShiftLeader);
    }

    private void UpdateDataUI(List<ShiftLeader> listUser)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          UpdateDataUI(listUser);
        }));
        return;
      }

      dataGridView2.Rows.Clear();
      if (listUser.Count() > 0)
      {
        for (int i = 0; i < listUser.Count(); i++)
        {
          int row = dataGridView2.Rows.Add();
          dataGridView2.Rows[row].Cells["Column4"].Value = i + 1;
          dataGridView2.Rows[row].Cells["Column1"].Value = listUser[i].UserName;
          dataGridView2.Rows[row].Cells["Column2"].Value = "Chỉnh sửa";
          dataGridView2.Rows[row].Cells["Column3"].Value = "Xóa";
        }
      }
    }


    private ShiftLeader user_choose = new ShiftLeader();
    private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      int rowIndex = e.RowIndex;
      int colmnIndex = e.ColumnIndex;
      if (rowIndex == -1) return;

      try
      {
        var data = dataGridView2.Rows[rowIndex];
        string nameUser = data.Cells[1].Value.ToString();
        user_choose = AppCore.Ins._listShiftLeader.Where(x => x.UserName == nameUser).FirstOrDefault();
        if (user_choose != null)
        {
          if (colmnIndex == 2)//Edit
          {
            if (!AppCore.Ins.CheckRole(ePermit.SettingShiftLeader))
            {
              new FrmNotification().ShowMessage("Tài khoản không có quyền !", eMsgType.Warning);
              return;
            }

            FrmAddNewShiftLeader frmUpdateUser = new FrmAddNewShiftLeader("Cập nhật tên người dùng", user_choose);
            frmUpdateUser.OnSendOKClicked += FrmUpdateUser_OnSendOKClicked;
            frmUpdateUser.ShowDialog();
          }
          else if (colmnIndex == 3)//Remove
          {
            if (!AppCore.Ins.CheckRole(ePermit.SettingShiftLeader))
            {
              new FrmNotification().ShowMessage("Tài khoản không có quyền !", eMsgType.Warning);
              return;
            }

            string content = $"Tên: {user_choose.UserName} \r\nSẽ được xóa khỏi danh sách vĩnh viễn ?";
            FrmConfirm frmConfirm = new FrmConfirm(content, eMsgType.Warning);
            frmConfirm.OnSendOKClicked += FrmConfirm_OnSendOKClicked;
            frmConfirm.ShowDialog();
          }
        }
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
      }
    }

    private async void FrmConfirm_OnSendOKClicked()
    {
      //Cập nhật
      user_choose.IsDelete = true;
      user_choose.UpdatedAt = DateTime.Now;
      await AppCore.Ins.Update(user_choose);

      //Load lại
      AppCore.Ins._listShiftLeader = await AppCore.Ins.GetList();
      UpdateDataUI(AppCore.Ins._listShiftLeader);
    }

    private async void FrmUpdateUser_OnSendOKClicked()
    {
      AppCore.Ins._listShiftLeader = await AppCore.Ins.GetList();
      UpdateDataUI(AppCore.Ins._listShiftLeader);
    }

    private void btnAddNew_Click(object sender, EventArgs e)
    {
      FrmAddNewShiftLeader frmAddUser = new FrmAddNewShiftLeader("Thêm tên người dùng");
      frmAddUser.OnSendOKClicked += FrmAddUser_OnSendOKClicked;
      frmAddUser.ShowDialog();
    }

    private async void FrmAddUser_OnSendOKClicked()
    {
      AppCore.Ins._listShiftLeader = await AppCore.Ins.GetList();
      UpdateDataUI(AppCore.Ins._listShiftLeader);
      ConfirmRestartApp();
    }

    private void ConfirmRestartApp()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          ConfirmRestartApp();
        }));
        return;
      }

      FrmConfirm frmConfirmRestartApp = new FrmConfirm($"Cập nhật dữ liệu thành công.\r\nKhởi động lại để cập nhật dữ liệu mới ?", eMsgType.Question);
      frmConfirmRestartApp.OnSendOKClicked += FrmConfirmRestartApp_OnSendOKClicked;
      frmConfirmRestartApp.ShowDialog();
    }

    private void btnImport_Click(object sender, EventArgs e)
    {
      DialogResult result = this.openFileDialogImport.ShowDialog();
      if (result == DialogResult.OK)
      {
        if (backgroundWorkerImport.IsBusy == false)
        {
          this.backgroundWorkerImport.RunWorkerAsync();
        }
      }
    }

    private void openFileDialogImport_FileOk(object sender, CancelEventArgs e)
    {
      file_name = openFileDialogImport.FileName;
    }

    private List<ShiftLeader> listUserExcel = new List<ShiftLeader> { new ShiftLeader() };
    private List<ShiftLeader> shiftLeaders_New = new List<ShiftLeader>();
    private List<ShiftLeader> shiftLeaders_ExitsDb = new List<ShiftLeader>();
    private List<ShiftLeader> shiftLeaders_Db_Old = new List<ShiftLeader>();
    private async void backgroundWorkerImport_DoWork(object sender, DoWorkEventArgs e)
    {
      try
      {
        listUserExcel = HelperImportExcel.PareXlsxByAspose_ShiftLeader(file_name);

        shiftLeaders_ExitsDb = new List<ShiftLeader>();
        shiftLeaders_New = new List<ShiftLeader>();
        if (listUserExcel.Count > 0)
        {
          shiftLeaders_Db_Old = await AppCore.Ins.GetList();
          foreach (ShiftLeader shiftLeader in listUserExcel)
          {
            if (shiftLeaders_Db_Old.Any(x => x.UserName == shiftLeader.UserName))
            {
              shiftLeaders_ExitsDb.Add(shiftLeader);
            }
            else
            {
              shiftLeaders_New.Add(shiftLeader);
            }
          }
        }
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
        new FrmNotification().ShowMessage("File excel load thất bại !", eMsgType.Warning);
      }
    }

    private void backgroundWorkerImport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (listUserExcel.Count > 0)
      {
        UpdateDataUI(listUserExcel);
        VisibleSave(shiftLeaders_New.Count() > 0);

        new FrmNotification().ShowMessage($"Có {shiftLeaders_New.Count()} dữ liệu mới\r\nCó {shiftLeaders_ExitsDb.Count()} dữ liệu cập nhật", eMsgType.Info);
      }
      else
      {
        new FrmNotification().ShowMessage("Không có dữ liệu !", eMsgType.Info);
      }
    }

    private async void btnSave_Click(object sender, EventArgs e)
    {
      try
      {
        if (shiftLeaders_Db_Old.Count > 0)
        {
          foreach (var shiftLeader in shiftLeaders_Db_Old)
          {
            if (shiftLeaders_ExitsDb.Any(x => x.UserName == shiftLeader.UserName))
            {
              shiftLeader.UpdatedAt = DateTime.Now;
            }
            else
            {
              shiftLeader.IsDelete = true;
              shiftLeader.UpdatedAt = DateTime.Now;
            }
          }

          await AppCore.Ins.UpdateRange(shiftLeaders_Db_Old);
        }

        if (shiftLeaders_New.Count() > 0)
        {
          shiftLeaders_New.ForEach(x => x.CreatedAt = DateTime.Now);
          await AppCore.Ins.AddRange(shiftLeaders_New);
        }

        FrmConfirm frmConfirmRestartApp = new FrmConfirm($"Cập nhật dữ liệu thành công.\r\nKhởi động lại để cập nhật dữ liệu mới ?", eMsgType.Question);
        frmConfirmRestartApp.OnSendOKClicked += FrmConfirmRestartApp_OnSendOKClicked;
        frmConfirmRestartApp.ShowDialog();
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
      }
      finally
      {
        VisibleSave(false);
      }
    }

    private void FrmConfirmRestartApp_OnSendOKClicked()
    {
      Program.RestartApp();
    }

    private void VisibleSave(bool isVisible)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          VisibleSave(isVisible);
        }));
        return;
      }

      this.btnSave.Visible = isVisible;
    }
  }
}
