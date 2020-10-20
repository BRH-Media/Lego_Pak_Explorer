namespace TT_Games_Explorer.Renderer.Textures.TGA.Enums
{
    /// <summary>
    /// The type of image read from the file.
    /// </summary>
    public enum ImageType : byte
    {
        /// <summary>
        /// No image data was found in file.
        /// </summary>
        NoImageData = 0,

        /// <summary>
        /// Image is an uncompressed, indexed color-mapped image.
        /// </summary>
        UncompressedColorMapped = 1,

        /// <summary>
        /// Image is an uncompressed, RGB image.
        /// </summary>
        UncompressedTrueColor = 2,

        /// <summary>
        /// Image is an uncompressed, Greyscale image.
        /// </summary>
        UncompressedBlackAndWhite = 3,

        /// <summary>
        /// Image is a compressed, indexed color-mapped image.
        /// </summary>
        RunLengthEncodedColorMapped = 9,

        /// <summary>
        /// Image is a compressed, RGB image.
        /// </summary>
        RunLengthEncodedTrueColor = 10,

        /// <summary>
        /// Image is a compressed, Greyscale image.
        /// </summary>
        RunLengthEncodedBlackAndWhite = 11
    }
}