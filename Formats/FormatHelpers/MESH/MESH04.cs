// Decompiled with JetBrains decompiler
// Type: ExtractNxgMESH.MESHs.MESH04
// Assembly: ExtractNxgMESH, Version=1.0.7313.34797, Culture=neutral, PublicKeyToken=null
// MVID: 5377D76F-1B3F-4F23-B65C-30203127E91A
// Assembly location: C:\Users\baele\Downloads\ExtractNxgMESH\ExtractNxgMESH.exe

using System;
using System.Collections.Generic;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.ExtractHelper.VariableTypes;
using TT_Games_Explorer.Formats.FormatHelpers.Vertex;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.MESH
{
    public class MESH04
    {
        protected float[] lookUp;
        public Dictionary<int, VertexList> Vertexlistsdictionary = new Dictionary<int, VertexList>();
        public Dictionary<int, List<ushort>> Indexlistsdictionary = new Dictionary<int, List<ushort>>();
        public List<Part> Parts = new List<Part>();
        protected byte[] fileData;
        protected int iPos;
        public int version;

        protected float[] LookUp
        {
            get
            {
                if (lookUp == null)
                {
                    var num = 1.0 / (double)sbyte.MaxValue;
                    lookUp = new float[256];
                    lookUp[0] = -1f;
                    for (var index = 1; index < 256; ++index)
                        lookUp[index] = lookUp[index - 1] + (float)num;
                    lookUp[(int)sbyte.MaxValue] = 0.0f;
                    lookUp[(int)byte.MaxValue] = 1f;
                }
                return lookUp;
            }
        }

        public MESH04(byte[] fileData, int iPos)
        {
            this.fileData = fileData;
            this.iPos = iPos;
        }

        public virtual int Read(ref int referencecounter)
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

        protected virtual Part ReadPart(ref int referencecounter)
        {
            var part = new Part();
            var int32_1 = BigEndianBitConverter.ToInt32(fileData, iPos);
            ColoredConsole.WriteLine("{0:x8}     Number of Vertex Lists: 0x{1:x8}", (object)iPos, (object)int32_1);
            iPos += 4;
            for (var index = 0; index < int32_1; ++index)
            {
                ColoredConsole.WriteLine("{0:x8}       Vertex List 0x{1:x8}", (object)iPos, (object)index);
                part.VertexListReferences1.Add(GetVertexListReference(ref referencecounter, out var _));
            }
            iPos += 4;
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
            ++referencecounter;
            byte lastByte = 0;
            iPos += 4;
            var int32_2 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            if (int32_2 > 0)
            {
                ColoredConsole.Write("{0:x8}     ", (object)iPos);
                for (var index = 0; index < int32_2; ++index)
                {
                    ColoredConsole.Write("{0:x2} ", (object)fileData[iPos]);
                    lastByte = fileData[iPos];
                    ++iPos;
                }
                ColoredConsole.WriteLine();
                ++referencecounter;
            }
            var int32_3 = BigEndianBitConverter.ToInt32(fileData, iPos);
            iPos += 4;
            if (int32_3 != 0)
            {
                var num = ReadRelativePositionList(lastByte);
                referencecounter += num;
            }
            return part;
        }

        protected virtual int ReadRelativePositionList(byte lastByte)
        {
            var int32_1 = BigEndianBitConverter.ToInt32(fileData, iPos);
            if (int32_1 != 0)
                ColoredConsole.WriteLineError("{0:x8}       Relative Position Lists iUnk1 = {1:x8} (!= 0)", (object)iPos, (object)int32_1);
            iPos += 4;
            var num1 = 1;
            var num2 = 0;
            while (BigEndianBitConverter.ToInt32(fileData, iPos) != 0)
            {
                ColoredConsole.WriteLine("{0:x8}       Count 4 Number of Relative Position Lists: 0x{1:x8} 0x{2:x8}", (object)iPos, (object)BigEndianBitConverter.ToInt32(fileData, iPos), (object)BigEndianBitConverter.ToInt32(fileData, iPos + 4));
                iPos += 8;
                ++num1;
            }
            ColoredConsole.WriteLine("{0:x8}       Number of Relative Position Lists: 0x{1:x8}", (object)iPos, (object)num1);
            var int32_2 = BigEndianBitConverter.ToInt32(fileData, iPos);
            if (int32_2 != 0)
                ColoredConsole.WriteLineError("{0:x8}       Relative Position Lists iUnk2 = {1:x8} (!= 0)", (object)iPos, (object)int32_2);
            iPos += 4;
            for (var index = 0; index < num1; ++index)
            {
                var int32_3 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                if (int32_3 != 0)
                {
                    ColoredConsole.WriteLine("{0:x8}       Number of Relative Positions: 0x{1:x8}", (object)iPos, (object)int32_3);
                    iPos += int32_3 * 12;
                }
                else
                    ColoredConsole.WriteLineError("{0:x8}       Number of Relative Positions unknown: 0x{1:x8}", (object)iPos, (object)0);
                int num3;
                if (lastByte == byte.MaxValue)
                {
                    iPos += 5;
                    num3 = num2 + 1;
                }
                else
                {
                    var int16 = (int)BigEndianBitConverter.ToInt16(fileData, iPos);
                    if (int16 != 0)
                        ColoredConsole.WriteLineError("{0:x8}       Relative Position Lists iUnk3a = {1:x4} (!= 0)", (object)iPos, (object)int16);
                    iPos += 2;
                    var int32_4 = BigEndianBitConverter.ToInt32(fileData, iPos);
                    if (int32_4 != 1)
                        ColoredConsole.WriteLineError("{0:x8}       Relative Position Lists iUnk3b = {1:x8} (!= 1)", (object)iPos, (object)int32_4);
                    else
                        ++num2;
                    iPos += 4;
                    var int32_5 = BigEndianBitConverter.ToInt32(fileData, iPos);
                    ColoredConsole.WriteLine("{0:x8}       Number of Relative Position Tupels: 0x{1:x8}", (object)iPos, (object)int32_5);
                    iPos += 4;
                    iPos += 4 * int32_5;
                    num3 = num2 + 1;
                }
                num2 = num3 + 1;
            }
            return num2;
        }

        protected virtual int GetIndexListReference(ref int referencecounter)
        {
            int num;
            if (fileData[iPos] == (byte)192)
            {
                num = (int)BigEndianBitConverter.ToInt16(fileData, iPos + 2);
                iPos += 4;
                ColoredConsole.WriteLine("{0:x8}     Index List Reference to 0x{1:x4}", (object)iPos, (object)num);
                iPos += 4;
            }
            else
            {
                ColoredConsole.WriteLine("{0:x8}         New Index List 0x{1:x4}", (object)iPos, (object)referencecounter);
                iPos += 4;
                iPos += 4;
                var int32 = BigEndianBitConverter.ToInt32(fileData, iPos);
                ColoredConsole.WriteLine("{0:x8}           Number of Indices: {1:x8}", (object)iPos, (object)int32);
                iPos += 4;
                iPos += 4;
                var ushortList = new List<ushort>();
                for (var index = 0; index < int32; ++index)
                {
                    ushortList.Add(BigEndianBitConverter.ToUInt16(fileData, iPos));
                    iPos += 2;
                }
                Indexlistsdictionary.Add(referencecounter, ushortList);
                num = referencecounter++;
            }
            return num;
        }

        protected virtual VertexListReference GetVertexListReference(
          ref int referencecounter,
          out int offset)
        {
            int num;
            if (fileData[iPos] == (byte)192)
            {
                num = (int)BigEndianBitConverter.ToInt16(fileData, iPos + 2);
                ColoredConsole.WriteLineWarn("{0:x8}         Vertex List Reference to 0x{1:x4}", (object)iPos, (object)num);
                iPos += 4;
                ColoredConsole.WriteLine("{0:x8}           Unknown 0x{1:x8}", (object)iPos, (object)BigEndianBitConverter.ToInt32(fileData, iPos));
                iPos += 4;
                offset = BigEndianBitConverter.ToInt32(fileData, iPos);
                ColoredConsole.WriteLine("{0:x8}           Offset 0x{1:x8}", (object)iPos, (object)offset);
                iPos += 4;
            }
            else
            {
                ColoredConsole.WriteLineWarn("{0:x8}         New Vertex List 0x{1:x4}", (object)iPos, (object)referencecounter);
                ColoredConsole.WriteLine("{0:x8}           Unknown 0x{1:x8}", (object)iPos, (object)BigEndianBitConverter.ToInt32(fileData, iPos));
                iPos += 4;
                ColoredConsole.WriteLine("{0:x8}           Unknown 0x{1:x8}", (object)iPos, (object)BigEndianBitConverter.ToInt32(fileData, iPos));
                iPos += 4;
                var int32 = BigEndianBitConverter.ToInt32(fileData, iPos);
                iPos += 4;
                var vertexList = ReadVertexList(int32);
                offset = BigEndianBitConverter.ToInt32(fileData, iPos);
                ColoredConsole.WriteLine("{0:x8}           Offset 0x{1:x8}", (object)iPos, (object)offset);
                iPos += 4;
                Vertexlistsdictionary.Add(referencecounter, vertexList);
                num = referencecounter++;
            }
            return new VertexListReference()
            {
                GlobalOffset = 0,
                Reference = num
            };
        }

        protected virtual VertexList ReadVertexList(int numberofvertices)
        {
            var vertexList = new VertexList();
            if (fileData[iPos] != (byte)0)
            {
                var vertexDefinition = new VertexDefinition();
                vertexDefinition.Variable = VertexDefinition.VariableEnum.position;
                vertexDefinition.VariableType = (VertexDefinition.VariableTypeEnum)fileData[iPos];
                vertexList.VertexDefinitions.Add(vertexDefinition);
                ColoredConsole.WriteLine("{0:x8}             {1} {2}", (object)iPos, (object)vertexDefinition.VariableType.ToString(), (object)vertexDefinition.Variable.ToString());
            }
            iPos += 2;
            if (fileData[iPos] != (byte)0)
            {
                var vertexDefinition = new VertexDefinition();
                vertexDefinition.Variable = VertexDefinition.VariableEnum.normal;
                vertexDefinition.VariableType = (VertexDefinition.VariableTypeEnum)fileData[iPos];
                vertexList.VertexDefinitions.Add(vertexDefinition);
                ColoredConsole.WriteLine("{0:x8}             {1} {2}", (object)iPos, (object)vertexDefinition.VariableType.ToString(), (object)vertexDefinition.Variable.ToString());
            }
            iPos += 2;
            if (fileData[iPos] != (byte)0)
            {
                var vertexDefinition = new VertexDefinition();
                vertexDefinition.Variable = VertexDefinition.VariableEnum.colorSet0;
                vertexDefinition.VariableType = (VertexDefinition.VariableTypeEnum)fileData[iPos];
                vertexList.VertexDefinitions.Add(vertexDefinition);
                ColoredConsole.WriteLine("{0:x8}             {1} {2}", (object)iPos, (object)vertexDefinition.VariableType.ToString(), (object)vertexDefinition.Variable.ToString());
            }
            iPos += 2;
            if (fileData[iPos] != (byte)0)
            {
                var vertexDefinition = new VertexDefinition();
                vertexDefinition.Variable = VertexDefinition.VariableEnum.tangent;
                vertexDefinition.VariableType = (VertexDefinition.VariableTypeEnum)fileData[iPos];
                vertexList.VertexDefinitions.Add(vertexDefinition);
                ColoredConsole.WriteLine("{0:x8}             {1} {2}", (object)iPos, (object)vertexDefinition.VariableType.ToString(), (object)vertexDefinition.Variable.ToString());
            }
            iPos += 2;
            if (fileData[iPos] != (byte)0)
            {
                var vertexDefinition = new VertexDefinition();
                vertexDefinition.Variable = VertexDefinition.VariableEnum.colorSet1;
                vertexDefinition.VariableType = (VertexDefinition.VariableTypeEnum)fileData[iPos];
                vertexList.VertexDefinitions.Add(vertexDefinition);
                ColoredConsole.WriteLine("{0:x8}             {1} {2}", (object)iPos, (object)vertexDefinition.VariableType.ToString(), (object)vertexDefinition.Variable.ToString());
            }
            iPos += 2;
            if (fileData[iPos] != (byte)0)
            {
                var vertexDefinition = new VertexDefinition();
                vertexDefinition.Variable = VertexDefinition.VariableEnum.uvSet01;
                vertexDefinition.VariableType = (VertexDefinition.VariableTypeEnum)fileData[iPos];
                vertexList.VertexDefinitions.Add(vertexDefinition);
                ColoredConsole.WriteLine("{0:x8}             {1} {2}", (object)iPos, (object)vertexDefinition.VariableType.ToString(), (object)vertexDefinition.Variable.ToString());
            }
            iPos += 2;
            iPos += 2;
            if (fileData[iPos] != (byte)0)
            {
                var vertexDefinition = new VertexDefinition();
                vertexDefinition.Variable = VertexDefinition.VariableEnum.uvSet2;
                vertexDefinition.VariableType = (VertexDefinition.VariableTypeEnum)fileData[iPos];
                vertexList.VertexDefinitions.Add(vertexDefinition);
                ColoredConsole.WriteLine("{0:x8}             {1} {2}", (object)iPos, (object)vertexDefinition.VariableType.ToString(), (object)vertexDefinition.Variable.ToString());
            }
            iPos += 2;
            iPos += 2;
            if (fileData[iPos] != (byte)0)
            {
                var vertexDefinition = new VertexDefinition();
                vertexDefinition.Variable = VertexDefinition.VariableEnum.blendIndices0;
                vertexDefinition.VariableType = (VertexDefinition.VariableTypeEnum)fileData[iPos];
                vertexList.VertexDefinitions.Add(vertexDefinition);
                ColoredConsole.WriteLine("{0:x8}             {1} {2}", (object)iPos, (object)vertexDefinition.VariableType.ToString(), (object)vertexDefinition.Variable.ToString());
            }
            iPos += 2;
            if (fileData[iPos] != (byte)0)
            {
                var vertexDefinition = new VertexDefinition();
                vertexDefinition.Variable = VertexDefinition.VariableEnum.blendWeight0;
                vertexDefinition.VariableType = (VertexDefinition.VariableTypeEnum)fileData[iPos];
                vertexList.VertexDefinitions.Add(vertexDefinition);
                ColoredConsole.WriteLine("{0:x8}             {1} {2}", (object)iPos, (object)vertexDefinition.VariableType.ToString(), (object)vertexDefinition.Variable.ToString());
            }
            iPos += 2;
            if (fileData[iPos] != (byte)0)
            {
                var vertexDefinition = new VertexDefinition();
                vertexDefinition.Variable = VertexDefinition.VariableEnum.lightDirSet;
                vertexDefinition.VariableType = (VertexDefinition.VariableTypeEnum)fileData[iPos];
                vertexList.VertexDefinitions.Add(vertexDefinition);
                ColoredConsole.WriteLine("{0:x8}             {1} {2}", (object)iPos, (object)vertexDefinition.VariableType.ToString(), (object)vertexDefinition.Variable.ToString());
            }
            iPos += 2;
            if (fileData[iPos] != (byte)0)
            {
                var vertexDefinition = new VertexDefinition();
                vertexDefinition.Variable = VertexDefinition.VariableEnum.lightColSet;
                vertexDefinition.VariableType = (VertexDefinition.VariableTypeEnum)fileData[iPos];
                vertexList.VertexDefinitions.Add(vertexDefinition);
                ColoredConsole.WriteLine("{0:x8}             {1} {2}", (object)iPos, (object)vertexDefinition.VariableType.ToString(), (object)vertexDefinition.Variable.ToString());
            }
            iPos += 2;
            iPos += 6;
            ColoredConsole.WriteLine("{0:x8}           Number of Vertices: {1:x8}", (object)iPos, (object)numberofvertices);
            for (var index = 0; index < numberofvertices; ++index)
                vertexList.Vertices.Add(ReadVertex(vertexList.VertexDefinitions));
            return vertexList;
        }

        protected virtual Vertex.Vertex ReadVertex(List<VertexDefinition> vertexdefinitions)
        {
            var vertex = new Vertex.Vertex();
            foreach (var vertexdefinition in vertexdefinitions)
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

                    case VertexDefinition.VariableEnum.colorSet1:
                        vertex.ColorSet1 = (Color4)ReadVariableValue(vertexdefinition.VariableType);
                        break;

                    case VertexDefinition.VariableEnum.uvSet01:
                        vertex.UVSet0 = (Vector2)ReadVariableValue(vertexdefinition.VariableType);
                        break;

                    default:
                        throw new NotSupportedException(vertexdefinition.Variable.ToString());
                }
            }
            return vertex;
        }

        protected virtual object ReadVariableValue(VertexDefinition.VariableTypeEnum variabletype)
        {
            switch (variabletype)
            {
                case VertexDefinition.VariableTypeEnum.vec2float:
                    var vector2_1 = new Vector2()
                    {
                        X = BigEndianBitConverter.ToSingle(fileData, iPos),
                        Y = BigEndianBitConverter.ToSingle(fileData, iPos + 4)
                    };
                    iPos += 8;
                    return (object)vector2_1;

                case VertexDefinition.VariableTypeEnum.vec3float:
                    var vector3_1 = new Vector3();
                    vector3_1.X = BigEndianBitConverter.ToSingle(fileData, iPos);
                    vector3_1.Y = BigEndianBitConverter.ToSingle(fileData, iPos + 4);
                    vector3_1.Z = BigEndianBitConverter.ToSingle(fileData, iPos + 8);
                    var vector3_2 = vector3_1;
                    iPos += 12;
                    return (object)vector3_2;

                case VertexDefinition.VariableTypeEnum.vec4float:
                    var vector4_1 = new Vector4();
                    vector4_1.X = BigEndianBitConverter.ToSingle(fileData, iPos);
                    vector4_1.Y = BigEndianBitConverter.ToSingle(fileData, iPos + 4);
                    vector4_1.Z = BigEndianBitConverter.ToSingle(fileData, iPos + 8);
                    vector4_1.W = (float)BigEndianBitConverter.ToHalf(fileData, iPos + 12);
                    var vector4_2 = vector4_1;
                    iPos += 16;
                    return (object)vector4_2;

                case VertexDefinition.VariableTypeEnum.vec2half:
                    var vector2_2 = new Vector2()
                    {
                        X = (float)BigEndianBitConverter.ToHalf(fileData, iPos),
                        Y = (float)BigEndianBitConverter.ToHalf(fileData, iPos + 2)
                    };
                    iPos += 4;
                    return (object)vector2_2;

                case VertexDefinition.VariableTypeEnum.vec4half:
                    var vector4_3 = new Vector4();
                    vector4_3.X = (float)BigEndianBitConverter.ToHalf(fileData, iPos);
                    vector4_3.Y = (float)BigEndianBitConverter.ToHalf(fileData, iPos + 2);
                    vector4_3.Z = (float)BigEndianBitConverter.ToHalf(fileData, iPos + 4);
                    vector4_3.W = (float)BigEndianBitConverter.ToHalf(fileData, iPos + 6);
                    var vector4_4 = vector4_3;
                    iPos += 8;
                    return (object)vector4_4;

                case VertexDefinition.VariableTypeEnum.vec4char:
                    iPos += 4;
                    return (object)1;

                case VertexDefinition.VariableTypeEnum.vec4mini:
                    var vector4_5 = new Vector4();
                    vector4_5.X = LookUp[(int)fileData[iPos]];
                    vector4_5.Y = LookUp[(int)fileData[iPos + 1]];
                    vector4_5.Z = LookUp[(int)fileData[iPos + 2]];
                    vector4_5.W = LookUp[(int)fileData[iPos + 3]];
                    var vector4_6 = vector4_5;
                    iPos += 4;
                    return (object)vector4_6;

                case VertexDefinition.VariableTypeEnum.color4char:
                    var color4 = new Color4()
                    {
                        R = (int)fileData[iPos],
                        G = (int)fileData[iPos + 1],
                        B = (int)fileData[iPos + 2],
                        A = (int)fileData[iPos + 3]
                    };
                    iPos += 4;
                    return (object)color4;

                default:
                    throw new NotImplementedException(variabletype.ToString());
            }
        }
    }
}