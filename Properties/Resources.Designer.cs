// Decompiled with JetBrains decompiler
// Type: Lego_Pak_Explorer.Properties.Resources
// Assembly: TT Games Explorer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C58D5CF4-46F7-4F26-903C-D70EAAA2DE7D
// Assembly location: C:\Users\baele\Downloads\TT Games Explorer\TT Games Explorer\TT Games Explorer.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Lego_Pak_Explorer.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (ReferenceEquals((object) resourceMan, (object) null))
          resourceMan = new ResourceManager("Lego_Pak_Explorer.Properties.Resources", typeof (Resources).Assembly);
        return resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => resourceCulture;
      set => resourceCulture = value;
    }

    internal static Bitmap arrow_refresh => (Bitmap) ResourceManager.GetObject(nameof (arrow_refresh), resourceCulture);

    internal static Bitmap BlackFigure => (Bitmap) ResourceManager.GetObject(nameof (BlackFigure), resourceCulture);

    internal static Bitmap brick_go => (Bitmap) ResourceManager.GetObject(nameof (brick_go), resourceCulture);

    internal static Bitmap cancel => (Bitmap) ResourceManager.GetObject(nameof (cancel), resourceCulture);

    internal static Bitmap cog => (Bitmap) ResourceManager.GetObject(nameof (cog), resourceCulture);

    internal static Bitmap database_refresh => (Bitmap) ResourceManager.GetObject(nameof (database_refresh), resourceCulture);

    internal static Bitmap disk => (Bitmap) ResourceManager.GetObject(nameof (disk), resourceCulture);

    internal static Bitmap disk_multiple => (Bitmap) ResourceManager.GetObject(nameof (disk_multiple), resourceCulture);

    internal static Bitmap folder => (Bitmap) ResourceManager.GetObject(nameof (folder), resourceCulture);

    internal static Bitmap folder_brick => (Bitmap) ResourceManager.GetObject(nameof (folder_brick), resourceCulture);

    internal static Bitmap folder_go => (Bitmap) ResourceManager.GetObject(nameof (folder_go), resourceCulture);

    internal static Bitmap GenericFigure => (Bitmap) ResourceManager.GetObject(nameof (GenericFigure), resourceCulture);

    internal static Bitmap help => (Bitmap) ResourceManager.GetObject(nameof (help), resourceCulture);

    internal static Bitmap magnifier => (Bitmap) ResourceManager.GetObject(nameof (magnifier), resourceCulture);

    internal static Bitmap transpback1 => (Bitmap) ResourceManager.GetObject(nameof (transpback1), resourceCulture);

    internal static Bitmap wrench => (Bitmap) ResourceManager.GetObject(nameof (wrench), resourceCulture);
  }
}
