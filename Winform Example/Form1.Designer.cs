namespace Winform_Example
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label2 = new System.Windows.Forms.Label();
            this.siticoneControlBox2 = new Siticone.UI.WinForms.SiticoneControlBox();
            this.siticoneControlBox1 = new Siticone.UI.WinForms.SiticoneControlBox();
            this.siticoneGroupBox1 = new Siticone.UI.WinForms.SiticoneGroupBox();
            this.info = new System.Windows.Forms.Label();
            this.secret = new Siticone.UI.WinForms.SiticoneTextBox();
            this.siticoneGroupBox2 = new Siticone.UI.WinForms.SiticoneGroupBox();
            this.expiry = new System.Windows.Forms.Label();
            this.ip = new System.Windows.Forms.Label();
            this.level = new System.Windows.Forms.Label();
            this.hwid = new System.Windows.Forms.Label();
            this.email = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.Label();
            this.siticoneDragControl1 = new Siticone.UI.WinForms.SiticoneDragControl(this.components);
            this.siticoneGroupBox1.SuspendLayout();
            this.siticoneGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(11, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 19);
            this.label2.TabIndex = 29;
            this.label2.Text = "AuthGuard.NET | Main";
            // 
            // siticoneControlBox2
            // 
            this.siticoneControlBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.siticoneControlBox2.ControlBoxType = Siticone.UI.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.siticoneControlBox2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
            this.siticoneControlBox2.HoveredState.Parent = this.siticoneControlBox2;
            this.siticoneControlBox2.IconColor = System.Drawing.Color.White;
            this.siticoneControlBox2.Location = new System.Drawing.Point(277, 1);
            this.siticoneControlBox2.Name = "siticoneControlBox2";
            this.siticoneControlBox2.ShadowDecoration.Parent = this.siticoneControlBox2;
            this.siticoneControlBox2.Size = new System.Drawing.Size(45, 29);
            this.siticoneControlBox2.TabIndex = 36;
            // 
            // siticoneControlBox1
            // 
            this.siticoneControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.siticoneControlBox1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
            this.siticoneControlBox1.HoveredState.Parent = this.siticoneControlBox1;
            this.siticoneControlBox1.IconColor = System.Drawing.Color.White;
            this.siticoneControlBox1.Location = new System.Drawing.Point(328, 1);
            this.siticoneControlBox1.Name = "siticoneControlBox1";
            this.siticoneControlBox1.ShadowDecoration.Parent = this.siticoneControlBox1;
            this.siticoneControlBox1.Size = new System.Drawing.Size(45, 29);
            this.siticoneControlBox1.TabIndex = 35;
            this.siticoneControlBox1.Click += new System.EventHandler(this.siticoneControlBox1_Click);
            // 
            // siticoneGroupBox1
            // 
            this.siticoneGroupBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.siticoneGroupBox1.Controls.Add(this.info);
            this.siticoneGroupBox1.Controls.Add(this.secret);
            this.siticoneGroupBox1.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.siticoneGroupBox1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
            this.siticoneGroupBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.siticoneGroupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.siticoneGroupBox1.Location = new System.Drawing.Point(13, 36);
            this.siticoneGroupBox1.Name = "siticoneGroupBox1";
            this.siticoneGroupBox1.ShadowDecoration.Parent = this.siticoneGroupBox1;
            this.siticoneGroupBox1.Size = new System.Drawing.Size(351, 104);
            this.siticoneGroupBox1.TabIndex = 37;
            this.siticoneGroupBox1.Text = "Server Variables";
            // 
            // info
            // 
            this.info.AutoSize = true;
            this.info.Font = new System.Drawing.Font("Segoe UI Semibold", 7.2F, System.Drawing.FontStyle.Bold);
            this.info.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.info.Location = new System.Drawing.Point(77, 50);
            this.info.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(191, 24);
            this.info.TabIndex = 30;
            this.info.Text = "These are only released after a successful\r\nlogin and are encrypted in traffic.";
            this.info.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // secret
            // 
            this.secret.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(132)))), ((int)(((byte)(231)))));
            this.secret.BorderThickness = 0;
            this.secret.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.secret.DefaultText = ".....";
            this.secret.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.secret.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.secret.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.secret.DisabledState.Parent = this.secret;
            this.secret.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.secret.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
            this.secret.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.secret.FocusedState.Parent = this.secret;
            this.secret.HoveredState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.secret.HoveredState.Parent = this.secret;
            this.secret.Location = new System.Drawing.Point(52, 70);
            this.secret.Name = "secret";
            this.secret.PasswordChar = '\0';
            this.secret.PlaceholderText = "";
            this.secret.SelectedText = "";
            this.secret.ShadowDecoration.Parent = this.secret;
            this.secret.Size = new System.Drawing.Size(246, 30);
            this.secret.TabIndex = 31;
            this.secret.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // siticoneGroupBox2
            // 
            this.siticoneGroupBox2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.siticoneGroupBox2.Controls.Add(this.expiry);
            this.siticoneGroupBox2.Controls.Add(this.ip);
            this.siticoneGroupBox2.Controls.Add(this.level);
            this.siticoneGroupBox2.Controls.Add(this.hwid);
            this.siticoneGroupBox2.Controls.Add(this.email);
            this.siticoneGroupBox2.Controls.Add(this.username);
            this.siticoneGroupBox2.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.siticoneGroupBox2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
            this.siticoneGroupBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.siticoneGroupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.siticoneGroupBox2.Location = new System.Drawing.Point(15, 146);
            this.siticoneGroupBox2.Name = "siticoneGroupBox2";
            this.siticoneGroupBox2.ShadowDecoration.Parent = this.siticoneGroupBox2;
            this.siticoneGroupBox2.Size = new System.Drawing.Size(351, 196);
            this.siticoneGroupBox2.TabIndex = 38;
            this.siticoneGroupBox2.Text = "User Info";
            // 
            // expiry
            // 
            this.expiry.AutoSize = true;
            this.expiry.Font = new System.Drawing.Font("Segoe UI Semibold", 7.2F, System.Drawing.FontStyle.Bold);
            this.expiry.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.expiry.Location = new System.Drawing.Point(16, 164);
            this.expiry.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.expiry.Name = "expiry";
            this.expiry.Size = new System.Drawing.Size(11, 12);
            this.expiry.TabIndex = 40;
            this.expiry.Text = "...";
            // 
            // ip
            // 
            this.ip.AutoSize = true;
            this.ip.Font = new System.Drawing.Font("Segoe UI Semibold", 7.2F, System.Drawing.FontStyle.Bold);
            this.ip.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ip.Location = new System.Drawing.Point(16, 143);
            this.ip.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(11, 12);
            this.ip.TabIndex = 39;
            this.ip.Text = "...";
            // 
            // level
            // 
            this.level.AutoSize = true;
            this.level.Font = new System.Drawing.Font("Segoe UI Semibold", 7.2F, System.Drawing.FontStyle.Bold);
            this.level.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.level.Location = new System.Drawing.Point(16, 122);
            this.level.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.level.Name = "level";
            this.level.Size = new System.Drawing.Size(11, 12);
            this.level.TabIndex = 38;
            this.level.Text = "...";
            // 
            // hwid
            // 
            this.hwid.AutoSize = true;
            this.hwid.Font = new System.Drawing.Font("Segoe UI Semibold", 7.2F, System.Drawing.FontStyle.Bold);
            this.hwid.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.hwid.Location = new System.Drawing.Point(16, 101);
            this.hwid.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.hwid.Name = "hwid";
            this.hwid.Size = new System.Drawing.Size(11, 12);
            this.hwid.TabIndex = 37;
            this.hwid.Text = "...";
            // 
            // email
            // 
            this.email.AutoSize = true;
            this.email.Font = new System.Drawing.Font("Segoe UI Semibold", 7.2F, System.Drawing.FontStyle.Bold);
            this.email.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.email.Location = new System.Drawing.Point(16, 80);
            this.email.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(11, 12);
            this.email.TabIndex = 36;
            this.email.Text = "...";
            // 
            // username
            // 
            this.username.AutoSize = true;
            this.username.Font = new System.Drawing.Font("Segoe UI Semibold", 7.2F, System.Drawing.FontStyle.Bold);
            this.username.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.username.Location = new System.Drawing.Point(16, 59);
            this.username.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(11, 12);
            this.username.TabIndex = 35;
            this.username.Text = "...";
            // 
            // siticoneDragControl1
            // 
            this.siticoneDragControl1.TargetControl = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(375, 354);
            this.Controls.Add(this.siticoneGroupBox2);
            this.Controls.Add(this.siticoneGroupBox1);
            this.Controls.Add(this.siticoneControlBox2);
            this.Controls.Add(this.siticoneControlBox1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.siticoneGroupBox1.ResumeLayout(false);
            this.siticoneGroupBox1.PerformLayout();
            this.siticoneGroupBox2.ResumeLayout(false);
            this.siticoneGroupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private Siticone.UI.WinForms.SiticoneControlBox siticoneControlBox2;
        private Siticone.UI.WinForms.SiticoneControlBox siticoneControlBox1;
        private Siticone.UI.WinForms.SiticoneGroupBox siticoneGroupBox1;
        private Siticone.UI.WinForms.SiticoneGroupBox siticoneGroupBox2;
        private System.Windows.Forms.Label expiry;
        private System.Windows.Forms.Label ip;
        private System.Windows.Forms.Label level;
        private System.Windows.Forms.Label hwid;
        private System.Windows.Forms.Label email;
        private System.Windows.Forms.Label username;
        private System.Windows.Forms.Label info;
        private Siticone.UI.WinForms.SiticoneTextBox secret;
        private Siticone.UI.WinForms.SiticoneDragControl siticoneDragControl1;
    }
}

