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
      this.ucChartV2_avgStdevMonthCurrent = new SyngentaWeigherQC.UI.UcUI.UcChartV2();
      this.label4 = new System.Windows.Forms.Label();
      this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
      this.ucChartV2_avgCpkMonthCurrent = new SyngentaWeigherQC.UI.UcUI.UcChartV2();
      this.label3 = new System.Windows.Forms.Label();
      this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
      this.ucChartV2_avgSampleLossMonthCurrent = new SyngentaWeigherQC.UI.UcUI.UcChartV2();
      this.label2 = new System.Windows.Forms.Label();
      this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
      this.label1 = new System.Windows.Forms.Label();
      this.ucChartV2_avgSampleErrorMonthCurrent = new SyngentaWeigherQC.UI.UcUI.UcChartV2();
      this.ucChartV1_avgStdevMonthCurrent = new SyngentaWeigherQC.UI.UcUI.UcChartV1();
      this.ucChartV1_avgCpkMonthCurrent = new SyngentaWeigherQC.UI.UcUI.UcChartV1();
      this.ucChartV1_avgSampleLossMonthCurrent = new SyngentaWeigherQC.UI.UcUI.UcChartV1();
      this.ucChartPieMothCurrent = new SyngentaWeigherQC.UI.UcUI.UcChartPie();
      this.ucSumaryMothCurrent = new SyngentaWeigherQC.UI.UcUI.UcSumary();
      this.ucChartV1_avgSampleErrorMonthCurrent = new SyngentaWeigherQC.UI.UcUI.UcChartV1();
      this.lbTitleMonth = new System.Windows.Forms.Label();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.label9 = new System.Windows.Forms.Label();
      this.tabControl1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.page1.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      this.tableLayoutPanel6.SuspendLayout();
      this.tableLayoutPanel5.SuspendLayout();
      this.tableLayoutPanel3.SuspendLayout();
      this.tableLayoutPanel4.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
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
      this.page1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.page1.ColumnCount = 1;
      this.page1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.page1.Controls.Add(this.tableLayoutPanel2, 0, 1);
      this.page1.Controls.Add(this.lbTitleMonth, 0, 0);
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
      this.tableLayoutPanel2.ColumnCount = 5;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
      this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 4, 1);
      this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 3, 1);
      this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 2, 1);
      this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 1, 1);
      this.tableLayoutPanel2.Controls.Add(this.ucChartV1_avgStdevMonthCurrent, 4, 0);
      this.tableLayoutPanel2.Controls.Add(this.ucChartV1_avgCpkMonthCurrent, 3, 0);
      this.tableLayoutPanel2.Controls.Add(this.ucChartV1_avgSampleLossMonthCurrent, 2, 0);
      this.tableLayoutPanel2.Controls.Add(this.ucChartPieMothCurrent, 0, 1);
      this.tableLayoutPanel2.Controls.Add(this.ucSumaryMothCurrent, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.ucChartV1_avgSampleErrorMonthCurrent, 1, 0);
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
      this.tableLayoutPanel6.Controls.Add(this.ucChartV2_avgStdevMonthCurrent, 0, 0);
      this.tableLayoutPanel6.Controls.Add(this.label4, 0, 1);
      this.tableLayoutPanel6.Location = new System.Drawing.Point(1504, 469);
      this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel6.Name = "tableLayoutPanel6";
      this.tableLayoutPanel6.RowCount = 2;
      this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel6.Size = new System.Drawing.Size(380, 469);
      this.tableLayoutPanel6.TabIndex = 14;
      // 
      // ucChartV2_avgStdevMonthCurrent
      // 
      this.ucChartV2_avgStdevMonthCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ucChartV2_avgStdevMonthCurrent.Location = new System.Drawing.Point(3, 3);
      this.ucChartV2_avgStdevMonthCurrent.Name = "ucChartV2_avgStdevMonthCurrent";
      this.ucChartV2_avgStdevMonthCurrent.Size = new System.Drawing.Size(374, 433);
      this.ucChartV2_avgStdevMonthCurrent.TabIndex = 6;
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
      this.label4.Location = new System.Drawing.Point(3, 439);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(374, 30);
      this.label4.TabIndex = 7;
      this.label4.Text = "stdev";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // tableLayoutPanel5
      // 
      this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tableLayoutPanel5.ColumnCount = 1;
      this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel5.Controls.Add(this.ucChartV2_avgCpkMonthCurrent, 0, 0);
      this.tableLayoutPanel5.Controls.Add(this.label3, 0, 1);
      this.tableLayoutPanel5.Location = new System.Drawing.Point(1128, 469);
      this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel5.Name = "tableLayoutPanel5";
      this.tableLayoutPanel5.RowCount = 2;
      this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel5.Size = new System.Drawing.Size(376, 469);
      this.tableLayoutPanel5.TabIndex = 13;
      // 
      // ucChartV2_avgCpkMonthCurrent
      // 
      this.ucChartV2_avgCpkMonthCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ucChartV2_avgCpkMonthCurrent.Location = new System.Drawing.Point(3, 3);
      this.ucChartV2_avgCpkMonthCurrent.Name = "ucChartV2_avgCpkMonthCurrent";
      this.ucChartV2_avgCpkMonthCurrent.Size = new System.Drawing.Size(370, 433);
      this.ucChartV2_avgCpkMonthCurrent.TabIndex = 6;
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
      this.label3.Location = new System.Drawing.Point(3, 439);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(370, 30);
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
      this.tableLayoutPanel3.Controls.Add(this.ucChartV2_avgSampleLossMonthCurrent, 0, 0);
      this.tableLayoutPanel3.Controls.Add(this.label2, 0, 1);
      this.tableLayoutPanel3.Location = new System.Drawing.Point(752, 469);
      this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel3.Name = "tableLayoutPanel3";
      this.tableLayoutPanel3.RowCount = 2;
      this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel3.Size = new System.Drawing.Size(376, 469);
      this.tableLayoutPanel3.TabIndex = 12;
      // 
      // ucChartV2_avgSampleLossMonthCurrent
      // 
      this.ucChartV2_avgSampleLossMonthCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ucChartV2_avgSampleLossMonthCurrent.Location = new System.Drawing.Point(3, 3);
      this.ucChartV2_avgSampleLossMonthCurrent.Name = "ucChartV2_avgSampleLossMonthCurrent";
      this.ucChartV2_avgSampleLossMonthCurrent.Size = new System.Drawing.Size(370, 433);
      this.ucChartV2_avgSampleLossMonthCurrent.TabIndex = 6;
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
      this.label2.Location = new System.Drawing.Point(3, 439);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(370, 30);
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
      this.tableLayoutPanel4.Controls.Add(this.ucChartV2_avgSampleErrorMonthCurrent, 0, 0);
      this.tableLayoutPanel4.Location = new System.Drawing.Point(376, 469);
      this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel4.Name = "tableLayoutPanel4";
      this.tableLayoutPanel4.RowCount = 2;
      this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel4.Size = new System.Drawing.Size(376, 469);
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
      this.label1.Location = new System.Drawing.Point(3, 439);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(370, 30);
      this.label1.TabIndex = 7;
      this.label1.Text = "% Mẫu lỗi";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // ucChartV2_avgSampleErrorMonthCurrent
      // 
      this.ucChartV2_avgSampleErrorMonthCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ucChartV2_avgSampleErrorMonthCurrent.Location = new System.Drawing.Point(3, 2);
      this.ucChartV2_avgSampleErrorMonthCurrent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.ucChartV2_avgSampleErrorMonthCurrent.Name = "ucChartV2_avgSampleErrorMonthCurrent";
      this.ucChartV2_avgSampleErrorMonthCurrent.Size = new System.Drawing.Size(370, 435);
      this.ucChartV2_avgSampleErrorMonthCurrent.TabIndex = 6;
      // 
      // ucChartV1_avgStdevMonthCurrent
      // 
      this.ucChartV1_avgStdevMonthCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ucChartV1_avgStdevMonthCurrent.Location = new System.Drawing.Point(1507, 3);
      this.ucChartV1_avgStdevMonthCurrent.Name = "ucChartV1_avgStdevMonthCurrent";
      this.ucChartV1_avgStdevMonthCurrent.Size = new System.Drawing.Size(374, 463);
      this.ucChartV1_avgStdevMonthCurrent.TabIndex = 5;
      // 
      // ucChartV1_avgCpkMonthCurrent
      // 
      this.ucChartV1_avgCpkMonthCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ucChartV1_avgCpkMonthCurrent.Location = new System.Drawing.Point(1131, 3);
      this.ucChartV1_avgCpkMonthCurrent.Name = "ucChartV1_avgCpkMonthCurrent";
      this.ucChartV1_avgCpkMonthCurrent.Size = new System.Drawing.Size(370, 463);
      this.ucChartV1_avgCpkMonthCurrent.TabIndex = 4;
      // 
      // ucChartV1_avgSampleLossMonthCurrent
      // 
      this.ucChartV1_avgSampleLossMonthCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ucChartV1_avgSampleLossMonthCurrent.Location = new System.Drawing.Point(755, 3);
      this.ucChartV1_avgSampleLossMonthCurrent.Name = "ucChartV1_avgSampleLossMonthCurrent";
      this.ucChartV1_avgSampleLossMonthCurrent.Size = new System.Drawing.Size(370, 463);
      this.ucChartV1_avgSampleLossMonthCurrent.TabIndex = 3;
      // 
      // ucChartPieMothCurrent
      // 
      this.ucChartPieMothCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ucChartPieMothCurrent.BackColor = System.Drawing.Color.White;
      this.ucChartPieMothCurrent.Location = new System.Drawing.Point(3, 472);
      this.ucChartPieMothCurrent.Name = "ucChartPieMothCurrent";
      this.ucChartPieMothCurrent.Size = new System.Drawing.Size(370, 463);
      this.ucChartPieMothCurrent.TabIndex = 1;
      // 
      // ucSumaryMothCurrent
      // 
      this.ucSumaryMothCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ucSumaryMothCurrent.BackColor = System.Drawing.Color.White;
      this.ucSumaryMothCurrent.Location = new System.Drawing.Point(3, 3);
      this.ucSumaryMothCurrent.Name = "ucSumaryMothCurrent";
      this.ucSumaryMothCurrent.Size = new System.Drawing.Size(370, 463);
      this.ucSumaryMothCurrent.TabIndex = 0;
      // 
      // ucChartV1_avgSampleErrorMonthCurrent
      // 
      this.ucChartV1_avgSampleErrorMonthCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ucChartV1_avgSampleErrorMonthCurrent.Location = new System.Drawing.Point(379, 2);
      this.ucChartV1_avgSampleErrorMonthCurrent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.ucChartV1_avgSampleErrorMonthCurrent.Name = "ucChartV1_avgSampleErrorMonthCurrent";
      this.ucChartV1_avgSampleErrorMonthCurrent.Size = new System.Drawing.Size(370, 465);
      this.ucChartV1_avgSampleErrorMonthCurrent.TabIndex = 2;
      // 
      // lbTitleMonth
      // 
      this.lbTitleMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lbTitleMonth.AutoSize = true;
      this.lbTitleMonth.BackColor = System.Drawing.Color.Black;
      this.lbTitleMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbTitleMonth.ForeColor = System.Drawing.Color.White;
      this.lbTitleMonth.Location = new System.Drawing.Point(0, 0);
      this.lbTitleMonth.Margin = new System.Windows.Forms.Padding(0);
      this.lbTitleMonth.Name = "lbTitleMonth";
      this.lbTitleMonth.Size = new System.Drawing.Size(1884, 50);
      this.lbTitleMonth.TabIndex = 3;
      this.lbTitleMonth.Text = "BÁO CÁO";
      this.lbTitleMonth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TableLayoutPanel page1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
    private UcUI.UcChartV2 ucChartV2_avgStdevMonthCurrent;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
    private UcUI.UcChartV2 ucChartV2_avgCpkMonthCurrent;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    private UcUI.UcChartV2 ucChartV2_avgSampleLossMonthCurrent;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    private System.Windows.Forms.Label label1;
    private UcUI.UcChartV2 ucChartV2_avgSampleErrorMonthCurrent;
    private UcUI.UcChartV1 ucChartV1_avgStdevMonthCurrent;
    private UcUI.UcChartV1 ucChartV1_avgCpkMonthCurrent;
    private UcUI.UcChartV1 ucChartV1_avgSampleLossMonthCurrent;
    private UcUI.UcChartPie ucChartPieMothCurrent;
    private UcUI.UcChartV1 ucChartV1_avgSampleErrorMonthCurrent;
    private UcUI.UcSumary ucSumaryMothCurrent;
    private System.Windows.Forms.Label lbTitleMonth;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Label label9;
  }
}