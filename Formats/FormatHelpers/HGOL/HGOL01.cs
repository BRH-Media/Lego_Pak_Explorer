using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.HGOL
{
	public class HGOL01
	{
		protected byte[] fileData;

		protected int iPos;

		public int version;

		public HGOL01(byte[] fileData, int iPos)
		{
			this.fileData = fileData;
			this.iPos = iPos;
			version = BigEndianBitConverter.ToInt32(fileData, iPos);
			this.iPos += 4;
			ColoredConsole.WriteLineInfo("{0:x8}   HGOL Version 0x{1:x2}", iPos, version);
		}

		public virtual int Read()
		{
			return iPos;
		}
	}
}
