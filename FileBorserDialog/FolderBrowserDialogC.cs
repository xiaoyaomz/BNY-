// Decompiled with JetBrains decompiler
// Type: FileBorserDialog.FolderBrowserDialogC
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace FileBorserDialog
{
  [Description("提供一个Vista样式的选择文件对话框")]
  [Editor(typeof (FolderNameEditor), typeof (UITypeEditor))]
  public class FolderBrowserDialogC : Component
  {
    private const uint ERROR_CANCELLED = 2147943623;

    public string DirectoryPath { get; set; }

    public DialogResult ShowDialog(IWin32Window owner)
    {
      IntPtr parent = owner != null ? owner.Handle : FolderBrowserDialogC.GetActiveWindow();
      FolderBrowserDialogC.IFileOpenDialog o = (FolderBrowserDialogC.IFileOpenDialog) new FolderBrowserDialogC.FileOpenDialog();
      try
      {
        FolderBrowserDialogC.IShellItem ppsi;
        if (!string.IsNullOrEmpty(this.DirectoryPath))
        {
          uint rgflnOut = 0;
          IntPtr ppIdl;
          if (FolderBrowserDialogC.SHILCreateFromPath(this.DirectoryPath, out ppIdl, ref rgflnOut) == 0 && FolderBrowserDialogC.SHCreateShellItem(IntPtr.Zero, IntPtr.Zero, ppIdl, out ppsi) == 0)
            o.SetFolder(ppsi);
        }
        o.SetOptions(FolderBrowserDialogC.FOS.FOS_FORCEFILESYSTEM | FolderBrowserDialogC.FOS.FOS_PICKFOLDERS);
        switch (o.Show(parent))
        {
          case 0:
            o.GetResult(out ppsi);
            string ppszName;
            ppsi.GetDisplayName(FolderBrowserDialogC.SIGDN.SIGDN_FILESYSPATH, out ppszName);
            this.DirectoryPath = ppszName;
            return DialogResult.OK;
          case 2147943623:
            return DialogResult.Cancel;
          default:
            return DialogResult.Abort;
        }
      }
      finally
      {
        Marshal.ReleaseComObject((object) o);
      }
    }

    [DllImport("shell32.dll")]
    private static extern int SHILCreateFromPath(
      [MarshalAs(UnmanagedType.LPWStr)] string pszPath,
      out IntPtr ppIdl,
      ref uint rgflnOut);

    [DllImport("shell32.dll")]
    private static extern int SHCreateShellItem(
      IntPtr pidlParent,
      IntPtr psfParent,
      IntPtr pidl,
      out FolderBrowserDialogC.IShellItem ppsi);

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [Guid("DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7")]
    [ComImport]
    private class FileOpenDialog
    {

    }

    [Guid("42f85136-db7e-439c-85f1-e4075d135fc8")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    private interface IFileOpenDialog
    {
      [MethodImpl(MethodImplOptions.PreserveSig)]
      uint Show([In] IntPtr parent);

      void SetFileTypes();

      void SetFileTypeIndex([In] uint iFileType);

      void GetFileTypeIndex(out uint piFileType);

      void Advise();

      void Unadvise();

      void SetOptions([In] FolderBrowserDialogC.FOS fos);

      void GetOptions(out FolderBrowserDialogC.FOS pfos);

      void SetDefaultFolder(FolderBrowserDialogC.IShellItem psi);

      void SetFolder(FolderBrowserDialogC.IShellItem psi);

      void GetFolder(out FolderBrowserDialogC.IShellItem ppsi);

      void GetCurrentSelection(out FolderBrowserDialogC.IShellItem ppsi);

      void SetFileName([MarshalAs(UnmanagedType.LPWStr), In] string pszName);

      void GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);

      void SetTitle([MarshalAs(UnmanagedType.LPWStr), In] string pszTitle);

      void SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr), In] string pszText);

      void SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr), In] string pszLabel);

      void GetResult(out FolderBrowserDialogC.IShellItem ppsi);

      void AddPlace(FolderBrowserDialogC.IShellItem psi, int alignment);

      void SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr), In] string pszDefaultExtension);

      void Close(int hr);

      void SetClientGuid();

      void ClearClientData();

      void SetFilter([MarshalAs(UnmanagedType.Interface)] IntPtr pFilter);

      void GetResults([MarshalAs(UnmanagedType.Interface)] out IntPtr ppenum);

      void GetSelectedItems([MarshalAs(UnmanagedType.Interface)] out IntPtr ppsai);
    }

    [Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    private interface IShellItem
    {
      void BindToHandler();

      void GetParent();

      void GetDisplayName([In] FolderBrowserDialogC.SIGDN sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);

      void GetAttributes();

      void Compare();
    }

    private enum SIGDN : uint
    {
      SIGDN_NORMALDISPLAY = 0,
      SIGDN_PARENTRELATIVEPARSING = 2147581953, // 0x80018001
      SIGDN_DESKTOPABSOLUTEPARSING = 2147647488, // 0x80028000
      SIGDN_PARENTRELATIVEEDITING = 2147684353, // 0x80031001
      SIGDN_DESKTOPABSOLUTEEDITING = 2147794944, // 0x8004C000
      SIGDN_FILESYSPATH = 2147844096, // 0x80058000
      SIGDN_URL = 2147909632, // 0x80068000
      SIGDN_PARENTRELATIVEFORADDRESSBAR = 2147991553, // 0x8007C001
      SIGDN_PARENTRELATIVE = 2148007937, // 0x80080001
    }

    [Flags]
    private enum FOS
    {
      FOS_ALLNONSTORAGEITEMS = 128, // 0x00000080
      FOS_ALLOWMULTISELECT = 512, // 0x00000200
      FOS_CREATEPROMPT = 8192, // 0x00002000
      FOS_DEFAULTNOMINIMODE = 536870912, // 0x20000000
      FOS_DONTADDTORECENT = 33554432, // 0x02000000
      FOS_FILEMUSTEXIST = 4096, // 0x00001000
      FOS_FORCEFILESYSTEM = 64, // 0x00000040
      FOS_FORCESHOWHIDDEN = 268435456, // 0x10000000
      FOS_HIDEMRUPLACES = 131072, // 0x00020000
      FOS_HIDEPINNEDPLACES = 262144, // 0x00040000
      FOS_NOCHANGEDIR = 8,
      FOS_NODEREFERENCELINKS = 1048576, // 0x00100000
      FOS_NOREADONLYRETURN = 32768, // 0x00008000
      FOS_NOTESTFILECREATE = 65536, // 0x00010000
      FOS_NOVALIDATE = 256, // 0x00000100
      FOS_OVERWRITEPROMPT = 2,
      FOS_PATHMUSTEXIST = 2048, // 0x00000800
      FOS_PICKFOLDERS = 32, // 0x00000020
      FOS_SHAREAWARE = 16384, // 0x00004000
      FOS_STRICTFILETYPES = 4,
    }
  }
}
