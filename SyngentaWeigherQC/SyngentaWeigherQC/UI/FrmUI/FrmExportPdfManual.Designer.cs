namespace SyngentaWeigherQC.UI.FrmUI
{
  partial class FrmExportPdfManual
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
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.dtpFromMonth = new System.Windows.Forms.DateTimePicker();
      this.dtpToMonth = new System.Windows.Forms.DateTimePicker();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.cbbMonth = new System.Windows.Forms.ComboBox();
      this.cbbYearMonth = new System.Windows.Forms.ComboBox();
      this.cbbYearWeek = new System.Windows.Forms.ComboBox();
      this.cbbWeek = new System.Windows.Forms.ComboBox();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.dtpFromWeek = new System.Windows.Forms.DateTimePicker();
      this.label8 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.dtpToWeek = new System.Windows.Forms.DateTimePicker();
      this.label10 = new System.Windows.Forms.Label();
      this.btnExportWeek = new CustomControls.RJControls.RJButton();
      this.btnExportMonth = new CustomControls.RJControls.RJButton();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.AutoSize = true;
      this.label1.BackColor = System.Drawing.Color.Transparent;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(12, 17);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(163, 24);
      this.label1.TabIndex = 3;
      this.label1.Text = "Xuất PDF tháng:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(13, 114);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(92, 24);
      this.label2.TabIndex = 0;
      this.label2.Text = "Từ ngày:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // dtpFromMonth
      // 
      this.dtpFromMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dtpFromMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dtpFromMonth.Location = new System.Drawing.Point(124, 113);
      this.dtpFromMonth.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
      this.dtpFromMonth.Name = "dtpFromMonth";
      this.dtpFromMonth.Size = new System.Drawing.Size(350, 29);
      this.dtpFromMonth.TabIndex = 3;
      // 
      // dtpToMonth
      // 
      this.dtpToMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dtpToMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dtpToMonth.Location = new System.Drawing.Point(124, 164);
      this.dtpToMonth.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
      this.dtpToMonth.Name = "dtpToMonth";
      this.dtpToMonth.Size = new System.Drawing.Size(350, 29);
      this.dtpToMonth.TabIndex = 8;
      // 
      // label4
      // 
      this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(13, 167);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(105, 24);
      this.label4.TabIndex = 9;
      this.label4.Text = "Đến ngày:";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label3
      // 
      this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(15, 65);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(59, 24);
      this.label3.TabIndex = 10;
      this.label3.Text = "Năm:";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label5
      // 
      this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(279, 65);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(76, 24);
      this.label5.TabIndex = 11;
      this.label5.Text = "Tháng:";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cbbMonth
      // 
      this.cbbMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cbbMonth.FormattingEnabled = true;
      this.cbbMonth.Location = new System.Drawing.Point(373, 62);
      this.cbbMonth.Name = "cbbMonth";
      this.cbbMonth.Size = new System.Drawing.Size(101, 32);
      this.cbbMonth.TabIndex = 13;
      // 
      // cbbYearMonth
      // 
      this.cbbYearMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cbbYearMonth.FormattingEnabled = true;
      this.cbbYearMonth.Location = new System.Drawing.Point(124, 63);
      this.cbbYearMonth.Name = "cbbYearMonth";
      this.cbbYearMonth.Size = new System.Drawing.Size(121, 32);
      this.cbbYearMonth.TabIndex = 14;
      // 
      // cbbYearWeek
      // 
      this.cbbYearWeek.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cbbYearWeek.FormattingEnabled = true;
      this.cbbYearWeek.Location = new System.Drawing.Point(694, 62);
      this.cbbYearWeek.Name = "cbbYearWeek";
      this.cbbYearWeek.Size = new System.Drawing.Size(121, 32);
      this.cbbYearWeek.TabIndex = 23;
      // 
      // cbbWeek
      // 
      this.cbbWeek.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cbbWeek.FormattingEnabled = true;
      this.cbbWeek.Location = new System.Drawing.Point(943, 62);
      this.cbbWeek.Name = "cbbWeek";
      this.cbbWeek.Size = new System.Drawing.Size(101, 32);
      this.cbbWeek.TabIndex = 22;
      // 
      // label6
      // 
      this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(856, 65);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(64, 24);
      this.label6.TabIndex = 21;
      this.label6.Text = "Tuần:";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label7
      // 
      this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label7.AutoSize = true;
      this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label7.Location = new System.Drawing.Point(583, 65);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(59, 24);
      this.label7.TabIndex = 20;
      this.label7.Text = "Năm:";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // dtpFromWeek
      // 
      this.dtpFromWeek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dtpFromWeek.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dtpFromWeek.Location = new System.Drawing.Point(694, 117);
      this.dtpFromWeek.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
      this.dtpFromWeek.Name = "dtpFromWeek";
      this.dtpFromWeek.Size = new System.Drawing.Size(350, 29);
      this.dtpFromWeek.TabIndex = 16;
      // 
      // label8
      // 
      this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label8.AutoSize = true;
      this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label8.Location = new System.Drawing.Point(583, 120);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(92, 24);
      this.label8.TabIndex = 15;
      this.label8.Text = "Từ ngày:";
      this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label9
      // 
      this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label9.AutoSize = true;
      this.label9.BackColor = System.Drawing.Color.Transparent;
      this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label9.Location = new System.Drawing.Point(583, 17);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(151, 24);
      this.label9.TabIndex = 17;
      this.label9.Text = "Xuất PDF tuần:";
      this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // dtpToWeek
      // 
      this.dtpToWeek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dtpToWeek.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dtpToWeek.Location = new System.Drawing.Point(694, 175);
      this.dtpToWeek.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
      this.dtpToWeek.Name = "dtpToWeek";
      this.dtpToWeek.Size = new System.Drawing.Size(350, 29);
      this.dtpToWeek.TabIndex = 18;
      // 
      // label10
      // 
      this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label10.AutoSize = true;
      this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label10.Location = new System.Drawing.Point(583, 178);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(105, 24);
      this.label10.TabIndex = 19;
      this.label10.Text = "Đến ngày:";
      this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // btnExportWeek
      // 
      this.btnExportWeek.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.btnExportWeek.BackColor = System.Drawing.Color.RoyalBlue;
      this.btnExportWeek.BackgroundColor = System.Drawing.Color.RoyalBlue;
      this.btnExportWeek.BorderColor = System.Drawing.Color.PapayaWhip;
      this.btnExportWeek.BorderRadius = 5;
      this.btnExportWeek.BorderSize = 0;
      this.btnExportWeek.FlatAppearance.BorderSize = 0;
      this.btnExportWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnExportWeek.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnExportWeek.ForeColor = System.Drawing.Color.White;
      this.btnExportWeek.Location = new System.Drawing.Point(882, 240);
      this.btnExportWeek.Margin = new System.Windows.Forms.Padding(3, 3, 8, 8);
      this.btnExportWeek.Name = "btnExportWeek";
      this.btnExportWeek.Size = new System.Drawing.Size(162, 46);
      this.btnExportWeek.TabIndex = 24;
      this.btnExportWeek.Text = "Xuất PDF tuần";
      this.btnExportWeek.TextColor = System.Drawing.Color.White;
      this.btnExportWeek.UseVisualStyleBackColor = false;
      this.btnExportWeek.Click += new System.EventHandler(this.btnExportWeek_Click);
      // 
      // btnExportMonth
      // 
      this.btnExportMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.btnExportMonth.BackColor = System.Drawing.Color.RoyalBlue;
      this.btnExportMonth.BackgroundColor = System.Drawing.Color.RoyalBlue;
      this.btnExportMonth.BorderColor = System.Drawing.Color.PapayaWhip;
      this.btnExportMonth.BorderRadius = 5;
      this.btnExportMonth.BorderSize = 0;
      this.btnExportMonth.FlatAppearance.BorderSize = 0;
      this.btnExportMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnExportMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnExportMonth.ForeColor = System.Drawing.Color.White;
      this.btnExportMonth.Location = new System.Drawing.Point(312, 240);
      this.btnExportMonth.Margin = new System.Windows.Forms.Padding(3, 3, 8, 8);
      this.btnExportMonth.Name = "btnExportMonth";
      this.btnExportMonth.Size = new System.Drawing.Size(162, 46);
      this.btnExportMonth.TabIndex = 2;
      this.btnExportMonth.Text = "Xuất PDF tháng";
      this.btnExportMonth.TextColor = System.Drawing.Color.White;
      this.btnExportMonth.UseVisualStyleBackColor = false;
      this.btnExportMonth.Click += new System.EventHandler(this.btnExportMonth_Click);
      // 
      // FrmExportPdfManual
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1064, 295);
      this.Controls.Add(this.btnExportWeek);
      this.Controls.Add(this.cbbYearWeek);
      this.Controls.Add(this.cbbWeek);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.dtpFromWeek);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.label9);
      this.Controls.Add(this.dtpToWeek);
      this.Controls.Add(this.label10);
      this.Controls.Add(this.cbbYearMonth);
      this.Controls.Add(this.cbbMonth);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.dtpFromMonth);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.dtpToMonth);
      this.Controls.Add(this.btnExportMonth);
      this.Controls.Add(this.label4);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FrmExportPdfManual";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Xuất PDF thủ công";
      this.Load += new System.EventHandler(this.FrmExportPdfManual_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private CustomControls.RJControls.RJButton btnExportMonth;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.DateTimePicker dtpFromMonth;
    private System.Windows.Forms.DateTimePicker dtpToMonth;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ComboBox cbbMonth;
    private System.Windows.Forms.ComboBox cbbYearMonth;
    private System.Windows.Forms.ComboBox cbbYearWeek;
    private System.Windows.Forms.ComboBox cbbWeek;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.DateTimePicker dtpFromWeek;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.DateTimePicker dtpToWeek;
    private System.Windows.Forms.Label label10;
    private CustomControls.RJControls.RJButton btnExportWeek;
  }
}