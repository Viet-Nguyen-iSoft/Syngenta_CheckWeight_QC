namespace SyngentaWeigherQC.UI.FrmUI
{
  partial class FrmChangeLine
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
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnCancel = new CustomControls.RJControls.RJButton();
      this.btnConfirm = new CustomControls.RJControls.RJButton();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.flowLayoutPanel1.BackColor = System.Drawing.Color.Gray;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(11, 12);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new System.Drawing.Size(970, 582);
      this.flowLayoutPanel1.TabIndex = 7;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnCancel);
      this.panel1.Controls.Add(this.btnConfirm);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 596);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(993, 49);
      this.panel1.TabIndex = 6;
      // 
      // btnCancel
      // 
      this.btnCancel.BackColor = System.Drawing.Color.Tomato;
      this.btnCancel.BackgroundColor = System.Drawing.Color.Tomato;
      this.btnCancel.BorderColor = System.Drawing.Color.PaleVioletRed;
      this.btnCancel.BorderRadius = 8;
      this.btnCancel.BorderSize = 0;
      this.btnCancel.FlatAppearance.BorderSize = 0;
      this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnCancel.ForeColor = System.Drawing.Color.White;
      this.btnCancel.Location = new System.Drawing.Point(674, 4);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(150, 40);
      this.btnCancel.TabIndex = 7;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.TextColor = System.Drawing.Color.White;
      this.btnCancel.UseVisualStyleBackColor = false;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnConfirm
      // 
      this.btnConfirm.BackColor = System.Drawing.Color.Green;
      this.btnConfirm.BackgroundColor = System.Drawing.Color.Green;
      this.btnConfirm.BorderColor = System.Drawing.Color.PaleVioletRed;
      this.btnConfirm.BorderRadius = 8;
      this.btnConfirm.BorderSize = 0;
      this.btnConfirm.FlatAppearance.BorderSize = 0;
      this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnConfirm.ForeColor = System.Drawing.Color.White;
      this.btnConfirm.Location = new System.Drawing.Point(831, 4);
      this.btnConfirm.Name = "btnConfirm";
      this.btnConfirm.Size = new System.Drawing.Size(150, 40);
      this.btnConfirm.TabIndex = 6;
      this.btnConfirm.Text = "Confirm";
      this.btnConfirm.TextColor = System.Drawing.Color.White;
      this.btnConfirm.UseVisualStyleBackColor = false;
      this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
      // 
      // FrmChangeLine
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(993, 645);
      this.Controls.Add(this.flowLayoutPanel1);
      this.Controls.Add(this.panel1);
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FrmChangeLine";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Load += new System.EventHandler(this.FrmChangeLine_Load);
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmChangeLine_KeyUp);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.Panel panel1;
    private CustomControls.RJControls.RJButton btnCancel;
    private CustomControls.RJControls.RJButton btnConfirm;
  }
}