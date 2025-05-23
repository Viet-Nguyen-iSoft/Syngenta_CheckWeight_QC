using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Irony.Parsing;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.UI.FrmAddProduct;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.enumSoftware;
using Production = SyngentaWeigherQC.Models.Production;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmMasterdata : Form
  {
    public FrmMasterdata()
    {
      InitializeComponent();
    }

    #region Singleton parttern
    private static FrmMasterdata _Instance = null;
    public static FrmMasterdata Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new FrmMasterdata();
        }
        return _Instance;
      }
    }









    #endregion


    private List<ProductionDTO> _productionDTOs = new List<ProductionDTO>();
    private List<Production> _products = new List<Production>();
    private List<Production> _productsSearch = new List<Production>();


    private List<ProductionDTO> _productionExcels = new List<ProductionDTO>();
    private List<ProductionDTO> _production_ExitsDb = new List<ProductionDTO>();
    private List<ProductionDTO> _production_New = new List<ProductionDTO>();
    private void FrmSettingProducts_Load(object sender, EventArgs e)
    {
      FrmSettingLine.Instance.OnSendChangeLine += Instance_OnSendChangeLine;

      //Load Data cũ
      _products = AppCore.Ins._listProductsForStation;

      LoadProducts();
    }

    private async void LoadProducts(string textSearch = "")
    {
      //Load Product //Chỉ lấy data của Line hiện tại 
      _productsSearch = await AppCore.Ins.LoadAllProducts();
      if (_productsSearch?.Count>0)
      {
        _productsSearch = _productsSearch
                    .Where(p => AppCore.Ins._listInforLine.Select(x => x.Id).ToList()
                    .Contains((int)p.InforLineId))
                    .OrderBy(x => x.LineCode)
                    .ToList();
        var dataDTO = CvtClassDTO.ConvertToProductionExcelList(_productsSearch);
        var dataDTO_AfterFilter = SearchData(dataDTO, textSearch);

        SetDatagridview(dataDTO_AfterFilter);
      }  
    }

    private void dgvMasterData_Scroll(object sender, ScrollEventArgs e)
    {
      if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
      {
        if (e.NewValue != e.OldValue)
        {
          dgvMasterData.Columns[0].Frozen = true;
          dgvMasterData.HorizontalScrollingOffset = e.NewValue;
          dgvMasterData.Columns[0].DisplayIndex = 0;
        }
      }
    }

    private List<ProductionDTO> SearchData(List<ProductionDTO> productionDTOs, string textSearch)
    {
      return productionDTOs?
            .Where(p =>
                  (!string.IsNullOrEmpty(p.LineCode) && p.LineCode.Contains(textSearch)) ||
                  (!string.IsNullOrEmpty(p.Name) && p.Name.Contains(textSearch)))
            .ToList();
    }

    private void Instance_OnSendChangeLine()
    {
      LoadProducts();
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
      LoadProducts(this.txtSearch.Texts);
    }


    #region Import
    private void btnImportExcel_Click(object sender, EventArgs e)
    {
      DialogResult result = this.openFileDialogImport.ShowDialog();
      if (result == DialogResult.OK)
      {
        if (backgroundWorkerImport.IsBusy == false)
        {
          this.backgroundWorkerImport.RunWorkerAsync();
        }
      }
    }


    private string file_name = "";
    private void openFileDialogImport_FileOk(object sender, CancelEventArgs e)
    {
      file_name = openFileDialogImport.FileName;
    }

    private void backgroundWorkerImport_DoWork(object sender, DoWorkEventArgs e)
    {
      try
      {
        var lineForStation = AppCore.Ins._listInforLine?.Where(x => x.IsEnable).ToList();
        _productionExcels = HelperImportExcel.PareXlsxByAspose_Production(file_name, lineForStation);
        _productionExcels = _productionExcels?.OrderBy(x=>x.LineCode).ToList();
        int id = 1;
        foreach (var item in _productionExcels)
        {
          item.No = id++;
        }

        if (_productionExcels?.Count > 0)
        {
          //Check Data cũ mới
          _production_ExitsDb = new List<ProductionDTO>();
          _production_New = new List<ProductionDTO>();

          foreach (var item in _productionExcels)
          {
            bool isSame = Compare(_products, item);
            if (isSame)
            {
              _production_ExitsDb.Add(item);
            }
            else
            {
              _production_New.Add(item);
            }
          }
        }
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
        new FrmNotification().ShowMessage("File excel load thất bại !", eMsgType.Warning);
      }
      finally
      {
        VisibleSave(true);
      }
    }
    private void backgroundWorkerImport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (_productionExcels?.Count > 0)
      {
        SetDatagridview(_productionExcels);
        
        new FrmNotification().ShowMessage($"Có {_production_New.Count()} dữ liệu mới\r\nCó {_production_ExitsDb.Count()} dữ liệu cập nhật", eMsgType.Info);
      }
      else
      {
        new FrmNotification().ShowMessage("Không có dữ liệu !", eMsgType.Info);
      }

      VisibleSave(_productionExcels.Count() > 0);
    }

    #endregion

    private void VisibleSave(bool isVis)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() => { VisibleSave(isVis); }));
        return;
      }

      this.btnSaveChange.Visible = isVis;
    }



    private void SetDatagridview(List<ProductionDTO> productionDTOs)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() => { SetDatagridview(productionDTOs); }));
        return;
      }
      dgvMasterData.DataSource = null;
      if (productionDTOs.Count > 0)
      {
        productionDTOs = productionDTOs.OrderBy(x => x.LineCode).ToList();
        dgvMasterData.DataSource = productionDTOs;

        dgvMasterData.Columns[0].Frozen = true;
        dgvMasterData.Columns[1].Frozen = true;
        dgvMasterData.Columns[2].Frozen = true;
      }
    }


    private bool Compare(List<Production> data_old, ProductionDTO data_new)
    {
      var data = data_old?.FirstOrDefault(x => x.Name == data_new.Name && x.IsDelete == false);

      if (data == null) return false;

      return data.Tare_no_label_lowerlimit == data_new.Tare_no_label_lowerlimit &&
            data.Tare_no_label_standard == data_new.Tare_no_label_standard &&
            data.Tare_no_label_upperlimit == data_new.Tare_no_label_upperlimit &&

            data.Tare_with_label_lowerlimit == data_new.Tare_with_label_lowerlimit &&
            data.Tare_with_label_standard == data_new.Tare_with_label_standard &&
            data.Tare_with_label_upperlimit == data_new.Tare_with_label_upperlimit &&

            data.LowerLimitFinal == data_new.LowerLimitFinal &&
            data.StandardFinal == data_new.StandardFinal &&
            data.UpperLimitFinal == data_new.UpperLimitFinal &&

            data.PackSize == data_new.PackSize;
    }





    private bool isCreateOrUpdateIfExits(List<Production> listProductsOld, Production production)
    {
      try
      {
        bool isChange = false;
        if (listProductsOld == null) return true;
        if (listProductsOld?.Count <= 0) return true;
        var data = listProductsOld?.Where(x => x.Name == production.Name).FirstOrDefault();
        if (data == null) return true;

        isChange = (data.PackSize != production.PackSize) ||
                        (data.Density != production.Density) ||
                        (data.Tare_no_label_lowerlimit != production.Tare_no_label_lowerlimit) ||
                        (data.Tare_no_label_standard != production.Tare_no_label_standard) ||
                        (data.Tare_no_label_upperlimit != production.Tare_no_label_upperlimit) ||
                        (data.Tare_with_label_lowerlimit != production.Tare_with_label_lowerlimit) ||
                        (data.Tare_with_label_standard != production.Tare_with_label_standard) ||
                        (data.Tare_with_label_upperlimit != production.Tare_with_label_upperlimit) ||
                        (data.LowerLimitFinal != production.LowerLimitFinal) ||
                        (data.StandardFinal != production.StandardFinal) ||
                        (data.UpperLimitFinal != production.UpperLimitFinal);

        return isChange;
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
        return false;
      }
    }


    private void btnSaveChange_Click(object sender, EventArgs e)
    {
      FrmConfirmChangeMasterData frmConfirmChangeMasterData = new FrmConfirmChangeMasterData();
      frmConfirmChangeMasterData.OnSendOKClicked += FrmConfirmChangeMasterData_OnSendOKClicked;
      frmConfirmChangeMasterData.ShowDialog();
    }

    private async void FrmConfirmChangeMasterData_OnSendOKClicked(object sender, string contentChange)
    {
      try
      {
        //Update Data Old
        if (_production_ExitsDb?.Count() > 0)
        {
          foreach (var item in _products)
          {
            item.UpdatedAt = DateTime.Now;
            item.IsDelete = !(_production_ExitsDb.Select(x => x.Name).ToList().Contains(item.Name));
          }
        }
        else
        {
          _products.ForEach(x => x.UpdatedAt = DateTime.Now);
          _products.ForEach(x => x.IsDelete = true);
        }

        await AppCore.Ins.UpdateRange(_products);


        //Thêm dữ liệu mới
        if (_production_New?.Count() > 0)
        {
          var productNew = CvtClassDTO.ConvertToProductionList(_production_New);
          await AppCore.Ins.AddRange(productNew);
        }  
      }
      catch (Exception)
      {
        new FrmNotification().ShowMessage("Cập nhật dữ liệu sản phẩm thất bại !", eMsgType.Warning);
      }
      finally
      {
        VisibleSave(false);
        LoadProducts();
        new FrmNotification().ShowMessage("Cập nhật dữ liệu thành công !", eMsgType.Warning);
      }
    }

    private async void btnCheckHistorical_Click(object sender, EventArgs e)
    {
      if (AppCore.Ins.CheckRole(ePermit.Role_Setting_Product) || AppCore.Ins._roleCurrent.Name == "iSOFT")
      {
        List<HistoricalChangeMasterData> dataHistorical = await AppCore.Ins.LoadHistoricalChangeMasterData();
        FrmSeeHistoricalChangeMasterData frmSeeHistoricalChangeMasterData = new FrmSeeHistoricalChangeMasterData(dataHistorical);
        frmSeeHistoricalChangeMasterData.ShowDialog(this);
      }
      else
      {
        new FrmNotification().ShowMessage("Tài khoản không có quyền xem lịch sử thay đổi dữ liệu !", eMsgType.Warning);
      }
    }

    

    private void btnAdd_Click(object sender, EventArgs e)
    {
      if (!AppCore.Ins.CheckRole(ePermit.Role_Setting_Product))
      {
        new FrmNotification().ShowMessage("Tài khoản không có quyền thêm dữ liệu sản phẩm !", eMsgType.Warning);
        return;
      }

      FrmAddProduct.FrmAddProduct frmAddProduct = new FrmAddProduct.FrmAddProduct();
      frmAddProduct.OnSendDoneChange += FrmAddProductKL_OnSendDoneChange;
      frmAddProduct.ShowDialog(this);
    }

    private void FrmAddProductKL_OnSendDoneChange()
    {
      LoadProducts();
    }

    private void dgvMasterData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (!(e.RowIndex >= 0)) return;

      if (!AppCore.Ins.CheckRole(ePermit.Role_Setting_Product))
      {
        new FrmNotification().ShowMessage("Tài khoản không có quyền sửa dữ liệu sản phẩm !", eMsgType.Warning);
        return;
      }

      var dataSelect = dgvMasterData.Rows[e.RowIndex].DataBoundItem as ProductionDTO;

      if (dataSelect!=null)
      {
        Production productChoose = _productsSearch?.FirstOrDefault(x => x.Id == dataSelect.Id);

        if (productChoose != null)
        {
          FrmAddProduct.FrmAddProduct frmEditProduct = new FrmAddProduct.FrmAddProduct(productChoose);
          frmEditProduct.OnSendDoneChange += FrmEditProduct_OnSendDoneChange;
          frmEditProduct.ShowDialog(this);
        }
        else
        {
          new FrmNotification().ShowMessage("Không tìm thấy sản phẩm !", eMsgType.Warning);
          return;
        }  
      }
    }

    private void FrmEditProduct_OnSendDoneChange()
    {
      LoadProducts();
    }
  }
}
