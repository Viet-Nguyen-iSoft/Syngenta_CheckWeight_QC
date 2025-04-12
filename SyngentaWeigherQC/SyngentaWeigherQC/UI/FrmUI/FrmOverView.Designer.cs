namespace SyngentaWeigherQC.UI.FrmUI
{
  partial class FrmOverView
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.flowLayoutPanelLine = new System.Windows.Forms.FlowLayoutPanel();
      this.SuspendLayout();
      // 
      // flowLayoutPanelLine
      // 
      this.flowLayoutPanelLine.AutoScroll = true;
      this.flowLayoutPanelLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(17)))), ((int)(((byte)(55)))));
      this.flowLayoutPanelLine.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flowLayoutPanelLine.Location = new System.Drawing.Point(0, 0);
      this.flowLayoutPanelLine.Name = "flowLayoutPanelLine";
      this.flowLayoutPanelLine.Size = new System.Drawing.Size(1140, 703);
      this.flowLayoutPanelLine.TabIndex = 0;
      // 
      // FrmOverView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1140, 703);
      this.Controls.Add(this.flowLayoutPanelLine);
      this.Name = "FrmOverView";
      this.Text = "FrmOverView";
      this.Load += new System.EventHandler(this.FrmOverView_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelLine;
  }
}