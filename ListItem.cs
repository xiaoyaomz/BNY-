// Decompiled with JetBrains decompiler
// Type: BnyEditor.ListItem
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

#nullable disable
namespace BnyEditor
{
  public class ListItem
  {
    public string Text { get; set; }

    public object Value { get; set; }

    public ListItem(string text, object value)
    {
      this.Text = text;
      this.Value = value;
    }

    public override string ToString() => this.Text;
  }
}
