namespace SyngentaWeigherQC.UI.UcUI
{
  partial class UcLine
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.lbStationName = new System.Windows.Forms.Label();
      this.picStation = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.picStation)).BeginInit();
      this.SuspendLayout();
      // 
      // lbStationName
      // 
      this.lbStationName.BackColor = System.Drawing.Color.Silver;
      this.lbStationName.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbStationName.ForeColor = System.Drawing.Color.Black;
      this.lbStationName.Location = new System.Drawing.Point(15, 125);
      this.lbStationName.Name = "lbStationName";
      this.lbStationName.Size = new System.Drawing.Size(113, 28);
      this.lbStationName.TabIndex = 3;
      this.lbStationName.Text = "line";
      this.lbStationName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbStationName.Click += new System.EventHandler(this.lbStationName_Click);
      // 
      // picStation
      // 
      this.picStation.Image = global::SyngentaWeigherQC.Properties.Resources.Weigher;
      this.picStation.Location = new System.Drawing.Point(16, 9);
      this.picStation.Name = "picStation";
      this.picStation.Size = new System.Drawing.Size(113, 116);
      this.picStation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.picStation.TabIndex = 2;
      this.picStation.TabStop = false;
      this.picStation.Click += new System.EventHandler(this.picStation_Click);
      // 
      // UcLine
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Gray;
      this.Controls.Add(this.lbStationName);
      this.Controls.Add(this.picStation);
      this.Name = "UcLine";
      this.Size = new System.Drawing.Size(146, 161);
      ((System.ComponentModel.ISupportInitialize)(this.picStation)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lbStationName;
    private System.Windows.Forms.PictureBox picStation;
  }
}
