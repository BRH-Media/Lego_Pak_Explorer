using System;

namespace TT_Games_Explorer.Formats.ExtractHelper
{
    public class BigEndianBitConverter
    {
        private static byte[] _tmp;

        public static int ToInt32(byte[] data, int i)
        {
            _tmp = new byte[4];
            _tmp[3] = data[i];
            _tmp[2] = data[i + 1];
            _tmp[1] = data[i + 2];
            _tmp[0] = data[i + 3];
            return BitConverter.ToInt32(_tmp, 0);
        }

        public static float ToSingle(byte[] data, int i)
        {
            _tmp = new byte[4];
            _tmp[3] = data[i];
            _tmp[2] = data[i + 1];
            _tmp[1] = data[i + 2];
            _tmp[0] = data[i + 3];
            return BitConverter.ToSingle(_tmp, 0);
        }

        public static Half ToHalf(byte[] data, int i)
        {
            _tmp = new byte[2];
            _tmp[1] = data[i];
            _tmp[0] = data[i + 1];
            return Half.ToHalf(_tmp, 0);
        }

        public static short ToInt16(byte[] data, int i)
        {
            _tmp = new byte[2];
            _tmp[1] = data[i];
            _tmp[0] = data[i + 1];
            return BitConverter.ToInt16(_tmp, 0);
        }

        public static ushort ToUInt16(byte[] data, int i)
        {
            _tmp = new byte[2];
            _tmp[1] = data[i];
            _tmp[0] = data[i + 1];
            return BitConverter.ToUInt16(_tmp, 0);
        }
    }
}