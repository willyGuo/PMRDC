﻿using IWshRuntimeLibrary;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.Linq;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PMRDC
{
    public partial class Form1 : Form
    {
        //設定LOG要存放的位置
        String logPath = "D:\\PMRDCFile\\log"; //Log目錄
        //判斷是否為開計時器的開始而已
        int timerCount = 0;
        //計時器開啟，預設先關閉
        bool timerStart = false;
        //紀錄上一次x座標
        int Pastx;
        //紀錄目前x座標
        int Nowx = int.Parse(string.Format("{0}", System.Windows.Forms.Cursor.Position.X));
        //紀錄次數
        int xrepeat = 0;
        //判斷6Sigma是否存在
        bool Sigma_exist;
        //取得目前登入的帳號
        string strUserName = WindowsIdentity.GetCurrent().Name;
        //此系統版本
        string Version = "v20230206";
        //紀錄6sigma開啟時間
        DateTime timeminstr;
        //紀錄6sigma關閉時間
        DateTime timeminend;
        //紀錄平台開啟時間
        DateTime timeminPlatformstr;
        //紀錄平台關閉時間
        DateTime timeminPlatformend;
        //不知道為啥關閉log會寫三筆，所以用這個判斷寫一次就好
        int closePlatformCount = 0;
        //是否為閒置關閉
        bool idleclose = false;
        //127.0.0.1/
        int aa =1 ;
        bool sigmaFirstOpen = true;
        public bool CreateDesktopShortcut( string FileName)
        {
            //建立桌面捷徑
            //取得目前開啟平台的路徑
            string str = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            try
            {
                //取得目前桌面路徑
                string deskTop = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\";
                if (System.IO.File.Exists(deskTop + FileName + ".lnk"))  
                {
                    System.IO.File.Delete(deskTop + FileName + ".lnk");//刪除原來的桌面快捷鍵方式
                }
                WshShell shell = new WshShell();  //建立一個新的Shell

                //快捷鍵方式建立的位置、名稱
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(deskTop + FileName + ".lnk");
                shortcut.TargetPath = str; //目標檔案
                                               //該屬性指定應用程式的工作目錄，當用戶沒有指定一個具體的目錄時，快捷方式的目標應用程式將使用該屬性所指定的目錄來裝載或儲存檔案。
                shortcut.WorkingDirectory = System.Environment.CurrentDirectory;
                shortcut.WindowStyle = 1; //目標應用程式的視窗狀態分為普通、最大化、最小化【1,3,7】
                shortcut.Description = FileName; //描述
                shortcut.IconLocation = str.Replace("exe", "ico");  //快捷方式圖示
                shortcut.Arguments = "";
                //shortcut.Hotkey = "CTRL+ALT+F11"; // 快捷鍵
                shortcut.Save(); //必須呼叫儲存快捷才成建立成功
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void variblelog()
        {
            Process[] localByName = Process.GetProcessesByName("PMRDC");
            using (StreamWriter writer = new StreamWriter(logPath + "\\" + "varible.txt"))
            {
                writer.Write("");

            }
            using (StreamWriter sw = System.IO.File.AppendText(logPath + "\\" + "varible.txt"))
            {
                foreach (Process process in localByName)
                {
                    sw.Write(process.Id.ToString() + ",");
                }

            }
        }
        public void delpast6sigma()
        {
            //紀錄變數的txt不存在就先建立，避免讀不到
            if (!System.IO.File.Exists(logPath + "\\" + "varible.txt"))
            {
                //建立檔案
                System.IO.File.Create(logPath + "\\" + "varible.txt").Close();
            }
            string text = System.IO.File.ReadAllText(logPath + "\\" + "varible.txt");
            List<string> list = new List<string>();
            list = text.Split(',').ToList();
            foreach (var id in list)
            {
                try
                {
                    Process p = Process.GetProcessById(Int32.Parse(id));
                    p.Kill();
                }
                catch { 
                }
            }
        }
        public void logwrite(string logmsg)
        {
            //紀錄LOG
            String nowTime =
                int.Parse(DateTime.Now.Year.ToString()).ToString("00")
                + "/"
                + int.Parse(DateTime.Now.Month.ToString()).ToString("00")
                + "/"
                + int.Parse(DateTime.Now.Day.ToString()).ToString("00")
                + " "
                + int.Parse(DateTime.Now.Hour.ToString()).ToString("00")
                + ":" 
                + int.Parse(DateTime.Now.Minute.ToString()).ToString("00")
                + ":" 
                + int.Parse(DateTime.Now.Second.ToString()).ToString("00");
            //開啟LOG的txt紀錄
            using (StreamWriter sw = System.IO.File.AppendText(logPath + "\\" + "PMRDCPlatform.txt"))
            {

                //WriteLine為換行 
                sw.Write(nowTime + " " + Version + " : ");
                sw.WriteLine(logmsg);

            }
        }
        
        public void Filecheck()
        {
            //判斷log檔案的資料夾和檔案是否存在
            String logFileName = DateTime.Now.Year.ToString() 
                + int.Parse(DateTime.Now.Month.ToString()).ToString("00") 
                + int.Parse(DateTime.Now.Day.ToString()).ToString("00") 
                + ".txt";
            //如果不存在就建立
            if (!Directory.Exists(logPath))
            {
                //建立資料夾
                Directory.CreateDirectory(logPath);
            }

            if (!System.IO.File.Exists(logPath + "\\" + "PMRDCPlatform.txt"))
            {
                //建立檔案
                System.IO.File.Create(logPath + "\\" + "PMRDCPlatform.txt").Close();
            }


        }

        public class Versionclass
        {
            //轉型網站API回傳的Json格式 to 字典
            public String SW { get; set; }

            public string Now_version { get; set; }

            public string user_version { get; set; }

            public bool check { get; set; }
            public string Downloaduri { get; set; }
        }
        public async Task<bool> LogapiAsync(string action, int deltatime = 0)
        {

            //將User使用狀況回傳到server紀錄
            //取得windows使用者名稱
            string[] aryUserInfo = strUserName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            //建立新連線後，將資料用post方式回傳
            HttpClient client = new HttpClient();
            string reponse = await client.GetStringAsync("http://127.0.0.1/log/6Sigma/" +Version +"/" + aryUserInfo[1]  + "/" + action + "/" + deltatime);
            //textBox1.Text = reponse;

            return true;
        }


        public async Task<bool> VersioncheckAsync()
        {
     
            //平台開始時，確認是否有新的版本要更新
            string[] aryUserInfo = strUserName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            //建立新的連線後，打API確認版本號
            HttpClient client = new HttpClient();
            string reponse = await client.GetStringAsync("http://127.0.0.1/swcheck/PMRDCPlatform/" + Version);
            Versionclass descJsonVer = JsonConvert.DeserializeObject<Versionclass>(reponse);//反序列化
            //如果版本號不同就強制下載，並關閉目前平台
            if (descJsonVer.user_version != descJsonVer.Now_version)
            {
                //string alert = "版本更新 ! 確認後自動下載，請執行最新版本";
                //DialogResult result =  MessageBox.Show(alert);
                //string myPath = @"D:\test";
                //System.Diagnostics.Process prc = new System.Diagnostics.Process();
                //prc.StartInfo.FileName = myPath;
                //prc.Start();
                //Process.Start("chrome.EXE", @"http://127.0.0.1/download/detail");
                string remoteUri = "http://127.0.0.1/media/PMRDCPlatform/";
                string fileName = descJsonVer.Downloaduri, myStringWebResource = null;
                //textBox2.Text = fileName;
                // Create a new WebClient instance.
                WebClient myWebClient = new WebClient();
                // Concatenate the domain with the Web resource filename.
                myStringWebResource = remoteUri + fileName;
                // Download the Web resource and save it into the current filesystem folder.
                myWebClient.DownloadFile(myStringWebResource, fileName);
                string zippath = Environment.GetFolderPath(Environment.SpecialFolder.Startup).Replace("Startup", "PMRDC/" + descJsonVer.Now_version);
                if (Directory.Exists(zippath))
                {
                    DirectoryInfo directory = new DirectoryInfo(zippath);
                    directory.Delete(true);
                }
                if (!Directory.Exists(zippath))
                {
                    //建立資料夾
                    Directory.CreateDirectory(zippath);
                }
           
                ZipFile.ExtractToDirectory(Application.StartupPath + "/" + descJsonVer.Downloaduri, zippath);

                Process PMRDCexe = new Process();
                // FileName 是要執行的檔案
                
                PMRDCexe.StartInfo.FileName = zippath + "/PMRDC.exe";
                PMRDCexe.Start();

                //this.Dispose();
            }
            return true;
        }

        public bool Simga_existFuc()
        {
            //判斷6Sigma是否存在，存在回傳true，不存在回傳false
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
            {
                for(int i = 0; i < MyProcess.Length; i++)
                {
                    MyProcess[i].Kill();
                }  
            } 
            return true;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //取消按鍵，先記錄關閉的log後(本地 & Server)，在關閉
            logwrite("ClosePlatform");
            timeminPlatformend = DateTime.Now;
            TimeSpan ts = timeminPlatformend.Subtract(timeminPlatformstr);
            int tsmin = ts.Minutes;
            LogapiAsync("ClosePlatform", tsmin);
            this.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = aa.ToString();
            ++aa;
            DateTime nowtime = DateTime.Now;
            if (nowtime.ToString("HH:mm") == "17:25" || nowtime.ToString("HH:mm") =="17:26"){
                Task<bool> task = VersioncheckAsync();
                return;
            }
            VersioncheckAsync();

            //計數器
            //如果第一次紀錄就先記錄第一次滑鼠x座標

            //lbltimer.Text = string.Format("時間：" + DateTime.Now.ToString("HH:mm:ss") + "，X：{0}，Y：{1}"
            //, System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
            Sigma_exist = Simga_existFuc();
            //如果Sigma有打開，就在執行
            if (Sigma_exist)
            {
                //如果Sigma是打開的，那就判斷是否曾經打開過
                //如果沒打開過，就紀錄第一次X，且判斷程已打開過
                if (sigmaFirstOpen == true)
                {
                    timeminstr = DateTime.Now;
                    Nowx = int.Parse(string.Format("{0}", System.Windows.Forms.Cursor.Position.X));
                    logwrite("Open6Sigma");
                    LogapiAsync("Open6Sigma");
                    ++timerCount;
                    sigmaFirstOpen = false;
                }
                //如果打開過，就來判斷X座標
                if (sigmaFirstOpen == false)
                {
                    Pastx = Nowx;
                    Nowx = int.Parse(string.Format("{0}", System.Windows.Forms.Cursor.Position.X));
                    if (Pastx == Nowx)
                    {
                        ++xrepeat;
                    }
                    else
                    {
                        xrepeat = 0;
                    }
                }
                if (xrepeat == 45)
                {
                    logwrite("idle45");
                    LogapiAsync("idle45");
                    string text45 = "電腦已閒置45分鐘，請立即存檔。系統若閒置60分鐘，將強制關閉6Sigma。";
                    MessageBox.Show(new Form { TopMost = true }, text45);
                    return;
                }
                //如果60分鐘都沒動，就紀錄後關閉平台
                if (xrepeat == 60)
                {
                    logwrite("idle60");
                    timeminend = DateTime.Now;
                    TimeSpan ts = timeminend.Subtract(timeminstr);
                    int tsmin = ts.Minutes;
                    LogapiAsync("idle60", tsmin);
                    ClosePress("6SigmaET");//關閉外部檔案
                    string text60 = "因電腦閒置60分鐘，故將6Sigma關閉。";
                    sigmaFirstOpen = true;
                    MessageBox.Show(new Form { TopMost = true }, text60);
                    return;
                }

            }
            //如果Sigma是關閉的，Sigma也曾經開啟
            if (!Sigma_exist && sigmaFirstOpen == false)
            {
                sigmaFirstOpen = true;
                logwrite("userclose6Sigma");
                timeminend = DateTime.Now;
                TimeSpan ts = timeminend.Subtract(timeminstr);
                int tsmin = ts.Minutes;
                int thour = ts.Hours * 60;
                LogapiAsync("userclose6Sigma", tsmin + thour);
                
            }
        }


      

        //    //如果沒打開就不執行
        //    if (!Sigma_exist)
        //    {
        //        //檢查是否曾經開啟過
        //    }


        //    if (timerCount == 0)
        //    {
        //        Nowx = int.Parse(string.Format("{0}", System.Windows.Forms.Cursor.Position.X));
        //        ++timerCount;
        //    }
        //    else  //第二次之後，將上一次紀錄的座標定義成上一次，再記錄一次新的x座標
        //    {
        //        Pastx = Nowx;
        //        Nowx = int.Parse(string.Format("{0}", System.Windows.Forms.Cursor.Position.X));
        //        //紀錄後判斷6Sigma是否存在
        //        Sigma_exist = Simga_existFuc();
        //        //如果存在就來判斷x座標是否相同，相同增加計次，不同就將計次歸0
        //        if (Sigma_exist)
        //        {
        //            if (Pastx == Nowx)
        //            {
        //                ++xrepeat;
        //            }
        //            else
        //            {
        //                xrepeat = 0;                     
        //            }
        //            timer1.Enabled = true;
        //        }

        //        //如果6Sigma不存在，則紀錄關閉6siggma時間後將平台關閉
        //        else
        //        {
        //            logwrite("userclose6Sigma");
        //            closePlatformCount++;
        //            timeminend = DateTime.Now;
        //            TimeSpan ts = timeminend.Subtract(timeminstr);
        //            int tsmin = ts.Minutes;
        //            int thour = ts.Hours * 60;
        //            LogapiAsync("userclose6Sigma", tsmin + thour);
        //        }

        //    }
        //    //如果45次(45分鐘)都沒再動，就跳出提示，並記錄
        //    if (xrepeat == 45)
        //    {
        //        logwrite("idle45");
        //        LogapiAsync("idle45");
        //        string text45 = "電腦已閒置45分鐘，請立即存檔。系統若閒置60分鐘，將強制關閉6Sigma。";
        //        MessageBox.Show(new Form { TopMost = true },text45);
        //    }
        //    //如果60分鐘都沒動，就紀錄後關閉平台
        //    if (xrepeat == 60 && idleclose==false)
        //    {
        //        logwrite("idle60");
        //        idleclose = true;
        //        timeminend = DateTime.Now;
        //        TimeSpan ts = timeminend.Subtract(timeminstr);
        //        int tsmin = ts.Minutes;
        //        LogapiAsync("idle60", tsmin);
        //        ClosePress("6SigmaET");//關閉外部檔案
        //        string text60 = "因電腦閒置60分鐘，故將6Sigma關閉。";
        //        MessageBox.Show(new Form { TopMost = true }, text60);
        //        //timer1.Enabled = false;
        //        //this.Dispose();
        //    }
            
        //}

        private void button1_Click(object sender, EventArgs e)
        {

            //先判斷程式有沒有正確開啟
            //讓程式在工具列中隱藏test
            Sigma_exist = Simga_existFuc();
            //如果Sigma已經被打開，但沒被計時，就直接寫log後計次
            if (Sigma_exist)
            {
                if (timerStart == false)
                {
                    logwrite("Open6Sigma");
                    Task<bool> task = LogapiAsync("Open6Sigma");
                    timeminstr = DateTime.Now;
                    this.ShowInTaskbar = false;
                    //隱藏程式本身的視窗
                    //this.Hide();
                    this.notifyIcon1.Visible = true;
                    timerStart = true;
                    timer1.Interval = 100;
                   // timer1.Tick += new EventHandler(timer1_Tick);
                    timer1.Enabled = true;
                }
            }
            //如果Sigma沒被打開，就打開6Sigma後寫LOG，並開始計次，並將程式縮到最小
            else
            {
                if (timerStart == false)
                {
                    try
                    {
                        
                        logwrite("Open6Sigma");
                        timeminstr = DateTime.Now;
                        LogapiAsync("Open6Sigma");
                        // test
                        //Process Sigma6 = new Process();
                        //// FileName 是要執行的檔案
                        //Sigma6.StartInfo.FileName = "C:\\Program Files\\6SigmaDCRelease15\\6SigmaET.exe";
                        //Sigma6.Start();
                        timerStart = true;
                        timer1.Interval = 100;
                        timer1.Enabled = true;
                        this.ShowInTaskbar = false;
    
                        //隱藏程式本身的視窗
                        //this.Hide();
                        //this.notifyIcon1.Visible = true;
                    }
                    //如果打不開程式，就pop windows顯示提示訊息
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
            
            //當平台開啟時，先預設一些要跑的動作
            //在桌面建立一個捷徑
            CreateDesktopShortcut("PMRDC.exe");
            //判斷紀錄LOG的資料夾和檔案是否存在
            Filecheck();
            //紀錄LOG
            logwrite("Open Platform");
            //判斷平台是否有重複開啟，有的話把先前的全部關掉，留一個並重新啟動
            delpast6sigma();
            variblelog();
            timeminPlatformstr = DateTime.Now;
            string[] aryUserInfo = strUserName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            label5.Text = "使用者 : " + aryUserInfo[1];
            label6.Text = Version;
            //判斷版本
            //Task<bool> task = VersioncheckAsync();
            //紀錄開啟平台LOG
            LogapiAsync("OpenPlatform");
            timerStart = true;
            timer1.Interval = 5000;
            timer1.Enabled = true;
            this.ShowInTaskbar = false;
            textBox1.Text = "start";
            this.notifyIcon1.Text = Version;

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) //當平台要被關閉前，紀錄被關閉
        {
            if (idleclose == true)
            {
            }
            else
            {
                logwrite("ClosePlatform");
                timeminPlatformend = DateTime.Now;
                TimeSpan ts = timeminPlatformend.Subtract(timeminPlatformstr);
                int tsmin = ts.Minutes;
                LogapiAsync("ClosePlatform", tsmin);
            }
            bool Sigma_exist_formC = Simga_existFuc();
            if (Sigma_exist_formC)
            {
                logwrite("userclose6Sigma");
                timeminend = DateTime.Now;
                TimeSpan ts = timeminend.Subtract(timeminstr);
                int tsmin = ts.Minutes;
                int thour = ts.Hours * 60;
                LogapiAsync("userclose6Sigma", tsmin + thour);
            }
            
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

            

        }

        private void ReloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string textrrload = "重載成功!";
            MessageBox.Show(new Form { TopMost = true }, textrrload);
            System.Windows.Forms.Application.Restart();
        }

        private void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string texthelp = "* 依軟體使用辦法，請勿長時間占用\r\n* " +
          "若有急需使用軟體，但無License情況請聯絡RDPM(Marcus Kuo #33930)\r\n* " +
          "程序問題可以先在右下角圖示右鍵Reload排除\r\n* " +
          "平台使用問題請聯絡Willy Guo(#32725)\r\n" +
          "版本號 : " + Version;

            MessageBox.Show(new Form { TopMost = true }, texthelp);
        }

        private void helpmeun_Paint(object sender, PaintEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            //variblelog();
            delpast6sigma();
        }
    }
}
