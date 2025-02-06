using System;
using System.IO;
using XDevkit;
using JRPC_Client;

namespace GhostStudio.Utilities
{
    public class GtaUtils
    {
        #region Variables
        protected bool stop;
        protected string drive = "HDD";
        public string profileId;
        protected string x, z, save;
        protected string path = Path.GetTempPath() + "PrologoueSkip\\";
        protected IXboxConsole jtag;
        #endregion

        #region Methods
        public void SkipPrologue()
        {
            if (jtag.IsConnected())
            {
                try
                {
                    if (profileId == "")
                    {
                        return;
                    }

                    if (profileId == "0000000000000000")
                    {
                        stop = false;
                        return;
                    }

                    using (BinaryWriter writer = new(new FileStream(save, FileMode.Open)))
                    {
                        writer.BaseStream.Position = 881L;
                        byte[] array = HexToArray(profileId);
                        writer.BaseStream.Write(array, 0, array.Length);
                        writer.Close();
                    }

                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: ", ex.Message, ex.StackTrace);
                }
            }
        }
        #endregion

        #region Private Methods
        private byte[] HexToArray(string hex)
        {
            int length = hex.Length;
            byte[] array = new byte[length / 2];
            
            for (int i = 0; i < length; i += 2)
            {
                array[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return array;
        }
        #endregion
    }
}
