using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.eNum
{
  public class enumSoftware
  {
    public enum AppModulSupport
    {
      OverView,
      Home,
      ReportExcel,
      Synthetic,
      Setting,
      MasterData,



      
      
      About,
      Exit, 
      SettingGeneral,
      LineSetting,
      SoftSetting,
      MachineSetting,
      Decentralization,
      Manual,
      UserSetting,
      PassChangeSetting,
      DeviceSetting
    }

    public enum eMsgType
    {
      Info,
      Error,
      Warning,
      Confirmation,
      Question
    }

    public enum eTypeAddProduct
    {
      Add,
      Edit,
    }

    public enum eReportConsumption
    {
      Month,
      Week,
      Date
    }

    public enum eRole
    {
      QC,
      ME,
      ShiftLeader,
      OP,
      Admin,
    }

    public enum eEditUser
    {
      ChangeUser,
      NewUser,
    }

    public enum eWeigherMode
    {
      NoApp,
      Normal,
      SampleRework,
      Tare,

    }

    public enum eReadyReceidDataTare
    {
      No,
      Yes,
    }

    public enum eReadyReceidWeigher
    {
      No,
      Yes,
    }

    public enum eModeUseApp
    {
      UNKNOWN,
      ON,
      OFF,
    }

    public enum eDataFormat
    {
      Packsize,
      Standard,
      Upperlimit,
      Lowerlmit,
      TareTotalSamples,
      TareStandard,
      TareUpperlimit,
      TareLowerlmit
    }

    public enum eShiftTypes
    {
      BaCa = 1,
      GianCa,
      HanhChinh,
    }

    

    public enum eModeTare
    {
      [Description("Không nhãn")]
      TareNoLabel,
      [Description("Có nhãn")]
      TareWithLabel,
    }

    public enum eEvaluate
    {
      [Description("NG")]
      None,
      [Description("Đạt")]
      Pass,
      [Description("Không đạt")]
      Fail,
    }

    public enum eSort
    {
      [Description("Giảm dần")]
      OrderBy,
      [Description("Tăng dần")]
      OrderByDescending,
    }
    public enum eEvaluateStatus
    {
      UNKNOWN,
      FAIL,
      PASS,
      OVER
    }
    public enum eStatusModeWeight
    {
      OverView,
      WeightForLine,
      TareForLine,
      Reweight,
    }

    public enum eStatusConnectWeight
    {
      [Description("Trạng thái: Không xác định")]
      None,
      [Description("Trạng thái: Kết nối")]
      Connected,
      [Description("Trạng thái: Mất kết nối")]
      Disconnnect,
    }

    public enum ePermit
    {
      ReportConsumption,
      ReportExcel,
      AddProduct,
      EditProduct,
      SeeHistoricalAddProduct,

      SettingInformationLine,
      SettingGeneral,
      SettingAccount,
      SettingShiftLeader,
      SettingDevice,
    }

    public enum eShift
    {
      All_Shift,
      Shift1,
      Shift2,
      Shift3,
      GianCa1,
      GianCa2,
      HanhChinh,
    }

    public enum eModeCommunication
    {
      None,
      Serial,
      TcpClient,
    }



  }
}
