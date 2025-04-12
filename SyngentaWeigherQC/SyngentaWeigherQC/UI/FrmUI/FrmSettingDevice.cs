using SerialPortLib;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.eUI;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmSettingDevice : Form
  {
    public delegate void SendChangeSerial();
    public event SendChangeSerial OnSendChangeSerial;

    public delegate void SendTestSendSerial(string Str);
    public event SendTestSendSerial OnSendTestSendSerial;

    public FrmSettingDevice()
    {
      InitializeComponent();
    }
    #region Singleton parttern
    private static FrmSettingDevice _Instance = null;
    public static FrmSettingDevice Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new FrmSettingDevice();
        }
        return _Instance;
      }
    }
    #endregion

    private SerialControllers _serialControllers = new SerialControllers();
    private InforLine _station = new InforLine();
    private void FrmSettingDevice_Load(object sender, EventArgs e)
    {
      _serialControllers = AppCore.Ins._serialControllers;
      _station = AppCore.Ins._stationCurrent;
      if (_serialControllers != null && _station!=null)
      {
        LoadData(_serialControllers, _station);
      }

      this.tableLayoutPanel6.Visible = (AppCore.Ins._roleCurrent.Name == "iSOFT" || AppCore.Ins._isPermitDev == false);
    }

    private void LoadData(SerialControllers serialControllers, InforLine stations)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LoadData(serialControllers, stations);
        }));
        return;
      }

      try
      {
        string[] ports = SerialPort.GetPortNames();
        if (ports.Length > 0)
        {
          this.cbCOM.DataSource = ports;
        }

        this.cbParity.DataSource = Enum.GetValues(typeof(Parity));
        this.cbDatabit.DataSource = Enum.GetValues(typeof(DataBits));
        this.cbStopbit.DataSource = Enum.GetValues(typeof(StopBits));


        this.cbCOM.Text = serialControllers.COM;
        this.cbBaudrate.SelectedItem = serialControllers.Baud.ToString();
        this.cbParity.SelectedItem = serialControllers.Parity;
        this.cbDatabit.SelectedItem = serialControllers.Databits;
        this.cbStopbit.SelectedItem = serialControllers.Stopbits;



        //Setting Station
        //btnSendRequest.Image = (AppCore.Ins._stationCurrent.IsEnableRequestWeigher) ?
        //                            Properties.Resources.switch_on : Properties.Resources.switch_off;

        //int time = AppCore.Ins._stationCurrent.TimeSendRequestWeigher;

        //this.numbersTime.Value = (decimal)time;
      }
      catch (Exception ex)
      {
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
      }
    }

    private async void btnSave_Click(object sender, EventArgs e)
    {
      try
      {
        int baudrate = 9600;
        int.TryParse(this.cbBaudrate.SelectedItem.ToString(), out baudrate);

        AppCore.Ins._serialControllers.COM = this.cbCOM.Text;
        AppCore.Ins._serialControllers.Baud = baudrate;
        AppCore.Ins._serialControllers.Parity = (Parity)Enum.Parse(typeof(Parity), this.cbParity.SelectedItem.ToString());
        AppCore.Ins._serialControllers.Databits = (DataBits)Enum.Parse(typeof(DataBits), this.cbDatabit.SelectedItem.ToString());
        AppCore.Ins._serialControllers.Stopbits = (StopBits)Enum.Parse(typeof(StopBits), this.cbStopbit.SelectedItem.ToString());
        AppCore.Ins._serialControllers.UpdatedAt = DateTime.Now;

        await AppCore.Ins.UpdateSerial(AppCore.Ins._serialControllers);
        OnSendChangeSerial?.Invoke();

        new FrmNotification().ShowMessage("Save thành công.", eMsgType.Info);
      }
      catch (Exception ex)
      {
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
        new FrmNotification().ShowMessage("Save thất bại.", eMsgType.Warning);
      }
      
    }

    private async void btnSendRequest_Click(object sender, EventArgs e)
    {
      //_station.IsEnableRequestWeigher = !_station.IsEnableRequestWeigher;
      //await AppCore.Ins.Update(_station);

      //btnSendRequest.Image = _station.IsEnableRequestWeigher ? Properties.Resources.switch_on : Properties.Resources.switch_off;
    }

    private async void btnSaveTime_Click(object sender, EventArgs e)
    {
      //_station.TimeSendRequestWeigher = (int)numbersTime.Value;
      //await AppCore.Ins.Update(_station);
    }

    private void rjButton1_Click(object sender, EventArgs e)
    {
      OnSendTestSendSerial?.Invoke(txtDataSend.Text);
    }
  }
}
