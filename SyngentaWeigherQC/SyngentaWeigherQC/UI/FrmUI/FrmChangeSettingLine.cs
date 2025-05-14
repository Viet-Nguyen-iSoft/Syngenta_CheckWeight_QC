using CustomControls.RJControls;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.enumSoftware;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmChangeSettingLine : Form
  {
    public delegate void SendSaveChange();
    public event SendSaveChange OnSendSaveChange;

    public FrmChangeSettingLine()
    {
      InitializeComponent();
    }

    private InforLine _inforLine = new InforLine();
    public FrmChangeSettingLine(InforLine inforLine):this()
    {
      _inforLine = inforLine;
      SetInforLine();
    }


    private void SetInforLine()
    {
      this.txtNameLine.Texts = _inforLine.Name;
      this.txtLineCode.Texts = _inforLine.Code;
      this.txtPathLocal.Texts = _inforLine.PathReportLocal;
      this.txtPathOnedrive.Texts = _inforLine.PathReportOneDrive;
      this.txtPassExcel.Texts = _inforLine.PassReport;

      this.btnCheckActionPassReport.Image = _inforLine.ActionPassReport==true ? 
                                            AppCore.Ins.ByteArrayToImage(Properties.Resources.tick) :  
                                            AppCore.Ins.ByteArrayToImage(Properties.Resources.no_tick);

      this.btnActionLine.Image = _inforLine.IsEnable == true ?
                                            AppCore.Ins.ByteArrayToImage(Properties.Resources.tick) :
                                            AppCore.Ins.ByteArrayToImage(Properties.Resources.no_tick);
    }

    private void btnActionLine_Click(object sender, EventArgs e)
    {
      _inforLine.IsEnable = !_inforLine.IsEnable;
      this.btnActionLine.Image = _inforLine.IsEnable == true ?
                                            AppCore.Ins.ByteArrayToImage(Properties.Resources.tick) :
                                            AppCore.Ins.ByteArrayToImage(Properties.Resources.no_tick);
    }

    private void btnCheckActionPassReport_Click(object sender, EventArgs e)
    {
      _inforLine.ActionPassReport = !_inforLine.ActionPassReport;
      this.btnCheckActionPassReport.Image = _inforLine.ActionPassReport == true ?
                                            AppCore.Ins.ByteArrayToImage(Properties.Resources.tick) :
                                            AppCore.Ins.ByteArrayToImage(Properties.Resources.no_tick);
    }

    private void btnPathLocal_Click(object sender, EventArgs e)
    {
      using (var folderBrowserDialog = new FolderBrowserDialog())
      {
        DialogResult result = folderBrowserDialog.ShowDialog();
        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
        {
          this._inforLine.PathReportLocal = folderBrowserDialog.SelectedPath;
          SetText(this.txtPathLocal, this._inforLine.PathReportLocal);
        }
      }
    }

    private void btnPathOnedrive_Click(object sender, EventArgs e)
    {
    
      using (var folderBrowserDialog = new FolderBrowserDialog())
      {
        DialogResult result = folderBrowserDialog.ShowDialog();
        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
        {
          this._inforLine.PathReportOneDrive = folderBrowserDialog.SelectedPath;
          SetText(this.txtPathOnedrive, this._inforLine.PathReportOneDrive);
        }
      }
    }

    private async void btnSaveChange_Click(object sender, EventArgs e)
    {
      _inforLine.Name = txtNameLine.Texts;
      _inforLine.Code = txtLineCode.Texts;
      _inforLine.PassReport = txtPassExcel.Texts;

      var rs = await AppCore.Ins.Update(this._inforLine);
      if (rs)
      {
        //new FrmNotification().ShowMessage("Lưu thay đổi thành công.", eMsgType.Info);
        OnSendSaveChange?.Invoke();
        this.Close();
      }
      else
      {
        new FrmNotification().ShowMessage("Lưu thay đổi thất bại !", eMsgType.Error);
      }  
    }

    private void SetText(RJTextBox rJTextBox, string text)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetText(rJTextBox, text);
        }));
        return;
      }

      rJTextBox.Texts = text;
    }


    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnDeteleLine_Click(object sender, EventArgs e)
    {

    }
  }
}
