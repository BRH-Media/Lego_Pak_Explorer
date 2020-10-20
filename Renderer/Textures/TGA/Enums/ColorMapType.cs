namespace TT_Games_Explorer.Renderer.Textures.TGA.Enums
{
    /// <summary>
    /// Indicates the type of color map, if any, included with the image file.
    /// </summary>
    public enum ColorMapType : byte
    {
        /// <summary>
        /// No color map was included in the file.
        /// </summary>
        NoColorMap = 0,

        /// <summary>
        /// Color map was included in the file.
        /// </summary>
        ColorMapIncluded = 1
    }
}