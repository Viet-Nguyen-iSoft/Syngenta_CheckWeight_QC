namespace SyngentaWeigherQC.UI.UcUI
{
  partial class UcCheckbox
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
      this.check = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // check
      // 
      this.check.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.check.Dock = System.Windows.Forms.DockStyle.Fill;
      this.check.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.check.Location = new System.Drawing.Point(0, 0);
      this.check.Margin = new System.Windows.Forms.Padding(0);
      this.check.Name = "check";
      this.check.Size = new System.Drawing.Size(80, 50);
      this.check.TabIndex = 1;
      this.check.UseVisualStyleBackColor = true;
      // 
      // UcCheckbox
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.check);
      this.Name = "UcCheckbox";
      this.Size = new System.Drawing.Size(80, 50);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.CheckBox check;
  }
}
