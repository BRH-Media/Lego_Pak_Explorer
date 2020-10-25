using System;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.FormatHelpers.Vertex;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.MESH
{
    public class MESHAF : MESHAA
    {
        public MESHAF(byte[] fileData, int iPos)
          : base(fileData, iPos)
        {
        }

        public override int Read(ref int referencecounter)
        {
            int int32 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            ColoredConsole.WriteLine("{0:x8}   Number of Parts: 0x{1:x8}", (object)this.iPos, (object)int32);
            this.iPos += 4;
            for (int index = 0; index < int32; ++index)
            {
                ColoredConsole.WriteLine("{0:x8}   Part 0x{1:x8}", (object)this.iPos, (object)index);
                this.Parts.Add(this.ReadPart(ref referencecounter));
            }
            return this.iPos;
        }

        protected override Part ReadPart(ref int referencecounter)
        {
            this.iPos += 4;
            Part part = new Part();
            int int32_1 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            ColoredConsole.WriteLine("{0:x8}     Number of Vertex Lists: 0x{1:x8}", (object)this.iPos, (object)int32_1);
            this.iPos += 4;
            for (int index = 0; index < int32_1; ++index)
            {
                ColoredConsole.WriteLine("{0:x8}       Vertex List 0x{1:x8}", (object)this.iPos, (object)index);
                int offset;
                VertexListReference vertexListReference = this.GetVertexListReference(ref referencecounter, out offset);
                part.VertexListReferences1.Add(vertexListReference);
                if (index == 0)
                    part.OffsetVertices = offset / this.Vertexlistsdictionary[vertexListReference.Reference].VertexSize;
                else
                    part.OffsetVertices2 = offset / this.Vertexlistsdictionary[vertexListReference.Reference].VertexSize;
            }
            this.iPos += 4;
            part.IndexListReference1 = this.GetIndexListReference(ref referencecounter);
            part.OffsetIndices = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            ColoredConsole.WriteLine("{0:x8}     Offset Indices: 0x{1:x8}", (object)this.iPos, (object)part.OffsetIndices);
            this.iPos += 4;
            part.NumberIndices = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            ColoredConsole.WriteLine("{0:x8}     Number Indices: 0x{1:x8}", (object)this.iPos, (object)part.NumberIndices);
            this.iPos += 4;
            int int32_2 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
            ColoredConsole.WriteLine("{0:x8}     Offset Vertices: 0x{1:x8}", (object)this.iPos, (object)int32_2);
            if (int32_2 != 0)
            {
                part.OffsetVertices = int32_2;
                part.OffsetVertices2 = int32_2;
            }
            else
            {
                if (part.OffsetVertices != 0)
                    ColoredConsole.WriteLine("{0:x8}       --> Calculated Offset1 Vertices: 0x{1:x8}", (object)this.iPos, (object)part.OffsetVertices);
                if (part.VertexListReferences1.Count > 1 && part.OffsetVertices2 != 0)
                    ColoredConsole.WriteLine("{0:x8}       --> Calculated Offset2 Vertices: 0x{1:x8}", (object)this.iPos, (object)part.OffsetVertices2);
            }
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
            ++referencecounter;
            ++referencecounter;
            return part;
        }

        protected override int ReadRelativePositionList(byte lastByte)
        {
            this.iPos += 4;
            int num1 = 1;
            int num2 = 0;
            while (BigEndianBitConverter.ToInt32(this.fileData, this.iPos) != 0)
            {
                this.iPos += 8;
                ++num1;
            }
            ColoredConsole.WriteLine("{0:x8}     Relative Position Lists: 0x{1:x8}", (object)this.iPos, (object)num1);
            this.iPos += 4;
            for (int index1 = 0; index1 < num1; ++index1)
            {
                this.iPos += 4;
                int int32_1 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                this.iPos += 4;
                if (int32_1 == 0)
                {
                    this.iPos += 4;
                    int int32_2 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                    this.iPos += 4;
                    this.iPos += int32_2;
                    ++num2;
                    this.iPos += 4;
                    int int32_3 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos);
                    this.iPos += 4;
                    this.iPos += 4 * int32_3;
                    if (int32_3 > 0)
                        ++num2;
                }
                else
                {
                    this.iPos += 4;
                    ++num2;
                    for (int index2 = 0; index2 < int32_1; ++index2)
                        this.iPos += 12;
                    this.iPos += 4;
                    this.iPos += 4;
                    this.iPos += 4;
                }
            }
            return num2;
        }
    }
}