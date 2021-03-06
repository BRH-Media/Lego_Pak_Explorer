﻿using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.TXGH
{
    public class TXGH06 : TXGH05
    {
        public TXGH06(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
        }

        public override int Read(ref int referencecounter)
        {
            var int32_1 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Unknown: 0x{1:x2}", (object)iPos, (object)int32_1);
            if (int32_1 != 0)
                ++referencecounter;
            iPos += 4 * int32_1;
            var int32_2 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Textures: 0x{1:x2}", (object)iPos, (object)int32_2);
            if (int32_2 != 0)
                ++referencecounter;
            for (var index = 0; index < int32_2; ++index)
            {
                ReadTextureMeta();
                ++referencecounter;
            }
            var int32_3 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Unknown: 0x{1:x2}", (object)iPos, (object)int32_3);
            if (int32_3 != 0)
                ++referencecounter;
            iPos += 4 * int32_3;
            var int32_4 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Cameras: 0x{1:x2}", (object)iPos, (object)int32_4);
            if (int32_4 != 0)
                ++referencecounter;
            for (var index = 0; index < int32_4; ++index)
                ReadCam();
            var int32_5 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            ColoredConsole.WriteLine("{0:x8}   Number of Unknown: 0x{1:x2}", (object)iPos, (object)int32_5);
            if (int32_5 != 0)
                ++referencecounter;
            iPos += 2 * int32_5;
            return iPos;
        }

        protected override void ReadTextureMeta()
        {
            this.iPos += 16;
            this.iPos += 4;
            this.iPos += 4;
            this.iPos += 4;
            this.iPos += 4;
            this.iPos += 4;
            this.iPos += 17;
            var int32 = BigEndianBitConverter.ToInt32(fileData, this.iPos);
            this.iPos += 4;
            var iPos = this.iPos;
            var str = readString(int32);
            ColoredConsole.WriteLineInfo("{0:x8}     {2:0000} {1}", (object)iPos, (object)str, (object)Names.Count);
            Names.Add(str);
            this.iPos += 8;
        }
    }
}