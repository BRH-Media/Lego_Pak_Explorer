namespace TT_Games_Explorer.Formats.FormatHelpers.Vertex
{
    public class VertexDefinition
    {
        public enum VariableEnum
        {
            position,
            normal,
            colorSet0,
            tangent,
            colorSet1,
            uvSet01,
            unknown6,
            uvSet2,
            unknown8,
            blendIndices0,
            blendWeight0,
            unknown11,
            lightDirSet,
            lightColSet
        }

        public enum VariableTypeEnum
        {
            vec2float = 2,
            vec3float,
            vec4float,
            vec2half,
            vec4half,
            vec4char,
            vec4mini,
            color4char
        }

        public VariableEnum Variable;

        public VariableTypeEnum VariableType;

        public int Offset;
    }
}