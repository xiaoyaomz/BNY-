// Decompiled with JetBrains decompiler
// Type: BnyEditor.Program
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System;
using System.Windows.Forms;

#nullable disable
namespace BnyEditor
{
  internal static class Program
  {
    [STAThread]
    private static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      if (args != null && args.Length != 0)
      {
        string file = "";
        for (int index = 0; index < args.Length; ++index)
          file = file + " " + args[index];
        Application.Run((Form) new FrmMain(file));
      }
      else
        Application.Run((Form) new FrmMain());
    }
  }
}
