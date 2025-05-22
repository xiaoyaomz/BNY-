// Decompiled with JetBrains decompiler
// Type: BnyEditor.DescUtil
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

#nullable disable
namespace BnyEditor
{
  public static class DescUtil
  {
    public static string Desc(this string name)
    {
      if (!BnyCommon.IsShowChinese || BnyCommon.FileDesc == null)
        return name;
      foreach (FiledItem filedItem in BnyCommon.FileDesc.filed)
      {
        if (filedItem.name.ToLower() == name.ToLower())
          return filedItem.desc.Replace("\r\n", "").Replace("\n", "");
      }
      return name;
    }

    public static string Tips(this string name)
    {
      if (BnyCommon.FileDesc == null)
        return name;
      foreach (FiledItem filedItem in BnyCommon.FileDesc.filed)
      {
        if (filedItem.name.ToLower() == name.ToLower())
          return filedItem.tip.Replace("<br>", "\r\n");
      }
      return name;
    }
  }
}
