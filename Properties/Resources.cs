// Decompiled with JetBrains decompiler
// Type: ShaderEdit.Properties.Resources
// Assembly: ShaderEdit, Version=0.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1518E5EC-D0FE-421C-9947-B6E80B5CE6F0

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ShaderEdit.Properties
{
  [DebuggerNonUserCode]
  [CompilerGenerated]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
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
        if (ShaderEdit.Properties.Resources.resourceMan == null)
          ShaderEdit.Properties.Resources.resourceMan = new ResourceManager("ShaderEdit.Properties.Resources", typeof (ShaderEdit.Properties.Resources).Assembly);
        return ShaderEdit.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => ShaderEdit.Properties.Resources.resourceCulture;
      set => ShaderEdit.Properties.Resources.resourceCulture = value;
    }
  }
}
