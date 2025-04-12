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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmSeeHistoricalChangeMasterData : Form
  {
    private List<HistoricalChangeMasterData> _listHistorical = new List<HistoricalChangeMasterData> ();
    public FrmSeeHistoricalChangeMasterData(List<HistoricalChangeMasterData> datas)
    {
      InitializeComponent();
      _listHistorical = datas;
    }

    private void FrmSeeHistoricalChangeMasterData_Load(object sender, EventArgs e)
    {
      try
      {
        if (_listHistorical.Count <= 0) return;
        _listHistorical.Reverse();
        for (int i = 0; i < _listHistorical.Count; i++)
        {
          int idUserAccoutUpdate = _listHistorical[i].UpdateBy;
          string nameAccoutUpdate = AppCore.Ins._listRoles?.Where(x => x.Id == idUserAccoutUpdate).Select(x => x.Name).FirstOrDefault();
          nameAccoutUpdate = (idUserAccoutUpdate == 0) ? "i-SOFT" : nameAccoutUpdate;

          int row = dataGridView1.Rows.Add();

          dataGridView1.Rows[row].Cells["Col1"].Value = _listHistorical.Count-i;
          dataGridView1.Rows[row].Cells["Col2"].Value = _listHistorical[i].CreatedAt;
          dataGridView1.Rows[row].Cells["Col3"].Value = _listHistorical[i].Reason;
          dataGridView1.Rows[row].Cells["Col4"].Value = nameAccoutUpdate;
        }

      }
      catch (Exception ex)
      {
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
      }
      
      
      
    }
  }
}
