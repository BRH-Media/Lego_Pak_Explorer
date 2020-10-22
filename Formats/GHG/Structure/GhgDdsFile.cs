namespace TT_Games_Explorer.Formats.GHG.Structure
{
    /// <summary>
    /// This isn't a renderer! It represents a data structure that must be manually rendered using TexTrend
    /// </summary>
    public class GhgDdsFile
    {
        public byte[] TextureData { get; set; }
        public string FileName { get; set; }
        public string ParentName { get; set; }

        /// <summary>
        /// Stores necessary information relating to texturing the related model file
        /// </summary>
        public string MaterialSettings => GhgMtlSettings.MtlFileContent(FileName);
    }
}