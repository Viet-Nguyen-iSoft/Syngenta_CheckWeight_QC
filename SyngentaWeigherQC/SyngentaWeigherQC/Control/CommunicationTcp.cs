using SyngentaWeigherQC.Communication;
using SyngentaWeigherQC.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static SyngentaWeigherQC.eNum.eUI;
using System.Windows.Forms;
using System.Timers;
using DocumentFormat.OpenXml.Drawing.Charts;
using SyngentaWeigherQC.Models;

namespace SyngentaWeigherQC.Control
{
  public partial class AppCore
  {
    public delegate void SendStatusConnectWeight(eStatusConnectWeight eStatusConnectWeight);
    public event SendStatusConnectWeight OnSendStatusConnectWeight;

    public System.Timers.Timer _timerCheckTimerCheckConnect = new System.Timers.Timer();
    public bool ensureLoadUI = false;



    #region // Cân Tcp
    private CommonTCPClient commonTCPClient { get; set; }

    private bool PingIp
    {
      get
      {
        Ping ping = new Ping();
        PingReply FindPLC = ping.Send(ip_tcp_mettler, 1000);
        return FindPLC.Status.ToString().Equals("Success");
      }
    }
    private void InitTcpConnectivity()
    {
      try
      {
        if (PingIp)
        {
          commonTCPClient = new CommonTCPClient(ip_tcp_mettler, port_tcp_mettler, "Syngenta");
          commonTCPClient.OnConnectionEventRaise += CommonTCPClient_OnConnectionEventRaise;
          commonTCPClient.OnDataReceive += CommonTCPClient_OnDataReceive;
          commonTCPClient.Connect();
        }
      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog("Kết nối InitTcpConnectivity: " + ex.ToString());
      }
      finally
      {
        _timerCheckTimerCheckConnect.Interval = 1000;
        _timerCheckTimerCheckConnect.Elapsed += _timerCheckTimerCheckConnect_Elapsed;
        _timerCheckTimerCheckConnect.Start();
      }
    }

    private void _timerCheckTimerCheckConnect_Elapsed(object sender, ElapsedEventArgs e)
    {
      this._timerCheckTimerCheckConnect.Stop();
      try
      {
        if (eModeCommunication == eModeCommunication.Serial)
        {
          //_listPortPC = SerialPort.GetPortNames();
          //bool isConnectSerrial = (_listPortPC.Contains(_serialControllers.COM));
          ////FrmHome.Instance.ConnectSerialWeigher(isConnectSerrial);
        }
        else if (eModeCommunication == eModeCommunication.TcpClient)
        {
          try
          {
            if (PingIp)
            {
              if (commonTCPClient == null)
              {
                //Mở kết nối lại
                commonTCPClient = new CommonTCPClient(ip_tcp_mettler, port_tcp_mettler, "Syngenta");
                commonTCPClient.OnConnectionEventRaise += CommonTCPClient_OnConnectionEventRaise;
                commonTCPClient.OnDataReceive += CommonTCPClient_OnDataReceive;
                commonTCPClient.Connect();
                return;
              }
              if (!commonTCPClient.Connected)
              {
                //Đóng kết nối
                commonTCPClient.OnConnectionEventRaise -= CommonTCPClient_OnConnectionEventRaise;
                commonTCPClient.OnDataReceive -= CommonTCPClient_OnDataReceive;
                commonTCPClient.Disconnect();

                //Mở kết nối lại
                commonTCPClient = new CommonTCPClient(ip_tcp_mettler, port_tcp_mettler, "Syngenta");
                commonTCPClient.OnConnectionEventRaise += CommonTCPClient_OnConnectionEventRaise;
                commonTCPClient.OnDataReceive += CommonTCPClient_OnDataReceive;
                commonTCPClient.Connect();
              }
              else
              {
                if (!ensureLoadUI)
                {
                  eStatusConnectWeight eStatusConnectWeight = eStatusConnectWeight.Disconnnect;
                  if (commonTCPClient != null)
                  {
                    eStatusConnectWeight = commonTCPClient.Connected ? eStatusConnectWeight.Connected : eStatusConnectWeight.Disconnnect;
                  }
                  OnSendStatusConnectWeight?.Invoke(eStatusConnectWeight);
                }  
              }  
            }
          }
          catch (Exception ex)
          {
            if (!ensureLoadUI)
            {
              eStatusConnectWeight eStatusConnectWeight = eStatusConnectWeight.Disconnnect;
              if (commonTCPClient != null)
              {
                eStatusConnectWeight = commonTCPClient.Connected ? eStatusConnectWeight.Connected : eStatusConnectWeight.Disconnnect;
              }
              OnSendStatusConnectWeight?.Invoke(eStatusConnectWeight);
            }

            eLoggerHelper.LogErrorToFileLog("Kết nối InitTcpConnectivity: " + ex.ToString());
          }
        }
      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex.ToString());
      }
      finally
      {
        this._timerCheckTimerCheckConnect.Start();
      }
    }




    private async void CommonTCPClient_OnDataReceive(object sender, SuperSimpleTcp.DataReceivedEventArgs e)
    {
      try
      {
        if (eStatusModeWeight == eStatusModeWeight.OverView)
        {
          return;
        }  

        _shiftIdCurrent = GetShiftCode();

        if (_shiftIdCurrent==-1)
        {
          OnSendWarning?.Invoke(inforLineOperation, "Vui lòng chọn loại ca !", eMsgType.Warning);
          return;
        }

        if (inforLineOperation.ProductionCurrent == null)
        {
          OnSendWarning?.Invoke(inforLineOperation, "Vui lòng chọn sản phẩm !", eMsgType.Warning);
          return;
        }

        
        if (e == null) return;
        if (e.Data == null) return;
        byte[] buffer = e.Data.Array;
        if (buffer == null) return;
        if (buffer.Length <= 0) return;

        string dataWeigher = Encoding.UTF8.GetString(buffer).Replace("\0", "");
        if (!string.IsNullOrEmpty(dataWeigher))
        {
          if (dataWeigher.Contains("ERR"))
          {
            //THông báo
            FrmMain.Instance.WarningWeight();
          }
          else
          {
            //Đã có data
            var result = eWeightHelper.ParseDataWeight(dataWeigher);


            if (eStatusModeWeight==eStatusModeWeight.WeightForLine)
            {
              if (inforLineOperation.RequestTare == true)
              {
                OnSendWarning?.Invoke(inforLineOperation, "Vui lòng Tare sản phẩm trước khi lấy mẫu !", eMsgType.Warning);
                return;
              }
              //Save DB
              DatalogWeight datalogWeight = new DatalogWeight();
              datalogWeight.Value = result.Weight;
              datalogWeight.ValuePrevious = result.Weight;
              datalogWeight.ShiftId = _shiftIdCurrent;

              datalogWeight.InforLineId = inforLineOperation?.Id;
              datalogWeight.ProductionId = inforLineOperation.ProductionCurrent?.Id;

              datalogWeight.DatalogTareId = inforLineOperation?.DatalogTareCurrent?.Id;

              DatalogWeight datalogWeightAdd = await Add(datalogWeight);

              OnSendValueDatalogWeight?.Invoke(inforLineOperation, datalogWeightAdd);
            }
            else if (eStatusModeWeight == eStatusModeWeight.TareForLine)
            {
              OnSendValueTare?.Invoke(result.Weight);
            }  
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }

    private void CommonTCPClient_OnConnectionEventRaise(object sender, SuperSimpleTcp.ConnectionEventArgs e)
    {
      var tcp = sender as SuperSimpleTcp.SimpleTcpClient;
      eStatusConnectWeight eStatusConnectWeight = eStatusConnectWeight.None;
      if (tcp != null)
      {
        eStatusConnectWeight = tcp.IsConnected ? eStatusConnectWeight.Connected : eStatusConnectWeight.Disconnnect;
      }
      OnSendStatusConnectWeight?.Invoke(eStatusConnectWeight);
    }

    private void FilterDataWeigherTcp(double current_weigher_value)
    {
      try
      {
        if (_currentProduct == null) return;

        //Save DB
        if (_eWeigherMode == eWeigherMode.Normal && _readyReceidDataWeigher == eReadyReceidWeigher.Yes)
        {
          //SaveDataWeigher(current_weigher_value);
        }
        else if (_eWeigherMode == eWeigherMode.SampleRework)
        {
          OnSendDataReWeigher?.Invoke(current_weigher_value);
        }
        else if (_eWeigherMode == eWeigherMode.Tare)
        {
          //SaveDataTareWeigher(valueFilter);
        }


        FrmMain.Instance.RefeshTimeOut();
      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog("FilterDataWeigher>> " + ex.Message + "&&" + ex.StackTrace);
      }

    }
    #endregion
  }
}
