// Decompiled with JetBrains decompiler
// Type: BnyEditor.BnyUtils
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace BnyEditor
{
  public class BnyUtils
  {
    public static int GetServerI(BinaryReader br)
    {
      return BnyCommon.Bnywork != null ? BnyCommon.Bnywork.ServerGetInt(br) : -1;
    }

    private static void SetServerI(BinaryWriter bw, int val)
    {
      if (BnyCommon.Bnywork == null)
        return;
      BnyCommon.Bnywork.SeverSetInt(bw, val);
    }

    private static void GenerateTable(BnyStruct bny)
    {
      BnyCommon.MainTable = new DataTable();
      int num = 0;
      for (int index = 0; index < bny.Explains.Length; ++index)
      {
        try
        {
          if (BnyCommon.Bnywork != null)
            BnyCommon.Bnywork.TableAddColum(BnyCommon.MainTable, (int) bny.Explains[index].Type, bny.Explains[index].Name);
        }
        catch
        {
          if (BnyCommon.Bnywork != null)
          {
            BnyCommon.Bnywork.TableAddColum(BnyCommon.MainTable, (int) bny.Explains[index].Type, bny.Explains[index].Name + "_" + (object) num);
            ++num;
          }
        }
      }
    }

    public static StructExplain Find(string name)
    {
      foreach (StructExplain explain in BnyCommon.BnyData.Explains)
      {
        if (explain.Name == name)
          return explain;
      }
      int num = 0;
      foreach (StructExplain explain in BnyCommon.BnyData.Explains)
      {
        if (explain.Name + "_" + (object) num == name)
          return explain;
        if (name.IndexOf(explain.Name) > -1)
          ++num;
      }
      return (StructExplain) null;
    }

    public static StructExplain Find(StructExplain se, string name)
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
          StructExplain structExplain = BnyUtils.Find(se1, name);
          if (structExplain != null)
            return structExplain;
        }
      }
      return (StructExplain) null;
    }

    public static DataTable GenerateSubTable(StructExplain se)
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

    private static DataTable ReadTableData(StructExplain se, BinaryReader br)
    {
      DataTable subTable = BnyUtils.GenerateSubTable(se);
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
                    (row[index2] as List<DataTable>).Add(BnyUtils.ReadTableData(current, br));
                  }
                  break;
                }
              }
              else
                break;
            case 12:
              row[index2] = (object) BnyUtils.ReadTableData(se.Structs[index2], br);
              break;
            default:
              Console.WriteLine("未知类型:" + (object) se.Structs[index2].Type);
              break;
          }
        }
        subTable.Rows.Add(row);
      }
      subTable.AcceptChanges();
      return subTable;
    }

    public static void WriteFile(string fileName, DataGridView dataGridView = null)
    {
      List<object> list = new List<object>();
      string str = fileName;
      if (BnyCommon.IsHotFile)
        str += ".tmp";
      using (FileStream output = new FileStream(str, FileMode.OpenOrCreate, FileAccess.Write))
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output, Encoding.UTF8))
        {
          binaryWriter.BaseStream.Position = 0L;
          int num = 0;
          if (dataGridView == null)
          {
            foreach (DataRow row in (InternalDataCollectionBase) BnyCommon.MainTable.Rows)
              num = BnyUtils.WriteFile2(list, binaryWriter, row, num);
          }
          else
          {
            for (int index = 0; index < dataGridView.Rows.Count; ++index)
            {
              DataRow row = (dataGridView.Rows[index].DataBoundItem as DataRowView).Row;
              num = BnyUtils.WriteFile2(list, binaryWriter, row, num);
            }
          }
          BnyCommon.BnyData.StructExPlainStart = (int) binaryWriter.BaseStream.Position;
          binaryWriter.Write(BnyCommon.BnyData.StructExplainBackUpData);
          BnyCommon.BnyData.OffsetStart = (int) binaryWriter.BaseStream.Position;
          binaryWriter.Write(BnyCommon.BnyData.Type);
          BnyUtils.SetServerI(binaryWriter, list.Count);
          foreach (object obj in list)
          {
            if (BnyCommon.BnyData.Type == (byte) 1)
            {
              OffsetStruct offsetStruct = obj as OffsetStruct;
              BnyUtils.SetServerI(binaryWriter, offsetStruct.Id);
              BnyUtils.SetServerI(binaryWriter, offsetStruct.Offset);
              BnyUtils.SetServerI(binaryWriter, offsetStruct.Size);
            }
            else
            {
              StringOffsetStruct stringOffsetStruct = obj as StringOffsetStruct;
              BnyUtils.SetServerI(binaryWriter, stringOffsetStruct.Len);
              binaryWriter.Write(stringOffsetStruct.Name);
              BnyUtils.SetServerI(binaryWriter, stringOffsetStruct.Offset);
              BnyUtils.SetServerI(binaryWriter, stringOffsetStruct.Size);
            }
          }
          BnyCommon.BnyData.OffsetSize = (int) binaryWriter.BaseStream.Position - BnyCommon.BnyData.OffsetStart;
          binaryWriter.Write(BnyCommon.MainTable.Rows.Count);
          binaryWriter.Write(BnyCommon.BnyData.OffsetStart);
          binaryWriter.Write(BnyCommon.BnyData.OffsetSize);
          binaryWriter.Write(BnyCommon.BnyData.StructExPlainStart);
          binaryWriter.Write(BnyCommon.BnyData.StructExPlainSize);
        }
      }
      if (!BnyCommon.IsHotFile)
        return;
      HotUpdate.ToHotBnyFile(str, fileName);
      File.Delete(str);
    }

    public static int WriteFile2(
      List<object> list,
      BinaryWriter binaryWriter,
      DataRow dataRow,
      int num)
    {
      int position = (int) binaryWriter.BaseStream.Position;
      foreach (DataColumn column in (InternalDataCollectionBase) BnyCommon.MainTable.Columns)
      {
        if (column.DataType == typeof (int))
          BnyUtils.SetServerI(binaryWriter, int.Parse(dataRow[column.ColumnName].ToString()));
        else if (column.DataType == typeof (string))
        {
          byte[] bytes = Encoding.UTF8.GetBytes(dataRow[column.ColumnName].ToString());
          BnyUtils.SetServerI(binaryWriter, bytes.Length);
          binaryWriter.Write(bytes);
        }
        else if (column.DataType == typeof (byte))
          binaryWriter.Write(byte.Parse(dataRow[column.ColumnName].ToString()));
        else if (column.DataType == typeof (List<DataTable>))
        {
          foreach (DataTable dt in dataRow[column.ColumnName] as List<DataTable>)
            BnyUtils.WriteTableData(binaryWriter, dt);
        }
        else if (column.DataType == typeof (DataTable))
          BnyUtils.WriteTableData(binaryWriter, dataRow[column.ColumnName] as DataTable);
      }
      if (BnyCommon.BnyData.Type == (byte) 1)
      {
        list.Add((object) new OffsetStruct()
        {
          Id = int.Parse(dataRow[0].ToString()),
          Size = ((int) binaryWriter.BaseStream.Position - num),
          Offset = position
        });
      }
      else
      {
        StringOffsetStruct stringOffsetStruct = new StringOffsetStruct()
        {
          Name = Encoding.UTF8.GetBytes(dataRow[0].ToString())
        };
        stringOffsetStruct.Len = stringOffsetStruct.Name.Length;
        stringOffsetStruct.Size = (int) binaryWriter.BaseStream.Position - num;
        stringOffsetStruct.Offset = position;
        list.Add((object) stringOffsetStruct);
      }
      return (int) binaryWriter.BaseStream.Position;
    }

    private static void WriteTableData(BinaryWriter bw, DataTable dt)
    {
      BnyUtils.SetServerI(bw, dt.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
      {
        foreach (DataColumn column in (InternalDataCollectionBase) dt.Columns)
        {
          if (column.DataType == typeof (int))
            BnyUtils.SetServerI(bw, int.Parse(row[column.ColumnName].ToString()));
          else if (column.DataType == typeof (string))
          {
            byte[] bytes = Encoding.UTF8.GetBytes(row[column.ColumnName].ToString());
            BnyUtils.SetServerI(bw, bytes.Length);
            bw.Write(bytes);
          }
          else if (column.DataType == typeof (byte))
            bw.Write(byte.Parse(row[column.ColumnName].ToString()));
          else if (column.DataType == typeof (DataTable))
            BnyUtils.WriteTableData(bw, row[column.ColumnName] as DataTable);
          else if (column.DataType == typeof (List<DataTable>))
          {
            foreach (DataTable dt1 in row[column.ColumnName] as List<DataTable>)
              BnyUtils.WriteTableData(bw, dt1);
          }
        }
      }
    }

    private static int FindColunmName(DataTable dt, string name)
    {
      int colunmName = 0;
      foreach (DataColumn column in (InternalDataCollectionBase) dt.Columns)
      {
        if (column.ColumnName == name)
          return colunmName;
        ++colunmName;
      }
      return -1;
    }

    public static void WriteNewFile(string fileName, BnyStruct bny)
    {
      List<object> objectList = new List<object>();
      string str = fileName;
      if (BnyCommon.IsHotFile)
        str += ".tmp";
      using (FileStream output = new FileStream(str, FileMode.OpenOrCreate, FileAccess.Write))
      {
        using (BinaryWriter bw = new BinaryWriter((Stream) output, Encoding.UTF8))
        {
          bw.BaseStream.Position = 0L;
          int num = 0;
          foreach (DataRow row in (InternalDataCollectionBase) BnyCommon.MainTable.Rows)
          {
            int position = (int) bw.BaseStream.Position;
            foreach (StructExplain explain in bny.Explains)
            {
              if (BnyUtils.FindColunmName(BnyCommon.MainTable, explain.Name) != -1)
              {
                switch (explain.Type)
                {
                  case 0:
                  case 6:
                    BnyUtils.SetServerI(bw, int.Parse(row[explain.Name].ToString()));
                    continue;
                  case 4:
                  case 13:
                    bw.Write(byte.Parse(row[explain.Name].ToString()));
                    continue;
                  case 9:
                    byte[] bytes = Encoding.UTF8.GetBytes(row[explain.Name].ToString());
                    BnyUtils.SetServerI(bw, bytes.Length);
                    bw.Write(bytes);
                    continue;
                  case 11:
                    int index = 0;
                    using (List<DataTable>.Enumerator enumerator = (row[explain.Name] as List<DataTable>).GetEnumerator())
                    {
                      while (enumerator.MoveNext())
                      {
                        DataTable current = enumerator.Current;
                        BnyUtils.WriteNewTableData(bw, current, explain.Structs[index]);
                        ++index;
                      }
                      continue;
                    }
                  case 12:
                    BnyUtils.WriteNewTableData(bw, row[explain.Name] as DataTable, explain);
                    continue;
                  default:
                    continue;
                }
              }
              else
              {
                switch (explain.Type)
                {
                  case 0:
                  case 6:
                    BnyUtils.SetServerI(bw, 0);
                    continue;
                  case 4:
                  case 13:
                    bw.Write(byte.Parse(row[explain.Name].ToString()));
                    continue;
                  case 9:
                    BnyUtils.SetServerI(bw, 0);
                    continue;
                  case 11:
                    using (List<StructExplain>.Enumerator enumerator = explain.Structs.GetEnumerator())
                    {
                      while (enumerator.MoveNext())
                      {
                        StructExplain current = enumerator.Current;
                        BnyUtils.SetServerI(bw, 0);
                      }
                      continue;
                    }
                  case 12:
                    BnyUtils.SetServerI(bw, 0);
                    continue;
                  default:
                    continue;
                }
              }
            }
            if (BnyCommon.BnyData.Type == (byte) 1)
            {
              objectList.Add((object) new OffsetStruct()
              {
                Id = int.Parse(row[0].ToString()),
                Size = ((int) bw.BaseStream.Position - num),
                Offset = position
              });
            }
            else
            {
              StringOffsetStruct stringOffsetStruct = new StringOffsetStruct()
              {
                Name = Encoding.UTF8.GetBytes(row[0].ToString())
              };
              stringOffsetStruct.Len = stringOffsetStruct.Name.Length;
              stringOffsetStruct.Size = (int) bw.BaseStream.Position - num;
              stringOffsetStruct.Offset = position;
              objectList.Add((object) stringOffsetStruct);
            }
            num = (int) bw.BaseStream.Position;
          }
          bny.StructExPlainStart = (int) bw.BaseStream.Position;
          bw.Write(bny.StructExplainBackUpData);
          bny.OffsetStart = (int) bw.BaseStream.Position;
          bw.Write(bny.Type);
          BnyUtils.SetServerI(bw, objectList.Count);
          foreach (object obj in objectList)
          {
            if (bny.Type == (byte) 1)
            {
              OffsetStruct offsetStruct = obj as OffsetStruct;
              BnyUtils.SetServerI(bw, offsetStruct.Id);
              BnyUtils.SetServerI(bw, offsetStruct.Offset);
              BnyUtils.SetServerI(bw, offsetStruct.Size);
            }
            else
            {
              StringOffsetStruct stringOffsetStruct = obj as StringOffsetStruct;
              BnyUtils.SetServerI(bw, stringOffsetStruct.Len);
              bw.Write(stringOffsetStruct.Name);
              BnyUtils.SetServerI(bw, stringOffsetStruct.Offset);
              BnyUtils.SetServerI(bw, stringOffsetStruct.Size);
            }
          }
          bny.OffsetSize = (int) bw.BaseStream.Position - bny.OffsetStart;
          bw.Write(BnyCommon.MainTable.Rows.Count);
          bw.Write(bny.OffsetStart);
          bw.Write(bny.OffsetSize);
          bw.Write(bny.StructExPlainStart);
          bw.Write(bny.StructExPlainSize);
        }
      }
      if (!BnyCommon.IsHotFile)
        return;
      HotUpdate.ToHotBnyFile(str, fileName);
      File.Delete(str);
    }

    private static void WriteNewTableData(BinaryWriter bw, DataTable dt, StructExplain se)
    {
      BnyUtils.SetServerI(bw, dt.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
      {
        foreach (StructExplain structExplain in se.Structs)
        {
          if (BnyUtils.FindColunmName(dt, structExplain.Name) != -1)
          {
            switch (structExplain.Type)
            {
              case 0:
              case 6:
                BnyUtils.SetServerI(bw, int.Parse(row[structExplain.Name].ToString()));
                continue;
              case 4:
              case 13:
                bw.Write(byte.Parse(row[structExplain.Name].ToString()));
                continue;
              case 9:
                byte[] bytes = Encoding.UTF8.GetBytes(row[structExplain.Name].ToString());
                BnyUtils.SetServerI(bw, bytes.Length);
                bw.Write(bytes);
                continue;
              case 11:
                int index = 0;
                using (List<DataTable>.Enumerator enumerator = (row[structExplain.Name] as List<DataTable>).GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    DataTable current = enumerator.Current;
                    BnyUtils.WriteNewTableData(bw, current, se.Structs[index]);
                    ++index;
                  }
                  continue;
                }
              case 12:
                BnyUtils.WriteNewTableData(bw, row[structExplain.Name] as DataTable, se);
                continue;
              default:
                continue;
            }
          }
          else
          {
            switch (structExplain.Type)
            {
              case 0:
              case 6:
                BnyUtils.SetServerI(bw, 0);
                continue;
              case 4:
              case 13:
                bw.Write(0);
                continue;
              case 9:
                BnyUtils.SetServerI(bw, 0);
                continue;
              case 11:
                using (List<StructExplain>.Enumerator enumerator = se.Structs.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    StructExplain current = enumerator.Current;
                    BnyUtils.SetServerI(bw, 0);
                  }
                  continue;
                }
              case 12:
                BnyUtils.SetServerI(bw, 0);
                continue;
              default:
                continue;
            }
          }
        }
      }
    }

    public static void ReadStructExplain(BinaryReader br, StructExplain se)
    {
      se.Type = br.ReadByte();
      se.NameLen = BnyUtils.GetServerI(br);
      se.Name = Encoding.UTF8.GetString(br.ReadBytes(se.NameLen));
      se.IsEnd = BnyUtils.GetServerI(br);
      if (se.IsEnd == 1)
      {
        se.Structs = new List<StructExplain>();
        StructExplain se1 = new StructExplain();
        BnyUtils.ReadStructExplain(br, se1);
        se.Structs.Add(se1);
      }
      else
      {
        if (se.IsEnd <= 1)
          return;
        se.Structs = new List<StructExplain>();
        for (int index = 0; index < se.IsEnd; ++index)
        {
          StructExplain se2 = new StructExplain();
          BnyUtils.ReadStructExplain(br, se2);
          se.Structs.Add(se2);
        }
      }
    }

    public static bool ReadFileIsHot(string fileName)
    {
      using (FileStream input = new FileStream(fileName, FileMode.Open, FileAccess.Read))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input, Encoding.UTF8))
          return binaryReader.ReadInt32() == 5943128;
      }
    }

    public static void ReadFile(string FileName)
    {
      BnyCommon.IsHotFile = BnyUtils.ReadFileIsHot(FileName);
      if (BnyCommon.IsHotFile)
        BnyUtils.ReadHotFile(FileName);
      else
        BnyUtils.ReadUnHotFile(FileName);
    }

    private static void ReadData(BnyStruct bny, BinaryReader br)
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
      BnyUtils.GenerateTable(bny);
      br.BaseStream.Position = 0L;
      for (int index = 0; index < bny.StructCount; ++index)
      {
        try
        {
          if (br.BaseStream.Position > (long) bny.StructExPlainStart)
            break;
          DataRow row = BnyCommon.MainTable.NewRow();
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
                      (row[columnIndex] as List<DataTable>).Add(BnyUtils.ReadTableData(current, br));
                    }
                    break;
                  }
                }
                else
                  break;
              case 12:
                row[columnIndex] = (object) BnyUtils.ReadTableData(bny.Explains[columnIndex], br);
                break;
              default:
                Console.WriteLine("未知类型:" + (object) bny.Explains[columnIndex].Type);
                break;
            }
          }
          BnyCommon.MainTable.Rows.Add(row);
        }
        catch (Exception ex)
        {
          Console.WriteLine("错误位置:" + (object) br.BaseStream.Position);
        }
      }
    }

    private static void ReadHotFile(string FileName)
    {
      BnyCommon.BnyData = (BnyStruct) null;
      BnyCommon.MainTable = (DataTable) null;
      BnyStruct bny = new BnyStruct();
      using (MemoryStream input = new MemoryStream(HotUpdate.DecompressBny(FileName)))
      {
        using (BinaryReader br = new BinaryReader((Stream) input, Encoding.UTF8))
          BnyUtils.ReadData(bny, br);
      }
      BnyCommon.BnyData = bny;
    }

    private static void ReadUnHotFile(string FileName)
    {
      BnyCommon.BnyData = (BnyStruct) null;
      BnyCommon.MainTable = (DataTable) null;
      BnyStruct bny = new BnyStruct();
      using (FileStream input = new FileStream(FileName, FileMode.Open, FileAccess.Read))
      {
        using (BinaryReader br = new BinaryReader((Stream) input, Encoding.UTF8))
          BnyUtils.ReadData(bny, br);
      }
      BnyCommon.BnyData = bny;
    }
  }
}
