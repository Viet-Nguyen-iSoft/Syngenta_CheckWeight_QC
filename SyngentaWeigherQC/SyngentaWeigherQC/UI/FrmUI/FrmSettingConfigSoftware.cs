using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using System;
using System.Net;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.eUI;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmSettingConfigSoftware : Form
  {
    public delegate void SendChangeNameStation(ConfigSoftware configSoftware);
    public event SendChangeNameStation OnSendChangeNameStation;

    public delegate void SendChangeConnection(ConfigSoftware configSoftware);
    public event SendChangeConnection OnSendChangeConnection;

    public FrmSettingConfigSoftware()
    {
      InitializeComponent();
    }
    #region Singleton parttern
    private static FrmSettingConfigSoftware _Instance = null;
    public static FrmSettingConfigSoftware Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new FrmSettingConfigSoftware();
        }
        return _Instance;
      }
    }
    #endregion


    private void FrmSettingDevice_Load(object sender, EventArgs e)
    {
      LoadData(AppCore.Ins._configSoftware);
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
          txtIp.Texts = configSoftware.IpTcp;
          txtPort.Texts = configSoftware.PortTcp.ToString();
          txtNameStation.Texts = configSoftware.NameStation.ToString();
        }
        else
        {
          txtIp.Texts = string.Empty;
          txtPort.Texts = string.Empty;
          txtNameStation.Texts = string.Empty;
        }
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
      }
    }

    private async void btnSave_Click(object sender, EventArgs e)
    {
      try
      {
        int port = 0;

        if (!IPAddress.TryParse(txtIp.Texts, out _))
        {
          new FrmNotification().ShowMessage("Sai định dạng IP", eMsgType.Warning);
          return;
        }

        if (!int.TryParse(txtPort.Texts, out port))
        {
          new FrmNotification().ShowMessage("Sai định dạng Port", eMsgType.Warning);
          return;
        }

        AppCore.Ins._configSoftware.IpTcp = txtIp.Texts.Trim();
        AppCore.Ins._configSoftware.PortTcp = port;
        await AppCore.Ins.Update(AppCore.Ins._configSoftware);

        new FrmNotification().ShowMessage("Lưu thành công.", eMsgType.Info);
        OnSendChangeConnection?.Invoke(AppCore.Ins._configSoftware);
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
        new FrmNotification().ShowMessage("Lưu thất bại.", eMsgType.Warning);
      }

    }

    private async void btnSaveStationName_Click(object sender, EventArgs e)
    {
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


  }
}
