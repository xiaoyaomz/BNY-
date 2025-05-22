// Decompiled with JetBrains decompiler
// Type: BnyEditor.HotUpdate
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using zlib;

#nullable disable
namespace BnyEditor
{
  public class HotUpdate
  {
    public static byte[] DeflateDecompress(byte[] Buffer)
    {
      int num1 = -1;
      ZInputStream zinputStream = new ZInputStream((Stream) new MemoryStream(Buffer));
      byte[] sourceArray = new byte[100000000];
      int length = 0;
      int num2;
      while (num1 != (num2 = zinputStream.Read()))
      {
        sourceArray[length] = (byte) num2;
        ++length;
      }
      byte[] destinationArray = new byte[length];
      Array.Copy((Array) sourceArray, (Array) destinationArray, length);
      zinputStream.Close();
      return destinationArray;
    }

    private static byte[] DeflateCompress(byte[] bytes)
    {
      MemoryStream out_Renamed = new MemoryStream();
      MemoryStream input = new MemoryStream(bytes);
      ZOutputStream output = new ZOutputStream((Stream) out_Renamed, -1);
      try
      {
        HotUpdate.CopyStream((Stream) input, (Stream) output);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        output.Close();
      }
      return out_Renamed.ToArray();
    }

    private static void CopyStream(Stream input, Stream output)
    {
      byte[] buffer = new byte[2000];
      int count;
      while ((count = input.Read(buffer, 0, 2000)) > 0)
        output.Write(buffer, 0, count);
      output.Flush();
    }

    public static byte[] DecompressBny(string filename)
    {
      int num = 5943128;
      using (FileStream input = new FileStream(filename, FileMode.Open, FileAccess.Read))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input, Encoding.UTF8))
        {
          if (binaryReader.ReadInt32() != num)
            return (byte[]) null;
          int length1 = binaryReader.ReadInt32();
          byte[] sourceArray = HotUpdate.DeflateDecompress(binaryReader.ReadBytes((int) binaryReader.BaseStream.Length));
          byte[] numArray = new byte[length1];
          byte[] destinationArray = numArray;
          int length2 = length1;
          Array.Copy((Array) sourceArray, (Array) destinationArray, length2);
          return numArray;
        }
      }
    }

    public static void ToSourceBnyFile(string fileName, string destFile)
    {
      byte[] buffer = HotUpdate.DecompressBny(fileName);
      if (buffer == null)
        return;
      using (FileStream output = new FileStream(destFile, FileMode.Create, FileAccess.Write))
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output, Encoding.UTF8))
        {
          binaryWriter.Write(buffer);
          binaryWriter.Flush();
          binaryWriter.Close();
          output.Close();
        }
      }
    }

    public static void ToHotBnyFile(string fileName, string destFile)
    {
      int num = 5943128;
      using (FileStream input = new FileStream(fileName, FileMode.Open, FileAccess.Read))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input, Encoding.UTF8))
        {
          byte[] bytes = binaryReader.ReadBytes((int) binaryReader.BaseStream.Length);
          int length = (int) binaryReader.BaseStream.Length;
          using (FileStream output = new FileStream(destFile, FileMode.Create, FileAccess.Write))
          {
            using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output, Encoding.UTF8))
            {
              binaryWriter.Write(num);
              binaryWriter.Write(length);
              byte[] buffer = HotUpdate.DeflateCompress(bytes);
              buffer[1] = (byte) 1;
              binaryWriter.Write(buffer);
              binaryWriter.Flush();
              binaryWriter.Close();
              output.Close();
            }
          }
        }
      }
    }

    public static string GetMD5HashFromFile(string fileName)
    {
      using (FileStream inputStream = new FileStream(fileName, FileMode.Open))
      {
        byte[] hash = new MD5CryptoServiceProvider().ComputeHash((Stream) inputStream);
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < hash.Length; ++index)
          stringBuilder.Append(hash[index].ToString("x2"));
        inputStream.Close();
        return stringBuilder.ToString().ToUpper();
      }
    }

    public static int GetFileSize(string fileName)
    {
      using (FileStream input = new FileStream(fileName, FileMode.Open, FileAccess.Read))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input, Encoding.UTF8))
          return (int) binaryReader.BaseStream.Length;
      }
    }
  }
}
