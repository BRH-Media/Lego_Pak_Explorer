namespace Lego_Pak_Explorer.Structs
{
    public struct DatFileEntry
    {
        public string Name;
        public string Parent;
        public int Crc;
        public uint Offset;
        public byte Offset2;
        public uint Size;
        public uint SizeUnComp;
        public uint Pack;
    }
}