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
            this.iPos += 4;
            this.iPos += 4;
            int int32 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            ColoredConsole.WriteLine("{0:x8}   Number of LoD: 0x{1:x8}", (object)this.iPos, (object)int32);
            this.iPos += 4;
            for (int i = 0; i < int32; ++i)
                this.readHGOL(i);
            this.iPos += 4;
            return this.iPos;
        }

        private void readHGOL(int i)
        {
            if (this.fileData[this.iPos] != (byte)76 || this.fileData[this.iPos + 1] != (byte)79 || this.fileData[this.iPos + 2] != (byte)71 || this.fileData[this.iPos + 3] != (byte)72)
                return;
            this.iPos += 4;
            this.hgol = BigEndianBitConverter.ToInt32(this.fileData, this.iPos) == 16 ? (HGOL01)new HGOL10(this.fileData, this.iPos) : throw new NotSupportedException(string.Format("HGOL Version {0:x2}", (object)BigEndianBitConverter.ToInt32(this.fileData, this.iPos)));
            this.iPos = this.hgol.Read();
        }
    }
}