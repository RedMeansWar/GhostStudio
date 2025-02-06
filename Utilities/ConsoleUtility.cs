using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XDevkit;
using JRPC_Client;
using IniParser;
using IniParser.Model;
using GhostStudio.Constants;
using System.Data;


namespace GhostStudio.Utilities
{
    public class ConsoleUtility
    {
        #region Variables
        protected static Settings Settings;
        public static IXboxConsole jtag;
        #endregion

        #region Properties
        /// <summary>
        /// Get the local launch.ini file backup directory.
        /// </summary>
        public static string LocalLaunchBackupFileDirectory { get; } = Path.Combine(UserFolders.BackupFolder, @"Launch\");

        /// <summary>
        /// Get the local launch.ini file backup directory.
        /// </summary>
        public static string LocalLaunchFileDirectory { get; } = Path.Combine(UserFolders.AppData, @"Launch\");

        /// <summary>
        /// Get the local launch.ini file directory.
        /// </summary>
        public static string LocalLaunchBackupFilePath { get; } = Path.Combine(UserFolders.BackupFolder, "launch.ini");

        /// <summary>
        /// Get the local launch.ini backup file path.
        /// </summary>
        public static string LocalLaunchFilePath { get; } = Path.Combine(UserFolders.AppData, "launch.ini");

        /// <summary>
        /// Get the local launch.ini file path.
        /// </summary>
        public static string ConsoleLaunchFilePath { get;  }

        /// <summary>
        /// Get the launch.ini file path located on the console HDD
        /// </summary>
        public static string ConsoleLaunchIniPath { get; } = Path.Combine(Settings.RootHddDirectory, "launch.ini");

        public IniData LaunchIniData { get; set; }
        #endregion

        #region Methods
        private void LoadLaunchIniFileData()
        {
            try
            {
                jtag.ReceiveFile(LocalLaunchBackupFileDirectory, ConsoleLaunchIniPath);
                jtag.ReceiveFile(LocalLaunchFilePath, ConsoleLaunchIniPath);
                LaunchIniData = new FileIniDataParser().ReadFile(LocalLaunchFilePath);
            }
            catch (Exception ex)
            {
                MainForm.ShowMessageBox("Unable to load the launch.ini file. Edit the file path in Settings to your correct file location.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable FileSections { get; } = DataExtension.CreateDataTable(
        [
            new("Key", typeof(string)),
            new("Value", typeof(string)),
        ]);

        private void LoadLaunchFileSection(string sectionName)
        {
            FileSections.Rows.Clear();
            IniData launchFile = LaunchIniData;

            foreach (SectionData section in launchFile.Sections)
            {
                if (section.SectionName == sectionName)
                {
                    foreach (KeyData key in section.Keys)
                    {
                        _ = FileSections.Rows.Add(key.KeyName, key.Value);
                    }
                }
            }


        }
        #endregion
    }
}
