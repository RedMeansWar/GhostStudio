using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using GhostStudio.Constants;
using JRPC_Client;
using ReaLTaiizor.Forms;
using XDevkit;
using Timer = System.Windows.Forms.Timer;

namespace GhostStudio
{
    public partial class MainForm : LostForm
    {
        #region Variables
        protected bool firstRun, isDevOrRGLoader;
        protected uint xamFreeMemory = 0x81AA1D30;
        protected Timer tempTimer, titleIdTimer;
        protected IXboxConsole jtag;
        #endregion
        
        public MainForm()
        {
            InitializeComponent();
            Thread.Sleep(1000);
            InitStartup();
        }

        private void InitConnection()
        {
            jtag.Connect(out jtag);
            if (jtag.IsConnected())
            {
                ConnectionStatusLabel.Text = "Connection Status: Connected";
                ConnectionStatusLabel.ForeColor = Color.LimeGreen;

                //string regexedMonitor = Regex.Replace(jtag.GetDMVersion(), @"\d+\.\d+\.(\d+)\.\d+", "$1");

                jtag.XNotify("Connected to GhostStudio!", XNotifyType.FlashingHappyFace);
                CpuKeyTextbox.Text = $"CPU Key: {jtag.GetCPUKey()}";
                DashboardTextbox.Text = $"Dashboard: {jtag.GetKernalVersion().ToString()}";

                switch (jtag.ConsoleType)
                {
                    case XDevkit.XboxConsoleType.DevelopmentKit: ConsoleTypeTextbox.Text = $"Console Type: Development Kit"; break;
                    case XDevkit.XboxConsoleType.TestKit: ConsoleTypeTextbox.Text = $"Console Type: Test Kit"; break;
                    case XDevkit.XboxConsoleType.ReviewerKit: ConsoleTypeTextbox.Text = $"Console Type: Reviewer Kit"; break;
                }

                ConsoleNameTextbox.Text = $"Console Name: {jtag.Name}";
                IpAddressTextbox.Text = $"IP Address: {jtag.XboxIP()}";
                TitleIdTextbox.Text = $"Title ID: {jtag.XamGetCurrentTitleId()}";

                InitTemperatureTimer();
                InitTitleIdTimer();
            }
            else
            {
                ConnectionStatusLabel.Text = "Connection Status: Disconnect";
                ConnectionStatusLabel.ForeColor = Color.Red;
                ConsoleNotConnected();
            }
        }

        #region Timers
        private void InitTemperatureTimer()
        {
            tempTimer = new();
            tempTimer.Interval = 750;
            tempTimer.Tick += TemperatureTimer;
            tempTimer.Start();
        }

        private void InitTitleIdTimer()
        {
            titleIdTimer = new();
            titleIdTimer.Interval = 750;
            titleIdTimer.Tick += TitleIdTimer;
            titleIdTimer.Start();
        }

        private void TitleIdTimer(object sender, EventArgs e)
        {
            try
            {
                TitleIdTextbox.Text = $"Title ID: {jtag.XamGetCurrentTitleId()}";
                if (jtag.XamGetCurrentTitleId().ToString() == "0")
                {
                    TitleIdTextbox.Text = "Title ID: None";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\nTrace: {ex.StackTrace}");
            }
        }

        private void TemperatureTimer(object sender, EventArgs e)
        {
            uint boardTemp = jtag.GetTemperature(TemperatureType.MotherBoard);
            uint cpuTemp = jtag.GetTemperature(TemperatureType.CPU);
            uint gpuTemp = jtag.GetTemperature(TemperatureType.GPU);
            uint ramTemp = jtag.GetTemperature(TemperatureType.EDRAM);

            if (firstRun)
            {
                SettingsManager.Settings.ShowTempsInFahrenheit = false;
            }

            MoBoTempTextbox.Text = SettingsManager.Settings.ShowTempsInFahrenheit ? $"Motherboard Temperature: {boardTemp.ConvertToFahrenheit()} °F" : $"Motherboard Temperature: {boardTemp} °C";
            CpuTempTextbox.Text = SettingsManager.Settings.ShowTempsInFahrenheit ? $"CPU Temperature: {cpuTemp.ConvertToFahrenheit()} °F" : $"CPU Temperature: {cpuTemp} °C";
            GpuTempTextbox.Text = SettingsManager.Settings.ShowTempsInFahrenheit ? $"GPU Temperature: {gpuTemp.ConvertToFahrenheit()} °F" : $"GPU Temperature: {gpuTemp} °C";
            RamTempTextbox.Text = SettingsManager.Settings.ShowTempsInFahrenheit ? $"RAM Temperature: {ramTemp.ConvertToFahrenheit()} °F" : $"RAM Temperature: {ramTemp} °C";
        }
        #endregion

        #region Methods
        public static void ShowMessageBox(string message, MessageBoxButtons button = MessageBoxButtons.OK, MessageBoxIcon Icon = MessageBoxIcon.None) => MessageBox.Show(message, "GhostStudio", button);

        private void ConsoleNotConnected() => ShowMessageBox("Failed to connect to console! Please ensure that you have JRPC2.xex or Neighborhood is connected.");

        private void InitStartup()
        {
            SettingsManager.LoadSettings();

            if (!SettingsManager.SettingsFileExists() && firstRun)
            {
                ShowMessageBox("Welcome to GhostStudio! Lets set you up for the first time.");

                SettingsManager.LoadDefaultSettings();
                SettingsManager.SaveSettings();

                firstRun = false;
            }

            if (!firstRun && SettingsManager.SettingsFileExists() && SettingsManager.Settings.AutoConnectToConsole)
            {
                InitConnection();
            }
        }

        private uint XAMFreeMemory()
        {
            if (jtag.IsConnected() && jtag.ReadUInt32(xamFreeMemory) != 0)
            {
                xamFreeMemory = 0x81D48680;
                isDevOrRGLoader = true;
            }

            return xamFreeMemory;
        }

        private uint LocateXUserAwardAvatarAssets()
        {
            byte[] targetSequence = { 0x60, 0x84, 0x00, 0x71 };
            byte[] bytes = jtag.GetMemory(0x82000000, 0x3000000);

            for (int i = 0; i <= bytes.Length - targetSequence.Length; i++)
            {
                bool match = true;
                for (int j = 0; j < targetSequence.Length; j++)
                {
                    if (bytes[i + j] != targetSequence[j])
                    {
                        match = false;
                        break;
                    }
                }

                uint address = 0x82000000 + (uint)i;
                if (match)
                {
                    uint address2 = address - 0x8;
                    byte[] testBytes = jtag.GetMemory(address2, 4);
                    byte[] target = { 0x38, 0xE0, 0x00, 0x08 };

                    if (testBytes.Length == target.Length && testBytes.SequenceEqual(target))
                    {
                        return address;
                    }
                }
            }

            return 0;
        }

        private uint LocateXUserWriteAchievements()
        {
            byte[] targetSequence = { 0x60, 0x84, 0x00, 0x08 };
            byte[] bytes = jtag.GetMemory(0x82000000, 0x3000000);

            for (int i = 0; i <= bytes.Length - targetSequence.Length; i++)
            {
                bool match = true;
                for (int j = 0; j < targetSequence.Length; j++)
                {
                    if (bytes[i + j] != targetSequence[j])
                    {
                        match = false;
                        break;
                    }
                }

                uint address = 0x82000000 + (uint)i;
                if (match)
                {
                    uint address2 = address - 0x8;
                    byte[] testBytes = jtag.GetMemory(address2, 4);
                    byte[] target = { 0x38, 0xE0, 0x00, 0x08 };

                    if (testBytes.Length == target.Length && testBytes.SequenceEqual(target))
                    {
                        return address;
                    }
                }
            }

            return 0;
        }

        private void AwardAvatarAsset(uint avatarAssestCallAddr, uint assetIdPtr, uint overlappedPtr, int awardCount)
        {
            jtag.WriteUInt32(overlappedPtr, 0);
            for (int i = 0; i <  awardCount; i++)
            {
                jtag.WriteUInt64(assetIdPtr, (ulong)i);
                jtag.CallVoid(avatarAssestCallAddr, 1, assetIdPtr, overlappedPtr);

                while (jtag.ReadUInt32(overlappedPtr) != 0)
                {
                    Thread.Sleep(30);
                }
            }

            Thread.Sleep(100);
        }

        private void AwardAchievements(uint achievementCallAddr, uint achievementIdPtr, uint overlappedPtr, int achievementCount)
        {
            jtag.WriteUInt32(overlappedPtr, 0);
            for (int i = 0; i < achievementCount; i++)
            {
                jtag.WriteUInt64(achievementIdPtr, (ulong)i);
                jtag.CallVoid(achievementCallAddr, 1, achievementIdPtr, overlappedPtr);

                while (jtag.ReadUInt32(overlappedPtr) != 0)
                {
                    Thread.Sleep(30);
                }
            }

            Thread.Sleep(100);
        }
        #endregion

        #region Buttons
        private void ConnectToConsoleButton_Click(object sender, EventArgs e)
        {
            InitConnection();
            ConnectToConsoleButton.Text = "Reconnect to Console";

            if (ConnectToConsoleButton.Text == "Reconnect To Console")
            {
                jtag.Reconnect();
                return;
            }
        }

        private void RestartColdButton_Click(object sender, EventArgs e)
        {
            if (jtag.IsConnected())
            {
                jtag.RebootConsole(RebootFlag.Cold);
                return;
            }

            ConsoleNotConnected();
        }

        private void ShutdownButton_Click(object sender, EventArgs e)
        {
            if (jtag.IsConnected())
            {
                jtag.ShutDownConsole();
                return;
            }

            ConsoleNotConnected();
        }

        private void TakeScreenshotButton_Click(object sender, EventArgs e)
        {
            ShowMessageBox("This feature has been implemented yet!");
        }

        private void OpenTrayButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (jtag.IsConnected())
                {
                    jtag.Shortcut(XboxShortcuts.OpenTray);
                    return;
                }

                ConsoleNotConnected();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.StackTrace);
            }
        }

        private void CloseTrayButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (jtag.IsConnected())
                {
                    jtag.Shortcut(XboxShortcuts.CloseTray);
                    return;
                }

                ConsoleNotConnected();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.StackTrace);
            }
        }

        private void UnlockAllAvatarAwardsButton_Click(object sender, EventArgs e)
        {
            if (jtag.IsConnected())
            {
                try
                {
                    uint callAddress = LocateXUserAwardAvatarAssets();
                    uint freeMemory = XAMFreeMemory() + 0x20;

                    if (callAddress != 0)
                    {
                        AwardAvatarAsset(callAddress - 0x24, freeMemory, freeMemory + 8, 35);
                    }
                    else
                    {
                        ShowMessageBox("Failed to find the function address! Check if the game has avatar awards.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: ", ex.Message, ex.StackTrace);
                }

                return;
            }

            ConsoleNotConnected();
        }

        private void UnlockAllAchievementsButton_Click(object sender, EventArgs e)
        {
            if (jtag.IsConnected())
            {
                try
                {
                    uint callAddress = LocateXUserWriteAchievements();
                    uint freeMemory = XAMFreeMemory();

                    if (callAddress != 0)
                    {
                        AwardAchievements(callAddress - 0x24, freeMemory, freeMemory + 8, 101);
                    }
                    else
                    {
                        ShowMessageBox("Failed to find the function address!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: ", ex.Message, ex.StackTrace);
                }

                return;
            }

            ConsoleNotConnected();
        }

        private void RestartWarmButton_Click(object sender, EventArgs e)
        {
            if (jtag.IsConnected())
            {
                jtag.RebootConsole(RebootFlag.Warm);
                return;
            }

            ConsoleNotConnected();
        }
        

        #endregion
    }
}
