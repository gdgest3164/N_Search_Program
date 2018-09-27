using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace N_Search_Deesse
{
    public partial class N_Search_Deesse : Form
    {
        public Color color_one= Color.FromArgb(27, 28, 32);
        public Color color_two= Color.FromArgb(35, 40, 50);

        database sDbs = new database();
        int login_result;

        public N_Search_Deesse()
        {
            InitializeComponent();
        }

        public struct POINT
        {
            public int X, Y;
        }
        [DllImport("user32.dll")] // 현재 마우스 위치를 얻기위한 API함수.
        public extern static void GetCursorPos(out POINT point);

        Point FormLocation; // 현재 폼 위치
        POINT LastLocation = new POINT(); // 방금 전의 마우스 위치
        POINT CurrentLocation = new POINT(); // 현재 마우스 위치
                                             // 폼이 움직일 양 = CurrentLocation - LastLocation.
        bool IsMouseMoveStart = false; // 현재 마우스 움직이기 기능이 켜져있는가.
                                       // 만약 이게 없으면 그냥 폼위에서 마우스가 움직이면 폼이 움직이게 될거임.

        private void N_Search_Deesse_MouseDown(object sender, MouseEventArgs e)
        {
            GetCursorPos(out CurrentLocation);
            FormLocation = this.Location;
            IsMouseMoveStart = true;
        }

        private void N_Search_Deesse_MouseMove(object sender, MouseEventArgs e)
        {
            // 마우스의 움직임에 맞춰서 폼을 움직인다.

            if (!IsMouseMoveStart) return;

            GetCursorPos(out LastLocation);
            FormLocation.X -= (CurrentLocation.X - LastLocation.X);
            FormLocation.Y -= (CurrentLocation.Y - LastLocation.Y);
            this.Location = FormLocation;
            CurrentLocation = LastLocation;
        }

        private void N_Search_Deesse_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseMoveStart = false;
        }

        private void minibtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            Application.Exit();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            //Environment.Exit(0);
        }

        private void minibtn_MouseHover(object sender, EventArgs e)
        {
            //minibtn.ForeColor = Color.FromArgb(87, 91, 193);
            minibtn.BackColor = color_two;


        }

        private void close_MouseHover(object sender, EventArgs e)
        {
            closebtn.BackColor = color_two;
        }

        private void minibtn_MouseLeave(object sender, EventArgs e)
        {
            minibtn.BackColor = color_one;
        }

        private void closebtn_MouseLeave(object sender, EventArgs e)
        {
            closebtn.BackColor = color_one;

        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            if (input_id.Text == "" || input_pw.Text == "")
            {
                login_error.Text = "아이디와 비밀번호가 입력해주세요.";
            }
            else
            {
                string sql = "Select * from blog_member where id='" + input_id.Text + "' and pw=password('" + input_pw.Text + "')";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, sDbs.Kon);
                DataSet Ds = new DataSet();
                Ds.Reset();
                da.Fill(Ds, sql);

                login_result = Convert.ToInt32(Ds.Tables[0].Rows.Count.ToString());

                if (login_result == 0)
                {
                    login_error.Text = "아이디와 비밀번호가 틀렸습니다.";
                }
                else
                {
                    sDbs.Kon.Close();
                    this.Hide();
                    N_Search_Deesse_Program fm = new N_Search_Deesse_Program();
                    fm.Show();
                }
            }
        }

        private void input_pw_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginBtn.PerformClick();
            }
        }

        private void input_id_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginBtn.PerformClick();
            }
        }

        private void input_id_TextChanged(object sender, EventArgs e)
        {

        }

        private void N_Search_Deesse_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            Application.Exit();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
