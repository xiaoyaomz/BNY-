// Decompiled with JetBrains decompiler
// Type: BnyEditor.EncryptHelper
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System;
using System.Security.Cryptography;
using System.Text;

#nullable disable
namespace BnyEditor
{
  public class EncryptHelper
  {
    private static string AESKey = "[68/*ADCd3x..hl]";
    private static string DESKey = "[&Iox85]";

    public static string ByteToHexStr(byte[] bytes)
    {
      string hexStr = "";
      if (bytes != null)
      {
        for (int index = 0; index < bytes.Length; ++index)
          hexStr += bytes[index].ToString("X2");
      }
      return hexStr;
    }

    public static byte[] StrToHexByte(string hexString)
    {
      hexString = hexString.Replace(" ", "");
      if (hexString.Length % 2 != 0)
        hexString += " ";
      byte[] hexByte = new byte[hexString.Length / 2];
      for (int index = 0; index < hexByte.Length; ++index)
        hexByte[index] = Convert.ToByte(hexString.Substring(index * 2, 2), 16);
      return hexByte;
    }

    public static byte[] AESEncryptData(string value, string _aeskey = null)
    {
      if (string.IsNullOrEmpty(_aeskey))
        _aeskey = EncryptHelper.AESKey;
      byte[] bytes1 = Encoding.UTF8.GetBytes(_aeskey);
      byte[] bytes2 = Encoding.UTF8.GetBytes(value);
      RijndaelManaged rijndaelManaged = new RijndaelManaged();
      rijndaelManaged.Key = bytes1;
      rijndaelManaged.Mode = CipherMode.ECB;
      rijndaelManaged.Padding = PaddingMode.PKCS7;
      return rijndaelManaged.CreateEncryptor().TransformFinalBlock(bytes2, 0, bytes2.Length);
    }

    public static string AESDecryptData(byte[] value, string _aeskey = null)
    {
      try
      {
        if (string.IsNullOrEmpty(_aeskey))
          _aeskey = EncryptHelper.AESKey;
        byte[] bytes = Encoding.UTF8.GetBytes(_aeskey);
        RijndaelManaged rijndaelManaged = new RijndaelManaged();
        rijndaelManaged.Key = bytes;
        rijndaelManaged.Mode = CipherMode.ECB;
        rijndaelManaged.Padding = PaddingMode.PKCS7;
        return Encoding.UTF8.GetString(rijndaelManaged.CreateDecryptor().TransformFinalBlock(value, 0, value.Length));
      }
      catch
      {
        return string.Empty;
      }
    }

    public static string AESEncrypt(string value, string _aeskey = null)
    {
      if (string.IsNullOrEmpty(_aeskey))
        _aeskey = EncryptHelper.AESKey;
      byte[] bytes1 = Encoding.UTF8.GetBytes(_aeskey);
      byte[] bytes2 = Encoding.UTF8.GetBytes(value);
      RijndaelManaged rijndaelManaged = new RijndaelManaged();
      rijndaelManaged.Key = bytes1;
      rijndaelManaged.Mode = CipherMode.ECB;
      rijndaelManaged.Padding = PaddingMode.PKCS7;
      byte[] inArray = rijndaelManaged.CreateEncryptor().TransformFinalBlock(bytes2, 0, bytes2.Length);
      return Convert.ToBase64String(inArray, 0, inArray.Length);
    }

    public static string AESDecrypt(string value, string _aeskey = null)
    {
      try
      {
        if (string.IsNullOrEmpty(_aeskey))
          _aeskey = EncryptHelper.AESKey;
        value = value.Trim().TrimEnd(new char[1]);
        byte[] bytes = Encoding.UTF8.GetBytes(_aeskey);
        byte[] inputBuffer = Convert.FromBase64String(value);
        RijndaelManaged rijndaelManaged = new RijndaelManaged();
        rijndaelManaged.Key = bytes;
        rijndaelManaged.Mode = CipherMode.ECB;
        rijndaelManaged.Padding = PaddingMode.PKCS7;
        return Encoding.UTF8.GetString(rijndaelManaged.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
      }
      catch
      {
        return string.Empty;
      }
    }

    public static string DESEncrypt(string value, string _deskey = null)
    {
      if (string.IsNullOrEmpty(_deskey))
        _deskey = EncryptHelper.DESKey;
      byte[] bytes1 = Encoding.UTF8.GetBytes(_deskey);
      byte[] bytes2 = Encoding.UTF8.GetBytes(value);
      DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
      cryptoServiceProvider.Key = bytes1;
      cryptoServiceProvider.Mode = CipherMode.ECB;
      cryptoServiceProvider.Padding = PaddingMode.PKCS7;
      byte[] inArray = cryptoServiceProvider.CreateEncryptor().TransformFinalBlock(bytes2, 0, bytes2.Length);
      return Convert.ToBase64String(inArray, 0, inArray.Length);
    }

    public static string DESDecrypt(string value, string _deskey = null)
    {
      try
      {
        if (string.IsNullOrEmpty(_deskey))
          _deskey = EncryptHelper.DESKey;
        byte[] bytes = Encoding.UTF8.GetBytes(_deskey);
        byte[] inputBuffer = Convert.FromBase64String(value);
        DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
        cryptoServiceProvider.Key = bytes;
        cryptoServiceProvider.Mode = CipherMode.ECB;
        cryptoServiceProvider.Padding = PaddingMode.PKCS7;
        return Encoding.UTF8.GetString(cryptoServiceProvider.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
      }
      catch
      {
        return string.Empty;
      }
    }

    public static string MD5(string value)
    {
      return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(value))).Replace("-", "");
    }

    public static string HMACMD5(string value, string hmacKey)
    {
      return BitConverter.ToString(new HMACSHA1(Encoding.UTF8.GetBytes(hmacKey)).ComputeHash(Encoding.UTF8.GetBytes(value))).Replace("-", "");
    }

    public static string Base64Encode(string value)
    {
      return Convert.ToBase64String(Encoding.Default.GetBytes(value));
    }

    public static string Base64Decode(string value)
    {
      return Encoding.Default.GetString(Convert.FromBase64String(value));
    }
  }
}
