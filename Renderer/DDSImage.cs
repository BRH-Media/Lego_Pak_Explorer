using System;
using System.Drawing;
using System.IO;

// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

#pragma warning disable 169

namespace Lego_Pak_Explorer.Renderer
{
    public class DDSImage
    {
        private const int DDPF_ALPHAPIXELS = 1;
        private const int DDPF_ALPHA = 2;
        private const int DDPF_FOURCC = 4;
        private const int DDPF_RGB = 64;
        private const int DDPF_YUV = 512;
        private const int DDPF_LUMINANCE = 131072;
        private const int DDSD_MIPMAPCOUNT = 131072;
        private const int FOURCC_DXT1 = 827611204;
        private const int FOURCC_DX10 = 808540228;
        private const int FOURCC_DXT5 = 894720068;
        public int dwMagic;
        private DDS_HEADER header = new DDS_HEADER();
        private DDS_HEADER_DXT10 header10;
        public byte[] bdata;
        public byte[] bdata2;
        public Bitmap[] images;

        public DDSImage(byte[] rawdata)
        {
            using (var memoryStream = new MemoryStream(rawdata))
            {
                using (var r = new BinaryReader(memoryStream))
                {
                    dwMagic = r.ReadInt32();
                    if (dwMagic != 542327876)
                        throw new Exception("This is not a DDS!");
                    Read_DDS_HEADER(header, r);
                    if ((header.ddspf.dwFlags & 4) != 0 && header.ddspf.dwFourCC == 808540228)
                        throw new Exception("DX10 not supported yet!");
                    var length = 1;
                    if ((header.dwFlags & 131072) != 0)
                        length = header.dwMipMapCount;
                    images = new Bitmap[length];
                    bdata = r.ReadBytes(header.dwPitchOrLinearSize);
                    for (var index = 0; index < length; ++index)
                    {
                        var w = (int)(header.dwWidth / Math.Pow(2.0, index));
                        var h = (int)(header.dwHeight / Math.Pow(2.0, index));
                        if ((header.ddspf.dwFlags & 64) != 0)
                            images[index] = readLinearImage(bdata, w, h);
                        else if ((header.ddspf.dwFlags & 4) != 0)
                            images[index] = readBlockImage(bdata, w, h);
                        else if ((header.ddspf.dwFlags & 4) == 0 && header.ddspf.dwRGBBitCount == 16 && (header.ddspf.dwRBitMask == byte.MaxValue && header.ddspf.dwGBitMask == 65280) && (header.ddspf.dwBBitMask == 0 && header.ddspf.dwABitMask == 0))
                            images[index] = UncompressV8U8(bdata, w, h);
                    }
                }
            }
        }

        private Bitmap readBlockImage(byte[] data, int w, int h)
        {
            switch (header.ddspf.dwFourCC)
            {
                case 827611204:
                    return UncompressDXT1(data, w, h);

                case 894720068:
                    return UncompressDXT5(data, w, h);

                default:
                    throw new Exception($"0x{header.ddspf.dwFourCC.ToString("X")} texture compression not implemented.");
            }
        }

        private Bitmap UncompressDXT1(byte[] data, int w, int h)
        {
            var image = new Bitmap(w < 4 ? 4 : w, h < 4 ? 4 : h);
            using (var memoryStream = new MemoryStream(data))
            {
                using (var binaryReader = new BinaryReader(memoryStream))
                {
                    for (var y = 0; y < h; y += 4)
                    {
                        for (var x = 0; x < w; x += 4)
                            DecompressBlockDXT1(x, y, binaryReader.ReadBytes(8), image);
                    }
                }
            }
            return image;
        }

        private void DecompressBlockDXT1(int x, int y, byte[] blockStorage, Bitmap image)
        {
            var num1 = (ushort)(blockStorage[0] | (uint)blockStorage[1] << 8);
            var num2 = (ushort)(blockStorage[2] | (uint)blockStorage[3] << 8);
            var num3 = (num1 >> 11) * byte.MaxValue + 16;
            var num4 = (byte)((num3 / 32 + num3) / 32);
            var num5 = ((num1 & 2016) >> 5) * byte.MaxValue + 32;
            var num6 = (byte)((num5 / 64 + num5) / 64);
            var num7 = (num1 & 31) * byte.MaxValue + 16;
            var num8 = (byte)((num7 / 32 + num7) / 32);
            var num9 = (num2 >> 11) * byte.MaxValue + 16;
            var num10 = (byte)((num9 / 32 + num9) / 32);
            var num11 = ((num2 & 2016) >> 5) * byte.MaxValue + 32;
            var num12 = (byte)((num11 / 64 + num11) / 64);
            var num13 = (num2 & 31) * byte.MaxValue + 16;
            var num14 = (byte)((num13 / 32 + num13) / 32);
            var num15 = (uint)(blockStorage[4] | blockStorage[5] << 8 | blockStorage[6] << 16 | blockStorage[7] << 24);
            for (var index1 = 0; index1 < 4; ++index1)
            {
                for (var index2 = 0; index2 < 4; ++index2)
                {
                    var color = Color.FromArgb(0);
                    var num16 = (byte)(num15 >> 2 * (4 * index1 + index2) & 3U);
                    if (num1 > num2)
                    {
                        switch (num16)
                        {
                            case 0:
                                color = Color.FromArgb(byte.MaxValue, num4, num6, num8);
                                break;

                            case 1:
                                color = Color.FromArgb(byte.MaxValue, num10, num12, num14);
                                break;

                            case 2:
                                color = Color.FromArgb(byte.MaxValue, (2 * num4 + num10) / 3, (2 * num6 + num12) / 3, (2 * num8 + num14) / 3);
                                break;

                            case 3:
                                color = Color.FromArgb(byte.MaxValue, (num4 + 2 * num10) / 3, (num6 + 2 * num12) / 3, (num8 + 2 * num14) / 3);
                                break;
                        }
                    }
                    else
                    {
                        switch (num16)
                        {
                            case 0:
                                color = Color.FromArgb(byte.MaxValue, num4, num6, num8);
                                break;

                            case 1:
                                color = Color.FromArgb(byte.MaxValue, num10, num12, num14);
                                break;

                            case 2:
                                color = Color.FromArgb(byte.MaxValue, (num4 + num10) / 2, (num6 + num12) / 2, (num8 + num14) / 2);
                                break;

                            case 3:
                                color = Color.FromArgb(byte.MaxValue, 0, 0, 0);
                                break;
                        }
                    }
                    image.SetPixel(x + index2, y + index1, color);
                }
            }
        }

        private Bitmap UncompressDXT5(byte[] data, int w, int h)
        {
            var image = new Bitmap(w < 4 ? 4 : w, h < 4 ? 4 : h);
            using (var memoryStream = new MemoryStream(data))
            {
                using (var binaryReader = new BinaryReader(memoryStream))
                {
                    for (var y = 0; y < h; y += 4)
                    {
                        for (var x = 0; x < w; x += 4)
                            DecompressBlockDXT5(x, y, binaryReader.ReadBytes(16), image);
                    }
                }
            }
            return image;
        }

        private void DecompressBlockDXT5(int x, int y, byte[] blockStorage, Bitmap image)
        {
            var num1 = blockStorage[0];
            var num2 = blockStorage[1];
            var index1 = 2;
            var num3 = (uint)(blockStorage[index1 + 2] | blockStorage[index1 + 3] << 8 | blockStorage[index1 + 4] << 16 | blockStorage[index1 + 5] << 24);
            var num4 = (ushort)(blockStorage[index1] | (uint)blockStorage[index1 + 1] << 8);
            var num5 = (ushort)(blockStorage[8] | (uint)blockStorage[9] << 8);
            var num6 = (ushort)(blockStorage[10] | (uint)blockStorage[11] << 8);
            var num7 = (num5 >> 11) * byte.MaxValue + 16;
            var num8 = (byte)((num7 / 32 + num7) / 32);
            var num9 = ((num5 & 2016) >> 5) * byte.MaxValue + 32;
            var num10 = (byte)((num9 / 64 + num9) / 64);
            var num11 = (num5 & 31) * byte.MaxValue + 16;
            var num12 = (byte)((num11 / 32 + num11) / 32);
            var num13 = (num6 >> 11) * byte.MaxValue + 16;
            var num14 = (byte)((num13 / 32 + num13) / 32);
            var num15 = ((num6 & 2016) >> 5) * byte.MaxValue + 32;
            var num16 = (byte)((num15 / 64 + num15) / 64);
            var num17 = (num6 & 31) * byte.MaxValue + 16;
            var num18 = (byte)((num17 / 32 + num17) / 32);
            var num19 = (uint)(blockStorage[12] | blockStorage[13] << 8 | blockStorage[14] << 16 | blockStorage[15] << 24);
            for (var index2 = 0; index2 < 4; ++index2)
            {
                for (var index3 = 0; index3 < 4; ++index3)
                {
                    var num20 = 3 * (4 * index2 + index3);
                    var num21 = num20 > 12 ? (num20 != 15 ? (int)(num3 >> num20 - 16) & 7 : (int)(num4 >> 15 | (uint)((int)num3 << 1 & 6))) : num4 >> num20 & 7;
                    byte num22;
                    switch (num21)
                    {
                        case 0:
                            num22 = num1;
                            break;

                        case 1:
                            num22 = num2;
                            break;

                        default:
                            if (num1 > num2)
                            {
                                num22 = (byte)(((8 - num21) * num1 + (num21 - 1) * num2) / 7);
                                break;
                            }
                            switch (num21)
                            {
                                case 6:
                                    num22 = 0;
                                    break;

                                case 7:
                                    num22 = byte.MaxValue;
                                    break;

                                default:
                                    num22 = (byte)(((6 - num21) * num1 + (num21 - 1) * num2) / 5);
                                    break;
                            }
                            break;
                    }
                    var num23 = (byte)(num19 >> 2 * (4 * index2 + index3) & 3U);
                    var color = new Color();
                    switch (num23)
                    {
                        case 0:
                            color = Color.FromArgb(num22, num8, num10, num12);
                            break;

                        case 1:
                            color = Color.FromArgb(num22, num14, num16, num18);
                            break;

                        case 2:
                            color = Color.FromArgb(num22, (2 * num8 + num14) / 3, (2 * num10 + num16) / 3, (2 * num12 + num18) / 3);
                            break;

                        case 3:
                            color = Color.FromArgb(num22, (num8 + 2 * num14) / 3, (num10 + 2 * num16) / 3, (num12 + 2 * num18) / 3);
                            break;
                    }
                    image.SetPixel(x + index3, y + index2, color);
                }
            }
        }

        private Bitmap UncompressV8U8(byte[] data, int w, int h)
        {
            var bitmap = new Bitmap(w, h);
            using (var memoryStream = new MemoryStream(data))
            {
                using (var binaryReader = new BinaryReader(memoryStream))
                {
                    for (var y = 0; y < h; ++y)
                    {
                        for (var x = 0; x < w; ++x)
                        {
                            var num1 = binaryReader.ReadSByte();
                            var num2 = binaryReader.ReadSByte();
                            var maxValue = byte.MaxValue;
                            bitmap.SetPixel(x, y, Color.FromArgb(sbyte.MaxValue - num1, sbyte.MaxValue - num2, maxValue));
                        }
                    }
                }
            }
            return bitmap;
        }

        private Bitmap readLinearImage(byte[] data, int w, int h)
        {
            var bitmap = new Bitmap(w, h);
            using (var memoryStream = new MemoryStream(data))
            {
                using (var binaryReader = new BinaryReader(memoryStream))
                {
                    for (var y = 0; y < h; ++y)
                    {
                        for (var x = 0; x < w; ++x)
                            bitmap.SetPixel(x, y, Color.FromArgb(binaryReader.ReadInt32()));
                    }
                }
            }
            return bitmap;
        }

        private void Read_DDS_HEADER(DDS_HEADER h, BinaryReader r)
        {
            h.dwSize = r.ReadInt32();
            h.dwFlags = r.ReadInt32();
            h.dwHeight = r.ReadInt32();
            h.dwWidth = r.ReadInt32();
            h.dwPitchOrLinearSize = r.ReadInt32();
            h.dwDepth = r.ReadInt32();
            h.dwMipMapCount = r.ReadInt32();
            for (var index = 0; index < 11; ++index)
                h.dwReserved1[index] = r.ReadInt32();
            Read_DDS_PIXELFORMAT(h.ddspf, r);
            h.dwCaps = r.ReadInt32();
            h.dwCaps2 = r.ReadInt32();
            h.dwCaps3 = r.ReadInt32();
            h.dwCaps4 = r.ReadInt32();
            h.dwReserved2 = r.ReadInt32();
        }

        private void Read_DDS_PIXELFORMAT(DDS_PIXELFORMAT p, BinaryReader r)
        {
            p.dwSize = r.ReadInt32();
            p.dwFlags = r.ReadInt32();
            p.dwFourCC = r.ReadInt32();
            p.dwRGBBitCount = r.ReadInt32();
            p.dwRBitMask = r.ReadInt32();
            p.dwGBitMask = r.ReadInt32();
            p.dwBBitMask = r.ReadInt32();
            p.dwABitMask = r.ReadInt32();
        }
    }
}