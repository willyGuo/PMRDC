using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Principal;
using System.Reflection.Emit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Http;
using Newtonsoft.Json;

namespace PMRDC
{
    public partial class Form1 : Form
    {
        int timerCount = 0;
        bool timerStart = false;
        int Pastx;
        int Nowx = int.Parse(string.Format("{0}", System.Windows.Forms.Cursor.Position.X));
        int xrepeat = 0;
        bool Sigma_exist;
        string strUserName = WindowsIdentity.GetCurrent().Name;
        string Version = "v20230119";
        public class Versionclass
        {
            public String SW { get; set; }

            public string Now_version { get; set; }

            public string user_version { get; set; }

            public bool check { get; set; }
        }
        public async Task<bool> LogapiAsync(string action, int deltatime = 0)
        {
            string[] aryUserInfo = strUserName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            label5.Text = "使用者 : " + aryUserInfo[1];
            HttpClient client = new HttpClient();
            string reponse = await client.GetStringAsync("http://127.0.0.1/log/6Sigma/" +Version +"/" + aryUserInfo[1]  + "/" + action + "/" + deltatime);
            textBox1.Text = reponse;
            return true;
        }

        public async Task<bool> VersioncheckAsync()
        {
            string[] aryUserInfo = strUserName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            label5.Text = "使用者 : " + aryUserInfo[1];
            HttpClient client = new HttpClient();
            string reponse = await client.GetStringAsync("http://127.0.0.1/swcheck/PMRDCscanPlatform/" + Version);
            Versionclass descJsonVer = JsonConvert.DeserializeObject<Versionclass>(reponse);//反序列化
            textBox1.Text = descJsonVer.user_version;
            if(descJsonVer.user_version != descJsonVer.Now_version)
            {
                string alert = "請更新版本";
                DialogResult result =  MessageBox.Show(alert);
                if(result == DialogResult.OK)
                {
                    string myPath = @"D:\teraterm-4.106";
                    System.Diagnostics.Process prc = new System.Diagnostics.Process();
                    prc.StartInfo.FileName = myPath;
                    prc.Start();
                    this.Dispose();
                }
                

            }
            return true;
        }
        public bool Simga_existFuc()
        {
            Process[] processes = Process.GetProcessesByName("6SigmaET");
            if (processes.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool ClosePress(string FileName)//關閉外部檔案
        {
            Process[] MyProcess = Process.GetProcessesByName(FileName);
            if (MyProcess.Length > 0)
                MyProcess[0].Kill(); //關閉執行中的程式
            return true;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //顯示滑鼠移動座標
            lbltimer.Text = string.Format("時間：" + DateTime.Now.ToString("HH:mm:ss") + "，X：{0}，Y：{1}"
            , System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
            if (timerCount == 0)
            {
                Nowx = int.Parse(string.Format("{0}", System.Windows.Forms.Cursor.Position.X));
                ++timerCount;
            }
            else
            {
                Pastx = Nowx;
                Nowx = int.Parse(string.Format("{0}", System.Windows.Forms.Cursor.Position.X));
                Sigma_exist = Simga_existFuc();
                if (Sigma_exist)
                {
                    if (Pastx == Nowx)
                    {
                        ++xrepeat;
                    }
                    else
                    {
                        xrepeat = 0;
                    }
                }
                else
                {
                    this.Dispose();
                }

            }
            if (xrepeat == 8)
            {
                string text = "電腦已閒置45分鐘，請立即存檔。系統若閒置60分鐘，將強制關閉6Sigma。";
                MessageBox.Show(text);
            }
            if (xrepeat == 11)
            {
                string text = "關閉程式";
                ClosePress("6SigmaET");//關閉外部檔案
                //this.Dispose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //先判斷程式有沒有正確開啟
            //讓程式在工具列中隱藏

            Sigma_exist = Simga_existFuc();
            if (Sigma_exist)
            {
                if (timerStart == false)
                {
                    this.ShowInTaskbar = false;
                    //隱藏程式本身的視窗
                    this.Hide();
                    this.notifyIcon1.Visible = true;
                    timerStart = true;
                    timer1.Interval = 200;
                    timer1.Tick += new EventHandler(timer1_Tick);
                    timer1.Enabled = true;
                }
            }
            else
            {
                if (timerStart == false)
                {
                    try
                    {
                        Task<bool> task = LogapiAsync("Open");
                        // test
                        Process Sigma6 = new Process();
                        // FileName 是要執行的檔案
                        Sigma6.StartInfo.FileName = "C:\\Program Files\\6SigmaDCRelease15\\6SigmaET.exe";
                        Sigma6.Start();
                        timerStart = true;
                        timer1.Interval = 1000;
                        timer1.Tick += new EventHandler(timer1_Tick);
                        timer1.Enabled = true;
                        this.ShowInTaskbar = false;
                        //隱藏程式本身的視窗
                        //this.Hide();
                        this.notifyIcon1.Visible = true;
                    }
                    catch 
                    {
                        string text = "電腦尚未安裝6Sigma，或路徑錯誤，請重新安裝。若有其他問題請聯絡Willy_Guo(#32725)";
                        MessageBox.Show(text);
                    }

                }

            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            string[] aryUserInfo = strUserName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            label5.Text = "使用者 : " + aryUserInfo[1];
            Task<bool> task = VersioncheckAsync();

        }
    }
}
