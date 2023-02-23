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
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2xrepeat = new System.Windows.Forms.Label();
            this.textBox1reapeat = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2timer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1_cmstatus = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1xsition = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1sleepbefore = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox1sleeptime = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox2totalsleepandxrepeat = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3path = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
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
            this.label1.Location = new System.Drawing.Point(28, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(610, 76);
            this.label1.TabIndex = 2;
            this.label1.Text = "PMRDC程式登入平台";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(462, 48);
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
            this.button3.Location = new System.Drawing.Point(282, 384);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(86, 30);
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
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadmeun,
            this.closemeun,
            this.helpmeun});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(223, 146);
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
            this.closemeun.Size = new System.Drawing.Size(222, 48);
            this.closemeun.Text = "Update";
            this.closemeun.Click += new System.EventHandler(this.UpdateToolStripMenuItem_Click);
            // 
            // helpmeun
            // 
            this.helpmeun.Image = ((System.Drawing.Image)(resources.GetObject("helpmeun.Image")));
            this.helpmeun.Name = "helpmeun";
            this.helpmeun.Size = new System.Drawing.Size(222, 48);
            this.helpmeun.Text = "Help";
            this.helpmeun.Click += new System.EventHandler(this.HelpToolStripMenuItem_Click);
            this.helpmeun.Paint += new System.Windows.Forms.PaintEventHandler(this.helpmeun_Paint);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(462, 71);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(100, 42);
            this.label6.TabIndex = 8;
            this.label6.Text = "版本 :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(51, 374);
            this.button1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 30);
            this.button1.TabIndex = 9;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2xrepeat
            // 
            this.label2xrepeat.AutoSize = true;
            this.label2xrepeat.Location = new System.Drawing.Point(451, 135);
            this.label2xrepeat.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2xrepeat.Name = "label2xrepeat";
            this.label2xrepeat.Size = new System.Drawing.Size(45, 13);
            this.label2xrepeat.TabIndex = 10;
            this.label2xrepeat.Text = "xRepeat";
            // 
            // textBox1reapeat
            // 
            this.textBox1reapeat.Location = new System.Drawing.Point(498, 134);
            this.textBox1reapeat.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.textBox1reapeat.Multiline = true;
            this.textBox1reapeat.Name = "textBox1reapeat";
            this.textBox1reapeat.Size = new System.Drawing.Size(80, 20);
            this.textBox1reapeat.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(451, 106);
            this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Timer";
            // 
            // textBox2timer
            // 
            this.textBox2timer.Location = new System.Drawing.Point(498, 105);
            this.textBox2timer.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.textBox2timer.Multiline = true;
            this.textBox2timer.Name = "textBox2timer";
            this.textBox2timer.Size = new System.Drawing.Size(80, 20);
            this.textBox2timer.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 105);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "電腦狀態";
            // 
            // textBox1_cmstatus
            // 
            this.textBox1_cmstatus.Location = new System.Drawing.Point(33, 140);
            this.textBox1_cmstatus.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.textBox1_cmstatus.Multiline = true;
            this.textBox1_cmstatus.Name = "textBox1_cmstatus";
            this.textBox1_cmstatus.Size = new System.Drawing.Size(152, 185);
            this.textBox1_cmstatus.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(238, 105);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "x座標";
            // 
            // textBox1xsition
            // 
            this.textBox1xsition.Location = new System.Drawing.Point(240, 140);
            this.textBox1xsition.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.textBox1xsition.Multiline = true;
            this.textBox1xsition.Name = "textBox1xsition";
            this.textBox1xsition.Size = new System.Drawing.Size(152, 185);
            this.textBox1xsition.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(417, 175);
            this.label7.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "睡眠前xRepeat";
            // 
            // textBox1sleepbefore
            // 
            this.textBox1sleepbefore.Location = new System.Drawing.Point(498, 174);
            this.textBox1sleepbefore.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.textBox1sleepbefore.Multiline = true;
            this.textBox1sleepbefore.Name = "textBox1sleepbefore";
            this.textBox1sleepbefore.Size = new System.Drawing.Size(80, 20);
            this.textBox1sleepbefore.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(436, 214);
            this.label8.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "睡眠多久";
            // 
            // textBox1sleeptime
            // 
            this.textBox1sleeptime.Location = new System.Drawing.Point(498, 214);
            this.textBox1sleeptime.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.textBox1sleeptime.Multiline = true;
            this.textBox1sleeptime.Name = "textBox1sleeptime";
            this.textBox1sleeptime.Size = new System.Drawing.Size(80, 20);
            this.textBox1sleeptime.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(426, 254);
            this.label9.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "睡眠+xrepeat";
            // 
            // textBox2totalsleepandxrepeat
            // 
            this.textBox2totalsleepandxrepeat.Location = new System.Drawing.Point(498, 252);
            this.textBox2totalsleepandxrepeat.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.textBox2totalsleepandxrepeat.Multiline = true;
            this.textBox2totalsleepandxrepeat.Name = "textBox2totalsleepandxrepeat";
            this.textBox2totalsleepandxrepeat.Size = new System.Drawing.Size(80, 20);
            this.textBox2totalsleepandxrepeat.TabIndex = 23;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(492, 305);
            this.textBox1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(80, 20);
            this.textBox1.TabIndex = 24;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(492, 344);
            this.textBox2.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(80, 20);
            this.textBox2.TabIndex = 25;
            // 
            // textBox3path
            // 
            this.textBox3path.Location = new System.Drawing.Point(123, 340);
            this.textBox3path.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.textBox3path.Multiline = true;
            this.textBox3path.Name = "textBox3path";
            this.textBox3path.Size = new System.Drawing.Size(356, 35);
            this.textBox3path.TabIndex = 26;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(49, 345);
            this.label10.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "檔案路徑";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(282, 38);
            this.textBox3.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(96, 32);
            this.textBox3.TabIndex = 28;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(706, 461);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox3path);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox2totalsleepandxrepeat);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox1sleeptime);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox1sleepbefore);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox1xsition);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1_cmstatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2timer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1reapeat);
            this.Controls.Add(this.label2xrepeat);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load_1);
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2xrepeat;
        private System.Windows.Forms.TextBox textBox1reapeat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2timer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1_cmstatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1xsition;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1sleepbefore;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox1sleeptime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox2totalsleepandxrepeat;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3path;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox3;
    }
}
