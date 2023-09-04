namespace PMRDC
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.reloadmeun = new System.Windows.Forms.ToolStripMenuItem();
            this.closemeun = new System.Windows.Forms.ToolStripMenuItem();
            this.helpmeun = new System.Windows.Forms.ToolStripMenuItem();
            this.devModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label6 = new System.Windows.Forms.Label();
            this.label2xrepeat = new System.Windows.Forms.Label();
            this.textBox1reapeat = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2timer = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.topmosttext = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.step2text = new System.Windows.Forms.TextBox();
            this.dayofweektext = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.step1text = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.workstr_text = new System.Windows.Forms.TextBox();
            this.workend_text = new System.Windows.Forms.TextBox();
            this.now_text = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.day6_text = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.day7_text = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cancelidletxt = new System.Windows.Forms.TextBox();
            this.cancelidlebtn = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Khaki;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(75, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(610, 76);
            this.label1.TabIndex = 2;
            this.label1.Text = "PMRDC程式登入平台";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(1232, 120);
            this.label5.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 42);
            this.label5.TabIndex = 5;
            this.label5.Text = "使用者 :";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button3.ForeColor = System.Drawing.Color.SteelBlue;
            this.button3.Location = new System.Drawing.Point(752, 960);
            this.button3.Margin = new System.Windows.Forms.Padding(8);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(229, 75);
            this.button3.TabIndex = 6;
            this.button3.Text = "退出";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "PMRDC";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.HelpToolStripMenuItem_Click);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadmeun,
            this.closemeun,
            this.helpmeun,
            this.devModeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(265, 194);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // reloadmeun
            // 
            this.reloadmeun.AutoSize = false;
            this.reloadmeun.Image = ((System.Drawing.Image)(resources.GetObject("reloadmeun.Image")));
            this.reloadmeun.Name = "reloadmeun";
            this.reloadmeun.Size = new System.Drawing.Size(204, 46);
            this.reloadmeun.Text = "Reload";
            this.reloadmeun.Click += new System.EventHandler(this.ReloadToolStripMenuItem_Click);
            // 
            // closemeun
            // 
            this.closemeun.Image = global::PMRDC.Properties.Resources.update;
            this.closemeun.Name = "closemeun";
            this.closemeun.Size = new System.Drawing.Size(264, 48);
            this.closemeun.Text = "Update";
            this.closemeun.Click += new System.EventHandler(this.UpdateToolStripMenuItem_Click);
            // 
            // helpmeun
            // 
            this.helpmeun.Image = ((System.Drawing.Image)(resources.GetObject("helpmeun.Image")));
            this.helpmeun.Name = "helpmeun";
            this.helpmeun.Size = new System.Drawing.Size(264, 48);
            this.helpmeun.Text = "Help";
            this.helpmeun.Click += new System.EventHandler(this.HelpToolStripMenuItem_Click);
            this.helpmeun.Paint += new System.Windows.Forms.PaintEventHandler(this.helpmeun_Paint);
            // 
            // devModeToolStripMenuItem
            // 
            this.devModeToolStripMenuItem.Image = global::PMRDC.Properties.Resources.work_tools;
            this.devModeToolStripMenuItem.Name = "devModeToolStripMenuItem";
            this.devModeToolStripMenuItem.Size = new System.Drawing.Size(264, 48);
            this.devModeToolStripMenuItem.Text = "Dev mode";
            this.devModeToolStripMenuItem.Click += new System.EventHandler(this.devModeToolStripMenuItem_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(1232, 178);
            this.label6.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(100, 42);
            this.label6.TabIndex = 8;
            this.label6.Text = "版本 :";
            // 
            // label2xrepeat
            // 
            this.label2xrepeat.AutoSize = true;
            this.label2xrepeat.Location = new System.Drawing.Point(1203, 338);
            this.label2xrepeat.Name = "label2xrepeat";
            this.label2xrepeat.Size = new System.Drawing.Size(107, 30);
            this.label2xrepeat.TabIndex = 10;
            this.label2xrepeat.Text = "xRepeat";
            // 
            // textBox1reapeat
            // 
            this.textBox1reapeat.Location = new System.Drawing.Point(1328, 335);
            this.textBox1reapeat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1reapeat.Multiline = true;
            this.textBox1reapeat.Name = "textBox1reapeat";
            this.textBox1reapeat.Size = new System.Drawing.Size(207, 44);
            this.textBox1reapeat.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1203, 265);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 30);
            this.label3.TabIndex = 12;
            this.label3.Text = "Timer";
            // 
            // textBox2timer
            // 
            this.textBox2timer.Location = new System.Drawing.Point(1328, 262);
            this.textBox2timer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox2timer.Multiline = true;
            this.textBox2timer.Name = "textBox2timer";
            this.textBox2timer.Size = new System.Drawing.Size(207, 44);
            this.textBox2timer.TabIndex = 13;
            this.textBox2timer.TextChanged += new System.EventHandler(this.textBox2timer_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1192, 449);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 30);
            this.label7.TabIndex = 18;
            this.label7.Text = "topmost";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1163, 535);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 30);
            this.label8.TabIndex = 20;
            this.label8.Text = "第一階0";
            // 
            // topmosttext
            // 
            this.topmosttext.Location = new System.Drawing.Point(1328, 449);
            this.topmosttext.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.topmosttext.Multiline = true;
            this.topmosttext.Name = "topmosttext";
            this.topmosttext.Size = new System.Drawing.Size(207, 44);
            this.topmosttext.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1168, 633);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 30);
            this.label9.TabIndex = 22;
            this.label9.Text = "第二階0";
            // 
            // step2text
            // 
            this.step2text.Location = new System.Drawing.Point(1328, 630);
            this.step2text.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.step2text.Multiline = true;
            this.step2text.Name = "step2text";
            this.step2text.Size = new System.Drawing.Size(207, 44);
            this.step2text.TabIndex = 23;
            // 
            // dayofweektext
            // 
            this.dayofweektext.Location = new System.Drawing.Point(337, 496);
            this.dayofweektext.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dayofweektext.Multiline = true;
            this.dayofweektext.Name = "dayofweektext";
            this.dayofweektext.Size = new System.Drawing.Size(207, 44);
            this.dayofweektext.TabIndex = 24;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1339, 862);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(207, 44);
            this.textBox1.TabIndex = 25;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1152, 862);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(133, 30);
            this.label10.TabIndex = 27;
            this.label10.Text = "檔案路徑";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(752, 95);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(249, 74);
            this.textBox3.TabIndex = 28;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(1376, 30);
            this.textBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(460, 64);
            this.textBox4.TabIndex = 29;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label11.Location = new System.Drawing.Point(1200, 40);
            this.label11.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(166, 42);
            this.label11.TabIndex = 30;
            this.label11.Text = "目前視窗 :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(25, 499);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(262, 30);
            this.label12.TabIndex = 22;
            this.label12.Text = "nowtime.DayOfWeek";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(1354, 792);
            this.textBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(207, 44);
            this.textBox5.TabIndex = 24;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1104, 795);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(223, 30);
            this.label13.TabIndex = 22;
            this.label13.Text = "是否第一次開啟";
            // 
            // step1text
            // 
            this.step1text.Location = new System.Drawing.Point(1328, 535);
            this.step1text.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.step1text.Multiline = true;
            this.step1text.Name = "step1text";
            this.step1text.Size = new System.Drawing.Size(207, 44);
            this.step1text.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(83, 245);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 30);
            this.label2.TabIndex = 31;
            this.label2.Text = "上班開始時間";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(83, 319);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(193, 30);
            this.label4.TabIndex = 32;
            this.label4.Text = "下班結束時間";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(104, 387);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(133, 30);
            this.label14.TabIndex = 33;
            this.label14.Text = "目前時間";
            // 
            // workstr_text
            // 
            this.workstr_text.Location = new System.Drawing.Point(303, 231);
            this.workstr_text.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.workstr_text.Multiline = true;
            this.workstr_text.Name = "workstr_text";
            this.workstr_text.Size = new System.Drawing.Size(254, 53);
            this.workstr_text.TabIndex = 34;
            // 
            // workend_text
            // 
            this.workend_text.Location = new System.Drawing.Point(303, 315);
            this.workend_text.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.workend_text.Multiline = true;
            this.workend_text.Name = "workend_text";
            this.workend_text.Size = new System.Drawing.Size(254, 53);
            this.workend_text.TabIndex = 34;
            // 
            // now_text
            // 
            this.now_text.Location = new System.Drawing.Point(303, 384);
            this.now_text.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.now_text.Multiline = true;
            this.now_text.Name = "now_text";
            this.now_text.Size = new System.Drawing.Size(254, 53);
            this.now_text.TabIndex = 34;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(104, 592);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(103, 30);
            this.label15.TabIndex = 22;
            this.label15.Text = "星期六";
            // 
            // day6_text
            // 
            this.day6_text.Location = new System.Drawing.Point(337, 578);
            this.day6_text.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.day6_text.Multiline = true;
            this.day6_text.Name = "day6_text";
            this.day6_text.Size = new System.Drawing.Size(207, 44);
            this.day6_text.TabIndex = 24;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(104, 677);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(103, 30);
            this.label16.TabIndex = 22;
            this.label16.Text = "星期日";
            // 
            // day7_text
            // 
            this.day7_text.Location = new System.Drawing.Point(337, 663);
            this.day7_text.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.day7_text.Multiline = true;
            this.day7_text.Name = "day7_text";
            this.day7_text.Size = new System.Drawing.Size(207, 44);
            this.day7_text.TabIndex = 24;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(44, 782);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(193, 30);
            this.label17.TabIndex = 35;
            this.label17.Text = "取消閒置小時";
            // 
            // cancelidletxt
            // 
            this.cancelidletxt.Location = new System.Drawing.Point(303, 768);
            this.cancelidletxt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cancelidletxt.Multiline = true;
            this.cancelidletxt.Name = "cancelidletxt";
            this.cancelidletxt.Size = new System.Drawing.Size(207, 44);
            this.cancelidletxt.TabIndex = 36;
            this.cancelidletxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cancelidletxt_KeyPress);
            // 
            // cancelidlebtn
            // 
            this.cancelidlebtn.BackColor = System.Drawing.Color.NavajoWhite;
            this.cancelidlebtn.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cancelidlebtn.ForeColor = System.Drawing.Color.SteelBlue;
            this.cancelidlebtn.Location = new System.Drawing.Point(545, 757);
            this.cancelidlebtn.Margin = new System.Windows.Forms.Padding(8);
            this.cancelidlebtn.Name = "cancelidlebtn";
            this.cancelidlebtn.Size = new System.Drawing.Size(229, 75);
            this.cancelidlebtn.TabIndex = 37;
            this.cancelidlebtn.Text = "確認";
            this.cancelidlebtn.UseVisualStyleBackColor = false;
            this.cancelidlebtn.Click += new System.EventHandler(this.cancelidlebtn_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(317, 844);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(43, 30);
            this.label18.TabIndex = 38;
            this.label18.Text = "無";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(612, 265);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(500, 445);
            this.textBox2.TabIndex = 39;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(607, 212);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(163, 30);
            this.label19.TabIndex = 40;
            this.label19.Text = "Xrepeat紀錄";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(1883, 1060);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.cancelidlebtn);
            this.Controls.Add(this.cancelidletxt);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.now_text);
            this.Controls.Add(this.workend_text);
            this.Controls.Add(this.workstr_text);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.day7_text);
            this.Controls.Add(this.day6_text);
            this.Controls.Add(this.dayofweektext);
            this.Controls.Add(this.step2text);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.topmosttext);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.step1text);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox2timer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1reapeat);
            this.Controls.Add(this.label2xrepeat);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem reloadmeun;
        private System.Windows.Forms.ToolStripMenuItem closemeun;
        private System.Windows.Forms.ToolStripMenuItem helpmeun;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2xrepeat;
        private System.Windows.Forms.TextBox textBox1reapeat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2timer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox topmosttext;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox step2text;
        private System.Windows.Forms.TextBox dayofweektext;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolStripMenuItem devModeToolStripMenuItem;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox step1text;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox workstr_text;
        private System.Windows.Forms.TextBox workend_text;
        private System.Windows.Forms.TextBox now_text;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox day6_text;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox day7_text;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox cancelidletxt;
        private System.Windows.Forms.Button cancelidlebtn;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label19;
    }
}
