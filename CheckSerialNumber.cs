// Decompiled with JetBrains decompiler
// Type: BnyEditor.CheckSerialNumber
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System;

#nullable disable
namespace BnyEditor
{
  public class CheckSerialNumber
  {
    public static string machineCode;

    static CheckSerialNumber() => ComputerInfo.GetComputerInfo();

    private static string getAppName() => "Bny编辑器";

    private static string BaseDir() => AppDomain.CurrentDomain.BaseDirectory;

    public static bool CheckCode()
    {
      if (BnyCommon.Bnywork == null)
      {
        BnyNetwork bnyNetwork = new BnyNetwork();
        bnyNetwork.CreateInstacne("");
        BnyCommon.Bnywork = bnyNetwork;
      }
      return true;
    }
  }
}
