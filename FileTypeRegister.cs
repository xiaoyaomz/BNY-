// Decompiled with JetBrains decompiler
// Type: BnyEditor.FileTypeRegister
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using Microsoft.Win32;

#nullable disable
namespace BnyEditor
{
  public class FileTypeRegister
  {
    public static void RegisterFileType(FileTypeRegInfo regInfo)
    {
      if (FileTypeRegister.FileTypeRegistered(regInfo.ExtendName))
      {
        FileTypeRegister.UpdateFileTypeRegInfo(regInfo);
      }
      else
      {
        string subkey = regInfo.ExtendName.Substring(1, regInfo.ExtendName.Length - 1).ToUpper() + "_FileType";
        RegistryKey subKey1 = Registry.ClassesRoot.CreateSubKey(regInfo.ExtendName);
        subKey1.SetValue("", (object) subkey);
        subKey1.Close();
        RegistryKey subKey2 = Registry.ClassesRoot.CreateSubKey(subkey);
        subKey2.SetValue("", (object) regInfo.Description);
        subKey2.CreateSubKey("DefaultIcon").SetValue("", (object) regInfo.IcoPath);
        subKey2.CreateSubKey("Shell").CreateSubKey("Open").CreateSubKey("Command").SetValue("", (object) (regInfo.ExePath + " %1"));
        subKey2.Close();
      }
    }

    public static FileTypeRegInfo GetFileTypeRegInfo(string extendName)
    {
      if (!FileTypeRegister.FileTypeRegistered(extendName))
        return (FileTypeRegInfo) null;
      FileTypeRegInfo fileTypeRegInfo = new FileTypeRegInfo(extendName);
      string name = extendName.Substring(1, extendName.Length - 1).ToUpper() + "_FileType";
      RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(name);
      fileTypeRegInfo.Description = registryKey.GetValue("").ToString();
      fileTypeRegInfo.IcoPath = registryKey.OpenSubKey("DefaultIcon").GetValue("").ToString();
      string str = registryKey.OpenSubKey("Shell").OpenSubKey("Open").OpenSubKey("Command").GetValue("").ToString();
      fileTypeRegInfo.ExePath = str.Substring(0, str.Length - 3);
      return fileTypeRegInfo;
    }

    public static bool UpdateFileTypeRegInfo(FileTypeRegInfo regInfo)
    {
      if (!FileTypeRegister.FileTypeRegistered(regInfo.ExtendName))
        return false;
      try
      {
        string extendName = regInfo.ExtendName;
        string name = extendName.Substring(1, extendName.Length - 1).ToUpper() + "_FileType";
        RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(name, true);
        registryKey.SetValue("", (object) regInfo.Description);
        registryKey.OpenSubKey("DefaultIcon", true).SetValue("", (object) regInfo.IcoPath);
        registryKey.OpenSubKey("Shell").OpenSubKey("Open").OpenSubKey("Command", true).SetValue("", (object) (regInfo.ExePath + " %1"));
        registryKey.Close();
        return true;
      }
      catch
      {
        Registry.ClassesRoot.DeleteSubKey(regInfo.ExtendName);
        FileTypeRegister.RegisterFileType(regInfo);
        return true;
      }
    }

    public static bool FileTypeRegistered(string extendName)
    {
      return Registry.ClassesRoot.OpenSubKey(extendName) != null;
    }
  }
}
