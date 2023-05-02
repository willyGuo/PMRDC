using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PMRDC
{
    public partial class Form2 : Form
    {
        private Form1 form1;
        public Form2(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
            pwdtextBox.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pwd = pwdtextBox.Text;
            if (pwd == "wewe1234")
            {
                this.Visible = false;//將Form1隱藏。由於在Form1的程式碼內使用this，所以this為Form1的物件本身
                form1.Show();
                form1.TopMost = true;
                form1.ShowInTaskbar = true;
                
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void pwdtextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // 禁止顯示Enter鍵
                button1.PerformClick(); // 觸發Button1的Click事件
            }
        }
    }
}
