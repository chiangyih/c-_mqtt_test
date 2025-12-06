namespace c__mqtt_test
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox_Connection = new GroupBox();
            label_ConnectionStatus = new Label();
            label_ServerIP = new Label();
            textBox_serverIP = new TextBox();
            button_connect = new Button();
            button_disconnect = new Button();
            groupBox_Subscribe = new GroupBox();
            label_Topic = new Label();
            textBox_topic1 = new TextBox();
            button_confirm1 = new Button();
            label_Messages = new Label();
            richTextBox_Sub_topic1 = new RichTextBox();
            button_exit = new Button();
            groupBox_Connection.SuspendLayout();
            groupBox_Subscribe.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox_Connection
            // 
            groupBox_Connection.Controls.Add(label_ConnectionStatus);
            groupBox_Connection.Controls.Add(label_ServerIP);
            groupBox_Connection.Controls.Add(textBox_serverIP);
            groupBox_Connection.Controls.Add(button_connect);
            groupBox_Connection.Controls.Add(button_disconnect);
            groupBox_Connection.Font = new Font("Microsoft JhengHei UI", 10F, FontStyle.Bold);
            groupBox_Connection.Location = new Point(20, 20);
            groupBox_Connection.Name = "groupBox_Connection";
            groupBox_Connection.Size = new Size(600, 120);
            groupBox_Connection.TabIndex = 0;
            groupBox_Connection.TabStop = false;
            groupBox_Connection.Text = "MQTT 連線設定";
            // 
            // label_ConnectionStatus
            // 
            label_ConnectionStatus.AutoSize = true;
            label_ConnectionStatus.Font = new Font("Microsoft JhengHei UI", 10F, FontStyle.Bold);
            label_ConnectionStatus.ForeColor = Color.Gray;
            label_ConnectionStatus.Location = new Point(380, 80);
            label_ConnectionStatus.Name = "label_ConnectionStatus";
            label_ConnectionStatus.Size = new Size(93, 18);
            label_ConnectionStatus.TabIndex = 4;
            label_ConnectionStatus.Text = "● 未連線";
            // 
            // label_ServerIP
            // 
            label_ServerIP.AutoSize = true;
            label_ServerIP.Font = new Font("Microsoft JhengHei UI", 10F);
            label_ServerIP.Location = new Point(20, 35);
            label_ServerIP.Name = "label_ServerIP";
            label_ServerIP.Size = new Size(133, 18);
            label_ServerIP.TabIndex = 0;
            label_ServerIP.Text = "MQTT Server IP：";
            // 
            // textBox_serverIP
            // 
            textBox_serverIP.Font = new Font("Microsoft JhengHei UI", 12F);
            textBox_serverIP.Location = new Point(20, 60);
            textBox_serverIP.Name = "textBox_serverIP";
            textBox_serverIP.PlaceholderText = "輸入 IP 位址或域名";
            textBox_serverIP.Size = new Size(340, 28);
            textBox_serverIP.TabIndex = 1;
            // 
            // button_connect
            // 
            button_connect.BackColor = Color.FromArgb(76, 175, 80);
            button_connect.FlatStyle = FlatStyle.Flat;
            button_connect.Font = new Font("Microsoft JhengHei UI", 11F, FontStyle.Bold);
            button_connect.ForeColor = Color.White;
            button_connect.Location = new Point(380, 30);
            button_connect.Name = "button_connect";
            button_connect.Size = new Size(100, 40);
            button_connect.TabIndex = 2;
            button_connect.Text = "連線";
            button_connect.UseVisualStyleBackColor = false;
            // 
            // button_disconnect
            // 
            button_disconnect.BackColor = Color.FromArgb(244, 67, 54);
            button_disconnect.FlatStyle = FlatStyle.Flat;
            button_disconnect.Font = new Font("Microsoft JhengHei UI", 11F, FontStyle.Bold);
            button_disconnect.ForeColor = Color.White;
            button_disconnect.Location = new Point(490, 30);
            button_disconnect.Name = "button_disconnect";
            button_disconnect.Size = new Size(100, 40);
            button_disconnect.TabIndex = 3;
            button_disconnect.Text = "斷線";
            button_disconnect.UseVisualStyleBackColor = false;
            // 
            // groupBox_Subscribe
            // 
            groupBox_Subscribe.Controls.Add(label_Topic);
            groupBox_Subscribe.Controls.Add(textBox_topic1);
            groupBox_Subscribe.Controls.Add(button_confirm1);
            groupBox_Subscribe.Controls.Add(label_Messages);
            groupBox_Subscribe.Controls.Add(richTextBox_Sub_topic1);
            groupBox_Subscribe.Font = new Font("Microsoft JhengHei UI", 10F, FontStyle.Bold);
            groupBox_Subscribe.Location = new Point(20, 160);
            groupBox_Subscribe.Name = "groupBox_Subscribe";
            groupBox_Subscribe.Size = new Size(600, 380);
            groupBox_Subscribe.TabIndex = 1;
            groupBox_Subscribe.TabStop = false;
            groupBox_Subscribe.Text = "主題訂閱";
            // 
            // label_Topic
            // 
            label_Topic.AutoSize = true;
            label_Topic.Font = new Font("Microsoft JhengHei UI", 10F);
            label_Topic.Location = new Point(20, 35);
            label_Topic.Name = "label_Topic";
            label_Topic.Size = new Size(79, 18);
            label_Topic.TabIndex = 0;
            label_Topic.Text = "主題名稱：";
            // 
            // textBox_topic1
            // 
            textBox_topic1.Font = new Font("Microsoft JhengHei UI", 12F);
            textBox_topic1.Location = new Point(20, 60);
            textBox_topic1.Name = "textBox_topic1";
            textBox_topic1.PlaceholderText = "例如：test/topic";
            textBox_topic1.Size = new Size(440, 28);
            textBox_topic1.TabIndex = 1;
            // 
            // button_confirm1
            // 
            button_confirm1.BackColor = Color.FromArgb(33, 150, 243);
            button_confirm1.FlatStyle = FlatStyle.Flat;
            button_confirm1.Font = new Font("Microsoft JhengHei UI", 11F, FontStyle.Bold);
            button_confirm1.ForeColor = Color.White;
            button_confirm1.Location = new Point(470, 55);
            button_confirm1.Name = "button_confirm1";
            button_confirm1.Size = new Size(120, 38);
            button_confirm1.TabIndex = 2;
            button_confirm1.Text = "訂閱";
            button_confirm1.UseVisualStyleBackColor = false;
            // 
            // label_Messages
            // 
            label_Messages.AutoSize = true;
            label_Messages.Font = new Font("Microsoft JhengHei UI", 10F);
            label_Messages.Location = new Point(20, 105);
            label_Messages.Name = "label_Messages";
            label_Messages.Size = new Size(79, 18);
            label_Messages.TabIndex = 3;
            label_Messages.Text = "接收訊息：";
            // 
            // richTextBox_Sub_topic1
            // 
            richTextBox_Sub_topic1.BackColor = Color.FromArgb(245, 245, 245);
            richTextBox_Sub_topic1.BorderStyle = BorderStyle.FixedSingle;
            richTextBox_Sub_topic1.Font = new Font("Consolas", 10F);
            richTextBox_Sub_topic1.Location = new Point(20, 130);
            richTextBox_Sub_topic1.Name = "richTextBox_Sub_topic1";
            richTextBox_Sub_topic1.ReadOnly = true;
            richTextBox_Sub_topic1.Size = new Size(570, 235);
            richTextBox_Sub_topic1.TabIndex = 4;
            richTextBox_Sub_topic1.Text = "";
            // 
            // button_exit
            // 
            button_exit.BackColor = Color.FromArgb(158, 158, 158);
            button_exit.FlatStyle = FlatStyle.Flat;
            button_exit.Font = new Font("Microsoft JhengHei UI", 11F, FontStyle.Bold);
            button_exit.ForeColor = Color.White;
            button_exit.Location = new Point(520, 555);
            button_exit.Name = "button_exit";
            button_exit.Size = new Size(100, 40);
            button_exit.TabIndex = 2;
            button_exit.Text = "離開";
            button_exit.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(640, 610);
            Controls.Add(button_exit);
            Controls.Add(groupBox_Subscribe);
            Controls.Add(groupBox_Connection);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MQTT 客戶端測試工具";
            groupBox_Connection.ResumeLayout(false);
            groupBox_Connection.PerformLayout();
            groupBox_Subscribe.ResumeLayout(false);
            groupBox_Subscribe.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox_Connection;
        private Label label_ServerIP;
        private TextBox textBox_serverIP;
        private Button button_connect;
        private Button button_disconnect;
        private Label label_ConnectionStatus;
        private GroupBox groupBox_Subscribe;
        private Label label_Topic;
        private TextBox textBox_topic1;
        private Button button_confirm1;
        private Label label_Messages;
        private RichTextBox richTextBox_Sub_topic1;
        private Button button_exit;
    }
}
