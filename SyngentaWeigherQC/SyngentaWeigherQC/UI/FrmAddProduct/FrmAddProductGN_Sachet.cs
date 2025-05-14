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
  public partial class FrmAddProductGN_Sachet : Form
  {
    public delegate void SendAddOK();
    public event SendAddOK OnSendAddOK;

    public delegate void SendDoneUpdate();
    public event SendDoneUpdate OnSendDoneUpdate;

    private Production _productions;
    private eTypeAddProduct _eTypeAddProduct;

    public FrmAddProductGN_Sachet()
    {
      InitializeComponent();
      _eTypeAddProduct = eTypeAddProduct.Add;
      this.btnAdd.Text = "Thêm";
    }
    public FrmAddProductGN_Sachet(Production productions)
    {
      InitializeComponent();
      this._eTypeAddProduct = eTypeAddProduct.Edit;
      this.btnAdd.Text = "Cập nhật";
      _productions = productions;
    }

   

    private double densityProduct = 0;
    private double capMin = 0;
    private double capTarget = 0;
    private double capMax = 0;
    private double tareValue = 0;

    private void FrmAddProductGN_Sachet_Load(object sender, EventArgs e)
    {
      if (_eTypeAddProduct == eTypeAddProduct.Edit)
      {
        if (_productions != null)
        {
          densityProduct = _productions.Density;
          tareValue = _productions.Tare_no_label_standard;
          
          this.txtNameProduct.Text = _productions.Name;
          this.numericUpDownPackSize.Value = (int)_productions.PackSize;
          this.txtDensity.Text = _productions.Density.ToString();



          this.txtTare.Text = _productions.Tare_with_label_upperlimit.ToString();

          this.txtFinalMax.Text = _productions.UpperLimitFinal.ToString();
          this.txtFinalTarget.Text = _productions.StandardFinal.ToString();
          this.txtFinalMin.Text = _productions.LowerLimitFinal.ToString();
        }
      }
    }

    private void txtDensity_TextChanged(object sender, EventArgs e)
    {
      var rs = double.TryParse(txtDensity.Text, out densityProduct);

      if (rs)
      {
        this.txtDensity.BackColor = Color.White;
        this.txtFinalMax.Text = (capMax * densityProduct).ToString();
        this.txtFinalTarget.Text = (capTarget * densityProduct).ToString();
        this.txtFinalMin.Text = (capMin * densityProduct).ToString();
      }
      else
      {
        this.txtDensity.BackColor = Color.Red;

        this.txtFinalMax.Text = "NAN";
        this.txtFinalTarget.Text = "NAN";
        this.txtFinalMin.Text = "NAN";
      }
    }

    private void textCapMax_TextChanged(object sender, EventArgs e)
    {
      var rs = double.TryParse(txtCapMax.Text, out capMax);
      if (rs)
      {
        this.txtCapMax.BackColor = Color.White;
        this.txtFinalMax.Text = (capMax * densityProduct).ToString();
      } 
      else
      {
        this.txtCapMax.BackColor = Color.Red;
        this.txtFinalMax.Text = "NAN";
      }  
    }

    private void textCapTarget_TextChanged(object sender, EventArgs e)
    {
      var rs = double.TryParse(txtCapTarget.Text, out capTarget);
      if (rs)
      {
        this.txtCapTarget.BackColor = Color.White;
        this.txtFinalTarget.Text = (capTarget * densityProduct).ToString();
      }
      else
      {
        this.txtCapTarget.BackColor = Color.Red;
        this.txtFinalTarget.Text = "NAN";
      }
    }

    private void textCapMin_TextChanged(object sender, EventArgs e)
    {
      var rs = double.TryParse(txtCapMin.Text, out capMin);
      if (rs)
      {
        this.txtCapMin.BackColor = Color.White;
        this.txtFinalMin.Text = (capMin * densityProduct).ToString();
      }
      else
      {
        this.txtCapMin.BackColor = Color.Red;
        this.txtFinalMin.Text = "NAN";
      }
    }

    private void txtTare_TextChanged(object sender, EventArgs e)
    {
      var rs = double.TryParse(txtTare.Text, out tareValue);
      if (rs)
      {
        this.txtTare.BackColor = Color.White;
      }
      else
      {
        this.txtTare.BackColor = Color.Red;
      }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      if (_eTypeAddProduct==eTypeAddProduct.Add)
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
                  double.TryParse(txtDensity.Text, out densityProduct) &&
                  double.TryParse(txtCapMax.Text, out capMax) &&
                  double.TryParse(txtCapTarget.Text, out capTarget) &&
                  double.TryParse(txtCapMin.Text, out capMin) &&
                  double.TryParse(txtTare.Text, out tareValue);

        if (!rs)
        {
          new FrmNotification().ShowMessage("Thông tin điền vào không hợp lệ. \r\nVui lòng kiểm tra lại !", eMsgType.Warning);
          return;
        }

        _productions.Name = txtNameProduct.Text;
        _productions.LineCode = AppCore.Ins._stationCurrent?.Name;
        _productions.PackSize = (double)numericUpDownPackSize.Value;
        _productions.Density = Math.Round(double.Parse(txtDensity.Text), 3);


        _productions.Tare_no_label_lowerlimit = Math.Round(double.Parse(txtTare.Text), 3);
        _productions.Tare_no_label_standard = Math.Round(double.Parse(txtTare.Text), 3);
        _productions.Tare_no_label_upperlimit = Math.Round(double.Parse(txtTare.Text), 3);

        _productions.Tare_with_label_lowerlimit = Math.Round(double.Parse(txtTare.Text), 3);
        _productions.Tare_with_label_standard = Math.Round(double.Parse(txtTare.Text), 3);
        _productions.Tare_with_label_upperlimit = Math.Round(double.Parse(txtTare.Text), 3);

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
        LoggerHelper.LogErrorToFileLog(ex);
        MessageBox.Show("Lỗi: " + ex.ToString());
      }
    }

    private async void AddProduct()
    {
      try
      {
        //Check Thỏa điều kiện
        var rs = !string.IsNullOrEmpty(txtNameProduct.Text) &&
                  double.TryParse(txtDensity.Text, out densityProduct) &&
                  double.TryParse(txtCapMax.Text, out capMax) &&
                  double.TryParse(txtCapTarget.Text, out capTarget) &&
                  double.TryParse(txtCapMin.Text, out capMin) &&
                  double.TryParse(txtTare.Text, out tareValue);

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


        production_data.Tare_no_label_lowerlimit = tareValue;
        production_data.Tare_no_label_standard = tareValue;
        production_data.Tare_no_label_upperlimit = tareValue;

        production_data.Tare_with_label_lowerlimit = tareValue;
        production_data.Tare_with_label_standard = tareValue;
        production_data.Tare_with_label_upperlimit = tareValue;

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
        LoggerHelper.LogErrorToFileLog(ex);
        MessageBox.Show("Lỗi: " + ex.ToString());
      }
    }


    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    
  }
}
