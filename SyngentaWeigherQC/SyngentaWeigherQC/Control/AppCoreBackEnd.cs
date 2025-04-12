using ClosedXML.Excel;
using SerialPortLib;
using SyngentaWeigherQC.Communication;
using SyngentaWeigherQC.Logs;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.Responsitory;
using SyngentaWeigherQC.UI.FrmUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.eUI;
using DateTime = System.DateTime;

namespace SyngentaWeigherQC.Control
{
  public partial class AppCore
  {
    private static AppCore _ins = new AppCore();
    public static AppCore Ins
    {
      get
      {
        return _ins == null ? _ins = new AppCore() : _ins;
      }
    }
    public AppCore()
    {
      var process = Process.GetProcessesByName($"{System.Reflection.Assembly.GetEntryAssembly().GetName().Name}");
      if (process.Length > 1)
      {
        process[1].Kill();
      }
    }



    //Sự kiện
    public delegate void SendSendUpdateProducts(object sender);
    public event SendSendUpdateProducts OnSendSendUpdateProducts;

    public delegate void SendUpdateDataDone();
    public event SendUpdateDataDone OnSendUpdateDataDone;

    public delegate void SendDataRealTimeWeigherHome(double value, eValuate eValuate);
    public event SendDataRealTimeWeigherHome OnSendDataRealTimeWeigherHome;

    public delegate void SendDataRealTimeWeigherTare(double value);
    public event SendDataRealTimeWeigherTare OnSendDataRealTimeWeigherTare;

    public delegate void SendDataRealTimeWeigherNoApp(double value);
    public event SendDataRealTimeWeigherNoApp OnSendDataRealTimeWeigherNoApp;

    public delegate void SendRequestOffApp();
    public event SendRequestOffApp OnSendRequestOffApp;

    public delegate void SendDataReWeigher(double value);
    public event SendDataReWeigher OnSendDataReWeigher;

    public delegate void SendChangeLine(List<Production> productions);
    public event SendChangeLine OnSendChangeLine;

    public System.Timers.Timer _timerCheckTimerCheckConnect = new System.Timers.Timer();
    public System.Timers.Timer _timerRealTimeShift = new System.Timers.Timer();
    public System.Timers.Timer _timerRealTimeCheckChangeDay = new System.Timers.Timer();
    public System.Timers.Timer _timerSendRequestWeigher = new System.Timers.Timer();


    public eWeigherMode _eWeigherMode = eWeigherMode.Normal;
    public eWeigherMode _eWeigherModeLast = eWeigherMode.Normal;
    public string[] _listPortPC = new string[100];


    //public string ip_tcp_mettler = "192.168.2.100"; //Sachet //"147.167.40.239";

    public string ip_tcp_mettler = "147.167.40.239"; //GN
    public int port_tcp_mettler = 2305; //2305;

    private eModeCommunication eModeCommunication = eModeCommunication.TcpClient;

    public void Init()
    {
      InitDB();

      LoadDataLine().Wait();

      InformationDeviceDev();


      if (eModeCommunication == eModeCommunication.Serial)
      {
        InitSerialPort();
        //InitSerialPortV2();
      }
      else if (eModeCommunication == eModeCommunication.TcpClient)
      {
        InitTcpConnectivity();
      }


      InitEvent();
      //Init_RandomData();

      CreateFolderReport();

      LogErrorToFileLog("Start App");

      StartShowUI();
    }

    #region Log Exception
    public void LogErrorToFileLog(Exception ex)
    {
      new LogHelper().LogErrorToFileLog(ex);
    }
    public void LogErrorToFileLog(string content)
    {
      new LogHelper().LogErrorToFileLog(content);
    }
    #endregion

    public Image ByteArrayToImage(byte[] byteArray)
    {
      using (MemoryStream ms = new MemoryStream(byteArray))
      {
        return Image.FromStream(ms);
      }
    }

    public Bitmap ByteArrayToBitmap(byte[] byteArray)
    {
      using (MemoryStream ms = new MemoryStream(byteArray))
      {
        return new Bitmap(ms);
      }
    }

    //Current Data
    public InforLine _stationCurrent { get; set; } = new InforLine();
    public List<Production> _listProductsWithStation { get; set; } = new List<Production>();
    public Production _currentProduct { get; set; } = new Production();
    public eModeTare _modeTare { get; set; } = eModeTare.TareWithLabel;

    public ShiftLeader _listUserCurrent { get; set; } = new ShiftLeader();
    public InforValueSettingStation _inforValueSettingStation { get; set; } = new InforValueSettingStation();

    public eModeUseApp _modeUseApp = eModeUseApp.OFF;
    public eReadyReceidDataTare _readyReceidDataTare = eReadyReceidDataTare.No;
    public eReadyReceidWeigher _readyReceidDataWeigher = eReadyReceidWeigher.No;

    public Roles _roleCurrent = new Roles();


    public List<Production> _listAllProductsBelongLine { get; set; } = new List<Production>();
    public List<InforLine> _listInforLine { get; set; } = new List<InforLine>();
    public async Task LoadDataLine()
    {
      try
      {
        //Line
        _listInforLine = await LoadInforLines();

        //Load Shift Infor
        _listShiftType = await LoadShiftTypes();

        //Shift leader list
        _listShiftLeader = await LoadAllShiftLeader();

        

        //Role
        _listRoles = await LoadRoles();
        _roleCurrent = _listRoles?.Where(s => s.Name == "OP").FirstOrDefault();

        //Load Shift
        _listShift = await LoadShifts();

        ////Product
        //_listAllProductsContainIsDelete = await LoadAllProducts();

        //_listAllProductsBelongLine = _listAllProductsContainIsDelete?
        //            .Where(a => _listInforLine.Any(b => b.IsEnable && b.Code == a.LineCode))
        //            .Where(x=>x.IsDelete==false)
        //            .ToList();
        return;

        _shiftTypeCurrent = _listShiftType?.Where(x => x.isEnable == true).FirstOrDefault();

        

        //Load Role

        

        //Get Shift
        _shiftIdCurrent = _shiftIdLast = GetShiftCode();

        // Load Line Info

        _stationCurrent = _listInforLine?.Where(x => x.IsEnable == true).FirstOrDefault();


        //Tạo folder chưa report tự động
        //CreateFolderReportAuto(_stationCurrent.PathReportLocal);
        //CreateFolderReportAuto(_stationCurrent.PathReportOneDrive);

        //Load Infor Value Setting Line
        _inforValueSettingStation = await LoadValueSetting();

        //Load User

        //_listUsers = _listUsers?.Where(x => x.IsDelete == false).ToList();
        //_listUserCurrent = _listUsers?.Where(x => x.IsEnable == true && x.IsDelete == false).FirstOrDefault();

        //Load Product
        LoadProduct().Wait();

        //Load all Sample và Datalog
        DateTime dt_fileDB = GetDataTimeFileDB(DateTime.Now);
        _listDatalogs = await LoadAllDatalogs(dt_fileDB);
        _listDataSamples = await LoadAllSamples(dt_fileDB);

        if (_listDataSamples != null && _listDatalogs != null)
        {
          if (_listDataSamples.Count() > 0)
          {
            Datalog datalogLastOrDefault = _listDatalogs.LastOrDefault();
            groupId_DB = datalogLastOrDefault.GroupId;

            //Get list datalog
            _listDatalogs = _listDatalogs?.Where(x => x.GroupId == groupId_DB).ToList();
            _datalogsRowNew = _listDatalogs?.LastOrDefault();
            stt_Datalog_DB = _datalogsRowNew.No;

            //Load all sample sản phẩm hiện tại
            List<int> listGroupId = _listDatalogs?.Select(x => x.Id).ToList();
            _listDataSamples = await LoadAllSamplesByDatalogId(listGroupId, dt_fileDB);

            //Lấy block sample hiện tại
            _listSamplesCurrent = _listDataSamples?.Where(x => x.DatalogId == _datalogsRowNew.Id).ToList();
            datalogId_Sample_DB = _listSamplesCurrent.Select(x => x.DatalogId).FirstOrDefault();

            //Check qua ca
            if (_datalogsRowNew.ShiftId != _shiftIdCurrent)
            {
              CloseListSample();
              ++groupId_DB;
            }
          }
        }

        //Load Mode Tare 
        _modeTare = (_inforValueSettingStation.ModeTare == 1) ? eModeTare.TareWithLabel : eModeTare.TareNoLabel;
        _readyReceidDataWeigher = (_inforValueSettingStation.IsChangeProductNoTare == false) ?
                                  eReadyReceidWeigher.Yes :
                                  eReadyReceidWeigher.No;

        //Load list tare
        _listTares = await LoadAllTare(dt_fileDB);

        if (_listTares != null)
        {
          if (_listTares.Count > 0)
          {
            _groupIdCurrentTare = _listTares.Max(x => x.GroupId);
            _listTares = _listTares?.Where(x => x.GroupId == _groupIdCurrentTare).ToList();
          }
        }

        //Update lên Home
        if (_listTares != null)
          LoadInforTareHome();

        if (_listTares == null)
        {
          if (_listTares.Count <= 0)
          {
            _readyReceidDataTare = eReadyReceidDataTare.No;
          }
          else
          {
            _readyReceidDataTare = eReadyReceidDataTare.Yes;
          }
        }
        else
        {
          _readyReceidDataTare = eReadyReceidDataTare.No;
        }
      }
      catch (Exception ex)
      {
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
      }
    }










    /// <summary>
    /// ///////////////////////////////////////////////////////
    /// </summary>














    private void CreateFolderReport()
    {
      try
      {
        string pathLocal = AppCore.Ins._stationCurrent?.PathReportLocal;
        string pathOneDrive = AppCore.Ins._stationCurrent?.PathReportOneDrive;

        CheckCreateFolderIfExits(pathLocal);
        CheckCreateFolderIfExits(pathLocal + "\\Months");
        CheckCreateFolderIfExits(pathLocal + "\\Weeks");
        CheckCreateFolderIfExits(pathLocal + "\\Dailys");


        CheckCreateFolderIfExits(pathOneDrive);
        CheckCreateFolderIfExits(pathOneDrive + "\\Months");
        CheckCreateFolderIfExits(pathOneDrive + "\\Weeks");
        CheckCreateFolderIfExits(pathOneDrive + "\\Dailys");
      }
      catch (Exception ex)
      {
        LogErrorToFileLog(ex.ToString());
      }

    }

    private void CheckCreateFolderIfExits(string pathFolder)
    {
      if (!Directory.Exists(pathFolder))
      {
        Directory.CreateDirectory(pathFolder);
      }
    }

    public bool _isPermitDev = false;
    private void InformationDeviceDev()
    {
      string computerName = Environment.MachineName;
      _isPermitDev = (computerName == "DESKTOP-VIPBB6Q");
    }

    #region //Thư viện 1
    //Thư viện dọc COM
    private DataBits databits;
    private Parity parity;
    private StopBits stopBits;

    public SerialPortInput serialWeigher = new SerialPortLib.SerialPortInput();
    public async void InitSerialPort()
    {
      //try
      //{
      //  //Load Setting Weigher Connect
      //  _serialControllers = await LoadSerialControlSetting();
      //  if (_serialControllers != null)
      //  {
      //    string com = _serialControllers.COM;
      //    int baudrate = _serialControllers.Baud;
      //    databits = (DataBits)Enum.ToObject(typeof(DataBits), _serialControllers.Databits);
      //    parity = (Parity)Enum.ToObject(typeof(Parity), _serialControllers.Parity);
      //    stopBits = (StopBits)Enum.ToObject(typeof(StopBits), _serialControllers.Stopbits);

      //    //databits = DataBits.Eight;
      //    //parity = Parity.Odd;
      //    //stopBits = StopBits.One;

      //    serialWeigher.MessageReceived += Serial_MessageReceived;
      //    //serialWeigher.ConnectionStatusChanged += Serial_ConnectionStatusChanged;
      //    serialWeigher.SetPort(com, baudrate, dataBits: databits, parity: parity, stopBits: stopBits);
      //    serialWeigher.Connect();

      //    if (_stationCurrent != null)
      //    {
      //      if (serialWeigher.IsConnected && _stationCurrent.IsEnableRequestWeigher)
      //      {
      //        _timerSendRequestWeigher.Elapsed += _timerSendRequestWeigher_Elapsed;
      //        _timerSendRequestWeigher.Interval = _stationCurrent.TimeSendRequestWeigher;
      //        _timerSendRequestWeigher.Start();
      //      }
      //    }

      //  }
      //}
      //catch (Exception ex)
      //{
      //  LogErrorToFileLog("Kết nối Serial: " + ex.ToString());
      //}
    }

    private void Instance_OnSendTestSendSerial(string Str)
    {
      if (serialWeigher.IsConnected)
      {
        string data_send = $"{Str}\r\n";
        byte[] bytes = Encoding.ASCII.GetBytes(data_send);
        serialWeigher.SendMessage(bytes);
      }
    }



    private DateTime lastTimeReceided = DateTime.Now;
    private void Serial_MessageReceived(object sender, MessageReceivedEventArgs args)
    {
      TimeSpan tp = DateTime.Now.Subtract(lastTimeReceided);
      if (tp.TotalMilliseconds > 50)
      {
        //Ver1
        byte[] buffer = args.Data;
        string dataWeigher = ConvertRawDataFromWeigher(buffer, buffer.Length);

        if (dataWeigher.Trim() == "" || buffer.Length < 16) return;

        //FIlter Do buffer ko resset
        dataWeigher = FilterBuffer(dataWeigher);

        _current_weigher_value = CvtStrToDouble(dataWeigher);

        ReceivedData(_current_weigher_value);

        lastTimeReceided = DateTime.Now;
      }
    }

    #endregion


    #region //Thư viện 2
    public SerialPort serialPort = new SerialPort("COM1", 9600);
    public void InitSerialPortV2()
    {
      try
      {
        string com = _serialControllers.COM;
        int baudrate = _serialControllers.Baud;
        DataBits databits = (DataBits)Enum.ToObject(typeof(DataBits), _serialControllers.Databits + 5);
        Parity parity = (Parity)Enum.ToObject(typeof(Parity), _serialControllers.Parity);
        StopBits stopBits = (StopBits)Enum.ToObject(typeof(StopBits), _serialControllers.Stopbits);

        serialPort = new SerialPort(com, baudrate, Parity.Odd, 7, StopBits.One);
        //serialPort.DataReceived += SerialPort_DataReceived;
        serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
        serialPort.Open();
      }
      catch (Exception ex)
      {
        LogErrorToFileLog("Kết nối Serial: " + ex.ToString());
      }
    }

    private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
      //if (serialPort.IsOpen)
      //{
      //  TimeSpan tp = DateTime.Now.Subtract(_lastupdated);
      //  if (tp.TotalMilliseconds>100)
      //  {
      //    SerialPort sp = (SerialPort)sender;
      //    if (sp != null)
      //    {
      //      byte[] buffer = new byte[sp.BytesToRead];

      //      if (buffer.Length >= 10)
      //      {
      //        sp.Read(buffer, 0, buffer.Length);
      //        string dataWeigher = ConvertRawDataFromWeigher(buffer, buffer.Length);

      //        if (dataWeigher.Trim()!="" && dataWeigher.Trim() != "+" && dataWeigher.Trim() != "-" && dataWeigher.Trim() != ".")
      //        {
      //          //dataWeigher = "+31.53+31.53+31.53+3----dhh1511.53";
      //          //case 31.51+0
      //          //"+31.46+3"
      //          //"+31.51+31"

      //          //Check kí tự cuối
      //          bool c = char.IsDigit(dataWeigher[dataWeigher.Length - 1]);
      //          if (!c)
      //          {
      //            dataWeigher = dataWeigher.Substring(0, dataWeigher.Length - 2);
      //          }

      //          dataWeigher = FilterBuffer(dataWeigher);

      //          if (dataWeigher!="")
      //          {
      //            _current_weigher_value = CvtStrToDouble(dataWeigher);

      //            if (_current_weigher_value == -1 || _current_weigher_value<10)
      //            {
      //              string dataWeigherTest = ConvertRawDataFromWeigher(buffer, buffer.Length);

      //              bool cc = char.IsDigit(dataWeigherTest[dataWeigherTest.Length - 1]);
      //              if (!cc)
      //              {
      //                dataWeigherTest = dataWeigherTest.Substring(0, dataWeigherTest.Length - 2);
      //              }
      //              dataWeigherTest = FilterBuffer(dataWeigherTest);
      //            }

      //            if (_current_weigher_value >= 0)
      //            {
      //              FilterDataWeigher(_current_weigher_value);
      //            }

      //            DisplayValueWeigher(_current_weigher_value);
      //          }   
      //        }  
      //      }
      //    }

      //    _lastupdated = DateTime.Now;
      //  }   
      //}
    }

    #endregion


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
        LogErrorToFileLog("Kết nối InitTcpConnectivity: " + ex.ToString());
      }
    }

    private void CommonTCPClient_OnDataReceive(object sender, SuperSimpleTcp.DataReceivedEventArgs e)
    {
      try
      {
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
            var result = ParseDataWeight(dataWeigher);
            ReceivedData(result.Weight);
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
      if (tcp != null)
      {
        bool isConnectSerrial = tcp.IsConnected;
        //FrmHome.Instance.ConnectSerialWeigher(isConnectSerrial);
      }

    }

    private static (string Name, double Weight) ParseDataWeight(string input)
    {
      input = input.Trim('\u0002', '\u0003');

      var match = Regex.Match(input, @"(\d+\.\d+)g");

      if (match.Success)
      {
        string name = match.Groups[1].Value.Trim();

        double weight = 0;
        double.TryParse(name, out weight);

        return (name, weight);
      }

      throw new ArgumentException("Format Data Weight Fail");
    }



    #endregion

    private void _timerSendRequestWeigher_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        _timerSendRequestWeigher.Stop();

        //string data_send = "ESC P" + "\r\n";
        //byte[] byteArray = Encoding.UTF8.GetBytes(data_send);

        string data_send = "ESC P" + "\r\n";
        //byte[] byteArray = Encoding.UTF8.GetBytes(data_send);
        byte[] bytes = Encoding.ASCII.GetBytes(data_send);
        serialWeigher.SendMessage(bytes);
      }
      catch (Exception ex)
      {
        LogErrorToFileLog("Send Request Weigher: " + ex.ToString());
      }
      finally
      {
        _timerSendRequestWeigher.Start();
      }
    }



    private void _timerCheckTimerCheckConnect_Elapsed(object sender, ElapsedEventArgs e)
    {
      this._timerCheckTimerCheckConnect.Stop();
      try
      {
        if (eModeCommunication == eModeCommunication.Serial)
        {
          _listPortPC = SerialPort.GetPortNames();
          bool isConnectSerrial = (_listPortPC.Contains(_serialControllers.COM));
          //FrmHome.Instance.ConnectSerialWeigher(isConnectSerrial);
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
            }
          }
          catch (Exception ex)
          {
            LogErrorToFileLog("Kết nối InitTcpConnectivity: " + ex.ToString());
          }
        }
      }
      catch (Exception ex)
      {
        LogErrorToFileLog(ex.ToString());
      }
      finally
      {
        this._timerCheckTimerCheckConnect.Start();
      }
    }

    private double _current_weigher_value = 0;

    //Hàm khi nhận data rồi đưa lên DashBoard và xử lý đưa xuống DB
    private void ReceivedData(double dataWeigherReceid)
    {
      //Hiển thị
      DisplayValueWeigher(dataWeigherReceid);

      if (eModeCommunication == eModeCommunication.Serial)
      {
        FilterDataWeigher(dataWeigherReceid);
      }
      else
      {
        FilterDataWeigherTcp(dataWeigherReceid);
      }
    }



    private string FilterBuffer(string bufferStr)
    {
      //Ver 2
      string subString = "";
      List<int> dotPositions = new List<int>();
      if (bufferStr.Length > 0)
      {
        for (int i = 0; i < bufferStr.Length; i++)
        {
          if (bufferStr[i] == '.')
          {
            dotPositions.Add(i);
          }
        }
      }

      if (dotPositions.Count > 0)
      {
        int lastIndex = dotPositions.LastOrDefault();

        if (bufferStr[bufferStr.Length - 1] == '.')
          lastIndex = dotPositions[-2];

        if (lastIndex > 0)
        {
          int indexGetData = lastIndex - 1;

          for (int k = lastIndex - 1; k > 0; k--)
          {
            int cnt = 1;
            if (indexGetData >= 0)
            {
              if (char.IsDigit(bufferStr[indexGetData - cnt]))
              {
                indexGetData--;
              }
              else
              {
                break;
              }
            }
            cnt++;
          }

          if (indexGetData - 1 >= 0)
          {
            if (bufferStr[indexGetData - 1] == '-' || bufferStr[indexGetData - 1] == '+')
            {
              indexGetData--;
            }
          }

          // Lấy chuỗi con bắt đầu từ index 0 đến 
          subString = bufferStr.Substring((indexGetData));
          int indexFilter = subString.LastIndexOf('.');
          int of = 0;
          //Check các số sau
          for (int i = indexFilter + 1; i < subString.Length; i++)
          {
            if (char.IsDigit(subString[i]))
              of++;
            else
              break;
          }

          if ((indexFilter + of - 1) < subString.Length)
            subString = subString.Substring(0, indexFilter + 1 + of);
        }
      }
      return subString;
    }

    public void DeInitCommunication()
    {
      try
      {
        lock (serialWeigher)
        {
          if (eModeCommunication == eModeCommunication.Serial)
          {
            if (serialWeigher.IsConnected)
              serialWeigher.Disconnect();
          }
          else if (eModeCommunication == eModeCommunication.TcpClient)
          {
            //if (commonTCPClient.Connected)
            //  commonTCPClient.Disconnect();
          }
        }
      }
      catch (Exception ex)
      {
        LogErrorToFileLog("Kết nối Serial: " + ex.ToString());
      }
    }


    public void InitDB()
    {
      try
      {
        DataBase.Init().Wait();
      }
      catch (Exception ex)
      {
        AppCore.Ins.LogErrorToFileLog($"Lỗi khi khởi tạo chương trình, vui lòng khởi động lại!" + ex.ToString());
        System.Windows.Forms.MessageBox.Show($"Lỗi khi khởi tạo chương trình, vui lòng khởi động lại!", "Lỗi");
        Environment.Exit(2);
      }
    }



    //DB Data
    
    public SerialControllers _serialControllers { get; set; } = new SerialControllers();

    
    public List<Production> _listAllProductsContainIsDelete { get; set; } = new List<Production>();
    public List<HistoricalChangeMasterData> _listChangeProductsLogging { get; set; } = new List<HistoricalChangeMasterData>();


    public List<ShiftType> _listShiftType { get; set; } = new List<ShiftType>();
    public ShiftType _shiftTypeCurrent { get; set; } = new ShiftType();
    public List<Shift> _listShift { get; set; } = new List<Shift>();
    public List<Roles> _listRoles { get; set; } = new List<Roles>();
    public List<ShiftLeader> _listShiftLeader { get; set; } = new List<ShiftLeader>();


    public List<Sample> _listSamplesCurrent { get; set; } = new List<Sample>();
    public List<Sample> _listDataSamples { get; set; } = new List<Sample>();

    public List<Datalog> _listDatalogs { get; set; } = new List<Datalog>();
    public Datalog _datalogsRowNew { get; set; } = new Datalog();
    public int groupId_DB { get; set; } = 1;
    public int stt_Datalog_DB { get; set; } = 0;
    public int datalogId_Sample_DB { get; set; } = 0;


    public List<Tare> _listTares { get; set; } = new List<Tare>();
    public int _groupIdCurrentTare { get; set; } = new int();





    public DateTime GetDataTimeFileDB(DateTime dateTimeCurent)
    {
      if (dateTimeCurent.Hour >= 0 && dateTimeCurent.Hour < 6)
      {
        return dateTimeCurent.AddDays(-1);
      }
      return dateTimeCurent;
    }

    public void CreateFolderReportAuto(string folderReport)
    {
      if (folderReport == null) return;
      if (folderReport == "") return;
      string folderByMonths = folderReport + "\\Months";
      if (!Directory.Exists(folderByMonths))
      {
        Directory.CreateDirectory(folderByMonths);
      }
      string folderByWeeks = folderReport + "\\Weeks";
      if (!Directory.Exists(folderByWeeks))
      {
        Directory.CreateDirectory(folderByWeeks);
      }
      string folderByDailys = folderReport + "\\Dailys";
      if (!Directory.Exists(folderByDailys))
      {
        Directory.CreateDirectory(folderByDailys);
      }
    }

    public async Task ReloadShift()
    {
      _listShift = await LoadShifts();
    }


    private async Task LoadProduct()
    {
      _listAllProductsContainIsDelete = await LoadAllProducts();
      _listAllProductsBelongLine = _listAllProductsContainIsDelete?.Where(x => x.IsDelete == false).ToList();

      if (_stationCurrent != null && _listAllProductsBelongLine.Count > 0)
      {
        _listProductsWithStation = _listAllProductsBelongLine?.Where(x => x.LineCode == _stationCurrent.Name).ToList();
        if (_listProductsWithStation.Count > 0)
        {
          _currentProduct = _listProductsWithStation?.FirstOrDefault();
        }
      }
    }


    private void DisplayValueWeigher(double value)
    {
      if (_eWeigherMode == eWeigherMode.Normal)
      {
        eValuate eValuate = eValuate.PASS;
        if (_currentProduct == null)
        {
          eValuate = eValuate.UNKNOWN;
        }
        else
        {
          if (value < _currentProduct.LowerLimitFinal) eValuate = eValuate.FAIL;
          else if (value > _currentProduct.UpperLimitFinal) eValuate = eValuate.OVER;
        }

        OnSendDataRealTimeWeigherHome?.Invoke(value, eValuate);
      }
      else if (_eWeigherMode == eWeigherMode.Tare)
      {
        OnSendDataRealTimeWeigherTare?.Invoke(value);
      }
      else if (_eWeigherMode == eWeigherMode.NoApp)
      {
        OnSendDataRealTimeWeigherNoApp?.Invoke(value);
      }
    }

    private string ConvertRawDataFromWeigher(byte[] receive_buffer, int data_len)
    {
      string weigher_data = "";
      string tmp = "";
      for (int i = 0; i < data_len; i++)
      {
        tmp += receive_buffer[i].ToString("");
        char chr = Convert.ToChar(receive_buffer[i]);
        if (Char.IsDigit(chr) || (chr == '.') || (chr == '-') || (chr == '+'))
        {
          weigher_data += String.Format("{0}", chr);
        }

      }/*for (int i = 0; i < data_len; i++)*/
      return weigher_data;
    }
    public double CvtStrToDouble(string src)
    {
      double ret = -1;
      try
      {
        ret = (src != "") ? Convert.ToDouble(src) : ret;
      }
      catch
      {
      }
      return ret;
    }



    //Processing Data
    private double[] listDataWeighers = new double[1000];
    private int cnt = 0;
    private void FilterDataWeigher(double current_weigher_value)
    {
      try
      {
        if (_currentProduct == null) return;

        if (current_weigher_value > _currentProduct.LowerLimitFinal / 2 && current_weigher_value < _currentProduct.UpperLimitFinal * 2)
        {
          listDataWeighers[cnt] = current_weigher_value;
          cnt++;
          if (cnt >= 1000) cnt = 0;
        }
        else if (current_weigher_value <= 0.1)
        {
          // Sử dụng Dictionary để đếm tần suất xuất hiện của từng giá trị
          Dictionary<double, int> frequencyCounter = listDataWeighers.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());

          //foreach (double value in listDataWeighers)
          //{
          //  if (frequencyCounter.ContainsKey(value))
          //  {
          //    frequencyCounter[value]++;
          //  }
          //  else
          //  {
          //    frequencyCounter.Add(value, 1);
          //  }
          //}

          double valueFilter = 0;
          if (frequencyCounter.Count > 0)
          {
            valueFilter = frequencyCounter.Where(x => x.Key > 0.1).OrderByDescending(x => x.Value).First().Key;
          }

          //Save DB
          if (_eWeigherMode == eWeigherMode.Normal && _readyReceidDataWeigher == eReadyReceidWeigher.Yes)
          {
            SaveDataWeigher(valueFilter);
          }
          else if (_eWeigherMode == eWeigherMode.SampleRework)
          {
            OnSendDataReWeigher?.Invoke(valueFilter);
          }
          else if (_eWeigherMode == eWeigherMode.Tare)
          {
            //SaveDataTareWeigher(valueFilter);
          }

          cnt = 0;
          listDataWeighers = new double[1000];
          FrmMain.Instance.RefeshTimeOut();
        }
      }
      catch (Exception ex)
      {
        AppCore.Ins.LogErrorToFileLog("FilterDataWeigher>> " + ex.Message + "&&" + ex.StackTrace);
      }

    }

    private void FilterDataWeigherTcp(double current_weigher_value)
    {
      try
      {
        if (_currentProduct == null) return;

        //Save DB
        if (_eWeigherMode == eWeigherMode.Normal && _readyReceidDataWeigher == eReadyReceidWeigher.Yes)
        {
          SaveDataWeigher(current_weigher_value);
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
        AppCore.Ins.LogErrorToFileLog("FilterDataWeigher>> " + ex.Message + "&&" + ex.StackTrace);
      }

    }


    public Datalog datalogCurrent = new Datalog();

    private async void SaveDataWeigher(double value)
    {
      try
      {
        DateTime dt_fileDB = GetDataTimeFileDB(DateTime.Now);

        Sample sampleUpdate = _listSamplesCurrent?.Where(x => x.isHasValue == false && x.isEnable).FirstOrDefault();

        if (sampleUpdate != null)
        {
          sampleUpdate.Value = value;
          sampleUpdate.PreviouValue = value;
          sampleUpdate.isHasValue = true;
          sampleUpdate.UpdatedAt = DateTime.Now;

          //Update lại db
          await Update(sampleUpdate, dt_fileDB);
          OnSendUpdateDataDone?.Invoke();
        }
        else
        {
          ++stt_Datalog_DB;
          ++datalogId_Sample_DB;
          await CreateBlockSampleData();
          await CreateDatalog();
          SaveDataWeigher(value);
        }
      }
      catch (Exception ex)
      {
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
      }
    }


    public async Task ConfirmTareDone(List<Tare> dataTares)
    {
      //Save DB
      await AddRangeTare(dataTares);

      //Update DB Config
      if (_modeTare == eModeTare.TareWithLabel)
      {
        _inforValueSettingStation.Min = dataTares[0].ValueLower_withlabel;
        _inforValueSettingStation.Target = dataTares[0].ValueStandard_withlabel;
        _inforValueSettingStation.Max = dataTares[0].ValueUpper_withlabel;
      }
      else
      {
        _inforValueSettingStation.Min = dataTares[0].ValueLower_nolabel;
        _inforValueSettingStation.Target = dataTares[0].ValueStandard_nolabel;
        _inforValueSettingStation.Max = dataTares[0].ValueUpper_nolabel;
      }

      _inforValueSettingStation.IsChangeProductNoTare = false;
      _inforValueSettingStation.ModeTare = (int)_modeTare;
      _inforValueSettingStation.ValueAvgTare = dataTares[0].ValueAvg;
      _inforValueSettingStation.UpdatedAt = DateTime.Now;
      await UpdateValueSetting(_inforValueSettingStation);

      //Update Home
      LoadInforTareHome();
      //FrmHome.Instance.ChangeProduct(false);
      FrmMain.Instance.UpdateInforModeTare(_inforValueSettingStation.ModeTare);
      _groupIdCurrentTare++;
    }

    private void LoadInforTareHome()
    {
      if (_inforValueSettingStation.ValueAvgTare != 0)
      {
        //FrmHome.Instance.UpdateValueTareUI(_inforValueSettingStation.ValueAvgTare,
        //                                _inforValueSettingStation.Target,
        //                                _inforValueSettingStation.Max,
        //                                _inforValueSettingStation.Min,
        //                                _inforValueSettingStation.ModeTare,
        //                                (DateTime)_inforValueSettingStation.UpdatedAt);

        FrmMain.Instance.UpdateInforModeTare(_inforValueSettingStation.ModeTare);
      }
    }


    public double Stdev(List<double> data)
    {
      if (data.Count > 0)
      {
        double mean = data.Average();
        double sumOfSquares = data.Sum(x => Math.Pow(x - mean, 2));
        double variance = (data.Count() > 1) ? sumOfSquares / (data.Count() - 1) : 1;
        return Math.Round(Math.Sqrt(variance), 3);
      }
      return 0;
    }


    private async Task CreateDatalog()
    {
      try
      {
        //Load all Sample và Datalog
        DateTime dt_fileDB = GetDataTimeFileDB(DateTime.Now);

        var user = _listShiftLeader?.Where(x => x.IsEnable == true).FirstOrDefault();
        int userId = (user != null) ? user.Id : -1;
        Datalog datalog = new Datalog()
        {
          StationId = (_stationCurrent != null) ? _stationCurrent.Id : -1,
          GroupId = groupId_DB,
          ShiftId = _shiftIdCurrent,
          No = stt_Datalog_DB,
          TareValue = _inforValueSettingStation.ValueAvgTare,
          IsTareWithLabel = (eModeTare)Enum.ToObject(typeof(eModeTare), _inforValueSettingStation.ModeTare),
          UserId = userId,
          ProductId = _currentProduct.Id,
          CreatedAt = DateTime.Now,
          UpdatedAt = DateTime.Now,
        };

        await AddDatalog(datalog, dt_fileDB);
        datalogCurrent = datalog;
      }
      catch (Exception ex)
      {
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
      }
    }

    private async Task CreateBlockSampleData()
    {
      try
      {
        //Load all Sample và Datalog
        DateTime dt_fileDB = DateTime.Now;
        if (dt_fileDB.Hour >= 0 && dt_fileDB.Hour < 6)
        {
          dt_fileDB = DateTime.Now.AddDays(-1);
        }

        List<Sample> samples = new List<Sample>();
        int sampleNumberMax = 15;
        int sampleNumber = 15;

        //Yêu cầu khách hàng full tất cả, ko cần check số vòi fill
        //if (_currentProduct != null)
        //{
        //  if (_currentProduct.FillterMax > 0)
        //  {
        //    sampleNumber = _currentProduct.FillterMax;
        //  }  
        //}  

        for (int i = 1; i <= sampleNumberMax; i++)
        {
          Sample sample = new Sample();
          if (i <= sampleNumber)
          {
            sample = new Sample()
            {
              DatalogId = datalogId_Sample_DB,
              GroupId = groupId_DB,
              LocalId = i,
              Value = 0,
              PreviouValue = 0,
              isHasValue = false,
              isEnable = true,
              isEdited = false,
              CreatedAt = DateTime.Now,
              UpdatedAt = DateTime.Now,
            };
          }
          else
          {
            sample = new Sample()
            {
              DatalogId = datalogId_Sample_DB,
              GroupId = groupId_DB,
              LocalId = i,
              Value = 0,
              PreviouValue = 0,
              isHasValue = false,
              isEnable = false,
              isEdited = false,
              CreatedAt = DateTime.Now,
              UpdatedAt = DateTime.Now,
            };
          }
          samples.Add(sample);
        }

        await AddSample(samples, dt_fileDB);
        _listSamplesCurrent = samples;
      }
      catch (Exception ex)
      {
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
      }
    }


    public int GetShiftCode()
    {
      if (_listShiftType.Count == 0 || _listShift.Count == 0)
      {
        return -1;
      }
      if (_shiftTypeCurrent != null)
      {
        return GetNameShift(_shiftTypeCurrent.Code);
      }
      return -1;
    }

    private int GetNameShift(int codeShiftType)
    {
      List<Shift> listShift = _listShift?.Where(x => x.ShiftTypeId == codeShiftType).ToList();
      foreach (var item in listShift)
      {
        TimeSpan startTime = new TimeSpan(item.StartHour, item.StartMinute, item.StartSecond);
        TimeSpan endTime = new TimeSpan(item.EndHour, item.EndMinute, item.EndSecond);
        TimeSpan datatimeCurrent = DateTime.Now.TimeOfDay;


        if (startTime < endTime)
        {
          if (datatimeCurrent >= startTime && datatimeCurrent <= endTime)
          {
            return item.CodeShift;
          }
        }
        else
        {
          TimeSpan endDay = new TimeSpan(23, 59, 59);
          TimeSpan startDay = new TimeSpan(0, 0, 0);
          if ((datatimeCurrent >= startTime && datatimeCurrent <= endDay) || (datatimeCurrent >= startDay && datatimeCurrent <= endTime))
          {
            return item.CodeShift;
          }
        }
      }
      return -1;
    }



    public void InitEvent()
    {
      try
      {
        FrmMain.Instance.OnSendChooseProduct += Instance_OnSendChooseProduct;
        FrmSettingDevice.Instance.OnSendTestSendSerial += Instance_OnSendTestSendSerial;

        _timerRealTimeShift.Interval = 1000;
        _timerRealTimeShift.Elapsed += _timerRealTimeShift_Elapsed;
        _timerRealTimeShift.Start();

        _timerRealTimeCheckChangeDay.Interval = 1000;
        _timerRealTimeCheckChangeDay.Elapsed += _timerRealTimeCheckChangeDay_Elapsed;
        _timerRealTimeCheckChangeDay.Start();

        _timerCheckTimerCheckConnect.Interval = 1000;
        _timerCheckTimerCheckConnect.Elapsed += _timerCheckTimerCheckConnect_Elapsed;
        _timerCheckTimerCheckConnect.Start();
      }
      catch (Exception ex)
      {
        LogErrorToFileLog(ex.ToString());
      }

    }


    public int _shiftIdCurrent = -1;
    private int _shiftIdLast = -1;
    private void _timerRealTimeShift_Elapsed(object sender, ElapsedEventArgs e)
    {
      _timerRealTimeShift.Stop();
      try
      {
        _shiftIdCurrent = GetShiftCode();
        string nameShift = (_shiftIdCurrent != -1) ? _listShift?.Where(x => x.CodeShift == _shiftIdCurrent).Select(x => x.Name).FirstOrDefault() : "";
        FrmMain.Instance.UpdateShiftUI(nameShift);

        //Check Qua ca
        if (_shiftIdCurrent != _shiftIdLast && _shiftIdCurrent != -1)
        {
          CloseListSample();
          stt_Datalog_DB = 0;
          ++groupId_DB;

          _shiftIdLast = _shiftIdCurrent;

          //FrmHome.Instance.numberChangeDataSampleForShift = 0;
        }
      }
      catch (Exception ex)
      {
        LogErrorToFileLog(ex.ToString());
      }
      finally { _timerRealTimeShift.Start(); }
    }

    private void ReportAutoDailys()
    {
      DateTime dt = DateTime.Now.AddDays(-1);
      FrmReportAutoExcel frmReportAutoExcel = new FrmReportAutoExcel(dt);
      frmReportAutoExcel.ShowDialog();
    }


    private bool isStartApp = true;
    private int dayCurrent = 0;
    private int dayLast = 0;
    private void _timerRealTimeCheckChangeDay_Elapsed(object sender, ElapsedEventArgs e)
    {
      _timerRealTimeCheckChangeDay.Stop();
      try
      {
        DateTime dt = DateTime.Now.AddHours(-6);
        if (!isStartApp)
        {
          dayCurrent = dt.Day;
          if (dayLast != dayCurrent)
          {
            //_datalogId_Sample = 0;
            groupId_DB = 1;
            stt_Datalog_DB = 0;
            datalogId_Sample_DB = 0;

            //Check Xuất report auto theo tháng, tuần
            ExportAutoMonthOrWeek();

            //Report Auto Dailys
            ReportAutoDailys();

            //FrmHome.Instance.ClearHome();
            dayLast = dayCurrent;
          }
        }
        else
        {
          dayCurrent = dayLast = dt.Day;
          isStartApp = false;
        }
      }
      catch (Exception ex)
      {
        LogErrorToFileLog(ex.ToString());
      }
      finally { _timerRealTimeCheckChangeDay.Start(); }
    }

    public void ExportAutoMonthOrWeek()
    {
      try
      {
        DateTime dt = DateTime.Now;
        int year = dt.Year;
        int month = dt.Month;
        CultureInfo cul = CultureInfo.CurrentCulture;
        int weekCurrent = cul.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        string pathLocal = AppCore.Ins._stationCurrent.PathReportLocal;
        string pathOneDrive = AppCore.Ins._stationCurrent.PathReportOneDrive;
        string nameStation = AppCore.Ins._stationCurrent.Name;
        FrmReportAutoPdf frmReportAutoPdfMonth = new FrmReportAutoPdf(year, month - 1, weekCurrent - 1, pathLocal, pathOneDrive, nameStation);
        frmReportAutoPdfMonth.ShowDialog();
      }
      catch (Exception ex)
      {
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
      }
      finally
      {

      }
    }

    public async void OnSendConfirmChangeLine(int stationID)
    {
      for (int i = 0; i < _listInforLine.Count(); i++)
      {
        _listInforLine[i].IsEnable = (_listInforLine[i].Id == stationID);
      }
      await UpdateStation(_listInforLine);
      _stationCurrent = _listInforLine.Where(x => x.IsEnable == true).FirstOrDefault();

      _listProductsWithStation = _listAllProductsBelongLine?.Where(x => x.LineCode == _stationCurrent.Name).ToList();
      OnSendChangeLine?.Invoke(_listProductsWithStation);
    }


    private void Instance_OnSendChooseProduct(Production production)
    {
      CloseListSample();
      stt_Datalog_DB = 0;
      groupId_DB++;
    }

    private async void CloseListSample()
    {
      //Bật cờ các sample chưa fill data
      if (_listSamplesCurrent.Count > 0)
      {
        for (int i = 0; i < _listSamplesCurrent.Count; i++)
        {
          if (!_listSamplesCurrent[i].isHasValue)
            _listSamplesCurrent[i].isEnable = false;
        }
      }

      await UpdateRangeSample(_listSamplesCurrent);
    }

    public void SendTimeout()
    {
      OnSendRequestOffApp?.Invoke();
    }


    public bool CheckRole(ePermit permit)
    {
      if (AppCore.Ins._isPermitDev) return true;
      string roleStr = _roleCurrent.Permission;
      if (!string.IsNullOrEmpty(roleStr))
      {
        return roleStr.Contains(permit.ToString()) || AppCore.Ins._roleCurrent?.Name == "iSOFT";
      }
      return false;
    }

    public void UpdateAccountCurrent(List<Roles> roles)
    {
      if (_roleCurrent.Name != "iSOFT")
      {
        _roleCurrent = roles?.Where(x => x.Name == _roleCurrent.Name).FirstOrDefault();
      }
    }


    //DB
    

    public async Task AddProduct(Production productions)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<Production, ConfigDBContext>(context);
        await repo.Add(productions);
      }

      //ReLoad Product
      LoadProduct().Wait();
      OnSendSendUpdateProducts?.Invoke(this);
    }

    public async Task UpdateProduct(Production productions)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<Production, ConfigDBContext>(context);
        await repo.Update(productions);
      }

      //ReLoad Product
      LoadProduct().Wait();
      OnSendSendUpdateProducts?.Invoke(this);
    }



    public async Task<List<HistoricalChangeMasterData>> LoadHistoricalChangeMasterData()
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryHistoricalChangeMasterData(context);
        return await repo.GetAllAsync();
      }
    }


    //Load Infor Setting Value
    public async Task<InforValueSettingStation> LoadValueSetting()
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryInfoValueSettingStation(context);
        return await repo.GetAsync();
      }
    }

    public async Task UpdateValueSetting(InforValueSettingStation inforValueSettingStation)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryInfoValueSettingStation(context);
        await repo.Update(inforValueSettingStation);
      }
    }

   
    public async Task UpdateStation(List<InforLine> stations)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryInforLines(context);
        await repo.UpdateRange(stations);
      }
    }


    public async Task<List<Production>> LoadAllProducts()
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryProducts(context);
        return await repo.GetAllAsync();
      }
    }

   

    public async Task<SerialControllers> LoadSerialControlSetting()
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitorySerialControllers(context);
        return await repo.GetAsync();
      }
    }


    public async Task<List<ShiftType>> LoadShiftTypes()
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryShiftTypes(context);
        return await repo.GetAllAsync();
      }
    }

    public async Task<List<Shift>> LoadShifts()
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryShifts(context);
        return await repo.GetAllAsync();
      }
    }

    public async Task<List<Roles>> LoadRoles()
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryRoles(context);
        return await repo.GetAllAsync();
      }
    }

    public async Task UpdateRole(Roles role)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<Roles, ConfigDBContext>(context);
        await repo.Update(role);
      }
    }

    public async Task UpdateRangeProduct(List<Production> productions)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<Production, ConfigDBContext>(context);
        await repo.UpdateRange(productions);
      }
    }

    //role
    public async Task UpdateRange(List<Roles> roles)
    {
      using (var context = new ConfigDBContext())
      {
        GenericRepository<Roles, ConfigDBContext> repo = new ResponsitoryRoles(context);
        await repo.UpdateRange(roles);
      }
    }

    //Shift Type
    public async Task UpdateShiftTypeChoose(List<ShiftType> shiftTypes)
    {
      using (var context = new ConfigDBContext())
      {
        GenericRepository<ShiftType, ConfigDBContext> repo = new ResponsitoryShiftTypes(context);
        await repo.UpdateShiftTypesChoose(shiftTypes);
      }
    }

    //Shift
    public async Task UpdateInforShift(Shift shift)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryShifts(context);
        await repo.Update(shift);
      }
    }


    //User
    public async Task AddUser(ShiftLeader user)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<ShiftLeader, ConfigDBContext>(context);
        await repo.Add(user);
      }
    }
    public async Task AddRangeUsers(List<ShiftLeader> users)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<ShiftLeader, ConfigDBContext>(context);
        await repo.AddRange(users);
      }
    }
    public async Task UpdateRangeUsers(List<ShiftLeader> users)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<ShiftLeader, ConfigDBContext>(context);
        await repo.UpdateRange(users);
      }
    }
    public async Task<List<ShiftLeader>> LoadAllShiftLeader()
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<ShiftLeader, ConfigDBContext>(context);
        return await repo.GetAllAsync();
      }
    }

    public async Task UpdateProductChoose(List<ShiftLeader> users)
    {
      using (var context = new ConfigDBContext())
      {
        GenericRepository<ShiftLeader, ConfigDBContext> repo = new ResponsitoryUser(context);
        await repo.UpdateUserChoose(users);
      }
    }

    //User
    public async Task UpdateUser(ShiftLeader user)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<ShiftLeader, ConfigDBContext>(context);
        await repo.Update(user);
      }
    }
    public async Task RemoveUser(ShiftLeader user)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<ShiftLeader, ConfigDBContext>(context);
        await repo.Remove(user);
      }
    }



    public async Task AddSample(List<Sample> samples, DateTime dt)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<Sample, ConfigDBContext>(context);
        await repo.AddRange(samples);
      }
    }

    public async Task<List<Sample>> LoadAllSamples(DateTime dt)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<Sample, ConfigDBContext>(context);
        var data = await repo.GetAllAsync();
        return data?.Where(x => x.isHasValue == true && x.isEnable == true).ToList();
      }
    }
    public async Task<List<Sample>> LoadAllSamplesContainZero(DateTime dt)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<Sample, ConfigDBContext>(context);
        return await repo.GetAllAsync();
      }
    }

    public async Task<List<Sample>> LoadAllSamplesByDatalogId(List<int> listIdDatalog, DateTime dt)
    {

      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitorySamples(context);
        return await repo.LoadAllSamplesByDatalogId(listIdDatalog);
      }
    }


    public async Task Update(Sample sample, DateTime dt)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitorySamples(context);
        await repo.Update(sample);
      }
    }

    public async Task UpdateRangeSample(List<Sample> sample)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitorySamples(context);
        await repo.UpdateRange(sample);
      }
    }




    //Datalog
    public async Task<List<Datalog>> LoadAllDatalogs(DateTime dt)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<Datalog, ConfigDBContext>(context);
        return await repo.GetAllAsync();
      }
    }


    public async Task<List<Datalog>> LoadAllDatalogsByProductId(int productId, DateTime dt)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryDatalog(context);
        return await repo.LoadAllDatalogsByProductId(productId);
      }
    }

    public async Task AddDatalog(Datalog datalog, DateTime dt)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<Datalog, ConfigDBContext>(context);
        await repo.Add(datalog);
      }
    }



    //Tare
    public async Task<List<Tare>> LoadAllTare(DateTime dt)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<Tare, ConfigDBContext>(context);
        return await repo.GetAllAsync();
      }
    }

    public async Task AddRangeTare(List<Tare> tares)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<Tare, ConfigDBContext>(context);
        await repo.AddRange(tares);
      }
    }

    //Nội dung Change MasterData

    public async Task SaveContentChangeMasterData(string content)
    {
      HistoricalChangeMasterData data = new HistoricalChangeMasterData();
      data.Reason = content;
      data.CreatedAt = DateTime.Now;
      data.UpdatedAt = DateTime.Now;
      data.UpdateBy = _roleCurrent.Id;

      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<HistoricalChangeMasterData, ConfigDBContext>(context);
        await repo.Add(data);
      }
    }



    public async Task UpdateMasterDataOld(List<Production> data)
    {
      using (var context = new ConfigDBContext())
      {
        GenericRepository<Production, ConfigDBContext> repo = new ResponsitoryProducts(context);
        await repo.UpdateRangeByIsDelete(data);
      }
    }


    //SerialControl
    public async Task UpdateSerial(SerialControllers serialControllers)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitorySerialControllers(context);
        await repo.Update(serialControllers);
      }
    }


    //Excel Report
    public async Task<List<Datalog>> GetDatalogReport(DateTime dt)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryDatalog(context);
        return await repo.GetAllAsync();
      }
    }
    public async Task<List<Sample>> GetSampleReport(DateTime dt)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitorySamples(context);
        return await repo.GetAllAsync();
      }
    }


    public List<Sample> GetDataSampleByListIdDatalog(List<Sample> listSamples, List<int> listIdDatalogs)
    {
      List<Sample> rs = new List<Sample>();

      foreach (var id in listIdDatalogs)
      {
        List<Sample> samples = listSamples?.Where(x => x.DatalogId == id).ToList();
        rs.AddRange(samples);
      }
      return rs;
    }

    public void SetColor(IXLWorksheet worksheet, double value, double min, double max, string location, bool isReweigher = false)
    {
      if (value != 0)
      {
        worksheet.Cell(location).Value = value;
        if (isReweigher)
        {
          worksheet.Cell(location).Style.Fill.BackgroundColor = XLColor.DarkViolet;
          worksheet.Cell(location).Style.Font.FontColor = XLColor.White;
          return;
        }
        if (value > max)
          worksheet.Cell(location).Style.Fill.BackgroundColor = XLColor.Orange;
        else if (value < min)
          worksheet.Cell(location).Style.Fill.BackgroundColor = XLColor.Red;
      }
      else
      {
        worksheet.Cell(location).Style.Fill.BackgroundColor = XLColor.DarkGray;
      }
    }

    public void SetColor(IXLWorksheet worksheet, Sample sample, string location)
    {
      if (sample != null)
      {
        if (sample.isEdited)
        {
          worksheet.Cell(location).Value = sample.PreviouValue;
        }
      }
    }


    public List<DateTime> GetAllDaysInMonth(int year, int month)
    {
      List<DateTime> listOfDaysInMonth = new List<DateTime>();
      int daysInMonth = DateTime.DaysInMonth(year, month);
      for (int day = 1; day <= daysInMonth; day++)
      {
        listOfDaysInMonth.Add(new DateTime(year, month, day));
      }
      return listOfDaysInMonth;
    }

    public Dictionary<int, List<DateTime>> GetDaysInWeeks(int year)
    {
      Dictionary<int, List<DateTime>> daysInWeeks = new Dictionary<int, List<DateTime>>();

      // Tính ngày đầu tiên của năm
      DateTime firstDayOfYear = new DateTime(year, 1, 1);
      // Tính ngày cuối cùng của năm
      DateTime lastDayOfYear = new DateTime(year, 12, 31);

      int week = 1; // Khởi tạo số tuần ban đầu
      List<DateTime> daysInWeek = new List<DateTime>(); // Danh sách ngày trong tuần
                                                        // Lặp qua từ ngày đầu tiên đến ngày cuối cùng của năm
      for (DateTime date = firstDayOfYear; date <= lastDayOfYear; date = date.AddDays(1))
      {
        // Kiểm tra nếu ngày hiện tại là ngày đầu tiên của một tuần mới
        if (date.DayOfWeek == DayOfWeek.Monday)
        {
          if (daysInWeek.Count > 0)
          {
            daysInWeeks.Add(week, daysInWeek); // Thêm danh sách ngày trong tuần vào Dictionary
            week++; // Tăng số tuần lên 1
            daysInWeek = new List<DateTime>(); // Tạo danh sách mới cho tuần tiếp theo
          }
        }
        daysInWeek.Add(date); // Thêm ngày vào danh sách của tuần
      }

      // Thêm danh sách ngày trong tuần cuối cùng của năm
      if (daysInWeek.Count > 0)
      {
        daysInWeeks.Add(week, daysInWeek);
      }
      return daysInWeeks;
    }




  }
}
