using System;
using System.Collections.Generic;
using System.Text;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.DISP
{
	public class DISP17 : DISP15
	{
		public DISP17(byte[] fileData, int iPos)
			: base(fileData, iPos)
		{
		}

		public override int Read()
		{
			int numberofchars = BigEndianBitConverter.ToInt32(fileData, iPos);
			iPos += 4;
			string text = readString(numberofchars);
			iPos += 4;
			int num = BigEndianBitConverter.ToInt32(fileData, iPos);
			iPos += 4;
			List<int> list = new List<int>();
			for (int i = 0; i < num; i++)
			{
				short num2 = BigEndianBitConverter.ToInt16(fileData, iPos);
				iPos += 2;
				int num3 = BigEndianBitConverter.ToInt32(fileData, iPos);
				iPos += 4;
				list.Add(num3);
				if (num2 == -19712)
				{
					ColoredConsole.WriteLineDebug("{0} --> {1}", i, num3);
				}
			}
			iPos += 4;
			int num4 = BigEndianBitConverter.ToInt32(fileData, iPos);
			iPos += 4;
			List<List<int>> list2 = new List<List<int>>();
			for (int i = 0; i < num4; i++)
			{
				List<int> list3 = new List<int>();
				short num5 = BigEndianBitConverter.ToInt16(fileData, iPos);
				iPos += 2;
				int num6 = BigEndianBitConverter.ToInt32(fileData, iPos);
				iPos += 4;
				for (int j = 0; j < num6; j++)
				{
					int num7 = BigEndianBitConverter.ToInt32(fileData, iPos);
					iPos += 4;
				}
				int num8 = BigEndianBitConverter.ToInt32(fileData, iPos);
				iPos += 4;
				for (int j = 0; j < num8; j++)
				{
					int index = BigEndianBitConverter.ToInt32(fileData, iPos);
					iPos += 4;
					list3.Add(list[index]);
				}
				list2.Add(list3);
			}
			iPos += 4;
			int num9 = BigEndianBitConverter.ToInt32(fileData, iPos);
			iPos += 4;
			iPos += 2 * num9;
			iPos += 4;
			int num10 = BigEndianBitConverter.ToInt32(fileData, iPos);
			iPos += 4;
			iPos += 4 * num10;
			iPos += 4;
			int num11 = BigEndianBitConverter.ToInt32(fileData, iPos);
			iPos += 4;
			for (int i = 0; i < num11; i++)
			{
				int num12 = BigEndianBitConverter.ToInt32(fileData, iPos);
				iPos += 4;
				string text2 = readStringAt(num12 + 1443);
				double num13 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos), 4);
				double num14 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 4), 4);
				double num15 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 8), 4);
				double num16 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 16), 4);
				double num17 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 20), 4);
				double num18 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 24), 4);
				double num19 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 32), 4);
				double num20 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 36), 4);
				double num21 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 40), 4);
				double num22 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 48) * 262f, 4);
				double num23 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 52) * 262f, 4);
				double num24 = Math.Round(BigEndianBitConverter.ToSingle(fileData, iPos + 56) * 262f, 4);
				iPos += 64;
				iPos += 16;
				iPos += 4;
				iPos += 16;
				iPos += 4;
				iPos += 80;
				int num25 = BigEndianBitConverter.ToInt32(fileData, iPos);
				iPos += 4;
				iPos += 4 * num25;
				int num26 = BigEndianBitConverter.ToInt32(fileData, iPos);
				iPos += 4;
				iPos += 4;
				iPos += 4;
			}
			iPos += 4;
			iPos += 5;
			iPos += 4;
			int num27 = BigEndianBitConverter.ToInt32(fileData, iPos);
			iPos += 4;
			for (int i = 0; i < num27; i++)
			{
				iPos += 16;
			}
			iPos += 4;
			int num28 = BigEndianBitConverter.ToInt32(fileData, iPos);
			iPos += 4;
			for (int i = 0; i < num28; i++)
			{
				iPos += 16;
			}
			iPos += 4;
			int num29 = BigEndianBitConverter.ToInt32(fileData, iPos);
			iPos += 4;
			for (int i = 0; i < num29; i++)
			{
				iPos += 44;
			}
			iPos += 4;
			ColoredConsole.WriteLineError("{0:x8}", iPos);
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

		private string readStringAt(int pos)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (fileData[pos] != 0)
			{
				stringBuilder.Append((char)fileData[pos]);
				pos++;
			}
			return stringBuilder.ToString();
		}
	}
}
