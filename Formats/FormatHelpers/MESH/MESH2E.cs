using System;
using System.Collections.Generic;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.ExtractHelper.VariableTypes;
using TT_Games_Explorer.Formats.FormatHelpers.Vertex;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.MESH
{
	public class MESH2E : MESH05
	{
		public MESH2E(byte[] fileData, int iPos)
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
			Part part = new Part();
			int num = BigEndianBitConverter.ToInt32(fileData, iPos);
			ColoredConsole.WriteLine("{0:x8}     Number of Vertex Lists: 0x{1:x8}", iPos, num);
			iPos += 4;
			for (int i = 0; i < num; i++)
			{
				ColoredConsole.WriteLine("{0:x8}       Vertex List 0x{1:x8}", iPos, i);
				part.VertexListReferences1.Add(GetVertexListReference(ref referencecounter, out var _));
			}
			int num2 = BigEndianBitConverter.ToInt32(fileData, iPos);
			ColoredConsole.WriteLine("{0:x8}           Unknown Number of Index Lists ? 0x{1:x8}", iPos, num2);
			iPos += 4;
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
			referencecounter++;
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
				int num3 = BigEndianBitConverter.ToInt32(fileData, iPos);
				iPos += 4;
				if (num3 == 0)
				{
					iPos += 5;
					iPos += 4;
					iPos += 4;
					int num4 = BigEndianBitConverter.ToInt32(fileData, iPos);
					ColoredConsole.WriteLine("{0:x8}       Size of Relative Positions: 0x{1:x8}", iPos, num4);
					iPos += 4;
					iPos += num4;
					num2++;
					int num5 = BigEndianBitConverter.ToInt32(fileData, iPos);
					ColoredConsole.WriteLine("{0:x8}       Relative Position Tupels: 0x{1:x8}", iPos, num5);
					iPos += 4;
					iPos += 4 * num5;
					if (num5 > 0)
					{
						num2++;
					}
					num2++;
				}
				else
				{
					num2++;
					num2++;
					for (int j = 0; j < num3; j++)
					{
						iPos += 12;
					}
					iPos += 21;
				}
			}
			return num2;
		}

		protected override int GetIndexListReference(ref int referencecounter)
		{
			int num = -1;
			if (fileData[iPos] == 192)
			{
				num = BigEndianBitConverter.ToInt16(fileData, iPos + 2);
				ColoredConsole.WriteLine("{0:x8}     Index List Reference to 0x{1:x4}", iPos, num);
				iPos += 4;
				int num2 = BigEndianBitConverter.ToInt32(fileData, iPos);
				ColoredConsole.WriteLine("{0:x8}       Unknown 0x{1:x8}", iPos, num2);
				iPos += 4;
			}
			else
			{
				ColoredConsole.WriteLineWarn("{0:x8}         New Index List 0x{1:x4}", iPos, referencecounter);
				int num2 = BigEndianBitConverter.ToInt32(fileData, iPos);
				ColoredConsole.WriteLine("{0:x8}           Unknown 0x{1:x8}", iPos, num2);
				iPos += 4;
				num2 = BigEndianBitConverter.ToInt32(fileData, iPos);
				ColoredConsole.WriteLine("{0:x8}           Unknown 0x{1:x8}", iPos, num2);
				iPos += 4;
				int num3 = BigEndianBitConverter.ToInt32(fileData, iPos);
				ColoredConsole.WriteLine("{0:x8}           Number of Indices: {1:x8}", iPos, num3);
				iPos += 4;
				num2 = BigEndianBitConverter.ToInt32(fileData, iPos);
				ColoredConsole.WriteLine("{0:x8}           Unknown 0x{1:x8}", iPos, num2);
				iPos += 4;
				List<ushort> list = new List<ushort>();
				for (int i = 0; i < num3; i++)
				{
					list.Add(BigEndianBitConverter.ToUInt16(fileData, iPos));
					iPos += 2;
				}
				Indexlistsdictionary.Add(referencecounter, list);
				num = referencecounter++;
			}
			return num;
		}

		protected override VertexListReference GetVertexListReference(ref int referencecounter, out int offset)
		{
			int num = -1;
			if (fileData[iPos] == 192)
			{
				num = BigEndianBitConverter.ToInt16(fileData, iPos + 2);
				ColoredConsole.WriteLineWarn("{0:x8}         Vertex List Reference to 0x{1:x4}", iPos, num);
				iPos += 4;
				int num2 = BigEndianBitConverter.ToInt32(fileData, iPos);
				ColoredConsole.WriteLine("{0:x8}           Unknown 0x{1:x8}", iPos, num2);
				iPos += 4;
				offset = BigEndianBitConverter.ToInt32(fileData, iPos);
				ColoredConsole.WriteLine("{0:x8}           Offset 0x{1:x8}", iPos, offset);
				iPos += 4;
			}
			else
			{
				ColoredConsole.WriteLineWarn("{0:x8}         New Vertex List 0x{1:x4}", iPos, referencecounter);
				int num2 = BigEndianBitConverter.ToInt32(fileData, iPos);
				ColoredConsole.WriteLine("{0:x8}           Unknown 0x{1:x8}", iPos, num2);
				iPos += 4;
				num2 = BigEndianBitConverter.ToInt32(fileData, iPos);
				ColoredConsole.WriteLine("{0:x8}           Unknown 0x{1:x8}", iPos, num2);
				iPos += 4;
				int numberofvertices = BigEndianBitConverter.ToInt32(fileData, iPos);
				iPos += 4;
				VertexList value = ReadVertexList(numberofvertices);
				offset = BigEndianBitConverter.ToInt32(fileData, iPos);
				ColoredConsole.WriteLine("{0:x8}           Offset 0x{1:x8}", iPos, offset);
				iPos += 4;
				Vertexlistsdictionary.Add(referencecounter, value);
				num = referencecounter++;
			}
			VertexListReference vertexListReference = new VertexListReference();
			vertexListReference.GlobalOffset = offset;
			vertexListReference.Reference = num;
			return vertexListReference;
		}

		protected override VertexList ReadVertexList(int numberofvertices)
		{
			VertexList vertexList = new VertexList();
			int num = BigEndianBitConverter.ToInt32(fileData, iPos);
			ColoredConsole.WriteLine("{0:x8}           Number of Vertex Definitions: {1:x8}", iPos, num);
			iPos += 4;
			for (int i = 0; i < num; i++)
			{
				VertexDefinition vertexDefinition = ReadVertexDefinition();
				vertexList.VertexDefinitions.Add(vertexDefinition);
				switch (vertexDefinition.VariableType)
				{
				case VertexDefinition.VariableTypeEnum.vec2half:
				case VertexDefinition.VariableTypeEnum.vec4char:
				case VertexDefinition.VariableTypeEnum.vec4mini:
				case VertexDefinition.VariableTypeEnum.color4char:
					vertexList.VertexSize += 4;
					break;
				case VertexDefinition.VariableTypeEnum.vec2float:
				case VertexDefinition.VariableTypeEnum.vec4half:
					vertexList.VertexSize += 8;
					break;
				case VertexDefinition.VariableTypeEnum.vec3float:
					vertexList.VertexSize += 12;
					break;
				case VertexDefinition.VariableTypeEnum.vec4float:
					vertexList.VertexSize += 16;
					break;
				default:
					throw new NotSupportedException("VariableType: " + vertexDefinition.VariableType);
				}
			}
			ColoredConsole.WriteLine("{0:x8}           Number of Vertices: {1:x8}", iPos, numberofvertices);
			for (int i = 0; i < numberofvertices; i++)
			{
				vertexList.Vertices.Add(ReadVertex(vertexList.VertexDefinitions));
			}
			return vertexList;
		}

		protected virtual VertexDefinition ReadVertexDefinition()
		{
			VertexDefinition vertexDefinition = new VertexDefinition();
			vertexDefinition.Variable = (VertexDefinition.VariableEnum)fileData[iPos];
			vertexDefinition.VariableType = (VertexDefinition.VariableTypeEnum)fileData[iPos + 1];
			vertexDefinition.Offset = fileData[iPos + 2];
			ColoredConsole.WriteLine("{0:x8}             {1} {2}", iPos, vertexDefinition.VariableType.ToString(), vertexDefinition.Variable.ToString());
			iPos += 3;
			return vertexDefinition;
		}

		protected override Vertex.Vertex ReadVertex(List<VertexDefinition> vertexdefinitions)
		{
			Vertex.Vertex vertex = new Vertex.Vertex();
			foreach (VertexDefinition vertexdefinition in vertexdefinitions)
			{
				switch (vertexdefinition.Variable)
				{
				case VertexDefinition.VariableEnum.position:
					vertex.Position = (Vector3)ReadVariableValue(vertexdefinition.VariableType);
					break;
				case VertexDefinition.VariableEnum.normal:
					vertex.Normal = (Vector3)ReadVariableValue(vertexdefinition.VariableType);
					break;
				case VertexDefinition.VariableEnum.colorSet0:
					vertex.ColorSet0 = (Color4)ReadVariableValue(vertexdefinition.VariableType);
					break;
				case VertexDefinition.VariableEnum.colorSet1:
					vertex.ColorSet1 = (Color4)ReadVariableValue(vertexdefinition.VariableType);
					break;
				case VertexDefinition.VariableEnum.uvSet01:
					vertex.UVSet0 = (Vector2)ReadVariableValue(vertexdefinition.VariableType);
					break;
				case VertexDefinition.VariableEnum.tangent:
				case VertexDefinition.VariableEnum.unknown6:
				case VertexDefinition.VariableEnum.uvSet2:
				case VertexDefinition.VariableEnum.unknown8:
				case VertexDefinition.VariableEnum.blendIndices0:
				case VertexDefinition.VariableEnum.blendWeight0:
				case VertexDefinition.VariableEnum.unknown11:
				case VertexDefinition.VariableEnum.lightDirSet:
				case VertexDefinition.VariableEnum.lightColSet:
					ReadVariableValue(vertexdefinition.VariableType);
					break;
				default:
					throw new NotSupportedException(vertexdefinition.Variable.ToString());
				}
			}
			return vertex;
		}

		protected override object ReadVariableValue(VertexDefinition.VariableTypeEnum variabletype)
		{
			switch (variabletype)
			{
			case VertexDefinition.VariableTypeEnum.vec2float:
			{
				Vector2 vector6 = new Vector2();
				vector6.X = BigEndianBitConverter.ToSingle(fileData, iPos);
				vector6.Y = BigEndianBitConverter.ToSingle(fileData, iPos + 4);
				Vector2 result7 = vector6;
				iPos += 8;
				return result7;
			}
			case VertexDefinition.VariableTypeEnum.vec3float:
			{
				Vector3 vector5 = new Vector3();
				vector5.X = BigEndianBitConverter.ToSingle(fileData, iPos);
				vector5.Y = BigEndianBitConverter.ToSingle(fileData, iPos + 4);
				vector5.Z = BigEndianBitConverter.ToSingle(fileData, iPos + 8);
				Vector3 result6 = vector5;
				iPos += 12;
				return result6;
			}
			case VertexDefinition.VariableTypeEnum.vec4float:
			{
				Vector4 vector4 = new Vector4();
				vector4.X = BigEndianBitConverter.ToSingle(fileData, iPos);
				vector4.Y = BigEndianBitConverter.ToSingle(fileData, iPos + 4);
				vector4.Z = BigEndianBitConverter.ToSingle(fileData, iPos + 8);
				vector4.W = BigEndianBitConverter.ToSingle(fileData, iPos + 12);
				Vector4 result5 = vector4;
				iPos += 16;
				return result5;
			}
			case VertexDefinition.VariableTypeEnum.vec2half:
			{
				Vector2 vector3 = new Vector2();
				vector3.X = BigEndianBitConverter.ToHalf(fileData, iPos);
				vector3.Y = BigEndianBitConverter.ToHalf(fileData, iPos + 2);
				Vector2 result4 = vector3;
				iPos += 4;
				return result4;
			}
			case VertexDefinition.VariableTypeEnum.vec4half:
			{
				Vector4 vector2 = new Vector4();
				vector2.X = BigEndianBitConverter.ToHalf(fileData, iPos);
				vector2.Y = BigEndianBitConverter.ToHalf(fileData, iPos + 2);
				vector2.Z = BigEndianBitConverter.ToHalf(fileData, iPos + 4);
				vector2.W = BigEndianBitConverter.ToHalf(fileData, iPos + 6);
				Vector4 result3 = vector2;
				iPos += 8;
				return result3;
			}
			case VertexDefinition.VariableTypeEnum.vec4char:
				iPos += 4;
				return 1;
			case VertexDefinition.VariableTypeEnum.vec4mini:
			{
				Vector4 vector = new Vector4();
				vector.X = base.LookUp[fileData[iPos]];
				vector.Y = base.LookUp[fileData[iPos + 1]];
				vector.Z = base.LookUp[fileData[iPos + 2]];
				vector.W = base.LookUp[fileData[iPos + 3]];
				Vector4 result2 = vector;
				iPos += 4;
				return result2;
			}
			case VertexDefinition.VariableTypeEnum.color4char:
			{
				Color4 color = new Color4();
				color.R = fileData[iPos];
				color.G = fileData[iPos + 1];
				color.B = fileData[iPos + 2];
				color.A = fileData[iPos + 3];
				Color4 result = color;
				iPos += 4;
				return result;
			}
			default:
				throw new NotImplementedException(variabletype.ToString());
			}
		}
	}
}
