using System;
using TT_Games_Explorer.Formats.ExtractHelper;
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
            var int32 = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}   Number of Parts: 0x{1:x8}", (object)iPos, (object)int32);
            iPos += 4;
            for (var index = 0; index < int32; ++index)
            {
                ColoredConsole.WriteLine("{0:x8}   Part 0x{1:x8}", (object)iPos, (object)index);
                Parts.Add(ReadPart(ref referencecounter));
            }
            return iPos;
        }

        protected override Part ReadPart(ref int referencecounter)
        {
            iPos += 4;
            var part = new Part();
            var int32_1 = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}     Number of Vertex Lists: 0x{1:x8}", (object)iPos, (object)int32_1);
            iPos += 4;
            for (var index = 0; index < int32_1; ++index)
            {
                ColoredConsole.WriteLine("{0:x8}       Vertex List 0x{1:x8}", (object)iPos, (object)index);
                int offset;
                var vertexListReference = GetVertexListReference(ref referencecounter, out offset);
                part.VertexListReferences1.Add(vertexListReference);
                if (index == 0)
                    part.OffsetVertices = offset / Vertexlistsdictionary[vertexListReference.Reference].VertexSize;
                else
                    part.OffsetVertices2 = offset / Vertexlistsdictionary[vertexListReference.Reference].VertexSize;
            }
            iPos += 4;
            part.IndexListReference1 = GetIndexListReference(ref referencecounter);
            part.OffsetIndices = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}     Offset Indices: 0x{1:x8}", (object)iPos, (object)part.OffsetIndices);
            iPos += 4;
            part.NumberIndices = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}     Number Indices: 0x{1:x8}", (object)iPos, (object)part.NumberIndices);
            iPos += 4;
            var int32_2 = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}     Offset Vertices: 0x{1:x8}", (object)iPos, (object)int32_2);
            if (int32_2 != 0)
            {
                part.OffsetVertices = int32_2;
                part.OffsetVertices2 = int32_2;
            }
            else
            {
                if (part.OffsetVertices != 0)
                    ColoredConsole.WriteLine("{0:x8}       --> Calculated Offset1 Vertices: 0x{1:x8}", (object)iPos, (object)part.OffsetVertices);
                if (part.VertexListReferences1.Count > 1 && part.OffsetVertices2 != 0)
                    ColoredConsole.WriteLine("{0:x8}       --> Calculated Offset2 Vertices: 0x{1:x8}", (object)iPos, (object)part.OffsetVertices2);
            }
            iPos += 4;
            if (BigEndianBitConverter.ToInt16(fileData, iPos) != (short)0)
                throw new NotSupportedException("ReadPart Offset Vertices + 4");
            iPos += 2;
            part.NumberVertices = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}     Number Vertices: 0x{1:x8}", (object)iPos, (object)part.NumberVertices);
            iPos += 4;
            iPos += 4;
            var int32_3 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            if (int32_3 > 0)
            {
                ColoredConsole.Write("{0:x8}     ", (object)iPos);
                for (var index = 0; index < int32_3; ++index)
                {
                    ColoredConsole.Write("{0:x2} ", (object)fileData[iPos]);
                    ++iPos;
                }
                ColoredConsole.WriteLine();
                ++referencecounter;
            }
            var int32_4 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            if (int32_4 != 0)
            {
                var num = ReadRelativePositionList((byte)0);
                referencecounter += num;
            }
            iPos += 4;
            iPos += 36;
            ++referencecounter;
            ++referencecounter;
            return part;
        }

        protected override int ReadRelativePositionList(byte lastByte)
        {
            iPos += 4;
            var num1 = 1;
            var num2 = 0;
            while (BigEndianBitConverter.ToInt32(fileData, iPos) != 0)
            {
                iPos += 8;
                ++num1;
            }
            ColoredConsole.WriteLine("{0:x8}     Relative Position Lists: 0x{1:x8}", (object)iPos, (object)num1);
            iPos += 4;
            for (var index1 = 0; index1 < num1; ++index1)
            {
                iPos += 4;
                var int32_1 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                if (int32_1 == 0)
                {
                    iPos += 4;
                    var int32_2 = BigEndianBitConverter.ToInt32(fileData, iPos);
                    iPos += 4;
                    iPos += int32_2;
                    ++num2;
                    iPos += 4;
                    var int32_3 = BigEndianBitConverter.ToInt32(fileData, iPos);
                    iPos += 4;
                    iPos += 4 * int32_3;
                    if (int32_3 > 0)
                        ++num2;
                }
                else
                {
                    iPos += 4;
                    ++num2;
                    for (var index2 = 0; index2 < int32_1; ++index2)
                        iPos += 12;
                    iPos += 4;
                    iPos += 4;
                    iPos += 4;
                }
            }
            return num2;
        }
    }
}