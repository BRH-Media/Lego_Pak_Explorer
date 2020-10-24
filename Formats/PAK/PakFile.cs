namespace TT_Games_Explorer.Formats.PAK
{
    public class PakFile
    {
        public PakFile(PakPkgInfo pakFileInfo)
        {
            PakFileInfo = pakFileInfo;
        }

        public PakPkgInfo PakFileInfo { get; }
        public PakFileEntry[] Files { get; }
    }
}