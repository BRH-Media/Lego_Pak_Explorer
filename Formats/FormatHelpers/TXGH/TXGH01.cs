using System.Collections.Generic;
using System.Text;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.TXGH
{
    public class TXGH01
    {
        protected byte[] fileData;
        protected int iPos;
        public List<string> Names = new List<string>();
        public int version;

        public TXGH01(byte[] fileData, int iPos)
        {
            this.fileData = fileData;
            this.iPos = iPos;
        }

        public virtual int Read(ref int referencecounter)
        {
            int int32_1 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Unknown: 0x{1:x2}", (object)this.iPos, (object)int32_1);
            if (int32_1 != 0)
                ++referencecounter;
            this.iPos += 4 * int32_1;
            int int32_2 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Textures: 0x{1:x2}", (object)this.iPos, (object)int32_2);
            if (int32_2 != 0)
                ++referencecounter;
            for (int index = 0; index < int32_2; ++index)
                this.ReadTextureMeta();
            int int32_3 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Unknown: 0x{1:x2}", (object)this.iPos, (object)int32_3);
            this.iPos += 4 * int32_3;
            int int32_4 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Cameras: 0x{1:x2}", (object)this.iPos, (object)int32_4);
            if (int32_3 != 0)
                ++referencecounter;
            for (int index = 0; index < int32_4; ++index)
            {
                this.ReadCam();
                ++referencecounter;
            }
            int int32_5 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Unknown: 0x{1:x2}", (object)this.iPos, (object)int32_5);
            if (int32_5 != 0)
                ++referencecounter;
            this.iPos += 2 * int32_5;
            return this.iPos;
        }

        protected virtual void ReadCam()
        {
        }

        protected virtual void ReadTextureMeta()
        {
            this.iPos += 16;
            this.iPos += 4;
            this.iPos += 4;
            this.iPos += 41;
        }

        protected string readString(int numberofchars)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < numberofchars; ++index)
            {
                if (this.fileData[this.iPos] != (byte)0)
                    stringBuilder.Append((char)this.fileData[this.iPos]);
                ++this.iPos;
            }
            return stringBuilder.ToString();
        }
    }
}