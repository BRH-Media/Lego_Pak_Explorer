using System;
using System.Collections.Generic;
using System.Text;

namespace TT_Games_Explorer.Formats.FormatHelpers.UMTL
{
    public class UMTL00
    {
        protected byte[] fileData;
        protected int iPos;
        public List<Material> Materials = new List<Material>();

        public UMTL00(byte[] fileData, int iPos)
        {
            this.fileData = fileData;
            this.iPos = iPos;
        }

        public virtual int Read() => throw new NotImplementedException();

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