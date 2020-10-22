using TT_Games_Explorer.Formats.ExtractHelper.VariableTypes;

namespace TT_Games_Explorer.Formats.ExtractHelper.LDraw
{
    public class OptionalLine
    {
        public Vector3 X;
        public Vector3 Y;
        public Vector3 A;
        public Vector3 B = null;
        public Vector3 Nx;
        public Vector3 Ny;
        public int Ix;
        public int Iy;

        public OptionalLine(Vector3 x, Vector3 y, Vector3 z, Vector3 nx, Vector3 ny)
        {
            X = x;
            Y = y;
            A = z;
            Nx = nx;
            Ny = ny;
        }

        public override string ToString() => "(" + X + ", " + Y + ")";
    }
}