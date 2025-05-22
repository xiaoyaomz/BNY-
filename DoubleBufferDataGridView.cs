// Decompiled with JetBrains decompiler
// Type: BnyEditor.DoubleBufferDataGridView
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace BnyEditor
{
  public static class DoubleBufferDataGridView
  {
    public static void DoubleBufferedDataGirdView(this DataGridView dgv, bool flag)
    {
      dgv.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue((object) dgv, (object) flag, (object[]) null);
    }
  }
}
