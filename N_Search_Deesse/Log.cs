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

namespace N_Search_Deesse
{
    public partial class Log : Form
    {
        public Log()
        {
            InitializeComponent();
        }

        static Log mb; static DialogResult result = DialogResult.No;

        public static DialogResult Show(string text)
        {
            mb = new Log();
            mb.log_text.Text += text+"\n";
            mb.ShowDialog();
            return result;
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            result = DialogResult.Yes; mb.Close();
        }

        private void Blog_upper_exposure2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            Application.Exit();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        //폼 움직임
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

        private void Log_MouseDown(object sender, MouseEventArgs e)
        {
            GetCursorPos(out CurrentLocation);
            FormLocation = this.Location;
            IsMouseMoveStart = true;
        }

        private void Log_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsMouseMoveStart) return;

            GetCursorPos(out LastLocation);
            FormLocation.X -= (CurrentLocation.X - LastLocation.X);
            FormLocation.Y -= (CurrentLocation.Y - LastLocation.Y);
            this.Location = FormLocation;
            CurrentLocation = LastLocation;
        }

        private void Log_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseMoveStart = false;
        }
    }
}
