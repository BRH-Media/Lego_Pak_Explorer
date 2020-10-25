using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace ExtractNxgMESH
{
  public class ExtractNxgMESH
  {
    private string directoryname;
    private string extention;
    private string filename;
    private string filenamewithoutextension;
    private string fullPath;
    private int iPos = 0;
    private byte[] fileData;
    private int referencecounter = 7;
    private MESH04 mesh;
    private TXGH01 txgh;
    private IVL501 ivl5;
    private DISP0F disp;
    private META0C meta;
    private UMTL00 umtl;
    private bool extractMesh = true;
    private bool onlyInfo = false;
    private bool DDS = false;

    public void ParseArgs(string[] args)
    {
      if (((IEnumerable<string>) args).Count<string>() < 1)
        throw new ArgumentException("No argument handed over!");
      this.directoryname = File.Exists(args[0]) ? Path.GetDirectoryName(args[0]) : throw new ArgumentException(string.Format("File {0} does not exist!", (object) args[0]));
      this.extention = Path.GetExtension(args[0]);
      this.filename = Path.GetFileName(args[0]);
      this.filenamewithoutextension = Path.GetFileNameWithoutExtension(args[0]);
      this.fullPath = Path.GetFullPath(args[0]);
      if (!(this.extention.ToLower() == ".ghg") && !(this.extention.ToLower() == ".gsc"))
        throw new ArgumentException("File extention != .ghg and != .gsc");
      for (int index = 1; index < args.Length; ++index)
      {
        switch (args[index])
        {
          case "-x":
            this.extractMesh = true;
            break;
          case "-i":
            this.onlyInfo = true;
            break;
          case "-DDS":
            this.DDS = true;
            break;
        }
      }
    }

    public void Extract()
    {
      ColoredConsole.WriteLineInfo("{0:x8} {1}", (object) this.iPos, (object) this.fullPath);
      FileInfo fileInfo = new FileInfo(this.fullPath);
      this.directoryname = fileInfo.DirectoryName;
      FileStream fileStream = File.Open(this.fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
      this.fileData = new byte[(int) fileInfo.Length];
      fileStream.Read(this.fileData, 0, (int) fileInfo.Length);
      fileStream.Close();
      while (this.iPos < this.fileData.Length - 4)
      {
        if (this.fileData[this.iPos] == (byte) 48 && this.fileData[this.iPos + 1] == (byte) 50 && this.fileData[this.iPos + 2] == (byte) 85 && this.fileData[this.iPos + 3] == (byte) 78)
        {
          ColoredConsole.WriteLineInfo("{0:x8} NU20 Version 0x{1:x2}", (object) this.iPos, (object) BigEndianBitConverter.ToInt32(this.fileData, this.iPos + 4));
          this.iPos += 4;
          this.iPos += 4;
        }
        else if (this.fileData[this.iPos] == (byte) 79 && this.fileData[this.iPos + 1] == (byte) 70 && this.fileData[this.iPos + 2] == (byte) 78 && this.fileData[this.iPos + 3] == (byte) 73)
        {
          ColoredConsole.WriteLineInfo("{0:x8} INFO Version 0x{1:x2}", (object) this.iPos, (object) BigEndianBitConverter.ToInt32(this.fileData, this.iPos + 4));
          this.iPos += 4;
          this.iPos += 4;
        }
        else if (this.fileData[this.iPos] == (byte) 76 && this.fileData[this.iPos + 1] == (byte) 66 && this.fileData[this.iPos + 2] == (byte) 84 && this.fileData[this.iPos + 3] == (byte) 78)
        {
          ColoredConsole.WriteLineInfo("{0:x8} NTBL Version 0x{1:x2}", (object) this.iPos, (object) BigEndianBitConverter.ToInt32(this.fileData, this.iPos + 4));
          this.iPos += 4;
          this.iPos += 4;
        }
        else if (this.fileData[this.iPos] == (byte) 83 && this.fileData[this.iPos + 1] == (byte) 68 && this.fileData[this.iPos + 2] == (byte) 78 && this.fileData[this.iPos + 3] == (byte) 66)
        {
          ColoredConsole.WriteLineInfo("{0:x8} BNDS Version 0x{1:x2}", (object) this.iPos, (object) BigEndianBitConverter.ToInt32(this.fileData, this.iPos + 4));
          this.iPos += 4;
          this.iPos += 4;
        }
        else if (this.fileData[this.iPos] == (byte) 72 && this.fileData[this.iPos + 1] == (byte) 83 && this.fileData[this.iPos + 2] == (byte) 69 && this.fileData[this.iPos + 3] == (byte) 77)
        {
          int int32 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos + 4);
          ColoredConsole.WriteLineInfo("{0:x8} MESH Version 0x{1:x2}", (object) this.iPos, (object) int32);
          this.iPos += 4;
          this.iPos += 4;
          if (!this.onlyInfo)
            this.readMESH(int32);
        }
        else if (this.fileData[this.iPos] == (byte) 72 && this.fileData[this.iPos + 1] == (byte) 71 && this.fileData[this.iPos + 2] == (byte) 88 && this.fileData[this.iPos + 3] == (byte) 84)
        {
          int int32 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos + 4);
          ColoredConsole.WriteLineInfo("{0:x8} TXGH Version 0x{1:x2}", (object) this.iPos, (object) int32);
          this.iPos += 4;
          this.iPos += 4;
          if (!this.onlyInfo)
            this.readTXGH(int32);
        }
        else if (this.fileData[this.iPos] == (byte) 53 && this.fileData[this.iPos + 1] == (byte) 76 && this.fileData[this.iPos + 2] == (byte) 86 && this.fileData[this.iPos + 3] == (byte) 73)
        {
          ColoredConsole.WriteLineInfo("{0:x8} IVL5 Version 0x{1:x2}", (object) this.iPos, (object) BigEndianBitConverter.ToInt32(this.fileData, this.iPos + 4));
          this.iPos += 4;
          this.iPos += 4;
        }
        else if (this.fileData[this.iPos] == (byte) 76 && this.fileData[this.iPos + 1] == (byte) 79 && this.fileData[this.iPos + 2] == (byte) 71 && this.fileData[this.iPos + 3] == (byte) 72)
        {
          ColoredConsole.WriteLineInfo("{0:x8} HGOL Version 0x{1:x2}", (object) this.iPos, (object) BigEndianBitConverter.ToInt32(this.fileData, this.iPos + 4));
          this.iPos += 4;
          this.iPos += 4;
        }
        else if (this.fileData[this.iPos] == (byte) 80 && this.fileData[this.iPos + 1] == (byte) 83 && this.fileData[this.iPos + 2] == (byte) 73 && this.fileData[this.iPos + 3] == (byte) 68)
        {
          int int32 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos + 4);
          ColoredConsole.WriteLineInfo("{0:x8} DISP Version 0x{1:x2}", (object) this.iPos, (object) int32);
          this.iPos += 4;
          this.iPos += 4;
          if (!this.onlyInfo)
            this.readDISP(int32);
        }
        else if (this.fileData[this.iPos] == (byte) 65 && this.fileData[this.iPos + 1] == (byte) 84 && this.fileData[this.iPos + 2] == (byte) 69 && this.fileData[this.iPos + 3] == (byte) 77)
        {
          int int32 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos + 4);
          ColoredConsole.WriteLineInfo("{0:x8} META Version 0x{1:x2}", (object) this.iPos, (object) int32);
          this.iPos += 4;
          this.iPos += 4;
          if (this.DDS)
            this.readMETA(int32);
        }
        else if (this.fileData[this.iPos] == (byte) 76 && this.fileData[this.iPos + 1] == (byte) 84 && this.fileData[this.iPos + 2] == (byte) 77 && this.fileData[this.iPos + 3] == (byte) 85)
        {
          int int32 = BigEndianBitConverter.ToInt32(this.fileData, this.iPos + 4);
          ColoredConsole.WriteLineInfo("{0:x8} UMTL Version 0x{1:x2}", (object) this.iPos, (object) int32);
          this.iPos += 4;
          this.iPos += 4;
          this.readUMTL(int32);
        }
        else
          ++this.iPos;
      }
      bool flag = true;
      flag = false;
      this.CreateObjFileGroup();
    }

    private void readIVL5(int version)
    {
      if (version == 1)
      {
        this.ivl5 = new IVL501(this.fileData, this.iPos);
        this.iPos = this.ivl5.Read();
      }
      else
        ColoredConsole.WriteLineError("Not Yet Supported: IVL5 Version {0:x2}", (object) version);
    }

    private void readUMTL(int version)
    {
      switch (version)
      {
        case 52:
          this.umtl = (UMTL00) new UMTL34(this.fileData, this.iPos);
          break;
        case 141:
          this.umtl = (UMTL00) new UMTL8D(this.fileData, this.iPos);
          break;
        default:
          ColoredConsole.WriteLineError("Not Yet Supported: UMTL Version {0:x2}", (object) version);
          return;
      }
      this.iPos = this.umtl.Read();
    }

    private void readMETA(int version)
    {
      switch (version)
      {
        case 12:
          this.meta = new META0C(this.fileData, this.iPos);
          break;
        case 50:
          this.meta = (META0C) new META32(this.fileData, this.iPos);
          break;
        case 60:
          this.meta = (META0C) new META3C(this.fileData, this.iPos);
          break;
        default:
          ColoredConsole.WriteLineError("Not Yet Supported: META Version {0:x2}", (object) version);
          return;
      }
      this.iPos = this.meta.Read(this.directoryname);
    }

    private void readDISP(int version)
    {
      switch (version)
      {
        case 8:
          this.disp = (DISP0F) new DISP08(this.fileData, this.iPos);
          break;
        case 15:
          this.disp = new DISP0F(this.fileData, this.iPos);
          break;
        case 21:
          this.disp = (DISP0F) new DISP15(this.fileData, this.iPos);
          break;
        case 23:
          this.disp = (DISP0F) new DISP17(this.fileData, this.iPos);
          break;
        case 33:
          this.disp = (DISP0F) new DISP21(this.fileData, this.iPos);
          break;
        default:
          ColoredConsole.WriteLineError("Not Yet Supported: DISP Version {0:x2}", (object) version);
          return;
      }
      this.iPos = this.disp.Read();
    }

    private void readMESH(int version)
    {
      switch (version)
      {
        case 4:
          this.mesh = new MESH04(this.fileData, this.iPos);
          break;
        case 5:
          this.mesh = (MESH04) new MESH05(this.fileData, this.iPos);
          break;
        case 46:
          this.mesh = (MESH04) new MESH2E(this.fileData, this.iPos);
          break;
        case 47:
          this.mesh = (MESH04) new MESH2F(this.fileData, this.iPos);
          break;
        case 48:
          this.mesh = (MESH04) new MESH30(this.fileData, this.iPos);
          break;
        case 169:
          this.mesh = (MESH04) new MESHA9(this.fileData, this.iPos);
          break;
        case 170:
          this.mesh = (MESH04) new MESHAA(this.fileData, this.iPos);
          this.referencecounter = 5;
          break;
        case 175:
          this.mesh = (MESH04) new MESHAF(this.fileData, this.iPos);
          this.referencecounter = 5;
          break;
        default:
          ColoredConsole.WriteLineError("Not Yet Supported: MESH Version {0:x2}", (object) version);
          return;
      }
      this.iPos = this.mesh.Read(ref this.referencecounter);
    }

    private void readTXGH(int version)
    {
      switch (version)
      {
        case 1:
          this.referencecounter = 9;
          this.txgh = new TXGH01(this.fileData, this.iPos);
          break;
        case 3:
          this.referencecounter = 9;
          this.txgh = (TXGH01) new TXGH03(this.fileData, this.iPos);
          break;
        case 4:
          this.referencecounter = 9;
          this.txgh = (TXGH01) new TXGH04(this.fileData, this.iPos);
          break;
        case 5:
          this.referencecounter = 9;
          this.txgh = (TXGH01) new TXGH05(this.fileData, this.iPos);
          break;
        case 6:
          this.referencecounter = 9;
          this.txgh = (TXGH01) new TXGH06(this.fileData, this.iPos);
          break;
        case 7:
          this.referencecounter = 9;
          this.txgh = (TXGH01) new TXGH07(this.fileData, this.iPos);
          break;
        case 8:
          this.referencecounter = 7;
          this.txgh = (TXGH01) new TXGH08(this.fileData, this.iPos);
          break;
        case 9:
          this.referencecounter = 7;
          this.txgh = (TXGH01) new TXGH09(this.fileData, this.iPos);
          break;
        case 10:
          this.txgh = (TXGH01) new TXGH0A(this.fileData, this.iPos);
          break;
        case 12:
          this.txgh = (TXGH01) new TXGH0C(this.fileData, this.iPos);
          break;
        default:
          ColoredConsole.WriteLineError("Not Yet Supported: TXGH Version {0:x2}", (object) version);
          return;
      }
      this.iPos = this.txgh.Read(ref this.referencecounter);
    }

    private void CheckData(Part part)
    {
      bool flag1 = false;
      bool flag2 = false;
      List<Vertex> vertexList1 = (List<Vertex>) null;
      VertexList vertexList2 = this.mesh.Vertexlistsdictionary[part.VertexListReferences1[0].Reference];
      VertexList vertexList3 = (VertexList) null;
      if (part.VertexListReferences1.Count > 1)
        vertexList3 = this.mesh.Vertexlistsdictionary[part.VertexListReferences1[1].Reference];
      List<ushort> ushortList = this.mesh.Indexlistsdictionary[part.IndexListReference1];
      Vector3 position;
      if (vertexList2.Vertices[0].Position != null)
      {
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
          position = vertexList2.Vertices[offsetVertices].Position;
      }
      else if (vertexList3 != null && vertexList3.Vertices[0].Position != null)
      {
        for (int offsetVertices2 = part.OffsetVertices2; offsetVertices2 < part.OffsetVertices2 + part.NumberVertices; ++offsetVertices2)
          position = vertexList3.Vertices[offsetVertices2].Position;
      }
      Vector2 uvSet0;
      if (vertexList2.Vertices[0].UVSet0 != null)
      {
        flag2 = true;
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
          uvSet0 = vertexList2.Vertices[offsetVertices].UVSet0;
      }
      else if (vertexList3 != null && vertexList3.Vertices[0].UVSet0 != null)
      {
        flag2 = true;
        for (int offsetVertices2 = part.OffsetVertices2; offsetVertices2 < part.OffsetVertices2 + part.NumberVertices; ++offsetVertices2)
          uvSet0 = vertexList3.Vertices[offsetVertices2].UVSet0;
      }
      Vector3 normal;
      if (vertexList2.Vertices[0].Normal != null)
      {
        flag1 = true;
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
          normal = vertexList2.Vertices[offsetVertices].Normal;
      }
      else if (vertexList3 != null && vertexList3.Vertices[0].Normal != null)
      {
        flag1 = true;
        for (int offsetVertices2 = part.OffsetVertices2; offsetVertices2 < part.OffsetVertices2 + part.NumberVertices; ++offsetVertices2)
          normal = vertexList3.Vertices[offsetVertices2].Normal;
      }
      if (vertexList2.Vertices[0].ColorSet0 != null)
        vertexList1 = vertexList2.Vertices;
      else if (vertexList3 != null && vertexList3.Vertices[0].ColorSet0 != null)
        vertexList1 = vertexList3.Vertices;
    }

    private void CreateDatFile(Part part, int partnumber)
    {
      float scale = 262f;
      string newfilename1 = this.directoryname + "\\" + this.filenamewithoutextension + string.Format("{0:0000}", (object) partnumber) + ".dat";
      VertexList vertexListN = this.mesh.Vertexlistsdictionary[part.VertexListReferences1[0].Reference];
      int offsetN = part.OffsetVertices;
      if (vertexListN.Vertices[0].Normal == null && part.VertexListReferences1.Count > 1)
      {
        vertexListN = this.mesh.Vertexlistsdictionary[part.VertexListReferences1[1].Reference];
        offsetN = part.OffsetVertices2;
      }
      List<ushort> IndexList = this.mesh.Indexlistsdictionary[part.IndexListReference1];
      VertexList vertexListP1 = this.mesh.Vertexlistsdictionary[part.VertexListReferences1[0].Reference];
      int offsetP = part.OffsetVertices;
      if (vertexListP1.Vertices[0].Position == null && part.VertexListReferences1.Count > 1)
      {
        vertexListP1 = this.mesh.Vertexlistsdictionary[part.VertexListReferences1[1].Reference];
        offsetP = part.OffsetVertices2;
      }
      if (vertexListP1.Vertices[0].Position == null && part.VertexListReferences11 != null)
      {
        for (int index = 0; index < part.VertexListReferences11.Count; ++index)
        {
          string newfilename2 = this.directoryname + "\\" + this.filenamewithoutextension + string.Format("{0:0000}.{1:000}", (object) partnumber, (object) index) + ".dat";
          VertexList vertexListP2 = this.mesh.Vertexlistsdictionary[part.VertexListReferences11[index].Reference];
          this.WriteDatFile(part, vertexListP2, vertexListN, newfilename2, IndexList, scale, offsetP, offsetN);
        }
      }
      else
        this.WriteDatFile(part, vertexListP1, vertexListN, newfilename1, IndexList, scale, offsetP, offsetN);
    }

    private void WriteDatFile(
      Part part,
      VertexList vertexListP,
      VertexList vertexListN,
      string newfilename,
      List<ushort> IndexList,
      float scale,
      int offsetP,
      int offsetN)
    {
      OptionalLines optionalLines = new OptionalLines();
      StreamWriter streamWriter = new StreamWriter(newfilename);
      DateTime now = DateTime.Now;
      string str = string.Format("{0:0000}-{1:00}-{2:00}", (object) now.Year, (object) now.Month, (object) now.Day);
      streamWriter.WriteLine("0 " + Path.GetFileName(newfilename) + " (Needs Work)");
      streamWriter.WriteLine("0 Name: " + Path.GetFileNameWithoutExtension(newfilename));
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
        Console.WriteLine("OffsetVertices: {0:x8}, OffsetNormals: {1:x8}, NumberIndices: {2:x8}", (object) offsetP, (object) offsetN, (object) part.NumberIndices);
        for (int index = 0; index < part.NumberIndices; index += 3)
        {
          streamWriter.WriteLine("3 16 " + vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index]].Position.ToString(scale) + " " + vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index + 1]].Position.ToString(scale) + " " + vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index + 2]].Position.ToString(scale));
          streamWriter.WriteLine("2 24 " + (vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index]].Position.ToString(scale) + " " + (object) (float) ((double) vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index]].Position.X * (double) scale + (double) vertexListN.Vertices[offsetN + (int) IndexList[part.OffsetIndices + index]].Normal.X) + " " + (object) (float) ((double) vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index]].Position.Y * (double) scale + (double) vertexListN.Vertices[offsetN + (int) IndexList[part.OffsetIndices + index]].Normal.Y) + " " + (object) (float) ((double) vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index]].Position.Z * (double) scale + (double) vertexListN.Vertices[offsetN + (int) IndexList[part.OffsetIndices + index]].Normal.Z)).Replace(',', '.'));
          streamWriter.WriteLine("2 24 " + (vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index + 1]].Position.ToString(scale) + " " + (object) (float) ((double) vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index + 1]].Position.X * (double) scale + (double) vertexListN.Vertices[offsetN + (int) IndexList[part.OffsetIndices + index + 1]].Normal.X) + " " + (object) (float) ((double) vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index + 1]].Position.Y * (double) scale + (double) vertexListN.Vertices[offsetN + (int) IndexList[part.OffsetIndices + index + 1]].Normal.Y) + " " + (object) (float) ((double) vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index + 1]].Position.Z * (double) scale + (double) vertexListN.Vertices[offsetN + (int) IndexList[part.OffsetIndices + index + 1]].Normal.Z)).Replace(',', '.'));
          streamWriter.WriteLine("2 24 " + (vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index + 2]].Position.ToString(scale) + " " + (object) (float) ((double) vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index + 2]].Position.X * (double) scale + (double) vertexListN.Vertices[offsetN + (int) IndexList[part.OffsetIndices + index + 2]].Normal.X) + " " + (object) (float) ((double) vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index + 2]].Position.Y * (double) scale + (double) vertexListN.Vertices[offsetN + (int) IndexList[part.OffsetIndices + index + 2]].Normal.Y) + " " + (object) (float) ((double) vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index + 2]].Position.Z * (double) scale + (double) vertexListN.Vertices[offsetN + (int) IndexList[part.OffsetIndices + index + 2]].Normal.Z)).Replace(',', '.'));
          optionalLines.Add(new OptionalLine(vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index]].Position, vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index + 1]].Position, vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index + 2]].Position, vertexListN.Vertices[offsetN + (int) IndexList[part.OffsetIndices + index]].Normal, vertexListN.Vertices[offsetN + (int) IndexList[part.OffsetIndices + index + 1]].Normal));
          optionalLines.Add(new OptionalLine(vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index + 1]].Position, vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index + 2]].Position, vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index]].Position, vertexListN.Vertices[offsetN + (int) IndexList[part.OffsetIndices + index + 1]].Normal, vertexListN.Vertices[offsetN + (int) IndexList[part.OffsetIndices + index + 2]].Normal));
          optionalLines.Add(new OptionalLine(vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index + 2]].Position, vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index]].Position, vertexListP.Vertices[offsetP + (int) IndexList[part.OffsetIndices + index + 1]].Position, vertexListN.Vertices[offsetN + (int) IndexList[part.OffsetIndices + index + 2]].Normal, vertexListN.Vertices[offsetN + (int) IndexList[part.OffsetIndices + index]].Normal));
        }
      }
      foreach (OptionalLine optionalLine in (List<OptionalLine>) optionalLines)
      {
        if (optionalLine.B == null)
        {
          streamWriter.Write("2 " + (object) 24 + " ");
          streamWriter.Write(optionalLine.X.ToString(scale));
          streamWriter.Write(optionalLine.Y.ToString(scale));
          streamWriter.WriteLine();
        }
        else
        {
          streamWriter.Write("5 " + (object) 24 + " ");
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
      if (this.disp == null || this.disp.Groups == null)
        return;
      foreach (Group group in this.disp.Groups)
      {
        StreamWriter streamWriter1 = new StreamWriter(this.directoryname + "\\" + this.filenamewithoutextension + string.Format("{0:0000}", (object) group.Parts[0]) + ".mtl");
        StreamWriter streamWriter2 = new StreamWriter(this.directoryname + "\\" + this.filenamewithoutextension + string.Format("{0:0000}", (object) group.Parts[0]) + ".obj");
        streamWriter2.WriteLine("# " + this.filenamewithoutextension);
        streamWriter2.WriteLine("mtllib " + this.filenamewithoutextension + string.Format("{0:0000}", (object) group.Parts[0]) + ".mtl");
        for (int index = 0; index < group.Parts.Count; ++index)
        {
          Part part = this.mesh.Parts[group.Parts[index]];
          Material material = this.umtl.Materials[group.Material[index]];
          streamWriter1.WriteLine(this.CreateMatFile(material, group.Parts[index]));
          streamWriter2.WriteLine(this.CreateObjFile(part, material, group.Parts[index]));
        }
        streamWriter2.Close();
        streamWriter1.Close();
      }
    }

    private string CreateMatFile(Material mat, int partnumber)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("newmtl " + mat.Name);
      string str = string.Format("{0} {1} {2}", (object) 1, (object) 1, (object) 1).Replace(',', '.');
      stringBuilder.AppendLine("   Ka " + str);
      stringBuilder.AppendLine("   Kd " + str);
      if (mat.Texture != -1)
        stringBuilder.AppendLine(string.Format("   map_Kd {0:0000}_{1}.dds", (object) mat.Texture, (object) Path.GetFileNameWithoutExtension(this.txgh.Names[mat.Texture])));
      stringBuilder.AppendLine();
      return stringBuilder.ToString();
    }

    private string CreateObjFile(Part part, Material mat, int partnumber)
    {
      VertexList vertexList1 = this.mesh.Vertexlistsdictionary[part.VertexListReferences1[0].Reference];
      VertexList vertexList2 = (VertexList) null;
      if (part.VertexListReferences1.Count > 1)
        vertexList2 = this.mesh.Vertexlistsdictionary[part.VertexListReferences1[1].Reference];
      List<ushort> IndexList = this.mesh.Indexlistsdictionary[part.IndexListReference1];
      StringBuilder objFileSub = this.CreateObjFileSub(part, mat, vertexList1, vertexList2, IndexList, partnumber);
      if (vertexList1.Vertices[0].Position != null)
      {
        Console.WriteLine("part.OffsetVertices: {0:x8}", (object) part.OffsetVertices);
        StringBuilder stringBuilder = new StringBuilder();
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
        {
          Vector3 position = vertexList1.Vertices[offsetVertices].Position;
          stringBuilder.AppendLine(string.Format("v {0:0.000000} {1:0.000000} {2:0.000000} ", (object) position.X, (object) position.Y, (object) position.Z).Replace(',', '.'));
        }
        stringBuilder.AppendLine(objFileSub.ToString());
        return stringBuilder.ToString();
      }
      if (vertexList2 != null && vertexList2.Vertices[0].Position != null)
      {
        Console.WriteLine("part.OffsetVertices2: {0:x8}", (object) part.OffsetVertices2);
        StreamWriter streamWriter = new StreamWriter(this.directoryname + "\\" + this.filenamewithoutextension + string.Format("{0:0000}", (object) partnumber) + ".obj");
        streamWriter.WriteLine("# " + this.filenamewithoutextension);
        streamWriter.WriteLine("mtllib " + this.filenamewithoutextension + string.Format("{0:0000}", (object) partnumber) + ".mtl");
        for (int offsetVertices2 = part.OffsetVertices2; offsetVertices2 < part.OffsetVertices2 + part.NumberVertices; ++offsetVertices2)
        {
          Vector3 position = vertexList2.Vertices[offsetVertices2].Position;
          streamWriter.WriteLine(string.Format("v {0:0.000000} {1:0.000000} {2:0.000000} ", (object) position.X, (object) position.Y, (object) position.Z).Replace(',', '.'));
        }
        streamWriter.Write(objFileSub.ToString());
        streamWriter.Close();
      }
      else if (part.VertexListReferences11 != null)
      {
        for (int index1 = 0; index1 < part.VertexListReferences11.Count; ++index1)
        {
          Console.WriteLine("part.OffsetVertices11: {0:x8}", (object) index1);
          StreamWriter streamWriter = new StreamWriter(this.directoryname + "\\" + this.filenamewithoutextension + string.Format("{0:0000}.{1:000}", (object) partnumber, (object) index1) + ".obj");
          streamWriter.WriteLine("# " + this.filenamewithoutextension);
          streamWriter.WriteLine("mtllib " + this.filenamewithoutextension + string.Format("{0:0000}", (object) partnumber) + ".mtl");
          VertexList vertexList = this.mesh.Vertexlistsdictionary[part.VertexListReferences11[index1].Reference];
          for (int index2 = 0; index2 < vertexList.Vertices.Count; ++index2)
          {
            Vector3 position = vertexList.Vertices[index2].Position;
            streamWriter.WriteLine(string.Format("v {0:0.000000} {1:0.000000} {2:0.000000} ", (object) position.X, (object) position.Y, (object) position.Z).Replace(',', '.'));
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
      List<ushort> IndexList,
      int partnumber)
    {
      StringBuilder stringBuilder = new StringBuilder();
      bool flag1 = false;
      bool flag2 = false;
      List<Vertex> vertexList = (List<Vertex>) null;
      if (vertexList1.Vertices[0].UVSet0 != null)
      {
        flag2 = true;
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
        {
          Vector2 uvSet0 = vertexList1.Vertices[offsetVertices].UVSet0;
          stringBuilder.AppendLine(string.Format("vt {0:0.000000} {1:0.000000} ", (object) uvSet0.X, (object) uvSet0.Y).Replace(',', '.'));
        }
      }
      else if (vertexList2 != null && vertexList2.Vertices[0].UVSet0 != null)
      {
        flag2 = true;
        for (int offsetVertices2 = part.OffsetVertices2; offsetVertices2 < part.OffsetVertices2 + part.NumberVertices; ++offsetVertices2)
        {
          Vector2 uvSet0 = vertexList2.Vertices[offsetVertices2].UVSet0;
          stringBuilder.AppendLine(string.Format("vt {0:0.000000} {1:0.000000} ", (object) uvSet0.X, (object) uvSet0.Y).Replace(',', '.'));
        }
      }
      if (vertexList1.Vertices[0].Normal != null)
      {
        flag1 = true;
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
        {
          Vector3 normal = vertexList1.Vertices[offsetVertices].Normal;
          stringBuilder.AppendLine(string.Format("vn {0:0.000000} {1:0.000000} {2:0.000000} ", (object) normal.X, (object) normal.Y, (object) normal.Z).Replace(',', '.'));
        }
      }
      else if (vertexList2 != null && vertexList2.Vertices[0].Normal != null)
      {
        flag1 = true;
        for (int offsetVertices2 = part.OffsetVertices2; offsetVertices2 < part.OffsetVertices2 + part.NumberVertices; ++offsetVertices2)
        {
          Vector3 normal = vertexList2.Vertices[offsetVertices2].Normal;
          stringBuilder.AppendLine(string.Format("vn {0:0.000000} {1:0.000000} {2:0.000000} ", (object) normal.X, (object) normal.Y, (object) normal.Z).Replace(',', '.'));
        }
      }
      if (vertexList1.Vertices[0].ColorSet0 != null)
        vertexList = vertexList1.Vertices;
      else if (vertexList2 != null && vertexList2.Vertices[0].ColorSet0 != null)
        vertexList = vertexList2.Vertices;
      stringBuilder.AppendLine("usemtl " + mat.Name);
      string format = "f {0} {1} {2}";
      if (flag2)
        format = !flag1 ? "f {0}/{0} {1}/{1} {2}/{2}" : "f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}";
      else if (flag1)
        format = "f {0}//{0} {1}//{1} {2}//{2}";
      for (int offsetIndices = part.OffsetIndices; offsetIndices < part.OffsetIndices + part.NumberIndices; offsetIndices += 3)
        stringBuilder.AppendLine(string.Format(format, (object) ((int) IndexList[offsetIndices] - part.NumberVertices), (object) ((int) IndexList[offsetIndices + 1] - part.NumberVertices), (object) ((int) IndexList[offsetIndices + 2] - part.NumberVertices)));
      return stringBuilder;
    }

    private void WriteIntoObjFile(Part part, StreamWriter streamwriter)
    {
      bool flag1 = false;
      bool flag2 = false;
      List<Vertex> vertexList1 = (List<Vertex>) null;
      VertexList vertexList2 = this.mesh.Vertexlistsdictionary[part.VertexListReferences1[0].Reference];
      VertexList vertexList3 = (VertexList) null;
      if (part.VertexListReferences1.Count > 1)
        vertexList3 = this.mesh.Vertexlistsdictionary[part.VertexListReferences1[1].Reference];
      List<ushort> ushortList = this.mesh.Indexlistsdictionary[part.IndexListReference1];
      if (vertexList2.Vertices[0].Position != null)
      {
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
        {
          Vector3 position = vertexList2.Vertices[offsetVertices].Position;
          streamwriter.WriteLine(string.Format("v {0:0.000000} {1:0.000000} {2:0.000000} ", (object) position.X, (object) position.Y, (object) position.Z).Replace(',', '.'));
        }
      }
      else if (vertexList3 != null && vertexList3.Vertices[0].Position != null)
      {
        for (int offsetVertices2 = part.OffsetVertices2; offsetVertices2 < part.OffsetVertices2 + part.NumberVertices; ++offsetVertices2)
        {
          Vector3 position = vertexList3.Vertices[offsetVertices2].Position;
          streamwriter.WriteLine(string.Format("v {0:0.000000} {1:0.000000} {2:0.000000} ", (object) position.X, (object) position.Y, (object) position.Z).Replace(',', '.'));
        }
      }
      if (vertexList2.Vertices[0].UVSet0 != null)
      {
        flag2 = true;
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
        {
          Vector2 uvSet0 = vertexList2.Vertices[offsetVertices].UVSet0;
          streamwriter.WriteLine(string.Format("vt {0:0.000000} {1:0.000000} ", (object) uvSet0.X, (object) uvSet0.Y).Replace(',', '.'));
        }
      }
      else if (vertexList3 != null && vertexList3.Vertices[0].UVSet0 != null)
      {
        flag2 = true;
        for (int offsetVertices2 = part.OffsetVertices2; offsetVertices2 < part.OffsetVertices2 + part.NumberVertices; ++offsetVertices2)
        {
          Vector2 uvSet0 = vertexList3.Vertices[offsetVertices2].UVSet0;
          streamwriter.WriteLine(string.Format("vt {0:0.000000} {1:0.000000} ", (object) uvSet0.X, (object) uvSet0.Y).Replace(',', '.'));
        }
      }
      if (vertexList2.Vertices[0].Normal != null)
      {
        flag1 = true;
        for (int offsetVertices = part.OffsetVertices; offsetVertices < part.OffsetVertices + part.NumberVertices; ++offsetVertices)
        {
          Vector3 normal = vertexList2.Vertices[offsetVertices].Normal;
          streamwriter.WriteLine(string.Format("vn {0:0.000000} {1:0.000000} {2:0.000000} ", (object) normal.X, (object) normal.Y, (object) normal.Z).Replace(',', '.'));
        }
      }
      else if (vertexList3 != null && vertexList3.Vertices[0].Normal != null)
      {
        flag1 = true;
        for (int offsetVertices2 = part.OffsetVertices2; offsetVertices2 < part.OffsetVertices2 + part.NumberVertices; ++offsetVertices2)
        {
          Vector3 normal = vertexList3.Vertices[offsetVertices2].Normal;
          streamwriter.WriteLine(string.Format("vn {0:0.000000} {1:0.000000} {2:0.000000} ", (object) normal.X, (object) normal.Y, (object) normal.Z).Replace(',', '.'));
        }
      }
      if (vertexList2.Vertices[0].ColorSet0 != null)
        vertexList1 = vertexList2.Vertices;
      else if (vertexList3 != null && vertexList3.Vertices[0].ColorSet0 != null)
        vertexList1 = vertexList3.Vertices;
      string format = "f {0} {1} {2}";
      if (flag2)
        format = !flag1 ? "f {0}/{0} {1}/{1} {2}/{2}" : "f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}";
      else if (flag1)
        format = "f {0}//{0} {1}//{1} {2}//{2}";
      for (int offsetIndices = part.OffsetIndices; offsetIndices < part.OffsetIndices + part.NumberIndices; offsetIndices += 3)
        streamwriter.WriteLine(string.Format(format, (object) ((int) ushortList[offsetIndices] - part.NumberVertices), (object) ((int) ushortList[offsetIndices + 1] - part.NumberVertices), (object) ((int) ushortList[offsetIndices + 2] - part.NumberVertices)));
    }
  }
}
