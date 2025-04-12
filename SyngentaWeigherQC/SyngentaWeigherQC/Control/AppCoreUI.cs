using SyngentaWeigherQC.UI.FrmUI;
using System.Windows.Forms;

namespace SyngentaWeigherQC.Control
{
  public partial class AppCore
  {
    private void StartShowUI()
    {
      Application.Run(FormMain.Instance);
    }
  }
}
