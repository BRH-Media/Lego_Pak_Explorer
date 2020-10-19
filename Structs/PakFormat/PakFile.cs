namespace TT_Games_Explorer.Structs.PakFormat
{
    public class PakFile
    {
        public PakFile(PakPkgInfo pakFileInfo)
        {
            var magicHeader = new byte[4];


            PakFileInfo = pakFileInfo;
        }

        public PakPkgInfo PakFileInfo { get; }
        public PakFileEntry[] Files { get; }
    }
}