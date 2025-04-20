using Aspose.Cells;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.eUI;
using Workbook = Aspose.Cells.Workbook;
using Worksheet = Aspose.Cells.Worksheet;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmUser : Form
  {
    public delegate void SendChangeLine();
    public event SendChangeLine OnSendChangeLine;

    public FrmUser()
    {
      InitializeComponent();
    }
    #region Singleton parttern
    private static FrmUser _Instance = null;
    public static FrmUser Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new FrmUser();
        }
        return _Instance;
      }
    }


    #endregion

    private void FrmUser_Load(object sender, EventArgs e)
    {
      UpdateDataUI(AppCore.Ins._listShiftLeader);
    }

    private void Instance_OnSendAddNewUser()
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


    private ShiftLeader user = new ShiftLeader();
    private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      int rowIndex = e.RowIndex;
      int colmnIndex = e.ColumnIndex;
      if (rowIndex == -1) return;

      try
      {
        var data = dataGridView2.Rows[rowIndex];
        string nameUser = data.Cells[1].Value.ToString();
        user = AppCore.Ins._listShiftLeader.Where(x => x.UserName == nameUser).FirstOrDefault();
        if (user != null)
        {
          if (colmnIndex == 2)//Edit
          {
            if (!AppCore.Ins.CheckRole(ePermit.role_ChangeUser))
            {
              new FrmNotification().ShowMessage("Tài khoản không có quyền chỉnh sửa User!", eMsgType.Warning);
              return;
            }

            FrmAddNewUser frmAddNewUser = new FrmAddNewUser("Cập nhật tên người dùng", user, eEditUser.ChangeUser);
            frmAddNewUser.OnSendOKClicked += FrmAddNewUser_OnSendOKClicked;
            frmAddNewUser.ShowDialog();
          }
          else if (colmnIndex == 3)//Remove
          {
            if (!AppCore.Ins.CheckRole(ePermit.role_ChangeUser))
            {
              new FrmNotification().ShowMessage("Tài khoản không có quyền xóa User!", eMsgType.Warning);
              return;
            }

            string content = $"Tên User: {user.UserName} \r\nSẽ được xóa khỏi danh sách vĩnh viễn ?";
            //FrmConfirmV2 frmConfirm = new FrmConfirmV2(content, eMsgType.Warning);
            //frmConfirm.OnSendOKClicked += FrmConfirm_OnSendOKClicked;
            //frmConfirm.ShowDialog();
          }
        }
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
      }
    }



    private void FrmAddNewUser_OnSendOKClicked(ShiftLeader user)
    {
      //await AppCore.Ins.UpdateUser(user);
      //UpdateDataUI(AppCore.Ins._listShiftLeader);
      //OnSendUpdateUser?.Invoke();
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

    private async void btnSave_Click(object sender, EventArgs e)
    {
      if (listUserExcel.Count > 0)
      {
        RemoveUserOld();
        await AppCore.Ins.AddRangeUsers(listUserExcel);
        VisibleSave(false);
        AppCore.Ins._listShiftLeader = listUserExcel;
        new FrmNotification().ShowMessage("Lưu thành công", eMsgType.Info);
      }
    }

    private async void RemoveUserOld()
    {
      if (AppCore.Ins._listShiftLeader.Count > 0)
      {
        for (int i = 0; i < AppCore.Ins._listShiftLeader.Count; i++)
        {
          AppCore.Ins._listShiftLeader[i].IsDelete = true;
        }

        await AppCore.Ins.UpdateRangeUsers(AppCore.Ins._listShiftLeader);
      }

    }

    private string file_name = "";
    private void openFileDialogImport_FileOk(object sender, CancelEventArgs e)
    {
      file_name = openFileDialogImport.FileName;
    }


    private List<ShiftLeader> listUserExcel = new List<ShiftLeader> { new ShiftLeader() };
    private void backgroundWorkerImport_DoWork(object sender, DoWorkEventArgs e)
    {
      try
      {
        listUserExcel = PareXlsxByAspose(file_name);
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
        VisibleSave(true);
      }
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



    public List<ShiftLeader> PareXlsxByAspose(string file_path)
    {
      try
      {
        FileInfo dest_file_info = new FileInfo(file_path);
        Workbook wb = new Workbook(dest_file_info.FullName);
        WorksheetCollection collection = wb.Worksheets;
        bool is_exit_loop = false;
        int max_rows = 0; int max_cols = 0;

        List<ShiftLeader> users = new List<ShiftLeader>();
        for (int worksheetIndex = 0; worksheetIndex < collection.Count && is_exit_loop == false; worksheetIndex++)
        {
          Worksheet worksheet = collection[worksheetIndex];
          if (worksheetIndex == 0)
          {
            max_rows = worksheet.Cells.MaxDataRow;
            max_cols = worksheet.Cells.MaxDataColumn;

            for (int row = 1; row <= max_rows; row++)
            {
              ShiftLeader user = new ShiftLeader();

              user.UserName = GetText(worksheet, row, 1);
              user.Passwords = "";
              user.RoleId = 4;
              user.IsEnable = false;
              user.CreatedAt = DateTime.Now;
              user.UpdatedAt = DateTime.Now;

              if (!string.IsNullOrEmpty(user.UserName)) users.Add(user);
            }
          }
        }

        return users;
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
        new FrmNotification().ShowMessage("File excel load thất bại !", eMsgType.Warning);
        return null;
      }
    }

    private string GetText(Worksheet worksheet, int row, int column)
    {
      string ret = "";
      try
      {
        object textObj = worksheet.Cells[row, column].Value;
        if (textObj != null)
        {
          ret = textObj.ToString().Trim();
        }

      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
      }
      return ret;
    }


  }
}
