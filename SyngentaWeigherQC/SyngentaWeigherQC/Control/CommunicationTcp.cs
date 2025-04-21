using SyngentaWeigherQC.Communication;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.UI.FrmUI;
using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.eUI;

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
        LoggerHelper.LogErrorToFileLog("Kết nối InitTcpConnectivity: " + ex.ToString());
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
        if (eModeCommunication == eModeCommunication.TcpClient)
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
                OnSendStatusConnectWeight?.Invoke(eStatusConnectWeight.Disconnnect);

                //Đóng kết nối
                commonTCPClient.OnConnectionEventRaise -= CommonTCPClient_OnConnectionEventRaise;
                commonTCPClient.OnDataReceive -= CommonTCPClient_OnDataReceive;
                commonTCPClient.Disconnect();

                //Mở kết nối lại
                commonTCPClient = new CommonTCPClient(ip_tcp_mettler, port_tcp_mettler, "Syngenta");
                commonTCPClient.OnConnectionEventRaise += CommonTCPClient_OnConnectionEventRaise;
                commonTCPClient.OnDataReceive += CommonTCPClient_OnDataReceive;
                commonTCPClient.Connect();
                return;
              }
              else
              {
                OnSendStatusConnectWeight?.Invoke(eStatusConnectWeight.Connected);
              }
            }
            else
            {
              OnSendStatusConnectWeight?.Invoke(eStatusConnectWeight.Disconnnect);
            }
          }
          catch (Exception ex)
          {
            //OnSendStatusConnectWeight?.Invoke(eStatusConnectWeight.Disconnnect);

            LoggerHelper.LogErrorToFileLog("Kết nối InitTcpConnectivity: " + ex.ToString());
          }
        }
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex.ToString());
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

        _shiftIdCurrent = GetShiftCode(inforLineOperation);

        if (_shiftIdCurrent == null)
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
            //FormMain.Instance.WarningWeight();
          }
          else
          {
            //Đã có data
            var result = WeightHelper.ParseDataWeight(dataWeigher);


            if (eStatusModeWeight == eStatusModeWeight.WeightForLine)
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
              datalogWeight.ShiftId = _shiftIdCurrent.Id;

              datalogWeight.InforLineId = inforLineOperation?.Id;

              datalogWeight.ProductionId = inforLineOperation.ProductionCurrent?.Id;

              datalogWeight.DatalogTareId = inforLineOperation?.DatalogTareCurrent?.Id;

              datalogWeight.ShiftLeaderId = inforLineOperation?.ShiftLeaderId;

              datalogWeight.ShiftTypeId = inforLineOperation?.ShiftTypesId;

              DatalogWeight datalogWeightAdd = await Add(datalogWeight);

              //Gửi đi update Home
              datalogWeight.InforLine = inforLineOperation;
              datalogWeight.Production = inforLineOperation.ProductionCurrent;
              datalogWeight.DatalogTare = inforLineOperation?.DatalogTareCurrent;
              datalogWeight.ShiftLeaderId = inforLineOperation?.ShiftLeaderId;
              datalogWeight.Shift = _shiftIdCurrent;
              OnSendValueDatalogWeight?.Invoke(inforLineOperation, datalogWeightAdd);
            }
            else if (eStatusModeWeight == eStatusModeWeight.TareForLine)
            {
              OnSendValueTare?.Invoke(result.Weight);
            }
            else
            {
              OnSendValueReweight?.Invoke(result.Weight);
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

    
    #endregion
  }
}
