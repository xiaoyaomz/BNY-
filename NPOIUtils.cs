// Decompiled with JetBrains decompiler
// Type: BnyEditor.NPOIUtils
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

#nullable disable
namespace BnyEditor
{
  public class NPOIUtils
  {
    public static bool ExportExcel(string fileName)
    {
      try
      {
        IWorkbook iworkbook = (IWorkbook) new HSSFWorkbook();
        ISheet sheet = iworkbook.CreateSheet(BnyCommon.CurrFileName);
        IRow row1 = sheet.CreateRow(0);
        IDataFormat dataFormat = iworkbook.CreateDataFormat();
        IFont font1 = iworkbook.CreateFont();
        font1.FontName = "微软雅黑";
                font1.FontHeightInPoints = (short)16; // 显式转换
                font1.Color = (short) 8;
        font1.Boldweight = (short) 700;
        ICellStyle cellStyle1 = iworkbook.CreateCellStyle();
        cellStyle1.FillForegroundColor = (short) 22;
        cellStyle1.DataFormat = dataFormat.GetFormat("@");
        cellStyle1.FillPattern = (FillPattern) 1;
        cellStyle1.Alignment = (HorizontalAlignment) 2;
        cellStyle1.SetFont(font1);
        sheet.DefaultColumnWidth = 30;
        sheet.DefaultRowHeight = (short) 500;
        IFont font2 = iworkbook.CreateFont();
        font2.FontName = "微软雅黑";
                font2.FontHeightInPoints = (short)12; // 显式转换
                ICellStyle cellStyle2 = iworkbook.CreateCellStyle();
        cellStyle2.SetFont(font2);
        cellStyle2.DataFormat = dataFormat.GetFormat("@");
        int index1 = 0;
        foreach (DataColumn column in (InternalDataCollectionBase) BnyCommon.MainTable.Columns)
        {
          if (column.DataType != typeof (List<DataTable>) && column.DataType != typeof (DataTable))
          {
            row1.CreateCell(index1).SetCellValue(column.Caption);
            row1.Cells[index1].CellStyle = cellStyle1;
            ++index1;
          }
        }
        for (int index2 = 0; index2 < BnyCommon.MainTable.Rows.Count; ++index2)
        {
          IRow row2 = sheet.CreateRow(index2 + 1);
          for (int index3 = 0; index3 < BnyCommon.MainTable.Columns.Count; ++index3)
          {
            if (BnyCommon.MainTable.Columns[index3].DataType != typeof (List<DataTable>) && BnyCommon.MainTable.Columns[index3].DataType != typeof (DataTable))
            {
              ICell cell = row2.CreateCell(index3);
              cell.SetCellValue(BnyCommon.MainTable.Rows[index2][index3].ToString());
              cell.CellStyle = cellStyle2;
            }
          }
        }
        using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
        {
          using (MemoryStream memoryStream = new MemoryStream())
          {
            iworkbook.Write((Stream) memoryStream);
            byte[] array = memoryStream.ToArray();
            fileStream.Write(array, 0, array.Length);
            fileStream.Flush();
          }
        }

        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    private static bool CheckCellValue(ICell cell, Type t)
    {
      if (cell == null || t == (Type) null)
        return false;
      if (t == typeof (int))
        return int.TryParse(cell.ToString(), out int _);
      return !(t == typeof (byte)) || byte.TryParse(cell.ToString(), out byte _);
    }

    public static DataTable ImportExcel(string fileName)
    {
      using (FileStream fileStream = File.OpenRead(fileName))
      {
        string lower = fileName.Substring(fileName.LastIndexOf(".")).ToString().ToLower();
        if (!(lower == ".xlsx") && !(lower == ".xls"))
          return (DataTable) null;
        DataTable dataTable = new DataTable();
        ISheet sheetAt = (!(lower == ".xlsx") ? (IWorkbook) new HSSFWorkbook((Stream) fileStream) : (IWorkbook) new XSSFWorkbook((Stream) fileStream)).GetSheetAt(0);
        if (sheetAt.SheetName != BnyCommon.CurrFileName)
        {
          NPOIException npoiException = new NPOIException();
          npoiException.Source = "您要导入的文件，与打开的BNY文件不匹配！";
          throw npoiException;
        }
        IRow row1 = sheetAt.GetRow(0);
        for (int firstCellNum = (int) row1.FirstCellNum; firstCellNum < row1.Cells.Count; ++firstCellNum)
        {
          try
          {
            if (BnyCommon.MainTable.Columns[row1.GetCell(firstCellNum).ToString()] == null)
            {
              NPOIException npoiException = new NPOIException();
              npoiException.Source = "您要导入的文件，与打开的BNY文件不匹配！";
              throw npoiException;
            }
          }
          catch (Exception ex)
          {
            NPOIException npoiException = new NPOIException();
            npoiException.Source = "您要导入的文件，与打开的BNY文件不匹配！";
            throw npoiException;
          }
          DataColumn column = new DataColumn(row1.GetCell(firstCellNum).ToString());
          dataTable.Columns.Add(column);
        }
        for (int index1 = 1; index1 <= sheetAt.LastRowNum; ++index1)
        {
          DataRow row2 = dataTable.NewRow();
          IRow row3 = sheetAt.GetRow(index1);
          for (int index2 = 0; index2 < row3.Cells.Count; ++index2)
          {
            ICell cell = row3.GetCell(index2);
            if (!NPOIUtils.CheckCellValue(cell, BnyCommon.MainTable.Columns[row1.GetCell(index2).ToString()].DataType))
            {
              NPOIException npoiException = new NPOIException();
              npoiException.Source = "Excel内容错误\r\n输入的格式不正确\r\n在列【" + dataTable.Columns[index2].ColumnName + "】 第" + (object) (index1 + 1) + "行";
              throw npoiException;
            }
            row2[index2] = (object) cell.ToString();
          }
          dataTable.Rows.Add(row2);
        }
        return dataTable;
      }
    }
  }
}
