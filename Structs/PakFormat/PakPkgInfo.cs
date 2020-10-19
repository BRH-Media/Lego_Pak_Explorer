namespace TT_Games_Explorer.Structs.PakFormat
{
    public struct PakPkgInfo
    {
        public byte[] Magic;
        public uint FilesNumber;
        public uint NamesOffset;
    }
}