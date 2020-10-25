using System.Collections.Generic;
using System.IO;
using System.Text;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.META
{
    public class META0C
    {
        protected byte[] fileData;
        protected int iPos;

        public META0C(byte[] fileData, int iPos)
        {
            this.fileData = fileData;
            this.iPos = iPos;
        }

        public virtual int Read(string directoryname)
        {
            List<string> stringList = new List<string>();
            int int32_1 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            ColoredConsole.WriteLine("{0:x8}  Number of File Names: {1:x8}", (object)this.iPos, (object)int32_1);
            this.iPos += 4;
            for (int index = 0; index < int32_1; ++index)
            {
                int int32_2 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                string str = this.readString(int32_2);
                stringList.Add(str);
                ColoredConsole.WriteLine("{0:x8}    Name: {1}", (object)this.iPos, (object)str);
            }
            for (int index = 0; index < stringList.Count; ++index)
            {
                int ddsFileSize = DdsHelper.CalculateDdsFileSize(this.iPos, this.fileData);
                ColoredConsole.WriteLine("{0:x8}    Size: {1:x8}", (object)this.iPos, (object)ddsFileSize);
                FileStream fileStream = File.OpenWrite(directoryname + "\\" + string.Format("{0:0000}_", (object)index) + Path.GetFileNameWithoutExtension(stringList[index]) + ".dds");
                fileStream.Write(this.fileData, this.iPos, ddsFileSize);
                fileStream.Close();
                this.iPos += ddsFileSize;
            }
            return this.iPos;
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