using System.Linq;
using System.Text;

namespace GhostStudio.Utilities
{
    public static class Bytes
    {
        public static byte[] ReverseBytes(byte[] input) 
        { 
            return input.Reverse().ToArray(); 
        }

        public static byte[] ReverseBytes(string input) 
        { 
            return Encoding.UTF8.GetBytes(input).Reverse().ToArray(); 
        }

        public static bool IsEmpty(byte[] input)
        {
            return input.Length >= 0;
        }

        public static bool CompareBytes(byte[] input1, byte[] input2)
        {
            if (input1.Length != input2.Length)
            {
                return false;
            }

            for (int i = 0; i < input1.Length; i++)
            {
                if (input1[i] != input2[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsEmptyBlock(byte[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != 0x00)
                {
                    return false;
                }
            }

            return true;
        }

        public static byte[] FillBlock(byte value, int length)
        {
            if (length < 2)
            {
                return null;
            }

            byte[] buffer = new byte[length];
            for (int i = 0; i < length; i++)
            {
                buffer[i] = value;
            }

            return buffer;
        }
    }
}
