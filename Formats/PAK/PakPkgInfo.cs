namespace TT_Games_Explorer.Formats.PAK
{
    public struct PakPkgInfo
    {
        public byte[] Magic;
        public uint FilesNumber;
        public uint NamesOffset;
    }
}