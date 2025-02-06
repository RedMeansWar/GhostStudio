using System;
using System.IO;

namespace GhostStudio.Constants
{
    public static class UserFolders
    {
        #region Private Variables
        private static Settings Settings;
        #endregion

        public static string AppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GhostStudio");
        public static string GhostFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GhostStudio");
        public static string BackupFolder = Settings.BackupDirectory ?? Path.Combine(GhostFolder, "Backups");
    }
}
