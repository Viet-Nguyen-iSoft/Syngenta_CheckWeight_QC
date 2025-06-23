namespace SyngentaWeigherQC.UI.FrmUI
{
  partial class FrmReportAutoPdf
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
      this.components = new System.ComponentModel.Container();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.page1 = new System.Windows.Forms.TableLayoutPanel();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
      this.label4 = new System.Windows.Forms.Label();
      this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
      this.label3 = new System.Windows.Forms.Label();
      this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
      this.label2 = new System.Windows.Forms.Label();
      this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
      this.label1 = new System.Windows.Forms.Label();
      this.lbTitleReport = new System.Windows.Forms.Label();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.label9 = new System.Windows.Forms.Label();
      this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
      this.label5 = new System.Windows.Forms.Label();
      this.chartAvgSigma = new SyngentaWeigherQC.UI.UcUI.UcChartV2();
      this.chartTopSigma = new SyngentaWeigherQC.UI.UcUI.UcChartV1();
      this.chartAvgStdev = new SyngentaWeigherQC.UI.UcUI.UcChartV2();
      this.chartAvgCpk = new SyngentaWeigherQC.UI.UcUI.UcChartV2();
      this.chartAvgLoss = new SyngentaWeigherQC.UI.UcUI.UcChartV2();
      this.chartAvgError = new SyngentaWeigherQC.UI.UcUI.UcChartV2();
      this.chartTopStdev = new SyngentaWeigherQC.UI.UcUI.UcChartV1();
      this.chartTopCpk = new SyngentaWeigherQC.UI.UcUI.UcChartV1();
      this.chartTopLoss = new SyngentaWeigherQC.UI.UcUI.UcChartV1();
      this.chartPie = new SyngentaWeigherQC.UI.UcUI.UcChartPie();
      this.sumary = new SyngentaWeigherQC.UI.UcUI.UcSumary();
      this.chartTopError = new SyngentaWeigherQC.UI.UcUI.UcChartV1();
      this.tabControl1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.page1.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      this.tableLayoutPanel6.SuspendLayout();
      this.tableLayoutPanel5.SuspendLayout();
      this.tableLayoutPanel3.SuspendLayout();
      this.tableLayoutPanel4.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.tableLayoutPanel7.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabControl1
      // 
      this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tabControl1.Location = new System.Drawing.Point(3, 63);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(1898, 1023);
      this.tabControl1.TabIndex = 1;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.page1);
      this.tabPage2.Location = new System.Drawing.Point(4, 25);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(1890, 994);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "ReportMonth";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // page1
      // 
      this.page1.ColumnCount = 1;
      this.page1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.page1.Controls.Add(this.tableLayoutPanel2, 0, 1);
      this.page1.Controls.Add(this.lbTitleReport, 0, 0);
      this.page1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.page1.Location = new System.Drawing.Point(3, 3);
      this.page1.Margin = new System.Windows.Forms.Padding(0);
      this.page1.Name = "page1";
      this.page1.RowCount = 2;
      this.page1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
      this.page1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.page1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.page1.Size = new System.Drawing.Size(1884, 988);
      this.page1.TabIndex = 0;
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.BackColor = System.Drawing.Color.Silver;
      this.tableLayoutPanel2.ColumnCount = 6;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
      this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel7, 5, 1);
      this.tableLayoutPanel2.Controls.Add(this.chartTopSigma, 5, 0);
      this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 4, 1);
      this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 3, 1);
      this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 2, 1);
      this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 1, 1);
      this.tableLayoutPanel2.Controls.Add(this.chartTopStdev, 4, 0);
      this.tableLayoutPanel2.Controls.Add(this.chartTopCpk, 3, 0);
      this.tableLayoutPanel2.Controls.Add(this.chartTopLoss, 2, 0);
      this.tableLayoutPanel2.Controls.Add(this.chartPie, 0, 1);
      this.tableLayoutPanel2.Controls.Add(this.sumary, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.chartTopError, 1, 0);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 50);
      this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 2;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(1884, 938);
      this.tableLayoutPanel2.TabIndex = 1;
      // 
      // tableLayoutPanel6
      // 
      this.tableLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tableLayoutPanel6.ColumnCount = 1;
      this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel6.Controls.Add(this.chartAvgStdev, 0, 0);
      this.tableLayoutPanel6.Controls.Add(this.label4, 0, 1);
      this.tableLayoutPanel6.Location = new System.Drawing.Point(1256, 469);
      this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel6.Name = "tableLayoutPanel6";
      this.tableLayoutPanel6.RowCount = 2;
      this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel6.Size = new System.Drawing.Size(314, 469);
      this.tableLayoutPanel6.TabIndex = 14;
      // 
      // label4
      // 
      this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label4.AutoSize = true;
      this.label4.BackColor = System.Drawing.Color.DarkSlateGray;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.ForeColor = System.Drawing.Color.White;
      this.label4.Location = new System.Drawing.Point(1, 439);
      this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(312, 30);
      this.label4.TabIndex = 7;
      this.label4.Text = "Stdev";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // tableLayoutPanel5
      // 
      this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tableLayoutPanel5.ColumnCount = 1;
      this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel5.Controls.Add(this.chartAvgCpk, 0, 0);
      this.tableLayoutPanel5.Controls.Add(this.label3, 0, 1);
      this.tableLayoutPanel5.Location = new System.Drawing.Point(942, 469);
      this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel5.Name = "tableLayoutPanel5";
      this.tableLayoutPanel5.RowCount = 2;
      this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel5.Size = new System.Drawing.Size(314, 469);
      this.tableLayoutPanel5.TabIndex = 13;
      // 
      // label3
      // 
      this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label3.AutoSize = true;
      this.label3.BackColor = System.Drawing.Color.DarkSlateGray;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.ForeColor = System.Drawing.Color.White;
      this.label3.Location = new System.Drawing.Point(1, 439);
      this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(312, 30);
      this.label3.TabIndex = 7;
      this.label3.Text = "Cpk";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // tableLayoutPanel3
      // 
      this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tableLayoutPanel3.ColumnCount = 1;
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel3.Controls.Add(this.chartAvgLoss, 0, 0);
      this.tableLayoutPanel3.Controls.Add(this.label2, 0, 1);
      this.tableLayoutPanel3.Location = new System.Drawing.Point(628, 469);
      this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel3.Name = "tableLayoutPanel3";
      this.tableLayoutPanel3.RowCount = 2;
      this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel3.Size = new System.Drawing.Size(314, 469);
      this.tableLayoutPanel3.TabIndex = 12;
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label2.AutoSize = true;
      this.label2.BackColor = System.Drawing.Color.DarkSlateGray;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.ForeColor = System.Drawing.Color.White;
      this.label2.Location = new System.Drawing.Point(1, 439);
      this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(312, 30);
      this.label2.TabIndex = 7;
      this.label2.Text = "% Hao hụt";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // tableLayoutPanel4
      // 
      this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tableLayoutPanel4.ColumnCount = 1;
      this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel4.Controls.Add(this.label1, 0, 1);
      this.tableLayoutPanel4.Controls.Add(this.chartAvgError, 0, 0);
      this.tableLayoutPanel4.Location = new System.Drawing.Point(314, 469);
      this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel4.Name = "tableLayoutPanel4";
      this.tableLayoutPanel4.RowCount = 2;
      this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel4.Size = new System.Drawing.Size(314, 469);
      this.tableLayoutPanel4.TabIndex = 11;
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.AutoSize = true;
      this.label1.BackColor = System.Drawing.Color.DarkSlateGray;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(1, 439);
      this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(312, 30);
      this.label1.TabIndex = 7;
      this.label1.Text = "% Mẫu lỗi";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbTitleReport
      // 
      this.lbTitleReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lbTitleReport.AutoSize = true;
      this.lbTitleReport.BackColor = System.Drawing.Color.Black;
      this.lbTitleReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbTitleReport.ForeColor = System.Drawing.Color.White;
      this.lbTitleReport.Location = new System.Drawing.Point(0, 0);
      this.lbTitleReport.Margin = new System.Windows.Forms.Padding(0);
      this.lbTitleReport.Name = "lbTitleReport";
      this.lbTitleReport.Size = new System.Drawing.Size(1884, 50);
      this.lbTitleReport.TabIndex = 3;
      this.lbTitleReport.Text = "BÁO CÁO";
      this.lbTitleReport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // timer1
      // 
      this.timer1.Interval = 1000;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.label9, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(1904, 1089);
      this.tableLayoutPanel1.TabIndex = 2;
      // 
      // label9
      // 
      this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label9.AutoSize = true;
      this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
      this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label9.ForeColor = System.Drawing.Color.White;
      this.label9.Location = new System.Drawing.Point(0, 0);
      this.label9.Margin = new System.Windows.Forms.Padding(0);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(1904, 60);
      this.label9.TabIndex = 4;
      this.label9.Text = "ĐANG XUẤT BÁO CÁO TỰ ĐỘNG. VUI LÒNG CHỜ ĐỢI TRONG GIÂY LÁT ...";
      this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // tableLayoutPanel7
      // 
      this.tableLayoutPanel7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tableLayoutPanel7.ColumnCount = 1;
      this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel7.Controls.Add(this.chartAvgSigma, 0, 0);
      this.tableLayoutPanel7.Controls.Add(this.label5, 0, 1);
      this.tableLayoutPanel7.Location = new System.Drawing.Point(1570, 469);
      this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel7.Name = "tableLayoutPanel7";
      this.tableLayoutPanel7.RowCount = 2;
      this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel7.Size = new System.Drawing.Size(314, 469);
      this.tableLayoutPanel7.TabIndex = 16;
      // 
      // label5
      // 
      this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label5.AutoSize = true;
      this.label5.BackColor = System.Drawing.Color.DarkSlateGray;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.ForeColor = System.Drawing.Color.White;
      this.label5.Location = new System.Drawing.Point(1, 439);
      this.label5.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(312, 30);
      this.label5.TabIndex = 7;
      this.label5.Text = "Sigma";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // chartAvgSigma
      // 
      this.chartAvgSigma.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.chartAvgSigma.Location = new System.Drawing.Point(3, 3);
      this.chartAvgSigma.Name = "chartAvgSigma";
      this.chartAvgSigma.Size = new System.Drawing.Size(308, 433);
      this.chartAvgSigma.TabIndex = 6;
      // 
      // chartTopSigma
      // 
      this.chartTopSigma.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.chartTopSigma.Location = new System.Drawing.Point(1574, 4);
      this.chartTopSigma.Margin = new System.Windows.Forms.Padding(4);
      this.chartTopSigma.Name = "chartTopSigma";
      this.chartTopSigma.Size = new System.Drawing.Size(306, 461);
      this.chartTopSigma.TabIndex = 15;
      // 
      // chartAvgStdev
      // 
      this.chartAvgStdev.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.chartAvgStdev.Location = new System.Drawing.Point(3, 3);
      this.chartAvgStdev.Name = "chartAvgStdev";
      this.chartAvgStdev.Size = new System.Drawing.Size(308, 433);
      this.chartAvgStdev.TabIndex = 6;
      // 
      // chartAvgCpk
      // 
      this.chartAvgCpk.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.chartAvgCpk.Location = new System.Drawing.Point(3, 3);
      this.chartAvgCpk.Name = "chartAvgCpk";
      this.chartAvgCpk.Size = new System.Drawing.Size(308, 433);
      this.chartAvgCpk.TabIndex = 6;
      // 
      // chartAvgLoss
      // 
      this.chartAvgLoss.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.chartAvgLoss.Location = new System.Drawing.Point(3, 3);
      this.chartAvgLoss.Name = "chartAvgLoss";
      this.chartAvgLoss.Size = new System.Drawing.Size(308, 433);
      this.chartAvgLoss.TabIndex = 6;
      // 
      // chartAvgError
      // 
      this.chartAvgError.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.chartAvgError.Location = new System.Drawing.Point(3, 2);
      this.chartAvgError.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.chartAvgError.Name = "chartAvgError";
      this.chartAvgError.Size = new System.Drawing.Size(308, 435);
      this.chartAvgError.TabIndex = 6;
      // 
      // chartTopStdev
      // 
      this.chartTopStdev.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.chartTopStdev.Location = new System.Drawing.Point(1259, 3);
      this.chartTopStdev.Name = "chartTopStdev";
      this.chartTopStdev.Size = new System.Drawing.Size(308, 463);
      this.chartTopStdev.TabIndex = 5;
      // 
      // chartTopCpk
      // 
      this.chartTopCpk.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.chartTopCpk.Location = new System.Drawing.Point(945, 3);
      this.chartTopCpk.Name = "chartTopCpk";
      this.chartTopCpk.Size = new System.Drawing.Size(308, 463);
      this.chartTopCpk.TabIndex = 4;
      // 
      // chartTopLoss
      // 
      this.chartTopLoss.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.chartTopLoss.Location = new System.Drawing.Point(631, 3);
      this.chartTopLoss.Name = "chartTopLoss";
      this.chartTopLoss.Size = new System.Drawing.Size(308, 463);
      this.chartTopLoss.TabIndex = 3;
      // 
      // chartPie
      // 
      this.chartPie.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.chartPie.BackColor = System.Drawing.Color.White;
      this.chartPie.Location = new System.Drawing.Point(3, 472);
      this.chartPie.Name = "chartPie";
      this.chartPie.Size = new System.Drawing.Size(308, 463);
      this.chartPie.TabIndex = 1;
      // 
      // sumary
      // 
      this.sumary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.sumary.BackColor = System.Drawing.Color.White;
      this.sumary.Location = new System.Drawing.Point(3, 3);
      this.sumary.Name = "sumary";
      this.sumary.Size = new System.Drawing.Size(308, 463);
      this.sumary.TabIndex = 0;
      // 
      // chartTopError
      // 
      this.chartTopError.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.chartTopError.Location = new System.Drawing.Point(317, 2);
      this.chartTopError.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.chartTopError.Name = "chartTopError";
      this.chartTopError.Size = new System.Drawing.Size(308, 465);
      this.chartTopError.TabIndex = 2;
      // 
      // FrmReportAutoPdf
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1904, 1089);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "FrmReportAutoPdf";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Load += new System.EventHandler(this.ReportAutoPdf_Load);
      this.tabControl1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.page1.ResumeLayout(false);
      this.page1.PerformLayout();
      this.tableLayoutPanel2.ResumeLayout(false);
      this.tableLayoutPanel6.ResumeLayout(false);
      this.tableLayoutPanel6.PerformLayout();
      this.tableLayoutPanel5.ResumeLayout(false);
      this.tableLayoutPanel5.PerformLayout();
      this.tableLayoutPanel3.ResumeLayout(false);
      this.tableLayoutPanel3.PerformLayout();
      this.tableLayoutPanel4.ResumeLayout(false);
      this.tableLayoutPanel4.PerformLayout();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.tableLayoutPanel7.ResumeLayout(false);
      this.tableLayoutPanel7.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TableLayoutPanel page1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
    private UcUI.UcChartV2 chartAvgStdev;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
    private UcUI.UcChartV2 chartAvgCpk;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    private UcUI.UcChartV2 chartAvgLoss;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    private System.Windows.Forms.Label label1;
    private UcUI.UcChartV2 chartAvgError;
    private UcUI.UcChartV1 chartTopStdev;
    private UcUI.UcChartV1 chartTopCpk;
    private UcUI.UcChartV1 chartTopLoss;
    private UcUI.UcChartPie chartPie;
    private UcUI.UcChartV1 chartTopError;
    private UcUI.UcSumary sumary;
    private System.Windows.Forms.Label lbTitleReport;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
    private UcUI.UcChartV2 chartAvgSigma;
    private System.Windows.Forms.Label label5;
    private UcUI.UcChartV1 chartTopSigma;
  }
}