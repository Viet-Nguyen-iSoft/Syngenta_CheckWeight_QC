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
using static SyngentaWeigherQC.eNum.eUI;
using static SyngentaWeigherQC.UI.FrmAddProduct.FrmAddProductCup;

namespace SyngentaWeigherQC.UI.FrmAddProduct
{
  public partial class FrmAddProductKL : Form
  {
    public delegate void SendAddOK();
    public event SendAddOK OnSendAddOK;


    public delegate void SendDoneUpdate();
    public event SendDoneUpdate OnSendDoneUpdate;

    private Production _productions;
    private eTypeAddProduct _eTypeAddProduct;

    public FrmAddProductKL()
    {
      InitializeComponent();
      _eTypeAddProduct = eTypeAddProduct.Add;
      this.btnAdd.Text = "Thêm";
    }

    
    public FrmAddProductKL(Production productions)
    {
      InitializeComponent();
      _productions = productions;
      _eTypeAddProduct = eTypeAddProduct.Edit;
      this.btnAdd.Text = "Cập nhật";

      this.txtDensityTareNoLabel.Enabled = false;
      this.txtDensityTareWithLabel.Enabled = false;

      this.txtTareMaxNoLabel.Enabled = true;
      this.txtTareMinNoLabel.Enabled = true;
      this.txtTareMaxWithLabel.Enabled = true;
      this.txtTareMinWithLabel.Enabled = true;
    }

    private double densityProduct = 0;
    private double capMin = 0;
    private double capTarget = 0;
    private double capMax = 0;

    private double densityTareNoLabel = 0;
    private double tareValueTargetNoLabel = 0;

    private double densityTareWithLabel = 0;
    private double tareValueTargetWithLabel = 0;

    private void FrmAddProductKL_Load(object sender, EventArgs e)
    {
      if (_eTypeAddProduct == eTypeAddProduct.Edit)
      {
        if (_productions != null)
        {
          densityProduct = _productions.Density;
          tareValueTargetNoLabel = _productions.Tare_no_label_standard;
          tareValueTargetWithLabel = _productions.Tare_with_label_standard;

          this.txtNameProduct.Text = _productions.Name;
          this.numericUpDownPackSize.Value = (int)_productions.PackSize;
          this.txtDensityProduct.Text = _productions.Density.ToString();

          this.txtTareMaxNoLabel.Text = _productions.Tare_with_label_upperlimit.ToString();
          this.txtTareTargetNoLabel.Text = _productions.Tare_with_label_standard.ToString();
          this.txtTareMinNoLabel.Text = _productions.Tare_with_label_lowerlimit.ToString();

          this.txtTareMaxWithLabel.Text = _productions.Tare_no_label_upperlimit.ToString();
          this.txtTareTargetWithLabel.Text = _productions.Tare_no_label_standard.ToString();
          this.txtTareMinWithLabel.Text = _productions.Tare_no_label_lowerlimit.ToString();

          this.txtFinalMax.Text = _productions.UpperLimitFinal.ToString();
          this.txtFinalTarget.Text = _productions.StandardFinal.ToString();
          this.txtFinalMin.Text = _productions.LowerLimitFinal.ToString();
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
        //Check Thỏa điều kiện
        var rs = !string.IsNullOrEmpty(txtNameProduct.Text) &&
                  double.TryParse(txtDensityProduct.Text, out densityProduct) &&
                  double.TryParse(txtCapMax.Text, out capMax) &&
                  double.TryParse(txtCapTarget.Text, out capTarget) &&
                  double.TryParse(txtCapMin.Text, out capMin) &&
                  double.TryParse(txtTareMaxNoLabel.Text, out densityTareNoLabel) &&
                  double.TryParse(txtTareMinNoLabel.Text, out densityTareNoLabel) &&
                  double.TryParse(txtTareTargetNoLabel.Text, out densityTareNoLabel) &&
                  double.TryParse(txtTareMaxWithLabel.Text, out densityTareWithLabel) &&
                  double.TryParse(txtTareMinWithLabel.Text, out densityTareWithLabel) &&
                  double.TryParse(txtTareTargetWithLabel.Text, out densityTareWithLabel);

        if (!rs)
        {
          new FrmNotification().ShowMessage("Thông tin điền vào không hợp lệ. \r\nVui lòng kiểm tra lại !", eMsgType.Warning);
          return;
        }

        _productions.Name = txtNameProduct.Text;
        _productions.LineCode = AppCore.Ins._stationCurrent?.Name;
        _productions.PackSize = (double)numericUpDownPackSize.Value;
        _productions.Density = Math.Round(double.Parse(txtDensityProduct.Text), 3);


        _productions.Tare_no_label_lowerlimit = Math.Round(double.Parse(txtTareMinNoLabel.Text), 3);
        _productions.Tare_no_label_standard = Math.Round(double.Parse(txtTareTargetNoLabel.Text), 3);
        _productions.Tare_no_label_upperlimit = Math.Round(double.Parse(txtTareMaxNoLabel.Text), 3);

        _productions.Tare_with_label_lowerlimit = Math.Round(double.Parse(txtTareMinWithLabel.Text), 3);
        _productions.Tare_with_label_standard = Math.Round(double.Parse(txtTareTargetWithLabel.Text), 3);
        _productions.Tare_with_label_upperlimit = Math.Round(double.Parse(txtTareMaxWithLabel.Text), 3);

        _productions.LowerLimitFinal = Math.Round(double.Parse(txtFinalMin.Text), 3);
        _productions.StandardFinal = Math.Round(double.Parse(txtFinalTarget.Text), 3);
        _productions.UpperLimitFinal = Math.Round(double.Parse(txtFinalMax.Text), 3);

        _productions.UpdatedAt = DateTime.Now;

        if (_productions != null)
          await AppCore.Ins.UpdateProduct(_productions);

        OnSendDoneUpdate?.Invoke();
        this.Close();
      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex);
        MessageBox.Show("Lỗi: " + ex.ToString());
      }
    }

    private async void AddProduct()
    {
      try
      {
        //Check Thỏa điều kiện
        var rs = !string.IsNullOrEmpty(txtNameProduct.Text) &&
                  double.TryParse(txtDensityProduct.Text, out densityProduct) &&
                  double.TryParse(txtCapMax.Text, out capMax) &&
                  double.TryParse(txtCapTarget.Text, out capTarget) &&
                  double.TryParse(txtCapMin.Text, out capMin) &&
                  double.TryParse(txtTareTargetNoLabel.Text, out tareValueTargetNoLabel) &&
                  double.TryParse(txtTareTargetWithLabel.Text, out tareValueTargetWithLabel) &&
                  double.TryParse(txtDensityTareNoLabel.Text, out densityTareNoLabel) &&
                  double.TryParse(txtDensityTareWithLabel.Text, out densityTareWithLabel);

        if (!rs)
        {
          new FrmNotification().ShowMessage("Thông tin điền vào không hợp lệ. \r\nVui lòng kiểm tra lại !", eMsgType.Warning);
          return;
        }



        Production production_data = new Production();

        production_data.Name = txtNameProduct.Text;
        production_data.LineCode = AppCore.Ins._stationCurrent?.Name;
        production_data.PackSize = (double)numericUpDownPackSize.Value;
        production_data.Density = densityProduct;


        production_data.Tare_no_label_lowerlimit = tareValueTargetNoLabel - densityTareNoLabel;
        production_data.Tare_no_label_standard = tareValueTargetNoLabel;
        production_data.Tare_no_label_upperlimit = tareValueTargetNoLabel + densityTareNoLabel;

        production_data.Tare_with_label_lowerlimit = tareValueTargetWithLabel - densityTareWithLabel;
        production_data.Tare_with_label_standard = tareValueTargetWithLabel;
        production_data.Tare_with_label_upperlimit = tareValueTargetWithLabel + densityTareWithLabel;

        production_data.LowerLimitFinal = Math.Round(densityProduct * capMin, 3);
        production_data.StandardFinal = Math.Round(densityProduct * capTarget, 3);
        production_data.UpperLimitFinal = Math.Round(densityProduct * capMax, 3);

        production_data.CreatedAt = DateTime.Now;
        production_data.UpdatedAt = DateTime.Now;

        production_data.IsDelete = false;

        if (production_data != null)
          await AppCore.Ins.AddProduct(production_data);

        OnSendAddOK?.Invoke();
        this.Close();
      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex);
        MessageBox.Show("Lỗi: " + ex.ToString());
      }
    }

    private void txtDensityProduct_TextChanged(object sender, EventArgs e)
    {
      var rs = double.TryParse(txtDensityProduct.Text, out densityProduct);

      if (rs)
      {
        this.txtDensityProduct.BackColor = Color.White;
        this.txtFinalMax.Text = Math.Round((capMax * densityProduct), 3).ToString();
        this.txtFinalTarget.Text = Math.Round((capTarget * densityProduct), 3).ToString();
        this.txtFinalMin.Text = Math.Round((capMin * densityProduct), 3).ToString();
      }
      else
      {
        this.txtDensityProduct.BackColor = Color.Red;

        this.txtFinalMax.Text = "NAN";
        this.txtFinalTarget.Text = "NAN";
        this.txtFinalMin.Text = "NAN";
      }
    }

    private void txtCapMax_TextChanged(object sender, EventArgs e)
    {
      var rs = double.TryParse(txtCapMax.Text, out capMax);
      if (rs)
      {
        this.txtCapMax.BackColor = Color.White;
        this.txtFinalMax.Text = Math.Round((capMax * densityProduct), 3).ToString();
      }
      else
      {
        this.txtCapMax.BackColor = Color.Red;
        this.txtFinalMax.Text = "NAN";
      }
    }

    private void txtCapTarget_TextChanged(object sender, EventArgs e)
    {
      var rs = double.TryParse(txtCapTarget.Text, out capTarget);
      if (rs)
      {
        this.txtCapMax.BackColor = Color.White;
        this.txtFinalTarget.Text = Math.Round((capTarget * densityProduct), 3).ToString();
      }
      else
      {
        this.txtCapMax.BackColor = Color.Red;
        this.txtFinalTarget.Text = "NAN";
      }
    }

    private void txtCapMin_TextChanged(object sender, EventArgs e)
    {
      var rs = double.TryParse(txtCapMin.Text, out capMin);
      if (rs)
      {
        this.txtCapMax.BackColor = Color.White;
        this.txtFinalMin.Text = Math.Round((capMin * densityProduct), 3).ToString();
      }
      else
      {
        this.txtCapMax.BackColor = Color.Red;
        this.txtFinalMin.Text = "NAN";
      }
    }

    private void txtTareTargetNoLabel_TextChanged(object sender, EventArgs e)
    {
      var rs = double.TryParse(txtTareTargetNoLabel.Text, out tareValueTargetNoLabel);
      this.txtTareTargetNoLabel.BackColor = rs ? Color.White : Color.Red;
      LoadValueTareNoLabel(rs);
    }

    private void txtDensityTareNoLabel_TextChanged(object sender, EventArgs e)
    {
      var rs = double.TryParse(txtDensityTareNoLabel.Text, out densityTareNoLabel);
      this.txtDensityTareNoLabel.BackColor = rs ? Color.White : Color.Red;
      LoadValueTareNoLabel(rs);
    }

    private void txtTareTargetWithLabel_TextChanged(object sender, EventArgs e)
    {
      var rs = double.TryParse(txtTareTargetWithLabel.Text, out tareValueTargetWithLabel);
      this.txtTareTargetWithLabel.BackColor = rs ? Color.White : Color.Red;
      LoadValueTareWithLabel(rs);
    }

    private void txtDensityTareWithLabel_TextChanged(object sender, EventArgs e)
    {
      var rs = double.TryParse(txtDensityTareWithLabel.Text, out densityTareWithLabel);
      this.txtDensityTareWithLabel.BackColor = rs ? Color.White : Color.Red;
      LoadValueTareWithLabel(rs);
    }

    private void LoadValueTareNoLabel(bool isOK)
    {
      if (isOK)
      {
        this.txtTareMaxNoLabel.Text = Math.Round((tareValueTargetNoLabel + densityTareNoLabel), 3).ToString();
        this.txtTareMinNoLabel.Text = Math.Round((tareValueTargetNoLabel - densityTareNoLabel), 3).ToString();
      }
      else
      {
        this.txtTareMaxNoLabel.Text = "NAN";
        this.txtTareMinNoLabel.Text = "NAN";
      }
    }

    private void LoadValueTareWithLabel(bool isOK)
    {
      if (isOK)
      {
        this.txtTareMaxWithLabel.Text = Math.Round((tareValueTargetWithLabel + densityTareWithLabel), 3).ToString();
        this.txtTareMinWithLabel.Text = Math.Round((tareValueTargetWithLabel - densityTareWithLabel), 3).ToString();
      }
      else
      {
        this.txtTareMaxWithLabel.Text = "NAN";
        this.txtTareMinWithLabel.Text = "NAN";
      }
    }

    
  }
}
