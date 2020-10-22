using System.Text;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.HGOL
{
	public class HGOL10 : HGOL01
	{
		public HGOL10(byte[] fileData, int iPos)
			: base(fileData, iPos)
		{
		}

		public override int Read()
		{
			iPos += 4;
			int num = BigEndianBitConverter.ToInt32(fileData, iPos);
			ColoredConsole.WriteLine("{0:x8}     Number of Bones: 0x{1:x8}", iPos, num);
			iPos += 4;
			for (int i = 0; i < num; i++)
			{
				short numberofchars = BigEndianBitConverter.ToInt16(fileData, iPos);
				iPos += 2;
				string text = readString(numberofchars);
				ColoredConsole.WriteLine("{0:x8}       Name: {1}", iPos, text);
				iPos += 64;
				iPos += 14;
			}
			iPos += 4;
			num = BigEndianBitConverter.ToInt32(fileData, iPos);
			ColoredConsole.WriteLine("{0:x8}     Number of Bones: 0x{1:x8}", iPos, num);
			iPos += 4;
			for (int i = 0; i < num; i++)
			{
				iPos += 64;
			}
			iPos += 4;
			num = BigEndianBitConverter.ToInt32(fileData, iPos);
			ColoredConsole.WriteLine("{0:x8}     Number of Bones: 0x{1:x8}", iPos, num);
			iPos += 4;
			for (int i = 0; i < num; i++)
			{
				iPos += 64;
			}
			return iPos;
		}

		private string readString(int numberofchars)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < numberofchars; i++)
			{
				if (fileData[iPos] != 0)
				{
					stringBuilder.Append((char)fileData[iPos]);
				}
				iPos++;
			}
			return stringBuilder.ToString();
		}
	}
}
