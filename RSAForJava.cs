// Decompiled with JetBrains decompiler
// Type: BnyEditor.RSAForJava
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Security;
using System;
using System.Text;

#nullable disable
namespace BnyEditor
{
  public class RSAForJava
  {
    private AsymmetricKeyParameter GetPublicKeyParameter(string s)
    {
      s = s.Replace("\r", "").Replace("\n", "").Replace(" ", "");
      byte[] hexByte = EncryptHelper.StrToHexByte(s);
      Asn1Object.FromByteArray(hexByte);
      return PublicKeyFactory.CreateKey(hexByte);
    }

    public string DecryptByPublicKey(string s, string key)
    {
      s = s.Replace("\r", "").Replace("\n", "").Replace(" ", "");
      IAsymmetricBlockCipher iasymmetricBlockCipher = (IAsymmetricBlockCipher) new Pkcs1Encoding((IAsymmetricBlockCipher) new RsaEngine());
      string empty = string.Empty;
      string str;
      try
      {
        iasymmetricBlockCipher.Init(false, (ICipherParameters) this.GetPublicKeyParameter(key));
        byte[] hexByte = EncryptHelper.StrToHexByte(s);
        int inputBlockSize = iasymmetricBlockCipher.GetInputBlockSize();
        if (hexByte.Length > inputBlockSize)
        {
          byte[] destinationArray = new byte[inputBlockSize];
          int num = hexByte.Length / inputBlockSize;
          bool flag = false;
          if (hexByte.Length % inputBlockSize > 0)
          {
            ++num;
            flag = true;
          }
          for (int index = 0; index < num; ++index)
          {
            Array.Clear((Array) destinationArray, 0, destinationArray.Length);
            if (index == num - 1 & flag)
            {
              int length = hexByte.Length % inputBlockSize;
              Array.Copy((Array) hexByte, index * inputBlockSize, (Array) destinationArray, 0, length);
            }
            else
              Array.Copy((Array) hexByte, index * inputBlockSize, (Array) destinationArray, 0, inputBlockSize);
            byte[] bytes = iasymmetricBlockCipher.ProcessBlock(destinationArray, 0, destinationArray.Length);
            empty += Encoding.UTF8.GetString(bytes);
          }
          str = empty;
        }
        else
          str = Encoding.UTF8.GetString(iasymmetricBlockCipher.ProcessBlock(hexByte, 0, hexByte.Length));
      }
      catch (Exception ex)
      {
        str = ex.Message;
      }
      return str;
    }

    public struct RSAKEY
    {
      public string PublicKey { get; set; }
    }
  }
}
