namespace SyngentaWeigherQC.UI.UcUI
{
  partial class UcTime
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
      this.numericUpDownSecond = new System.Windows.Forms.NumericUpDown();
      this.numericUpDownMinute = new System.Windows.Forms.NumericUpDown();
      this.numericUpDownHour = new System.Windows.Forms.NumericUpDown();
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecond)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinute)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHour)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 3;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.Controls.Add(this.numericUpDownSecond, 2, 0);
      this.tableLayoutPanel1.Controls.Add(this.numericUpDownMinute, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.numericUpDownHour, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 1;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(182, 34);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // numericUpDownSecond
      // 
      this.numericUpDownSecond.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.numericUpDownSecond.Location = new System.Drawing.Point(123, 3);
      this.numericUpDownSecond.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
      this.numericUpDownSecond.Name = "numericUpDownSecond";
      this.numericUpDownSecond.Size = new System.Drawing.Size(56, 29);
      this.numericUpDownSecond.TabIndex = 2;
      // 
      // numericUpDownMinute
      // 
      this.numericUpDownMinute.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.numericUpDownMinute.Location = new System.Drawing.Point(63, 3);
      this.numericUpDownMinute.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
      this.numericUpDownMinute.Name = "numericUpDownMinute";
      this.numericUpDownMinute.Size = new System.Drawing.Size(54, 29);
      this.numericUpDownMinute.TabIndex = 1;
      // 
      // numericUpDownHour
      // 
      this.numericUpDownHour.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.numericUpDownHour.Location = new System.Drawing.Point(3, 3);
      this.numericUpDownHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
      this.numericUpDownHour.Name = "numericUpDownHour";
      this.numericUpDownHour.Size = new System.Drawing.Size(54, 29);
      this.numericUpDownHour.TabIndex = 0;
      // 
      // UcTime
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "UcTime";
      this.Size = new System.Drawing.Size(182, 34);
      this.tableLayoutPanel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecond)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinute)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHour)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.NumericUpDown numericUpDownSecond;
    private System.Windows.Forms.NumericUpDown numericUpDownMinute;
    private System.Windows.Forms.NumericUpDown numericUpDownHour;
  }
}
