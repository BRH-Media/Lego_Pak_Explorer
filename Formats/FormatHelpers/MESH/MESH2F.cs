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
            var part = new Part();
            var int32_1 = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}     Number of Vertex Lists: 0x{1:x8}", (object)iPos, (object)int32_1);
            iPos += 4;
            int offset;
            for (var index = 0; index < int32_1; ++index)
            {
                ColoredConsole.WriteLine("{0:x8}       Vertex List 0x{1:x8}", (object)iPos, (object)index);
                part.VertexListReferences1.Add(GetVertexListReference(ref referencecounter, out offset));
            }
            var int32_2 = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}     Unknown Number (should be 0x0): 0x{1:x8}", (object)iPos, (object)int32_2);
            iPos += 4;
            if (int32_2 != 0)
                part.VertexListReferences11 = new List<VertexListReference>();
            for (var index = 0; index < int32_2; ++index)
                part.VertexListReferences11.Add(GetVertexListReference(ref referencecounter, out offset));
            if (int32_2 != 0)
                ++referencecounter;
            part.IndexListReference1 = GetIndexListReference(ref referencecounter);
            part.OffsetIndices = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}     Offset Indices: 0x{1:x8}", (object)iPos, (object)part.OffsetIndices);
            iPos += 4;
            part.NumberIndices = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}     Number Indices: 0x{1:x8}", (object)iPos, (object)part.NumberIndices);
            iPos += 4;
            part.OffsetVertices = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}     Offset Vertices: 0x{1:x8}", (object)iPos, (object)part.OffsetVertices);
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
            var num1 = (int)fileData[iPos + 3];
            ColoredConsole.WriteLine("{0:x8}     Number of Vertex Lists: 0x{1:x8} ???", (object)iPos, (object)num1);
            iPos += 4;
            if (num1 != 0)
            {
                ColoredConsole.WriteLine("{0:x8}       Vertex List 0x{1:x8}", (object)iPos, (object)0);
                part.VertexListReferences2.Add(GetVertexListReference(ref referencecounter, out offset));
                part.NumberVertices2 = BigEndianBitConverter.ToInt32(fileData, iPos);
                ColoredConsole.WriteLine("{0:x8}     Number Vertices: 0x{1:x8}", (object)iPos, (object)part.NumberVertices2);
                iPos += 4;
                part.IndexListReference2 = GetIndexListReference(ref referencecounter);
                part.OffsetVertices2 = BigEndianBitConverter.ToInt32(fileData, iPos);
                ColoredConsole.WriteLine("{0:x8}     Offset Vertices: 0x{1:x8}", (object)iPos, (object)part.OffsetVertices2);
                iPos += 4;
                ColoredConsole.WriteLine("{0:x8}     Number Indices: 0x{1:x8}", (object)iPos, (object)part.NumberIndices);
                part.OffsetIndices2 = BigEndianBitConverter.ToInt32(fileData, iPos);
                ColoredConsole.WriteLine("{0:x8}     Offset Indices: 0x{1:x8}", (object)iPos, (object)part.OffsetIndices2);
                iPos += 4;
                iPos += 4;
            }
            else
            {
                iPos += 4;
                iPos += 4;
                iPos += 4;
                iPos += 4;
                iPos += 4;
                iPos += 4;
                iPos += 4;
            }
            ++referencecounter;
            return part;
        }
    }
}