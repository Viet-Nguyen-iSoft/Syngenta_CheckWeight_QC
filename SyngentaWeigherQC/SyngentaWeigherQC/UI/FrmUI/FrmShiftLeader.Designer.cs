namespace SyngentaWeigherQC.UI.FrmUI
{
  partial class FrmShiftLeader
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.dataGridView2 = new System.Windows.Forms.DataGridView();
      this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewButtonColumn();
      this.Column3 = new System.Windows.Forms.DataGridViewButtonColumn();
      this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
      this.btnAddNew = new CustomControls.RJControls.RJButton();
      this.label2 = new System.Windows.Forms.Label();
      this.btnSave = new CustomControls.RJControls.RJButton();
      this.btnImport = new CustomControls.RJControls.RJButton();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.backgroundWorkerImport = new System.ComponentModel.BackgroundWorker();
      this.openFileDialogImport = new System.Windows.Forms.OpenFileDialog();
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
      this.tableLayoutPanel3.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.83178F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.16822F));
      this.tableLayoutPanel1.Controls.Add(this.dataGridView2, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
      this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 13);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(1281, 646);
      this.tableLayoutPanel1.TabIndex = 1;
      // 
      // dataGridView2
      // 
      this.dataGridView2.AllowUserToAddRows = false;
      this.dataGridView2.AllowUserToResizeColumns = false;
      this.dataGridView2.AllowUserToResizeRows = false;
      this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(61)))), ((int)(((byte)(90)))));
      this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
      dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(61)))), ((int)(((byte)(90)))));
      dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
      dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
      this.dataGridView2.ColumnHeadersHeight = 40;
      this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column1,
            this.Column2,
            this.Column3});
      dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle6;
      this.dataGridView2.EnableHeadersVisualStyles = false;
      this.dataGridView2.Location = new System.Drawing.Point(3, 63);
      this.dataGridView2.Name = "dataGridView2";
      this.dataGridView2.ReadOnly = true;
      dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
      this.dataGridView2.RowHeadersVisible = false;
      dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
      this.dataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle8;
      this.dataGridView2.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(61)))), ((int)(((byte)(90)))));
      this.dataGridView2.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
      this.dataGridView2.RowTemplate.Height = 40;
      this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dataGridView2.Size = new System.Drawing.Size(683, 580);
      this.dataGridView2.TabIndex = 3;
      this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
      // 
      // Column4
      // 
      this.Column4.FillWeight = 10F;
      this.Column4.HeaderText = "Stt";
      this.Column4.Name = "Column4";
      this.Column4.ReadOnly = true;
      // 
      // Column1
      // 
      this.Column1.FillWeight = 48.24385F;
      this.Column1.HeaderText = "Họ và tên";
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      // 
      // Column2
      // 
      this.Column2.FillWeight = 20F;
      this.Column2.HeaderText = "";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      // 
      // Column3
      // 
      this.Column3.FillWeight = 20F;
      this.Column3.HeaderText = "";
      this.Column3.Name = "Column3";
      this.Column3.ReadOnly = true;
      this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      // 
      // tableLayoutPanel3
      // 
      this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tableLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
      this.tableLayoutPanel3.ColumnCount = 4;
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel3.Controls.Add(this.btnAddNew, 2, 0);
      this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
      this.tableLayoutPanel3.Controls.Add(this.btnSave, 3, 0);
      this.tableLayoutPanel3.Controls.Add(this.btnImport, 1, 0);
      this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
      this.tableLayoutPanel3.Name = "tableLayoutPanel3";
      this.tableLayoutPanel3.RowCount = 1;
      this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel3.Size = new System.Drawing.Size(683, 54);
      this.tableLayoutPanel3.TabIndex = 4;
      // 
      // btnAddNew
      // 
      this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAddNew.BackColor = System.Drawing.Color.PaleVioletRed;
      this.btnAddNew.BackgroundColor = System.Drawing.Color.PaleVioletRed;
      this.btnAddNew.BorderColor = System.Drawing.Color.PaleVioletRed;
      this.btnAddNew.BorderRadius = 5;
      this.btnAddNew.BorderSize = 0;
      this.btnAddNew.FlatAppearance.BorderSize = 0;
      this.btnAddNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnAddNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnAddNew.ForeColor = System.Drawing.Color.White;
      this.btnAddNew.Location = new System.Drawing.Point(426, 4);
      this.btnAddNew.Name = "btnAddNew";
      this.btnAddNew.Size = new System.Drawing.Size(124, 45);
      this.btnAddNew.TabIndex = 13;
      this.btnAddNew.Text = "Thêm";
      this.btnAddNew.TextColor = System.Drawing.Color.White;
      this.btnAddNew.UseVisualStyleBackColor = false;
      this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
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
      this.label2.Size = new System.Drawing.Size(293, 54);
      this.label2.TabIndex = 1;
      this.label2.Text = "Danh sách:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.BackColor = System.Drawing.Color.DarkGreen;
      this.btnSave.BackgroundColor = System.Drawing.Color.DarkGreen;
      this.btnSave.BorderColor = System.Drawing.Color.PaleVioletRed;
      this.btnSave.BorderRadius = 5;
      this.btnSave.BorderSize = 0;
      this.btnSave.FlatAppearance.BorderSize = 0;
      this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnSave.ForeColor = System.Drawing.Color.White;
      this.btnSave.Location = new System.Drawing.Point(556, 4);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(124, 45);
      this.btnSave.TabIndex = 11;
      this.btnSave.Text = "Lưu";
      this.btnSave.TextColor = System.Drawing.Color.White;
      this.btnSave.UseVisualStyleBackColor = false;
      this.btnSave.Visible = false;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnImport
      // 
      this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.btnImport.BackColor = System.Drawing.Color.LightSeaGreen;
      this.btnImport.BackgroundColor = System.Drawing.Color.LightSeaGreen;
      this.btnImport.BorderColor = System.Drawing.Color.PaleVioletRed;
      this.btnImport.BorderRadius = 5;
      this.btnImport.BorderSize = 0;
      this.btnImport.FlatAppearance.BorderSize = 0;
      this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnImport.ForeColor = System.Drawing.Color.White;
      this.btnImport.Location = new System.Drawing.Point(296, 4);
      this.btnImport.Name = "btnImport";
      this.btnImport.Size = new System.Drawing.Size(124, 45);
      this.btnImport.TabIndex = 12;
      this.btnImport.Text = "Import";
      this.btnImport.TextColor = System.Drawing.Color.White;
      this.btnImport.UseVisualStyleBackColor = false;
      this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(17)))), ((int)(((byte)(70)))));
      this.tableLayoutPanel2.ColumnCount = 3;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 1, 1);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 3;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(1312, 682);
      this.tableLayoutPanel2.TabIndex = 2;
      // 
      // backgroundWorkerImport
      // 
      this.backgroundWorkerImport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerImport_DoWork);
      this.backgroundWorkerImport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerImport_RunWorkerCompleted);
      // 
      // openFileDialogImport
      // 
      this.openFileDialogImport.FileName = "openFileDialog1";
      this.openFileDialogImport.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogImport_FileOk);
      // 
      // FrmShiftLeader
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1312, 682);
      this.Controls.Add(this.tableLayoutPanel2);
      this.Name = "FrmShiftLeader";
      this.Text = "FrmUser";
      this.Load += new System.EventHandler(this.FrmUser_Load);
      this.tableLayoutPanel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
      this.tableLayoutPanel3.ResumeLayout(false);
      this.tableLayoutPanel3.PerformLayout();
      this.tableLayoutPanel2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.DataGridView dataGridView2;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    private System.ComponentModel.BackgroundWorker backgroundWorkerImport;
    private System.Windows.Forms.OpenFileDialog openFileDialogImport;
    private CustomControls.RJControls.RJButton btnSave;
    private CustomControls.RJControls.RJButton btnImport;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewButtonColumn Column2;
    private System.Windows.Forms.DataGridViewButtonColumn Column3;
    private CustomControls.RJControls.RJButton btnAddNew;
  }
}