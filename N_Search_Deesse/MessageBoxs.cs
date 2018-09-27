using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace N_Search_Deesse
{
    public partial class MessageBoxs : Form
    {
        public MessageBoxs()
        {
            InitializeComponent();
        }

        static MessageBoxs mb; static DialogResult result = DialogResult.No;

        public static DialogResult Show(string text, string caption, string btnOK)
        {
            mb = new MessageBoxs();
            mb.content_text.Text = text;
            mb.ok_btn.Text = btnOK;
            mb.ShowDialog();
            

            return result;
        }

        private void keyword_add_btn_Click(object sender, EventArgs e)
        {
            result = DialogResult.Yes; mb.Close();
        }
    }
}
