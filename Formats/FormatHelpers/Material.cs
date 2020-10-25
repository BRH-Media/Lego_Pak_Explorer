namespace TT_Games_Explorer.Formats.FormatHelpers
{
    public class Material
    {
        public string Name;
        public int NormalTexture;
        public int Texture;

        public Material(string name, int texture, int norm)
        {
            this.Name = name;
            this.Texture = texture;
            this.NormalTexture = norm;
        }
    }
}