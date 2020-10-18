using System;
using System.Collections.Generic;

namespace Lego_Pak_Explorer
{
    /// <summary>
    /// Commonly accessed functions and values
    /// </summary>
    public static class Common
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
        /// Converts a byte count into a human-readable string
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="includeSpace"></param>
        /// <returns></returns>
        public static string FormatSize(long bytes, bool includeSpace = false)
        {
            string[] suffix =
            {
                "B", "KB", "MB", "GB", "TB"
            };

            int i;
            double dblSByte = bytes;

            for (i = 0; i < suffix.Length && bytes >= 1024; i++, bytes /= 1024)
                dblSByte = bytes / 1024.0;

            return includeSpace
                ? $"{dblSByte:0.##} {suffix[i]}"
                : $"{dblSByte:0.##}{suffix[i]}";
        }

        /// <summary>
        /// Manages recent game listings
        /// </summary>
        public static MruManager MruManager;

        /// <summary>
        /// Maps extensions to their ListView description
        /// </summary>
        public static string GetLegoFileType(string ext)
        {
            try
            {
                return LegoFileType[ext.ToLower()];
            }
            catch (Exception)
            {
                //ignore
            }

            //default
            return @"Unknown File";
        }

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
                ".sf",
                "Script File"
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
            }
        };
    }
}