// Decompiled with JetBrains decompiler
// Type: BnyEditor.ComputerInfo
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

#nullable disable
namespace BnyEditor
{
  public class ComputerInfo
  {
    public static void GetComputerInfo()
    {
      CheckSerialNumber.machineCode = "95E2FD34F01D3506C774B9C8B9D60802";
    }

    private static string GetCPUInfo()
    {
      string empty = string.Empty;
      return ComputerInfo.GetHardWareInfo("Win32_Processor", "ProcessorId");
    }

    private static string GetBIOSInfo()
    {
      string empty = string.Empty;
      return ComputerInfo.GetHardWareInfo("Win32_BIOS", "SerialNumber");
    }

    private static string GetBaseBoardInfo()
    {
      string empty = string.Empty;
      return ComputerInfo.GetHardWareInfo("Win32_BaseBoard", "SerialNumber");
    }

    private static string GetMACInfo()
    {
      string empty = string.Empty;
      return ComputerInfo.GetMacAddressByNetworkInformation();
    }

    private static string GetHardWareInfo(string typePath, string key)
    {
      try
      {
        ManagementClass managementClass = new ManagementClass(typePath);
        ManagementObjectCollection instances = managementClass.GetInstances();
        foreach (PropertyData property in managementClass.Properties)
        {
          if (property.Name == key)
          {
            using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
            {
              if (enumerator.MoveNext())
                return enumerator.Current.Properties[property.Name].Value.ToString();
            }
          }
        }
      }
      catch (Exception ex)
      {
      }
      return string.Empty;
    }

    private static string GetMacAddressByNetworkInformation()
    {
      string str1 = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\";
      string networkInformation = string.Empty;
      try
      {
        foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
        {
          if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet && networkInterface.GetPhysicalAddress().ToString().Length != 0)
          {
            string name = str1 + networkInterface.Id + "\\Connection";
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name, false);
            if (registryKey != null)
            {
              string str2 = registryKey.GetValue("PnpInstanceID", (object) "").ToString();
              Convert.ToInt32(registryKey.GetValue("MediaSubType", (object) 0));
              if (str2.Length > 3 && str2.Substring(0, 3) == "PCI")
              {
                networkInformation = networkInterface.GetPhysicalAddress().ToString();
                for (int index = 1; index < 6; ++index)
                  networkInformation = networkInformation.Insert(3 * index - 1, ":");
                break;
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
      }
      return networkInformation;
    }

    private static string GetSystemDiskNo()
    {
      ManagementObjectCollection instances1 = new ManagementClass("Win32_PhysicalMedia").GetInstances();
      Dictionary<string, string> source1 = new Dictionary<string, string>();
      foreach (ManagementObject managementObject in instances1)
      {
        string key = managementObject.Properties["Tag"].Value.ToString().ToLower().Trim();
        string str = ((string) managementObject.Properties["SerialNumber"].Value ?? string.Empty).Trim();
        source1.Add(key, str);
      }
      ManagementObjectCollection instances2 = new ManagementClass("Win32_OperatingSystem").GetInstances();
      string currentSysRunDisk = string.Empty;
      foreach (ManagementObject managementObject in instances2)
        currentSysRunDisk = Regex.Match(managementObject.Properties["Name"].Value.ToString().ToLower(), "harddisk\\d+").Value;
      IEnumerable<KeyValuePair<string, string>> source2 = source1.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (x => Regex.IsMatch(x.Key, "physicaldrive" + Regex.Match(currentSysRunDisk, "\\d+$").Value)));
      return source2.Any<KeyValuePair<string, string>>() ? source2.ElementAt<KeyValuePair<string, string>>(0).Value : "";
    }
  }
}
