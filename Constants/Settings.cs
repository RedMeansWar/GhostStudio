using System;
using System.IO;
using Newtonsoft.Json;

namespace GhostStudio.Constants
{
    public class Settings
    {
        private static string _ghostFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GhostStudio");

        public bool AutoConnectToConsole { get; set; } = false;
        public bool ShowTempsInFahrenheit { get; set; } = false;
        public string GamesDirectory { get; set; } = "Hdd:\\Games\\";
        public string HomebrewDirectory { get; set; } = "Hdd:\\Homebrew\\";
        public string PluginDirectory { get; set; } = "Hdd:\\Plugins\\";
        public string RootHddDirectory { get; set; } = "Hdd:\\";
        public string BackupDirectory { get; set; } = Path.Combine(_ghostFolder, "Backup");
    }

    public static class SettingsManager
    {
        #region Variables
        private static string _ghostFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GhostStudio");
        private static string _settingsFile = Path.Combine(_ghostFolder, "settings.json");
        private static Settings _settings;
        #endregion

        #region Public Variables
        public static string GhostStudioFolder => _ghostFolder;
        public static string GhostSettingsFile => _settingsFile;
        #endregion

        public static Settings Settings => _settings;

        #region Methods
        public static void SaveSettings()
        {
            try
            {
                if (!Directory.Exists(_ghostFolder))
                {
                    Directory.CreateDirectory(_ghostFolder);
                }

                string json = JsonConvert.SerializeObject(_settings, Formatting.Indented);
                File.WriteAllText(_settingsFile, json);

                Console.WriteLine("Saved Settings");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving settings: {ex.Message}\n{ex.StackTrace}");
            }
        }

        public static void LoadSettings()
        {
            try
            {
                if (!File.Exists(_settingsFile))
                {
                    Console.WriteLine("Settings file not found. Loading default settings.");
                    LoadDefaultSettings();
                    return;
                }

                string json = File.ReadAllText(_settingsFile);
                _settings = JsonConvert.DeserializeObject<Settings>(json) ?? new Settings();

                Console.WriteLine("Loaded Settings");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading settings: {ex.Message}\n{ex.StackTrace}");
                LoadDefaultSettings();
            }
        }

        public static void LoadDefaultSettings()
        {
            try
            {
                _settings = new Settings
                {
                    AutoConnectToConsole = true,
                    ShowTempsInFahrenheit = false,
                    GamesDirectory = "Hdd:\\Games\\",
                    HomebrewDirectory = "Hdd:\\Homebrew\\",
                    PluginDirectory = "Hdd:\\Plugins\\",
                    RootHddDirectory = "Hdd:\\",
                    BackupDirectory = Path.Combine(_ghostFolder, "Backups")
                };

                SaveSettings();

                Console.WriteLine("Loaded Default Settings");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading default settings: {ex.Message}\n{ex.StackTrace}");
            }
        }

        public static bool SettingsFileExists()
        {
            return File.Exists(_settingsFile);
        }
        #endregion
    }
}
