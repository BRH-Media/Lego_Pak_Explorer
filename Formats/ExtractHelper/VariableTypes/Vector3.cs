namespace TT_Games_Explorer.Formats.ExtractHelper.VariableTypes
{
    public class Vector3 : Vector2
    {
        public float Z;

        public override bool Equals(object obj) => obj is Vector3 vector3 && (vector3.X.Equals(X) && vector3.Y.Equals(Y) && vector3.Z.Equals(Z));

        public bool Equals(Vector3 obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return ReferenceEquals(this, obj) || obj.Z == (double)Z;
        }

        public override int GetHashCode() => Z.GetHashCode();

        public string ToString(float scale) =>
            $"{(float)(X * (double)scale):0.000000} {(float)(Y * (double)scale):0.000000} {(float)(Z * (double)scale):0.000000} "
                .Replace(',', '.');
    }
}