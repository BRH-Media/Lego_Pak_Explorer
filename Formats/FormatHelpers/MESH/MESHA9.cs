using System;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.FormatHelpers.Vertex;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;

namespace TT_Games_Explorer.Formats.FormatHelpers.MESH
{
    public class MESHA9 : MESH30
    {
        public MESHA9(byte[] fileData, int iPos)
            : base(fileData, iPos)
        {
        }

        protected override VertexList ReadVertexList(int numberofvertices)
        {
            VertexList vertexList = new VertexList();
            iPos += 4;
            iPos += 4;
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
            iPos += 6;
            ColoredConsole.WriteLine("{0:x8}           Number of Vertices: {1:x8}", iPos, numberofvertices);
            for (int i = 0; i < numberofvertices; i++)
            {
                vertexList.Vertices.Add(ReadVertex(vertexList.VertexDefinitions));
            }
            return vertexList;
        }
    }
}