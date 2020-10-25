using System;
using System.Collections.Generic;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.FormatHelpers.Vertex;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.MESH
{
    public class MESH2F : MESH2E
    {
        public MESH2F(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
        }

        protected override Part ReadPart(ref int referencecounter)
        {
            Part part = new Part();
            int int32_1 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            ColoredConsole.WriteLine("{0:x8}     Number of Vertex Lists: 0x{1:x8}", (object)this.iPos, (object)int32_1);
            this.iPos += 4;
            int offset;
            for (int index = 0; index < int32_1; ++index)
            {
                ColoredConsole.WriteLine("{0:x8}       Vertex List 0x{1:x8}", (object)this.iPos, (object)index);
                part.VertexListReferences1.Add(this.GetVertexListReference(ref referencecounter, out offset));
            }
            int int32_2 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            ColoredConsole.WriteLine("{0:x8}     Unknown Number (should be 0x0): 0x{1:x8}", (object)this.iPos, (object)int32_2);
            this.iPos += 4;
            if (int32_2 != 0)
                part.VertexListReferences11 = new List<VertexListReference>();
            for (int index = 0; index < int32_2; ++index)
                part.VertexListReferences11.Add(this.GetVertexListReference(ref referencecounter, out offset));
            if (int32_2 != 0)
                ++referencecounter;
            part.IndexListReference1 = this.GetIndexListReference(ref referencecounter);
            part.OffsetIndices = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            ColoredConsole.WriteLine("{0:x8}     Offset Indices: 0x{1:x8}", (object)this.iPos, (object)part.OffsetIndices);
            this.iPos += 4;
            part.NumberIndices = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            ColoredConsole.WriteLine("{0:x8}     Number Indices: 0x{1:x8}", (object)this.iPos, (object)part.NumberIndices);
            this.iPos += 4;
            part.OffsetVertices = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            ColoredConsole.WriteLine("{0:x8}     Offset Vertices: 0x{1:x8}", (object)this.iPos, (object)part.OffsetVertices);
            this.iPos += 4;
            if (BigEndianBitConverter.ToInt16(this.fileData, this.iPos) != (short)0)
                throw new NotSupportedException("ReadPart Offset Vertices + 4");
            this.iPos += 2;
            part.NumberVertices = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            ColoredConsole.WriteLine("{0:x8}     Number Vertices: 0x{1:x8}", (object)this.iPos, (object)part.NumberVertices);
            this.iPos += 4;
            this.iPos += 4;
            int int32_3 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            if (int32_3 > 0)
            {
                ColoredConsole.Write("{0:x8}     ", (object)this.iPos);
                for (int index = 0; index < int32_3; ++index)
                {
                    ColoredConsole.Write("{0:x2} ", (object)this.fileData[this.iPos]);
                    ++this.iPos;
                }
                ColoredConsole.WriteLine();
                ++referencecounter;
            }
            int int32_4 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            this.iPos += 4;
            if (int32_4 != 0)
            {
                int num = this.ReadRelativePositionList((byte)0);
                referencecounter += num;
            }
            this.iPos += 4;
            this.iPos += 36;
            int num1 = (int)this.fileData[this.iPos + 3];
            ColoredConsole.WriteLine("{0:x8}     Number of Vertex Lists: 0x{1:x8} ???", (object)this.iPos, (object)num1);
            this.iPos += 4;
            if (num1 != 0)
            {
                ColoredConsole.WriteLine("{0:x8}       Vertex List 0x{1:x8}", (object)this.iPos, (object)0);
                part.VertexListReferences2.Add(this.GetVertexListReference(ref referencecounter, out offset));
                part.NumberVertices2 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                ColoredConsole.WriteLine("{0:x8}     Number Vertices: 0x{1:x8}", (object)this.iPos, (object)part.NumberVertices2);
                this.iPos += 4;
                part.IndexListReference2 = this.GetIndexListReference(ref referencecounter);
                part.OffsetVertices2 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                ColoredConsole.WriteLine("{0:x8}     Offset Vertices: 0x{1:x8}", (object)this.iPos, (object)part.OffsetVertices2);
                this.iPos += 4;
                ColoredConsole.WriteLine("{0:x8}     Number Indices: 0x{1:x8}", (object)this.iPos, (object)part.NumberIndices);
                part.OffsetIndices2 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                ColoredConsole.WriteLine("{0:x8}     Offset Indices: 0x{1:x8}", (object)this.iPos, (object)part.OffsetIndices2);
                this.iPos += 4;
                this.iPos += 4;
            }
            else
            {
                this.iPos += 4;
                this.iPos += 4;
                this.iPos += 4;
                this.iPos += 4;
                this.iPos += 4;
                this.iPos += 4;
                this.iPos += 4;
            }
            ++referencecounter;
            return part;
        }
    }
}