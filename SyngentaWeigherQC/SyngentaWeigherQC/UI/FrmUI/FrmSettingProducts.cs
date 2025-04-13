using Aspose.Cells;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Irony.Parsing;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.UI.FrmAddProduct;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.eUI;
using static System.Net.Mime.MediaTypeNames;
using Color = System.Drawing.Color;
using Production = SyngentaWeigherQC.Models.Production;
using Workbook = Aspose.Cells.Workbook;
using Worksheet = Aspose.Cells.Worksheet;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmSettingProducts : Form
  {
    public FrmSettingProducts()
    {
      InitializeComponent();
    }

    #region Singleton parttern
    private static FrmSettingProducts _Instance = null;
    public static FrmSettingProducts Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new FrmSettingProducts();
        }
        return _Instance;
      }
    }









    #endregion

    private void FrmSettingProducts_Load(object sender, EventArgs e)
    {
      LoadProducts();

      InitEvent();
    }

    private void InitEvent()
    {
      FrmSettingLine.Instance.OnSendChangeLine += Instance_OnSendChangeLine;
    }

    private void Instance_OnSendChangeLine()
    {
      LoadProducts();
    }

    private List<Production> productions;
    private void LoadProducts()
    {
      productions = new List<Production>();
      var listStationEnable = AppCore.Ins._listInforLine?.Where(x=>x.IsEnable==true).ToList();
      if (listStationEnable.Count()>0)
      {
        foreach (var item in listStationEnable)
        {
          productions.AddRange(item.Productions);
        }
      }

      SearchData();
    }

   

    private void btnSearch_Click(object sender, EventArgs e)
    {
      SearchData();
    }

    private void SearchData()
    {
      string keyword = txtSearch.Texts;
      var result = productions?
                  .Where
                  (p =>
                    (!string.IsNullOrEmpty(p.LineCode) && p.LineCode.Contains(keyword)) ||
                    (!string.IsNullOrEmpty(p.Name) && p.Name.Contains(keyword))
                  )
                  .ToList();

      SetFDgv(result);
    }

    private void btnImportExcel_Click(object sender, EventArgs e)
    {
      if (AppCore.Ins.CheckRole(ePermit.role_ImportProduct))
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
      else
      {
        new FrmNotification().ShowMessage("Tài khoản không có quyền import dữ liệu sản phẩm !", eMsgType.Warning);
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
        PareXlsxByAspose(file_name);
        if (allDataProductExcel?.Count <= 0)
        {
          new FrmNotification().ShowMessage("Không có dữ liệu sản phẩm chuyền này !", eMsgType.Info);
        }
      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex);
        new FrmNotification().ShowMessage("File excel load thất bại !", eMsgType.Warning);
      }
      finally
      {
        VisibleSave(true);
      }
    }

    private void VisibleSave(bool isVis)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() => { VisibleSave(isVis); }));
        return;
      }

      this.btnSaveChange.Visible = isVis;
    }

    private void backgroundWorkerImport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      SetFDgv(allDataProductExcel, allDataProductExcelCreate);
      new FrmNotification().ShowMessage("File excel load thành công.", eMsgType.Info);
    }


    private void SetFDgv(List<Production> listProducts)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() => { SetFDgv(listProducts); }));
        return;
      }
      dgvMasterData.DataSource = null;
      if (listProducts.Count > 0)
      {
        listProducts = listProducts.OrderBy(x => x.LineCode).ToList();
        dgvMasterData.DataSource = listProducts;

        //dgvMasterData.Columns[0].Width = 600;
        //dgvMasterData.Columns[8].Width = 230;
        //dgvMasterData.Columns[dgvMasterData.ColumnCount - 1].Visible = false;
        //dgvMasterData.Columns[dgvMasterData.ColumnCount - 2].Visible = false;
        //dgvMasterData.Columns[dgvMasterData.ColumnCount - 3].Visible = false;

        //string nameLine = datas[0].LineCode;
        //if (nameLine.ToString().Contains("KL") || nameLine.ToString().Contains("SBL"))
        //{
          
        //}
        //else if (nameLine.ToString().Contains("CUP"))
        //{
        //  dgvMasterData.Columns[10].Visible = false;
        //  dgvMasterData.Columns[11].Visible = false;
        //  dgvMasterData.Columns[12].Visible = false;
        //}
        //else
        //{
        //  dgvMasterData.Columns[7].HeaderText = "Giá trị Tare chuẩn";
        //  dgvMasterData.Columns[8].Visible = false;
        //  dgvMasterData.Columns[9].Visible = false;
        //  dgvMasterData.Columns[10].Visible = false;
        //  dgvMasterData.Columns[11].Visible = false;
        //  dgvMasterData.Columns[12].Visible = false;
        //}
      }
    }

    private void SetFDgv(List<Production> listProducts, List<Production> listProductsUpdateOrCreate)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() => { SetFDgv(listProducts, listProductsUpdateOrCreate); }));
        return;
      }
      try
      {
        dgvMasterData.DataSource = null;
        if (listProducts.Count > 0)
        {
          dgvMasterData.DataSource = listProducts?.ToList();

          //dgvMasterData.Columns[0].Width = 600;
          //dgvMasterData.Columns[8].Width = 230;
          //dgvMasterData.Columns[dgvMasterData.ColumnCount - 1].Visible = false;
          //dgvMasterData.Columns[dgvMasterData.ColumnCount - 2].Visible = false;
          //dgvMasterData.Columns[dgvMasterData.ColumnCount - 3].Visible = false;

          //string nameLine = listProducts[0].LineCode;
          //if (nameLine.ToString().Contains("KL") || nameLine.ToString().Contains("SBL"))
          //{
          //  //Nothing
          //}
          //else if (nameLine.ToString().Contains("CUP"))
          //{
          //  dgvMasterData.Columns[10].Visible = false;
          //  dgvMasterData.Columns[11].Visible = false;
          //  dgvMasterData.Columns[12].Visible = false;
          //}
          //else
          //{
          //  dgvMasterData.Columns[7].HeaderText = "Giá trị Tare chuẩn";
          //  dgvMasterData.Columns[8].Visible = false;
          //  dgvMasterData.Columns[9].Visible = false;
          //  dgvMasterData.Columns[10].Visible = false;
          //  dgvMasterData.Columns[11].Visible = false;
          //  dgvMasterData.Columns[12].Visible = false;
          //}

          //highlight những data cập nhật hay có sự thay đổi
          //foreach (DataGridViewRow row in dgvMasterData.Rows)
          //{
          //  string nameProduct = row.Cells["NameProduct"].Value?.ToString();
          //  var exits = listProductsUpdateOrCreate?.Where(x => x.Name == nameProduct).FirstOrDefault();
          //  if (exits != null)
          //  {
          //    row.DefaultCellStyle.BackColor = Color.Yellow;
          //  }
          //}
        }
      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex);
      }
    }


    #region Read Exxcel
    private List<Production> allDataProductExcel = new List<Production>();
    private List<Production> allDataProductExcelCreate = new List<Production>();

    public void PareXlsxByAspose(string file_path)
    {
      try
      {
        List<Production> allProductDatasOld = AppCore.Ins._listProductsWithStation;

        allDataProductExcel = new List<Production>();
        allDataProductExcelCreate = new List<Production>();

        FileInfo dest_file_info = new FileInfo(file_path);
        Workbook wb = new Workbook(dest_file_info.FullName);
        WorksheetCollection collection = wb.Worksheets;
        int max_rows = 0; int max_cols = 0;

        for (int worksheetIndex = 0; worksheetIndex < collection.Count; worksheetIndex++)
        {
          Worksheet worksheet = collection[worksheetIndex];
          if (worksheet.Name.Trim().ToLower() == "data_kl" || worksheet.Name.Trim().ToLower() == "data_sbl")
          {
            max_rows = worksheet.Cells.MaxDataRow;
            max_cols = worksheet.Cells.MaxDataColumn;

            for (int row = 6; row <= max_rows; row++)
            {
              Production production_data = new Production();

              production_data.Name = GetTextNameProduct(worksheet, row, 0);
              production_data.LineCode = GetText(worksheet, row, 1);
              production_data.PackSize = GetDouble(worksheet, row, 2);
              production_data.Density = GetDouble(worksheet, row, 3);

              production_data.Tare_no_label_lowerlimit = GetDouble(worksheet, row, 16);
              production_data.Tare_no_label_standard = GetDouble(worksheet, row, 17);
              production_data.Tare_no_label_upperlimit = GetDouble(worksheet, row, 18);

              production_data.Tare_with_label_lowerlimit = GetDouble(worksheet, row, 19);
              production_data.Tare_with_label_standard = GetDouble(worksheet, row, 20);
              production_data.Tare_with_label_upperlimit = GetDouble(worksheet, row, 21);

              production_data.LowerLimitFinal = GetDouble(worksheet, row, 22);
              production_data.StandardFinal = GetDouble(worksheet, row, 23);
              production_data.UpperLimitFinal = GetDouble(worksheet, row, 24);

              production_data.CreatedAt = DateTime.Now;
              production_data.UpdatedAt = DateTime.Now;

              production_data.IsDelete = false;

              //Check Line
              var is_line = AppCore.Ins._listInforLine.FirstOrDefault(x=>x.Code == production_data.LineCode);
              if (is_line!=null)
              {
                production_data.InforLineId = is_line.Id;
                //Check phải dataline hiện tại
                bool isCreateOrUpdate = isCreateOrUpdateIfExits(allProductDatasOld, production_data);
                if (isCreateOrUpdate)
                {
                  //Có sự thay đổi
                  if (production_data.Name.Trim() != "") allDataProductExcelCreate.Add(production_data);
                }
                if (production_data.Name.Trim() != "") allDataProductExcel.Add(production_data);
              }  
            }
          }
          else if (worksheet.Name.Trim().ToLower() == "data_cup")
          {
            max_rows = worksheet.Cells.MaxDataRow;
            max_cols = worksheet.Cells.MaxDataColumn;

            for (int row = 6; row <= max_rows; row++)
            {
              Production production_data = new Production();

              production_data.Name = GetTextNameProduct(worksheet, row, 0);
              production_data.LineCode = GetText(worksheet, row, 1);
              production_data.PackSize = GetDouble(worksheet, row, 2);
              production_data.Density = GetDouble(worksheet, row, 3);


              production_data.Tare_no_label_lowerlimit = GetDouble(worksheet, row, 8);
              production_data.Tare_no_label_standard = GetDouble(worksheet, row, 9);
              production_data.Tare_no_label_upperlimit = GetDouble(worksheet, row, 10);

              production_data.Tare_with_label_lowerlimit = GetDouble(worksheet, row, 8);
              production_data.Tare_with_label_standard = GetDouble(worksheet, row, 9);
              production_data.Tare_with_label_upperlimit = GetDouble(worksheet, row, 10);

              production_data.LowerLimitFinal = GetDouble(worksheet, row, 11);
              production_data.StandardFinal = GetDouble(worksheet, row, 12);
              production_data.UpperLimitFinal = GetDouble(worksheet, row, 13);

              production_data.CreatedAt = DateTime.Now;
              production_data.UpdatedAt = DateTime.Now;

              production_data.IsDelete = false;

              //Check Line
              var is_line = AppCore.Ins._listInforLine.FirstOrDefault(x => x.Code == production_data.LineCode);
              if (is_line != null)
              {
                production_data.InforLineId = is_line.Id;
                //Check phải dataline hiện tại
                bool isCreateOrUpdate = isCreateOrUpdateIfExits(allProductDatasOld, production_data);
                if (isCreateOrUpdate)
                {
                  //Có sự thay đổi
                  if (production_data.Name.Trim() != "") allDataProductExcelCreate.Add(production_data);
                }
                if (production_data.Name.Trim() != "") allDataProductExcel.Add(production_data);
              }
            }
          }
          else if (worksheet.Name.Trim().ToLower() == "data_sachet" || worksheet.Name.Trim().ToLower() == "data_gn")
          {
            max_rows = worksheet.Cells.MaxDataRow;
            max_cols = worksheet.Cells.MaxDataColumn;

            for (int row = 6; row <= max_rows; row++)
            {
              Production production_data = new Production();

              production_data.Name = GetTextNameProduct(worksheet, row, 0);
              production_data.LineCode = GetText(worksheet, row, 1);
              production_data.PackSize = GetDouble(worksheet, row, 2);
              production_data.Density = GetDouble(worksheet, row, 3);


              production_data.Tare_no_label_lowerlimit = GetDouble(worksheet, row, 7);
              production_data.Tare_no_label_standard = GetDouble(worksheet, row, 7);
              production_data.Tare_no_label_upperlimit = GetDouble(worksheet, row, 7);

              production_data.Tare_with_label_lowerlimit = GetDouble(worksheet, row, 7);
              production_data.Tare_with_label_standard = GetDouble(worksheet, row, 7);
              production_data.Tare_with_label_upperlimit = GetDouble(worksheet, row, 7);

              production_data.LowerLimitFinal = GetDouble(worksheet, row, 8);
              production_data.StandardFinal = GetDouble(worksheet, row, 9);
              production_data.UpperLimitFinal = GetDouble(worksheet, row, 10);

              production_data.CreatedAt = DateTime.Now;
              production_data.UpdatedAt = DateTime.Now;

              production_data.IsDelete = false;

              //Check Line
              var is_line = AppCore.Ins._listInforLine.FirstOrDefault(x => x.Code == production_data.LineCode);
              if (is_line != null)
              {
                production_data.InforLineId = is_line.Id;
                //Check phải dataline hiện tại
                bool isCreateOrUpdate = isCreateOrUpdateIfExits(allProductDatasOld, production_data);
                if (isCreateOrUpdate)
                {
                  //Có sự thay đổi
                  if (production_data.Name.Trim() != "") allDataProductExcelCreate.Add(production_data);
                }
                if (production_data.Name.Trim() != "") allDataProductExcel.Add(production_data);
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex);
        new FrmNotification().ShowMessage("File excel load thất bại !", eMsgType.Warning);
      }
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
        eLoggerHelper.LogErrorToFileLog(ex);
        return false;
      }
      
    }

    public List<Production> PareXlsxByAsposeOld(string file_path)
    {
      try
      {
        List<Production> allDataProductExcel = new List<Production>();

        FileInfo dest_file_info = new FileInfo(file_path);
        Workbook wb = new Workbook(dest_file_info.FullName);
        WorksheetCollection collection = wb.Worksheets;
        bool is_exit_loop = false;
        int max_rows = 0; int max_cols = 0;

        for (int worksheetIndex = 0; worksheetIndex < collection.Count && is_exit_loop == false; worksheetIndex++)
        {
          Worksheet worksheet = collection[worksheetIndex];
          if (worksheet.Name.Trim().ToLower() == "data_kl" || worksheet.Name.Trim().ToLower() == "data_sbl")
          {
            max_rows = worksheet.Cells.MaxDataRow;
            max_cols = worksheet.Cells.MaxDataColumn;

            for (int row = 6; row <= max_rows; row++)
            {
              Production production_data = new Production();

              production_data.Name = GetTextNameProduct(worksheet, row, 0);
              production_data.LineCode = GetText(worksheet, row, 1);
              production_data.PackSize = GetDouble(worksheet, row, 2);
              production_data.Density = GetDouble(worksheet, row, 3);


              production_data.Tare_no_label_lowerlimit = GetDouble(worksheet, row, 16);
              production_data.Tare_no_label_standard = GetDouble(worksheet, row, 17);
              production_data.Tare_no_label_upperlimit = GetDouble(worksheet, row, 18);

              production_data.Tare_with_label_lowerlimit = GetDouble(worksheet, row, 19);
              production_data.Tare_with_label_standard = GetDouble(worksheet, row, 20);
              production_data.Tare_with_label_upperlimit = GetDouble(worksheet, row, 21);

              production_data.LowerLimitFinal = GetDouble(worksheet, row, 22);
              production_data.StandardFinal = GetDouble(worksheet, row, 23);
              production_data.UpperLimitFinal = GetDouble(worksheet, row, 24);

              production_data.CreatedAt = DateTime.Now;
              production_data.UpdatedAt = DateTime.Now;

              production_data.IsDelete = false;

              if (production_data.Name.Trim() != "") allDataProductExcel.Add(production_data);
            }
          }
          else if (worksheet.Name.Trim().ToLower() == "data_cup")
          {
            max_rows = worksheet.Cells.MaxDataRow;
            max_cols = worksheet.Cells.MaxDataColumn;

            for (int row = 6; row <= max_rows; row++)
            {
              Production production_data = new Production();

              production_data.Name = GetTextNameProduct(worksheet, row, 0);
              production_data.LineCode = GetText(worksheet, row, 1);
              production_data.PackSize = GetDouble(worksheet, row, 2);
              production_data.Density = GetDouble(worksheet, row, 3);


              production_data.Tare_no_label_lowerlimit = GetDouble(worksheet, row, 8);
              production_data.Tare_no_label_standard = GetDouble(worksheet, row, 9);
              production_data.Tare_no_label_upperlimit = GetDouble(worksheet, row, 10);

              production_data.Tare_with_label_lowerlimit = GetDouble(worksheet, row, 8);
              production_data.Tare_with_label_standard = GetDouble(worksheet, row, 9);
              production_data.Tare_with_label_upperlimit = GetDouble(worksheet, row, 10);

              production_data.LowerLimitFinal = GetDouble(worksheet, row, 11);
              production_data.StandardFinal = GetDouble(worksheet, row, 12);
              production_data.UpperLimitFinal = GetDouble(worksheet, row, 13);

              production_data.CreatedAt = DateTime.Now;
              production_data.UpdatedAt = DateTime.Now;

              production_data.IsDelete = false;

              if (production_data.Name.Trim() != "") allDataProductExcel.Add(production_data);
            }
          }
          else if (worksheet.Name.Trim().ToLower() == "data_sachet" || worksheet.Name.Trim().ToLower() == "data_gn")
          {
            max_rows = worksheet.Cells.MaxDataRow;
            max_cols = worksheet.Cells.MaxDataColumn;

            for (int row = 6; row <= max_rows; row++)
            {
              Production production_data = new Production();

              production_data.Name = GetTextNameProduct(worksheet, row, 0);
              production_data.LineCode = GetText(worksheet, row, 1);
              production_data.PackSize = GetDouble(worksheet, row, 2);
              production_data.Density = GetDouble(worksheet, row, 3);



              production_data.Tare_no_label_lowerlimit = GetDouble(worksheet, row, 7);
              production_data.Tare_no_label_standard = GetDouble(worksheet, row, 7);
              production_data.Tare_no_label_upperlimit = GetDouble(worksheet, row, 7);

              production_data.Tare_with_label_lowerlimit = GetDouble(worksheet, row, 7);
              production_data.Tare_with_label_standard = GetDouble(worksheet, row, 7);
              production_data.Tare_with_label_upperlimit = GetDouble(worksheet, row, 7);

              production_data.LowerLimitFinal = GetDouble(worksheet, row, 8);
              production_data.StandardFinal = GetDouble(worksheet, row, 9);
              production_data.UpperLimitFinal = GetDouble(worksheet, row, 10);

              production_data.CreatedAt = DateTime.Now;
              production_data.UpdatedAt = DateTime.Now;

              production_data.IsDelete = false;

              if (production_data.Name.Trim() != "") allDataProductExcel.Add(production_data);
            }
          }
        }

        return allDataProductExcel;
      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex);
        new FrmNotification().ShowMessage("File excel load thất bại !", eMsgType.Warning);
        return null;
      }
    }

    private static string GetText(Worksheet worksheet, int row, int column)
    {
      string ret = "";
      try
      {
        object textObj = worksheet.Cells[row, column].Value;
        if (textObj != null)
          ret = textObj.ToString().Trim();
      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex);
      }
      return ret;
    }

    private string GetTextNameProduct(Worksheet worksheet, int row, int column)
    {
      string ret = "";
      try
      {
        object textObj = worksheet.Cells[row, column].Value;
        if (textObj != null)
        {
          ret = textObj.ToString().Trim();
          ret = RemoveTextInQuotes(ret);
        }  
          
      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex);
      }
      return ret;
    }

    private string RemoveTextInQuotes(string input)
    {
      int startIndex = input.IndexOf('<');
      int endIndex = input.LastIndexOf('>');
      if (startIndex >= 0 && endIndex >= 0 && endIndex > startIndex)
      {
        return input.Remove(startIndex, endIndex - startIndex + 1);
      }
      return input;
    }

    private Double GetDouble(Worksheet worksheet, int row, int column)
    {
      double value = 0;
      try
      {
        object textObj = worksheet.Cells[row, column].Value;
        if (textObj != null)
        {
          Double.TryParse(textObj.ToString(), out value);
        }
      }
      catch (Exception ex) 
      {
        eLoggerHelper.LogErrorToFileLog(ex);
      }
      return Math.Round(value, 3);
    }
    #endregion

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
        await AppCore.Ins.SaveContentChangeMasterData(contentChange);
        await AppCore.Ins.UpdateMasterDataOld(allDataProductExcelCreate);
        await AppCore.Ins.AddRangeProducts(allDataProductExcelCreate);

        new FrmNotification().ShowMessage("Cập nhật dữ liệu sản phẩm thành công.", eMsgType.Info);
        //FrmHome.Instance.ChangeProduct(true);

        VisibleSave(false);
        SetFDgv(allDataProductExcel);
      }
      catch (Exception)
      {
        new FrmNotification().ShowMessage("Cập nhật dữ liệu sản phẩm thất bại !", eMsgType.Warning);
      }
    }

    private async void btnCheckHistorical_Click(object sender, EventArgs e)
    {
      if (AppCore.Ins.CheckRole(ePermit.role_ImportProduct) || AppCore.Ins._roleCurrent.Name == "iSOFT")
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

    private void btnAdd_Click(object sender, EventArgs e)
    {
      if (!AppCore.Ins.CheckRole(ePermit.role_ImportProduct))
      {
        new FrmNotification().ShowMessage("Tài khoản không có quyền thêm dữ liệu sản phẩm !", eMsgType.Warning);
        return;
      }
      

      if (AppCore.Ins._stationCurrent!=null)
      {
        if ((AppCore.Ins._stationCurrent.Name?.ToLower()).Contains("cup"))
        {
          FrmAddProductCup frmAddProductCup = new FrmAddProductCup();
          frmAddProductCup.OnSendDone += FrmAddProductCup_OnSendAddOK;
          frmAddProductCup.ShowDialog(this);
        }
        else if ((AppCore.Ins._stationCurrent.Name?.ToLower()).Contains("kl") || (AppCore.Ins._stationCurrent.Name?.ToLower()).Contains("sbl"))
        {
          FrmAddProductKL frmAddProductKL = new FrmAddProductKL();
          frmAddProductKL.OnSendAddOK += FrmAddProductCup_OnSendAddOK;
          frmAddProductKL.ShowDialog(this);
        }
        else if ((AppCore.Ins._stationCurrent.Name?.ToLower()).Contains("sachet") || (AppCore.Ins._stationCurrent.Name?.ToLower()).Contains("gn"))
        {
          FrmAddProductGN_Sachet frmAddProductGN_Sachet = new FrmAddProductGN_Sachet();
          frmAddProductGN_Sachet.OnSendAddOK += FrmAddProductCup_OnSendAddOK;
          frmAddProductGN_Sachet.ShowDialog(this);
        }

      }  
    }

    private void FrmAddProductCup_OnSendAddOK()
    {
      SetFDgv(AppCore.Ins._listProductsWithStation);
    }

    private void dgvMasterData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (!(e.RowIndex >= 0)) return;

      if (!AppCore.Ins.CheckRole(ePermit.role_ImportProduct))
      {
        new FrmNotification().ShowMessage("Tài khoản không có quyền sửa dữ liệu sản phẩm !", eMsgType.Warning);
        return;
      }

      Production productions = AppCore.Ins._listProductsWithStation[e.RowIndex];

      if (AppCore.Ins._stationCurrent != null && productions!=null)
      {
        if ((AppCore.Ins._stationCurrent.Name?.ToLower()).Contains("cup"))
        {
          FrmAddProductCup frmAddProductCup = new FrmAddProductCup(productions);
          frmAddProductCup.OnSendDoneUpdate += FrmAddProductCup_OnSendDoneUpdate;
          frmAddProductCup.ShowDialog(this);
        }
        else if ((AppCore.Ins._stationCurrent.Name?.ToLower()).Contains("kl") || (AppCore.Ins._stationCurrent.Name?.ToLower()).Contains("sbl"))
        {
          FrmAddProductKL frmAddProductKL = new FrmAddProductKL(productions);
          frmAddProductKL.OnSendDoneUpdate += FrmAddProductKL_OnSendDoneUpdate;
          frmAddProductKL.ShowDialog(this);
        }
        else if ((AppCore.Ins._stationCurrent.Name?.ToLower()).Contains("sachet") || (AppCore.Ins._stationCurrent.Name?.ToLower()).Contains("gn"))
        {
          FrmAddProductGN_Sachet frmAddProductGN_Sachet = new FrmAddProductGN_Sachet(productions);
          frmAddProductGN_Sachet.OnSendDoneUpdate += FrmAddProductGN_Sachet_OnSendDoneUpdate;
          frmAddProductGN_Sachet.ShowDialog(this);
        }
      }

    }

    private void FrmAddProductGN_Sachet_OnSendDoneUpdate()
    {
      SetFDgv(AppCore.Ins._listProductsWithStation);
    }

    private void FrmAddProductKL_OnSendDoneUpdate()
    {
      SetFDgv(AppCore.Ins._listProductsWithStation);
    }

    private void FrmAddProductCup_OnSendDoneUpdate()
    {
      SetFDgv(AppCore.Ins._listProductsWithStation);
    }

   
  }
}
