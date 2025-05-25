namespace SyngentaWeigherQC.UI.FrmUI
{
  partial class FrmExcelExport
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.label4 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
      this.label2 = new System.Windows.Forms.Label();
      this.btnExport = new System.Windows.Forms.Button();
      this.cbShift = new System.Windows.Forms.ComboBox();
      this.btnPreview = new System.Windows.Forms.Button();
      this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
      this.label3 = new System.Windows.Forms.Label();
      this.cbbLine = new System.Windows.Forms.ComboBox();
      this.flowLayoutPanelProduct = new System.Windows.Forms.FlowLayoutPanel();
      this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
      this.ucChartHistogram1 = new SyngentaWeigherQC.UI.UcUI.UcChartHistogram();
      this.ucChartLine1 = new SyngentaWeigherQC.UI.UcUI.UcChartLine();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.ucTemplateExcel1 = new SyngentaWeigherQC.UI.UcUI.UcTemplateExcel();
      this.backgroundWorkerLoadData = new System.ComponentModel.BackgroundWorker();
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      this.tableLayoutPanel2.SuspendLayout();
      this.tableLayoutPanel3.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(17)))), ((int)(((byte)(70)))));
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 6);
      this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanelProduct, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 5);
      this.tableLayoutPanel1.Controls.Add(this.progressBar1, 0, 3);
      this.tableLayoutPanel1.Controls.Add(this.ucTemplateExcel1, 0, 4);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 7;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 325F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.30534F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.69466F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(1787, 955);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // dataGridView1
      // 
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToResizeColumns = false;
      this.dataGridView1.AllowUserToResizeRows = false;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
      this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(17)))), ((int)(((byte)(70)))));
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
      this.dataGridView1.ColumnHeadersHeight = 40;
      this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column20,
            this.Column21,
            this.Column12,
            this.Column13,
            this.Column15});
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
      this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataGridView1.EnableHeadersVisualStyles = false;
      this.dataGridView1.Location = new System.Drawing.Point(3, 758);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.ReadOnly = true;
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlDarkDark;
      dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
      this.dataGridView1.RowHeadersVisible = false;
      dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
      this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle5;
      this.dataGridView1.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
      this.dataGridView1.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
      this.dataGridView1.RowTemplate.Height = 40;
      this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
      this.dataGridView1.Size = new System.Drawing.Size(1781, 194);
      this.dataGridView1.TabIndex = 10;
      // 
      // Column1
      // 
      this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.Column1.HeaderText = "Ca";
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      this.Column1.Width = 58;
      // 
      // Column2
      // 
      this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.Column2.HeaderText = "STT";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      this.Column2.Width = 71;
      // 
      // Column3
      // 
      this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.Column3.HeaderText = "DateTime";
      this.Column3.Name = "Column3";
      this.Column3.ReadOnly = true;
      this.Column3.Width = 116;
      // 
      // Column4
      // 
      this.Column4.HeaderText = "Mẫu 1";
      this.Column4.Name = "Column4";
      this.Column4.ReadOnly = true;
      // 
      // Column5
      // 
      this.Column5.HeaderText = "Mẫu 2";
      this.Column5.Name = "Column5";
      this.Column5.ReadOnly = true;
      // 
      // Column6
      // 
      this.Column6.HeaderText = "Mẫu 3";
      this.Column6.Name = "Column6";
      this.Column6.ReadOnly = true;
      // 
      // Column7
      // 
      this.Column7.HeaderText = "Mẫu 4";
      this.Column7.Name = "Column7";
      this.Column7.ReadOnly = true;
      // 
      // Column8
      // 
      this.Column8.HeaderText = "Mẫu 5";
      this.Column8.Name = "Column8";
      this.Column8.ReadOnly = true;
      // 
      // Column9
      // 
      this.Column9.HeaderText = "Mẫu 6";
      this.Column9.Name = "Column9";
      this.Column9.ReadOnly = true;
      // 
      // Column10
      // 
      this.Column10.HeaderText = "Mẫu 7";
      this.Column10.Name = "Column10";
      this.Column10.ReadOnly = true;
      // 
      // Column11
      // 
      this.Column11.HeaderText = "Mẫu 8";
      this.Column11.Name = "Column11";
      this.Column11.ReadOnly = true;
      // 
      // Column20
      // 
      this.Column20.HeaderText = "Mẫu 9";
      this.Column20.Name = "Column20";
      this.Column20.ReadOnly = true;
      // 
      // Column21
      // 
      this.Column21.HeaderText = "Mẫu 10";
      this.Column21.Name = "Column21";
      this.Column21.ReadOnly = true;
      // 
      // Column12
      // 
      this.Column12.HeaderText = "TB (Đo)";
      this.Column12.Name = "Column12";
      this.Column12.ReadOnly = true;
      // 
      // Column13
      // 
      this.Column13.HeaderText = "TB";
      this.Column13.Name = "Column13";
      this.Column13.ReadOnly = true;
      // 
      // Column15
      // 
      this.Column15.HeaderText = "Đánh giá";
      this.Column15.Name = "Column15";
      this.Column15.ReadOnly = true;
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tableLayoutPanel2.ColumnCount = 15;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
      this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.label1, 3, 0);
      this.tableLayoutPanel2.Controls.Add(this.dateTimePickerFrom, 4, 0);
      this.tableLayoutPanel2.Controls.Add(this.label2, 9, 0);
      this.tableLayoutPanel2.Controls.Add(this.btnExport, 13, 0);
      this.tableLayoutPanel2.Controls.Add(this.cbShift, 10, 0);
      this.tableLayoutPanel2.Controls.Add(this.btnPreview, 12, 0);
      this.tableLayoutPanel2.Controls.Add(this.dateTimePickerTo, 7, 0);
      this.tableLayoutPanel2.Controls.Add(this.label3, 6, 0);
      this.tableLayoutPanel2.Controls.Add(this.cbbLine, 1, 0);
      this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 5);
      this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 1;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(1787, 45);
      this.tableLayoutPanel2.TabIndex = 0;
      // 
      // label4
      // 
      this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.ForeColor = System.Drawing.Color.White;
      this.label4.Location = new System.Drawing.Point(3, 0);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(56, 45);
      this.label4.TabIndex = 11;
      this.label4.Text = "Line:";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(290, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(35, 45);
      this.label1.TabIndex = 0;
      this.label1.Text = "Từ";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // dateTimePickerFrom
      // 
      this.dateTimePickerFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dateTimePickerFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dateTimePickerFrom.Location = new System.Drawing.Point(331, 8);
      this.dateTimePickerFrom.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
      this.dateTimePickerFrom.Name = "dateTimePickerFrom";
      this.dateTimePickerFrom.Size = new System.Drawing.Size(344, 29);
      this.dateTimePickerFrom.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.ForeColor = System.Drawing.Color.White;
      this.label2.Location = new System.Drawing.Point(1134, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(41, 45);
      this.label2.TabIndex = 1;
      this.label2.Text = "Ca:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // btnExport
      // 
      this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnExport.Location = new System.Drawing.Point(1620, 3);
      this.btnExport.Name = "btnExport";
      this.btnExport.Size = new System.Drawing.Size(154, 39);
      this.btnExport.TabIndex = 5;
      this.btnExport.Text = "Export Excel";
      this.btnExport.UseVisualStyleBackColor = true;
      this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
      // 
      // cbShift
      // 
      this.cbShift.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbShift.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cbShift.FormattingEnabled = true;
      this.cbShift.Items.AddRange(new object[] {
            "Tất cả",
            "Ca 1",
            "Ca 2",
            "Ca 3",
            "Giãn ca 1",
            "Giãn ca 3",
            "Hành chính"});
      this.cbShift.Location = new System.Drawing.Point(1181, 6);
      this.cbShift.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
      this.cbShift.Name = "cbShift";
      this.cbShift.Size = new System.Drawing.Size(179, 32);
      this.cbShift.TabIndex = 7;
      // 
      // btnPreview
      // 
      this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.btnPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnPreview.Location = new System.Drawing.Point(1490, 3);
      this.btnPreview.Name = "btnPreview";
      this.btnPreview.Size = new System.Drawing.Size(124, 39);
      this.btnPreview.TabIndex = 6;
      this.btnPreview.Text = "Xem";
      this.btnPreview.UseVisualStyleBackColor = true;
      this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
      // 
      // dateTimePickerTo
      // 
      this.dateTimePickerTo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dateTimePickerTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dateTimePickerTo.Location = new System.Drawing.Point(759, 8);
      this.dateTimePickerTo.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
      this.dateTimePickerTo.Name = "dateTimePickerTo";
      this.dateTimePickerTo.Size = new System.Drawing.Size(344, 29);
      this.dateTimePickerTo.TabIndex = 8;
      // 
      // label3
      // 
      this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.ForeColor = System.Drawing.Color.White;
      this.label3.Location = new System.Drawing.Point(706, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(47, 45);
      this.label3.TabIndex = 9;
      this.label3.Text = "đến";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cbbLine
      // 
      this.cbbLine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbbLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cbbLine.FormattingEnabled = true;
      this.cbbLine.Location = new System.Drawing.Point(65, 6);
      this.cbbLine.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
      this.cbbLine.Name = "cbbLine";
      this.cbbLine.Size = new System.Drawing.Size(194, 32);
      this.cbbLine.TabIndex = 10;
      // 
      // flowLayoutPanelProduct
      // 
      this.flowLayoutPanelProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.flowLayoutPanelProduct.AutoScroll = true;
      this.flowLayoutPanelProduct.Location = new System.Drawing.Point(1, 51);
      this.flowLayoutPanelProduct.Margin = new System.Windows.Forms.Padding(1);
      this.flowLayoutPanelProduct.Name = "flowLayoutPanelProduct";
      this.flowLayoutPanelProduct.Size = new System.Drawing.Size(1785, 61);
      this.flowLayoutPanelProduct.TabIndex = 1;
      this.flowLayoutPanelProduct.Visible = false;
      this.flowLayoutPanelProduct.WrapContents = false;
      // 
      // tableLayoutPanel3
      // 
      this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tableLayoutPanel3.ColumnCount = 2;
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel3.Controls.Add(this.ucChartHistogram1, 0, 0);
      this.tableLayoutPanel3.Controls.Add(this.ucChartLine1, 1, 0);
      this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 453);
      this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel3.Name = "tableLayoutPanel3";
      this.tableLayoutPanel3.RowCount = 1;
      this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel3.Size = new System.Drawing.Size(1787, 302);
      this.tableLayoutPanel3.TabIndex = 3;
      // 
      // ucChartHistogram1
      // 
      this.ucChartHistogram1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ucChartHistogram1.Location = new System.Drawing.Point(3, 3);
      this.ucChartHistogram1.Name = "ucChartHistogram1";
      this.ucChartHistogram1.Size = new System.Drawing.Size(887, 296);
      this.ucChartHistogram1.TabIndex = 0;
      // 
      // ucChartLine1
      // 
      this.ucChartLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ucChartLine1.Location = new System.Drawing.Point(896, 3);
      this.ucChartLine1.Name = "ucChartLine1";
      this.ucChartLine1.Size = new System.Drawing.Size(888, 296);
      this.ucChartLine1.TabIndex = 1;
      // 
      // progressBar1
      // 
      this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar1.BackColor = System.Drawing.SystemColors.Control;
      this.progressBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
      this.progressBar1.Location = new System.Drawing.Point(3, 116);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(1781, 9);
      this.progressBar1.TabIndex = 9;
      // 
      // ucTemplateExcel1
      // 
      this.ucTemplateExcel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ucTemplateExcel1.Location = new System.Drawing.Point(0, 128);
      this.ucTemplateExcel1.Margin = new System.Windows.Forms.Padding(0);
      this.ucTemplateExcel1.Name = "ucTemplateExcel1";
      this.ucTemplateExcel1.Size = new System.Drawing.Size(1787, 325);
      this.ucTemplateExcel1.TabIndex = 2;
      // 
      // FrmExcelExport
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1787, 955);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "FrmExcelExport";
      this.Text = "FrmExcelExport";
      this.Load += new System.EventHandler(this.FrmExcelExport_Load);
      this.tableLayoutPanel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      this.tableLayoutPanel2.ResumeLayout(false);
      this.tableLayoutPanel2.PerformLayout();
      this.tableLayoutPanel3.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnExport;
    private System.Windows.Forms.ComboBox cbShift;
    private System.Windows.Forms.Button btnPreview;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProduct;
    private UcUI.UcTemplateExcel ucTemplateExcel1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    private UcUI.UcChartHistogram ucChartHistogram1;
    private UcUI.UcChartLine ucChartLine1;
    private System.ComponentModel.BackgroundWorker backgroundWorkerLoadData;
    private System.Windows.Forms.DateTimePicker dateTimePickerTo;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox cbbLine;
    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column20;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column21;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
  }
}