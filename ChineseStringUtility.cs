// Decompiled with JetBrains decompiler
// Type: BnyEditor.ChineseStringUtility
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System.Runtime.InteropServices;

#nullable disable
namespace BnyEditor
{
  public class ChineseStringUtility
  {
    private const int LOCALE_SYSTEM_DEFAULT = 2048;
    private const int LCMAP_SIMPLIFIED_CHINESE = 33554432;
    private const int LCMAP_TRADITIONAL_CHINESE = 67108864;

    [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int LCMapString(
      int Locale,
      int dwMapFlags,
      string lpSrcStr,
      int cchSrc,
      [Out] string lpDestStr,
      int cchDest);

    public static string ToSimplified(string source)
    {
      string lpDestStr = new string(' ', source.Length);
      ChineseStringUtility.LCMapString(2048, 33554432, source, source.Length, lpDestStr, source.Length);
      return lpDestStr;
    }

    public static string ToTraditional(string source)
    {
      string lpDestStr = new string(' ', source.Length);
      ChineseStringUtility.LCMapString(2048, 67108864, source, source.Length, lpDestStr, source.Length);
      return lpDestStr;
    }
  }
}
