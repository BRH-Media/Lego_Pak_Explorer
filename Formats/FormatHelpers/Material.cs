namespace TT_Games_Explorer.Formats.FormatHelpers
{
    public class Material
    {
        public string Name;
        public int NormalTexture;
        public int Texture;

        public Material(string name, int texture, int norm)
        {
            Name = name;
            Texture = texture;
            NormalTexture = norm;
        }
    }
}