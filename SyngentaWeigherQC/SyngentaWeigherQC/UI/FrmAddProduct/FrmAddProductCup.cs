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
  public partial class FrmAddProductCup : Form
  {
    public delegate void SendDone();
    public event SendDone OnSendDone;

    public delegate void SendDoneUpdate();
    public event SendDoneUpdate OnSendDoneUpdate;

    public FrmAddProductCup()
    {
      InitializeComponent();
      _eTypeAddProduct = eTypeAddProduct.Add;
      this.btnAdd.Text = "Thêm";
    }

    private Production _productions;
    private eTypeAddProduct _eTypeAddProduct;
    public FrmAddProductCup(Production productions)
    {
      InitializeComponent();
      _productions = productions;
      this._eTypeAddProduct = eTypeAddProduct.Edit;
      this.btnAdd.Text = "Cập nhật";
      this.txtTareMax.Enabled = true;
      this.txtTareMin.Enabled = true;
      this.txtDensityTare.Enabled = false;
    }

    private double densityProduct = 0;
    private double capMin = 0;
    private double capTarget = 0;
    private double capMax = 0;

    private double densityTare = 0;
    private double tareValueTarget = 0;

    private void FrmAddProductCup_Load(object sender, EventArgs e)
    {
      if (_eTypeAddProduct==eTypeAddProduct.Edit)
      {
        if (_productions!=null)
        {
          densityProduct = _productions.Density;
          tareValueTarget = _productions.Tare_no_label_standard;

          this.txtNameProduct.Text = _productions.Name;
          this.numericUpDownPackSize.Value = (int)_productions.PackSize;
          this.txtDensity.Text = _productions.Density.ToString();
          

          this.txtTareMax.Text = _productions.Tare_no_label_upperlimit.ToString();
          this.txtTareTarget.Text = _productions.Tare_no_label_standard.ToString();
          this.txtTareMin.Text = _productions.Tare_no_label_lowerlimit.ToString();

          this.txtFinalMax.Text = _productions.UpperLimitFinal.ToString();
          this.txtFinalTarget.Text = _productions.StandardFinal.ToString();
          this.txtFinalMin.Text = _productions.LowerLimitFinal.ToString();

        }  
      }

      this.txtDensity.TextChanged += txtDensity_TextChanged;

      this.txtCapMax.TextChanged += txtCapMax_TextChanged;
      this.txtCapTarget.TextChanged += txtCapTarget_TextChanged;
      this.txtCapMin.TextChanged += txtCapMin_TextChanged;

      this.txtTareTarget.TextChanged += txtTareTarget_TextChanged;
      this.txtDensityTare.TextChanged += txtDensityTare_TextChanged;
    }

   

   

    private void txtDensity_TextChanged(object sender, EventArgs e)
    {
      var rs = double.TryParse(txtDensity.Text, out densityProduct);

      if (rs)
      {
        this.txtDensity.BackColor = Color.White;
        this.txtFinalMax.Text = Math.Round((capMax * densityProduct), 3).ToString();
        this.txtFinalTarget.Text = Math.Round((capTarget * densityProduct), 3).ToString();
        this.txtFinalMin.Text = Math.Round((capMin * densityProduct), 3).ToString();
      }
      else
      {
        this.txtDensity.BackColor = Color.Red;

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
        this.txtFinalTarget.Text = Math.Round((capTarget * densityProduct),3).ToString();
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

   
    /// <summary>
    /// /
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void txtTareTarget_TextChanged(object sender, EventArgs e)
    {
      var rs = double.TryParse(txtTareTarget.Text, out tareValueTarget);
      LoadValueTare(rs);
    }

    private void txtDensityTare_TextChanged(object sender, EventArgs e)
    {
      var rs = double.TryParse(txtDensityTare.Text, out densityTare);
      LoadValueTare(rs);
    }


    private void LoadValueTare(bool isOK)
    {
      if (isOK)
      {
        this.txtDensityTare.BackColor = Color.White;
        this.txtTareMax.Text = Math.Round((tareValueTarget + densityTare),3).ToString();
        this.txtTareMin.Text = Math.Round((tareValueTarget - densityTare),3).ToString();
      }
      else
      {
        this.txtDensityTare.BackColor = Color.Red;
        this.txtTareMax.Text = "NAN";
        this.txtTareMin.Text = "NAN";
      }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      if (_eTypeAddProduct == eTypeAddProduct.Add)
      {
        Add();
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
                  double.TryParse(txtTareMin.Text, out densityTare) &&
                  double.TryParse(txtTareMax.Text, out densityTare) &&
                  double.TryParse(txtTareTarget.Text, out tareValueTarget);

        if (!rs)
        {
          new FrmNotification().ShowMessage("Thông tin điền vào không hợp lệ. \r\nVui lòng kiểm tra lại !", eMsgType.Warning);
          return;
        }

        _productions.Name = txtNameProduct.Text;
        _productions.LineCode = AppCore.Ins._stationCurrent?.Name;
        _productions.PackSize = (double)numericUpDownPackSize.Value;
        _productions.Density = Math.Round(double.Parse(txtDensity.Text), 3);


        _productions.Tare_no_label_lowerlimit = Math.Round(double.Parse(txtTareMin.Text), 3);
        _productions.Tare_no_label_standard = Math.Round(double.Parse(txtTareTarget.Text), 3);
        _productions.Tare_no_label_upperlimit = Math.Round(double.Parse(txtTareMax.Text), 3);

        _productions.Tare_with_label_lowerlimit = Math.Round(double.Parse(txtTareMin.Text), 3);
        _productions.Tare_with_label_standard = Math.Round(double.Parse(txtTareTarget.Text), 3);
        _productions.Tare_with_label_upperlimit = Math.Round(double.Parse(txtTareMax.Text), 3);

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


    private async void Add()
    {
      try
      {
        //Check Thỏa điều kiện
        var rs = !string.IsNullOrEmpty(txtNameProduct.Text) &&
                  double.TryParse(txtDensity.Text, out densityProduct) &&
                  double.TryParse(txtCapMax.Text, out capMax) &&
                  double.TryParse(txtCapTarget.Text, out capTarget) &&
                  double.TryParse(txtCapMin.Text, out capMin) &&
                  double.TryParse(txtDensityTare.Text, out densityTare) &&
                  double.TryParse(txtTareTarget.Text, out tareValueTarget);

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

      
        production_data.Tare_no_label_lowerlimit = tareValueTarget - densityTare;
        production_data.Tare_no_label_standard = tareValueTarget;
        production_data.Tare_no_label_upperlimit = tareValueTarget + densityTare;

        production_data.Tare_with_label_lowerlimit = tareValueTarget - densityTare;
        production_data.Tare_with_label_standard = tareValueTarget;
        production_data.Tare_with_label_upperlimit = tareValueTarget + densityTare;

        production_data.LowerLimitFinal = Math.Round(densityProduct * capMin, 3);
        production_data.StandardFinal = Math.Round(densityProduct * capTarget, 3);
        production_data.UpperLimitFinal = Math.Round(densityProduct * capMax, 3);

        production_data.CreatedAt = DateTime.Now;
        production_data.UpdatedAt = DateTime.Now;

        production_data.IsDelete = false;

        if (production_data != null)
          await AppCore.Ins.AddProduct(production_data);

        OnSendDone?.Invoke();
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
