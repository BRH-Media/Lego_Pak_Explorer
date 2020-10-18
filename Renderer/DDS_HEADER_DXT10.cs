﻿// ReSharper disable All

using Lego_Pak_Explorer.Renderer.Enums;

namespace Lego_Pak_Explorer.Renderer
{
    public class DDS_HEADER_DXT10
    {
        public DXGI_FORMAT dxgiFormat;
        public D3D10_RESOURCE_DIMENSION resourceDimension;
        public uint miscFlag;
        public uint arraySize;
        public uint reserved;
    }
}