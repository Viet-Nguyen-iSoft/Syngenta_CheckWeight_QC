namespace SyngentaWeigherQC.UI.FrmUI
{
  partial class FrmSeeHistoricalReweigher
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
      this.lblNewValue = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.lblOldSampleValue = new System.Windows.Forms.Label();
      this.btTareNoLabel = new System.Windows.Forms.Label();
      this.lbDateCreate = new System.Windows.Forms.Label();
      this.lbDateUpdate = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // lblNewValue
      // 
      this.lblNewValue.BackColor = System.Drawing.Color.LightBlue;
      this.lblNewValue.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblNewValue.ForeColor = System.Drawing.Color.Black;
      this.lblNewValue.Location = new System.Drawing.Point(316, 55);
      this.lblNewValue.Name = "lblNewValue";
      this.lblNewValue.Size = new System.Drawing.Size(283, 87);
      this.lblNewValue.TabIndex = 15;
      this.lblNewValue.Text = "0";
      this.lblNewValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label2
      // 
      this.label2.BackColor = System.Drawing.Color.LightBlue;
      this.label2.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.ForeColor = System.Drawing.Color.Black;
      this.label2.Location = new System.Drawing.Point(316, 10);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(283, 43);
      this.label2.TabIndex = 14;
      this.label2.Text = "Mẫu cân lại";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblOldSampleValue
      // 
      this.lblOldSampleValue.BackColor = System.Drawing.Color.Silver;
      this.lblOldSampleValue.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblOldSampleValue.ForeColor = System.Drawing.Color.Black;
      this.lblOldSampleValue.Location = new System.Drawing.Point(10, 55);
      this.lblOldSampleValue.Name = "lblOldSampleValue";
      this.lblOldSampleValue.Size = new System.Drawing.Size(283, 87);
      this.lblOldSampleValue.TabIndex = 13;
      this.lblOldSampleValue.Text = "0";
      this.lblOldSampleValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // btTareNoLabel
      // 
      this.btTareNoLabel.BackColor = System.Drawing.Color.Silver;
      this.btTareNoLabel.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btTareNoLabel.ForeColor = System.Drawing.Color.Black;
      this.btTareNoLabel.Location = new System.Drawing.Point(10, 10);
      this.btTareNoLabel.Name = "btTareNoLabel";
      this.btTareNoLabel.Size = new System.Drawing.Size(283, 43);
      this.btTareNoLabel.TabIndex = 12;
      this.btTareNoLabel.Text = "Mẫu cũ";
      this.btTareNoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbDateCreate
      // 
      this.lbDateCreate.BackColor = System.Drawing.Color.Silver;
      this.lbDateCreate.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbDateCreate.ForeColor = System.Drawing.Color.Black;
      this.lbDateCreate.Location = new System.Drawing.Point(10, 150);
      this.lbDateCreate.Name = "lbDateCreate";
      this.lbDateCreate.Size = new System.Drawing.Size(283, 43);
      this.lbDateCreate.TabIndex = 16;
      this.lbDateCreate.Text = "Mẫu cũ";
      this.lbDateCreate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbDateUpdate
      // 
      this.lbDateUpdate.BackColor = System.Drawing.Color.LightBlue;
      this.lbDateUpdate.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbDateUpdate.ForeColor = System.Drawing.Color.Black;
      this.lbDateUpdate.Location = new System.Drawing.Point(316, 150);
      this.lbDateUpdate.Name = "lbDateUpdate";
      this.lbDateUpdate.Size = new System.Drawing.Size(283, 43);
      this.lbDateUpdate.TabIndex = 17;
      this.lbDateUpdate.Text = "Mẫu cân lại";
      this.lbDateUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // FrmSeeHistoricalReweigher
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(612, 205);
      this.Controls.Add(this.lbDateUpdate);
      this.Controls.Add(this.lbDateCreate);
      this.Controls.Add(this.lblNewValue);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.lblOldSampleValue);
      this.Controls.Add(this.btTareNoLabel);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FrmSeeHistoricalReweigher";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Xem lịch sử cân lại";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lblNewValue;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label lblOldSampleValue;
    private System.Windows.Forms.Label btTareNoLabel;
    private System.Windows.Forms.Label lbDateCreate;
    private System.Windows.Forms.Label lbDateUpdate;
  }
}