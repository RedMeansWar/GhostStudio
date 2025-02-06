using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostStudio.Utilities
{
    internal class DataExtension
    {
        public static byte[] GetData(byte[] source, int offset, int count)
        {
            var dest = new byte[count];
            Buffer.BlockCopy(source, offset, dest, 0, count);
            return dest;
        }

        public static double ComputePercentage(long Current, long Total) { return (double)Current * 100 / Total; }

        public static string ByteArrayToString(byte[] ByteArray) { return BitConverter.ToString(ByteArray).Replace("-", string.Empty); }

        public static byte[] ReadAllBytes(Stream Stream)
        {
            Stream.Position = 0;

            var stream = Stream as MemoryStream;
            if (stream is not null)
            {
                return stream.ToArray();
            }

            using (var ms = new MemoryStream())
            {
                Stream.CopyTo(ms, 0x2000);
                ms.Flush();
                return ms.ToArray();
            }
        }

        public static DataTable CreateDataTable(List<DataColumn> columns, List<DataRow> rows = null)
        {
            DataTable dataTable = new();
            dataTable.Columns.AddRange(columns.ToArray());

            if (rows is not null)
            {
                foreach (DataRow row in rows)
                {
                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }
    }
}
