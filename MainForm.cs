using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using JRPC_Client;
using ReaLTaiizor.Forms;
using XDevkit;
using Timer = System.Windows.Forms.Timer;

namespace GhostStudio
{
    public partial class MainForm : LostForm
    {
        #region Variables
        protected bool firstRun;
        protected Timer tempTimer, titleIdTimer;
        protected IXboxConsole jtag;
        #endregion

        public MainForm()
        {
            InitializeComponent();
            InitConnection();
        }

        private void InitConnection()
        {
            jtag.Connect(out jtag);
            if (jtag.IsConnected())
            {
                ConnectionStatusLabel.Text = "Connection Status: Connected";
                ConnectionStatusLabel.ForeColor = Color.LimeGreen;

                //string regexedMonitor = Regex.Replace(jtag.GetDMVersion(), @"\d+\.\d+\.(\d+)\.\d+", "$1");

                jtag.XNotify("Connected to GhostStudio!", XNotifyType.FlashingXboxConsole);
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
            uint boardTemp = jtag.GetTemperature(TemperatureType.MotherBoard);
            uint cpuTemp = jtag.GetTemperature(TemperatureType.CPU);
            uint gpuTemp = jtag.GetTemperature(TemperatureType.GPU);
            uint ramTemp = jtag.GetTemperature(TemperatureType.EDRAM);

            MoBoTempTextbox.Text = $"Motherboard Temperature: {boardTemp} °C";
            CpuTempTextbox.Text = $"CPU Temperature: {cpuTemp} °C";
            GpuTempTextbox.Text = $"GPU Temperature: {gpuTemp} °C";
            RamTempTextbox.Text = $"RAM Temperature: {ramTemp} °C";
        }

        private void TemperatureTimer(object sender, EventArgs e)
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
        #endregion

        #region Methods
        private void ShowMessageBox(string message, MessageBoxButtons button = MessageBoxButtons.OK) => MessageBox.Show(message, "GhostStudio", button);

        private void ConsoleNotConnected() => ShowMessageBox("Failed to connect to console! Please ensure that you have JRPC2.xex or Neighborhood is connected.");
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
