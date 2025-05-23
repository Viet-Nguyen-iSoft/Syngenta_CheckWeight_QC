using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.Responsitory;
using SyngentaWeigherQC.UI.UcUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.enumSoftware;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmOverView : Form
  {
    public delegate void SendChooseProduct(InforLine inforLine);
    public event SendChooseProduct OnSendChooseProduct;

    public delegate void SendChooseShiftLeader(InforLine inforLine);
    public event SendChooseShiftLeader OnSendChooseShiftLeader;

    public delegate void SendChooseTypeShift(InforLine inforLine);
    public event SendChooseTypeShift OnSendChooseTypeShift;

    public FrmOverView()
    {
      InitializeComponent();
      this.Shown += FrmOverView_Shown;
    }

    private void FrmOverView_Shown(object sender, EventArgs e)
    {
      LoadLine();
    }

    #region Singleton parttern
    private static FrmOverView _Instance = null;
    public static FrmOverView Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new FrmOverView();
        }
        return _Instance;
      }
    }
    #endregion

    private void FrmOverView_Load(object sender, EventArgs e)
    {
      FrmSettingLine.Instance.OnSendChangeLine += Instance_OnSendChangeLine;
      AppCore.Ins.OnSendStatusConnectWeight += Ins_OnSendStatusConnectWeight;
    }

    private void Ins_OnSendStatusConnectWeight(eStatusConnectWeight eStatusConnectWeight)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          Ins_OnSendStatusConnectWeight(eStatusConnectWeight);
        }));
        return;
      }

      UpdateStatusConnectWeight(eStatusConnectWeight);
    }

    private void Instance_OnSendChangeLine()
    {
      LoadLine();
    }

    private Size _Size = new Size(830, 540);
    private void LoadLine()
    {
      try
      {
        flowLayoutPanelLine.Controls.Clear();

        if (AppCore.Ins._listInforLine.Count > 0)
        {
          var lines = AppCore.Ins._listInforLine?.Where(x => x.IsEnable == true).ToList();
          var shift_leader = AppCore.Ins._listShiftLeader?.Where(x => x.IsDelete == false).ToList();

          foreach (var item in lines)
          {
            if (!item.RequestTare)
            {
              item.DatalogTareCurrent = item.DatalogTares.Where(x=>x.Id == item.LastTareId).FirstOrDefault();
            }

            UcOverViewMachine settingUC = new UcOverViewMachine(item);
           
            settingUC.Margin = new Padding(5);
            settingUC.Name = $"Setting{item.Name}";
            settingUC.Tag = item;
            settingUC.ShiftType = AppCore.Ins._listShiftType;
            settingUC.ShiftLeader = shift_leader;
            settingUC.Size = _Size;

            settingUC.SetShiftLeader();
            settingUC.SetShiftType();
            settingUC.InitEventChangeInforLine();

            //SetData Sumary
            var listDatalogByLine = AppCore.Ins._listDatalogWeight?.Where(x => x.InforLineId == item.Id).ToList();
            settingUC.SetSumary(listDatalogByLine);

            settingUC.OnSendChooseLineWeight += SettingUC_OnSendChooseLineWeight;
            settingUC.OnSendChooseProduct += SettingUC_OnSendChooseProduct;
            settingUC.OnSendChangeShiftLeader += SettingUC_OnSendChangeShiftLeader;
            settingUC.OnSendChangeShiftType += SettingUC_OnSendChangeShiftType;
            flowLayoutPanelLine.Controls.Add(settingUC);
          }
        }
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
      }
    }

    private void SettingUC_OnSendChangeShiftType(InforLine inforLine)
    {
      OnSendChooseTypeShift?.Invoke(inforLine);
    }

    private void SettingUC_OnSendChangeShiftLeader(InforLine inforLine)
    {
      OnSendChooseShiftLeader?.Invoke(inforLine);
    }

    private void SettingUC_OnSendChooseProduct(InforLine inforLine)
    {
      OnSendChooseProduct?.Invoke(inforLine);
    }

    private void SettingUC_OnSendChooseLineWeight(InforLine inforLine)
    {
      FormMain.Instance.OpenPageWeight(inforLine);
      AppCore.Ins.eStatusModeWeight = eNum.enumSoftware.eStatusModeWeight.WeightForLine;
      AppCore.Ins.inforLineOperation = inforLine;
    }

    public void FindAndUpdateTypeTare(InforLine inforLine)
    {
      foreach (var control in flowLayoutPanelLine.Controls)
      {
        if (control is UcOverViewMachine)
        {
          UcOverViewMachine parametterSimpleUc = (UcOverViewMachine)control;
          if (parametterSimpleUc.Tag == inforLine)
          {
            ((UcOverViewMachine)control).SetInforTare();
          }
        }
      }
    }

    public void UpdateStatusConnectWeight(eStatusConnectWeight eStatusConnectWeight)
    {
      foreach (var control in flowLayoutPanelLine.Controls)
      {
        if (control is UcOverViewMachine)
        {
          UcOverViewMachine parametterSimpleUc = (UcOverViewMachine)control;
          ((UcOverViewMachine)control).SetStatusConnectWeight(eStatusConnectWeight);
        }
      }
    }

    public void UpdateTareId(InforLine inforLine, DatalogTare datalogTare)
    {
      foreach (var control in flowLayoutPanelLine.Controls)
      {
        if (control is UcOverViewMachine)
        {
          UcOverViewMachine parametterSimpleUc = (UcOverViewMachine)control;

          var tag = parametterSimpleUc.Tag as InforLine;
          if (tag.Id == inforLine.Id)
          {
            ((UcOverViewMachine)control).UpdateTareCurrent = datalogTare;
          }  
        }
      }
    }

    public void FindAndUpdateStatisticalData(InforLine inforLine,List<StatisticalData> statisticalDatas)
    {
      foreach (var control in flowLayoutPanelLine.Controls)
      {
        if (control is UcOverViewMachine)
        {
          UcOverViewMachine parametterSimpleUc = (UcOverViewMachine)control;
          if (parametterSimpleUc.Tag == inforLine)
          {
            ((UcOverViewMachine)control).SetSumary(statisticalDatas);
          }
        }
      }
    }

    public void FindAndUpdateProduct(InforLine inforLine)
    {
      foreach (var control in flowLayoutPanelLine.Controls)
      {
        if (control is UcOverViewMachine)
        {
          UcOverViewMachine parametterSimpleUc = (UcOverViewMachine)control;
          if (parametterSimpleUc.Tag == inforLine)
          {
            ((UcOverViewMachine)control).SelectProduct();
          }
        }
      }
    }

    public void FindAndUpdateTypeShift(InforLine inforLine)
    {
      foreach (var control in flowLayoutPanelLine.Controls)
      {
        if (control is UcOverViewMachine)
        {
          UcOverViewMachine parametterSimpleUc = (UcOverViewMachine)control;
          if (parametterSimpleUc.Tag == inforLine)
          {
            ((UcOverViewMachine)control).SelectTypeShift();
          }
        }
      }
    }

    public void FindAndUpdateShiftLeader(InforLine inforLine)
    {
      foreach (var control in flowLayoutPanelLine.Controls)
      {
        if (control is UcOverViewMachine)
        {
          UcOverViewMachine parametterSimpleUc = (UcOverViewMachine)control;
          if (parametterSimpleUc.Tag == inforLine)
          {
            ((UcOverViewMachine)control).SelectShiftLeader();
          }
        }
      }
    }

    

  }
}
