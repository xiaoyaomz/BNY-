// Decompiled with JetBrains decompiler
// Type: BnyEditor.SearchInfo
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System.Data;

#nullable disable
namespace BnyEditor
{
  public class SearchInfo
  {
    public BnyStruct BnyData { get; set; }

    public DataTable MainTable { get; set; }

    public FileItem FileDesc { get; set; }
  }
}
