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

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmSeeHistoricalReweigher : Form
  {
    public FrmSeeHistoricalReweigher(Sample sample)
    {
      InitializeComponent();

      lblOldSampleValue.Text = sample.PreviouValue.ToString();
      lblNewValue.Text = sample.Value.ToString();
      lbDateCreate.Text = ((DateTime)sample.CreatedAt).ToString("HH:mm:ss dd/MM/yyyy");
      lbDateUpdate.Text = ((DateTime)sample.UpdatedAt).ToString("HH:mm:ss dd/MM/yyyy");
    }



  }
}
