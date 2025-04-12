namespace SyngentaWeigherQC.UI.FrmUI
{
  partial class FrmConfirmChangeMasterData
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
      this.btConfirm = new System.Windows.Forms.Button();
      this.btCancel = new System.Windows.Forms.Button();
      this.txtContentChange = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btConfirm
      // 
      this.btConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btConfirm.Location = new System.Drawing.Point(509, 236);
      this.btConfirm.Name = "btConfirm";
      this.btConfirm.Size = new System.Drawing.Size(138, 49);
      this.btConfirm.TabIndex = 12;
      this.btConfirm.Text = "Xác nhận";
      this.btConfirm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btConfirm.UseVisualStyleBackColor = true;
      this.btConfirm.Click += new System.EventHandler(this.btConfirm_Click_1);
      // 
      // btCancel
      // 
      this.btCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btCancel.Location = new System.Drawing.Point(651, 236);
      this.btCancel.Name = "btCancel";
      this.btCancel.Size = new System.Drawing.Size(140, 49);
      this.btCancel.TabIndex = 11;
      this.btCancel.Text = "Thoát";
      this.btCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btCancel.UseVisualStyleBackColor = true;
      this.btCancel.Click += new System.EventHandler(this.btCancel_Click_1);
      // 
      // txtContentChange
      // 
      this.txtContentChange.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtContentChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtContentChange.Location = new System.Drawing.Point(12, 58);
      this.txtContentChange.Multiline = true;
      this.txtContentChange.Name = "txtContentChange";
      this.txtContentChange.Size = new System.Drawing.Size(779, 172);
      this.txtContentChange.TabIndex = 10;
      // 
      // label1
      // 
      this.label1.Dock = System.Windows.Forms.DockStyle.Top;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(803, 70);
      this.label1.TabIndex = 9;
      this.label1.Text = "Vui lòng điền thông tin thay đổi sản phẩm";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // FrmConfirmChangeMasterData
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(803, 294);
      this.Controls.Add(this.btConfirm);
      this.Controls.Add(this.btCancel);
      this.Controls.Add(this.txtContentChange);
      this.Controls.Add(this.label1);
      this.KeyPreview = true;
      this.Name = "FrmConfirmChangeMasterData";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmConfirmChangeMasterData_KeyUp);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btConfirm;
    private System.Windows.Forms.Button btCancel;
    private System.Windows.Forms.TextBox txtContentChange;
    private System.Windows.Forms.Label label1;
  }
}