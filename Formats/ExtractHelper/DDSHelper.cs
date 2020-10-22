using System;
using System.Text;

namespace TT_Games_Explorer.Formats.ExtractHelper
{
    public class DdsHelper
    {
        private static readonly ASCIIEncoding AsciiEncoding = new ASCIIEncoding();

        public static int CalculateDdsFileSize(int pos, byte[] fileData)
        {
            if (fileData[pos] != 68 || fileData[pos + 1] != 68 || fileData[pos + 2] != 83)
                throw new ApplicationException($"No DDS file at {pos:x8}");
            if (fileData[pos + 84] == 68 && fileData[pos + 85] == 88 && fileData[pos + 86] == 84)
            {
                if (fileData[pos + 87] == 49)
                    return CalculateDxt1FileSize(pos, fileData);
                return fileData[pos + 87] == (byte)53 ? CalculateDxt5FileSize(pos, fileData) : throw new NotSupportedException("DXT" + AsciiEncoding.GetString(fileData, pos + 87, 1));
            }
            var int32 = BitConverter.ToInt32(fileData, pos + 84);
            if (int32 == 116)
                return CalculateD3DFORMAT_74FileSize(pos, fileData);
            throw new NotSupportedException($"_D3DFORMAT {int32:x8}");
        }

        private static int CalculateD3DFORMAT_74FileSize(int pos, byte[] fileData)
        {
            var int321 = BitConverter.ToInt32(fileData, pos + 12);
            var int322 = BitConverter.ToInt32(fileData, pos + 16);
            var int323 = BitConverter.ToInt32(fileData, pos + 28);
            Console.WriteLine("{0:x8} DDS D3DFORMAT_74 height={1} width={2} mipmaps={3}", pos, int321, int322, int323);
            if (int323 != 1)
                throw new NotSupportedException($"D3DFORMAT_74 mipmaps {int323:x8}");
            var num = int321 * int322 * 16 + 128;
            Console.WriteLine("{0:x8} calculatedsize = {1:x8}", pos, num);
            return num;
        }

        private static int CalculateDxt5FileSize(int pos, byte[] fileData)
        {
            var num1 = BitConverter.ToInt32(fileData, pos + 12);
            var num2 = BitConverter.ToInt32(fileData, pos + 16);
            var int321 = BitConverter.ToInt32(fileData, pos + 28);
            var int322 = BitConverter.ToInt32(fileData, pos + 112);
            Console.WriteLine("{0:x8} DDS DXT5 height={1} width={2} mipmaps={3} cubemapflags={4:x8}", (object)pos, (object)num1, (object)num2, (object)int321, (object)int322);
            var num3 = num1 * num2;
            for (var index = 1; index < int321; ++index)
            {
                num1 = num1 >> 1 < 4 ? 4 : num1 >> 1;
                num2 = num2 >> 1 < 4 ? 4 : num2 >> 1;
                num3 += num1 * num2;
            }
            if (int322 != 0)
                num3 *= 6;
            Console.WriteLine("{0:x8} calculatedsize = {1:x8}", pos, num3 + 128);
            return num3 + 128;
        }

        private static int CalculateDxt1FileSize(int pos, byte[] fileData)
        {
            var num1 = BitConverter.ToInt32(fileData, pos + 12);
            var num2 = BitConverter.ToInt32(fileData, pos + 16);
            var int321 = BitConverter.ToInt32(fileData, pos + 28);
            var int322 = BitConverter.ToInt32(fileData, pos + 112);
            Console.WriteLine("{0:x8} DDS DXT1 height={1} width={2} mipmaps={3} cubemapflags={4:x8}", (object)pos, (object)num1, (object)num2, (object)int321, (object)int322);
            var num3 = num1 * num2 >> 1;
            for (var index = 1; index < int321; ++index)
            {
                num1 = num1 >> 1 < 4 ? 4 : num1 >> 1;
                num2 = num2 >> 1 < 4 ? 4 : num2 >> 1;
                num3 += num1 * num2 >> 1;
            }
            if (int322 != 0)
                num3 *= 6;
            Console.WriteLine("{0:x8} calculatedsize = {1:x8}", pos, num3 + 128);
            return num3 + 128;
        }
    }
}