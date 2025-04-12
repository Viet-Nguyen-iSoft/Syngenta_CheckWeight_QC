namespace SynCheckWeigherLoggerApp.DashboardViews
{
  partial class FrmSampleRework
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
      this.btTareNoLabel = new System.Windows.Forms.Label();
      this.lblOldSampleValue = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.lblNewValue = new System.Windows.Forms.Label();
      this.lblStatus = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btTareNoLabel
      // 
      this.btTareNoLabel.BackColor = System.Drawing.Color.Silver;
      this.btTareNoLabel.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btTareNoLabel.ForeColor = System.Drawing.Color.Black;
      this.btTareNoLabel.Location = new System.Drawing.Point(12, 9);
      this.btTareNoLabel.Name = "btTareNoLabel";
      this.btTareNoLabel.Size = new System.Drawing.Size(223, 43);
      this.btTareNoLabel.TabIndex = 7;
      this.btTareNoLabel.Text = "Mẫu cũ";
      this.btTareNoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblOldSampleValue
      // 
      this.lblOldSampleValue.BackColor = System.Drawing.Color.Silver;
      this.lblOldSampleValue.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblOldSampleValue.ForeColor = System.Drawing.Color.Black;
      this.lblOldSampleValue.Location = new System.Drawing.Point(12, 54);
      this.lblOldSampleValue.Name = "lblOldSampleValue";
      this.lblOldSampleValue.Size = new System.Drawing.Size(223, 87);
      this.lblOldSampleValue.TabIndex = 8;
      this.lblOldSampleValue.Text = "0";
      this.lblOldSampleValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label2
      // 
      this.label2.BackColor = System.Drawing.Color.LightBlue;
      this.label2.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.ForeColor = System.Drawing.Color.Black;
      this.label2.Location = new System.Drawing.Point(253, 9);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(223, 43);
      this.label2.TabIndex = 9;
      this.label2.Text = "Mẫu cân lại";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblNewValue
      // 
      this.lblNewValue.BackColor = System.Drawing.Color.LightBlue;
      this.lblNewValue.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblNewValue.ForeColor = System.Drawing.Color.Black;
      this.lblNewValue.Location = new System.Drawing.Point(253, 54);
      this.lblNewValue.Name = "lblNewValue";
      this.lblNewValue.Size = new System.Drawing.Size(223, 87);
      this.lblNewValue.TabIndex = 10;
      this.lblNewValue.Text = "0";
      this.lblNewValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblStatus
      // 
      this.lblStatus.BackColor = System.Drawing.Color.Black;
      this.lblStatus.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblStatus.ForeColor = System.Drawing.Color.White;
      this.lblStatus.Location = new System.Drawing.Point(12, 154);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(462, 43);
      this.lblStatus.TabIndex = 11;
      this.lblStatus.Text = "Đang chờ cân";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // FrmSampleRework
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(484, 208);
      this.Controls.Add(this.lblStatus);
      this.Controls.Add(this.lblNewValue);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.lblOldSampleValue);
      this.Controls.Add(this.btTareNoLabel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FrmSampleRework";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Vui lòng đặt sản phẩm cân lại";
      this.ResumeLayout(false);

    }

        #endregion

        private System.Windows.Forms.Label btTareNoLabel;
        private System.Windows.Forms.Label lblOldSampleValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNewValue;
        private System.Windows.Forms.Label lblStatus;
  }
}