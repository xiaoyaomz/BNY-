// Decompiled with JetBrains decompiler
// Type: BnyEditor.BnyNetwork
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using NetWork;
using System.Data;
using System.IO;
using System.Reflection;

#nullable disable
namespace BnyEditor
{
  public class BnyNetwork
  {
    private object xWork;

    public Assembly BnyAssembly { get; set; }

    public bool CreateInstacne(string codeStr)
    {
      this.xWork = (object) new XReader();
      return true;
    }

    public int ServerGetInt(BinaryReader br)
    {
      return (int) this.xWork.GetType().GetMethod("ReadInt").Invoke(this.xWork, new object[1]
      {
        (object) br
      });
    }

    public void SeverSetInt(BinaryWriter bw, int val)
    {
      this.xWork.GetType().GetMethod("WriteInt").Invoke(this.xWork, new object[2]
      {
        (object) bw,
        (object) val
      });
    }

    public void TableAddColum(DataTable dt, int type, string name)
    {
      this.xWork.GetType().GetMethod(nameof (TableAddColum)).Invoke(this.xWork, new object[3]
      {
        (object) dt,
        (object) type,
        (object) name
      });
    }
  }
}
