﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.eNum
{
  public class eUI
  {
    public enum AppModulSupport
    {
      Home,
      Setting,
      SettingProducts,
      OverView,

      MasterData,
      ReportExcel,
      Consumption,
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
      BaCa,
      GianCa,
      HanhChinh,
    }

    public enum eValuate
    {
      UNKNOWN,
      FAIL,
      PASS,
      OVER
    }

    public enum eModeTare
    {
      [Description("Không nhãn")]
      TareNoLabel,
      [Description("Có nhãn")]
      TareWithLabel,
    }

    public enum eStatusModeWeight
    {
      OverView,
      WeightForLine,
      TareForLine,
    }

    public enum ePermit
    {
      role_ImportProduct,
      role_Synthetic,
      role_Excel,
      role_ShiftLineSetting,
      role_AccountSetting,
      role_ChangeProduct,
      role_ChangeShiftType,
      role_CreateUser,
      role_ChangeUser,


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
