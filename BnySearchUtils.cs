// Decompiled with JetBrains decompiler
// Type: BnyEditor.BnySearchUtils
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

#nullable disable
namespace BnyEditor
{
  public class BnySearchUtils
  {
    public SearchInfo SearchFile { get; set; }

    private void GenerateTable(BnyStruct bny)
    {
      this.SearchFile.MainTable = new DataTable();
      int num = 0;
      for (int index = 0; index < bny.Explains.Length; ++index)
      {
        try
        {
          if (BnyCommon.Bnywork != null)
            BnyCommon.Bnywork.TableAddColum(this.SearchFile.MainTable, (int) bny.Explains[index].Type, bny.Explains[index].Name);
        }
        catch
        {
          if (BnyCommon.Bnywork != null)
          {
            BnyCommon.Bnywork.TableAddColum(this.SearchFile.MainTable, (int) bny.Explains[index].Type, bny.Explains[index].Name + "_" + (object) num);
            ++num;
          }
        }
      }
    }

    public StructExplain Find(string name)
    {
      foreach (StructExplain explain in this.SearchFile.BnyData.Explains)
      {
        if (explain.Name == name)
          return explain;
      }
      int num = 0;
      foreach (StructExplain explain in this.SearchFile.BnyData.Explains)
      {
        if (explain.Name + "_" + (object) num == name)
          return explain;
        if (name.IndexOf(explain.Name) > -1)
          ++num;
      }
      return (StructExplain) null;
    }

    public StructExplain Find(StructExplain se, string name)
    {
      if (se.Structs == null)
        return (StructExplain) null;
      foreach (StructExplain structExplain in se.Structs)
      {
        if (structExplain.Name == name)
          return structExplain;
      }
      foreach (StructExplain se1 in se.Structs)
      {
        if (se1.Structs != null)
        {
          StructExplain structExplain = this.Find(se1, name);
          if (structExplain != null)
            return structExplain;
        }
      }
      return (StructExplain) null;
    }

    public DataTable GenerateSubTable(StructExplain se)
    {
      DataTable dt = new DataTable(se.Name);
      for (int index = 0; index < se.Structs.Count; ++index)
      {
        if (BnyCommon.Bnywork != null)
          BnyCommon.Bnywork.TableAddColum(dt, (int) se.Structs[index].Type, se.Structs[index].Name);
      }
      dt.AcceptChanges();
      return dt;
    }

    private DataTable ReadTableData(StructExplain se, BinaryReader br)
    {
      DataTable subTable = this.GenerateSubTable(se);
      int serverI1 = BnyUtils.GetServerI(br);
      for (int index1 = 0; index1 < serverI1; ++index1)
      {
        DataRow row = subTable.NewRow();
        for (int index2 = 0; index2 < se.Structs.Count; ++index2)
        {
          switch (se.Structs[index2].Type)
          {
            case 0:
            case 6:
              row[index2] = (object) BnyUtils.GetServerI(br);
              break;
            case 4:
            case 13:
              row[index2] = (object) br.ReadByte();
              break;
            case 9:
              int serverI2 = BnyUtils.GetServerI(br);
              if (serverI2 > 0)
              {
                row[index2] = (object) Encoding.UTF8.GetString(br.ReadBytes(serverI2));
                break;
              }
              break;
            case 11:
              row[index2] = (object) new List<DataTable>();
              if (se.Structs[index2].Structs != null && se.Structs[index2].Structs.Count > 0)
              {
                using (List<StructExplain>.Enumerator enumerator = se.Structs[index2].Structs.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    StructExplain current = enumerator.Current;
                    (row[index2] as List<DataTable>).Add(this.ReadTableData(current, br));
                  }
                  break;
                }
              }
              else
                break;
            case 12:
              row[index2] = (object) this.ReadTableData(se.Structs[index2], br);
              break;
          }
        }
        subTable.Rows.Add(row);
      }
      subTable.AcceptChanges();
      return subTable;
    }

    private void ReadData(BnyStruct bny, BinaryReader br)
    {
      br.BaseStream.Position = br.BaseStream.Length - 20L;
      bny.StructCount = br.ReadInt32();
      bny.OffsetStart = br.ReadInt32();
      bny.OffsetSize = br.ReadInt32();
      bny.StructExPlainStart = br.ReadInt32();
      bny.StructExPlainSize = br.ReadInt32();
      br.BaseStream.Position = 0L;
      bny.Datas = br.ReadBytes(bny.StructExPlainStart);
      bny.StructExplainBackUpData = br.ReadBytes(bny.StructExPlainSize);
      br.BaseStream.Position = (long) bny.StructExPlainStart;
      bny.StructExplainCount = BnyUtils.GetServerI(br);
      bny.Explains = new StructExplain[bny.StructExplainCount];
      for (int index = 0; index < bny.StructExplainCount; ++index)
      {
        bny.Explains[index] = new StructExplain();
        BnyUtils.ReadStructExplain(br, bny.Explains[index]);
      }
      bny.Type = br.ReadByte();
      this.GenerateTable(bny);
      br.BaseStream.Position = 0L;
      for (int index = 0; index < bny.StructCount && br.BaseStream.Position <= (long) bny.StructExPlainStart; ++index)
      {
        DataRow row = this.SearchFile.MainTable.NewRow();
        for (int columnIndex = 0; columnIndex < bny.Explains.Length; ++columnIndex)
        {
          switch (bny.Explains[columnIndex].Type)
          {
            case 0:
            case 6:
              row[columnIndex] = (object) BnyUtils.GetServerI(br);
              break;
            case 4:
            case 13:
              row[columnIndex] = (object) br.ReadByte();
              break;
            case 9:
              int serverI = BnyUtils.GetServerI(br);
              if (serverI > 0)
              {
                row[columnIndex] = (object) Encoding.UTF8.GetString(br.ReadBytes(serverI));
                break;
              }
              break;
            case 11:
              row[columnIndex] = (object) new List<DataTable>();
              if (bny.Explains[columnIndex].Structs.Count > 0)
              {
                using (List<StructExplain>.Enumerator enumerator = bny.Explains[columnIndex].Structs.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    StructExplain current = enumerator.Current;
                    (row[columnIndex] as List<DataTable>).Add(this.ReadTableData(current, br));
                  }
                  break;
                }
              }
              else
                break;
            case 12:
              row[columnIndex] = (object) this.ReadTableData(bny.Explains[columnIndex], br);
              break;
          }
        }
        this.SearchFile.MainTable.Rows.Add(row);
      }
    }

    public void ReadFile(string FileName)
    {
      this.SearchFile.BnyData = (BnyStruct) null;
      this.SearchFile.MainTable = (DataTable) null;
      BnyStruct bny = new BnyStruct();
      if (BnyUtils.ReadFileIsHot(FileName))
      {
        using (MemoryStream input = new MemoryStream(HotUpdate.DecompressBny(FileName)))
        {
          using (BinaryReader br = new BinaryReader((Stream) input, Encoding.UTF8))
            this.ReadData(bny, br);
        }
      }
      else
      {
        using (FileStream input = new FileStream(FileName, FileMode.Open, FileAccess.Read))
        {
          using (BinaryReader br = new BinaryReader((Stream) input, Encoding.UTF8))
            this.ReadData(bny, br);
        }
      }
      this.SearchFile.BnyData = bny;
    }
  }
}
