// Decompiled with JetBrains decompiler
// Type: NetWork.XReader
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

#nullable disable
namespace NetWork
{
  public class XReader
  {
    public int ReadInt(BinaryReader br)
    {
      byte[] numArray1 = br.ReadBytes(4);
      byte[] numArray2 = new byte[4]
      {
        numArray1[3],
        numArray1[2],
        numArray1[1],
        numArray1[0]
      };
      int int32 = BitConverter.ToInt32(numArray2, 0);
      Array.Clear((Array) numArray2, 0, 4);
      Array.Clear((Array) numArray1, 0, 4);
      return int32;
    }

    public void WriteInt(BinaryWriter bw, int val)
    {
      byte[] bytes = BitConverter.GetBytes(val);
      byte[] buffer = new byte[4]
      {
        bytes[3],
        bytes[2],
        bytes[1],
        bytes[0]
      };
      bw.Write(buffer);
      Array.Clear((Array) buffer, 0, 4);
      Array.Clear((Array) bytes, 0, 4);
    }

    public void TableAddColum(DataTable dt, int type, string name)
    {
      switch (type)
      {
        case 0:
        case 6:
          dt.Columns.Add(new DataColumn(name, typeof (int)));
          break;
        case 4:
        case 13:
          dt.Columns.Add(new DataColumn(name, typeof (byte)));
          break;
        case 9:
          dt.Columns.Add(new DataColumn(name, typeof (string)));
          break;
        case 11:
          dt.Columns.Add(new DataColumn(name, typeof (List<DataTable>)));
          break;
        case 12:
          dt.Columns.Add(new DataColumn(name, typeof (DataTable)));
          break;
      }
    }
  }
}
