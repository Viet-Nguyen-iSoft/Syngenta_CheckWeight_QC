using System.Drawing;
using System.Windows.Forms;

namespace SynCheckWeigherLoggerApp.DashboardViews
{
  public partial class DataInfor : UserControl
  {
    private string _title = "";
    public DataInfor()
    {
      InitializeComponent();
    }

    public string Title
    {
      get { return _title; }
      set
      {
        _title = value;
        this.lblTitle.Text = value;
      }
    }

    public string SetTitle
    {
      set
      {
        this.lblTitle.Text = value;
      }
    }

    public void ClearAll()
    {
      this.label1.Text = "";
      this.label2.Text = "";
      this.label3.Text = "";
      this.label1.BackColor = Color.White;
      this.label2.BackColor = Color.White;
      this.label3.BackColor = Color.White;
    }



    public void SetText(int id, string text)
    {
      if (id == 1) this.label1.Text = text;
      else if (id == 2) this.label2.Text = text;
      else if (id == 3) this.label3.Text = text;
    }

    public void SetBackColor(int id, Color color)
    {
      if (id == 1) this.label1.BackColor = color;
      else if (id == 2) this.label2.BackColor = color;
      else if (id == 3) this.label3.BackColor = color;
    }

    public void SetForeColor(int id, Color color)
    {
      if (id == 1) this.label1.ForeColor = color;
      else if (id == 2) this.label2.ForeColor = color;
      else if (id == 3) this.label3.ForeColor = color;
    }

    public string GetText(int id)
    {
      string text = "";
      if (id == 1) text = this.label1.Text;
      else if (id == 2) text = (this.label2.Text);
      else if (id == 3) text = (this.label3.Text);
      return text;
    }

    public Color GetBackColor(int id)
    {
      Color color = Color.Gray;
      if (id == 1) color = this.label1.BackColor;
      else if (id == 2) color = this.label2.BackColor;
      else if (id == 3) color = this.label3.BackColor;
      return color;
    }

    public Color GetForeColor(int id)
    {
      Color color = Color.Gray;
      if (id == 1) color = this.label1.ForeColor;
      else if (id == 2) color = this.label2.ForeColor;
      else if (id == 3) color = this.label3.ForeColor;
      return color;
    }



  }
}
