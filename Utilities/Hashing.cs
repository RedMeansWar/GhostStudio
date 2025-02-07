using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GhostStudio.Utilities
{
    public class Hashing
    {
        #region Methods
        public static string MD5(byte[] input)
        {
            string result = "";
            MD5CryptoServiceProvider _MD5 = new();
            _MD5.ComputeHash(input);

            for (int i = 0; i < input.Length; i++)
            {
                result += _MD5.Hash[i].ToString("X2");
            }

            return result;
        }

        public static string MD5(string input)
        {
            return MD5(Encoding.ASCII.GetBytes(input));
        }

        public static string MD5(FileStream input)
        {
            return MD5(input);
        }

        public static string GetExecutingFileHash()
        {
            return MD5(GetSelfBytes());
        }

        public static void RC4(ref byte[] data, byte[] key)
        {
            byte num;
            int num2;

            byte[] buffer = new byte[0x100];
            byte[] buffer2 = new byte[0x100];

            for (num2 = 0; num2 < 0x100; num2++)
            {
                buffer[num2] = (byte)num2;
                buffer2[num2] = key[num2 % key.GetLength(0)];
            }

            int index = 0;

            for (num2 = 0; num2 < 0x100; num2++)
            {
                index = ((index + buffer[num2]) + buffer2[num2]) % 0x100;
                num = buffer[num2];

                buffer[num2] = buffer[index];
                buffer[index] = num;
            }
            
            num2 = index = 0;

            for (int i = 0; i < data.GetLength(0); i++)
            {
                num2 = (num2 + 1) % 0x100;
                index = (index + buffer[num2]) % 0x100;
                num = buffer[num2];

                buffer[num2] = buffer[index];
                buffer[index] = num;

                int num5 = (buffer[num2] + buffer[index]) % 0x100;
                data[i] = (byte)(data[i] ^ buffer[num5]);
            }
        }
        #endregion

        #region Private Methodss
        private static byte[] GetSelfBytes()
        {
            string path = Application.ExecutablePath;
            FileStream running = File.OpenRead(path);

            byte[] exeBytes = new byte[running.Length];
            running.Read(exeBytes, 0, exeBytes.Length);
            running.Close();

            return exeBytes;
        }
        #endregion
    }
}
