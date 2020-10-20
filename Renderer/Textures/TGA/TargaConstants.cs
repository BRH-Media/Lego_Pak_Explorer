namespace TT_Games_Explorer.Renderer.Textures.TGA
{
    internal static class TargaConstants
    {
        // constant byte lengths for various fields in the Targa format
        internal const int HEADER_BYTE_LENGTH = 18;

        internal const int FOOTER_BYTE_LENGTH = 26;
        internal const int FOOTER_SIGNATURE_OFFSET_FROM_END = 18;
        internal const int FOOTER_SIGNATURE_BYTE_LENGTH = 16;
        internal const int FOOTER_RESERVED_CHAR_BYTE_LENGTH = 1;
        internal const int EXTENSION_AREA_AUTHOR_NAME_BYTE_LENGTH = 41;
        internal const int EXTENSION_AREA_AUTHOR_COMMENTS_BYTE_LENGTH = 324;
        internal const int EXTENSION_AREA_JOB_NAME_BYTE_LENGTH = 41;
        internal const int EXTENSION_AREA_SOFTWARE_ID_BYTE_LENGTH = 41;
        internal const int EXTENSION_AREA_SOFTWARE_VERSION_LETTER_BYTE_LENGTH = 1;
        internal const int EXTENSION_AREA_COLOR_CORRECTION_TABLE_VALUE_LENGTH = 256;
        internal const string TARGA_FOOTER_ASCII_SIGNATURE = "TRUEVISION-XFILE";
    }
}