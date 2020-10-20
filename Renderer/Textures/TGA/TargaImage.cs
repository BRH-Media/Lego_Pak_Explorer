// ==========================================================
// TargaImage
//
// Design and implementation by
// - David Polomis (paloma_sw@cox.net)
//
//
// This source code, along with any associated files, is licensed under
// The Code Project Open License (CPOL) 1.02
// A copy of this license can be found in the CPOL.html file
// which was downloaded with this source code
// or at http://www.codeproject.com/info/cpol10.aspx
//
//
// COVERED CODE IS PROVIDED UNDER THIS LICENSE ON AN "AS IS" BASIS,
// WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED,
// INCLUDING, WITHOUT LIMITATION, WARRANTIES THAT THE COVERED CODE IS
// FREE OF DEFECTS, MERCHANTABLE, FIT FOR A PARTICULAR PURPOSE OR
// NON-INFRINGING. THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE
// OF THE COVERED CODE IS WITH YOU. SHOULD ANY COVERED CODE PROVE
// DEFECTIVE IN ANY RESPECT, YOU (NOT THE INITIAL DEVELOPER OR ANY
// OTHER CONTRIBUTOR) ASSUME THE COST OF ANY NECESSARY SERVICING,
// REPAIR OR CORRECTION. THIS DISCLAIMER OF WARRANTY CONSTITUTES AN
// ESSENTIAL PART OF THIS LICENSE. NO USE OF ANY COVERED CODE IS
// AUTHORIZED HEREUNDER EXCEPT UNDER THIS DISCLAIMER.
//
// Use at your own risk!
//
// ==========================================================

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using TT_Games_Explorer.Renderer.Textures.TGA.Common;
using TT_Games_Explorer.Renderer.Textures.TGA.Enums;
using ColorMapType = TT_Games_Explorer.Renderer.Textures.TGA.Enums.ColorMapType;

namespace TT_Games_Explorer.Renderer.Textures.TGA
{
    /// <summary>
    /// Reads and loads a Truevision TGA Format image file.
    /// </summary>
    public class TargaImage : IDisposable

    {
        private GCHandle _imageByteHandle;
        private GCHandle _thumbnailByteHandle;

        // Track whether Dispose has been called.
        private bool _disposed;

        /// <summary>
        /// Creates a new instance of the TargaImage object.
        /// </summary>
        public TargaImage()
        {
            Footer = new TargaFooter();
            Header = new TargaHeader();
            ExtensionArea = new TargaExtensionArea();
            Image = null;
            Thumbnail = null;
        }

        /// <summary>
        /// Gets a TargaHeader object that holds the Targa Header information of the loaded file.
        /// </summary>
        public TargaHeader Header { get; private set; }

        /// <summary>
        /// Gets a TargaExtensionArea object that holds the Targa Extension Area information of the loaded file.
        /// </summary>
        public TargaExtensionArea ExtensionArea { get; private set; }

        /// <summary>
        /// Gets a TargaExtensionArea object that holds the Targa Footer information of the loaded file.
        /// </summary>
        public TargaFooter Footer { get; private set; }

        /// <summary>
        /// Gets the Targa format of the loaded file.
        /// </summary>
        public TgaFormat Format { get; private set; } = TgaFormat.Unknown;

        /// <summary>
        /// Gets a Bitmap representation of the loaded file.
        /// </summary>
        public Bitmap Image { get; private set; }

        /// <summary>
        /// Gets the thumbnail of the loaded file if there is one in the file.
        /// </summary>
        public Bitmap Thumbnail { get; private set; }

        /// <summary>
        /// Gets the full path and filename of the loaded file.
        /// </summary>
        public string FileName { get; private set; } = string.Empty;

        /// <summary>
        /// Gets the byte offset between the beginning of one scan line and the next. Used when loading the image into the Image Bitmap.
        /// </summary>
        /// <remarks>
        /// The memory allocated for Microsoft Bitmaps must be aligned on a 32bit boundary.
        /// The stride refers to the number of bytes allocated for one scanline of the bitmap.
        /// </remarks>
        public int Stride { get; private set; }

        /// <summary>
        /// Gets the number of bytes used to pad each scan line to meet the Stride value. Used when loading the image into the Image Bitmap.
        /// </summary>
        /// <remarks>
        /// The memory allocated for Microsoft Bitmaps must be aligned on a 32bit boundary.
        /// The stride refers to the number of bytes allocated for one scanline of the bitmap.
        /// In your loop, you copy the pixels one scanline at a time and take into
        /// consideration the amount of padding that occurs due to memory alignment.
        /// </remarks>
        public int Padding { get; private set; }

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        /// <summary>
        /// TargaImage deconstructor.
        /// </summary>
        ~TargaImage()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        /// <summary>
        /// Creates a new instance of the TargaImage object with strFileName as the image loaded.
        /// </summary>
        public TargaImage(string strFileName) : this()
        {
            // make sure we have a .tga file
            if (Path.GetExtension(strFileName).ToLower() == ".tga")
            {
                // make sure the file exists
                if (File.Exists(strFileName))
                {
                    FileName = strFileName;

                    // load the file as an array of bytes
                    var fileBytes = File.ReadAllBytes(FileName);
                    if (fileBytes.Length > 0)
                    {
                        // create a seekable memory stream of the file bytes
                        MemoryStream fileStream;
                        using (fileStream = new MemoryStream(fileBytes))
                        {
                            if (fileStream.Length > 0 && fileStream.CanSeek)
                            {
                                // create a BinaryReader used to read the Targa file
                                BinaryReader binReader;
                                using (binReader = new BinaryReader(fileStream))
                                {
                                    LoadTgaFooterInfo(binReader);
                                    LoadTgaHeaderInfo(binReader);
                                    LoadTgaExtensionArea(binReader);
                                    LoadTgaImage(binReader);
                                }
                            }
                            else
                                throw new Exception(@"Error loading file, could not read file from disk.");
                        }
                    }
                    else
                        throw new Exception(@"Error loading file, could not read file from disk.");
                }
                else
                    throw new Exception(@"Error loading file, could not find file '" + strFileName + "' on disk.");
            }
            else
                throw new Exception(@"Error loading file, file '" + strFileName + "' must have an extension of '.tga'.");
        }

        /// <summary>
        /// Creates a new instance of the TargaImage object loading the image data from the provided stream.
        /// </summary>
        public TargaImage(Stream imageStream)
            : this()
        {
            if (imageStream != null && imageStream.Length > 0 && imageStream.CanSeek)
            {
                // create a BinaryReader used to read the Targa file
                using (var binReader = new BinaryReader(imageStream))
                {
                    LoadTgaFooterInfo(binReader);
                    LoadTgaHeaderInfo(binReader);
                    LoadTgaExtensionArea(binReader);
                    LoadTgaImage(binReader);
                }
            }
            else
                throw new ArgumentException(@"Error loading image, Null, zero length or non-seekable stream provided.", nameof(imageStream));
        }

        /// <summary>
        /// Loads the Targa Footer information from the file.
        /// </summary>
        /// <param name="binReader">A BinaryReader that points the loaded file byte stream.</param>
        private void LoadTgaFooterInfo(BinaryReader binReader)
        {
            if (binReader != null && binReader.BaseStream.Length > 0 && binReader.BaseStream.CanSeek)
            {
                try
                {
                    // set the cursor at the beginning of the signature string.
                    binReader.BaseStream.Seek((TargaConstants.FOOTER_SIGNATURE_OFFSET_FROM_END * -1), SeekOrigin.End);

                    // read the signature bytes and convert to ascii string
                    var signature = Encoding.ASCII.GetString(binReader.ReadBytes(TargaConstants.FOOTER_SIGNATURE_BYTE_LENGTH)).TrimEnd('\0');

                    // do we have a proper signature
                    if (string.CompareOrdinal(signature, TargaConstants.TARGA_FOOTER_ASCII_SIGNATURE) == 0)
                    {
                        // this is a NEW targa file.
                        // create the footer
                        Format = TgaFormat.NewTga;

                        // set cursor to beginning of footer info
                        binReader.BaseStream.Seek((TargaConstants.FOOTER_BYTE_LENGTH * -1), SeekOrigin.End);

                        // read the Extension Area Offset value
                        var extOffset = binReader.ReadInt32();

                        // read the Developer Directory Offset value
                        var devDirOff = binReader.ReadInt32();

                        // skip the signature we have already read it.
                        binReader.ReadBytes(TargaConstants.FOOTER_SIGNATURE_BYTE_LENGTH);

                        // read the reserved character
                        var resChar = Encoding.ASCII.GetString(binReader.ReadBytes(TargaConstants.FOOTER_RESERVED_CHAR_BYTE_LENGTH)).TrimEnd('\0');

                        // set all values to our TargaFooter class
                        Footer.SetExtensionAreaOffset(extOffset);
                        Footer.SetDeveloperDirectoryOffset(devDirOff);
                        Footer.SetSignature(signature);
                        Footer.SetReservedCharacter(resChar);
                    }
                    else
                    {
                        // this is not an ORIGINAL targa file.
                        Format = TgaFormat.OriginalTga;
                    }
                }
                catch (Exception ex)
                {
                    // clear all
                    ClearAll();
                    throw ex;
                }
            }
            else
            {
                ClearAll();
                throw new Exception(@"Error loading file, could not read file from disk.");
            }
        }

        /// <summary>
        /// Loads the Targa Header information from the file.
        /// </summary>
        /// <param name="binReader">A BinaryReader that points the loaded file byte stream.</param>
        private void LoadTgaHeaderInfo(BinaryReader binReader)
        {
            if (binReader != null && binReader.BaseStream.Length > 0 && binReader.BaseStream.CanSeek)
            {
                try
                {
                    // set the cursor at the beginning of the file.
                    binReader.BaseStream.Seek(0, SeekOrigin.Begin);

                    // read the header properties from the file
                    Header.SetImageIdLength(binReader.ReadByte());
                    Header.SetColorMapType((ColorMapType)binReader.ReadByte());
                    Header.SetImageType((ImageType)binReader.ReadByte());

                    Header.SetColorMapFirstEntryIndex(binReader.ReadInt16());
                    Header.SetColorMapLength(binReader.ReadInt16());
                    Header.SetColorMapEntrySize(binReader.ReadByte());

                    Header.SetXOrigin(binReader.ReadInt16());
                    Header.SetYOrigin(binReader.ReadInt16());
                    Header.SetWidth(binReader.ReadInt16());
                    Header.SetHeight(binReader.ReadInt16());

                    var pixeldepth = binReader.ReadByte();
                    switch (pixeldepth)
                    {
                        case 8:
                        case 16:
                        case 24:
                        case 32:
                            Header.SetPixelDepth(pixeldepth);
                            break;

                        default:
                            ClearAll();
                            throw new Exception("Targa Image only supports 8, 16, 24, or 32 bit pixel depths.");
                    }

                    var imageDescriptor = binReader.ReadByte();
                    Header.SetAttributeBits((byte)Utilities.GetBits(imageDescriptor, 0, 4));

                    Header.SetVerticalTransferOrder((VerticalTransferOrder)Utilities.GetBits(imageDescriptor, 5, 1));
                    Header.SetHorizontalTransferOrder((HorizontalTransferOrder)Utilities.GetBits(imageDescriptor, 4, 1));

                    // load ImageID value if any
                    if (Header.ImageIdLength > 0)
                    {
                        var imageIdValueBytes = binReader.ReadBytes(Header.ImageIdLength);
                        Header.SetImageIdValue(Encoding.ASCII.GetString(imageIdValueBytes).TrimEnd('\0'));
                    }
                }
                catch (Exception ex)
                {
                    ClearAll();
                    throw ex;
                }

                // load color map if it's included and/or needed
                // Only needed for UNCOMPRESSED_COLOR_MAPPED and RUN_LENGTH_ENCODED_COLOR_MAPPED
                // image types. If color map is included for other file types we can ignore it.
                if (Header.ColorMapType == ColorMapType.ColorMapIncluded)
                {
                    if (Header.ImageType == ImageType.UncompressedColorMapped ||
                        Header.ImageType == ImageType.RunLengthEncodedColorMapped)
                    {
                        if (Header.ColorMapLength > 0)
                        {
                            try
                            {
                                for (var i = 0; i < Header.ColorMapLength; i++)
                                {
                                    int a;
                                    int r;
                                    int g;
                                    int b;

                                    // load each color map entry based on the ColorMapEntrySize value
                                    switch (Header.ColorMapEntrySize)
                                    {
                                        case 15:
                                            var color15 = binReader.ReadBytes(2);
                                            // remember that the bytes are stored in reverse oreder
                                            Header.ColorMap.Add(Utilities.GetColorFrom2Bytes(color15[1], color15[0]));
                                            break;

                                        case 16:
                                            var color16 = binReader.ReadBytes(2);
                                            // remember that the bytes are stored in reverse oreder
                                            Header.ColorMap.Add(Utilities.GetColorFrom2Bytes(color16[1], color16[0]));
                                            break;

                                        case 24:
                                            b = Convert.ToInt32(binReader.ReadByte());
                                            g = Convert.ToInt32(binReader.ReadByte());
                                            r = Convert.ToInt32(binReader.ReadByte());
                                            Header.ColorMap.Add(Color.FromArgb(r, g, b));
                                            break;

                                        case 32:
                                            a = Convert.ToInt32(binReader.ReadByte());
                                            b = Convert.ToInt32(binReader.ReadByte());
                                            g = Convert.ToInt32(binReader.ReadByte());
                                            r = Convert.ToInt32(binReader.ReadByte());
                                            Header.ColorMap.Add(Color.FromArgb(a, r, g, b));
                                            break;

                                        default:
                                            ClearAll();
                                            throw new Exception("TargaImage only supports ColorMap Entry Sizes of 15, 16, 24 or 32 bits.");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ClearAll();
                                throw ex;
                            }
                        }
                        else
                        {
                            ClearAll();
                            throw new Exception("Image Type requires a Color Map and Color Map Length is zero.");
                        }
                    }
                }
                else
                {
                    if (Header.ImageType == ImageType.UncompressedColorMapped ||
                        Header.ImageType == ImageType.RunLengthEncodedColorMapped)
                    {
                        ClearAll();
                        throw new Exception("Image Type requires a Color Map and there was not a Color Map included in the file.");
                    }
                }
            }
            else
            {
                ClearAll();
                throw new Exception(@"Error loading file, could not read file from disk.");
            }
        }

        /// <summary>
        /// Loads the Targa Extension Area from the file, if it exists.
        /// </summary>
        /// <param name="binReader">A BinaryReader that points the loaded file byte stream.</param>
        private void LoadTgaExtensionArea(BinaryReader binReader)
        {
            if (binReader != null && binReader.BaseStream.Length > 0 && binReader.BaseStream.CanSeek)
            {
                // is there an Extension Area in file
                if (Footer.ExtensionAreaOffset > 0)
                {
                    try
                    {
                        // set the cursor at the beginning of the Extension Area using ExtensionAreaOffset.
                        binReader.BaseStream.Seek(Footer.ExtensionAreaOffset, SeekOrigin.Begin);

                        // load the extension area fields from the file

                        ExtensionArea.SetExtensionSize(binReader.ReadInt16());
                        ExtensionArea.SetAuthorName(Encoding.ASCII.GetString(binReader.ReadBytes(TargaConstants.EXTENSION_AREA_AUTHOR_NAME_BYTE_LENGTH)).TrimEnd('\0'));
                        ExtensionArea.SetAuthorComments(Encoding.ASCII.GetString(binReader.ReadBytes(TargaConstants.EXTENSION_AREA_AUTHOR_COMMENTS_BYTE_LENGTH)).TrimEnd('\0'));

                        // get the date/time stamp of the file
                        var iMonth = binReader.ReadInt16();
                        var iDay = binReader.ReadInt16();
                        var iYear = binReader.ReadInt16();
                        var iHour = binReader.ReadInt16();
                        var iMinute = binReader.ReadInt16();
                        var iSecond = binReader.ReadInt16();
                        var strStamp = iMonth + @"/" + iDay + @"/" + iYear + @" ";
                        strStamp += iHour + @":" + iMinute + @":" + iSecond;
                        if (DateTime.TryParse(strStamp, out var dtStamp))
                            ExtensionArea.SetDateTimeStamp(dtStamp);

                        ExtensionArea.SetJobName(Encoding.ASCII.GetString(binReader.ReadBytes(TargaConstants.EXTENSION_AREA_JOB_NAME_BYTE_LENGTH)).TrimEnd('\0'));

                        // get the job time of the file
                        iHour = binReader.ReadInt16();
                        iMinute = binReader.ReadInt16();
                        iSecond = binReader.ReadInt16();
                        var ts = new TimeSpan(iHour, iMinute, iSecond);
                        ExtensionArea.SetJobTime(ts);

                        ExtensionArea.SetSoftwareId(Encoding.ASCII.GetString(binReader.ReadBytes(TargaConstants.EXTENSION_AREA_SOFTWARE_ID_BYTE_LENGTH)).TrimEnd('\0'));

                        // get the version number and letter from file
                        var iVersionNumber = binReader.ReadInt16() / 100.0F;
                        var strVersionLetter = Encoding.ASCII.GetString(binReader.ReadBytes(TargaConstants.EXTENSION_AREA_SOFTWARE_VERSION_LETTER_BYTE_LENGTH)).TrimEnd('\0');

                        ExtensionArea.SetSoftwareId(iVersionNumber.ToString(@"F2") + strVersionLetter);

                        // get the color key of the file
                        int a = binReader.ReadByte();
                        int r = binReader.ReadByte();
                        int b = binReader.ReadByte();
                        int g = binReader.ReadByte();
                        ExtensionArea.SetKeyColor(Color.FromArgb(a, r, g, b));

                        ExtensionArea.SetPixelAspectRatioNumerator(binReader.ReadInt16());
                        ExtensionArea.SetPixelAspectRatioDenominator(binReader.ReadInt16());
                        ExtensionArea.SetGammaNumerator(binReader.ReadInt16());
                        ExtensionArea.SetGammaDenominator(binReader.ReadInt16());
                        ExtensionArea.SetColorCorrectionOffset(binReader.ReadInt32());
                        ExtensionArea.SetPostageStampOffset(binReader.ReadInt32());
                        ExtensionArea.SetScanLineOffset(binReader.ReadInt32());
                        ExtensionArea.SetAttributesType(binReader.ReadByte());

                        // load Scan Line Table from file if any
                        if (ExtensionArea.ScanLineOffset > 0)
                        {
                            binReader.BaseStream.Seek(ExtensionArea.ScanLineOffset, SeekOrigin.Begin);
                            for (var i = 0; i < Header.Height; i++)
                            {
                                ExtensionArea.ScanLineTable.Add(binReader.ReadInt32());
                            }
                        }

                        // load Color Correction Table from file if any
                        if (ExtensionArea.ColorCorrectionOffset > 0)
                        {
                            binReader.BaseStream.Seek(ExtensionArea.ColorCorrectionOffset, SeekOrigin.Begin);
                            for (var i = 0; i < TargaConstants.EXTENSION_AREA_COLOR_CORRECTION_TABLE_VALUE_LENGTH; i++)
                            {
                                a = binReader.ReadInt16();
                                r = binReader.ReadInt16();
                                b = binReader.ReadInt16();
                                g = binReader.ReadInt16();
                                ExtensionArea.ColorCorrectionTable.Add(Color.FromArgb(a, r, g, b));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ClearAll();
                        throw ex;
                    }
                }
            }
            else
            {
                ClearAll();
                throw new Exception(@"Error loading file, could not read file from disk.");
            }
        }

        /// <summary>
        /// Reads the image data bytes from the file. Handles Uncompressed and RLE Compressed image data.
        /// Uses FirstPixelDestination to properly align the image.
        /// </summary>
        /// <param name="binReader">A BinaryReader that points the loaded file byte stream.</param>
        /// <returns>An array of bytes representing the image data in the proper alignment.</returns>
        private byte[] LoadImageBytes(BinaryReader binReader)
        {
            // read the image data into a byte array
            // take into account stride has to be a multiple of 4
            // use padding to make sure multiple of 4

            byte[] data;
            if (binReader != null && binReader.BaseStream.Length > 0 && binReader.BaseStream.CanSeek)
            {
                if (Header.ImageDataOffset > 0)
                {
                    // padding bytes
                    var padding = new byte[Padding];
                    MemoryStream msData;
                    var rows = new List<List<byte>>();
                    var row = new List<byte>();

                    // seek to the beginning of the image data using the ImageDataOffset value
                    binReader.BaseStream.Seek(Header.ImageDataOffset, SeekOrigin.Begin);

                    // get the size in bytes of each row in the image
                    var intImageRowByteSize = Header.Width * Header.BytesPerPixel;

                    // get the size in bytes of the whole image
                    var intImageByteSize = intImageRowByteSize * Header.Height;

                    // is this a RLE compressed image type
                    if (Header.ImageType == ImageType.RunLengthEncodedBlackAndWhite ||
                       Header.ImageType == ImageType.RunLengthEncodedColorMapped ||
                       Header.ImageType == ImageType.RunLengthEncodedTrueColor)
                    {
                        #region COMPRESSED

                        // used to keep track of bytes read
                        var intImageBytesRead = 0;
                        var intImageRowBytesRead = 0;

                        // keep reading until we have the all image bytes
                        while (intImageBytesRead < intImageByteSize)
                        {
                            // get the RLE packet
                            var bRlePacket = binReader.ReadByte();
                            var intRlePacketType = Utilities.GetBits(bRlePacket, 7, 1);
                            var intRlePixelCount = Utilities.GetBits(bRlePacket, 0, 7) + 1;

                            // check the RLE packet type
                            if ((RlePacketType)intRlePacketType == RlePacketType.RunLength)
                            {
                                // get the pixel color data
                                var bRunLengthPixel = binReader.ReadBytes(Header.BytesPerPixel);

                                // add the number of pixels specified using the read pixel color
                                for (var i = 0; i < intRlePixelCount; i++)
                                {
                                    foreach (var b in bRunLengthPixel)
                                        row.Add(b);

                                    // increment the byte counts
                                    intImageRowBytesRead += bRunLengthPixel.Length;
                                    intImageBytesRead += bRunLengthPixel.Length;

                                    // if we have read a full image row
                                    // add the row to the row list and clear it
                                    // restart row byte count
                                    if (intImageRowBytesRead == intImageRowByteSize)
                                    {
                                        rows.Add(row);
                                        row = null;
                                        row = new List<byte>();
                                        intImageRowBytesRead = 0;
                                    }
                                }
                            }
                            else if ((RlePacketType)intRlePacketType == RlePacketType.Raw)
                            {
                                // get the number of bytes to read based on the read pixel count
                                var intBytesToRead = intRlePixelCount * Header.BytesPerPixel;

                                // read each byte
                                for (var i = 0; i < intBytesToRead; i++)
                                {
                                    row.Add(binReader.ReadByte());

                                    // increment the byte counts
                                    intImageBytesRead++;
                                    intImageRowBytesRead++;

                                    // if we have read a full image row
                                    // add the row to the row list and clear it
                                    // restart row byte count
                                    if (intImageRowBytesRead == intImageRowByteSize)
                                    {
                                        rows.Add(row);
                                        row = null;
                                        row = new List<byte>();
                                        intImageRowBytesRead = 0;
                                    }
                                }
                            }
                        }

                        #endregion COMPRESSED
                    }
                    else
                    {
                        #region NON-COMPRESSED

                        // loop through each row in the image
                        for (var i = 0; i < (int)Header.Height; i++)
                        {
                            // loop through each byte in the row
                            for (var j = 0; j < intImageRowByteSize; j++)
                            {
                                // add the byte to the row
                                row.Add(binReader.ReadByte());
                            }

                            // add row to the list of rows
                            rows.Add(row);
                            // create a new row
                            row = null;
                            row = new List<byte>();
                        }

                        #endregion NON-COMPRESSED
                    }

                    // flag that states whether or not to reverse the location of all rows.
                    var blnRowsReverse = false;

                    // flag that states whether or not to reverse the bytes in each row.
                    var blnEachRowReverse = false;

                    // use FirstPixelDestination to determine the alignment of the
                    // image data byte
                    switch (Header.FirstPixelDestination)
                    {
                        case FirstPixelDestination.TopLeft:
                            blnRowsReverse = false;
                            blnEachRowReverse = true;
                            break;

                        case FirstPixelDestination.TopRight:
                            blnRowsReverse = false;
                            blnEachRowReverse = false;
                            break;

                        case FirstPixelDestination.BottomLeft:
                            blnRowsReverse = true;
                            blnEachRowReverse = true;
                            break;

                        case FirstPixelDestination.BottomRight:
                        case FirstPixelDestination.Unknown:
                            blnRowsReverse = true;
                            blnEachRowReverse = false;
                            break;
                    }

                    // write the bytes from each row into a memory stream and get the
                    // resulting byte array
                    using (msData = new MemoryStream())
                    {
                        // do we reverse the rows in the row list.
                        if (blnRowsReverse)
                            rows.Reverse();

                        // go through each row
                        foreach (var r in rows)
                        {
                            // do we reverse the bytes in the row
                            if (blnEachRowReverse)
                                r.Reverse();

                            // get the byte array for the row
                            var brow = r.ToArray();

                            // write the row bytes and padding bytes to the memory streem
                            msData.Write(brow, 0, brow.Length);
                            msData.Write(padding, 0, padding.Length);
                        }
                        // get the image byte array
                        data = msData.ToArray();
                    }

                    // clear our row arrays
                    if (rows != null)
                    {
                        for (var i = 0; i < rows.Count; i++)
                        {
                            rows[i].Clear();
                            rows[i] = null;
                        }
                        rows.Clear();
                        rows = null;
                    }
                }
                else
                {
                    ClearAll();
                    throw new Exception(@"Error loading file, No image data in file.");
                }
            }
            else
            {
                ClearAll();
                throw new Exception(@"Error loading file, could not read file from disk.");
            }

            // return the image byte array
            return data;
        }

        /// <summary>
        /// Reads the image data bytes from the file and loads them into the Image Bitmap object.
        /// Also loads the color map, if any, into the Image Bitmap.
        /// </summary>
        /// <param name="binReader">A BinaryReader that points the loaded file byte stream.</param>
        private void LoadTgaImage(BinaryReader binReader)
        {
            // make sure we don't have a phantom Bitmap
            Image?.Dispose();

            // make sure we don't have a phantom Thumbnail
            Thumbnail?.Dispose();

            //**************  NOTE  *******************
            // The memory allocated for Microsoft Bitmaps must be aligned on a 32bit boundary.
            // The stride refers to the number of bytes allocated for one scanline of the bitmap.
            // In your loop, you copy the pixels one scanline at a time and take into
            // consideration the amount of padding that occurs due to memory alignment.
            // calculate the stride, in bytes, of the image (32bit aligned width of each image row)
            Stride = ((Header.Width * Header.PixelDepth + 31) & ~31) >> 3; // width in bytes

            // calculate the padding, in bytes, of the image
            // number of bytes to add to make each row a 32bit aligned row
            // padding in bytes
            Padding = Stride - (((Header.Width * Header.PixelDepth) + 7) / 8);

            // get the Pixel format to use with the Bitmap object
            var pf = GetPixelFormat();

            // get the image data bytes
            var bimagedata = LoadImageBytes(binReader);

            // since the Bitmap constructor requires a poiter to an array of image bytes
            // we have to pin down the memory used by the byte array and use the pointer
            // of this pinned memory to create the Bitmap.
            // This tells the Garbage Collector to leave the memory alone and DO NOT touch it.
            _imageByteHandle = GCHandle.Alloc(bimagedata, GCHandleType.Pinned);

            // create a Bitmap object using the image Width, Height,
            // Stride, PixelFormat and the pointer to the pinned byte array.
            Image = new Bitmap(Header.Width,
                                            Header.Height,
                                            Stride,
                                            pf,
                                            _imageByteHandle.AddrOfPinnedObject());

            // lets free the pinned bytes
            if (_imageByteHandle != null && _imageByteHandle.IsAllocated)
                _imageByteHandle.Free();

            // load the thumbnail if any.
            LoadThumbnail(binReader, pf);

            // load the color map into the Bitmap, if it exists
            if (Header.ColorMap.Count > 0)
            {
                // get the Bitmap's current palette
                var pal = Image.Palette;

                // loop trough each color in the loaded file's color map
                for (var i = 0; i < Header.ColorMap.Count; i++)
                {
                    // is the AttributesType 0 or 1 bit
                    var forceOpaque = false;

                    if (Format == TgaFormat.NewTga && Footer.ExtensionAreaOffset > 0)
                    {
                        if (ExtensionArea.AttributesType == 0 || ExtensionArea.AttributesType == 1)
                            forceOpaque = true;
                    }
                    else if (Header.AttributeBits == 0 || Header.AttributeBits == 1)
                        forceOpaque = true;

                    if (forceOpaque)
                        // use 255 for alpha ( 255 = opaque/visible ) so we can see the image
                        pal.Entries[i] = Color.FromArgb(255, Header.ColorMap[i].R, Header.ColorMap[i].G, Header.ColorMap[i].B);
                    else
                        // use whatever value is there
                        pal.Entries[i] = Header.ColorMap[i];
                }

                // set the new palette back to the Bitmap object
                Image.Palette = pal;

                // set the palette to the thumbnail also, if there is one
                if (Thumbnail != null)
                {
                    Thumbnail.Palette = pal;
                }

                pal = null;
            }
            else
            { // no color map
                // check to see if this is a Black and White (Greyscale)
                if (Header.PixelDepth == 8 && (Header.ImageType == ImageType.UncompressedBlackAndWhite ||
                    Header.ImageType == ImageType.RunLengthEncodedBlackAndWhite))
                {
                    // get the current palette
                    var pal = Image.Palette;

                    // create the Greyscale palette
                    for (var i = 0; i < 256; i++)
                    {
                        pal.Entries[i] = Color.FromArgb(i, i, i);
                    }

                    // set the new palette back to the Bitmap object
                    Image.Palette = pal;

                    // set the palette to the thumbnail also, if there is one
                    if (Thumbnail != null)
                    {
                        Thumbnail.Palette = pal;
                    }
                    pal = null;
                }
            }
        }

        /// <summary>
        /// Gets the PixelFormat to be used by the Image based on the Targa file's attributes
        /// </summary>
        /// <returns></returns>
        private PixelFormat GetPixelFormat()
        {
            var pfTargaPixelFormat = PixelFormat.Undefined;

            // first off what is our Pixel Depth (bits per pixel)
            switch (Header.PixelDepth)
            {
                case 8:
                    pfTargaPixelFormat = PixelFormat.Format8bppIndexed;
                    break;

                case 16:
                    // if this is a new tga file and we have an extension area, we'll determine the alpha based on
                    // the extension area Attributes
                    if (Format == TgaFormat.NewTga && Footer.ExtensionAreaOffset > 0)
                    {
                        switch (ExtensionArea.AttributesType)
                        {
                            case 0:
                            case 1:
                            case 2: // no alpha data
                                pfTargaPixelFormat = PixelFormat.Format16bppRgb555;
                                break;

                            case 3: // useful alpha data
                                pfTargaPixelFormat = PixelFormat.Format16bppArgb1555;
                                break;
                        }
                    }
                    else
                    {
                        // just a regular tga, determine the alpha based on the Header Attributes
                        if (Header.AttributeBits == 0)
                            pfTargaPixelFormat = PixelFormat.Format16bppRgb555;
                        if (Header.AttributeBits == 1)
                            pfTargaPixelFormat = PixelFormat.Format16bppArgb1555;
                    }

                    break;

                case 24:
                    pfTargaPixelFormat = PixelFormat.Format24bppRgb;
                    break;

                case 32:

                    // if this is a new tga file and we have an extension area, we'll determine the alpha based on
                    // the extension area Attributes
                    if (Format == TgaFormat.NewTga && Footer.ExtensionAreaOffset > 0)
                    {
                        switch (ExtensionArea.AttributesType)
                        {
                            case 0:
                            case 1:
                            case 2: // no alpha data
                                pfTargaPixelFormat = PixelFormat.Format32bppRgb;
                                break;

                            case 3: // useful alpha data
                                pfTargaPixelFormat = PixelFormat.Format32bppArgb;
                                break;

                            case 4: // premultiplied alpha data
                                pfTargaPixelFormat = PixelFormat.Format32bppPArgb;
                                break;
                        }
                    }
                    else
                    {
                        // just a regular tga, determine the alpha based on the Header Attributes
                        if (Header.AttributeBits == 0)
                            pfTargaPixelFormat = PixelFormat.Format32bppRgb;
                        if (Header.AttributeBits == 8)
                            pfTargaPixelFormat = PixelFormat.Format32bppArgb;
                    }

                    break;
            }

            return pfTargaPixelFormat;
        }

        /// <summary>
        /// Loads the thumbnail of the loaded image file, if any.
        /// </summary>
        /// <param name="binReader">A BinaryReader that points the loaded file byte stream.</param>
        /// <param name="pfPixelFormat">A PixelFormat value indicating what pixel format to use when loading the thumbnail.</param>
        private void LoadThumbnail(BinaryReader binReader, PixelFormat pfPixelFormat)
        {
            // read the Thumbnail image data into a byte array
            // take into account stride has to be a multiple of 4
            // use padding to make sure multiple of 4

            byte[] data = null;
            if (binReader != null && binReader.BaseStream != null && binReader.BaseStream.Length > 0 && binReader.BaseStream.CanSeek)
            {
                if (ExtensionArea.PostageStampOffset > 0)
                {
                    // seek to the beginning of the image data using the ImageDataOffset value
                    binReader.BaseStream.Seek(ExtensionArea.PostageStampOffset, SeekOrigin.Begin);

                    int iWidth = binReader.ReadByte();
                    int iHeight = binReader.ReadByte();

                    var iStride = ((iWidth * Header.PixelDepth + 31) & ~31) >> 3; // width in bytes
                    var iPadding = iStride - (((iWidth * Header.PixelDepth) + 7) / 8);

                    var rows = new List<List<byte>>();
                    var row = new List<byte>();

                    var padding = new byte[iPadding];
                    MemoryStream msData = null;
                    var blnEachRowReverse = false;
                    var blnRowsReverse = false;

                    using (msData = new MemoryStream())
                    {
                        // get the size in bytes of each row in the image
                        var intImageRowByteSize = iWidth * (Header.PixelDepth / 8);

                        // get the size in bytes of the whole image
                        var intImageByteSize = intImageRowByteSize * iHeight;

                        // thumbnails are never compressed
                        for (var i = 0; i < iHeight; i++)
                        {
                            for (var j = 0; j < intImageRowByteSize; j++)
                            {
                                row.Add(binReader.ReadByte());
                            }
                            rows.Add(row);
                            row = null;
                            row = new List<byte>();
                        }

                        switch (Header.FirstPixelDestination)
                        {
                            case FirstPixelDestination.TopLeft:
                                break;

                            case FirstPixelDestination.TopRight:
                                blnRowsReverse = false;
                                blnEachRowReverse = false;
                                break;

                            case FirstPixelDestination.BottomLeft:
                                break;

                            case FirstPixelDestination.BottomRight:
                            case FirstPixelDestination.Unknown:
                                blnRowsReverse = true;
                                blnEachRowReverse = false;

                                break;
                        }

                        if (blnRowsReverse)
                            rows.Reverse();

                        for (var i = 0; i < rows.Count; i++)
                        {
                            if (blnEachRowReverse)
                                rows[i].Reverse();

                            var brow = rows[i].ToArray();
                            msData.Write(brow, 0, brow.Length);
                            msData.Write(padding, 0, padding.Length);
                        }
                        data = msData.ToArray();
                    }

                    if (data != null && data.Length > 0)
                    {
                        _thumbnailByteHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
                        Thumbnail = new Bitmap(iWidth, iHeight, iStride, pfPixelFormat,
                                                        _thumbnailByteHandle.AddrOfPinnedObject());

                        if (_thumbnailByteHandle != null && _thumbnailByteHandle.IsAllocated)
                            _thumbnailByteHandle.Free();
                    }

                    // clear our row arrays
                    if (rows != null)
                    {
                        for (var i = 0; i < rows.Count; i++)
                        {
                            rows[i].Clear();
                            rows[i] = null;
                        }
                        rows.Clear();
                        rows = null;
                    }
                    if (rows != null)
                    {
                        row.Clear();
                        row = null;
                    }
                }
                else
                {
                    if (Thumbnail != null)
                    {
                        Thumbnail.Dispose();
                        Thumbnail = null;
                    }
                }
            }
            else
            {
                if (Thumbnail != null)
                {
                    Thumbnail.Dispose();
                    Thumbnail = null;
                }
            }
        }

        /// <summary>
        /// Clears out all objects and resources.
        /// </summary>
        private void ClearAll()
        {
            if (Image != null)
            {
                Image.Dispose();
                Image = null;
            }

            if (Thumbnail != null)
            {
                Thumbnail.Dispose();
                Thumbnail = null;
            }

            if (_imageByteHandle != null && _imageByteHandle.IsAllocated)
                _imageByteHandle.Free();

            if (_thumbnailByteHandle != null && _thumbnailByteHandle.IsAllocated)
                _thumbnailByteHandle.Free();

            Header = new TargaHeader();
            ExtensionArea = new TargaExtensionArea();
            Footer = new TargaFooter();
            Format = TgaFormat.Unknown;
            Stride = 0;
            Padding = 0;
            FileName = string.Empty;
        }

        /// <summary>
        /// Loads a Targa image file into a Bitmap object.
        /// </summary>
        /// <param name="sFileName">The Targa image filename</param>
        /// <returns>A Bitmap object with the Targa image loaded into it.</returns>
        public static Bitmap LoadTargaImage(string sFileName)
        {
            using (var ti = new TargaImage(sFileName))
            {
                return CopyToBitmap(ti);
            }
        }

        /// <summary>
        /// Loads a stream with Targa image data into a Bitmap object.
        /// </summary>
        /// <param name="sFileName">The Targa image stream</param>
        /// <returns>A Bitmap object with the Targa image loaded into it.</returns>
        public static Bitmap LoadTargaImage(Stream imageStream)
        {
            using (var ti = new TargaImage(imageStream))
            {
                return CopyToBitmap(ti);
            }
        }

        private static Bitmap CopyToBitmap(TargaImage ti)
        {
            Bitmap b = null;
            if (ti.Image.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                b = (Bitmap)ti.Image.Clone();
            }
            else
            {
                b = new Bitmap(ti.Image.Width, ti.Image.Height, ti.Image.PixelFormat);
                using (var g = Graphics.FromImage(b))
                {
                    g.DrawImage(ti.Image, 0, 0, new Rectangle(0, 0, b.Width, b.Height), GraphicsUnit.Pixel);
                }
            }
            return b;
        }

        #region IDisposable Members

        /// <summary>
        /// Disposes all resources used by this instance of the TargaImage class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // Take yourself off the Finalization queue
            // to prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose(bool disposing) executes in two distinct scenarios.
        /// If disposing equals true, the method has been called directly
        /// or indirectly by a user's code. Managed and unmanaged resources
        /// can be disposed.
        /// If disposing equals false, the method has been called by the
        /// runtime from inside the finalizer and you should not reference
        /// other objects. Only unmanaged resources can be disposed.
        /// </summary>
        /// <param name="disposing">If true dispose all resources, else dispose only release unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!_disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    if (Image != null)
                    {
                        Image.Dispose();
                    }

                    if (Thumbnail != null)
                    {
                        Thumbnail.Dispose();
                    }

                    if (_imageByteHandle != null)
                    {
                        if (_imageByteHandle.IsAllocated)
                        {
                            _imageByteHandle.Free();
                        }
                    }

                    if (_thumbnailByteHandle != null)
                    {
                        if (_thumbnailByteHandle.IsAllocated)
                        {
                            _thumbnailByteHandle.Free();
                        }
                    }

                    if (Header != null)
                    {
                        Header.ColorMap.Clear();
                        Header = null;
                    }
                    if (ExtensionArea != null)
                    {
                        ExtensionArea.ColorCorrectionTable.Clear();
                        ExtensionArea.ScanLineTable.Clear();
                        ExtensionArea = null;
                    }

                    Footer = null;
                }
                // Release unmanaged resources. If disposing is false,
                // only the following code is executed.
                // ** release unmanged resources here **

                // Note that this is not thread safe.
                // Another thread could start disposing the object
                // after the managed resources are disposed,
                // but before the disposed flag is set to true.
                // If thread safety is necessary, it must be
                // implemented by the client.
            }
            _disposed = true;
        }

        #endregion IDisposable Members
    }
}