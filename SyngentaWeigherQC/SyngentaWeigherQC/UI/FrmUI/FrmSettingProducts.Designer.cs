namespace SyngentaWeigherQC.UI.FrmUI
{
  partial class FrmSettingProducts
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettingProducts));
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.label2 = new System.Windows.Forms.Label();
      this.btnCheckHistorical = new CustomControls.RJControls.RJButton();
      this.btnSaveChange = new CustomControls.RJControls.RJButton();
      this.btnImportExcel = new CustomControls.RJControls.RJButton();
      this.btnAdd = new CustomControls.RJControls.RJButton();
      this.btnSearch = new CustomControls.RJControls.RJButton();
      this.txtSearch = new CustomControls.RJControls.RJTextBox();
      this.dgvMasterData = new System.Windows.Forms.DataGridView();
      this.openFileDialogImport = new System.Windows.Forms.OpenFileDialog();
      this.backgroundWorkerImport = new System.ComponentModel.BackgroundWorker();
      this.tableLayoutPanel1.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvMasterData)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(17)))), ((int)(((byte)(70)))));
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.dgvMasterData, 0, 3);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 4;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(1324, 645);
      this.tableLayoutPanel1.TabIndex = 1;
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(17)))), ((int)(((byte)(70)))));
      this.tableLayoutPanel2.ColumnCount = 8;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.42857F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 325F));
      this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.btnCheckHistorical, 7, 0);
      this.tableLayoutPanel2.Controls.Add(this.btnSaveChange, 6, 0);
      this.tableLayoutPanel2.Controls.Add(this.btnImportExcel, 5, 0);
      this.tableLayoutPanel2.Controls.Add(this.btnAdd, 4, 0);
      this.tableLayoutPanel2.Controls.Add(this.btnSearch, 2, 0);
      this.tableLayoutPanel2.Controls.Add(this.txtSearch, 1, 0);
      this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 3);
      this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 1;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(1324, 60);
      this.tableLayoutPanel2.TabIndex = 0;
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label2.AutoSize = true;
      this.label2.BackColor = System.Drawing.Color.Transparent;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.ForeColor = System.Drawing.Color.White;
      this.label2.Location = new System.Drawing.Point(0, 0);
      this.label2.Margin = new System.Windows.Forms.Padding(0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(101, 60);
      this.label2.TabIndex = 17;
      this.label2.Text = "Tìm kiếm:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // btnCheckHistorical
      // 
      this.btnCheckHistorical.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCheckHistorical.BackColor = System.Drawing.Color.CornflowerBlue;
      this.btnCheckHistorical.BackgroundColor = System.Drawing.Color.CornflowerBlue;
      this.btnCheckHistorical.BorderColor = System.Drawing.Color.PaleVioletRed;
      this.btnCheckHistorical.BorderRadius = 8;
      this.btnCheckHistorical.BorderSize = 0;
      this.btnCheckHistorical.FlatAppearance.BorderSize = 0;
      this.btnCheckHistorical.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnCheckHistorical.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnCheckHistorical.ForeColor = System.Drawing.Color.White;
      this.btnCheckHistorical.Location = new System.Drawing.Point(1002, 8);
      this.btnCheckHistorical.Name = "btnCheckHistorical";
      this.btnCheckHistorical.Size = new System.Drawing.Size(319, 44);
      this.btnCheckHistorical.TabIndex = 16;
      this.btnCheckHistorical.Text = "Lịch sử Import";
      this.btnCheckHistorical.TextColor = System.Drawing.Color.White;
      this.btnCheckHistorical.UseVisualStyleBackColor = false;
      this.btnCheckHistorical.Click += new System.EventHandler(this.btnCheckHistorical_Click);
      // 
      // btnSaveChange
      // 
      this.btnSaveChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSaveChange.BackColor = System.Drawing.Color.CornflowerBlue;
      this.btnSaveChange.BackgroundColor = System.Drawing.Color.CornflowerBlue;
      this.btnSaveChange.BorderColor = System.Drawing.Color.PaleVioletRed;
      this.btnSaveChange.BorderRadius = 8;
      this.btnSaveChange.BorderSize = 0;
      this.btnSaveChange.FlatAppearance.BorderSize = 0;
      this.btnSaveChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnSaveChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnSaveChange.ForeColor = System.Drawing.Color.White;
      this.btnSaveChange.Location = new System.Drawing.Point(816, 8);
      this.btnSaveChange.Name = "btnSaveChange";
      this.btnSaveChange.Size = new System.Drawing.Size(180, 44);
      this.btnSaveChange.TabIndex = 15;
      this.btnSaveChange.Text = "Lưu thay đổi";
      this.btnSaveChange.TextColor = System.Drawing.Color.White;
      this.btnSaveChange.UseVisualStyleBackColor = false;
      this.btnSaveChange.Click += new System.EventHandler(this.btnSaveChange_Click);
      // 
      // btnImportExcel
      // 
      this.btnImportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.btnImportExcel.BackColor = System.Drawing.Color.CornflowerBlue;
      this.btnImportExcel.BackgroundColor = System.Drawing.Color.CornflowerBlue;
      this.btnImportExcel.BorderColor = System.Drawing.Color.PaleVioletRed;
      this.btnImportExcel.BorderRadius = 8;
      this.btnImportExcel.BorderSize = 0;
      this.btnImportExcel.FlatAppearance.BorderSize = 0;
      this.btnImportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnImportExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnImportExcel.ForeColor = System.Drawing.Color.White;
      this.btnImportExcel.Location = new System.Drawing.Point(646, 8);
      this.btnImportExcel.Name = "btnImportExcel";
      this.btnImportExcel.Size = new System.Drawing.Size(164, 44);
      this.btnImportExcel.TabIndex = 14;
      this.btnImportExcel.Text = "Import Excel";
      this.btnImportExcel.TextColor = System.Drawing.Color.White;
      this.btnImportExcel.UseVisualStyleBackColor = false;
      this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
      // 
      // btnAdd
      // 
      this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAdd.BackColor = System.Drawing.Color.CornflowerBlue;
      this.btnAdd.BackgroundColor = System.Drawing.Color.CornflowerBlue;
      this.btnAdd.BorderColor = System.Drawing.Color.PaleVioletRed;
      this.btnAdd.BorderRadius = 8;
      this.btnAdd.BorderSize = 0;
      this.btnAdd.FlatAppearance.BorderSize = 0;
      this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnAdd.ForeColor = System.Drawing.Color.White;
      this.btnAdd.Location = new System.Drawing.Point(476, 8);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(164, 44);
      this.btnAdd.TabIndex = 13;
      this.btnAdd.Text = "Thêm dữ liệu";
      this.btnAdd.TextColor = System.Drawing.Color.White;
      this.btnAdd.UseVisualStyleBackColor = false;
      this.btnAdd.Visible = false;
      this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
      // 
      // btnSearch
      // 
      this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSearch.BackColor = System.Drawing.Color.CornflowerBlue;
      this.btnSearch.BackgroundColor = System.Drawing.Color.CornflowerBlue;
      this.btnSearch.BorderColor = System.Drawing.Color.PaleVioletRed;
      this.btnSearch.BorderRadius = 5;
      this.btnSearch.BorderSize = 0;
      this.btnSearch.FlatAppearance.BorderSize = 0;
      this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnSearch.ForeColor = System.Drawing.Color.White;
      this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
      this.btnSearch.Location = new System.Drawing.Point(334, 9);
      this.btnSearch.Name = "btnSearch";
      this.btnSearch.Size = new System.Drawing.Size(44, 42);
      this.btnSearch.TabIndex = 18;
      this.btnSearch.TextColor = System.Drawing.Color.White;
      this.btnSearch.UseVisualStyleBackColor = false;
      this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
      // 
      // txtSearch
      // 
      this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSearch.BackColor = System.Drawing.SystemColors.Window;
      this.txtSearch.BorderColor = System.Drawing.Color.MediumSlateBlue;
      this.txtSearch.BorderFocusColor = System.Drawing.Color.HotPink;
      this.txtSearch.BorderRadius = 5;
      this.txtSearch.BorderSize = 2;
      this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.txtSearch.Location = new System.Drawing.Point(105, 10);
      this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
      this.txtSearch.Multiline = false;
      this.txtSearch.Name = "txtSearch";
      this.txtSearch.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
      this.txtSearch.PasswordChar = false;
      this.txtSearch.PlaceholderColor = System.Drawing.Color.DarkGray;
      this.txtSearch.PlaceholderText = "";
      this.txtSearch.Size = new System.Drawing.Size(222, 39);
      this.txtSearch.TabIndex = 19;
      this.txtSearch.Texts = "";
      this.txtSearch.UnderlinedStyle = false;
      // 
      // dgvMasterData
      // 
      this.dgvMasterData.AllowUserToResizeRows = false;
      this.dgvMasterData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgvMasterData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
      this.dgvMasterData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(61)))), ((int)(((byte)(90)))));
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(61)))), ((int)(((byte)(90)))));
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgvMasterData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dgvMasterData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dgvMasterData.DefaultCellStyle = dataGridViewCellStyle2;
      this.dgvMasterData.EnableHeadersVisualStyles = false;
      this.dgvMasterData.Location = new System.Drawing.Point(3, 69);
      this.dgvMasterData.Name = "dgvMasterData";
      this.dgvMasterData.ReadOnly = true;
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgvMasterData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
      this.dgvMasterData.RowHeadersVisible = false;
      this.dgvMasterData.RowHeadersWidth = 60;
      this.dgvMasterData.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(61)))), ((int)(((byte)(90)))));
      this.dgvMasterData.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
      this.dgvMasterData.RowTemplate.Height = 40;
      this.dgvMasterData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgvMasterData.Size = new System.Drawing.Size(1318, 573);
      this.dgvMasterData.TabIndex = 1;
      this.dgvMasterData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMasterData_CellDoubleClick);
      this.dgvMasterData.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvMasterData_Scroll);
      // 
      // openFileDialogImport
      // 
      this.openFileDialogImport.FileName = "openFileDialog1";
      this.openFileDialogImport.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogImport_FileOk);
      // 
      // backgroundWorkerImport
      // 
      this.backgroundWorkerImport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerImport_DoWork);
      this.backgroundWorkerImport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerImport_RunWorkerCompleted);
      // 
      // FrmSettingProducts
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1324, 645);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "FrmSettingProducts";
      this.Text = "FrmSettingProducts";
      this.Load += new System.EventHandler(this.FrmSettingProducts_Load);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel2.ResumeLayout(false);
      this.tableLayoutPanel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvMasterData)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.DataGridView dgvMasterData;
    private System.Windows.Forms.OpenFileDialog openFileDialogImport;
    private System.ComponentModel.BackgroundWorker backgroundWorkerImport;
    private CustomControls.RJControls.RJButton btnSaveChange;
    private CustomControls.RJControls.RJButton btnImportExcel;
    private CustomControls.RJControls.RJButton btnAdd;
    private CustomControls.RJControls.RJButton btnCheckHistorical;
    private System.Windows.Forms.Label label2;
    private CustomControls.RJControls.RJButton btnSearch;
    private CustomControls.RJControls.RJTextBox txtSearch;
  }
}