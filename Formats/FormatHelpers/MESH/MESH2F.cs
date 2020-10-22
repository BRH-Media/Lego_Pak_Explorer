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
			int num = BigEndianBitConverter.ToInt32(fileData, iPos);
			ColoredConsole.WriteLine("{0:x8}     Number of Vertex Lists: 0x{1:x8}", iPos, num);
			iPos += 4;
			int offset;
			for (int i = 0; i < num; i++)
			{
				ColoredConsole.WriteLine("{0:x8}       Vertex List 0x{1:x8}", iPos, i);
				part.VertexListReferences1.Add(GetVertexListReference(ref referencecounter, out offset));
			}
			int num2 = BigEndianBitConverter.ToInt32(fileData, iPos);
			ColoredConsole.WriteLine("{0:x8}     Unknown Number (should be 0x0): 0x{1:x8}", iPos, num2);
			iPos += 4;
			if (num2 != 0)
			{
				part.VertexListReferences11 = new List<VertexListReference>();
			}
			for (int i = 0; i < num2; i++)
			{
				part.VertexListReferences11.Add(GetVertexListReference(ref referencecounter, out offset));
			}
			if (num2 != 0)
			{
				referencecounter++;
			}
			part.IndexListReference1 = GetIndexListReference(ref referencecounter);
			part.OffsetIndices = BigEndianBitConverter.ToInt32(fileData, iPos);
			ColoredConsole.WriteLine("{0:x8}     Offset Indices: 0x{1:x8}", iPos, part.OffsetIndices);
			iPos += 4;
			part.NumberIndices = BigEndianBitConverter.ToInt32(fileData, iPos);
			ColoredConsole.WriteLine("{0:x8}     Number Indices: 0x{1:x8}", iPos, part.NumberIndices);
			iPos += 4;
			part.OffsetVertices = BigEndianBitConverter.ToInt32(fileData, iPos);
			ColoredConsole.WriteLine("{0:x8}     Offset Vertices: 0x{1:x8}", iPos, part.OffsetVertices);
			iPos += 4;
			if (BigEndianBitConverter.ToInt16(fileData, iPos) != 0)
			{
				throw new NotSupportedException("ReadPart Offset Vertices + 4");
			}
			iPos += 2;
			part.NumberVertices = BigEndianBitConverter.ToInt32(fileData, iPos);
			ColoredConsole.WriteLine("{0:x8}     Number Vertices: 0x{1:x8}", iPos, part.NumberVertices);
			iPos += 4;
			iPos += 4;
			int num3 = BigEndianBitConverter.ToInt32(fileData, iPos);
			iPos += 4;
			if (num3 > 0)
			{
				ColoredConsole.Write("{0:x8}     ", iPos);
				for (int i = 0; i < num3; i++)
				{
					ColoredConsole.Write("{0:x2} ", fileData[iPos]);
					iPos++;
				}
				ColoredConsole.WriteLine();
				referencecounter++;
			}
			int num4 = BigEndianBitConverter.ToInt32(fileData, iPos);
			iPos += 4;
			if (num4 != 0)
			{
				int num5 = ReadRelativePositionList(0);
				referencecounter += num5;
			}
			iPos += 4;
			iPos += 36;
			num = fileData[iPos + 3];
			ColoredConsole.WriteLine("{0:x8}     Number of Vertex Lists: 0x{1:x8} ???", iPos, num);
			iPos += 4;
			if (num != 0)
			{
				ColoredConsole.WriteLine("{0:x8}       Vertex List 0x{1:x8}", iPos, 0);
				part.VertexListReferences2.Add(GetVertexListReference(ref referencecounter, out offset));
				part.NumberVertices2 = BigEndianBitConverter.ToInt32(fileData, iPos);
				ColoredConsole.WriteLine("{0:x8}     Number Vertices: 0x{1:x8}", iPos, part.NumberVertices2);
				iPos += 4;
				part.IndexListReference2 = GetIndexListReference(ref referencecounter);
				part.OffsetVertices2 = BigEndianBitConverter.ToInt32(fileData, iPos);
				ColoredConsole.WriteLine("{0:x8}     Offset Vertices: 0x{1:x8}", iPos, part.OffsetVertices2);
				iPos += 4;
				ColoredConsole.WriteLine("{0:x8}     Number Indices: 0x{1:x8}", iPos, part.NumberIndices);
				part.OffsetIndices2 = BigEndianBitConverter.ToInt32(fileData, iPos);
				ColoredConsole.WriteLine("{0:x8}     Offset Indices: 0x{1:x8}", iPos, part.OffsetIndices2);
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
			referencecounter++;
			return part;
		}
	}
}
