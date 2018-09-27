namespace N_Search_Deesse
{
    partial class N_Search_Deesse
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(N_Search_Deesse));
            this.N_Search_Deesse_top = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.closebtn = new System.Windows.Forms.Label();
            this.minibtn = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.deesse_id = new System.Windows.Forms.Label();
            this.deesse_pw = new System.Windows.Forms.Label();
            this.input_id = new System.Windows.Forms.TextBox();
            this.input_pw = new System.Windows.Forms.TextBox();
            this.login_error = new System.Windows.Forms.Label();
            this.loginBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.N_Search_Deesse_top.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // N_Search_Deesse_top
            // 
            this.N_Search_Deesse_top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(28)))), ((int)(((byte)(32)))));
            this.N_Search_Deesse_top.Controls.Add(this.panel4);
            this.N_Search_Deesse_top.Controls.Add(this.closebtn);
            this.N_Search_Deesse_top.Controls.Add(this.minibtn);
            this.N_Search_Deesse_top.Location = new System.Drawing.Point(-4, -1);
            this.N_Search_Deesse_top.Name = "N_Search_Deesse_top";
            this.N_Search_Deesse_top.Size = new System.Drawing.Size(446, 40);
            this.N_Search_Deesse_top.TabIndex = 3;
            this.N_Search_Deesse_top.MouseDown += new System.Windows.Forms.MouseEventHandler(this.N_Search_Deesse_MouseDown);
            this.N_Search_Deesse_top.MouseMove += new System.Windows.Forms.MouseEventHandler(this.N_Search_Deesse_MouseMove);
            this.N_Search_Deesse_top.MouseUp += new System.Windows.Forms.MouseEventHandler(this.N_Search_Deesse_MouseUp);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panel4.Location = new System.Drawing.Point(3, -21);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(440, 23);
            this.panel4.TabIndex = 8;
            // 
            // closebtn
            // 
            this.closebtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(28)))), ((int)(((byte)(32)))));
            this.closebtn.Font = new System.Drawing.Font("Consolas", 12F);
            this.closebtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.closebtn.Location = new System.Drawing.Point(399, 0);
            this.closebtn.Margin = new System.Windows.Forms.Padding(0);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(40, 40);
            this.closebtn.TabIndex = 5;
            this.closebtn.Text = "x";
            this.closebtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            this.closebtn.MouseLeave += new System.EventHandler(this.closebtn_MouseLeave);
            this.closebtn.MouseHover += new System.EventHandler(this.close_MouseHover);
            // 
            // minibtn
            // 
            this.minibtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(28)))), ((int)(((byte)(32)))));
            this.minibtn.Font = new System.Drawing.Font("Consolas", 12F);
            this.minibtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.minibtn.Location = new System.Drawing.Point(357, 0);
            this.minibtn.Margin = new System.Windows.Forms.Padding(0);
            this.minibtn.Name = "minibtn";
            this.minibtn.Size = new System.Drawing.Size(40, 40);
            this.minibtn.TabIndex = 4;
            this.minibtn.Text = "_";
            this.minibtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.minibtn.Click += new System.EventHandler(this.minibtn_Click);
            this.minibtn.MouseLeave += new System.EventHandler(this.minibtn_MouseLeave);
            this.minibtn.MouseHover += new System.EventHandler(this.minibtn_MouseHover);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label1.Location = new System.Drawing.Point(341, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Ver.1.0.0";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(1, 45);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(413, 173);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // deesse_id
            // 
            this.deesse_id.AutoSize = true;
            this.deesse_id.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deesse_id.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.deesse_id.Location = new System.Drawing.Point(29, 235);
            this.deesse_id.Name = "deesse_id";
            this.deesse_id.Size = new System.Drawing.Size(55, 22);
            this.deesse_id.TabIndex = 9;
            this.deesse_id.Text = "아이디";
            // 
            // deesse_pw
            // 
            this.deesse_pw.AutoSize = true;
            this.deesse_pw.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deesse_pw.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.deesse_pw.Location = new System.Drawing.Point(28, 270);
            this.deesse_pw.Name = "deesse_pw";
            this.deesse_pw.Size = new System.Drawing.Size(70, 22);
            this.deesse_pw.TabIndex = 10;
            this.deesse_pw.Text = "비밀번호";
            // 
            // input_id
            // 
            this.input_id.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(37)))), ((int)(((byte)(45)))));
            this.input_id.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.input_id.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.input_id.Location = new System.Drawing.Point(99, 238);
            this.input_id.Name = "input_id";
            this.input_id.Size = new System.Drawing.Size(214, 19);
            this.input_id.TabIndex = 11;
            this.input_id.TextChanged += new System.EventHandler(this.input_id_TextChanged);
            this.input_id.KeyUp += new System.Windows.Forms.KeyEventHandler(this.input_id_KeyUp);
            // 
            // input_pw
            // 
            this.input_pw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(37)))), ((int)(((byte)(45)))));
            this.input_pw.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.input_pw.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.input_pw.Location = new System.Drawing.Point(99, 270);
            this.input_pw.Name = "input_pw";
            this.input_pw.PasswordChar = '♥';
            this.input_pw.Size = new System.Drawing.Size(214, 19);
            this.input_pw.TabIndex = 12;
            this.input_pw.KeyUp += new System.Windows.Forms.KeyEventHandler(this.input_pw_KeyUp);
            // 
            // login_error
            // 
            this.login_error.AutoSize = true;
            this.login_error.ForeColor = System.Drawing.Color.Red;
            this.login_error.Location = new System.Drawing.Point(100, 299);
            this.login_error.Name = "login_error";
            this.login_error.Size = new System.Drawing.Size(0, 19);
            this.login_error.TabIndex = 14;
            // 
            // loginBtn
            // 
            this.loginBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.loginBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.loginBtn.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginBtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.loginBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.loginBtn.Location = new System.Drawing.Point(321, 234);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(92, 58);
            this.loginBtn.TabIndex = 15;
            this.loginBtn.Text = "로그인";
            this.loginBtn.UseVisualStyleBackColor = false;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panel1.Location = new System.Drawing.Point(436, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(35, 328);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panel2.Location = new System.Drawing.Point(-26, -7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(27, 334);
            this.panel2.TabIndex = 7;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panel3.Location = new System.Drawing.Point(-4, 325);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(446, 23);
            this.panel3.TabIndex = 7;
            // 
            // N_Search_Deesse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(28)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(437, 326);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.loginBtn);
            this.Controls.Add(this.login_error);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.input_pw);
            this.Controls.Add(this.input_id);
            this.Controls.Add(this.deesse_pw);
            this.Controls.Add(this.deesse_id);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.N_Search_Deesse_top);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "N_Search_Deesse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "N Search Deesse";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.N_Search_Deesse_FormClosing);
            this.N_Search_Deesse_top.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel N_Search_Deesse_top;
        private System.Windows.Forms.Label closebtn;
        private System.Windows.Forms.Label minibtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label deesse_id;
        private System.Windows.Forms.Label deesse_pw;
        private System.Windows.Forms.TextBox input_id;
        private System.Windows.Forms.TextBox input_pw;
        private System.Windows.Forms.Label login_error;
        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
    }
}

