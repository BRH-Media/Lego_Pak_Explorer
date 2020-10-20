using System;
using System.Collections.Generic;
using System.Drawing;

namespace TT_Games_Explorer.Renderer.Textures.TGA
{
    /// <summary>
    /// This class holds all of the Extension Area properties of the Targa image. If an Extension Area exists in the file.
    /// </summary>
    public class TargaExtensionArea
    {
        /// <summary>
        /// Gets the number of Bytes in the fixed-length portion of the ExtensionArea.
        /// For Version 2.0 of the TGA File Format, this number should be set to 495
        /// </summary>
        public int ExtensionSize { get; private set; }

        /// <summary>
        /// Sets the ExtensionSize property, available only to objects in the same assembly as TargaExtensionArea.
        /// </summary>
        /// <param name="intExtensionSize">The Extension Size value read from the file.</param>
        protected internal void SetExtensionSize(int intExtensionSize)
        {
            ExtensionSize = intExtensionSize;
        }

        /// <summary>
        /// Gets the name of the person who created the image.
        /// </summary>
        public string AuthorName { get; private set; } = string.Empty;

        /// <summary>
        /// Sets the AuthorName property, available only to objects in the same assembly as TargaExtensionArea.
        /// </summary>
        /// <param name="strAuthorName">The Author Name value read from the file.</param>
        protected internal void SetAuthorName(string strAuthorName)
        {
            AuthorName = strAuthorName;
        }

        /// <summary>
        /// Gets the comments from the author who created the image.
        /// </summary>
        public string AuthorComments { get; private set; } = string.Empty;

        /// <summary>
        /// Sets the AuthorComments property, available only to objects in the same assembly as TargaExtensionArea.
        /// </summary>
        /// <param name="strAuthorComments">The Author Comments value read from the file.</param>
        protected internal void SetAuthorComments(string strAuthorComments)
        {
            AuthorComments = strAuthorComments;
        }

        /// <summary>
        /// Gets the date and time that the image was saved.
        /// </summary>
        public DateTime DateTimeStamp { get; private set; } = DateTime.Now;

        /// <summary>
        /// Sets the DateTimeStamp property, available only to objects in the same assembly as TargaExtensionArea.
        /// </summary>
        /// <param name="dtDateTimeStamp">The Date Time Stamp value read from the file.</param>
        protected internal void SetDateTimeStamp(DateTime dtDateTimeStamp)
        {
            DateTimeStamp = dtDateTimeStamp;
        }

        /// <summary>
        /// Gets the name or id tag which refers to the job with which the image was associated.
        /// </summary>
        public string JobName { get; private set; } = string.Empty;

        /// <summary>
        /// Sets the JobName property, available only to objects in the same assembly as TargaExtensionArea.
        /// </summary>
        /// <param name="strJobName">The Job Name value read from the file.</param>
        protected internal void SetJobName(string strJobName)
        {
            JobName = strJobName;
        }

        /// <summary>
        /// Gets the job elapsed time when the image was saved.
        /// </summary>
        public TimeSpan JobTime { get; private set; } = TimeSpan.Zero;

        /// <summary>
        /// Sets the JobTime property, available only to objects in the same assembly as TargaExtensionArea.
        /// </summary>
        /// <param name="dtJobTime">The Job Time value read from the file.</param>
        protected internal void SetJobTime(TimeSpan dtJobTime)
        {
            JobTime = dtJobTime;
        }

        /// <summary>
        /// Gets the Software ID. Usually used to determine and record with what program a particular image was created.
        /// </summary>
        public string SoftwareId { get; private set; } = string.Empty;

        /// <summary>
        /// Sets the SoftwareID property, available only to objects in the same assembly as TargaExtensionArea.
        /// </summary>
        /// <param name="strSoftwareId">The Software ID value read from the file.</param>
        protected internal void SetSoftwareId(string strSoftwareId)
        {
            SoftwareId = strSoftwareId;
        }

        /// <summary>
        /// Gets the version of software defined by the SoftwareID.
        /// </summary>
        public string SoftwareVersion { get; private set; } = string.Empty;

        /// <summary>
        /// Sets the SoftwareVersion property, available only to objects in the same assembly as TargaExtensionArea.
        /// </summary>
        /// <param name="strSoftwareVersion">The Software Version value read from the file.</param>
        protected internal void SetSoftwareVersion(string strSoftwareVersion)
        {
            SoftwareVersion = strSoftwareVersion;
        }

        /// <summary>
        /// Gets the key color in effect at the time the image is saved.
        /// The Key Color can be thought of as the "background color" or "transparent color".
        /// </summary>
        public Color KeyColor { get; private set; } = Color.Empty;

        /// <summary>
        /// Sets the KeyColor property, available only to objects in the same assembly as TargaExtensionArea.
        /// </summary>
        /// <param name="cKeyColor">The Key Color value read from the file.</param>
        protected internal void SetKeyColor(Color cKeyColor)
        {
            KeyColor = cKeyColor;
        }

        /// <summary>
        /// Gets the Pixel Ratio Numerator.
        /// </summary>
        public int PixelAspectRatioNumerator { get; private set; }

        /// <summary>
        /// Sets the PixelAspectRatioNumerator property, available only to objects in the same assembly as TargaExtensionArea.
        /// </summary>
        /// <param name="intPixelAspectRatioNumerator">The Pixel Aspect Ratio Numerator value read from the file.</param>
        protected internal void SetPixelAspectRatioNumerator(int intPixelAspectRatioNumerator)
        {
            PixelAspectRatioNumerator = intPixelAspectRatioNumerator;
        }

        /// <summary>
        /// Gets the Pixel Ratio Denominator.
        /// </summary>
        public int PixelAspectRatioDenominator { get; private set; }

        /// <summary>
        /// Sets the PixelAspectRatioDenominator property, available only to objects in the same assembly as TargaExtensionArea.
        /// </summary>
        /// <param name="intPixelAspectRatioDenominator">The Pixel Aspect Ratio Denominator value read from the file.</param>
        protected internal void SetPixelAspectRatioDenominator(int intPixelAspectRatioDenominator)
        {
            PixelAspectRatioDenominator = intPixelAspectRatioDenominator;
        }

        /// <summary>
        /// Gets the Pixel Aspect Ratio.
        /// </summary>
        public float PixelAspectRatio
        {
            get
            {
                if (PixelAspectRatioDenominator > 0)
                {
                    return PixelAspectRatioNumerator / (float)PixelAspectRatioDenominator;
                }

                return 0.0F;
            }
        }

        /// <summary>
        /// Gets the Gamma Numerator.
        /// </summary>
        public int GammaNumerator { get; private set; }

        /// <summary>
        /// Sets the GammaNumerator property, available only to objects in the same assembly as TargaExtensionArea.
        /// </summary>
        /// <param name="intGammaNumerator">The Gamma Numerator value read from the file.</param>
        protected internal void SetGammaNumerator(int intGammaNumerator)
        {
            GammaNumerator = intGammaNumerator;
        }

        /// <summary>
        /// Gets the Gamma Denominator.
        /// </summary>
        public int GammaDenominator { get; private set; }

        /// <summary>
        /// Sets the GammaDenominator property, available only to objects in the same assembly as TargaExtensionArea.
        /// </summary>
        /// <param name="intGammaDenominator">The Gamma Denominator value read from the file.</param>
        protected internal void SetGammaDenominator(int intGammaDenominator)
        {
            GammaDenominator = intGammaDenominator;
        }

        /// <summary>
        /// Gets the Gamma Ratio.
        /// </summary>
        public float GammaRatio
        {
            get
            {
                if (GammaDenominator > 0)
                {
                    var ratio = GammaNumerator / (float)GammaDenominator;
                    return (float)Math.Round(ratio, 1);
                }

                return 1.0F;
            }
        }

        /// <summary>
        /// Gets the offset from the beginning of the file to the start of the Color Correction table.
        /// </summary>
        public int ColorCorrectionOffset { get; private set; }

        /// <summary>
        /// Sets the ColorCorrectionOffset property, available only to objects in the same assembly as TargaExtensionArea.
        /// </summary>
        /// <param name="intColorCorrectionOffset">The Color Correction Offset value read from the file.</param>
        protected internal void SetColorCorrectionOffset(int intColorCorrectionOffset)
        {
            ColorCorrectionOffset = intColorCorrectionOffset;
        }

        /// <summary>
        /// Gets the offset from the beginning of the file to the start of the Postage Stamp image data.
        /// </summary>
        public int PostageStampOffset { get; private set; }

        /// <summary>
        /// Sets the PostageStampOffset property, available only to objects in the same assembly as TargaExtensionArea.
        /// </summary>
        /// <param name="intPostageStampOffset">The Postage Stamp Offset value read from the file.</param>
        protected internal void SetPostageStampOffset(int intPostageStampOffset)
        {
            PostageStampOffset = intPostageStampOffset;
        }

        /// <summary>
        /// Gets the offset from the beginning of the file to the start of the Scan Line table.
        /// </summary>
        public int ScanLineOffset { get; private set; }

        /// <summary>
        /// Sets the ScanLineOffset property, available only to objects in the same assembly as TargaExtensionArea.
        /// </summary>
        /// <param name="intScanLineOffset">The Scan Line Offset value read from the file.</param>
        protected internal void SetScanLineOffset(int intScanLineOffset)
        {
            ScanLineOffset = intScanLineOffset;
        }

        /// <summary>
        /// Gets the type of Alpha channel data contained in the file.
        /// 0: No Alpha data included.
        /// 1: Undefined data in the Alpha field, can be ignored
        /// 2: Undefined data in the Alpha field, but should be retained
        /// 3: Useful Alpha channel data is present
        /// 4: Pre-multiplied Alpha (see description below)
        /// 5-127: RESERVED
        /// 128-255: Un-assigned
        /// </summary>
        public int AttributesType { get; private set; }

        /// <summary>
        /// Sets the AttributesType property, available only to objects in the same assembly as TargaExtensionArea.
        /// </summary>
        /// <param name="intAttributesType">The Attributes Type value read from the file.</param>
        protected internal void SetAttributesType(int intAttributesType)
        {
            AttributesType = intAttributesType;
        }

        /// <summary>
        /// Gets a list of offsets from the beginning of the file that point to the start of the next scan line,
        /// in the order that the image was saved
        /// </summary>
        public List<int> ScanLineTable { get; } = new List<int>();

        /// <summary>
        /// Gets a list of Colors where each Color value is the desired Color correction for that entry.
        /// This allows the user to store a correction table for image remapping or LUT driving.
        /// </summary>
        public List<Color> ColorCorrectionTable { get; } = new List<Color>();
    }
}