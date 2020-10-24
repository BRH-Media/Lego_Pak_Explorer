using System.Collections.Generic;
using TT_Games_Explorer.Formats.FormatHelpers.Vertex;

namespace TT_Games_Explorer.Formats.FormatHelpers
{
    public class Part
    {
        public List<VertexListReference> VertexListReferences1 = new List<VertexListReference>();

        public List<VertexListReference> VertexListReferences11;

        public List<VertexListReference> VertexListReferences2 = new List<VertexListReference>();

        public int IndexListReference1;

        public int IndexListReference2;

        public int OffsetIndices;

        public int NumberIndices;

        public int OffsetVertices;

        public int NumberVertices;

        public int OffsetIndices2;

        public int OffsetVertices2;

        public int NumberVertices2;
    }
}