namespace TT_Games_Explorer.Common
{
    /// <summary>
    /// Commonly accessed functions and values
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// Stores contributors in a 2D array
        /// </summary>
        public static string[][] Credits { get; } =
        {
            new[] {
                @"Based off of the original by",
                @"Ac_K"
            },
            new[] {
                @"Decompression routines written by",
                @"Luigi Auriemma"
            },
            new[]
            {
                @"TargaImage Bitmap parser by",
                @"David Polomis"
            },
            new[] {
                @"Syntax highlighting written by",
                @"Pavel Torgashov"
            },
            new[] {
                @"DDS Loader and parser written by",
                @"Kons"
            },
            new[] {
                @"Icon pack by",
                @"FamFamFam and Dave Brasgalla"
            }
        };

        /// <summary>
        /// Manages recent game listings
        /// </summary>
        public static MruManager MruManager;
    }
}