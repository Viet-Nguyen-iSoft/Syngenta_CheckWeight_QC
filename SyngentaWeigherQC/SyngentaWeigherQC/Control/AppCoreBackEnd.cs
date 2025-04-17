using ClosedXML.Excel;
using SyngentaWeigherQC.DTO;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.Responsitory;
using SyngentaWeigherQC.UI.FrmUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using static SyngentaWeigherQC.eNum.eUI;
using DateTime = System.DateTime;
using Image = System.Drawing.Image;
using Production = SyngentaWeigherQC.Models.Production;

namespace SyngentaWeigherQC.Control
{
  public partial class AppCore
  {
    #region 
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

    #endregion

    //Sự kiện
    #region Event
    public delegate void SendWarning(InforLine inforLine, string content, eMsgType eMsgType);
    public event SendWarning OnSendWarning;

    public delegate void SendValueDatalogWeight(InforLine inforLine, DatalogWeight datalogWeight);
    public event SendValueDatalogWeight OnSendValueDatalogWeight;

    public delegate void SendValueTare(double value);
    public event SendValueTare OnSendValueTare;

    #endregion



    public delegate void SendSendUpdateProducts(object sender);
    public event SendSendUpdateProducts OnSendSendUpdateProducts;

    public delegate void SendUpdateDataDone();
    public event SendUpdateDataDone OnSendUpdateDataDone;

    public delegate void SendDataRealTimeWeigherHome(double value, eEvaluateStatus eValuate);
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


    public System.Timers.Timer _timerRealTimeShift = new System.Timers.Timer();
    public System.Timers.Timer _timerRealTimeCheckChangeDay = new System.Timers.Timer();
    public System.Timers.Timer _timerSendRequestWeigher = new System.Timers.Timer();



    public eStatusModeWeight eStatusModeWeight = eStatusModeWeight.OverView;
    public InforLine inforLineOperation;


    public eWeigherMode _eWeigherMode = eWeigherMode.Normal;
    public eWeigherMode _eWeigherModeLast = eWeigherMode.Normal;
    public string[] _listPortPC = new string[100];



    //public string ip_tcp_mettler = "192.168.2.100"; //Sachet //"147.167.40.239";

    //public string ip_tcp_mettler = "147.167.40.239"; //GN
    //public int port_tcp_mettler = 2305; //2305;

    public string ip_tcp_mettler = "127.0.0.1"; //GN
    public int port_tcp_mettler = 8080; //2305;

    private eModeCommunication eModeCommunication = eModeCommunication.TcpClient;

    public int _numberDataEachRow = 10;

    public void Init()
    {
      InitDB();

      LoadDataLine().Wait();

      InformationDeviceDev();

      if (eModeCommunication == eModeCommunication.Serial)
      {

      }
      else if (eModeCommunication == eModeCommunication.TcpClient)
      {
        InitTcpConnectivity();
      }



      InitEvent();
      //Init_RandomData();

      CreateFolderReport();

      eLoggerHelper.LogErrorToFileLog("Start App");

      StartShowUI();
    }



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
    public List<DatalogWeight> _listDatalogWeight { get; set; } = new List<DatalogWeight>();
    public async Task LoadDataLine()
    {
      try
      {
        //Line
        _listInforLine = await LoadInforLines();

        //Load Shift Infor
        _listShiftType = await LoadShiftTypes();
        //Load Shift
        _listShift = await LoadShifts();

        //Shift leader list
        _listShiftLeader = await LoadAllShiftLeader();

        //Load Product
        _listAllProductsBelongLine = await LoadAllProducts();

        // Datalog 
        _listDatalogWeight = await LoadAllDatalogWeight();

        //Role
        //_listRoles = await LoadRoles();
        _roleCurrent = _listRoles?.Where(s => s.Name == "OP").FirstOrDefault();

        


        //Get Shift Currrent
        //_shiftIdCurrent = _shiftIdLast = GetShiftCode();
      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex);
      }
    }

    public List<TableDatalogDTO> ConvertToDTOList(List<DatalogWeight> dataList)
    {
      var result = new List<TableDatalogDTO>();

      if (dataList == null || dataList.Count == 0)
        return result;

      int groupSize = 10;
      int totalGroups = (int)Math.Ceiling((double)dataList.Count / groupSize);


      int productId = (int)dataList.FirstOrDefault().ProductionId;
      Production production = _listAllProductsBelongLine?.Where(x => x.Id == productId).FirstOrDefault();

      for (int i = 0; i < totalGroups; i++)
      {
        var group = dataList
            .Skip(i * groupSize)
            .Take(groupSize)
            .ToList();

        if (group.Count == 0) continue;

        // Tạo Samples dạng Dictionary<Id, Value>
        var samples = group.ToDictionary(item => item.Id, item => item.Value);

        var dto = new TableDatalogDTO
        {
          No = i + 1,
          Shift = "Shift " + group.First().ShiftId, // hoặc group.First().Production?.Shift?.Name
          DateTime = group.First().CreatedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? "",
          Samples = samples,
          AvgRaw = Math.Round(group.Average(x => x.Value), 2),
          AvgTotal = Math.Round(dataList.Average(x => x.Value), 2),
          eEvaluate = EvaluateData(Math.Round(group.Average(x => x.Value), 2), production)
        };

        result.Add(dto);
      }

      return result;
    }

    public eEvaluate EvaluateData(double avgRaw, Production production)
    {
      return (avgRaw >= production.StandardFinal && avgRaw <= production.UpperLimitFinal) ?
        eEvaluate.Pass :
        eEvaluate.Fail;
    }

    public Tuple< Color, Color> EvaluateRetureColor(double value, Production production)
    {
      if (value > production.UpperLimitFinal)
      {
        return new Tuple<Color, Color>(Color.DarkOrange, Color.White);
      }
      else if (value < production.LowerLimitFinal)
      {
        return new Tuple<Color, Color>(Color.Red, Color.White);
      }
      return new Tuple<Color, Color>(Color.White, Color.Black);
    }

    public eEvaluateStatus EvaluateRetureStatus(double value, Production production)
    {
      if (value > production.UpperLimitFinal)
      {
        return eEvaluateStatus.OVER;
      }
      else if (value < production.LowerLimitFinal)
      {
        return eEvaluateStatus.FAIL;
      }
      return eEvaluateStatus.PASS; ;
    }

    public int GetShiftCode()
    {
      if (_listShiftType.Count == 0 || _listShift.Count == 0)
      {
        return -1;
      }

      if (inforLineOperation==null)
      {
        return -1;
      }
      if (inforLineOperation.ShiftTypes == null)
      {
        return -1;
      }
      if (inforLineOperation.ShiftTypes?.Code == null)
      {
        return -1;
      }

      if (inforLineOperation.ShiftTypes.Code > 0)
      {
        return GetNameShift(inforLineOperation.ShiftTypes.Code);
      }
      return -1;
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
        eLoggerHelper.LogErrorToFileLog(ex.ToString());
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










    private double _current_weigher_value = 0;

    //Hàm khi nhận data rồi đưa lên DashBoard và xử lý đưa xuống DB
    private void ReceivedData(double dataWeigherReceid)
    {
      //Hiển thị
      DisplayValueWeigher(dataWeigherReceid);

      if (eModeCommunication == eModeCommunication.Serial)
      {
        //FilterDataWeigher(dataWeigherReceid);
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




    public void InitDB()
    {
      try
      {
        DataBase.Init().Wait();
      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog($"Lỗi khi khởi tạo chương trình, vui lòng khởi động lại!" + ex.ToString());
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

    public List<DatalogWeight> _listDatalogs { get; set; } = new List<DatalogWeight>();
    public DatalogWeight _datalogsRowNew { get; set; } = new DatalogWeight();
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
        eEvaluateStatus eValuate = eEvaluateStatus.PASS;
        if (_currentProduct == null)
        {
          eValuate = eEvaluateStatus.UNKNOWN;
        }
        else
        {
          if (value < _currentProduct.LowerLimitFinal) eValuate = eEvaluateStatus.FAIL;
          else if (value > _currentProduct.UpperLimitFinal) eValuate = eEvaluateStatus.OVER;
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








    public DatalogWeight datalogCurrent = new DatalogWeight();




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
        eLoggerHelper.LogErrorToFileLog(ex.ToString());
      }
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
        _timerRealTimeShift.Interval = 1000;
        _timerRealTimeShift.Elapsed += _timerRealTimeShift_Elapsed;
        _timerRealTimeShift.Start();

        _timerRealTimeCheckChangeDay.Interval = 1000;
        _timerRealTimeCheckChangeDay.Elapsed += _timerRealTimeCheckChangeDay_Elapsed;
        _timerRealTimeCheckChangeDay.Start();


      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex.ToString());
      }
    }


    public int _shiftIdCurrent = -1;
    private int _shiftIdLast = -1;
    private void _timerRealTimeShift_Elapsed(object sender, ElapsedEventArgs e)
    {
      _timerRealTimeShift.Stop();
      try
      {
        //_shiftIdCurrent = GetShiftCode();
        //string nameShift = (_shiftIdCurrent != -1) ? _listShift?.Where(x => x.CodeShift == _shiftIdCurrent).Select(x => x.Name).FirstOrDefault() : "";
        //FrmMain.Instance.UpdateShiftUI(nameShift);

      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex.ToString());
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
        eLoggerHelper.LogErrorToFileLog(ex.ToString());
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
        eLoggerHelper.LogErrorToFileLog(ex);
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
    public async Task<List<DatalogWeight>> LoadAllDatalogs(DateTime dt)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<DatalogWeight, ConfigDBContext>(context);
        return await repo.GetAllAsync();
      }
    }


    public async Task<List<DatalogWeight>> LoadAllDatalogsByProductId(int productId, DateTime dt)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryDatalogWeight(context);
        return await repo.LoadAllDatalogsByProductId(productId);
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
    public async Task<List<DatalogWeight>> GetDatalogReport(DateTime dt)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryDatalogWeight(context);
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
