using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Endian;
using GhostStudio.Structures;

namespace GhostStudio.Utilities
{
    public class KeyVaultUtils
    {
        #region Variables
        protected string FileName = "kv.bin";
        protected List<KVInformation> keyVaults = new();
        #endregion

        #region Methods
        public bool VerifyKVSignature(byte[] kvBits)
        {
            if (kvBits.Length != 0x4000)
            {
                return false;
            }

            return false;
        }
        
        public void AddKeyVault(string binFile)
        {
            if (File.Exists(binFile))
            {
                Hashing.MD5(File.ReadAllBytes(binFile));
            }
            else
            {
                throw new Exception("File doesn't exists.");
            }
        }

        public void CheckKeyVault(KVInformation keyVaultInfo)
        {
            byte[] XMACsLogonKey = null;

            for (int i = 0; i < 2; i++)
            {
                //XMACsLogonKey = GetXMACSLogonKey(keyVaultInfo);
                keyVaultInfo.LastLog = "Getting XMACsLogonKey... Try " + i.ToString() + "/2";
                if (XMACsLogonKey is not null)
                {
                    break;
                }

                if (i >= 2 && XMACsLogonKey is null)
                {
                    keyVaultInfo.LastLog = "Failed to get XMACs... Skipping.";
                    return;
                }
            }

            //byte[] consoleId = 
        }
        #endregion

        #region Private Methods
        //private byte[] GetXMACSLogonKey(KVInformation keyVaultInfo)
        //{
        //    RSACryptoServiceProvider provider = LoadXMACs();
        //}

        //private RSACryptoServiceProvider LoadXMACs()
        //{
        //    EndianIO mainIo = new()
        //}
        #endregion
    }
}
