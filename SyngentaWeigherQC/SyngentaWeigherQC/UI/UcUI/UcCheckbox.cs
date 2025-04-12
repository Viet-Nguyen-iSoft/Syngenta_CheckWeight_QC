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
    [Category("Custom Pros")]
    public bool IsCheck()
    {
      return check.Checked;
    }

    [Category("Custom Pros")]
    public bool SetCheck
    {
      set { check.Checked = value; }
    }
  }
}
