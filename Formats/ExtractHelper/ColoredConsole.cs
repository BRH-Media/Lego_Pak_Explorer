using System;

// ReSharper disable All

namespace TT_Games_Explorer.Formats.GHG.ExtractHelper
{
    public class ColoredConsole
    {
        private static bool _writeDebug = true;
        private static bool _writeInfo = true;
        private static bool _writeWarn = true;
        private static bool _writeError = true;
        private static bool _writePlain = true;

        public static void SetWriteInfo(bool writeInfo) => _writeInfo = writeInfo;

        public static void WriteLine()
        {
            if (!_writePlain)
                return;
            Console.WriteLine();
        }

        public static void Write(string format, params object[] values)
        {
            if (!_writePlain)
                return;
            Console.Write(format, values);
        }

        public static void WriteLine(string format, params object[] values)
        {
            if (!_writePlain)
                return;
            Console.WriteLine(format, values);
        }

        public static void WriteDebug(string format, params object[] values)
        {
            if (!_writeDebug)
                return;
            Console.Write(format, values);
        }

        public static void WriteLineDebug(string format, params object[] values)
        {
            if (!_writeDebug)
                return;
            Console.WriteLine(format, values);
        }

        public static void WriteInfo(string format, params object[] values)
        {
            if (!_writeInfo)
                return;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(format, values);
            Console.ResetColor();
        }

        public static void WriteLineInfo(string format, params object[] values)
        {
            if (!_writeInfo)
                return;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(format, values);
            Console.ResetColor();
        }

        public static void WriteWarn(string format, params object[] values)
        {
            if (!_writeWarn)
                return;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(format, values);
            Console.ResetColor();
        }

        public static void WriteLineWarn(string format, params object[] values)
        {
            if (!_writeWarn)
                return;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(format, values);
            Console.ResetColor();
        }

        public static void WriteError(string format, params object[] values)
        {
            if (!_writeError)
                return;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(format, values);
            Console.ResetColor();
        }

        public static void WriteLineError(string format, params object[] values)
        {
            if (!_writeError)
                return;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(format, values);
            Console.ResetColor();
        }
    }
}