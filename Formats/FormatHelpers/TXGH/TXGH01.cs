using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.TXGH
{
	public class TXGH01
	{
		protected byte[] fileData;

		protected int iPos;

		public int version;

		public TXGH01(byte[] fileData, int iPos)
		{
			this.fileData = fileData;
			this.iPos = iPos;
		}

		public virtual int Read(ref int referencecounter)
		{
			int num = BigEndianBitConverter.ToInt32(fileData, iPos);
			iPos += 4;
			ColoredConsole.WriteLine("{0:x8}   Number of Unknown: 0x{1:x2}", iPos, num);
			if (num != 0)
			{
				referencecounter++;
			}
			iPos += 4 * num;
			int num2 = BigEndianBitConverter.ToInt32(fileData, iPos);
			iPos += 4;
			ColoredConsole.WriteLine("{0:x8}   Number of Textures: 0x{1:x2}", iPos, num2);
			if (num2 != 0)
			{
				referencecounter++;
			}
			for (int i = 0; i < num2; i++)
			{
				ReadTextureMeta();
			}
			num = BigEndianBitConverter.ToInt32(fileData, iPos);
			iPos += 4;
			ColoredConsole.WriteLine("{0:x8}   Number of Unknown: 0x{1:x2}", iPos, num);
			iPos += 4 * num;
			int num3 = BigEndianBitConverter.ToInt32(fileData, iPos);
			iPos += 4;
			ColoredConsole.WriteLine("{0:x8}   Number of Cameras: 0x{1:x2}", iPos, num3);
			if (num != 0)
			{
				referencecounter++;
			}
			for (int i = 0; i < num3; i++)
			{
				ReadCam();
				referencecounter++;
			}
			num = BigEndianBitConverter.ToInt32(fileData, iPos);
			iPos += 4;
			ColoredConsole.WriteLine("{0:x8}   Number of Unknown: 0x{1:x2}", iPos, num);
			if (num != 0)
			{
				referencecounter++;
			}
			iPos += 2 * num;
			return iPos;
		}

		protected virtual void ReadCam()
		{
		}

		protected virtual void ReadTextureMeta()
		{
			iPos += 16;
			iPos += 4;
			iPos += 4;
			iPos += 41;
		}
	}
}
