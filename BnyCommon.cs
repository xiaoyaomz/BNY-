// Decompiled with JetBrains decompiler
// Type: BnyEditor.BnyCommon
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

#nullable disable
namespace BnyEditor
{
  public class BnyCommon
  {
    public static BnyStruct BnyData;
    public static DataTable MainTable;
    public static string CurrFileName;
    public static RootData Root = (RootData) null;
    public static FileItem FileDesc = (FileItem) null;
    public static bool IsHotFile = false;
    public static BnyNetwork Bnywork;
    public static bool IsShowChinese = true;
    public static FrmMain MainForm;
    public static Dictionary<string, SearchInfo> SearchData = (Dictionary<string, SearchInfo>) null;
    public static Dictionary<string, CustomShow> ShowData = (Dictionary<string, CustomShow>) null;
    public static string machineCode = string.Empty;
    public static List<FlagTable> FlagInfos = new List<FlagTable>();
    public static string FlagFile = AppDomain.CurrentDomain.BaseDirectory + "Flag.desc";

    public static bool WriteText(string fileName, string data)
    {
      try
      {
        using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        {
          using (StreamWriter streamWriter = new StreamWriter((Stream) fileStream, Encoding.UTF8))
          {
            streamWriter.Write(data);
            streamWriter.Flush();
            return true;
          }
        }
      }
      catch
      {
        return false;
      }
    }

    public static string ReadText(string fileName)
    {
      try
      {
        using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        {
          using (StreamReader streamReader = new StreamReader((Stream) fileStream, Encoding.UTF8))
            return streamReader.ReadToEnd();
        }
      }
      catch
      {
        return string.Empty;
      }
    }

    public static void GetSearchData()
    {
      try
      {
        BnyCommon.SearchData = new Dictionary<string, SearchInfo>();
        string path = AppDomain.CurrentDomain.BaseDirectory + "\\搜索设置.txt";
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        string empty1 = string.Empty;
        if (!File.Exists(path))
          return;
        using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
          using (StreamReader streamReader = new StreamReader((Stream) fileStream, Encoding.Default))
          {
            string empty2 = string.Empty;
            string str;
            while ((str = streamReader.ReadLine()) != null)
            {
              if (!string.IsNullOrEmpty(str) && str.Substring(0, 1) != "#")
              {
                string[] strArray = str.Split('|');
                if (strArray[0] == "文件目录")
                  empty1 = strArray[1];
                else if (!dictionary.ContainsKey(strArray[0]))
                  dictionary.Add(strArray[0], strArray[1]);
              }
            }
          }
        }
        if (string.IsNullOrEmpty(empty1) || !Directory.Exists(empty1))
          return;
        foreach (KeyValuePair<string, string> keyValuePair in dictionary)
        {
          if (File.Exists(empty1 + "\\" + keyValuePair.Value))
          {
            if (!BnyCommon.SearchData.ContainsKey(keyValuePair.Key))
            {
              try
              {
                BnySearchUtils bnySearchUtils = new BnySearchUtils();
                SearchInfo searchInfo = new SearchInfo();
                bnySearchUtils.SearchFile = searchInfo;
                bnySearchUtils.ReadFile(empty1 + "\\" + keyValuePair.Value);
                string empty3 = string.Empty;
                string str1 = keyValuePair.Value.Replace(".bny", "");
                string str2 = str1.IndexOf(".") <= -1 ? str1 : str1.Substring(str1.LastIndexOf(".") + 1);
                if (!string.IsNullOrEmpty(str2) && BnyCommon.Root != null)
                {
                  foreach (FileItem data in BnyCommon.Root.datas)
                  {
                    if (data.name == str2)
                    {
                      searchInfo.FileDesc = data;
                      break;
                    }
                  }
                }
                BnyCommon.SearchData.Add(keyValuePair.Key, searchInfo);
              }
              catch
              {
              }
            }
          }
        }
      }
      catch
      {
      }
    }

    public static void GetShowData()
    {
      try
      {
        string path = AppDomain.CurrentDomain.BaseDirectory + "\\自定义显示.txt";
        BnyCommon.ShowData = new Dictionary<string, CustomShow>();
        string empty1 = string.Empty;
        if (!File.Exists(path))
          return;
        using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
          using (StreamReader streamReader = new StreamReader((Stream) fileStream, Encoding.Default))
          {
            string empty2 = string.Empty;
            string str;
            while ((str = streamReader.ReadLine()) != null)
            {
              if (!string.IsNullOrEmpty(str) && str.Substring(0, 1) != "#")
              {
                string[] strArray = str.Split('|');
                if (strArray[0] == "文件目录")
                {
                  empty1 = strArray[1];
                }
                else
                {
                  CustomShow customShow = new CustomShow();
                  customShow.MenuName = strArray[0];
                  customShow.FileName = strArray[1];
                  customShow.Key = strArray[2];
                  customShow.ShowName = strArray[3];
                  if (!BnyCommon.ShowData.ContainsKey(customShow.MenuName))
                    BnyCommon.ShowData.Add(customShow.MenuName, customShow);
                }
              }
            }
          }
        }
        if (string.IsNullOrEmpty(empty1) || !Directory.Exists(empty1))
          return;
        foreach (KeyValuePair<string, CustomShow> keyValuePair in BnyCommon.ShowData)
        {
          if (File.Exists(empty1 + "\\" + keyValuePair.Value.FileName))
          {
            try
            {
              BnySearchUtils bnySearchUtils = new BnySearchUtils();
              SearchInfo searchInfo = new SearchInfo();
              bnySearchUtils.SearchFile = searchInfo;
              bnySearchUtils.ReadFile(empty1 + "\\" + keyValuePair.Value.FileName);
              keyValuePair.Value.MainTable = searchInfo.MainTable;
            }
            catch
            {
            }
          }
        }
      }
      catch
      {
      }
    }

    public static void ReloadRoot()
    {
      try
      {
        if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "data.desc"))
          return;
        using (FileStream fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "data.desc", FileMode.Open, FileAccess.Read))
        {
          using (StreamReader streamReader = new StreamReader((Stream) fileStream, Encoding.UTF8))
            BnyCommon.Root = JsonConvert.DeserializeObject<RootData>(EncryptHelper.DESDecrypt(streamReader.ReadToEnd()));
        }
      }
      catch
      {
      }
    }

    public static void FindFileDesc()
    {
      if (!string.IsNullOrEmpty(BnyCommon.CurrFileName) && BnyCommon.Root != null)
      {
        foreach (FileItem data in BnyCommon.Root.datas)
        {
          if (data.name.ToLower() == BnyCommon.CurrFileName.ToLower())
          {
            BnyCommon.FileDesc = data;
            return;
          }
        }
      }
      BnyCommon.FileDesc = (FileItem) null;
    }

    public static bool IsMoreInfo()
    {
      foreach (StructExplain explain in BnyCommon.BnyData.Explains)
      {
        if (explain.Structs != null)
          return true;
      }
      return false;
    }
  }
}
