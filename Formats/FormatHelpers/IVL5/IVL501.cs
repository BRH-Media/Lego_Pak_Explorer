using System;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.FormatHelpers.HGOL;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.IVL5
{
    public class IVL501
    {
        protected byte[] fileData;
        protected int iPos;
        private HGOL01 hgol;
        public int version;

        public IVL501(byte[] fileData, int iPos)
        {
            this.fileData = fileData;
            this.iPos = iPos;
        }

        public virtual int Read()
        {
            iPos += 4;
            iPos += 4;
            var int32 = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}   Number of LoD: 0x{1:x8}", (object)iPos, (object)int32);
            iPos += 4;
            for (var i = 0; i < int32; ++i)
                readHGOL(i);
            iPos += 4;
            return iPos;
        }

        private void readHGOL(int i)
        {
            if (fileData[iPos] != (byte)76 || fileData[iPos + 1] != (byte)79 || fileData[iPos + 2] != (byte)71 || fileData[iPos + 3] != (byte)72)
                return;
            iPos += 4;
            hgol = BigEndianBitConverter.ToInt32(fileData, iPos) == 16 ? (HGOL01)new HGOL10(fileData, iPos) : throw new NotSupportedException(
                $"HGOL Version {(object)BigEndianBitConverter.ToInt32(fileData, iPos):x2}");
            iPos = hgol.Read();
        }
    }
}