using IWshRuntimeLibrary;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;


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
        string Version = "v20230118";
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
        int aa = 1;
        //127.0.0.1/

        public bool CreateDesktopShortcut( string FileName)
        {
            //建立桌面捷徑
            //取得目前開啟平台的路徑
            string str = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            try
            {
                //取得目前桌面路徑
                string deskTop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
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
            if(descJsonVer.user_version != descJsonVer.Now_version)
            {
                string alert = "版本更新 ! 確認後自動下載，請執行最新版本";
                DialogResult result =  MessageBox.Show(alert);
                if(result == DialogResult.OK)
                {
                    //string myPath = @"D:\test";
                    //System.Diagnostics.Process prc = new System.Diagnostics.Process();
                    //prc.StartInfo.FileName = myPath;
                    //prc.Start();
                    Process.Start("chrome.EXE", @"http://127.0.0.1/download/detail");
                    this.Dispose();
                }

                

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
                MyProcess[0].Kill(); //關閉執行中的程式
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
            timer1.Enabled = false;
            //計數器
            //如果第一次紀錄就先記錄第一次滑鼠x座標

            //lbltimer.Text = string.Format("時間：" + DateTime.Now.ToString("HH:mm:ss") + "，X：{0}，Y：{1}"
            //, System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
            if (timerCount == 0)
            {
                Nowx = int.Parse(string.Format("{0}", System.Windows.Forms.Cursor.Position.X));
                ++timerCount;
            }
            else  //第二次之後，將上一次紀錄的座標定義成上一次，再記錄一次新的x座標
            {
                Pastx = Nowx;
                Nowx = int.Parse(string.Format("{0}", System.Windows.Forms.Cursor.Position.X));
                //紀錄後判斷6Sigma是否存在
                Sigma_exist = Simga_existFuc();
                //如果存在就來判斷x座標是否相同，相同增加計次，不同就將計次歸0
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
                    timer1.Enabled = true;
                }

                //如果6Sigma不存在，則紀錄關閉6siggma時間後將平台關閉
                else
                {
                    if(true)
                    {
                        logwrite("userclose6Sigma");
                        closePlatformCount++;
                        timeminend = DateTime.Now;
                        TimeSpan ts = timeminend.Subtract(timeminstr);
                        int tsmin = ts.Minutes;
                        LogapiAsync("userclose6Sigma", tsmin);
                        timer1.Enabled = false;
                        textBox1.Text = aa.ToString();
                        ++aa;
                        //this.Dispose();
                    }

                }

            }
            //如果45次(45分鐘)都沒再動，就跳出提示，並記錄
            if (xrepeat == 45)
            {
                logwrite("idle45");
                LogapiAsync("idle45");
                string text = "電腦已閒置45分鐘，請立即存檔。系統若閒置60分鐘，將強制關閉6Sigma。";
                MessageBox.Show(new Form { TopMost = true },text);
            }
            //如果60分鐘都沒動，就紀錄後關閉平台
            if (xrepeat == 60)
            {
                logwrite("idle60");
                idleclose = true;
                timeminend = DateTime.Now;
                TimeSpan ts = timeminend.Subtract(timeminstr);
                int tsmin = ts.Minutes;
                LogapiAsync("idle60", tsmin);
                ClosePress("6SigmaET");//關閉外部檔案
                this.Dispose();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //先判斷程式有沒有正確開啟
            //讓程式在工具列中隱藏

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
                    timer1.Interval = 1000;
                    timer1.Tick += new EventHandler(timer1_Tick);
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
                        Task<bool> task = LogapiAsync("Open6Sigma");
                        // test
                        Process Sigma6 = new Process();
                        // FileName 是要執行的檔案
                        Sigma6.StartInfo.FileName = "C:\\Program Files\\6SigmaDCRelease15\\6SigmaET.exe";
                        Sigma6.Start();
                        timerStart = true;
                        timer1.Interval = 1000;
                        timer1.Tick +=  timer1_Tick;
                        timer1.Enabled = true;
                        this.ShowInTaskbar = false;
                        //隱藏程式本身的視窗
                        //this.Hide();
                        this.notifyIcon1.Visible = true;
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
            Process[] processesPMRDC = Process.GetProcessesByName("PMRDC");
            int PMRDCLength = processesPMRDC.Length;
            if(PMRDCLength > 1)
            {
                for(int i = 0; i<1; i++)
                {
                    processesPMRDC[i].Kill();
                }

                System.Windows.Forms.Application.Restart();

            }
            timeminPlatformstr = DateTime.Now;
            string[] aryUserInfo = strUserName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            label5.Text = "使用者 : " + aryUserInfo[1];
            //判斷版本
            Task<bool> task = VersioncheckAsync();
            //紀錄開啟平台LOG
            LogapiAsync("OpenPlatform");

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
            
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

            

        }

        private void lbltimer_Click(object sender, EventArgs e)
        {

        }
    }
}
