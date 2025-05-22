// Decompiled with JetBrains decompiler
// Type: BnyEditor.FlagUtils
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace BnyEditor
{
  public class FlagUtils
  {
    public static void LoadFlags()
    {
      if (!File.Exists(BnyCommon.FlagFile))
        return;
      BnyCommon.FlagInfos.Clear();
      BnyCommon.FlagInfos = JsonConvert.DeserializeObject<List<FlagTable>>(EncryptHelper.AESDecrypt(BnyCommon.ReadText(BnyCommon.FlagFile)));
    }

    public static bool WriteFlags()
    {
      return BnyCommon.FlagInfos.Count == 0 || BnyCommon.WriteText(BnyCommon.FlagFile, EncryptHelper.AESEncrypt(JsonConvert.SerializeObject((object) BnyCommon.FlagInfos)));
    }

    public static FlagTable CurrentFlagTable()
    {
      if (!string.IsNullOrEmpty(BnyCommon.CurrFileName))
      {
        foreach (FlagTable flagInfo in BnyCommon.FlagInfos)
        {
          if (flagInfo.FileName.ToLower() == BnyCommon.CurrFileName.ToLower())
            return flagInfo;
        }
      }
      return (FlagTable) null;
    }

    public static bool DeleteFlag(string columnname, string value, int col, int row)
    {
      FlagTable flagTable = FlagUtils.CurrentFlagTable();
      if (flagTable == null)
        return false;
      foreach (FlagInfo info in flagTable.Infos)
      {
        if (info.ColumnName == columnname)
        {
          if (info.isValueType)
          {
            if (info.Value == value)
            {
              flagTable.Infos.Remove(info);
              return true;
            }
          }
          else if (info.RowIndex == row && info.ColumnIndex == col)
          {
            flagTable.Infos.Remove(info);
            return true;
          }
        }
      }
      return false;
    }

    public static bool AddOrEditFlag(FlagInfo info)
    {
      bool flag;
      try
      {
        if (!string.IsNullOrEmpty(BnyCommon.CurrFileName))
        {
          foreach (FlagTable flagInfo in BnyCommon.FlagInfos)
          {
            if (flagInfo.FileName.ToLower() == BnyCommon.CurrFileName.ToLower())
            {
              for (int index = 0; index < flagInfo.Infos.Count; ++index)
              {
                if (flagInfo.Infos[index].ColumnName == info.ColumnName)
                {
                  if (flagInfo.Infos[index].isValueType && flagInfo.Infos[index].Value == info.Value)
                  {
                    flagInfo.Infos[index] = info;
                    return true;
                  }
                  if (!flagInfo.Infos[index].isValueType && flagInfo.Infos[index].ColumnIndex == info.ColumnIndex && flagInfo.Infos[index].RowIndex == info.RowIndex)
                  {
                    flagInfo.Infos[index] = info;
                    return true;
                  }
                }
              }
              flagInfo.Infos.Add(info);
              return true;
            }
          }
          FlagTable flagTable = new FlagTable()
          {
            FileName = BnyCommon.CurrFileName,
            Infos = new List<FlagInfo>()
          };
          flagTable.Infos.Add(info);
          BnyCommon.FlagInfos.Add(flagTable);
          flag = true;
        }
        else
        {
          FlagTable flagTable = new FlagTable()
          {
            FileName = BnyCommon.CurrFileName,
            Infos = new List<FlagInfo>()
          };
          flagTable.Infos.Add(info);
          BnyCommon.FlagInfos.Add(flagTable);
          flag = true;
        }
      }
      catch
      {
        flag = false;
      }
      return flag;
    }

    public static string getFlag(string columnname, string value, int col, int row)
    {
      FlagTable flagTable = FlagUtils.CurrentFlagTable();
      if (flagTable != null)
      {
        foreach (FlagInfo info in flagTable.Infos)
        {
          if (info.ColumnName == columnname)
          {
            if (info.isValueType)
            {
              if (info.Value == value)
                return info.FlagName;
            }
            else if (info.RowIndex == row && info.ColumnIndex == col)
              return info.FlagName;
          }
        }
      }
      return string.Empty;
    }

    public static FlagInfo getFlagInfo(string columnname, string value, int col, int row)
    {
      FlagTable flagTable = FlagUtils.CurrentFlagTable();
      if (flagTable != null)
      {
        foreach (FlagInfo info in flagTable.Infos)
        {
          if (info.ColumnName == columnname)
          {
            if (info.isValueType)
            {
              if (info.Value == value)
                return info;
            }
            else if (info.RowIndex == row && info.ColumnIndex == col)
              return info;
          }
        }
      }
      return (FlagInfo) null;
    }
  }
}
