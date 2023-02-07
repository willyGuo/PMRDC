using IWshRuntimeLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PMRDC
{
    public partial class Form1 : Form
    {
        //設定LOG要存放的位置
        String logPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup).Replace("Startup", "PMRDC"); //Log目錄
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
        string Version = "v20230208";
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
        int aa;
        //172.18.212.76/
        //172.18.212.76
        bool sigmaFirstOpen = true;
        string catchexlog = "default";
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        [DllImport("user32.dll")]
        static extern bool CloseWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        private const UInt32 WM_CLOSE = 0x0010;
        public void idlecloseSW()
        {
            Process[] processlist = Process.GetProcesses();

            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    //textBox1.Text += process.ProcessName + "," + process.Id + "," + process.MainWindowTitle + "\r\n";
                    if (process.ProcessName.Contains("6SigmaET"))
                    {
                        bringToFront(process.MainWindowTitle);
                        process.Kill();
                    }
                }
            }
        }

        public  void bringToFront(string title)
        {
            
            // Get a handle to the Calculator application.
            IntPtr handle = FindWindow(null, title);
            // Verify that Calculator is a running process.
            //if (handle == IntPtr.Zero)
            //{
            //    return;
            //}
            BringWindowToTop(handle); // 將視窗浮在最上層
            ShowWindow(handle, 3); // 將視窗最大化
            Thread.Sleep(2000);
            //CloseWindow(handle,4);
            //存檔 且案Enter
            SendKeys.SendWait("^(s)");
            Thread.Sleep(2000);
            SendKeys.SendWait("{ENTER}");
            Thread.Sleep(2000);
            //關閉視窗
            //SendMessage(handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);


        }
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
        public void delpast6sigma2()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName("PMRDC");
            foreach (Process process in processes)
            {
                if(process.Id.ToString() != currentProcess.Id.ToString())
                {
                    process.Kill();
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
            string reponse = await client.GetStringAsync("http://172.18.212.76/log/6Sigma/" +Version +"/" + aryUserInfo[1]  + "/" + action + "/" + deltatime);
            //textBox1.Text = reponse;

            return true;
        }


        public async Task<bool> VersioncheckAsync()
        {
     
            //平台開始時，確認是否有新的版本要更新
            string[] aryUserInfo = strUserName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            //建立新的連線後，打API確認版本號
            HttpClient client = new HttpClient();
            string reponse = await client.GetStringAsync("http://172.18.212.76/swcheck/PMRDCPlatform/" + Version);
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
                //Process.Start("chrome.EXE", @"http://172.18.212.76/download/detail");
                string remoteUri = "http://172.18.212.76/media/PMRDCPlatform/";
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
            int tsmin = (int)ts.TotalMinutes;
            LogapiAsync("ClosePlatform", tsmin);
            this.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try {
                
                DateTime nowtime = DateTime.Now;
                if (nowtime.ToString("HH:mm") == "13:00" || nowtime.ToString("HH:mm") =="13:01"){
                    VersioncheckAsync();
                }

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
                        int tsmin = (int)ts.TotalMinutes;
                        LogapiAsync("idle60", tsmin);
                        idlecloseSW();
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
                    int tsmin = (int)ts.TotalMinutes;
                    LogapiAsync("userclose6Sigma", tsmin);
                
                }
            }
            catch(Exception ex) {
                if(catchexlog != ex.Message)
                {
                    logwrite(ex.Message);
                    LogapiAsync(ex.Message, 0);
                    catchexlog = ex.Message;
                }
                
            }

        }



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
            delpast6sigma2();
            CreateDesktopShortcut("PMRDC.exe");
            //判斷紀錄LOG的資料夾和檔案是否存在
            Filecheck();
            //紀錄LOG
            logwrite("Open Platform");
            VersioncheckAsync();
            //判斷平台是否有重複開啟，有的話把先前的全部關掉，留一個並重新啟動
            //delpast6sigma();
            //variblelog();
            //
            timeminPlatformstr = DateTime.Now;
            string[] aryUserInfo = strUserName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            label5.Text = "使用者 : " + aryUserInfo[1];
            label6.Text = Version;
            //判斷版本
            //Task<bool> task = VersioncheckAsync();
            //紀錄開啟平台LOG
            LogapiAsync("OpenPlatform");
            timerStart = true;
            timer1.Interval = 60000;
            timer1.Enabled = true;
            this.ShowInTaskbar = false;
            //textBox1.Text = "start";
            this.notifyIcon1.Text = Version;
            this.Hide();

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
                int tsmin = (int)ts.TotalMinutes;
                LogapiAsync("ClosePlatform", tsmin);
            }
            bool Sigma_exist_formC = Simga_existFuc();
            if (Sigma_exist_formC)
            {
                logwrite("userclose6Sigma");
                timeminend = DateTime.Now;
                TimeSpan ts = timeminend.Subtract(timeminstr);
                int tsmin = (int)ts.TotalMinutes;
                LogapiAsync("userclose6Sigma", tsmin);
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
            DialogResult dr = MessageBox.Show("是否手動執行更新", "版本更新", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                VersioncheckAsync();
            }
            else if (dr == DialogResult.Cancel)
            {
                
            }

            //VersioncheckAsync();

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
            
            

        }

        private void button4_Click_3(object sender, EventArgs e)
        {

        }
    }
}
