// Decompiled with JetBrains decompiler
// Type: BnyEditor.FlagInfo
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

#nullable disable
namespace BnyEditor
{
  public class FlagInfo
  {
    public bool isValueType { get; set; }

    public int ColumnIndex { get; set; } = -1;

    public int RowIndex { get; set; } = -1;

    public string ColumnName { get; set; }

    public string FlagName { get; set; }

    public string Value { get; set; }
  }
}
