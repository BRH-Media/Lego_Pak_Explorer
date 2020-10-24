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
            int num = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}   Number of Parts: 0x{1:x8}", iPos, num);
            iPos += 4;
            for (int i = 0; i < num; i++)
            {
                ColoredConsole.WriteLine("{0:x8}   Part 0x{1:x8}", iPos, i);
                Parts.Add(ReadPart(ref referencecounter));
            }
            return iPos;
        }

        protected override Part ReadPart(ref int referencecounter)
        {
            iPos += 4;
            Part part = new Part();
            int num = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}     Number of Vertex Lists: 0x{1:x8}", iPos, num);
            iPos += 4;
            int offset;
            for (int i = 0; i < num; i++)
            {
                ColoredConsole.WriteLine("{0:x8}       Vertex List 0x{1:x8}", iPos, i);
                VertexListReference vertexListReference = GetVertexListReference(ref referencecounter, out offset);
                part.VertexListReferences1.Add(vertexListReference);
                if (i == 0)
                {
                    part.OffsetVertices = offset / Vertexlistsdictionary[vertexListReference.Reference].VertexSize;
                }
                else
                {
                    part.OffsetVertices2 = offset / Vertexlistsdictionary[vertexListReference.Reference].VertexSize;
                }
            }
            iPos += 4;
            part.IndexListReference1 = GetIndexListReference(ref referencecounter);
            part.OffsetIndices = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}     Offset Indices: 0x{1:x8}", iPos, part.OffsetIndices);
            iPos += 4;
            part.NumberIndices = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}     Number Indices: 0x{1:x8}", iPos, part.NumberIndices);
            iPos += 4;
            offset = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}     Offset Vertices: 0x{1:x8}", iPos, offset);
            if (offset != 0)
            {
                part.OffsetVertices = offset;
                part.OffsetVertices2 = offset;
            }
            else
            {
                if (part.OffsetVertices != 0)
                {
                    ColoredConsole.WriteLine("{0:x8}       --> Calculated Offset1 Vertices: 0x{1:x8}", iPos, part.OffsetVertices);
                }
                if (part.VertexListReferences1.Count > 1 && part.OffsetVertices2 != 0)
                {
                    ColoredConsole.WriteLine("{0:x8}       --> Calculated Offset2 Vertices: 0x{1:x8}", iPos, part.OffsetVertices2);
                }
            }
            iPos += 4;
            bool flag = true;
            if (BigEndianBitConverter.ToInt16(fileData, iPos) != 0)
            {
                throw new NotSupportedException("ReadPart Offset Vertices + 4");
            }
            iPos += 2;
            part.NumberVertices = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}     Number Vertices: 0x{1:x8}", iPos, part.NumberVertices);
            iPos += 4;
            iPos += 4;
            int num2 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            if (num2 > 0)
            {
                ColoredConsole.Write("{0:x8}     ", iPos);
                for (int i = 0; i < num2; i++)
                {
                    ColoredConsole.Write("{0:x2} ", fileData[iPos]);
                    iPos++;
                }
                ColoredConsole.WriteLine();
                referencecounter++;
            }
            int num3 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            if (num3 != 0)
            {
                int num4 = ReadRelativePositionList(0);
                referencecounter += num4;
            }
            iPos += 4;
            iPos += 36;
            referencecounter++;
            referencecounter++;
            return part;
        }

        protected override int ReadRelativePositionList(byte lastByte)
        {
            iPos += 4;
            int num = 1;
            int num2 = 0;
            while (BigEndianBitConverter.ToInt32(fileData, iPos) != 0)
            {
                iPos += 8;
                num++;
            }
            ColoredConsole.WriteLine("{0:x8}     Relative Position Lists: 0x{1:x8}", iPos, num);
            iPos += 4;
            for (int i = 0; i < num; i++)
            {
                iPos += 4;
                int num3 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                if (num3 == 0)
                {
                    iPos += 4;
                    int num4 = BigEndianBitConverter.ToInt32(fileData, iPos);
                    iPos += 4;
                    iPos += num4;
                    num2++;
                    iPos += 4;
                    int num5 = BigEndianBitConverter.ToInt32(fileData, iPos);
                    iPos += 4;
                    iPos += 4 * num5;
                    if (num5 > 0)
                    {
                        num2++;
                    }
                }
                else
                {
                    iPos += 4;
                    num2++;
                    for (int j = 0; j < num3; j++)
                    {
                        iPos += 12;
                    }
                    iPos += 4;
                    iPos += 4;
                    iPos += 4;
                }
            }
            return num2;
        }
    }
}