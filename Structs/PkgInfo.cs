namespace TT_Games_Explorer.Structs
{
    public struct PkgInfo
    {
        public int Version;
        public string FilePath;
        public uint FilesNumber;
        public uint NamesNumber;
        public uint DirNumber;
        public uint InfoFilesOffset;
        public uint InfoFilesSize;
        public uint NamesNumberOffset;
        public uint NamesOffset;
        public uint NamesCrcOffset;
        public uint TypeUnk;
        public uint NameFieldSize;
    }
}