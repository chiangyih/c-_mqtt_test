using MQTTnet;
using MQTTnet.Protocol;

namespace c__mqtt_test
{
    /// <summary>
    /// MQTT 客戶端測試工具主表單
    /// </summary>
    /// <remarks>
    /// <para>提供以下核心功能：</para>
    /// <list type="bullet">
    ///   <item><description>連線到 MQTT Broker（支援 TCP 連線）</description></item>
    ///   <item><description>訂閱單一或多個主題</description></item>
    ///   <item><description>即時接收和顯示訊息</description></item>
    ///   <item><description>發布訊息到指定主題</description></item>
    ///   <item><description>LED 控制（ON/OFF 切換）</description></item>
    /// </list>
    /// <para>使用 MQTTnet 5.0 函式庫實作 MQTT 協定</para>
    /// </remarks>
    /// <example>
    /// 基本使用流程：
    /// 1. 輸入 MQTT Server IP
    /// 2. 點擊「連線」建立連線
    /// 3. 輸入主題並點擊「訂閱」
    /// 4. 接收即時訊息
    /// 5. 使用 LED 開關發送控制訊息
    /// </example>
    public partial class Form1 : Form
    {
        #region 常數定義

        /// <summary>MQTT 配置常數</summary>
        private static class MqttConfig
        {
            /// <summary>預設 MQTT 連接埠號</summary>
            public const int DefaultPort = 1883;

            /// <summary>連線超時時間（毫秒）</summary>
            public const int ConnectionTimeout = 10000;

            /// <summary>預設 QoS 等級</summary>
            public const MqttQualityOfServiceLevel DefaultQoS = MqttQualityOfServiceLevel.AtLeastOnce;

            /// <summary>訊息保留旗標</summary>
            public const bool RetainMessage = false;

            /// <summary>訊息框最大行數</summary>
            public const int MaxMessageLines = 1000;
        }

        /// <summary>UI 色彩配置常數</summary>
        private static class ColorConfig
        {
            /// <summary>連線成功顏色（綠色）</summary>
            public static readonly Color ConnectedColor = Color.FromArgb(76, 175, 80);

            /// <summary>連線中顏色（橘色）</summary>
            public static readonly Color ConnectingColor = Color.Orange;

            /// <summary>未連線顏色（灰色）</summary>
            public static readonly Color DisconnectedColor = Color.Gray;

            /// <summary>LED 開啟顏色（紅色）</summary>
            public static readonly Color LedOnColor = Color.Red;

            /// <summary>LED 關閉顏色（灰色）</summary>
            public static readonly Color LedOffColor = Color.Gray;
        }

        /// <summary>訊息文字常數</summary>
        private static class MessageText
        {
            public const string Connected = "● 已連線";
            public const string Connecting = "● 連線中...";
            public const string Disconnected = "● 未連線";
            public const string LedOn = "ON";
            public const string LedOff = "OFF";
        }

        #endregion

        #region 欄位

        /// <summary>MQTT 客戶端實例</summary>
        /// <remarks>負責處理所有 MQTT 通訊，包括連線、訂閱、發布和接收訊息</remarks>
        private IMqttClient? mqttClient;

        /// <summary>當前訂閱的主題</summary>
        /// <remarks>
        /// 儲存目前訂閱的主題名稱，用於過濾接收到的訊息
        /// 僅顯示與訂閱主題匹配的訊息
        /// </remarks>
        private string? currentSubscribedTopic;

        /// <summary>當前發布的主題</summary>
        /// <remarks>
        /// 儲存用於發布訊息的主題名稱
        /// 必須先透過「設定發布主題」按鈕設定，才能使用 LED 開關
        /// </remarks>
        private string? currentPublishTopic;

        /// <summary>LED 開關狀態</summary>
        /// <remarks>
        /// true: LED 開啟，發送 "ON" 訊息
        /// false: LED 關閉，發送 "OFF" 訊息
        /// </remarks>
        private bool isLedOn = false;

        /// <summary>訊息計數器</summary>
        /// <remarks>追蹤接收到的訊息數量，用於訊息管理</remarks>
        private int messageCount = 0;

        #endregion

        #region 初始化

        /// <summary>
        /// 建構函式 - 初始化 Form1 表單
        /// </summary>
        /// <remarks>
        /// 執行順序：
        /// 1. 初始化 UI 元件
        /// 2. 綁定事件處理器
        /// 3. 初始化 MQTT 客戶端
        /// 4. 設定初始按鈕狀態
        /// </remarks>
        public Form1()
        {
            InitializeComponent();
            InitializeEventHandlers();
            InitializeMqtt();
            InitializeButtonStates();
        }

        /// <summary>
        /// 初始化所有事件處理器
        /// </summary>
        /// <remarks>
        /// 集中管理所有 UI 控件的事件綁定，便於維護和追蹤
        /// 包含按鈕點擊事件和表單關閉事件
        /// </remarks>
        private void InitializeEventHandlers()
        {
            // 連線相關事件
            button_connect.Click += Button_connect_Click;
            button_disconnect.Click += Button_disconnect_Click;

            // 訂閱相關事件
            button_confirm1.Click += Button_confirm1_Click;

            // 發布相關事件
            pub_bt1.Click += Pub_bt1_Click;
            LED_sw.Click += LED_sw_Click;

            // 程式結束事件
            button_exit.Click += Button_exit_Click;
            this.FormClosing += Form1_FormClosing;
        }

        /// <summary>
        /// 初始化 MQTT 客戶端
        /// </summary>
        /// <remarks>
        /// 建立 MQTT 客戶端實例並註冊訊息接收事件處理器
        /// 使用 MQTTnet 的工廠模式建立客戶端
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// 當 MQTT 客戶端建立失敗時拋出
        /// </exception>
        private void InitializeMqtt()
        {
            try
            {
                var factory = new MqttClientFactory();
                mqttClient = factory.CreateMqttClient();

                // 註冊訊息接收事件
                mqttClient.ApplicationMessageReceivedAsync += MqttClient_ApplicationMessageReceivedAsync;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"MQTT 客戶端初始化失敗：{ex.Message}",
                    "初始化錯誤",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 初始化按鈕和控件的初始狀態
        /// </summary>
        /// <remarks>
        /// 設定程式啟動時的 UI 狀態：
        /// - 斷線按鈕：停用
        /// - 訂閱按鈕：停用
        /// - 連線狀態：未連線
        /// - LED 按鈕：灰色（關閉）
        /// </remarks>
        private void InitializeButtonStates()
        {
            button_disconnect.Enabled = false;
            button_confirm1.Enabled = false;
            UpdateConnectionStatus(false);
            UpdateLedButtonColor(false);
        }

        #endregion

        #region 連線管理

        /// <summary>
        /// 更新連線狀態顯示
        /// </summary>
        /// <param name="isConnected">連線狀態：true=已連線，false=未連線</param>
        /// <remarks>
        /// 根據連線狀態更新 UI 顯示：
        /// - 已連線：綠色 "● 已連線"
        /// - 連線中：橘色 "● 連線中..."
        /// - 未連線：灰色 "● 未連線"
        /// </remarks>
        private void UpdateConnectionStatus(bool isConnected)
        {
            if (isConnected)
            {
                label_ConnectionStatus.Text = MessageText.Connected;
                label_ConnectionStatus.ForeColor = ColorConfig.ConnectedColor;
            }
            else
            {
                label_ConnectionStatus.Text = MessageText.Disconnected;
                label_ConnectionStatus.ForeColor = ColorConfig.DisconnectedColor;
            }
        }

        /// <summary>
        /// 連線按鈕點擊事件處理
        /// </summary>
        /// <remarks>
        /// 執行流程：
        /// 1. 驗證 Server IP 輸入
        /// 2. 嘗試建立連線
        /// 3. 處理連線結果或異常
        /// </remarks>
        /// <exception cref="Exception">連線過程中的任何異常</exception>
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

        /// <summary>
        /// 驗證伺服器 IP 輸入
        /// </summary>
        /// <returns>true=輸入有效，false=輸入無效</returns>
        /// <remarks>
        /// 檢查項目：
        /// - 輸入框不可為空白
        /// - 輸入框不可僅包含空白字元
        /// </remarks>
        private bool ValidateServerInput()
        {
            if (string.IsNullOrWhiteSpace(textBox_serverIP.Text))
            {
                MessageBox.Show(
                    "請輸入 MQTT Server IP 位址",
                    "輸入錯誤",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 嘗試連線到 MQTT Broker
        /// </summary>
        /// <remarks>
        /// 連線步驟：
        /// 1. 停用連線按鈕，防止重複點擊
        /// 2. 更新狀態為「連線中」
        /// 3. 建立 MQTT 連線選項
        /// 4. 執行連線
        /// 5. 根據結果執行成功或失敗處理
        /// </remarks>
        /// <exception cref="Exception">連線失敗時拋出異常</exception>
        private async Task AttemptConnection()
        {
            button_connect.Enabled = false;
            label_ConnectionStatus.Text = MessageText.Connecting;
            label_ConnectionStatus.ForeColor = ColorConfig.ConnectingColor;

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
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 構建 MQTT 客戶端連線選項
        /// </summary>
        /// <returns>配置完成的 MQTT 連線選項</returns>
        /// <remarks>
        /// 連線配置：
        /// - 使用 TCP 協定
        /// - 預設連接埠：1883
        /// - 自動產生唯一的 Client ID（使用 GUID）
        /// - 無帳號密碼驗證
        /// </remarks>
        private MqttClientOptions BuildMqttClientOptions()
        {
            return new MqttClientOptionsBuilder()
                .WithTcpServer(textBox_serverIP.Text, MqttConfig.DefaultPort)
                .WithClientId(Guid.NewGuid().ToString())
                .WithTimeout(TimeSpan.FromMilliseconds(MqttConfig.ConnectionTimeout))
                .Build();
        }

        /// <summary>
        /// 連線成功時的處理
        /// </summary>
        /// <remarks>
        /// 執行動作：
        /// 1. 顯示成功訊息對話框
        /// 2. 啟用斷線和訂閱按鈕
        /// 3. 停用 Server IP 輸入框
        /// 4. 更新連線狀態顯示為「已連線」
        /// </remarks>
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

        /// <summary>
        /// 連線失敗時的處理
        /// </summary>
        /// <param name="resultCode">MQTT 連線結果代碼</param>
        /// <remarks>
        /// 執行動作：
        /// 1. 顯示失敗原因對話框
        /// 2. 重新啟用連線按鈕
        /// 3. 更新連線狀態為「未連線」
        /// </remarks>
        private void OnConnectionFailed(string resultCode)
        {
            MessageBox.Show(
                $"連線失敗：{resultCode}",
                "連線錯誤",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            button_connect.Enabled = true;
            UpdateConnectionStatus(false);
        }

        /// <summary>
        /// 處理連線錯誤
        /// </summary>
        /// <param name="ex">異常物件</param>
        /// <remarks>
        /// 處理各種連線異常，例如：
        /// - 網路不通
        /// - Broker 未回應
        /// - 連線超時
        /// - 其他通訊錯誤
        /// </remarks>
        private void HandleConnectionError(Exception ex)
        {
            MessageBox.Show(
                $"連線錯誤：{ex.Message}",
                "系統錯誤",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            button_connect.Enabled = true;
            UpdateConnectionStatus(false);
        }

        #endregion

        #region 斷線管理

        /// <summary>
        /// 斷線按鈕點擊事件處理
        /// </summary>
        /// <remarks>
        /// 執行流程：
        /// 1. 詢問使用者確認斷線
        /// 2. 執行斷線操作
        /// 3. 處理斷線結果或異常
        /// </remarks>
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

        /// <summary>
        /// 確認使用者是否要斷線
        /// </summary>
        /// <returns>true=使用者確認斷線，false=使用者取消</returns>
        /// <remarks>
        /// 顯示確認對話框，防止誤觸斷線按鈕
        /// </remarks>
        private bool ConfirmDisconnection()
        {
            DialogResult result = MessageBox.Show(
                "確定要斷開 MQTT 連線嗎？",
                "確認斷線",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            return result == DialogResult.Yes;
        }

        /// <summary>
        /// 斷開與 MQTT Broker 的連線
        /// </summary>
        /// <remarks>
        /// 執行步驟：
        /// 1. 檢查客戶端是否已連線
        /// 2. 執行斷線操作
        /// 3. 重置所有連線相關狀態
        /// 4. 顯示斷線成功訊息
        /// </remarks>
        private async Task DisconnectFromBroker()
        {
            if (mqttClient != null && mqttClient.IsConnected)
            {
                await mqttClient.DisconnectAsync();
            }

            ResetConnectionState();
            OnDisconnectionSuccess();
        }

        /// <summary>
        /// 重置連線相關的狀態
        /// </summary>
        /// <remarks>
        /// 重置項目：
        /// - 啟用連線按鈕
        /// - 停用斷線和訂閱按鈕
        /// - 啟用 Server IP 輸入框
        /// - 清除訂閱主題記錄
        /// - 重置 LED 狀態
        /// </remarks>
        private void ResetConnectionState()
        {
            button_connect.Enabled = true;
            button_disconnect.Enabled = false;
            button_confirm1.Enabled = false;
            textBox_serverIP.Enabled = true;
            currentSubscribedTopic = null;
            isLedOn = false;
            UpdateLedButtonColor(false);
        }

        /// <summary>
        /// 斷線成功時的處理
        /// </summary>
        /// <remarks>
        /// 顯示斷線成功訊息並更新連線狀態顯示
        /// </remarks>
        private void OnDisconnectionSuccess()
        {
            MessageBox.Show(
                "已斷開 MQTT 連線",
                "斷線完成",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            UpdateConnectionStatus(false);
        }

        /// <summary>
        /// 處理斷線錯誤
        /// </summary>
        /// <param name="ex">異常物件</param>
        /// <remarks>
        /// 處理斷線過程中可能發生的異常
        /// </remarks>
        private void HandleDisconnectionError(Exception ex)
        {
            MessageBox.Show(
                $"斷線錯誤：{ex.Message}",
                "系統錯誤",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        #endregion

        #region 主題訂閱

        /// <summary>
        /// 訂閱按鈕點擊事件處理
        /// </summary>
        /// <remarks>
        /// 執行流程：
        /// 1. 驗證主題輸入
        /// 2. 驗證 MQTT 連線狀態
        /// 3. 執行主題訂閱
        /// 4. 處理訂閱結果或異常
        /// </remarks>
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

        /// <summary>
        /// 驗證主題輸入
        /// </summary>
        /// <returns>true=輸入有效，false=輸入無效</returns>
        /// <remarks>
        /// 檢查項目：
        /// - 主題名稱不可為空白
        /// - 主題名稱不可僅包含空白字元
        /// </remarks>
        private bool ValidateTopicInput()
        {
            if (string.IsNullOrWhiteSpace(textBox_topic1.Text))
            {
                MessageBox.Show(
                    "請輸入訂閱主題",
                    "輸入錯誤",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 驗證 MQTT 連線狀態
        /// </summary>
        /// <returns>true=已連線，false=未連線</returns>
        /// <remarks>
        /// 確保在執行訂閱或發布操作前，MQTT 客戶端已連線
        /// </remarks>
        private bool ValidateConnection()
        {
            if (mqttClient == null || !mqttClient.IsConnected)
            {
                MessageBox.Show(
                    "請先連線到 MQTT Server",
                    "連線錯誤",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 訂閱指定的 MQTT 主題
        /// </summary>
        /// <remarks>
        /// 執行步驟：
        /// 1. 取得主題名稱
        /// 2. 建立主題過濾器（使用預設 QoS）
        /// 3. 執行訂閱操作
        /// 4. 記錄訂閱主題
        /// 5. 記錄訊息到訊息框
        /// 6. 顯示成功通知
        /// </remarks>
        /// <exception cref="Exception">訂閱失敗時拋出異常</exception>
        private async Task SubscribeToTopic()
        {
            string topic = textBox_topic1.Text.Trim();

            var topicFilter = new MqttTopicFilterBuilder()
                .WithTopic(topic)
                .Build();

            await mqttClient!.SubscribeAsync(topicFilter, CancellationToken.None);

            currentSubscribedTopic = topic;
            LogSubscriptionMessage(topic);
            NotifySubscriptionSuccess(topic);
        }

        /// <summary>
        /// 記錄訂閱訊息到訊息框
        /// </summary>
        /// <param name="topic">訂閱的主題名稱</param>
        /// <remarks>
        /// 訊息格式：[HH:mm:ss] 已訂閱主題：主題名稱
        /// </remarks>
        private void LogSubscriptionMessage(string topic)
        {
            string message = $"[{DateTime.Now:HH:mm:ss}] 已訂閱主題：{topic}\r\n";
            AppendText(richTextBox_Sub_topic1, message);
        }

        /// <summary>
        /// 通知使用者訂閱成功
        /// </summary>
        /// <param name="topic">訂閱的主題名稱</param>
        /// <remarks>
        /// 顯示包含主題名稱的成功對話框
        /// </remarks>
        private void NotifySubscriptionSuccess(string topic)
        {
            MessageBox.Show(
                $"成功訂閱主題：{topic}",
                "訂閱成功",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// 處理訂閱錯誤
        /// </summary>
        /// <param name="ex">異常物件</param>
        /// <remarks>
        /// 處理訂閱過程中可能發生的異常，例如：
        /// - 網路中斷
        /// - Broker 拒絕訂閱
        /// - 無效的主題名稱
        /// </remarks>
        private void HandleSubscriptionError(Exception ex)
        {
            MessageBox.Show(
                $"訂閱錯誤：{ex.Message}",
                "系統錯誤",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        #endregion

        #region 訊息接收

        /// <summary>
        /// MQTT 訊息接收事件處理器
        /// </summary>
        /// <param name="e">包含接收到的訊息資料</param>
        /// <returns>完成的 Task</returns>
        /// <remarks>
        /// 處理流程：
        /// 1. 解析訊息主題和內容
        /// 2. 檢查是否為訂閱的主題
        /// 3. 使用 UI 執行緒更新顯示
        /// 4. 記錄訊息到訊息框
        /// 5. 處理任何異常
        /// </remarks>
        /// <exception cref="Exception">訊息處理過程中的任何異常</exception>
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
                        messageCount++;
                        
                        // 訊息數量管理：超過限制時清除舊訊息
                        ManageMessageBoxContent();
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

        /// <summary>
        /// 檢查收到的主題是否為訂閱的主題
        /// </summary>
        /// <param name="topic">接收到的主題名稱</param>
        /// <returns>true=是訂閱的主題，false=不是</returns>
        /// <remarks>
        /// 用於過濾訊息，僅顯示使用者訂閱的主題訊息
        /// 支援精確匹配（不支援萬用字元）
        /// </remarks>
        private bool IsSubscribedTopic(string topic)
        {
            return !string.IsNullOrEmpty(currentSubscribedTopic) &&
                   topic == currentSubscribedTopic;
        }

        /// <summary>
        /// 記錄接收到的訊息
        /// </summary>
        /// <param name="topic">訊息主題</param>
        /// <param name="payload">訊息內容</param>
        /// <remarks>
        /// 訊息格式：[HH:mm:ss] 訊息內容
        /// 訊息會自動捲動到最新位置
        /// </remarks>
        private void LogReceivedMessage(string topic, string payload)
        {
            string message = $"[{DateTime.Now:HH:mm:ss}] {payload}\r\n";
            AppendText(richTextBox_Sub_topic1, message);
        }

        /// <summary>
        /// 管理訊息框內容，防止訊息過多
        /// </summary>
        /// <remarks>
        /// 當訊息行數超過 <see cref="MqttConfig.MaxMessageLines"/> 時：
        /// 1. 清除前半部分的舊訊息
        /// 2. 保留後半部分的新訊息
        /// 3. 重置訊息計數器
        /// </remarks>
        private void ManageMessageBoxContent()
        {
            if (richTextBox_Sub_topic1.Lines.Length > MqttConfig.MaxMessageLines)
            {
                // 保留最新的一半訊息
                var lines = richTextBox_Sub_topic1.Lines;
                var keepLines = lines.Skip(lines.Length / 2).ToArray();
                richTextBox_Sub_topic1.Lines = keepLines;
                messageCount = keepLines.Length;
            }
        }

        /// <summary>
        /// 處理訊息接收錯誤
        /// </summary>
        /// <param name="ex">異常物件</param>
        /// <remarks>
        /// 處理接收或顯示訊息時的異常
        /// </remarks>
        private void HandleMessageReceiveError(Exception ex)
        {
            MessageBox.Show(
                $"接收訊息錯誤：{ex.Message}",
                "系統錯誤",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        #endregion

        #region 發布訊息

        /// <summary>
        /// 設定發布主題按鈕點擊事件處理
        /// </summary>
        /// <remarks>
        /// 執行流程：
        /// 1. 驗證發布主題輸入
        /// 2. 儲存發布主題
        /// 3. 顯示設定成功訊息
        /// 
        /// 必須先設定發布主題，才能使用 LED 開關功能
        /// </remarks>
        private void Pub_bt1_Click(object? sender, EventArgs e)
        {
            if (!ValidatePublishTopicInput())
                return;

            currentPublishTopic = pub_topic1.Text.Trim();
            MessageBox.Show(
                $"已設定發布主題：{currentPublishTopic}",
                "設定成功",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// 驗證發布主題輸入
        /// </summary>
        /// <returns>true=輸入有效，false=輸入無效</returns>
        /// <remarks>
        /// 檢查項目：
        /// - 主題名稱不可為空白
        /// - 主題名稱不可僅包含空白字元
        /// </remarks>
        private bool ValidatePublishTopicInput()
        {
            if (string.IsNullOrWhiteSpace(pub_topic1.Text))
            {
                MessageBox.Show(
                    "請輸入發布主題",
                    "輸入錯誤",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// LED 開關按鈕點擊事件處理
        /// </summary>
        /// <remarks>
        /// 執行流程：
        /// 1. 驗證是否已設定發布主題
        /// 2. 驗證 MQTT 連線狀態
        /// 3. 切換 LED 狀態（ON/OFF）
        /// 4. 發布控制訊息到 MQTT Broker
        /// 5. 更新按鈕顏色反映狀態
        /// 
        /// LED 狀態：

        /// - ON：紅色按鈕，發送 "ON" 訊息
        /// - OFF：灰色按鈕，發送 "OFF" 訊息
        /// </remarks>
        /// <exception cref="Exception">發布訊息失敗時拋出異常</exception>
        private async void LED_sw_Click(object? sender, EventArgs e)
        {
            // 驗證是否已設定發布主題
            if (string.IsNullOrWhiteSpace(currentPublishTopic))
            {
                MessageBox.Show(
                    "請先設定發布主題",
                    "設定錯誤",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // 驗證連線
            if (!ValidateConnection())
                return;

            try
            {
                // 切換 LED 狀態
                isLedOn = !isLedOn;

                // 發布訊息
                string message = isLedOn ? MessageText.LedOn : MessageText.LedOff;
                await PublishMessage(currentPublishTopic, message);

                // 更新按鈕顏色
                UpdateLedButtonColor(isLedOn);
            }
            catch (Exception ex)
            {
                HandlePublishError(ex);
                // 發布失敗時還原 LED 狀態
                isLedOn = !isLedOn;
                UpdateLedButtonColor(isLedOn);
            }
        }

        /// <summary>
        /// 發布訊息到 MQTT Broker
        /// </summary>
        /// <param name="topic">發布主題</param>
        /// <param name="payload">訊息內容</param>
        /// <remarks>
        /// 訊息配置：
        /// - QoS 等級：<see cref="MqttConfig.DefaultQoS"/>（至少一次）
        /// - Retain 旗標：<see cref="MqttConfig.RetainMessage"/>（不保留）
        /// - 編碼：UTF-8
        /// </remarks>
        /// <exception cref="Exception">發布失敗時拋出異常</exception>
        private async Task PublishMessage(string topic, string payload)
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithQualityOfServiceLevel(MqttConfig.DefaultQoS)
                .WithRetainFlag(MqttConfig.RetainMessage)
                .Build();

            await mqttClient!.PublishAsync(message, CancellationToken.None);
        }

        /// <summary>
        /// 更新 LED 按鈕顏色
        /// </summary>
        /// <param name="isOn">LED 狀態：true=開啟，false=關閉</param>
        /// <remarks>
        /// 顏色配置：
        /// - 開啟：<see cref="ColorConfig.LedOnColor"/>（紅色）
        /// - 關閉：<see cref="ColorConfig.LedOffColor"/>（灰色）
        /// </remarks>
        private void UpdateLedButtonColor(bool isOn)
        {
            LED_sw.BackColor = isOn ? ColorConfig.LedOnColor : ColorConfig.LedOffColor;
        }

        /// <summary>
        /// 處理發布錯誤
        /// </summary>
        /// <param name="ex">異常物件</param>
        /// <remarks>
        /// 處理發布訊息時可能發生的異常，例如：
        /// - 連線中斷
        /// - Broker 拒絕訊息
        /// - 網路錯誤
        /// </remarks>
        private void HandlePublishError(Exception ex)
        {
            MessageBox.Show(
                $"發布訊息錯誤：{ex.Message}",
                "系統錯誤",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        #endregion

        #region UI 輔助方法

        /// <summary>
        /// 在 RichTextBox 中追加文字並自動捲動
        /// </summary>
        /// <param name="rtb">目標 RichTextBox 控件</param>
        /// <param name="text">要追加的文字內容</param>
        /// <remarks>
        /// 功能：
        /// 1. 將文字追加到 RichTextBox 末尾
        /// 2. 自動捲動到最新內容
        /// 
        /// 使用情境：
        /// - 記錄訂閱訊息
        /// - 顯示接收到的 MQTT 訊息
        /// - 記錄系統日誌
        /// </remarks>
        private void AppendText(RichTextBox rtb, string text)
        {
            rtb.AppendText(text);
            rtb.ScrollToCaret();
        }

        #endregion

        #region 程式結束

        /// <summary>
        /// 退出按鈕點擊事件處理
        /// </summary>
        /// <remarks>
        /// 執行流程：
        /// 1. 清理所有 MQTT 資源
        /// 2. 關閉應用程式
        /// 
        /// 確保程式正常結束，避免資源洩漏
        /// </remarks>
        private async void Button_exit_Click(object? sender, EventArgs e)
        {
            await CleanupResources();
            Application.Exit();
        }

        /// <summary>
        /// 表單關閉事件處理
        /// </summary>
        /// <remarks>
        /// 當使用者點擊視窗關閉按鈕或使用 Alt+F4 時觸發
        /// 確保清理所有資源後才關閉程式
        /// </remarks>
        private async void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            await CleanupResources();
        }

        /// <summary>
        /// 清理程式資源
        /// </summary>
        /// <remarks>
        /// 清理項目：
        /// 1. 斷開 MQTT 連線（如果已連線）
        /// 2. 釋放 MQTT 客戶端資源
        /// 3. 處理任何清理時的異常（忽略錯誤）
        /// 
        /// 重要性：
        /// - 防止資源洩漏
        /// - 確保連線正常關閉
        /// - 避免殭屍連線
        /// </remarks>
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
                // 避免因為網路錯誤或 Broker 不可用而無法關閉程式
            }
        }

        #endregion

        /// <summary>
        /// GroupBox 訂閱區域進入事件（保留供未來使用）
        /// </summary>
        /// <remarks>
        /// 目前未實作任何功能
        /// 保留此方法以維持與設計器的相容性
        /// </remarks>
        private void groupBox_Subscribe_Enter(object sender, EventArgs e)
        {
            // 保留供未來擴展使用
        }
    }
}
