using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
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
using static SyngentaWeigherQC.eNum.enumSoftware;

namespace SyngentaWeigherQC.UI.FrmAddProduct
{
  public partial class FrmAddProduct : Form
  {
    public delegate void SendDoneChange();
    public event SendDoneChange OnSendDoneChange;

    private List<Production> _productions = new List<Production>();
    private Production _production;
    private eTypeAddProduct _eTypeAddProduct = eTypeAddProduct.Add;

    public FrmAddProduct()
    {
      InitializeComponent();
    }

    
    public FrmAddProduct(Production productions):this() 
    {
      _production = productions;
      _eTypeAddProduct = eTypeAddProduct.Edit;
      btnAdd.Text = "Cập nhật";
      txtNameProduct.Enabled = false;
    }

    private double density_product = 0;
    private double packsize = 0;

    private double weight_upper = 0;
    private double weight_target = 0;
    private double weight_lower = 0;

    private double weight_tare_no_label_upper = 0;
    private double weight_tare_no_label_target = 0;
    private double weight_tare_no_label_lower = 0;

    private double weight_tare_with_label_upper = 0;
    private double weight_tare_with_label_target = 0;
    private double weight_tare_with_label_lower = 0;

    private async void FrmAddProductKL_Load(object sender, EventArgs e)
    {
      _productions = await AppCore.Ins.LoadAllProducts();
      if (_productions?.Count > 0)
      {
        _productions = _productions
                    .Where(p => AppCore.Ins._listInforLine.Select(x => x.Id).ToList()
                    .Contains((int)p.InforLineId))
                    .OrderBy(x => x.LineCode)
                    .ToList();
      }

      ListLine = AppCore.Ins._listInforLine?.Where(x => x.IsEnable).ToList();
      cbbLine.SelectedIndex = -1;

      if (_eTypeAddProduct == eTypeAddProduct.Edit)
      {
        if (_production != null)
        {
          this.txtNameProduct.Text = _production.Name;
          this.txtPackSize.Text = _production.PackSize.ToString();
          this.txtDensityProduct.Text = _production.Density.ToString();

          this.txtTareMaxNoLabel.Text = _production.Tare_with_label_upperlimit.ToString();
          this.txtTareTargetNoLabel.Text = _production.Tare_with_label_standard.ToString();
          this.txtTareMinNoLabel.Text = _production.Tare_with_label_lowerlimit.ToString();

          this.txtTareMaxWithLabel.Text = _production.Tare_no_label_upperlimit.ToString();
          this.txtTareTargetWithLabel.Text = _production.Tare_no_label_standard.ToString();
          this.txtTareMinWithLabel.Text = _production.Tare_no_label_lowerlimit.ToString();

          this.txtFinalMax.Text = _production.UpperLimitFinal.ToString();
          this.txtFinalTarget.Text = _production.StandardFinal.ToString();
          this.txtFinalMin.Text = _production.LowerLimitFinal.ToString();

          this.cbbLine.SelectedItem = AppCore.Ins._listInforLine?.FirstOrDefault(x => x.Id== _production.InforLineId);
          this.cbbLine.Enabled = false;
        }
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      if (_eTypeAddProduct == eTypeAddProduct.Add)
      {
        AddProduct();
      } 
      else
      {
        UpdateProduct();
      }  
    }

    private async void UpdateProduct()
    {
      try
      {
        if (!ConditionFill())
        {
          new FrmNotification().ShowMessage("Thông tin điền vào không hợp lệ. \r\nVui lòng kiểm tra lại !", eMsgType.Warning);
          return;
        }

        _production.Name = txtNameProduct.Text;

        _production.PackSize = packsize;
        _production.Density = Math.Round(double.Parse(txtDensityProduct.Text), 3);

        _production.Tare_no_label_lowerlimit = Math.Round(double.Parse(txtTareMinNoLabel.Text), 3);
        _production.Tare_no_label_standard = Math.Round(double.Parse(txtTareTargetNoLabel.Text), 3);
        _production.Tare_no_label_upperlimit = Math.Round(double.Parse(txtTareMaxNoLabel.Text), 3);

        _production.Tare_with_label_lowerlimit = Math.Round(double.Parse(txtTareMinWithLabel.Text), 3);
        _production.Tare_with_label_standard = Math.Round(double.Parse(txtTareTargetWithLabel.Text), 3);
        _production.Tare_with_label_upperlimit = Math.Round(double.Parse(txtTareMaxWithLabel.Text), 3);

        _production.LowerLimitFinal = Math.Round(double.Parse(txtFinalMin.Text), 3);
        _production.StandardFinal = Math.Round(double.Parse(txtFinalTarget.Text), 3);
        _production.UpperLimitFinal = Math.Round(double.Parse(txtFinalMax.Text), 3);

        _production.UpdatedAt = DateTime.Now;

        if (_production != null)
          await AppCore.Ins.UpdateProduct(_production);

        OnSendDoneChange?.Invoke();
        this.Close();
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
        MessageBox.Show("Lỗi: " + ex.ToString());
      }
    }

    private async void AddProduct()
    {
      try
      {
        if (_productions.Any(x=>x.Name==txtNameProduct.Text))
        {
          new FrmNotification().ShowMessage("Tên sản phẩm đã tồn tại !", eMsgType.Warning);
          return;
        }  

        if (!ConditionFill())
        {
          new FrmNotification().ShowMessage("Thông tin điền vào không hợp lệ. \r\nVui lòng kiểm tra lại !", eMsgType.Warning);
          return;
        }

        Production production_data = new Production();

        production_data.Name = txtNameProduct.Text;
        production_data.PackSize = packsize;
        production_data.Density = density_product;


        production_data.Tare_no_label_lowerlimit = weight_tare_no_label_lower;
        production_data.Tare_no_label_standard = weight_tare_no_label_target;
        production_data.Tare_no_label_upperlimit = weight_tare_no_label_upper;

        production_data.Tare_with_label_lowerlimit = weight_tare_with_label_lower;
        production_data.Tare_with_label_standard = weight_tare_with_label_target;
        production_data.Tare_with_label_upperlimit = weight_tare_with_label_upper;

        production_data.LowerLimitFinal = weight_lower;
        production_data.StandardFinal = weight_target;
        production_data.UpperLimitFinal = weight_upper;

        production_data.IsDelete = false;
        production_data.InforLineId =  (cbbLine.SelectedItem as InforLine).Id;
        production_data.LineCode =  (cbbLine.SelectedItem as InforLine).Code;

        if (production_data != null)
          await AppCore.Ins.AddProduct(production_data);

        OnSendDoneChange?.Invoke();
        this.Close();
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
        MessageBox.Show("Lỗi: " + ex.ToString());
      }
    }


    private bool ConditionFill()
    {
      //Check Thỏa điều kiện
     return (!String.IsNullOrEmpty(txtNameProduct.Text)) &&

            double.TryParse(txtPackSize.Text, out packsize) &&
            double.TryParse(txtDensityProduct.Text, out density_product) &&

            double.TryParse(txtTareMaxNoLabel.Text, out weight_tare_no_label_upper) &&
            double.TryParse(txtTareTargetNoLabel.Text, out weight_tare_no_label_target) &&
            double.TryParse(txtTareMinNoLabel.Text, out weight_tare_no_label_lower) &&

            double.TryParse(txtTareMaxWithLabel.Text, out weight_tare_with_label_upper) &&
            double.TryParse(txtTareTargetWithLabel.Text, out weight_tare_with_label_target) &&
            double.TryParse(txtTareMinWithLabel.Text, out weight_tare_with_label_lower) &&
            
            double.TryParse(txtFinalMax.Text, out weight_upper) &&
            double.TryParse(txtFinalTarget.Text, out weight_target) &&
            double.TryParse(txtFinalMin.Text, out weight_lower) &&

            cbbLine.SelectedIndex != -1;
    }

    public List<InforLine> ListLine
    {
      set
      {
        this.cbbLine.DataSource = value;
        this.cbbLine.DisplayMember = "Name";
      }
    }
  }
}
