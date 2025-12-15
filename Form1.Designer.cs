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
            components = new System.ComponentModel.Container();
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
            groupBox_Publish = new GroupBox();
            LED_sw = new Button();
            pub_bt1 = new Button();
            pub_topic1 = new TextBox();
            label_PublishTopic = new Label();
            toolTip1 = new ToolTip(components);
            groupBox_Connection.SuspendLayout();
            groupBox_Subscribe.SuspendLayout();
            groupBox_Publish.SuspendLayout();
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
            groupBox_Connection.Padding = new Padding(15, 10, 15, 15);
            groupBox_Connection.Size = new Size(640, 130);
            groupBox_Connection.TabIndex = 0;
            groupBox_Connection.TabStop = false;
            groupBox_Connection.Text = "MQTT 連線設定";
            // 
            // label_ConnectionStatus
            // 
            label_ConnectionStatus.AutoSize = true;
            label_ConnectionStatus.Font = new Font("Microsoft JhengHei UI", 11F, FontStyle.Bold);
            label_ConnectionStatus.ForeColor = Color.Gray;
            label_ConnectionStatus.Location = new Point(400, 85);
            label_ConnectionStatus.Name = "label_ConnectionStatus";
            label_ConnectionStatus.Size = new Size(73, 19);
            label_ConnectionStatus.TabIndex = 4;
            label_ConnectionStatus.Text = "● 未連線";
            // 
            // label_ServerIP
            // 
            label_ServerIP.AutoSize = true;
            label_ServerIP.Font = new Font("Microsoft JhengHei UI", 10F);
            label_ServerIP.Location = new Point(20, 35);
            label_ServerIP.Name = "label_ServerIP";
            label_ServerIP.Size = new Size(124, 18);
            label_ServerIP.TabIndex = 0;
            label_ServerIP.Text = "MQTT Server IP：";
            // 
            // textBox_serverIP
            // 
            textBox_serverIP.Font = new Font("Microsoft JhengHei UI", 12F);
            textBox_serverIP.Location = new Point(20, 60);
            textBox_serverIP.Name = "textBox_serverIP";
            textBox_serverIP.PlaceholderText = "輸入 IP 位址或域名（例如：broker.emqx.io）";
            textBox_serverIP.Size = new Size(360, 28);
            textBox_serverIP.TabIndex = 1;
            toolTip1.SetToolTip(textBox_serverIP, "輸入 MQTT Broker 的 IP 位址或域名");
            // 
            // button_connect
            // 
            button_connect.BackColor = Color.FromArgb(76, 175, 80);
            button_connect.Cursor = Cursors.Hand;
            button_connect.FlatAppearance.BorderSize = 0;
            button_connect.FlatStyle = FlatStyle.Flat;
            button_connect.Font = new Font("Microsoft JhengHei UI", 11F, FontStyle.Bold);
            button_connect.ForeColor = Color.White;
            button_connect.Location = new Point(400, 30);
            button_connect.Name = "button_connect";
            button_connect.Size = new Size(110, 45);
            button_connect.TabIndex = 2;
            button_connect.Text = "連線";
            toolTip1.SetToolTip(button_connect, "連線到 MQTT Broker");
            button_connect.UseVisualStyleBackColor = false;
            // 
            // button_disconnect
            // 
            button_disconnect.BackColor = Color.FromArgb(244, 67, 54);
            button_disconnect.Cursor = Cursors.Hand;
            button_disconnect.FlatAppearance.BorderSize = 0;
            button_disconnect.FlatStyle = FlatStyle.Flat;
            button_disconnect.Font = new Font("Microsoft JhengHei UI", 11F, FontStyle.Bold);
            button_disconnect.ForeColor = Color.White;
            button_disconnect.Location = new Point(520, 30);
            button_disconnect.Name = "button_disconnect";
            button_disconnect.Size = new Size(110, 45);
            button_disconnect.TabIndex = 3;
            button_disconnect.Text = "斷線";
            toolTip1.SetToolTip(button_disconnect, "斷開 MQTT 連線");
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
            groupBox_Subscribe.Location = new Point(20, 170);
            groupBox_Subscribe.Name = "groupBox_Subscribe";
            groupBox_Subscribe.Padding = new Padding(15, 10, 15, 15);
            groupBox_Subscribe.Size = new Size(640, 420);
            groupBox_Subscribe.TabIndex = 1;
            groupBox_Subscribe.TabStop = false;
            groupBox_Subscribe.Text = "主題訂閱與訊息接收";
            groupBox_Subscribe.Enter += groupBox_Subscribe_Enter;
            // 
            // label_Topic
            // 
            label_Topic.AutoSize = true;
            label_Topic.Font = new Font("Microsoft JhengHei UI", 10F);
            label_Topic.Location = new Point(20, 35);
            label_Topic.Name = "label_Topic";
            label_Topic.Size = new Size(78, 18);
            label_Topic.TabIndex = 0;
            label_Topic.Text = "主題名稱：";
            // 
            // textBox_topic1
            // 
            textBox_topic1.Font = new Font("Microsoft JhengHei UI", 12F);
            textBox_topic1.Location = new Point(20, 60);
            textBox_topic1.Name = "textBox_topic1";
            textBox_topic1.PlaceholderText = "例如：test/topic 或 sensor/temperature";
            textBox_topic1.Size = new Size(460, 28);
            textBox_topic1.TabIndex = 1;
            toolTip1.SetToolTip(textBox_topic1, "輸入要訂閱的 MQTT 主題");
            // 
            // button_confirm1
            // 
            button_confirm1.BackColor = Color.FromArgb(33, 150, 243);
            button_confirm1.Cursor = Cursors.Hand;
            button_confirm1.FlatAppearance.BorderSize = 0;
            button_confirm1.FlatStyle = FlatStyle.Flat;
            button_confirm1.Font = new Font("Microsoft JhengHei UI", 11F, FontStyle.Bold);
            button_confirm1.ForeColor = Color.White;
            button_confirm1.Location = new Point(490, 54);
            button_confirm1.Name = "button_confirm1";
            button_confirm1.Size = new Size(140, 40);
            button_confirm1.TabIndex = 2;
            button_confirm1.Text = "訂閱主題";
            toolTip1.SetToolTip(button_confirm1, "訂閱指定的 MQTT 主題");
            button_confirm1.UseVisualStyleBackColor = false;
            // 
            // label_Messages
            // 
            label_Messages.AutoSize = true;
            label_Messages.Font = new Font("Microsoft JhengHei UI", 10F);
            label_Messages.Location = new Point(20, 110);
            label_Messages.Name = "label_Messages";
            label_Messages.Size = new Size(78, 18);
            label_Messages.TabIndex = 3;
            label_Messages.Text = "接收訊息：";
            // 
            // richTextBox_Sub_topic1
            // 
            richTextBox_Sub_topic1.BackColor = Color.FromArgb(250, 250, 250);
            richTextBox_Sub_topic1.BorderStyle = BorderStyle.FixedSingle;
            richTextBox_Sub_topic1.Font = new Font("Consolas", 10F);
            richTextBox_Sub_topic1.ForeColor = Color.FromArgb(33, 33, 33);
            richTextBox_Sub_topic1.Location = new Point(20, 135);
            richTextBox_Sub_topic1.Name = "richTextBox_Sub_topic1";
            richTextBox_Sub_topic1.ReadOnly = true;
            richTextBox_Sub_topic1.Size = new Size(610, 270);
            richTextBox_Sub_topic1.TabIndex = 4;
            richTextBox_Sub_topic1.Text = "";
            // 
            // button_exit
            // 
            button_exit.BackColor = Color.FromArgb(158, 158, 158);
            button_exit.Cursor = Cursors.Hand;
            button_exit.FlatAppearance.BorderSize = 0;
            button_exit.FlatStyle = FlatStyle.Flat;
            button_exit.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            button_exit.ForeColor = Color.White;
            button_exit.Location = new Point(690, 530);
            button_exit.Name = "button_exit";
            button_exit.Size = new Size(300, 60);
            button_exit.TabIndex = 2;
            button_exit.Text = "離開程式";
            toolTip1.SetToolTip(button_exit, "關閉應用程式");
            button_exit.UseVisualStyleBackColor = false;
            // 
            // groupBox_Publish
            // 
            groupBox_Publish.Controls.Add(LED_sw);
            groupBox_Publish.Controls.Add(pub_bt1);
            groupBox_Publish.Controls.Add(pub_topic1);
            groupBox_Publish.Controls.Add(label_PublishTopic);
            groupBox_Publish.Font = new Font("Microsoft JhengHei UI", 10F, FontStyle.Bold);
            groupBox_Publish.Location = new Point(690, 20);
            groupBox_Publish.Name = "groupBox_Publish";
            groupBox_Publish.Padding = new Padding(15, 10, 15, 15);
            groupBox_Publish.Size = new Size(300, 490);
            groupBox_Publish.TabIndex = 3;
            groupBox_Publish.TabStop = false;
            groupBox_Publish.Text = "訊息發布控制";
            // 
            // LED_sw
            // 
            LED_sw.BackColor = Color.Gray;
            LED_sw.Cursor = Cursors.Hand;
            LED_sw.FlatAppearance.BorderSize = 0;
            LED_sw.FlatStyle = FlatStyle.Flat;
            LED_sw.Font = new Font("Microsoft JhengHei UI", 11F, FontStyle.Bold);
            LED_sw.ForeColor = Color.White;
            LED_sw.Location = new Point(20, 180);
            LED_sw.Name = "LED_sw";
            LED_sw.Size = new Size(260, 100);
            LED_sw.TabIndex = 3;
            LED_sw.Text = "LED 開關\r\n(點擊切換 ON/OFF)";
            toolTip1.SetToolTip(LED_sw, "控制 LED 開關狀態");
            LED_sw.UseVisualStyleBackColor = false;
            // 
            // pub_bt1
            // 
            pub_bt1.BackColor = Color.FromArgb(0, 188, 212);
            pub_bt1.Cursor = Cursors.Hand;
            pub_bt1.FlatAppearance.BorderSize = 0;
            pub_bt1.FlatStyle = FlatStyle.Flat;
            pub_bt1.Font = new Font("Microsoft JhengHei UI", 11F, FontStyle.Bold);
            pub_bt1.ForeColor = Color.White;
            pub_bt1.Location = new Point(20, 105);
            pub_bt1.Name = "pub_bt1";
            pub_bt1.Size = new Size(260, 50);
            pub_bt1.TabIndex = 2;
            pub_bt1.Text = "設定發布主題";
            toolTip1.SetToolTip(pub_bt1, "設定要發布訊息的主題");
            pub_bt1.UseVisualStyleBackColor = false;
            // 
            // pub_topic1
            // 
            pub_topic1.Font = new Font("Microsoft JhengHei UI", 12F);
            pub_topic1.Location = new Point(20, 60);
            pub_topic1.Name = "pub_topic1";
            pub_topic1.PlaceholderText = "例如：led/control";
            pub_topic1.Size = new Size(260, 28);
            pub_topic1.TabIndex = 1;
            toolTip1.SetToolTip(pub_topic1, "輸入要發布訊息的主題");
            // 
            // label_PublishTopic
            // 
            label_PublishTopic.AutoSize = true;
            label_PublishTopic.Font = new Font("Microsoft JhengHei UI", 10F);
            label_PublishTopic.Location = new Point(20, 35);
            label_PublishTopic.Name = "label_PublishTopic";
            label_PublishTopic.Size = new Size(78, 18);
            label_PublishTopic.TabIndex = 0;
            label_PublishTopic.Text = "發布主題：";
            // 
            // toolTip1
            // 
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 300;
            toolTip1.ReshowDelay = 100;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1010, 610);
            Controls.Add(groupBox_Publish);
            Controls.Add(button_exit);
            Controls.Add(groupBox_Subscribe);
            Controls.Add(groupBox_Connection);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MQTT 客戶端測試工具 v2.0";
            groupBox_Connection.ResumeLayout(false);
            groupBox_Connection.PerformLayout();
            groupBox_Subscribe.ResumeLayout(false);
            groupBox_Subscribe.PerformLayout();
            groupBox_Publish.ResumeLayout(false);
            groupBox_Publish.PerformLayout();
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
        private GroupBox groupBox_Publish;
        private Button pub_bt1;
        private TextBox pub_topic1;
        private Label label_PublishTopic;
        private Button LED_sw;
        private ToolTip toolTip1;
    }
}
