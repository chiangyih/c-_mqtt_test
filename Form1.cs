using MQTTnet;

namespace c__mqtt_test
{
    /// <summary>
    /// MQTT 客戶端測試工具主表單
    /// 功能：連線 MQTT Broker、訂閱主題、接收訊息
    /// </summary>
    public partial class Form1 : Form
    {
        #region 欄位

        /// <summary>MQTT 客戶端實例</summary>
        private IMqttClient? mqttClient;

        /// <summary>當前訂閱的主題</summary>
        private string? currentSubscribedTopic;

        #endregion

        #region 初始化

        public Form1()
        {
            InitializeComponent();
            InitializeEventHandlers();
            InitializeMqtt();
            InitializeButtonStates();
        }

        /// <summary>初始化事件處理器</summary>
        private void InitializeEventHandlers()
        {
            button_connect.Click += Button_connect_Click;
            button_disconnect.Click += Button_disconnect_Click;
            button_confirm1.Click += Button_confirm1_Click;
            button_exit.Click += Button_exit_Click;
            this.FormClosing += Form1_FormClosing;
        }

        /// <summary>初始化 MQTT 客戶端</summary>
        private void InitializeMqtt()
        {
            var factory = new MqttClientFactory();
            mqttClient = factory.CreateMqttClient();
            mqttClient.ApplicationMessageReceivedAsync += MqttClient_ApplicationMessageReceivedAsync;
        }

        /// <summary>初始化按鈕狀態</summary>
        private void InitializeButtonStates()
        {
            button_disconnect.Enabled = false;
            button_confirm1.Enabled = false;
            UpdateConnectionStatus(false);
        }

        #endregion

        #region 連線管理

        /// <summary>
        /// 更新連線狀態顯示
        /// </summary>
        /// <param name="isConnected">是否已連線</param>
        private void UpdateConnectionStatus(bool isConnected)
        {
            if (isConnected)
            {
                label_ConnectionStatus.Text = "● 已連線";
                label_ConnectionStatus.ForeColor = Color.FromArgb(76, 175, 80); // 綠色
            }
            else
            {
                label_ConnectionStatus.Text = "● 未連線";
                label_ConnectionStatus.ForeColor = Color.Gray;
            }
        }

        /// <summary>連線按鈕點擊事件處理</summary>
        private async void Button_connect_Click(object? sender, EventArgs e)
        {
            // 驗證輸入
            if (!ValidateServerInput())
                return;

            try
            {
                await AttemptConnection();
            }
            catch (Exception ex)
            {
                HandleConnectionError(ex);
            }
        }

        /// <summary>驗證伺服器 IP 輸入</summary>
        /// <returns>輸入是否有效</returns>
        private bool ValidateServerInput()
        {
            if (string.IsNullOrWhiteSpace(textBox_serverIP.Text))
            {
                MessageBox.Show(
                    "請輸入 MQTT Server IP 位址",
                    "錯誤",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>嘗試連線到 MQTT Broker</summary>
        private async Task AttemptConnection()
        {
            button_connect.Enabled = false;
            label_ConnectionStatus.Text = "● 連線中...";
            label_ConnectionStatus.ForeColor = Color.Orange;

            try
            {
                var options = BuildMqttClientOptions();
                var response = await mqttClient!.ConnectAsync(options, CancellationToken.None);

                if (response.ResultCode == MqttClientConnectResultCode.Success)
                {
                    OnConnectionSuccess();
                }
                else
                {
                    OnConnectionFailed(response.ResultCode.ToString());
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>構建 MQTT 客戶端選項</summary>
        /// <returns>MQTT 客戶端選項</returns>
        private MqttClientOptions BuildMqttClientOptions()
        {
            return new MqttClientOptionsBuilder()
                .WithTcpServer(textBox_serverIP.Text, 1883)
                .WithClientId(Guid.NewGuid().ToString())
                .Build();
        }

        /// <summary>連線成功時的處理</summary>
        private void OnConnectionSuccess()
        {
            MessageBox.Show(
                "已完成連線",
                "連線成功",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            button_disconnect.Enabled = true;
            button_confirm1.Enabled = true;
            textBox_serverIP.Enabled = false;
            UpdateConnectionStatus(true);
        }

        /// <summary>連線失敗時的處理</summary>
        /// <param name="resultCode">失敗原因碼</param>
        private void OnConnectionFailed(string resultCode)
        {
            MessageBox.Show(
                $"連線失敗：{resultCode}",
                "錯誤",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            button_connect.Enabled = true;
            UpdateConnectionStatus(false);
        }

        /// <summary>處理連線錯誤</summary>
        /// <param name="ex">異常物件</param>
        private void HandleConnectionError(Exception ex)
        {
            MessageBox.Show(
                $"連線錯誤：{ex.Message}",
                "錯誤",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            button_connect.Enabled = true;
            UpdateConnectionStatus(false);
        }

        #endregion

        #region 斷線管理

        /// <summary>斷線按鈕點擊事件處理</summary>
        private async void Button_disconnect_Click(object? sender, EventArgs e)
        {
            // 確認使用者操作
            if (!ConfirmDisconnection())
                return;

            try
            {
                await DisconnectFromBroker();
            }
            catch (Exception ex)
            {
                HandleDisconnectionError(ex);
            }
        }

        /// <summary>確認使用者是否要斷線</summary>
        /// <returns>使用者是否確認斷線</returns>
        private bool ConfirmDisconnection()
        {
            DialogResult result = MessageBox.Show(
                "確定要斷開 MQTT 連線嗎？",
                "確認斷線",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            return result == DialogResult.Yes;
        }

        /// <summary>斷開與 MQTT Broker 的連線</summary>
        private async Task DisconnectFromBroker()
        {
            if (mqttClient != null && mqttClient.IsConnected)
            {
                await mqttClient.DisconnectAsync();
            }

            ResetConnectionState();
            OnDisconnectionSuccess();
        }

        /// <summary>重置連線相關的狀態</summary>
        private void ResetConnectionState()
        {
            button_connect.Enabled = true;
            button_disconnect.Enabled = false;
            button_confirm1.Enabled = false;
            textBox_serverIP.Enabled = true;
            currentSubscribedTopic = null;
        }

        /// <summary>斷線成功時的處理</summary>
        private void OnDisconnectionSuccess()
        {
            MessageBox.Show(
                "已斷開 MQTT 連線",
                "斷線完成",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            UpdateConnectionStatus(false);
        }

        /// <summary>處理斷線錯誤</summary>
        /// <param name="ex">異常物件</param>
        private void HandleDisconnectionError(Exception ex)
        {
            MessageBox.Show(
                $"斷線錯誤：{ex.Message}",
                "錯誤",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        #endregion

        #region 主題訂閱

        /// <summary>訂閱按鈕點擊事件處理</summary>
        private async void Button_confirm1_Click(object? sender, EventArgs e)
        {
            // 驗證輸入
            if (!ValidateTopicInput())
                return;

            // 驗證連線
            if (!ValidateConnection())
                return;

            try
            {
                await SubscribeToTopic();
            }
            catch (Exception ex)
            {
                HandleSubscriptionError(ex);
            }
        }

        /// <summary>驗證主題輸入</summary>
        /// <returns>輸入是否有效</returns>
        private bool ValidateTopicInput()
        {
            if (string.IsNullOrWhiteSpace(textBox_topic1.Text))
            {
                MessageBox.Show(
                    "請輸入訂閱主題",
                    "錯誤",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>驗證是否已連線</summary>
        /// <returns>是否已連線</returns>
        private bool ValidateConnection()
        {
            if (mqttClient == null || !mqttClient.IsConnected)
            {
                MessageBox.Show(
                    "請先連線到 MQTT Server",
                    "錯誤",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>訂閱指定的主題</summary>
        private async Task SubscribeToTopic()
        {
            string topic = textBox_topic1.Text;

            var topicFilter = new MqttTopicFilterBuilder()
                .WithTopic(topic)
                .Build();

            await mqttClient!.SubscribeAsync(topicFilter, CancellationToken.None);

            currentSubscribedTopic = topic;
            LogSubscriptionMessage(topic);
            NotifySubscriptionSuccess(topic);
        }

        /// <summary>記錄訂閱訊息到訊息框</summary>
        /// <param name="topic">訂閱的主題</param>
        private void LogSubscriptionMessage(string topic)
        {
            string message = $"[{DateTime.Now:HH:mm:ss}] 已訂閱主題：{topic}\r\n";
            AppendText(richTextBox_Sub_topic1, message);
        }

        /// <summary>通知使用者訂閱成功</summary>
        /// <param name="topic">訂閱的主題</param>
        private void NotifySubscriptionSuccess(string topic)
        {
            MessageBox.Show(
                $"成功訂閱主題：{topic}",
                "成功",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>處理訂閱錯誤</summary>
        /// <param name="ex">異常物件</param>
        private void HandleSubscriptionError(Exception ex)
        {
            MessageBox.Show(
                $"訂閱錯誤：{ex.Message}",
                "錯誤",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        #endregion

        #region 訊息接收

        /// <summary>MQTT 訊息接收事件處理</summary>
        private Task MqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            try
            {
                string topic = e.ApplicationMessage.Topic;
                string payload = e.ApplicationMessage.ConvertPayloadToString();

                // 只顯示訂閱主題的訊息
                if (IsSubscribedTopic(topic))
                {
                    this.Invoke(() =>
                    {
                        LogReceivedMessage(topic, payload);
                    });
                }
            }
            catch (Exception ex)
            {
                this.Invoke(() =>
                {
                    HandleMessageReceiveError(ex);
                });
            }

            return Task.CompletedTask;
        }

        /// <summary>檢查收到的主題是否為訂閱的主題</summary>
        /// <param name="topic">接收到的主題</param>
        /// <returns>是否為訂閱的主題</returns>
        private bool IsSubscribedTopic(string topic)
        {
            return !string.IsNullOrEmpty(currentSubscribedTopic) && 
                   topic == currentSubscribedTopic;
        }

        /// <summary>記錄接收到的訊息</summary>
        /// <param name="topic">訊息主題</param>
        /// <param name="payload">訊息內容</param>
        private void LogReceivedMessage(string topic, string payload)
        {
            string message = $"[{DateTime.Now:HH:mm:ss}] {payload}\r\n";
            AppendText(richTextBox_Sub_topic1, message);
        }

        /// <summary>處理訊息接收錯誤</summary>
        /// <param name="ex">異常物件</param>
        private void HandleMessageReceiveError(Exception ex)
        {
            MessageBox.Show(
                $"接收訊息錯誤：{ex.Message}",
                "錯誤",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        #endregion

        #region UI 輔助方法

        /// <summary>在 RichTextBox 中追加文字</summary>
        /// <param name="rtb">目標 RichTextBox</param>
        /// <param name="text">要追加的文字</param>
        private void AppendText(RichTextBox rtb, string text)
        {
            rtb.AppendText(text);
            rtb.ScrollToCaret();
        }

        #endregion

        #region 程式結束

        /// <summary>退出按鈕點擊事件處理</summary>
        private async void Button_exit_Click(object? sender, EventArgs e)
        {
            await CleanupResources();
            Application.Exit();
        }

        /// <summary>表單關閉事件處理</summary>
        private async void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            await CleanupResources();
        }

        /// <summary>清理程式資源</summary>
        private async Task CleanupResources()
        {
            try
            {
                if (mqttClient != null && mqttClient.IsConnected)
                {
                    await mqttClient.DisconnectAsync();
                }
                
                mqttClient?.Dispose();
            }
            catch
            {
                // 忽略清理時的錯誤，確保程式可以正常結束
            }
        }

        #endregion
    }
}
