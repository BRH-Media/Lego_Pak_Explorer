using System;
using System.IO;

namespace TT_Games_Explorer.Common
{
    public static class Methods
    {
        /// <summary>
        /// Converts a byte count into a human-readable string
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="includeSpace"></param>
        /// <returns></returns>
        public static string FormatSize(long bytes, bool includeSpace = false)
        {
            string[] suffix =
            {
                "B", "KB", "MB", "GB", "TB"
            };

            int i;
            double dblSByte = bytes;

            for (i = 0; i < suffix.Length && bytes >= 1024; i++, bytes /= 1024)
                dblSByte = bytes / 1024.0;

            return includeSpace
                ? $"{dblSByte:0.##} {suffix[i]}"
                : $"{dblSByte:0.##}{suffix[i]}";
        }

        /// <summary>
        /// Reads the file, loads the bytes, loads a stream, reads the stream, then returns the uint reversed result
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static uint ReadFourCc(string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var num = new BinaryReader(fileStream).ReadUInt32();
            fileStream.Close();
            return ReverseBytes(num);
        }

        /// <summary>
        /// Reads the bytes provided, loads a stream, reads the steam, then returns the uint reversed result.
        /// </summary>
        /// <param name="fileBytes"></param>
        /// <returns></returns>
        public static uint ReadFourCc(byte[] fileBytes)
        {
            var memStream = new MemoryStream(fileBytes);
            var num = new BinaryReader(memStream).ReadUInt32();
            memStream.Close();
            return ReverseBytes(num);
        }

        /// <summary>
        /// Maps extensions to their ListView description
        /// </summary>
        public static string GetLegoFileType(string fileName)
        {
            try
            {
                var ext = Path.GetExtension(fileName);
                return Globals.LegoFileType[ext.ToLower()];
            }
            catch (Exception)
            {
                //ignore
            }

            //default
            return @"Unknown File";
        }

        public static int IntReverseBytes(int val)
        {
            var bytes = BitConverter.GetBytes(val);
            Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public static uint ReverseBytes(uint value) =>
            (uint)(((int)value & byte.MaxValue) << 24 | ((int)value & 65280) << 8) | (value & 16711680U) >> 8 |
            (value & 4278190080U) >> 24;
    }
}