// Decompiled with JetBrains decompiler
// Type: BnyEditor.BnyStruct
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

#nullable disable
namespace BnyEditor
{
  public class BnyStruct
  {
    public byte[] Datas { get; set; }

    public byte[] StructExplainBackUpData { get; set; }

    public int StructExplainCount { get; set; }

    public StructExplain[] Explains { get; set; }

    public byte Type { get; set; }

    public int OffsetCount { get; set; }

    public OffsetStruct[] OffsetStructs { get; set; }

    public int StructCount { get; set; }

    public int OffsetStart { get; set; }

    public int OffsetSize { get; set; }

    public int StructExPlainStart { get; set; }

    public int StructExPlainSize { get; set; }
  }
}
