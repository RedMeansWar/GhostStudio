using System;

namespace GhostStudio.Utilities
{
    public class Blocks
    {
        #region Variables
        protected byte[] inputBytes;
        #endregion

        #region Constructor
        public Blocks(byte[] input)
        {
            if (!Bytes.IsEmpty(input))
            {
                inputBytes = input;
            }
            else
            {
                throw new Exception("Input array is empty.");
            }
        }
        #endregion

        #region Methods
        #endregion
    }
}
