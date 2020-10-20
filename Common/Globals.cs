using System.Collections.Generic;

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

        /// <summary>
        /// Stores extensions and their description mappings
        /// </summary>
        public static Dictionary<string, string> LegoFileType { get; } = new Dictionary<string, string>
        {
            {
                ".cfg",
                "Configuration File"
            },
            {
                ".csv",
                "Comma Separated Values File"
            },
            {
                ".dat",
                "Data Archive File"
            },
            {
                ".pak",
                "Data Archive File"
            },
            {
                ".dds",
                "Direct Draw Surface File"
            },
            {
                ".exe",
                "Executable File"
            },
            {
                ".dll",
                "Dynamic-Link Library File"
            },
            {
                ".fmv",
                "Full Motion Video File"
            },
            {
                ".hdr",
                "Header Data Archive File"
            },
            {
                ".ogg",
                "OGG Vorbis Sound File"
            },
            {
                ".png",
                "Portable Network Graphics File"
            },
            {
                ".tga",
                "Targa Image File"
            },
            {
                ".ini",
                "Configuration File"
            },
            {
                ".inf",
                "Setup Information File"
            },
            {
                ".jpg",
                "JPEG Image File"
            },
            {
                ".ico",
                "Windows Icon File"
            },
            {
                ".jpeg",
                "JPEG Image File"
            },
            {
                ".sf",
                "Script File"
            },
            {
                ".scp",
                "Script File"
            },
            {
                ".an3",
                "Animation Config File"
            },
            {
                ".tex",
                "Texture File"
            },
            {
                ".txt",
                "Text File"
            },
            {
                ".wav",
                "Waveform Audio File"
            },
            {
                ".xma",
                "Xbox360 Media Audio File"
            },
            {
                ".obj",
                "3D Object File"
            },
            {
                ".ghg",
                "Complex Model File"
            },
            {
                ".gsc",
                "Basic Model File"
            },
            {
                ".cab",
                "Windows Cabinet File"
            },
            {
                ".rtf",
                "Rich Text File"
            },
            {
                ".pdf",
                "Portable Document File"
            },
            {
                ".cbx",
                "LEGO Audio File"
            },
            {
                ".vdf",
                "Valve Script File"
            }
        };
    }
}