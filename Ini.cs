// Decompiled with JetBrains decompiler
// Type: BnyEditor.Ini
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace BnyEditor
{
  public class Ini
  {
    private string sPath;

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(
      string section,
      string key,
      string val,
      string filePath);

    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(
      string section,
      string key,
      string def,
      StringBuilder retVal,
      int size,
      string filePath);

    public Ini(string path) => this.sPath = path;

    public void Writue(string section, string key, string value)
    {
      Ini.WritePrivateProfileString(section, key, value, this.sPath);
    }

    public string ReadValue(string section, string key)
    {
      StringBuilder retVal = new StringBuilder((int) byte.MaxValue);
      Ini.GetPrivateProfileString(section, key, "", retVal, (int) byte.MaxValue, this.sPath);
      return retVal.ToString();
    }
  }
}
