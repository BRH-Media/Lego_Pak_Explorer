using System.Collections.Generic;
using System.Drawing;
using TT_Games_Explorer.Renderer.Textures.TGA.Enums;

namespace TT_Games_Explorer.Renderer.Textures.TGA
{
    /// <summary>
    /// This class holds all of the header properties of a Targa image.
    /// This includes the TGA File Header section the ImageID and the Color Map.
    /// </summary>
    public class TargaHeader
    {
        /// <summary>
        /// Gets the number of bytes contained the ImageIDValue property. The maximum
        /// number of characters is 255. A value of zero indicates that no ImageIDValue is included with the
        /// image.
        /// </summary>
        public byte ImageIdLength { get; private set; }

        /// <summary>
        /// Sets the ImageIDLength property, available only to objects in the same assembly as TargaHeader.
        /// </summary>
        /// <param name="bImageIdLength">The Image ID Length value read from the file.</param>
        protected internal void SetImageIdLength(byte bImageIdLength)
        {
            ImageIdLength = bImageIdLength;
        }

        /// <summary>
        /// Gets the type of color map (if any) included with the image. There are currently 2
        /// defined values for this field:
        /// NO_COLOR_MAP - indicates that no color-map data is included with this image.
        /// COLOR_MAP_INCLUDED - indicates that a color-map is included with this image.
        /// </summary>
        public ColorMapType ColorMapType { get; private set; } = ColorMapType.NoColorMap;

        /// <summary>
        /// Sets the ColorMapType property, available only to objects in the same assembly as TargaHeader.
        /// </summary>
        /// <param name="eColorMapType">One of the ColorMapType enumeration values.</param>
        protected internal void SetColorMapType(ColorMapType eColorMapType)
        {
            ColorMapType = eColorMapType;
        }

        /// <summary>
        /// Gets one of the ImageType enumeration values indicating the type of Targa image read from the file.
        /// </summary>
        public ImageType ImageType { get; private set; } = ImageType.NoImageData;

        /// <summary>
        /// Sets the ImageType property, available only to objects in the same assembly as TargaHeader.
        /// </summary>
        /// <param name="eImageType">One of the ImageType enumeration values.</param>
        protected internal void SetImageType(ImageType eImageType)
        {
            ImageType = eImageType;
        }

        /// <summary>
        /// Gets the index of the first color map entry. ColorMapFirstEntryIndex refers to the starting entry in loading the color map.
        /// </summary>
        public short ColorMapFirstEntryIndex { get; private set; }

        /// <summary>
        /// Sets the ColorMapFirstEntryIndex property, available only to objects in the same assembly as TargaHeader.
        /// </summary>
        /// <param name="sColorMapFirstEntryIndex">The First Entry Index value read from the file.</param>
        protected internal void SetColorMapFirstEntryIndex(short sColorMapFirstEntryIndex)
        {
            ColorMapFirstEntryIndex = sColorMapFirstEntryIndex;
        }

        /// <summary>
        /// Gets total number of color map entries included.
        /// </summary>
        public short ColorMapLength { get; private set; }

        /// <summary>
        /// Sets the ColorMapLength property, available only to objects in the same assembly as TargaHeader.
        /// </summary>
        /// <param name="sColorMapLength">The Color Map Length value read from the file.</param>
        protected internal void SetColorMapLength(short sColorMapLength)
        {
            ColorMapLength = sColorMapLength;
        }

        /// <summary>
        /// Gets the number of bits per entry in the Color Map. Typically 15, 16, 24 or 32-bit values are used.
        /// </summary>
        public byte ColorMapEntrySize { get; private set; }

        /// <summary>
        /// Sets the ColorMapEntrySize property, available only to objects in the same assembly as TargaHeader.
        /// </summary>
        /// <param name="bColorMapEntrySize">The Color Map Entry Size value read from the file.</param>
        protected internal void SetColorMapEntrySize(byte bColorMapEntrySize)
        {
            ColorMapEntrySize = bColorMapEntrySize;
        }

        /// <summary>
        /// Gets the absolute horizontal coordinate for the lower
        /// left corner of the image as it is positioned on a display device having
        /// an origin at the lower left of the screen (e.g., the TARGA series).
        /// </summary>
        public short XOrigin { get; private set; }

        /// <summary>
        /// Sets the XOrigin property, available only to objects in the same assembly as TargaHeader.
        /// </summary>
        /// <param name="sXOrigin">The X Origin value read from the file.</param>
        protected internal void SetXOrigin(short sXOrigin)
        {
            XOrigin = sXOrigin;
        }

        /// <summary>
        /// These bytes specify the absolute vertical coordinate for the lower left
        /// corner of the image as it is positioned on a display device having an
        /// origin at the lower left of the screen (e.g., the TARGA series).
        /// </summary>
        public short YOrigin { get; private set; }

        /// <summary>
        /// Sets the YOrigin property, available only to objects in the same assembly as TargaHeader.
        /// </summary>
        /// <param name="sYOrigin">The Y Origin value read from the file.</param>
        protected internal void SetYOrigin(short sYOrigin)
        {
            YOrigin = sYOrigin;
        }

        /// <summary>
        /// Gets the width of the image in pixels.
        /// </summary>
        public short Width { get; private set; }

        /// <summary>
        /// Sets the Width property, available only to objects in the same assembly as TargaHeader.
        /// </summary>
        /// <param name="sWidth">The Width value read from the file.</param>
        protected internal void SetWidth(short sWidth)
        {
            Width = sWidth;
        }

        /// <summary>
        /// Gets the height of the image in pixels.
        /// </summary>
        public short Height { get; private set; }

        /// <summary>
        /// Sets the Height property, available only to objects in the same assembly as TargaHeader.
        /// </summary>
        /// <param name="sHeight">The Height value read from the file.</param>
        protected internal void SetHeight(short sHeight)
        {
            Height = sHeight;
        }

        /// <summary>
        /// Gets the number of bits per pixel. This number includes
        /// the Attribute or Alpha channel bits. Common values are 8, 16, 24 and 32.
        /// </summary>
        public byte PixelDepth { get; private set; }

        /// <summary>
        /// Sets the PixelDepth property, available only to objects in the same assembly as TargaHeader.
        /// </summary>
        /// <param name="bPixelDepth">The Pixel Depth value read from the file.</param>
        protected internal void SetPixelDepth(byte bPixelDepth)
        {
            PixelDepth = bPixelDepth;
        }

        /// <summary>
        /// Gets or Sets the ImageDescriptor property. The ImageDescriptor is the byte that holds the
        /// Image Origin and Attribute Bits values.
        /// Available only to objects in the same assembly as TargaHeader.
        /// </summary>
        protected internal byte ImageDescriptor { get; set; }

        /// <summary>
        /// Gets one of the FirstPixelDestination enumeration values specifying the screen destination of first pixel based on VerticalTransferOrder and HorizontalTransferOrder
        /// </summary>
        public FirstPixelDestination FirstPixelDestination
        {
            get
            {
                if (VerticalTransferOrder == VerticalTransferOrder.Unknown || HorizontalTransferOrder == HorizontalTransferOrder.Unknown)
                    return FirstPixelDestination.Unknown;
                if (VerticalTransferOrder == VerticalTransferOrder.Bottom && HorizontalTransferOrder == HorizontalTransferOrder.Left)
                    return FirstPixelDestination.BottomLeft;
                if (VerticalTransferOrder == VerticalTransferOrder.Bottom && HorizontalTransferOrder == HorizontalTransferOrder.Right)
                    return FirstPixelDestination.BottomRight;
                if (VerticalTransferOrder == VerticalTransferOrder.Top && HorizontalTransferOrder == HorizontalTransferOrder.Left)
                    return FirstPixelDestination.TopLeft;
                return FirstPixelDestination.TopRight;
            }
        }

        /// <summary>
        /// Gets one of the VerticalTransferOrder enumeration values specifying the top-to-bottom ordering in which pixel data is transferred from the file to the screen.
        /// </summary>
        public VerticalTransferOrder VerticalTransferOrder { get; private set; } = VerticalTransferOrder.Unknown;

        /// <summary>
        /// Sets the VerticalTransferOrder property, available only to objects in the same assembly as TargaHeader.
        /// </summary>
        /// <param name="eVerticalTransferOrder">One of the VerticalTransferOrder enumeration values.</param>
        protected internal void SetVerticalTransferOrder(VerticalTransferOrder eVerticalTransferOrder)
        {
            VerticalTransferOrder = eVerticalTransferOrder;
        }

        /// <summary>
        /// Gets one of the HorizontalTransferOrder enumeration values specifying the left-to-right ordering in which pixel data is transferred from the file to the screen.
        /// </summary>
        public HorizontalTransferOrder HorizontalTransferOrder { get; private set; } = HorizontalTransferOrder.Unknown;

        /// <summary>
        /// Sets the HorizontalTransferOrder property, available only to objects in the same assembly as TargaHeader.
        /// </summary>
        /// <param name="eHorizontalTransferOrder">One of the HorizontalTransferOrder enumeration values.</param>
        protected internal void SetHorizontalTransferOrder(HorizontalTransferOrder eHorizontalTransferOrder)
        {
            HorizontalTransferOrder = eHorizontalTransferOrder;
        }

        /// <summary>
        /// Gets the number of attribute bits per pixel.
        /// </summary>
        public byte AttributeBits { get; private set; }

        /// <summary>
        /// Sets the AttributeBits property, available only to objects in the same assembly as TargaHeader.
        /// </summary>
        /// <param name="bAttributeBits">The Attribute Bits value read from the file.</param>
        protected internal void SetAttributeBits(byte bAttributeBits)
        {
            AttributeBits = bAttributeBits;
        }

        /// <summary>
        /// Gets identifying information about the image.
        /// A value of zero in ImageIDLength indicates that no ImageIDValue is included with the image.
        /// </summary>
        public string ImageIdValue { get; private set; } = string.Empty;

        /// <summary>
        /// Sets the ImageIDValue property, available only to objects in the same assembly as TargaHeader.
        /// </summary>
        /// <param name="strImageIdValue">The Image ID value read from the file.</param>
        protected internal void SetImageIdValue(string strImageIdValue)
        {
            ImageIdValue = strImageIdValue;
        }

        /// <summary>
        /// Gets the Color Map of the image, if any. The Color Map is represented by a list of System.Drawing.Color objects.
        /// </summary>
        public List<Color> ColorMap { get; } = new List<Color>();

        /// <summary>
        /// Gets the offset from the beginning of the file to the Image Data.
        /// </summary>
        public int ImageDataOffset
        {
            get
            {
                // calculate the image data offset

                // start off with the number of bytes holding the header info.
                var intImageDataOffset = TargaConstants.HEADER_BYTE_LENGTH;

                // add the Image ID length (could be variable)
                intImageDataOffset += ImageIdLength;

                // determine the number of bytes for each Color Map entry
                var bytes = 0;
                switch (ColorMapEntrySize)
                {
                    case 15:
                        bytes = 2;
                        break;

                    case 16:
                        bytes = 2;
                        break;

                    case 24:
                        bytes = 3;
                        break;

                    case 32:
                        bytes = 4;
                        break;
                }

                // add the length of the color map
                intImageDataOffset += (ColorMapLength * bytes);

                // return result
                return intImageDataOffset;
            }
        }

        /// <summary>
        /// Gets the number of bytes per pixel.
        /// </summary>
        public int BytesPerPixel => PixelDepth / 8;
    }
}