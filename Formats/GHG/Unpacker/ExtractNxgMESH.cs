using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TT_Games_Explorer.Formats.ExtractHelper;
using TT_Games_Explorer.Formats.ExtractHelper.LDraw;
using TT_Games_Explorer.Formats.ExtractHelper.VariableTypes;
using TT_Games_Explorer.Formats.FormatHelpers;
using TT_Games_Explorer.Formats.FormatHelpers.DISP;
using TT_Games_Explorer.Formats.FormatHelpers.IVL5;
using TT_Games_Explorer.Formats.FormatHelpers.MESH;
using TT_Games_Explorer.Formats.FormatHelpers.META;
using TT_Games_Explorer.Formats.FormatHelpers.TXGH;
using TT_Games_Explorer.Formats.FormatHelpers.UMTL;
using TT_Games_Explorer.Formats.FormatHelpers.Vertex;
using TT_Games_Explorer.Formats.GHG.ExtractHelper;
using TT_Games_Explorer.Formats.GHG.Structure;

#pragma warning disable 414
#pragma warning disable 219

// ReSharper disable All

namespace TT_Games_Explorer.Formats.GHG.Unpacker
{
    public class ExtractNxgMesh
    {
        private string _directoryName;
        private string _extension;
        private string _fileName;
        private string _fileNameWithoutExtension;
        private string _fullPath;
        private int _iPos;
        private byte[] _fileData;
        private int _referenceCounter = 7;
        private MESH04 _mesh;
        private TXGH01 _txgh;
        private IVL501 _ivl5;
        private DISP0F _disp;
        private META0C _meta;
        private UMTL00 _umtl;
        private bool _extractMesh = true;
        private bool _onlyInfo;
        private bool _dds;
        private int _skippingInterval = 8;

        public void ParseArgs(string[] args)
        {
            _directoryName = File.Exists(args[0]) ? Path.GetDirectoryName(args[0]) : throw new ArgumentException(
                $"File {(object)args[0]} does not exist!");
            _extension = Path.GetExtension(args[0]);
            _fileName = Path.GetFileName(args[0]);
            _fileNameWithoutExtension = Path.GetFileNameWithoutExtension(args[0]);
            _fullPath = Path.GetFullPath(args[0]);

            for (var index = 1; index < args.Length; ++index)
            {
                switch (args[index])
                {
                    case "-x":
                        _extractMesh = true;
                        break;

                    case "-i":
                        _onlyInfo = true;
                        break;

                    case "-DDS":
                        _dds = true;
                        break;
                }
            }
        }

        public GhgFile ExtractToStructure(byte[] ghgData, string fileName)
        {
            //global set
            _directoryName = @".";
            _fileData = ghgData;
            _fileName = fileName;

            //data parse
            while (_iPos < _fileData.Length - 4)
            {
                switch (_fileData[_iPos])
                {
                    case (byte)48 when _fileData[_iPos + 1] == (byte)50 && _fileData[_iPos + 2] == (byte)85 && _fileData[_iPos + 3] == (byte)78:
                        _iPos += _skippingInterval;
                        break;

                    case (byte)79 when _fileData[_iPos + 1] == (byte)70 && _fileData[_iPos + 2] == (byte)78 && _fileData[_iPos + 3] == (byte)73:
                        _iPos += _skippingInterval;
                        break;

                    case (byte)76 when _fileData[_iPos + 1] == (byte)66 && _fileData[_iPos + 2] == (byte)84 && _fileData[_iPos + 3] == (byte)78:
                        _iPos += _skippingInterval;
                        break;

                    case (byte)83 when _fileData[_iPos + 1] == (byte)68 && _fileData[_iPos + 2] == (byte)78 && _fileData[_iPos + 3] == (byte)66:
                        _iPos += _skippingInterval;
                        break;

                    case (byte)72 when _fileData[_iPos + 1] == (byte)83 && _fileData[_iPos + 2] == (byte)69 && _fileData[_iPos + 3] == (byte)77:
                        {
                            var meshVersion = BigEndianBitConverter.ToInt32(_fileData, _iPos + 4);
                            _iPos += _skippingInterval;
                            if (!_onlyInfo)
                                ReadMesh(meshVersion);
                            break;
                        }
                    case (byte)72 when _fileData[_iPos + 1] == (byte)71 && _fileData[_iPos + 2] == (byte)88 && _fileData[_iPos + 3] == (byte)84:
                        {
                            var txghVersion = BigEndianBitConverter.ToInt32(_fileData, _iPos + 4);
                            _iPos += _skippingInterval;
                            if (!_onlyInfo)
                                ReadTxgh(txghVersion);
                            break;
                        }
                    case (byte)53 when _fileData[_iPos + 1] == (byte)76 && _fileData[_iPos + 2] == (byte)86 && _fileData[_iPos + 3] == (byte)73:
                        _iPos += _skippingInterval;
                        break;

                    case (byte)76 when _fileData[_iPos + 1] == (byte)79 && _fileData[_iPos + 2] == (byte)71 && _fileData[_iPos + 3] == (byte)72:
                        _iPos += _skippingInterval;
                        break;

                    case (byte)80 when _fileData[_iPos + 1] == (byte)83 && _fileData[_iPos + 2] == (byte)73 && _fileData[_iPos + 3] == (byte)68:
                        {
                            var dispVersion = BigEndianBitConverter.ToInt32(_fileData, _iPos + 4);
                            _iPos += _skippingInterval;
                            if (!_onlyInfo)
                                ReadDisp(dispVersion);
                            break;
                        }
                    case (byte)65 when _fileData[_iPos + 1] == (byte)84 && _fileData[_iPos + 2] == (byte)69 && _fileData[_iPos + 3] == (byte)77:
                        {
                            var metaVersion = BigEndianBitConverter.ToInt32(_fileData, _iPos + 4);
                            _iPos += _skippingInterval;
                            if (_dds)
                                ReadMeta(metaVersion);
                            break;
                        }
                    case (byte)76 when _fileData[_iPos + 1] == (byte)84 && _fileData[_iPos + 2] == (byte)77 && _fileData[_iPos + 3] == (byte)85:
                        {
                            var umtlVersion = BigEndianBitConverter.ToInt32(_fileData, _iPos + 4);
                            _iPos += _skippingInterval;
                            ReadUmtl(umtlVersion);
                            break;
                        }
                    default:
                        ++_iPos;
                        break;
                }
            }

            CreateObjFileGroup();

            //default
            return null;
        }

        public void Extract()
        {
            var fileInfo = new FileInfo(_fullPath);
            _directoryName = fileInfo.DirectoryName;

            var fileStream = File.Open(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            _fileData = new byte[(int)fileInfo.Length];

            fileStream.Read(_fileData, 0, (int)fileInfo.Length);
            fileStream.Close();

            while (_iPos < _fileData.Length - 4)
            {
                switch (_fileData[_iPos])
                {
                    case (byte)48 when _fileData[_iPos + 1] == (byte)50 && _fileData[_iPos + 2] == (byte)85 && _fileData[_iPos + 3] == (byte)78:
                        _iPos += _skippingInterval;
                        break;

                    case (byte)79 when _fileData[_iPos + 1] == (byte)70 && _fileData[_iPos + 2] == (byte)78 && _fileData[_iPos + 3] == (byte)73:
                        _iPos += _skippingInterval;
                        break;

                    case (byte)76 when _fileData[_iPos + 1] == (byte)66 && _fileData[_iPos + 2] == (byte)84 && _fileData[_iPos + 3] == (byte)78:
                        _iPos += _skippingInterval;
                        break;

                    case (byte)83 when _fileData[_iPos + 1] == (byte)68 && _fileData[_iPos + 2] == (byte)78 && _fileData[_iPos + 3] == (byte)66:
                        _iPos += _skippingInterval;
                        break;

                    case (byte)72 when _fileData[_iPos + 1] == (byte)83 && _fileData[_iPos + 2] == (byte)69 && _fileData[_iPos + 3] == (byte)77:
                        {
                            var int32 = BigEndianBitConverter.ToInt32(_fileData, _iPos + 4);
                            _iPos += _skippingInterval;
                            if (!_onlyInfo)
                                ReadMesh(int32);
                            break;
                        }
                    case (byte)72 when _fileData[_iPos + 1] == (byte)71 && _fileData[_iPos + 2] == (byte)88 && _fileData[_iPos + 3] == (byte)84:
                        {
                            var int32 = BigEndianBitConverter.ToInt32(_fileData, _iPos + 4);
                            _iPos += _skippingInterval;
                            if (!_onlyInfo)
                                ReadTxgh(int32);
                            break;
                        }
                    case (byte)53 when _fileData[_iPos + 1] == (byte)76 && _fileData[_iPos + 2] == (byte)86 && _fileData[_iPos + 3] == (byte)73:
                        _iPos += _skippingInterval;
                        break;

                    case (byte)76 when _fileData[_iPos + 1] == (byte)79 && _fileData[_iPos + 2] == (byte)71 && _fileData[_iPos + 3] == (byte)72:
                        _iPos += _skippingInterval;
                        break;

                    case (byte)80 when _fileData[_iPos + 1] == (byte)83 && _fileData[_iPos + 2] == (byte)73 && _fileData[_iPos + 3] == (byte)68:
                        {
                            var int32 = BigEndianBitConverter.ToInt32(_fileData, _iPos + 4);
                            _iPos += _skippingInterval;
                            if (!_onlyInfo)
                                ReadDisp(int32);
                            break;
                        }
                    case (byte)65 when _fileData[_iPos + 1] == (byte)84 && _fileData[_iPos + 2] == (byte)69 && _fileData[_iPos + 3] == (byte)77:
                        {
                            var int32 = BigEndianBitConverter.ToInt32(_fileData, _iPos + 4);
                            _iPos += _skippingInterval;
                            if (_dds)
                                ReadMeta(int32);
                            break;
                        }
                    case (byte)76 when _fileData[_iPos + 1] == (byte)84 && _fileData[_iPos + 2] == (byte)77 && _fileData[_iPos + 3] == (byte)85:
                        {
                            var int32 = BigEndianBitConverter.ToInt32(_fileData, _iPos + 4);
                            _iPos += _skippingInterval;
                            ReadUmtl(int32);
                            break;
                        }
                    default:
                        ++_iPos;
                        break;
                }
            }

            CreateObjFileGroup();
        }

        private void ReadIvl5(int version)
        {
            if (version == 1)
            {
                _ivl5 = new IVL501(_fileData, _iPos);
                _iPos = _ivl5.Read();
            }
            else
                ColoredConsole.WriteLineError("Not Yet Supported: IVL5 Version {0:x2}", (object)version);
        }

        private void ReadUmtl(int version)
        {
            switch (version)
            {
                case 52:
                    _umtl = (UMTL00)new UMTL34(_fileData, _iPos);
                    break;

                case 141:
                    _umtl = (UMTL00)new UMTL8D(_fileData, _iPos);
                    break;

                default:
                    ColoredConsole.WriteLineError("Not Yet Supported: UMTL Version {0:x2}", (object)version);
                    return;
            }
            _iPos = _umtl.Read();
        }

        private void ReadMeta(int version)
        {
            switch (version)
            {
                case 12:
                    _meta = new META0C(_fileData, _iPos);
                    break;

                case 50:
                    _meta = (META0C)new META32(_fileData, _iPos);
                    break;

                case 60:
                    _meta = (META0C)new META3C(_fileData, _iPos);
                    break;

                default:
                    ColoredConsole.WriteLineError("Not Yet Supported: META Version {0:x2}", (object)version);
                    return;
            }
            _iPos = _meta.Read(_directoryName);
        }

        private void ReadDisp(int version)
        {
            switch (version)
            {
                case 8:
                    _disp = (DISP0F)new DISP08(_fileData, _iPos);
                    break;

                case 15:
                    _disp = new DISP0F(_fileData, _iPos);
                    break;

                case 21:
                    _disp = (DISP0F)new DISP15(_fileData, _iPos);
                    break;

                case 23:
                    _disp = (DISP0F)new DISP17(_fileData, _iPos);
                    break;

                case 33:
                    _disp = (DISP0F)new DISP21(_fileData, _iPos);
                    break;

                default:
                    ColoredConsole.WriteLineError("Not Yet Supported: DISP Version {0:x2}", (object)version);
                    return;
            }
            _iPos = _disp.Read();
        }

        private void ReadMesh(int version)
        {
            switch (version)
            {
                case 4:
                    _mesh = new MESH04(_fileData, _iPos);
                    break;

                case 5:
                    _mesh = (MESH04)new MESH05(_fileData, _iPos);
                    break;

                case 46:
                    _mesh = (MESH04)new MESH2E(_fileData, _iPos);
                    break;

                case 47:
                    _mesh = (MESH04)new MESH2F(_fileData, _iPos);
                    break;

                case 48:
                    _mesh = (MESH04)new MESH30(_fileData, _iPos);
                    break;

                case 169:
                    _mesh = (MESH04)new MESHA9(_fileData, _iPos);
                    break;

                case 170:
                    _mesh = (MESH04)new MESHAA(_fileData, _iPos);
                    _referenceCounter = 5;
                    break;

                case 175:
                    _mesh = (MESH04)new MESHAF(_fileData, _iPos);
                    _referenceCounter = 5;
                    break;

                default:
                    ColoredConsole.WriteLineError("Not Yet Supported: MESH Version {0:x2}", (object)version);
                    return;
            }

            _iPos = _mesh.Read(ref _referenceCounter);
        }

        private void ReadTxgh(int version)
        {
            switch (version)
            {
                case 1:
                    _referenceCounter = 9;
                    _txgh = new TXGH01(_fileData, _iPos);
                    break;

                case 3:
                    _referenceCounter = 9;
                    _txgh = (TXGH01)new TXGH03(_fileData, _iPos);
                    break;

                case 4:
                    _referenceCounter = 9;
                    _txgh = (TXGH01)new TXGH04(_fileData, _iPos);
                    break;

                case 5:
                    _referenceCounter = 9;
                    _txgh = (TXGH01)new TXGH05(_fileData, _iPos);
                    break;

                case 6:
                    _referenceCounter = 9;
                    _txgh = (TXGH01)new TXGH06(_fileData, _iPos);
                    break;

                case 7:
                    _referenceCounter = 9;
                    _txgh = (TXGH01)new TXGH07(_fileData, _iPos);
                    break;

                case 8:
                    _referenceCounter = 7;
                    _txgh = (TXGH01)new TXGH08(_fileData, _iPos);
                    break;

                case 9:
                    _referenceCounter = 7;
                    _txgh = (TXGH01)new TXGH09(_fileData, _iPos);
                    break;

                case 10:
                    _txgh = (TXGH01)new TXGH0A(_fileData, _iPos);
                    break;

                case 12:
                    _txgh = (TXGH01)new TXGH0C(_fileData, _iPos);
                    break;

                default:
                    ColoredConsole.WriteLineError("Not Yet Supported: TXGH Version {0:x2}", (object)version);
                    return;
            }
            _iPos = _txgh.Read(ref _referenceCounter);
        }

        private void CheckData(Part part)
        {
            var flag1 = false;
            var flag2 = false;
            var vertexList1 = (List<Vertex>)null;
            var vertexList2 = _mesh.Vertexlistsdictionary[part.VertexListReferences1[0].Reference];
            var vertexList3 = (VertexList)null;
            if (part.VertexListReferences1.Count > 1)
                vertexList3 = _mesh.Vertexlistsdictionary[part.VertexListReferences1[1].Reference];
            var ushortList = _mesh.Indexlistsdictionary[part.IndexListReference1];
            Vector3 position;
            if (vertexList2.Vertices[0].Position != null)
            {
                for (var offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
                    position = vertexList2.Vertices[offsetVertices].Position;
            }
            else if (vertexList3?.Vertices[0].Position != null)
            {
                for (var offsetVertices2 = part.OffsetVertices2; offsetVertices2 < part.OffsetVertices2 + part.NumberVertices; ++offsetVertices2)
                    position = vertexList3.Vertices[offsetVertices2].Position;
            }
            Vector2 uvSet0;
            if (vertexList2.Vertices[0].UVSet0 != null)
            {
                flag2 = true;
                for (var offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
                    uvSet0 = vertexList2.Vertices[offsetVertices].UVSet0;
            }
            else if (vertexList3?.Vertices[0].UVSet0 != null)
            {
                flag2 = true;
                for (var offsetVertices2 = part.OffsetVertices2; offsetVertices2 < part.OffsetVertices2 + part.NumberVertices; ++offsetVertices2)
                    uvSet0 = vertexList3.Vertices[offsetVertices2].UVSet0;
            }
            Vector3 normal;
            if (vertexList2.Vertices[0].Normal != null)
            {
                flag1 = true;
                for (var offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
                    normal = vertexList2.Vertices[offsetVertices].Normal;
            }
            else if (vertexList3?.Vertices[0].Normal != null)
            {
                flag1 = true;
                for (var offsetVertices2 = part.OffsetVertices2; offsetVertices2 < part.OffsetVertices2 + part.NumberVertices; ++offsetVertices2)
                    normal = vertexList3.Vertices[offsetVertices2].Normal;
            }
            if (vertexList2.Vertices[0].ColorSet0 != null)
                vertexList1 = vertexList2.Vertices;
            else if (vertexList3?.Vertices[0].ColorSet0 != null)
                vertexList1 = vertexList3.Vertices;
        }

        private void CreateDatFile(Part part, int partnumber)
        {
            var scale = 262f;
            var newfileName1 = _directoryName + "\\" + _fileNameWithoutExtension + $"{(object)partnumber:0000}" + ".dat";
            var vertexListN = _mesh.Vertexlistsdictionary[part.VertexListReferences1[0].Reference];
            var offsetN = part.OffsetVertices;
            if (vertexListN.Vertices[0].Normal == null && part.VertexListReferences1.Count > 1)
            {
                vertexListN = _mesh.Vertexlistsdictionary[part.VertexListReferences1[1].Reference];
                offsetN = part.OffsetVertices2;
            }
            var indexList = _mesh.Indexlistsdictionary[part.IndexListReference1];
            var vertexListP1 = _mesh.Vertexlistsdictionary[part.VertexListReferences1[0].Reference];
            var offsetP = part.OffsetVertices;
            if (vertexListP1.Vertices[0].Position == null && part.VertexListReferences1.Count > 1)
            {
                vertexListP1 = _mesh.Vertexlistsdictionary[part.VertexListReferences1[1].Reference];
                offsetP = part.OffsetVertices2;
            }
            if (vertexListP1.Vertices[0].Position == null && part.VertexListReferences11 != null)
            {
                for (var index = 0; index < part.VertexListReferences11.Count; ++index)
                {
                    var newFileName2 = _directoryName + "\\" + _fileNameWithoutExtension +
                                       $"{(object)partnumber:0000}.{(object)index:000}" + ".dat";
                    var vertexListP2 = _mesh.Vertexlistsdictionary[part.VertexListReferences11[index].Reference];
                    WriteDatFile(part, vertexListP2, vertexListN, newFileName2, indexList, scale, offsetP, offsetN);
                }
            }
            else
                WriteDatFile(part, vertexListP1, vertexListN, newfileName1, indexList, scale, offsetP, offsetN);
        }

        private static void WriteDatFile(
          Part part,
          VertexList vertexListP,
          VertexList vertexListN,
          string newFileName,
          IReadOnlyList<ushort> indexList,
          float scale,
          int offsetP,
          int offsetN)
        {
            var optionalLines = new OptionalLines();
            var streamWriter = new StreamWriter(newFileName);
            var now = DateTime.Now;
            var str = $"{(object)now.Year:0000}-{(object)now.Month:00}-{(object)now.Day:00}";
            streamWriter.WriteLine("0 " + Path.GetFileName(newFileName) + " (Needs Work)");
            streamWriter.WriteLine("0 Name: " + Path.GetFileNameWithoutExtension(newFileName));
            streamWriter.WriteLine("0 Author: <AuthorRealName> [<AuthorLDrawName>]");
            streamWriter.WriteLine("0 !LDRAW_ORG Unofficial_Part");
            streamWriter.WriteLine("0 !LICENSE Redistributable under CCAL version 2.0 : see CAreadme.txt");
            streamWriter.WriteLine();
            streamWriter.WriteLine("0 BFC CERTIFY CCW");
            streamWriter.WriteLine();
            streamWriter.WriteLine("0 !HISTORY " + str + " {TtGames} Original part shape");
            streamWriter.WriteLine("0 !HISTORY " + str + " [<AuthorLDrawName>] File preparation for LDraw Parts Tracker");
            streamWriter.WriteLine();
            streamWriter.WriteLine("0 // Needs Work: Clean necessary");
            streamWriter.WriteLine("0 // Scale and Origin are not right");
            streamWriter.WriteLine();
            if (vertexListP.Vertices[0].Position != null)
            {
                Console.WriteLine(@"OffsetVertices: {0:x8}, OffsetNormals: {1:x8}, NumberIndices: {2:x8}", (object)offsetP, (object)offsetN, (object)part.NumberIndices);
                for (var index = 0; index < part.NumberIndices; index += 3)
                {
                    streamWriter.WriteLine("3 16 " + vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index]].Position.ToString(scale) + " " + vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index + 1]].Position.ToString(scale) + " " + vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index + 2]].Position.ToString(scale));
                    streamWriter.WriteLine("2 24 " + (vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index]].Position.ToString(scale) + " " + (object)(float)((double)vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index]].Position.X * (double)scale + (double)vertexListN.Vertices[offsetN + (int)indexList[part.OffsetIndices + index]].Normal.X) + " " + (object)(float)((double)vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index]].Position.Y * (double)scale + (double)vertexListN.Vertices[offsetN + (int)indexList[part.OffsetIndices + index]].Normal.Y) + " " + (object)(float)((double)vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index]].Position.Z * (double)scale + (double)vertexListN.Vertices[offsetN + (int)indexList[part.OffsetIndices + index]].Normal.Z)).Replace(',', '.'));
                    streamWriter.WriteLine("2 24 " + (vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index + 1]].Position.ToString(scale) + " " + (object)(float)((double)vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index + 1]].Position.X * (double)scale + (double)vertexListN.Vertices[offsetN + (int)indexList[part.OffsetIndices + index + 1]].Normal.X) + " " + (object)(float)((double)vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index + 1]].Position.Y * (double)scale + (double)vertexListN.Vertices[offsetN + (int)indexList[part.OffsetIndices + index + 1]].Normal.Y) + " " + (object)(float)((double)vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index + 1]].Position.Z * (double)scale + (double)vertexListN.Vertices[offsetN + (int)indexList[part.OffsetIndices + index + 1]].Normal.Z)).Replace(',', '.'));
                    streamWriter.WriteLine("2 24 " + (vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index + 2]].Position.ToString(scale) + " " + (object)(float)((double)vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index + 2]].Position.X * (double)scale + (double)vertexListN.Vertices[offsetN + (int)indexList[part.OffsetIndices + index + 2]].Normal.X) + " " + (object)(float)((double)vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index + 2]].Position.Y * (double)scale + (double)vertexListN.Vertices[offsetN + (int)indexList[part.OffsetIndices + index + 2]].Normal.Y) + " " + (object)(float)((double)vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index + 2]].Position.Z * (double)scale + (double)vertexListN.Vertices[offsetN + (int)indexList[part.OffsetIndices + index + 2]].Normal.Z)).Replace(',', '.'));
                    optionalLines.Add(new OptionalLine(vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index]].Position, vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index + 1]].Position, vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index + 2]].Position, vertexListN.Vertices[offsetN + (int)indexList[part.OffsetIndices + index]].Normal, vertexListN.Vertices[offsetN + (int)indexList[part.OffsetIndices + index + 1]].Normal));
                    optionalLines.Add(new OptionalLine(vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index + 1]].Position, vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index + 2]].Position, vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index]].Position, vertexListN.Vertices[offsetN + (int)indexList[part.OffsetIndices + index + 1]].Normal, vertexListN.Vertices[offsetN + (int)indexList[part.OffsetIndices + index + 2]].Normal));
                    optionalLines.Add(new OptionalLine(vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index + 2]].Position, vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index]].Position, vertexListP.Vertices[offsetP + (int)indexList[part.OffsetIndices + index + 1]].Position, vertexListN.Vertices[offsetN + (int)indexList[part.OffsetIndices + index + 2]].Normal, vertexListN.Vertices[offsetN + (int)indexList[part.OffsetIndices + index]].Normal));
                }
            }
            foreach (var optionalLine in (List<OptionalLine>)optionalLines)
            {
                if (optionalLine.B == null)
                {
                    streamWriter.Write("2 " + (object)24 + " ");
                    streamWriter.Write(optionalLine.X.ToString(scale));
                    streamWriter.Write(optionalLine.Y.ToString(scale));
                    streamWriter.WriteLine();
                }
                else
                {
                    streamWriter.Write("5 " + (object)24 + " ");
                    streamWriter.Write(optionalLine.X.ToString(scale));
                    streamWriter.Write(optionalLine.Y.ToString(scale));
                    streamWriter.Write(optionalLine.A.ToString(scale));
                    streamWriter.Write(optionalLine.B.ToString(scale));
                    streamWriter.WriteLine();
                }
            }
            streamWriter.Close();
        }

        private void CreateObjFileGroup()
        {
            if (_disp?.Groups == null)
                return;

            foreach (var group in _disp.Groups)
            {
                var streamWriter1 = new StreamWriter(_directoryName + "\\" + _fileNameWithoutExtension +
                                                     $"{(object)@group.Parts[0]:0000}" + ".mtl");
                var streamWriter2 = new StreamWriter(_directoryName + "\\" + _fileNameWithoutExtension +
                                                     $"{(object)@group.Parts[0]:0000}" + ".obj");

                streamWriter2.WriteLine("# " + _fileNameWithoutExtension);
                streamWriter2.WriteLine("mtllib " + _fileNameWithoutExtension + $"{(object)@group.Parts[0]:0000}" + ".mtl");

                for (var index = 0; index < group.Parts.Count; ++index)
                {
                    var part = _mesh.Parts[group.Parts[index]];
                    var material = _umtl.Materials[group.Material[index]];
                    streamWriter1.WriteLine(CreateMatFile(material, group.Parts[index]));
                    streamWriter2.WriteLine(CreateObjFile(part, material, group.Parts[index]));
                }

                streamWriter2.Close();
                streamWriter1.Close();
            }
        }

        private string CreateMatFile(Material mat, int partnumber)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("newmtl " + mat.Name);
            var str = $"{(object)1} {(object)1} {(object)1}".Replace(',', '.');
            stringBuilder.AppendLine("   Ka " + str);
            stringBuilder.AppendLine("   Kd " + str);
            if (mat.Texture != -1)
                stringBuilder.AppendLine(
                    $"   map_Kd {(object)mat.Texture:0000}_{(object)Path.GetFileNameWithoutExtension(_txgh.Names[mat.Texture])}.dds");
            stringBuilder.AppendLine();
            return stringBuilder.ToString();
        }

        private string CreateObjFile(Part part, Material mat, int partnumber)
        {
            var vertexList1 = _mesh.Vertexlistsdictionary[part.VertexListReferences1[0].Reference];
            var vertexList2 = (VertexList)null;
            if (part.VertexListReferences1.Count > 1)
                vertexList2 = _mesh.Vertexlistsdictionary[part.VertexListReferences1[1].Reference];
            var indexList = _mesh.Indexlistsdictionary[part.IndexListReference1];
            var objFileSub = CreateObjFileSub(part, mat, vertexList1, vertexList2, indexList, partnumber);
            if (vertexList1.Vertices[0].Position != null)
            {
                Console.WriteLine(@"part.OffsetVertices: {0:x8}", (object)part.OffsetVertices);
                var stringBuilder = new StringBuilder();
                for (var offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
                {
                    var position = vertexList1.Vertices[offsetVertices].Position;
                    stringBuilder.AppendLine(
                        $"v {(object)position.X:0.000000} {(object)position.Y:0.000000} {(object)position.Z:0.000000} "
                            .Replace(',', '.'));
                }
                stringBuilder.AppendLine(objFileSub.ToString());
                return stringBuilder.ToString();
            }
            if (vertexList2?.Vertices[0].Position != null)
            {
                Console.WriteLine(@"part.OffsetVertices2: {0:x8}", (object)part.OffsetVertices2);
                var streamWriter = new StreamWriter(_directoryName + "\\" + _fileNameWithoutExtension +
                                                    $"{(object)partnumber:0000}" + ".obj");
                streamWriter.WriteLine("# " + _fileNameWithoutExtension);
                streamWriter.WriteLine("mtllib " + _fileNameWithoutExtension + $"{(object)partnumber:0000}" + ".mtl");
                for (var offsetVertices2 = part.OffsetVertices2; offsetVertices2 < part.OffsetVertices2 + part.NumberVertices; ++offsetVertices2)
                {
                    var position = vertexList2.Vertices[offsetVertices2].Position;
                    streamWriter.WriteLine(
                        $"v {(object)position.X:0.000000} {(object)position.Y:0.000000} {(object)position.Z:0.000000} "
                            .Replace(',', '.'));
                }
                streamWriter.Write(objFileSub.ToString());
                streamWriter.Close();
            }
            else if (part.VertexListReferences11 != null)
            {
                for (var index1 = 0; index1 < part.VertexListReferences11.Count; ++index1)
                {
                    Console.WriteLine(@"part.OffsetVertices11: {0:x8}", (object)index1);
                    var streamWriter = new StreamWriter(_directoryName + "\\" + _fileNameWithoutExtension +
                                                        $"{(object)partnumber:0000}.{(object)index1:000}" + ".obj");
                    streamWriter.WriteLine("# " + _fileNameWithoutExtension);
                    streamWriter.WriteLine("mtllib " + _fileNameWithoutExtension + $"{(object)partnumber:0000}" + ".mtl");
                    var vertexList = _mesh.Vertexlistsdictionary[part.VertexListReferences11[index1].Reference];

                    foreach (var t in vertexList.Vertices)
                    {
                        var position = t.Position;
                        streamWriter.WriteLine(
                            $"v {(object)position.X:0.000000} {(object)position.Y:0.000000} {(object)position.Z:0.000000} "
                                .Replace(',', '.'));
                    }
                    streamWriter.Write(objFileSub.ToString());
                    streamWriter.Close();
                }
            }
            return "";
        }

        private StringBuilder CreateObjFileSub(
          Part part,
          Material mat,
          VertexList vertexList1,
          VertexList vertexList2,
          List<ushort> indexList,
          int partnumber)
        {
            var stringBuilder = new StringBuilder();
            var flag1 = false;
            var flag2 = false;
            if (vertexList1.Vertices[0].UVSet0 != null)
            {
                flag2 = true;
                for (var offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
                {
                    var uvSet0 = vertexList1.Vertices[offsetVertices].UVSet0;
                    stringBuilder.AppendLine($"vt {(object)uvSet0.X:0.000000} {(object)uvSet0.Y:0.000000} ".Replace(',', '.'));
                }
            }
            else if (vertexList2?.Vertices[0].UVSet0 != null)
            {
                flag2 = true;
                for (var offsetVertices2 = part.OffsetVertices2; offsetVertices2 < part.OffsetVertices2 + part.NumberVertices; ++offsetVertices2)
                {
                    var uvSet0 = vertexList2.Vertices[offsetVertices2].UVSet0;
                    stringBuilder.AppendLine($"vt {(object)uvSet0.X:0.000000} {(object)uvSet0.Y:0.000000} ".Replace(',', '.'));
                }
            }
            if (vertexList1.Vertices[0].Normal != null)
            {
                flag1 = true;
                for (var offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
                {
                    var normal = vertexList1.Vertices[offsetVertices].Normal;
                    stringBuilder.AppendLine(
                        $"vn {(object)normal.X:0.000000} {(object)normal.Y:0.000000} {(object)normal.Z:0.000000} ".Replace(',', '.'));
                }
            }
            else if (vertexList2?.Vertices[0].Normal != null)
            {
                flag1 = true;
                for (var offsetVertices2 = part.OffsetVertices2; offsetVertices2 < part.OffsetVertices2 + part.NumberVertices; ++offsetVertices2)
                {
                    var normal = vertexList2.Vertices[offsetVertices2].Normal;
                    stringBuilder.AppendLine(
                        $"vn {(object)normal.X:0.000000} {(object)normal.Y:0.000000} {(object)normal.Z:0.000000} ".Replace(',', '.'));
                }
            }
            if (vertexList1.Vertices[0].ColorSet0 != null)
            {
            }
            else if (vertexList2?.Vertices[0].ColorSet0 != null)
            {
            }

            stringBuilder.AppendLine("usemtl " + mat.Name);
            var format = "f {0} {1} {2}";
            if (flag2)
                format = !flag1 ? "f {0}/{0} {1}/{1} {2}/{2}" : "f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}";
            else if (flag1)
                format = "f {0}//{0} {1}//{1} {2}//{2}";
            for (var offsetIndices = part.OffsetIndices; offsetIndices < part.OffsetIndices + part.NumberIndices; offsetIndices += 3)
                stringBuilder.AppendLine(string.Format(format, (object)((int)indexList[offsetIndices] - part.NumberVertices), (object)((int)indexList[offsetIndices + 1] - part.NumberVertices), (object)((int)indexList[offsetIndices + 2] - part.NumberVertices)));
            return stringBuilder;
        }

        private void WriteIntoObjFile(Part part, StreamWriter streamWriter)
        {
            var flag1 = false;
            var flag2 = false;
            var vertexList1 = (List<Vertex>)null;
            var vertexList2 = _mesh.Vertexlistsdictionary[part.VertexListReferences1[0].Reference];
            var vertexList3 = (VertexList)null;
            if (part.VertexListReferences1.Count > 1)
                vertexList3 = _mesh.Vertexlistsdictionary[part.VertexListReferences1[1].Reference];
            var ushortList = _mesh.Indexlistsdictionary[part.IndexListReference1];
            if (vertexList2.Vertices[0].Position != null)
            {
                for (var offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
                {
                    var position = vertexList2.Vertices[offsetVertices].Position;
                    streamWriter.WriteLine(
                        $"v {(object)position.X:0.000000} {(object)position.Y:0.000000} {(object)position.Z:0.000000} "
                            .Replace(',', '.'));
                }
            }
            else if (vertexList3?.Vertices[0].Position != null)
            {
                for (var offsetVertices2 = part.OffsetVertices2; offsetVertices2 < part.OffsetVertices2 + part.NumberVertices; ++offsetVertices2)
                {
                    var position = vertexList3.Vertices[offsetVertices2].Position;
                    streamWriter.WriteLine(
                        $"v {(object)position.X:0.000000} {(object)position.Y:0.000000} {(object)position.Z:0.000000} "
                            .Replace(',', '.'));
                }
            }
            if (vertexList2.Vertices[0].UVSet0 != null)
            {
                flag2 = true;
                for (var offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
                {
                    var uvSet0 = vertexList2.Vertices[offsetVertices].UVSet0;
                    streamWriter.WriteLine($"vt {(object)uvSet0.X:0.000000} {(object)uvSet0.Y:0.000000} ".Replace(',', '.'));
                }
            }
            else if (vertexList3?.Vertices[0].UVSet0 != null)
            {
                flag2 = true;
                for (var offsetVertices2 = part.OffsetVertices2; offsetVertices2 < part.OffsetVertices2 + part.NumberVertices; ++offsetVertices2)
                {
                    var uvSet0 = vertexList3.Vertices[offsetVertices2].UVSet0;
                    streamWriter.WriteLine($"vt {(object)uvSet0.X:0.000000} {(object)uvSet0.Y:0.000000} ".Replace(',', '.'));
                }
            }
            if (vertexList2.Vertices[0].Normal != null)
            {
                flag1 = true;
                for (var offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
                {
                    var normal = vertexList2.Vertices[offsetVertices].Normal;
                    streamWriter.WriteLine(
                        $"vn {(object)normal.X:0.000000} {(object)normal.Y:0.000000} {(object)normal.Z:0.000000} ".Replace(',', '.'));
                }
            }
            else if (vertexList3?.Vertices[0].Normal != null)
            {
                flag1 = true;
                for (var offsetVertices2 = part.OffsetVertices2; offsetVertices2 < part.OffsetVertices2 + part.NumberVertices; ++offsetVertices2)
                {
                    var normal = vertexList3.Vertices[offsetVertices2].Normal;
                    streamWriter.WriteLine(
                        $"vn {(object)normal.X:0.000000} {(object)normal.Y:0.000000} {(object)normal.Z:0.000000} ".Replace(',', '.'));
                }
            }
            if (vertexList2.Vertices[0].ColorSet0 != null)
                vertexList1 = vertexList2.Vertices;
            else if (vertexList3?.Vertices[0].ColorSet0 != null)
                vertexList1 = vertexList3.Vertices;
            var format = "f {0} {1} {2}";
            if (flag2)
                format = !flag1 ? "f {0}/{0} {1}/{1} {2}/{2}" : "f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}";
            else if (flag1)
                format = "f {0}//{0} {1}//{1} {2}//{2}";
            for (var offsetIndices = part.OffsetIndices; offsetIndices < part.OffsetIndices + part.NumberIndices; offsetIndices += 3)
                streamWriter.WriteLine(string.Format(format, (object)((int)ushortList[offsetIndices] - part.NumberVertices), (object)((int)ushortList[offsetIndices + 1] - part.NumberVertices), (object)((int)ushortList[offsetIndices + 2] - part.NumberVertices)));
        }
    }
}