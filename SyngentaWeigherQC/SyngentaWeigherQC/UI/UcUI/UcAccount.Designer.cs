namespace SyngentaWeigherQC.UI.UcUI
{
  partial class UcAccount
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
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.lbAccount = new System.Windows.Forms.Label();
      this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.txtPass = new CustomControls.RJControls.RJTextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnSave = new CustomControls.RJControls.RJButton();
      this.btnHidePass = new CustomControls.RJControls.RJButton();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.elipseControl1 = new SyngentaWeigherQC.ItemControls.ElipseControl();
      this.tableLayoutPanel1.SuspendLayout();
      this.tableLayoutPanel3.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.lbAccount, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(666, 125);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // lbAccount
      // 
      this.lbAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lbAccount.AutoSize = true;
      this.lbAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbAccount.Location = new System.Drawing.Point(3, 0);
      this.lbAccount.Name = "lbAccount";
      this.lbAccount.Size = new System.Drawing.Size(660, 40);
      this.lbAccount.TabIndex = 1;
      this.lbAccount.Text = "Tên Account:";
      this.lbAccount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel3
      // 
      this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
      this.tableLayoutPanel3.ColumnCount = 3;
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
      this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel2, 1, 0);
      this.tableLayoutPanel3.Controls.Add(this.pictureBox1, 0, 0);
      this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 40);
      this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel3.Name = "tableLayoutPanel3";
      this.tableLayoutPanel3.RowCount = 1;
      this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel3.Size = new System.Drawing.Size(666, 85);
      this.tableLayoutPanel3.TabIndex = 2;
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
      this.tableLayoutPanel2.ColumnCount = 4;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
      this.tableLayoutPanel2.Controls.Add(this.txtPass, 1, 1);
      this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
      this.tableLayoutPanel2.Controls.Add(this.btnSave, 3, 1);
      this.tableLayoutPanel2.Controls.Add(this.btnHidePass, 2, 1);
      this.tableLayoutPanel2.Location = new System.Drawing.Point(93, 3);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 3;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(565, 79);
      this.tableLayoutPanel2.TabIndex = 0;
      // 
      // txtPass
      // 
      this.txtPass.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPass.BackColor = System.Drawing.SystemColors.Window;
      this.txtPass.BorderColor = System.Drawing.Color.MediumSlateBlue;
      this.txtPass.BorderFocusColor = System.Drawing.Color.HotPink;
      this.txtPass.BorderRadius = 5;
      this.txtPass.BorderSize = 2;
      this.txtPass.Enabled = false;
      this.txtPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.txtPass.Location = new System.Drawing.Point(131, 19);
      this.txtPass.Margin = new System.Windows.Forms.Padding(4);
      this.txtPass.Multiline = false;
      this.txtPass.Name = "txtPass";
      this.txtPass.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
      this.txtPass.PasswordChar = true;
      this.txtPass.PlaceholderColor = System.Drawing.Color.DarkGray;
      this.txtPass.PlaceholderText = "";
      this.txtPass.Size = new System.Drawing.Size(260, 40);
      this.txtPass.TabIndex = 2;
      this.txtPass.Texts = "";
      this.txtPass.UnderlinedStyle = false;
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(3, 15);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(121, 49);
      this.label1.TabIndex = 0;
      this.label1.Text = "Password:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.BackColor = System.Drawing.Color.MediumSlateBlue;
      this.btnSave.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
      this.btnSave.BorderColor = System.Drawing.Color.PaleVioletRed;
      this.btnSave.BorderRadius = 5;
      this.btnSave.BorderSize = 0;
      this.btnSave.FlatAppearance.BorderSize = 0;
      this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnSave.ForeColor = System.Drawing.Color.White;
      this.btnSave.Location = new System.Drawing.Point(448, 18);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(114, 43);
      this.btnSave.TabIndex = 3;
      this.btnSave.Text = "Change";
      this.btnSave.TextColor = System.Drawing.Color.White;
      this.btnSave.UseVisualStyleBackColor = false;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnHidePass
      // 
      this.btnHidePass.BackColor = System.Drawing.Color.DimGray;
      this.btnHidePass.BackgroundColor = System.Drawing.Color.DimGray;
      this.btnHidePass.BorderColor = System.Drawing.Color.PaleVioletRed;
      this.btnHidePass.BorderRadius = 5;
      this.btnHidePass.BorderSize = 0;
      this.btnHidePass.FlatAppearance.BorderSize = 0;
      this.btnHidePass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnHidePass.ForeColor = System.Drawing.Color.White;
      this.btnHidePass.Image = global::SyngentaWeigherQC.Properties.Resources.passDisable32px;
      this.btnHidePass.Location = new System.Drawing.Point(398, 18);
      this.btnHidePass.Name = "btnHidePass";
      this.btnHidePass.Size = new System.Drawing.Size(44, 40);
      this.btnHidePass.TabIndex = 4;
      this.btnHidePass.TextColor = System.Drawing.Color.White;
      this.btnHidePass.UseVisualStyleBackColor = false;
      this.btnHidePass.Click += new System.EventHandler(this.btnHidePass_Click);
      // 
      // pictureBox1
      // 
      this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureBox1.Image = global::SyngentaWeigherQC.Properties.Resources.user;
      this.pictureBox1.Location = new System.Drawing.Point(10, 10);
      this.pictureBox1.Margin = new System.Windows.Forms.Padding(10);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(70, 65);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 1;
      this.pictureBox1.TabStop = false;
      // 
      // elipseControl1
      // 
      this.elipseControl1.CornerRadius = 20;
      this.elipseControl1.TargetControl = this.tableLayoutPanel1;
      // 
      // UcAccount
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "UcAccount";
      this.Size = new System.Drawing.Size(666, 125);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.tableLayoutPanel3.ResumeLayout(false);
      this.tableLayoutPanel2.ResumeLayout(false);
      this.tableLayoutPanel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.Label label1;
    private CustomControls.RJControls.RJTextBox txtPass;
    private System.Windows.Forms.Label lbAccount;
    private CustomControls.RJControls.RJButton btnSave;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    private System.Windows.Forms.PictureBox pictureBox1;
    private ItemControls.ElipseControl elipseControl1;
    private CustomControls.RJControls.RJButton btnHidePass;
  }
}
