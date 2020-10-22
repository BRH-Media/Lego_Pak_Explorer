using System;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.TXGH
{
	public class TXGH0A : TXGH08
	{
		public TXGH0A(byte[] fileData, int iPos)
			: base(fileData, iPos)
		{
		}

		public override int Read(ref int referencecounter)
		{
			iPos += 4;
			int num = BigEndianBitConverter.ToInt32(fileData, iPos);
			iPos += 4;
			for (int i = 0; i < num; i++)
			{
				if (BigEndianBitConverter.ToInt32(fileData, iPos) != 0)
				{
					throw new NotSupportedException("TXGH VTOR 1");
				}
				iPos += 4;
			}
			iPos += 4;
			num = BigEndianBitConverter.ToInt32(fileData, iPos);
			iPos += 4;
			ColoredConsole.WriteLine("{0:x8}   Number of Textures: 0x{1:x2}", iPos, num);
			for (int i = 0; i < num; i++)
			{
				iPos += 16;
				int num2 = BigEndianBitConverter.ToInt32(fileData, iPos);
				iPos += 4;
				iPos += num2;
				iPos += 4;
				if (num2 != 0)
				{
					referencecounter++;
				}
			}
			iPos += 4;
			referencecounter += BigEndianBitConverter.ToInt32(fileData, iPos);
			iPos += 4;
			return iPos;
		}
	}
}
