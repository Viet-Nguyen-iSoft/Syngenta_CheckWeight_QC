using Newtonsoft.Json.Linq;
using SynCheckWeigherLoggerApp.SettingsViews;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.UI.FrmAddProduct;
using SyngentaWeigherQC.UI.FrmUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SynCheckWeigherLoggerApp.DashboardViews.FrmSampleRework;
using static SyngentaWeigherQC.eNum.eUI;
using static SyngentaWeigherQC.FrmMain;
using static SyngentaWeigherQC.UI.FrmUI.FrmChangeLine;

namespace SyngentaWeigherQC
{
  public partial class FrmMain : Form
  {
    public delegate void SendChooseProduct(Production production);
    public event SendChooseProduct OnSendChooseProduct;

    public delegate void SendAddNewUser();
    public event SendAddNewUser OnSendAddNewUser;

    public delegate void SendChangeLogin();
    public event SendChangeLogin OnSendChangeLogin;

    public FrmMain()
    {
      InitializeComponent();

      this.WindowState = FormWindowState.Maximized;
      this.StartPosition = FormStartPosition.CenterScreen;
    }

    private void Ins_OnSendSendUpdateProducts(object sender)
    {
      this.cbProductions.SelectedIndexChanged -= CbProductions_SelectedIndexChanged;
      //ReLoad Product
      LoadProducts();
      this.cbProductions.SelectedIndexChanged += CbProductions_SelectedIndexChanged;
    }

    #region Singleton parttern
    private static FrmMain _Instance = null;
    public static FrmMain Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new FrmMain();
        }
        return _Instance;
      }
    }
    #endregion

    #region Call form child
    private Form CurrentForm;
    public void OpenChildForm(AppModulSupport modulSupport, Form ChildForm)
    {
      bool Is_same_form = false;
      if (this.pnBody.Tag != null)
      {
        if (this.pnBody.Tag is Tuple<AppModulSupport, Form>)
        {
          Tuple<AppModulSupport, Form> TagAsForm = (Tuple<AppModulSupport, Form>)(this.pnBody.Tag);
          if (TagAsForm.Item1 == modulSupport)
          {
            Is_same_form = true;
          }
        }
      }

      if (CurrentForm != null)
      {
        CurrentForm.Visible = false;

      }
      this.pnBody.Tag = Tuple.Create(modulSupport, ChildForm);
      CurrentForm = ChildForm;
      ChildForm.TopLevel = false;
      ChildForm.FormBorderStyle = FormBorderStyle.None;
      ChildForm.Dock = DockStyle.Fill;
      ChildForm.BringToFront();
      this.pnBody.Controls.Add(ChildForm);
      ChildForm.Show();
    }
    #endregion

    public System.Timers.Timer _timeCalTimeOut = new System.Timers.Timer();
    private List<Production> productions = new List<Production>();
    private List<ShiftType> shiftTypes = new List<ShiftType>();


    public int cntCheckTimeOut = 0;
    private void FrmMain_Load(object sender, EventArgs e)
    {
      this.lbNameLine.Text = AppCore.Ins._stationCurrent?.Name;
      this.btHome.PerformClick();

      LoadProducts();

      LoadShiftType();

      LoadUsers(AppCore.Ins._listShiftLeader);


      this.cbProductions.SelectedIndexChanged += CbProductions_SelectedIndexChanged;
      this.cbUsers.SelectedIndexChanged += CbUsers_SelectedIndexChanged;
      this.cbShiftTypes.SelectedIndexChanged += CbShiftTypes_SelectedIndexChanged;

      AppCore.Ins.OnSendSendUpdateProducts += Ins_OnSendSendUpdateProducts;

      
      AppCore.Ins.OnSendChangeLine += Ins_OnSendChangeLine;

      //Sự kiện nút nhấn
      //FrmHome.Instance.OnSendChangeModeUseApp += Instance_OnSendChangeModeUseApp1;

      //Sự kiện Setting
      FrmSettingGeneral.Instance.OnSendModeOnOffApp += Instance_OnSendModeOnOffApp;
      FrmSettingGeneral.Instance.OnSendSettingNumberTimeOut += Instance_OnSendSettingNumberTimeOut;

      //bool isCheckOffApp = (AppCore.Ins._stationCurrent==null)? false: AppCore.Ins._stationCurrent.IsEnableOffApp;
      //if (isCheckOffApp)
      //{
      //  ShowFrmWaiting();
      //  //Timer check hiển thị app
      //  _timeCalTimeOut.Interval = 1000;
      //  _timeCalTimeOut.Elapsed += _timeCalTimeOut_Elapsed;
      //}
      //FrmHome.Instance.StatusBtnActive(isCheckOffApp);
    }

    private void Instance_OnSendSettingNumberTimeOut(int timerNumber)
    {
      cntCheckTimeOut = (timerNumber > 0) ? timerNumber * 60 : 300;
    }

    private void Instance_OnSendModeOnOffApp(bool isOn)
    {
      AppCore.Ins._modeUseApp = isOn ? eModeUseApp.ON : eModeUseApp.OFF;

      _timeCalTimeOut.Stop();

      if (!isOn)
      {
        //Nothing
      }
      else
      {
        //FrmHome.Instance.StatusBtnActive(false);
        ShowFrmWaiting();
      }
    }

    private void Instance_OnSendChangeModeUseApp1(eModeUseApp eModeUseApp)
    {
      AppCore.Ins._modeUseApp = eModeUseApp;

      if (eModeUseApp == eModeUseApp.ON)
      {
        //Nothing
      }
      else
      {
        _timeCalTimeOut.Stop();
        //FrmHome.Instance.StatusBtnActive(false);
        ShowFrmWaiting();
      }
    }

    private void ShowFrmWaiting()
    {
      AppCore.Ins._eWeigherMode = eWeigherMode.NoApp;
      FrmWaiting frmWaiting = new FrmWaiting();
      frmWaiting.OnSendActiveApp += FrmWaiting_OnSendActiveApp;
      frmWaiting.ShowDialog();
    }

    //Active
    private void FrmWaiting_OnSendActiveApp()
    {
      //int timerCheckTimeOut = AppCore.Ins._stationCurrent.TimeCheckTimeOutModeWeigher; //phut
      //cntCheckTimeOut = (timerCheckTimeOut > 0) ? timerCheckTimeOut * 60 : 300;//ĐỔi ra s. Mặc định 5 phút
      //AppCore.Ins._eWeigherMode = AppCore.Ins._eWeigherModeLast;
      //AppCore.Ins._modeUseApp = eModeUseApp.ON;
      //FrmHome.Instance.StatusBtnActive(true);

      ////Đếm số giây còn lại
      //_timeCalTimeOut.Start();
    }

    public void RefeshTimeOut()
    {
      //int timerCheckTimeOut = AppCore.Ins._stationCurrent.TimeCheckTimeOutModeWeigher; //phut
      //cntCheckTimeOut = (timerCheckTimeOut > 0) ? timerCheckTimeOut * 60 : 300;//ĐỔi ra s. Mặc định 5 phút
    }

    private void _timeCalTimeOut_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      _timeCalTimeOut.Stop();
      try
      {
        if (cntCheckTimeOut>=0)
        {
          cntCheckTimeOut--;
          //FrmHome.Instance.ShowTimeOutUI(cntCheckTimeOut);
        }  
        else
        {
          AppCore.Ins.SendTimeout();
        }  
      }
      catch (Exception ex)
      {
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
      }
      finally { _timeCalTimeOut.Start(); }
    }


    private void Ins_OnSendChangeLine(List<Production> productions)
    {
      this.cbProductions.SelectedIndexChanged -= CbProductions_SelectedIndexChanged;
      //ReLoad Product
      LoadProducts();
      this.cbProductions.SelectedIndexChanged += CbProductions_SelectedIndexChanged;
    }


    private void Instance_OnSendUpdateUser()
    {
      this.cbUsers.SelectedIndexChanged -= CbUsers_SelectedIndexChanged;
      LoadUsers(AppCore.Ins._listShiftLeader);
      this.cbUsers.SelectedIndexChanged += CbUsers_SelectedIndexChanged;
    }

    private void Instance_OnSendRemoveUser()
    {
      this.cbUsers.SelectedIndexChanged -= CbUsers_SelectedIndexChanged;
      LoadUsers(AppCore.Ins._listShiftLeader);
      this.cbUsers.SelectedIndexChanged += CbUsers_SelectedIndexChanged;
    }

    private async void CbShiftTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (AppCore.Ins.CheckRole(ePermit.role_ChangeShiftType) || AppCore.Ins._roleCurrent.Name == "iSOFT")
      {
        string shiftTypesName = this.cbShiftTypes.SelectedItem.ToString();
        ShiftType shiftTypesChoose = shiftTypes?.Where(s => s.Name == shiftTypesName).FirstOrDefault();

        if (shiftTypesChoose != null)
        {
          //Save DB 
          for (int i = 0; i < shiftTypes.Count; i++)
          {
            if (shiftTypes[i].Name == shiftTypesChoose.Name)
            {
              shiftTypes[i].isEnable = true;
            }
            else
            {
              if (shiftTypes[i].isEnable == true) 
                shiftTypes[i].isEnable = false;
            }
          }

          await AppCore.Ins.UpdateShiftTypeChoose(shiftTypes);
          AppCore.Ins._listShiftType = shiftTypes;
          AppCore.Ins._shiftTypeCurrent = shiftTypesChoose;
        }
      }
      else
      {
        this.cbShiftTypes.SelectedIndexChanged -= CbShiftTypes_SelectedIndexChanged;
        new FrmNotification().ShowMessage("Tài khoản không có quyền thay đổi loại ca !", eMsgType.Warning);
        this.cbShiftTypes.SelectedItem = AppCore.Ins._listShiftType?.Where(x => x.isEnable == true).FirstOrDefault().Name;
        this.cbShiftTypes.SelectedIndexChanged += CbShiftTypes_SelectedIndexChanged;
      }
      
    }

    private void LoadShiftType()
    {
      //Load ShiftType
      shiftTypes = AppCore.Ins._listShiftType;
      if (shiftTypes.Count > 0)
      {
        cbShiftTypes.DataSource = shiftTypes?.Select(x => x.Name).ToList();
        ShiftType shiftTypeChoose = shiftTypes?.Where(x => x.isEnable == true).FirstOrDefault();
        if (shiftTypeChoose == null) cbShiftTypes.SelectedIndex = -1;
        else
        {
          cbShiftTypes.SelectedItem = shiftTypeChoose?.Name;
        }
      }
    }

    private async void CbUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (AppCore.Ins.CheckRole(ePermit.role_ChangeUser) || AppCore.Ins._roleCurrent.Name == "iSOFT")
      {
        try
        {
          if (cbUsers.Items.Count > 0)
          {
            string userNameChoose = cbUsers.SelectedItem?.ToString();
            List<ShiftLeader> users = AppCore.Ins._listShiftLeader;
            if (users == null) return;
            if (users.Count() > 0)
            {
              ShiftLeader userChoose = users?.Where(s => s.UserName == userNameChoose).FirstOrDefault();

              //Save DB 
              if (userChoose != null)
              {
                for (int i = 0; i < users.Count; i++)
                {
                  if (users[i].UserName == userChoose.UserName)
                    users[i].IsEnable = true;
                  else
                    if (users[i].IsEnable == true) users[i].IsEnable = false;
                }
                await AppCore.Ins.UpdateProductChoose(users);
                AppCore.Ins._listUserCurrent = userChoose;
              }
            }
          }
        }
        catch (Exception ex)
        {
          AppCore.Ins.LogErrorToFileLog(ex.ToString());
        }
      }
      else
      {
        this.cbUsers.SelectedIndexChanged -= CbUsers_SelectedIndexChanged;
        new FrmNotification().ShowMessage("Tài khoản không có quyền thay đổi tổ trưởng truyền !", eMsgType.Warning);
        this.cbUsers.SelectedItem = AppCore.Ins._listUserCurrent.UserName;
        this.cbUsers.SelectedIndexChanged += CbUsers_SelectedIndexChanged;
      }
    }

    private void LoadProducts()
    {
      //Load Product
      productions = AppCore.Ins._listProductsWithStation;
      cbProductions.DataSource = null;
      if (productions.Count > 0)
      {
        cbProductions.DataSource = productions?.Select(x => x.Name).ToList();

        Production nameProductCurrent = productions?.FirstOrDefault();
        if (nameProductCurrent == null) cbProductions.SelectedIndex = -1;
        else
        {
          cbProductions.SelectedItem = nameProductCurrent?.Name;
          //FrmHome.Instance.UpdateInforProductCurrent(nameProductCurrent);

          this.lblTyTrong.Text = nameProductCurrent?.Density.ToString();
        }
      }
    }  

    

    private void CbProductions_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (AppCore.Ins.CheckRole(ePermit.role_ChangeProduct) || AppCore.Ins._roleCurrent.Name == "iSOFT")
      {
        FrmInformation frmInformation = new FrmInformation(cbProductions.SelectedItem.ToString());
        frmInformation.OnSendOKClicked += FrmInformation_OnSendOKClicked;
        frmInformation.ShowDialog();
      }
      else
      {
        this.cbProductions.SelectedIndexChanged -= CbProductions_SelectedIndexChanged;
        new FrmNotification().ShowMessage("Tài khoản không có quyền thay đổi sản phẩm !", eMsgType.Warning);
        this.cbProductions.SelectedItem = AppCore.Ins._currentProduct.Name;
        this.cbProductions.SelectedIndexChanged += CbProductions_SelectedIndexChanged;
      }
    }

    private void FrmInformation_OnSendCancelClicked(object sender)
    {
      this.cbProductions.SelectedIndexChanged -= CbProductions_SelectedIndexChanged;
      this.cbProductions.SelectedItem = AppCore.Ins._currentProduct.Name;
      this.cbProductions.SelectedIndexChanged += CbProductions_SelectedIndexChanged;
    }

    private async void FrmInformation_OnSendOKClicked(object sender)
    {
      string nameProduct = cbProductions.SelectedItem.ToString();
      Production productionCurrent = productions?.Where(s=>s.Name== nameProduct).FirstOrDefault();

      //FrmHome.Instance.UpdateInforProductCurrent(productionCurrent);
      //FrmHome.Instance.ChangeProduct(true);
      if (productionCurrent!=null)
        this.lblTyTrong.Text = productionCurrent?.Density.ToString();

      //Save DB ProductCurrent
      if (productions.Count>0 && productionCurrent != null)
      {
        for (int i = 0; i < productions.Count; i++)
        {
          //if (productions[i].NameProduct == productionCurrent.NameProduct)
          //{
          //  productions[i].IsEnable = true;
          //}  
          //else
          //{
          //  if (productions[i].IsEnable == true) productions[i].IsEnable = false;
          //}  
        }

        await AppCore.Ins.UpdateRangeProduct(productions);
        AppCore.Ins._currentProduct = productionCurrent;

        AppCore.Ins._inforValueSettingStation.IsChangeProductNoTare = true;
        await AppCore.Ins.UpdateValueSetting(AppCore.Ins._inforValueSettingStation);

        OnSendChooseProduct?.Invoke(productionCurrent);
      }  
    }

  

    private void btHome_Click(object sender, EventArgs e)
    {
      //OpenChildForm(AppModulSupport.Home, FrmHome.Instance);
      OpenChildForm(AppModulSupport.OverView, FrmOverView.Instance);
    }


    private void btSettings_Click(object sender, EventArgs e)
    {
      OpenChildForm(AppModulSupport.Setting, FrmSettings.Instance);
    }

    private void btnAddUser_Click(object sender, EventArgs e)
    {
      if (AppCore.Ins.CheckRole(ePermit.role_CreateUser) || AppCore.Ins._roleCurrent.Name == "iSOFT")
      {
        FrmAddNewUser frmAddNewUser = new FrmAddNewUser("Thêm tài khoản người dùng", new ShiftLeader(), eEditUser.NewUser);
        frmAddNewUser.OnSendOKClicked += FrmAddNewUser_OnSendOKClicked;
        frmAddNewUser.ShowDialog();
      }
      else
      {
        new FrmNotification().ShowMessage("Tài khoản không có quyền tạo thêm tên mới !", eMsgType.Warning);
      }
    }

    private async void FrmAddNewUser_OnSendOKClicked(ShiftLeader user)
    {
      this.cbUsers.SelectedIndexChanged -= CbUsers_SelectedIndexChanged;

      await AppCore.Ins.AddUser(user);
      AppCore.Ins._listShiftLeader.Add(user);
      LoadUsers(AppCore.Ins._listShiftLeader);
      OnSendAddNewUser?.Invoke();
      this.cbUsers.SelectedIndexChanged += CbUsers_SelectedIndexChanged;
    }


    public void LoadUsers(List<ShiftLeader> user)
    {
      if (user.Count()>0)
      {
        cbUsers.DataSource = user?.Select(s=>s.UserName).ToList();

        ShiftLeader userCurent = user?.Where(x=>x.IsEnable==true).FirstOrDefault();
        if (userCurent!=null)
        {
          cbUsers.SelectedItem = userCurent.UserName;
        }  
        else
        {
          cbUsers.SelectedIndex = -1;
        }
      }
      else
      {
        cbUsers.DataSource = null;
      }  
    }

    

    private void btTare_Click(object sender, EventArgs e)
    {
      Production productions = AppCore.Ins._currentProduct;
      if (productions==null)
      {
        new FrmNotification().ShowMessage("Vui lòng chọn sản phẩm cần Tare !", eMsgType.Warning);
      }
      else
      {
        if (string.IsNullOrEmpty(productions.Name))
        {
          new FrmNotification().ShowMessage("Vui lòng chọn sản phẩm cần Tare !", eMsgType.Warning);
        }
        else
        {
          RefeshTimeOut();
          eModeTare eModeTare = AppCore.Ins._modeTare;
          FrmTare frmTare = new FrmTare(AppCore.Ins._currentProduct, eModeTare);
          frmTare.OnSendCloseFrmTare += FrmTare_OnSendCloseFrmTare;
          AppCore.Ins._eWeigherMode = eWeigherMode.Tare;
          AppCore.Ins._eWeigherModeLast = eWeigherMode.Tare;
          frmTare.ShowDialog();
        } 
      }  
     
    }

    private void FrmTare_OnSendCloseFrmTare()
    {
      AppCore.Ins._eWeigherMode = eWeigherMode.Normal;
      AppCore.Ins._eWeigherModeLast = eWeigherMode.Normal;
    }

    private void btExportExcel_Click(object sender, EventArgs e)
    {
      if (AppCore.Ins.CheckRole(ePermit.role_Excel) || AppCore.Ins._roleCurrent.Name=="iSOFT")
      {
        OpenChildForm(AppModulSupport.ReportExcel, FrmExcelExport.Instance);
      }  
      else
      {
        new FrmNotification().ShowMessage("Tài khoản không có quyền truy cập trang Report này !", eMsgType.Warning);
      }  
    }

    private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      AppCore.Ins.DeInitCommunication();
    }

    private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
    {
      //AppCore.Ins.serialPort.Close();
    }

    private void btUserLogin_Click(object sender, EventArgs e)
    {
      if (AppCore.Ins._roleCurrent.Name=="OP")
      {
        FrmLogIn frmLogIn = new FrmLogIn(AppCore.Ins._listRoles);
        frmLogIn.OnSendLogInOK += FrmLogIn_OnSendLogInOK;
        frmLogIn.ShowDialog();
      }  
      else
      {
        AppCore.Ins._roleCurrent = AppCore.Ins._listRoles.Where(x => x.Name == "OP").FirstOrDefault();
        this.btUserLogin.Text = "OP";
        OnSendChangeLogin?.Invoke();
      }  
      
    }

    private void FrmLogIn_OnSendLogInOK(object sender, string account, bool isOK = true)
    {
      if (account=="iSOFT")
      {
        Roles roleAuthor = new Roles();
        roleAuthor.Name = account;
        roleAuthor.Description = account;
        roleAuthor.Passwords = "1";
        roleAuthor.Permission = "role_ImportProduct_role_Synthetic_role_Excel_role_ShiftLineSetting_role_AccountSetting_role_ChangeProduct_role_ChangeShiftType_role_CreateUser_role_ChangeUser";

        AppCore.Ins._roleCurrent = roleAuthor;
      }  
      else
      {
        AppCore.Ins._roleCurrent = AppCore.Ins._listRoles?.Where(x => x.Name == account).FirstOrDefault();
      }
      
      this.btUserLogin.Text = account;
      OnSendChangeLogin?.Invoke();
    }


    private void lbNameLine_Click(object sender, EventArgs e)
    {
      //FrmAddProductCup frmAddProductGN_Sachet = new FrmAddProductCup();
      //frmAddProductGN_Sachet.ShowDialog(this);

      //FrmAddProductGN_Sachet frmAddProductGN_Sachet = new FrmAddProductGN_Sachet();
      //frmAddProductGN_Sachet.ShowDialog(this);

      //FrmAddProductGN_Sachet frmAddProductGN_Sachet = new FrmAddProductGN_Sachet();
      //frmAddProductGN_Sachet.ShowDialog(this);

      ////FrmLoading.Ins.ShowLoading();
      //cntCheckTimeOut = 10;
      //return;

      if (AppCore.Ins._roleCurrent.Name == "iSOFT" || AppCore.Ins._isPermitDev)
      {
        FrmChangeLine frmChangeLine = new FrmChangeLine();
        frmChangeLine.OnSendConfirmChangeLine += FrmChangeLine_OnSendConfirmChangeLine;
        frmChangeLine.ShowDialog();
      }
    }

    private void FrmChangeLine_OnSendConfirmChangeLine(object sender, int stationID)
    {
      AppCore.Ins.OnSendConfirmChangeLine(stationID);
      this.lbNameLine.Text = AppCore.Ins._listInforLine.Where(x=>x.Id== stationID).Select(x=>x.Name).FirstOrDefault();
    }

    public void UpdateShiftUI(string nameShift)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          UpdateShiftUI(nameShift);
        }));
        return;
      }

      this.lblCurrentShift.Text = (nameShift != null) ? nameShift : "";
    }

    public void UpdateInforModeTare(int modeTare)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          UpdateInforModeTare(modeTare);
        }));
        return;
      }

      this.lblTareType.Text = (modeTare == 1) ? "Tare có nhãn" : "Tare không nhãn";
    }

    private void btConsumption_Click(object sender, EventArgs e)
    {
      OpenChildForm(AppModulSupport.Consumption, FrmConsumption.Instance);
    }


    public void WarningWeight()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          WarningWeight();
        }));
        return;
      }

      new FrmNotification().ShowMessage("Lỗi cân\r\nVui lòng thả sản phẩm không liền kề !", eMsgType.Warning);
    }

  }
}
