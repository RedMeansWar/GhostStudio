using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GhostStudio.Constants
{
    public class Settings
    {
        public bool AutoConnectToConsole { get; set; } = false;
        public bool ShowTempsInFahrenheit { get; set; } = false;
    }

    public class SettingsManager
    {

    }
}
