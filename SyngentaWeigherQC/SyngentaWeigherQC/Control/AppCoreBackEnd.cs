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
using Color = System.Drawing.Color;
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

    public delegate void SendValueReweight(double value);
    public event SendValueReweight OnSendValueReweight;

    #endregion


    public System.Timers.Timer _timerRealTimeCheckChangeDay = new System.Timers.Timer();

    public eStatusModeWeight eStatusModeWeight = eStatusModeWeight.OverView;
    public InforLine inforLineOperation;


    public eWeigherMode _eWeigherMode = eWeigherMode.Normal;
    public eWeigherMode _eWeigherModeLast = eWeigherMode.Normal;
    public string[] _listPortPC = new string[100];

    public Shift _shiftIdCurrent = null;

    //public string ip_tcp_mettler = "192.168.2.100"; //Sachet //"147.167.40.239";

    //public string ip_tcp_mettler = "147.167.40.239"; //GN
    //public int port_tcp_mettler = 2305; //2305;

    public string ip_tcp_mettler = "127.0.0.1"; //GN
    public int port_tcp_mettler = 8080; //2305;

    private eModeCommunication eModeCommunication = eModeCommunication.TcpClient;

    public int _numberDataEachRow = 10;

    public List<Roles> _listRoles { get; set; } = new List<Roles>();
    public void Init()
    {
      InitDB();

      LoadDataLine().Wait();

      InformationDeviceDev();




      InitEvent();
      //Init_RandomData();

      CreateFolderReport();

      LoggerHelper.LogErrorToFileLog("Start App");

      StartShowUI();
    }

    public void ConnectDataWeight()
    {
      if (eModeCommunication == eModeCommunication.Serial)
      {

      }
      else if (eModeCommunication == eModeCommunication.TcpClient)
      {
        InitTcpConnectivity();
      }

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
    public ConfigSoftware _inforValueSettingStation { get; set; } = new ConfigSoftware();

    public eModeUseApp _modeUseApp = eModeUseApp.OFF;
    public eReadyReceidDataTare _readyReceidDataTare = eReadyReceidDataTare.No;
    public eReadyReceidWeigher _readyReceidDataWeigher = eReadyReceidWeigher.No;

    public Roles _roleCurrent = new Roles();


    public List<Production> _listAllProductsBelongLine { get; set; } = new List<Production>();
    public List<InforLine> _listInforLine { get; set; } = new List<InforLine>();
    public List<DatalogWeight> _listDatalogWeight { get; set; } = new List<DatalogWeight>();
    public ConfigSoftware _configSoftware { get; set; } = new ConfigSoftware();
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
        _listDatalogWeight = await AppCore.Ins.LoadAllDatalogWeight(0, DateTime.Now, DateTime.Now, 0);

        //Role
        _listRoles = await LoadAllRole();
        _listRoles = _listRoles.OrderBy(x => x.Id).ToList();
        _roleCurrent = _listRoles?.Where(s => s.Name == "OP").FirstOrDefault();

        //Config Software
        var configSoftwares = await LoadConfigSoftware();
        _configSoftware = configSoftwares.FirstOrDefault();
        if (_configSoftware != null)
        {
          ip_tcp_mettler = _configSoftware.IpTcp;
          port_tcp_mettler = _configSoftware.PortTcp;
        }
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
      }
    }



    public eEvaluate EvaluateData(double avgRaw, Production production)
    {
      return (avgRaw >= production.StandardFinal && avgRaw <= production.UpperLimitFinal) ?
        eEvaluate.Pass :
        eEvaluate.Fail;
    }

    public Tuple<Color, Color> EvaluateRetureColor(DatalogWeight datalogWeight, Production production)
    {
      if (datalogWeight.IsChange)
      {
        return new Tuple<Color, Color>(Color.Purple, Color.White);
      }
      if (datalogWeight.Value > production.UpperLimitFinal)
      {
        return new Tuple<Color, Color>(Color.DarkOrange, Color.White);
      }
      else if (datalogWeight.Value < production.LowerLimitFinal)
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


    public Shift GetShiftCode(InforLine inforLine)
    {
      if (_listShiftType.Count == 0 || _listShift.Count == 0)
      {
        return null;
      }
      if (inforLine == null)
      {
        return null;
      }

      if (inforLine.ShiftTypesId > 0)
      {
        return GetNameShift((int)inforLine.ShiftTypesId);
      }
      return null;
    }


    public List<StatisticalData> SumaryDTO(List<DatalogWeight> listDatalogByLine)
    {
      List<StatisticalData> statisticalDatas = new List<StatisticalData>();
      var groupedData = listDatalogByLine
                        .Where(x => x.Shift != null && x.Production != null)
                        .GroupBy(x => x.ShiftId)
                        .Select(shiftGroup => new
                        {
                          ShiftId = shiftGroup.Key,
                          Shift = AppCore.Ins._listShift.FirstOrDefault(x => x.Id == (int)shiftGroup.Key),
                          Productions = shiftGroup
                            .GroupBy(x => x.ProductionId)
                            .Select(prodGroup => new
                            {
                              ProductionId = prodGroup.Key,
                              Production = prodGroup.FirstOrDefault().Production,
                              Items = prodGroup.ToList()
                            }).ToList()
                        }).ToList();

      foreach (var group in groupedData)
      {
        var data = group.Productions.LastOrDefault();

        StatisticalData statisticalData = CvtDatalogWeightToStatisticalData(data.Items, data.Production);

        if (group.Shift.CodeShift == 1 || group.Shift.CodeShift == 4)
        {
          statisticalData.Index = 1;
        }
        else if (group.Shift.CodeShift == 2 || group.Shift.CodeShift == 5)
        {
          statisticalData.Index = 2;
        }
        else if (group.Shift.CodeShift == 3)
        {
          statisticalData.Index = 3;
        }
        statisticalDatas.Add(statisticalData);
      }

      return statisticalDatas;
    }

    public List<DataReportExcel> GenerateDataReport(List<DatalogWeight> datalogWeights)
    {
      if (datalogWeights == null)
        return new List<DataReportExcel>();

      return datalogWeights
      .GroupBy(dw => ((DateTime)dw.CreatedAt).Date) // Giả sử BaseModel có CreateDate
      .Select(dateGroup => new DataReportExcel
      {
        DateTime = dateGroup.Key,
        DataByDates = dateGroup
              .GroupBy(dw => dw.ShiftType)
              .Select(shiftTypeGroup => new DataByShiftType
              {
                ShiftType = shiftTypeGroup.Key,
                DataByProducts = shiftTypeGroup
                      .GroupBy(dw => dw.Production)
                      .Select(prodGroup => new DataByProduction
                      {
                        Production = prodGroup.Key,
                        DataByProductionByShifts = prodGroup
                              .GroupBy(dw => dw.Shift)
                              .Select(shiftGroup => new DataByProductionByShift
                              {
                                Shift = shiftGroup.Key,
                                DatalogWeights = shiftGroup.ToList()
                              })
                              .ToList()
                      })
                      .ToList()
              })
              .ToList()
      })
      .ToList();

      //var result = datalogWeights
      //    .Where(d => d.CreatedAt != null && d.Shift != null && d.Production != null)
      //    .GroupBy(d => ((DateTime)d.CreatedAt).Date)
      //    .Select(dateGroup => new DataReportExcel
      //    {
      //      DateTime = dateGroup.Key,
      //      DataByDates = dateGroup
      //        .GroupBy(d => d.Shift)
      //        .Select(shiftGroup => new DataByShiftType
      //        {
      //          Shift = shiftGroup.Key,
      //          DataByProducts = shiftGroup
      //                .GroupBy(d => d.Production)
      //                .Select(prodGroup => new DataByProduction
      //                {
      //                  Production = prodGroup.Key,
      //                  DatalogWeights = prodGroup.OrderBy(x => x.Id).ToList()
      //                })
      //                .ToList()
      //        })
      //        .OrderBy(x => x.Shift.Id)
      //        .ToList()
      //    })
      //    .OrderBy(x => x.DateTime)
      //    .ToList();

      return null;
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
        LoggerHelper.LogErrorToFileLog(ex.ToString());
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
      _isPermitDev = (computerName == "DESKTOP-VIPBB6Qx");
    }















    public void InitDB()
    {
      try
      {
        DataBase.Init().Wait();
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog($"Lỗi khi khởi tạo chương trình, vui lòng khởi động lại!" + ex.ToString());
        System.Windows.Forms.MessageBox.Show($"Lỗi khi khởi tạo chương trình, vui lòng khởi động lại!", "Lỗi");
        Environment.Exit(2);
      }
    }



    //DB Data
    public List<Production> _listAllProductsContainIsDelete { get; set; } = new List<Production>();
    public List<HistoricalChangeMasterData> _listChangeProductsLogging { get; set; } = new List<HistoricalChangeMasterData>();


    public List<ShiftType> _listShiftType { get; set; } = new List<ShiftType>();
    public ShiftType _shiftTypeCurrent { get; set; } = new ShiftType();
    public List<Shift> _listShift { get; set; } = new List<Shift>();
    
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



    private Shift GetNameShift(int codeShiftType)
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
            return item;
          }
        }
        else
        {
          TimeSpan endDay = new TimeSpan(23, 59, 59);
          TimeSpan startDay = new TimeSpan(0, 0, 0);
          if ((datatimeCurrent >= startTime && datatimeCurrent <= endDay) || (datatimeCurrent >= startDay && datatimeCurrent <= endTime))
          {
            return item;
          }
        }
      }
      return null;
    }



    public void InitEvent()
    {
      try
      {
        _timerRealTimeCheckChangeDay.Interval = 1000;
        _timerRealTimeCheckChangeDay.Elapsed += _timerRealTimeCheckChangeDay_Elapsed;
        _timerRealTimeCheckChangeDay.Start();


        FrmSettingConfigSoftware.Instance.OnSendChangeConnection += Instance_OnSendChangeConnection;
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex.ToString());
      }
    }

    private void Instance_OnSendChangeConnection(ConfigSoftware configSoftware)
    {
      if (configSoftware != null)
      {
        ip_tcp_mettler = configSoftware.IpTcp;
        port_tcp_mettler = configSoftware.PortTcp;
      }
    }

    public async void ReportAutoDailys(DateTime dateTime)
    {
      if (AppCore.Ins._listInforLine.Count > 0)
      {
        var lines = AppCore.Ins._listInforLine?.Where(x => x.IsEnable == true).ToList();
        foreach (var line in lines)
        {
          var datalogs = await AppCore.Ins.LoadAllDatalogWeight(line.Id, dateTime, dateTime, 0);
          List<DataReportExcel> dataReportExcels = AppCore.Ins.GenerateDataReport(datalogs);

          if (dataReportExcels != null)
          {
            if (dataReportExcels.Count > 0)
            {
              string[] pathReport = new string[3];
              pathReport[0] = line.PathReportLocal;
              pathReport[1] = line.PathReportOneDrive;
              foreach (var item in dataReportExcels)
              {
                ExcelHelper.ReportExcel(item, pathReport);
              }
            }
          }
        }
      }
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
            //Check Xuất report auto theo tháng, tuần
            //ExportAutoMonthOrWeek();

            //Report Auto Dailys
            ReportAutoDailys(DateTime.Now.AddDays(-1));

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
        LoggerHelper.LogErrorToFileLog(ex.ToString());
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
        LoggerHelper.LogErrorToFileLog(ex);
      }
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
    public async Task<ConfigSoftware> LoadValueSetting()
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryConfigSoftware(context);
        return await repo.GetAsync();
      }
    }

    public async Task UpdateValueSetting(ConfigSoftware inforValueSettingStation)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryConfigSoftware(context);
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
