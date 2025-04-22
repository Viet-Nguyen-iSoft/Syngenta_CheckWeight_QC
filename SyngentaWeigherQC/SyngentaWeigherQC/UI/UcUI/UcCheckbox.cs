using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyngentaWeigherQC.UI.UcUI
{
  public partial class UcCheckbox : UserControl
  {
    public UcCheckbox()
    {
      InitializeComponent();
    }
    public bool IsCheckData = false;
    public bool IsCheck()
    {
      return this.IsCheckData;
    }


    public void SetCheck(bool isCheck)
    {
      this.IsCheckData = isCheck;
      SetStatus(this.IsCheckData);
    }

    private void SetStatus(bool isCheck)
    {
      pictureBox1.Image = (isCheck == true) ?
                                AppCore.Ins.ByteArrayToImage(Properties.Resources.tick) :
                                AppCore.Ins.ByteArrayToImage(Properties.Resources.no_tick);
    }

    public void pictureBox1_Click(object sender, EventArgs e)
    {
      this.IsCheckData = !this.IsCheckData;
      SetStatus(this.IsCheckData);
    }
  }
}
