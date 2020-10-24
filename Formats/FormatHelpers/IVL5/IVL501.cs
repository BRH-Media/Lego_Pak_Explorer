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
            int num = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}   Number of LoD: 0x{1:x8}", iPos, num);
            iPos += 4;
            for (int i = 0; i < num; i++)
            {
                readHGOL(i);
            }
            iPos += 4;
            return iPos;
        }

        private void readHGOL(int i)
        {
            if (fileData[iPos] == 76 && fileData[iPos + 1] == 79 && fileData[iPos + 2] == 71 && fileData[iPos + 3] == 72)
            {
                iPos += 4;
                int num = BigEndianBitConverter.ToInt32(fileData, iPos);
                if (num != 16)
                {
                    throw new NotSupportedException($"HGOL Version {BigEndianBitConverter.ToInt32(fileData, iPos):x2}");
                }
                hgol = new HGOL10(fileData, iPos);
                iPos = hgol.Read();
            }
        }
    }
}