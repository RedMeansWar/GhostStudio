using System;

namespace GhostStudio
{
    public static class Extensions
    {
        public static int ConvertToFahrenheit(this uint Temperature)
        {
            int tempInF = (int)Math.Round(Temperature * 9.0 / 5.0 + 32.0);
            return tempInF;
        }
    }
}
