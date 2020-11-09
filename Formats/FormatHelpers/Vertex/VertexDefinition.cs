namespace TT_Games_Explorer.Formats.FormatHelpers.Vertex
{
    public class VertexDefinition
    {
        public VariableEnum Variable;
        public VariableTypeEnum VariableType;
        public int Offset;

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
            lightColSet,
        }

        public enum VariableTypeEnum
        {
            vec2float = 2,
            vec3float = 3,
            vec4float = 4,
            vec2half = 5,
            vec4half = 6,
            vec4char = 7,
            vec4mini = 8,
            color4char = 9,
        }
    }
}