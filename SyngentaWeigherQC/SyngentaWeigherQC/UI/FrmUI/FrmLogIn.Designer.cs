namespace SyngentaWeigherQC.UI.FrmUI
{
  partial class FrmLogIn
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
      this.cbAccount = new System.Windows.Forms.ComboBox();
      this.txtPassword = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.btnCancel = new CustomControls.RJControls.RJButton();
      this.btnLogin = new CustomControls.RJControls.RJButton();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(3, 70);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(141, 31);
      this.label1.TabIndex = 0;
      this.label1.Text = "Tài khoản:";
      // 
      // cbAccount
      // 
      this.cbAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cbAccount.FormattingEnabled = true;
      this.cbAccount.Location = new System.Drawing.Point(176, 69);
      this.cbAccount.Name = "cbAccount";
      this.cbAccount.Size = new System.Drawing.Size(262, 39);
      this.cbAccount.TabIndex = 2;
      // 
      // txtPassword
      // 
      this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtPassword.Location = new System.Drawing.Point(176, 126);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.Size = new System.Drawing.Size(262, 38);
      this.txtPassword.TabIndex = 3;
      this.txtPassword.UseSystemPasswordChar = true;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(159, 9);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(163, 31);
      this.label2.TabIndex = 5;
      this.label2.Text = "Đăng nhập ";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(3, 126);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(133, 31);
      this.label3.TabIndex = 6;
      this.label3.Text = "Mật khẩu:";
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
      this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnCancel.ForeColor = System.Drawing.Color.White;
      this.btnCancel.Location = new System.Drawing.Point(119, 200);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(150, 40);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.TextColor = System.Drawing.Color.White;
      this.btnCancel.UseVisualStyleBackColor = false;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnLogin
      // 
      this.btnLogin.BackColor = System.Drawing.Color.Green;
      this.btnLogin.BackgroundColor = System.Drawing.Color.Green;
      this.btnLogin.BorderColor = System.Drawing.Color.PaleVioletRed;
      this.btnLogin.BorderRadius = 8;
      this.btnLogin.BorderSize = 0;
      this.btnLogin.FlatAppearance.BorderSize = 0;
      this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnLogin.ForeColor = System.Drawing.Color.White;
      this.btnLogin.Location = new System.Drawing.Point(288, 200);
      this.btnLogin.Name = "btnLogin";
      this.btnLogin.Size = new System.Drawing.Size(150, 40);
      this.btnLogin.TabIndex = 1;
      this.btnLogin.Text = "Login";
      this.btnLogin.TextColor = System.Drawing.Color.White;
      this.btnLogin.UseVisualStyleBackColor = false;
      this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
      // 
      // FrmLogIn
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(456, 252);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.txtPassword);
      this.Controls.Add(this.cbAccount);
      this.Controls.Add(this.btnLogin);
      this.Controls.Add(this.label1);
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FrmLogIn";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Load += new System.EventHandler(this.FrmLogIn_Load);
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmLogIn_KeyUp);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private CustomControls.RJControls.RJButton btnLogin;
    private System.Windows.Forms.ComboBox cbAccount;
    private System.Windows.Forms.TextBox txtPassword;
    private CustomControls.RJControls.RJButton btnCancel;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
  }
}