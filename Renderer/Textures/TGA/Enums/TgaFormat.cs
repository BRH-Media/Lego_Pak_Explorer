namespace TT_Games_Explorer.Renderer.Textures.TGA.Enums
{
    /// <summary>
    /// The Targa format of the file.
    /// </summary>
    public enum TgaFormat
    {
        /// <summary>
        /// Unknown Targa Image format.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Original Targa Image format.
        /// </summary>
        /// <remarks>Targa Image does not have a Signature of ""TRUEVISION-XFILE"".</remarks>
        OriginalTga = 100,

        /// <summary>
        /// New Targa Image format
        /// </summary>
        /// <remarks>Targa Image has a TargaFooter with a Signature of ""TRUEVISION-XFILE"".</remarks>
        NewTga = 200
    }
}