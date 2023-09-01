using IWshRuntimeLibrary;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Policy;
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
        //計時器開啟，預設先關閉
        //紀錄上一次x座標
        int Pastx;
        //紀錄目前x座標
        int Nowx = int.Parse(string.Format("{0}", System.Windows.Forms.Cursor.Position.X));
        //紀錄次數
        int xrepeat = 0;
        //判斷6Sigma是否存在
        bool SW_exist;
        //取得目前登入的帳號
        string strUserName = WindowsIdentity.GetCurrent().Name;
        //此系統版本
        string Version = "v20230729";
        int idlestop = -1;
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
        bool sigmacheck = false;
        string oaname = Environment.MachineName;
        string idlestoptime = "無";
        int suspendxrepeat;
        int aa = 0;
        string updatemin;
        string processname;
        TimeSpan worktime_str = DateTime.Parse("09:00:00").TimeOfDay;
        TimeSpan worktime_end = DateTime.Parse("18:00:00").TimeOfDay;
        //172.18.212.76/
        //172.18.212.76
        bool sigmaFirstOpen = true;
        int idlestopcount = 0;
        DateTime suspendtimestr;
        DateTime suspendtimeend;
        int totolsleeptime = 0;
        string catchexlog = "default";
        Dictionary<string, Dictionary<string, object>> SWDictionary = new Dictionary<string, Dictionary<string, object>>();
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
        public void idlecloseSW(string SWname)
        {
            Process[] processlist = Process.GetProcesses();
            try
            {
                foreach (Process process in processlist)
                {
                    if (!String.IsNullOrEmpty(process.MainWindowTitle))
                    {
                        //textBox1.Text += process.ProcessName + "," + process.Id + "," + process.MainWindowTitle + "\r\n";
                        if (process.ProcessName.Contains(SWname))
                        {
                            processname = process.ProcessName;
                            //bringToFront(process.MainWindowTitle);
                            process.Kill();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (catchexlog != ex.Message)
                {
                    //logwrite(ex.ToString());
                    LogapiAsync(ex.Message + ":" + processname, SWname, 0);
                    catchexlog = ex.Message;
                }

            }

        }
        private string GetActiveWindowTitle(string SWname)
        {
            const int nChars = 256;
            System.Text.StringBuilder Buff = new System.Text.StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                if (Buff.ToString().Contains(SWname))
                {
                    return "WindowsTitleCheck";
                }
                else
                {
                    return "WindowsTitleCheckNot";
                }

            }
            return "WindowsTitleCheckNot";
        }
        public void bringToFront(string title)
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
        public bool CreateDesktopShortcut(string FileName)
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
                //logwrite("建立捷徑錯誤");
                LogapiAsync("建立捷徑錯誤", "PMRDC");
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
        public void delpastPMRDC()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName("PMRDC");
            foreach (Process process in processes)
            {
                if (process.Id.ToString() != currentProcess.Id.ToString())
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
                catch
                {
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
                sw.WriteLine(logmsg + "  xRepeat : " + xrepeat);
                sw.Dispose();

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
        public async Task<bool> LogapiAsync(string action, string SWname, int deltatime = 0)
        {

            //將User使用狀況回傳到server紀錄
            //取得windows使用者名稱
            string[] aryUserInfo = strUserName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            //建立新連線後，將資料用post方式回傳
            HttpClient client = new HttpClient();
            string reponse = await client.GetStringAsync("http://172.18.212.76/log/" + SWname + "/ " + Version + "/" + aryUserInfo[1] + "/" + action + "/" + deltatime + "/" + oaname + "/" + xrepeat);
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
                timeminend = DateTime.Now;
                TimeSpan ts = timeminend.Subtract(timeminstr);
                int tsmin = (int)ts.TotalMinutes;
                foreach (var outerKvp in SWDictionary)
                {
                    SW_exist = SW_existFuc((string)outerKvp.Value["SWname"]);
                    if (SW_exist && (bool)outerKvp.Value["check"])
                    {
                        outerKvp.Value["xrepeat"] = 0;
                        LogapiAsync("userclose", (string)outerKvp.Value["SWname"], tsmin);
                        //ClosePress("6SigmaET");//關閉外部檔案
                        outerKvp.Value["FirstOpen"] = true;
                    }
                }
                    
                
                string zippath = Environment.GetFolderPath(Environment.SpecialFolder.Startup).Replace("Startup", "PMRDC/" + descJsonVer.Now_version);
                int tempi = 1;
                while (Directory.Exists(zippath))
                {
                    zippath = Environment.GetFolderPath(Environment.SpecialFolder.Startup).Replace("Startup", "PMRDC/" + descJsonVer.Now_version + "_" + tempi.ToString());
                    tempi++;
                }
                if (!Directory.Exists(zippath))
                {
                    //建立資料夾
                    Directory.CreateDirectory(zippath);
                }
                int titleindex = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName.IndexOf("PMRDC.exe");
                string notitlepath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName.Remove(titleindex);
                string remoteUri = @"http://172.18.212.76/media/PMRDCPlatform";
                string fileName = descJsonVer.Downloaduri, myStringWebResource = null;
                WebClient myWebClient = new WebClient();
                myStringWebResource = Path.Combine(remoteUri, fileName);
                string targetsave = Path.Combine(notitlepath, fileName);
                myWebClient.DownloadFile(myStringWebResource, targetsave);

                ZipFile.ExtractToDirectory(Application.StartupPath + "/" + descJsonVer.Downloaduri, zippath);
                //logwrite("Update from_" + descJsonVer.user_version);
                LogapiAsync("Update from_" + descJsonVer.user_version, "PMRDC");
                Process PMRDCexe = new Process();
                // FileName 是要執行的檔案
                PMRDCexe.StartInfo.FileName = zippath + "/PMRDC.exe";
                PMRDCexe.Start();
                System.Environment.Exit(0);
            }
            return true;
        }

        public bool SW_existFuc(string SWname)
        {
            //判斷6Sigma是否存在，存在回傳true，不存在回傳false
            Process[] processes = Process.GetProcessesByName(SWname);
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
                for (int i = 0; i < MyProcess.Length; i++)
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
            this.Dispose();
        }
        public string nametitle()
        {
            string[] aryUserInfo = strUserName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            char str1 = aryUserInfo[1].ToUpper().FirstOrDefault();
            int asciicode = ((int)str1 - 65) % 3;
            return asciicode.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //textBox1xsition.Text += (string.Format("{0}", System.Windows.Forms.Cursor.Position.X) + "\r\n");
            textBox2timer.Text = aa.ToString();
            ++aa;
            
            //workstr_text.Text = worktime_str.ToString();
            //workend_text.Text = worktime_end.ToString();
            //day6_text.Text = DayOfWeek.Saturday.ToString();
            //day7_text.Text = DayOfWeek.Sunday.ToString();
            DateTime nowtime = DateTime.Now;
            if (nowtime.ToString("HH:mm") == ("13:0" + updatemin) || nowtime.ToString("HH:mm") == ("17:0" + updatemin))
            {
                Task<bool> task = VersioncheckAsync();
            }
            if (idlestop > 0)
            {
                ++idlestopcount;
                if (idlestopcount != idlestop)
                {
                    return;
                }
                if (idlestopcount > idlestop)
                {
                    idlestopcount = 0;
                    idlestop = -1;
                }

            }
            foreach (var outerKvp in SWDictionary)
            {
                textBox1reapeat.Text = outerKvp.Value["xrepeat"].ToString();
                SW_exist = SW_existFuc((string)outerKvp.Value["SWname"]);
                textBox1.Text = SW_exist.ToString();
                //textBox5.Text = sigmaFirstOpen.ToString();
                //如果Sigma有打開，就在執行
                if (SW_exist)
                {
                    outerKvp.Value["check"] = true;
                    //如果Sigma是打開的，那就判斷是否曾經打開過
                    //如果沒打開過，就紀錄第一次X，且判斷程已打開過
                    if ((bool)outerKvp.Value["FirstOpen"] == true)
                    {
                        outerKvp.Value["timeminstr"] = DateTime.Now;
                        outerKvp.Value["nowX"] = int.Parse(string.Format("{0}", System.Windows.Forms.Cursor.Position.X));
                        //logwrite("Open6Sigma");
                        LogapiAsync("Open", (string)outerKvp.Value["SWname"]);
                        outerKvp.Value["FirstOpen"] = false;
                        return;
                    }
                    //如果打開過，就來判斷X座標
                    if ((bool)outerKvp.Value["FirstOpen"] == false)
                    {
                        outerKvp.Value["PastX"] = outerKvp.Value["nowX"];
                        outerKvp.Value["nowX"] = int.Parse(string.Format("{0}", System.Windows.Forms.Cursor.Position.X));
                        string topmostcheck = GetActiveWindowTitle((string)outerKvp.Value["SWname"]);
                        topmosttext.Text = topmostcheck;
                        //如果滑鼠座標跟上一次依樣，或是滑鼠座標等於0，或是6Simga不在最上層
                        if (((int)outerKvp.Value["PastX"] == (int)outerKvp.Value["nowX"]) || ((int)outerKvp.Value["PastX"] == 0) || (topmostcheck == "WindowsTitleCheckNot"))
                        {
                            dayofweektext.Text = nowtime.DayOfWeek.ToString();
                            TimeSpan dspNow = nowtime.TimeOfDay;
                            now_text.Text = dspNow.ToString();
                            //如果目前時間在工作時間內且不是星期六和星期日，xrepeat就必須跑
                            if ((dspNow >= worktime_str) && (dspNow <= worktime_end) && (DayOfWeek.Saturday != nowtime.DayOfWeek) && (DayOfWeek.Sunday != nowtime.DayOfWeek))
                            {
                                outerKvp.Value["xrepeat"] = (int)outerKvp.Value["xrepeat"] + 1;
                            }
                            else
                            {
                                outerKvp.Value["xrepeat"] = 0;
                            }

                        }
                        else
                        {
                            outerKvp.Value["xrepeat"] = 0;

                        }
                    }
                    if (((int)outerKvp.Value["xrepeat"] % 135 == 0) && ((int)outerKvp.Value["xrepeat"] != 0))
                    {
                        //logwrite("idle45");
                        LogapiAsync("idle45", (string)outerKvp.Value["SWname"]);
                        string text45 = "電腦已閒置45分鐘，請立即存檔。系統若閒置60分鐘，將強制關閉" + (string)outerKvp.Value["SWname"] + "。\r\n" +
                            "需持續動作該軟體一分鐘以上，解除閒置狀態" + nowtime.ToString("HH:mm:ss");
                        MessageBox.Show(new Form { TopMost = true }, text45);
                        return;
                    }
                    //如果60分鐘都沒動，就紀錄後關閉平台
                    if (((int)outerKvp.Value["xrepeat"] % 180 == 0) && ((int)outerKvp.Value["xrepeat"] != 0))
                    {
                        timeminend = DateTime.Now;
                        TimeSpan ts = timeminend.Subtract((DateTime)outerKvp.Value["timeminstr"]);
                        int tsmin = (int)ts.TotalMinutes;
                        //logwrite("idle60");
                        Task<bool> task = LogapiAsync("idle60", (string)outerKvp.Value["SWname"], tsmin);
                        idlecloseSW((string)outerKvp.Value["SWname"]);
                        //ClosePress("6SigmaET");//關閉外部檔案
                        string text60 = "因電腦閒置60分鐘，故將"+ (string)outerKvp.Value["SWname"] + "關閉。";
                        outerKvp.Value["FirstOpen"] = true;
                        outerKvp.Value["check"] = false;
                        totolsleeptime = 0;
                        outerKvp.Value["xrepeat"] = 0;
                        MessageBox.Show(new Form { TopMost = true }, text60);
                        return;
                    }

                }
                //如果Sigma是關閉的，Sigma也曾經開啟
                if (!SW_exist && (bool)outerKvp.Value["FirstOpen"] == false)
                {
                    outerKvp.Value["check"] = false;
                    //logwrite("userclose6Sigma");
                    timeminend = DateTime.Now;
                    TimeSpan ts = timeminend.Subtract((DateTime)outerKvp.Value["timeminstr"]);
                    int tsmin = (int)ts.TotalMinutes - totolsleeptime;
                    LogapiAsync("userclose", (string)outerKvp.Value["SWname"], tsmin);
                    outerKvp.Value["FirstOpen"] = true;
                    totolsleeptime = 0;
                    outerKvp.Value["xrepeat"] = 0;
                }
            }
        }


        void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            //textBox1_cmstatus.Text += (e.Mode.ToString() + "\r\n");
            
            if (e.Mode.ToString() == "Suspend")
            {
                suspendtimestr = DateTime.Now;
                timeminend = DateTime.Now;
                foreach (var outerKvp in SWDictionary)
                {
                    SW_exist = SW_existFuc((string)outerKvp.Value["SWname"]);
                    if ((int)outerKvp.Value["xrepeat"] > 90 && SW_exist)
                    {
                        TimeSpan ts = timeminend.Subtract((DateTime)outerKvp.Value["timeminstr"]);
                        int tsmin = (int)ts.TotalMinutes;
                        //logwrite("sleepclose");
                        Task<bool> task = LogapiAsync("sleepclose", (string)outerKvp.Value["SWname"], tsmin);
                        idlecloseSW((string)outerKvp.Value["SWname"]);
                        //ClosePress("6SigmaET");//關閉外部檔案
                        string text60 = "因電腦即將睡眠，故將" + (string)outerKvp.Value["SWname"] + "關閉。";
                        outerKvp.Value["FirstOpen"] = true;
                        MessageBox.Show(new Form { TopMost = true }, text60);
                    }
                    if (!SW_exist && (bool)outerKvp.Value["FirstOpen"] == false)
                    {
                        outerKvp.Value["check"] = false;
                        totolsleeptime = 0;
                        //logwrite("userclose6Sigma");
                        timeminend = DateTime.Now;
                        TimeSpan ts = timeminend.Subtract((DateTime)outerKvp.Value["timeminstr"]);
                        int tsmin = (int)ts.TotalMinutes;
                        LogapiAsync("userclose", (string)outerKvp.Value["SWname"], tsmin);
                        outerKvp.Value["FirstOpen"] = true;
                        outerKvp.Value["xrepeat"] = 0;
                    }
                };
            }
            if (e.Mode.ToString() == "Resume")
            {
                suspendtimeend = DateTime.Now;
                TimeSpan ts = suspendtimeend.Subtract(suspendtimestr);
                int tsmin = (int)ts.TotalMinutes;
                totolsleeptime += tsmin;
                foreach (var outerKvp in SWDictionary)
                {
                    int totsuspendtimeandxrepeat = tsmin + (int)outerKvp.Value["xrepeat"];
                    SW_exist = SW_existFuc((string)outerKvp.Value["SWname"]);
                    try
                    {
                        //textBox1sleeptime.Text = tsmin.ToString();
                        //textBox2totalsleepandxrepeat.Text = totsuspendtimeandxrepeat.ToString();
                        if (totsuspendtimeandxrepeat > 60 && SW_exist == true)
                        {
                            //logwrite("SleepOver60min");
                            LogapiAsync("SleepOver60min", (string)outerKvp.Value["SWname"], totsuspendtimeandxrepeat);
                        }
                    }
                    catch (Exception ex)
                    {
                        //logwrite(ex.ToString());
                        LogapiAsync(ex.Message + ": 紀錄睡眠錯誤", "PMRDC", 0);
                    }
                }
            }
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            Dictionary<string, object> SigmaDictionary = new Dictionary<string, object>();
            SigmaDictionary["SWname"] = "6SigmaET";
            SigmaDictionary["xrepeat"] = 0;
            SigmaDictionary["SW_exist"] = SW_existFuc("6Sigma");
            SigmaDictionary["check"] = false;
            SigmaDictionary["FirstOpen"] = true;
            SigmaDictionary["totolsleeptime"] = 0;
            SigmaDictionary["timeminstr"] = DateTime.Now;
            SigmaDictionary["xrepeat"] = 0;
            SigmaDictionary["PastX"] = 0;
            SigmaDictionary["nowX"] = 0;
            SWDictionary["Sigma"] = SigmaDictionary;
            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
            //當平台開啟時，先預設一些要跑的動作
            //在桌面建立一個捷徑
            this.notifyIcon1.Text = Version;
            Thread.Sleep(5000);
            delpastPMRDC();
            //判斷紀錄LOG的資料夾和檔案是否存在
            Filecheck();
            CreateDesktopShortcut("PMRDC.exe");
            LogapiAsync("OpenPlatform", "PMRDC");
            //紀錄LOG
            //logwrite("Open Platform");
            Task<bool> task = VersioncheckAsync();
            //判斷平台是否有重複開啟，有的話把先前的全部關掉，留一個並重新啟動
            string[] aryUserInfo = strUserName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            label5.Text = "使用者 : " + aryUserInfo[1];
            label6.Text = Version;
            //判斷版本
            //Task<bool> task = VersioncheckAsync();
            //紀錄開啟平台LOG
            //LogapiAsync("OpenPlatform");
            timer1.Interval = 20000;
            timer1.Enabled = true;
            this.ShowInTaskbar = false;
            this.Hide();
            updatemin = nametitle();
            //textBox2timer.Text = aa.ToString();
            //v20230410
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e) //當平台要被關閉前，紀錄被關閉
        {
            foreach (var outerKvp in SWDictionary)
            {
                SW_exist = SW_existFuc((string)outerKvp.Value["SWname"]);
                //logwrite("測試是否關閉且記錄到log");
                //如果平台關閉之前，sigma還是開著;或是sigma被打開，但沒紀錄到關掉
                DateTime timerstr_temp = (DateTime)outerKvp.Value["timeminstr"];
                if ((SW_exist == true || (bool)outerKvp.Value["check"] == true) && timerstr_temp.ToString("yyyy") != "0001")
                {
                    //logwrite("userclose6Sigma");
                    timeminend = DateTime.Now;
                    TimeSpan ts = timeminend.Subtract(timerstr_temp);
                    int tsmin = (int)ts.TotalMinutes;
                    LogapiAsync("userclose", (string)outerKvp.Value["SWname"], tsmin);
                    outerKvp.Value["xrepeat"] = 0;
                }
            }
        }


        private void ReloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string textrrload = "重載成功!";
            foreach (var outerKvp in SWDictionary)
            {
                timeminend = DateTime.Now;
                SW_exist = SW_existFuc((string)outerKvp.Value["SWname"]);
                if (SW_exist && (bool)outerKvp.Value["check"])
                {
                    timeminend = DateTime.Now;
                    TimeSpan ts = timeminend.Subtract((DateTime)outerKvp.Value["timeminstr"]);
                    int tsmin = (int)ts.TotalMinutes;
                    //logwrite("userclose6Sigma");
                    outerKvp.Value["xrepeat"] = 0;
                    LogapiAsync("userclose", (string)outerKvp.Value["SWname"], tsmin);
                    //ClosePress("6SigmaET");//關閉外部檔案
                    outerKvp.Value["FirstOpen"] = true;
                }
            }
                
            MessageBox.Show(new Form { TopMost = true }, textrrload);
            System.Windows.Forms.Application.Restart();
        }

        private void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否手動執行更新", "版本更新", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                Task<bool> task = VersioncheckAsync();
                //Process PMRDCexe = new Process();
                //// FileName 是要執行的檔案
                //PMRDCexe.StartInfo.FileName = this.GetType().Assembly.Location;
                //PMRDCexe.Start();

            }
            else if (dr == DialogResult.Cancel)
            {

            }

            //VersioncheckAsync();

        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (idlestop == -1)
            {
                idlestoptime = "無";
            }
            string texthelp = "* 依軟體使用辦法，請勿長時間占用\r\n* " +
          "若有急需使用軟體，但無License情況請聯絡RDPM(Marcus Kuo #33930)\r\n* " +
          "程序問題可以先在右下角圖示右鍵Reload排除\r\n* " +
          "平台使用問題請聯絡Willy Guo(#32725)\r\n" +
          "版本號 : " + Version + "\r\n" +
          "xrepeate : " + xrepeat + "\r\n" +
          "閒置取消時間到 : " + idlestoptime;

            MessageBox.Show(new Form { TopMost = true }, texthelp);
        }

        private void helpmeun_Paint(object sender, PaintEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timeminstr = DateTime.Now;
            timeminend = DateTime.Now;
            TimeSpan ts = timeminend.Subtract(timeminstr);
            int tsmin = (int)ts.TotalMinutes;
            //logwrite("idle60");
            //Task<bool> task = LogapiAsync("idle60", tsmin);
            idlecloseSW("6SigmaET");
            //ClosePress("6SigmaET");//關閉外部檔案
            string text60 = "因電腦閒置60分鐘，故將6Sigma關閉。";
            sigmaFirstOpen = true;
            MessageBox.Show(new Form { TopMost = true }, text60);
        }

        private void textBox2timer_TextChanged(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void devModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(this);//產生Form2的物件，才可以使用它所提供的Method

            this.Visible = false;//將Form1隱藏。由於在Form1的程式碼內使用this，所以this為Form1的物件本身
            f.Visible = true;//顯示第二個視窗
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cancelidlebtn_Click(object sender, EventArgs e)
        {
            string textBoxValue = cancelidletxt.Text;
            if (string.IsNullOrEmpty(textBoxValue))
            {
                // 处理文本框为空的情况
            }
            else
            {
                xrepeat = 0;
                idlestopcount = 0;
                label18.Text = textBoxValue + "小時";
                idlestop = int.Parse(textBoxValue);
                DateTime currentTime = DateTime.Now;
                DateTime newTime = currentTime.AddHours(idlestop);
                idlestop = idlestop * 180;
                idlestoptime = newTime + "結束";
                // 处理文本框不为空的情况
            }
        }

        private void cancelidletxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // 如果输入的不是数字或允许的特殊键，则阻止该字符的输入
                e.Handled = true;
            }
        }
    }
}
