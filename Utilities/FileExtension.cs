using System.IO;

namespace GhostStudio.Utilities
{
    public static class FileExtension
    {
        public static byte[] ReadBytes(string location, int ordinal, int length)
        {
            byte[] tempBuffer = new byte[length];
            using (FileStream stream = new(location, FileMode.Open, FileAccess.Read))
            {
                stream.Read(tempBuffer, ordinal, length);
            }

            return tempBuffer;
        }
    }
}
