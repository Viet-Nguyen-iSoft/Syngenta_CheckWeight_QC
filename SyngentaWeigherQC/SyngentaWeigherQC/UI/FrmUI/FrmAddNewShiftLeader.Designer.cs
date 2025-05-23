namespace SyngentaWeigherQC.UI.FrmUI
{
  partial class FrmAddNewShiftLeader
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
      this.lbTitle = new System.Windows.Forms.Label();
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnAdd = new CustomControls.RJControls.RJButton();
      this.btnCancel = new CustomControls.RJControls.RJButton();
      this.txtName = new CustomControls.RJControls.RJTextBox();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // lbTitle
      // 
      this.lbTitle.BackColor = System.Drawing.Color.Transparent;
      this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbTitle.ForeColor = System.Drawing.Color.White;
      this.lbTitle.Location = new System.Drawing.Point(5, 5);
      this.lbTitle.Name = "lbTitle";
      this.lbTitle.Size = new System.Drawing.Size(400, 47);
      this.lbTitle.TabIndex = 5;
      this.lbTitle.Text = "Thêm mới người dùng";
      this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.Transparent;
      this.panel1.Controls.Add(this.btnAdd);
      this.panel1.Controls.Add(this.btnCancel);
      this.panel1.Controls.Add(this.txtName);
      this.panel1.Location = new System.Drawing.Point(5, 55);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(400, 142);
      this.panel1.TabIndex = 6;
      // 
      // btnAdd
      // 
      this.btnAdd.BackColor = System.Drawing.Color.Green;
      this.btnAdd.BackgroundColor = System.Drawing.Color.Green;
      this.btnAdd.BorderColor = System.Drawing.Color.PaleVioletRed;
      this.btnAdd.BorderRadius = 8;
      this.btnAdd.BorderSize = 0;
      this.btnAdd.FlatAppearance.BorderSize = 0;
      this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnAdd.ForeColor = System.Drawing.Color.White;
      this.btnAdd.Location = new System.Drawing.Point(276, 90);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(112, 40);
      this.btnAdd.TabIndex = 18;
      this.btnAdd.Text = "Add";
      this.btnAdd.TextColor = System.Drawing.Color.White;
      this.btnAdd.UseVisualStyleBackColor = false;
      this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
      this.btnCancel.Location = new System.Drawing.Point(162, 90);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(108, 40);
      this.btnCancel.TabIndex = 17;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.TextColor = System.Drawing.Color.White;
      this.btnCancel.UseVisualStyleBackColor = false;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // txtName
      // 
      this.txtName.BackColor = System.Drawing.SystemColors.Window;
      this.txtName.BorderColor = System.Drawing.Color.MediumSlateBlue;
      this.txtName.BorderFocusColor = System.Drawing.Color.HotPink;
      this.txtName.BorderRadius = 8;
      this.txtName.BorderSize = 2;
      this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.txtName.Location = new System.Drawing.Point(8, 24);
      this.txtName.Margin = new System.Windows.Forms.Padding(4);
      this.txtName.Multiline = false;
      this.txtName.Name = "txtName";
      this.txtName.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
      this.txtName.PasswordChar = false;
      this.txtName.PlaceholderColor = System.Drawing.Color.DarkGray;
      this.txtName.PlaceholderText = "";
      this.txtName.Size = new System.Drawing.Size(380, 39);
      this.txtName.TabIndex = 16;
      this.txtName.Texts = "";
      this.txtName.UnderlinedStyle = false;
      // 
      // FrmAddNewShiftLeader
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(17)))), ((int)(((byte)(70)))));
      this.ClientSize = new System.Drawing.Size(410, 200);
      this.Controls.Add(this.lbTitle);
      this.Controls.Add(this.panel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FrmAddNewShiftLeader";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmAddNewUser_KeyUp);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lbTitle;
    private System.Windows.Forms.Panel panel1;
    private CustomControls.RJControls.RJButton btnAdd;
    private CustomControls.RJControls.RJButton btnCancel;
    private CustomControls.RJControls.RJTextBox txtName;
  }
}