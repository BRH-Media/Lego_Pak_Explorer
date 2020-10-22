using System;
using System.IO;

namespace TT_Games_Explorer.Formats.ExtractHelper
{
    public class EndianBinaryWriter : BinaryWriter
    {
        private static byte[] _tmp;

        public EndianBinaryWriter(Stream stream)
          : base(stream)
        {
        }

        public void Write(int value, Endianes endianes)
        {
            _tmp = BitConverter.GetBytes(value);
            if (endianes == Endianes.Little)
                Array.Reverse(_tmp);
            Write(_tmp);
        }

        public void Write(float value, Endianes endianes)
        {
            _tmp = BitConverter.GetBytes(value);
            if (endianes == Endianes.Little)
                Array.Reverse(_tmp);
            Write(_tmp);
        }

        public void Write(Half value, Endianes endianes)
        {
            _tmp = Half.GetBytes(value);
            if (endianes == Endianes.Little)
                Array.Reverse(_tmp);
            Write(_tmp);
        }

        public enum Endianes
        {
            Big,
            Little,
        }
    }
}