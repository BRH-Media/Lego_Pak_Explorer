namespace TT_Games_Explorer.Renderer.Textures.TGA.Enums
{
    /// <summary>
    /// The left-to-right ordering in which pixel data is transferred from the file to the screen.
    /// </summary>
    public enum HorizontalTransferOrder
    {
        /// <summary>
        /// Unknown transfer order.
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// Transfer order of pixels is from the right to left.
        /// </summary>
        Right = 0,

        /// <summary>
        /// Transfer order of pixels is from the left to right.
        /// </summary>
        Left = 1
    }
}