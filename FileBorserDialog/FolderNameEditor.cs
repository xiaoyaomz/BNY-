// Decompiled with JetBrains decompiler
// Type: FileBorserDialog.FolderNameEditor
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace FileBorserDialog
{
  public class FolderNameEditor : UITypeEditor
  {
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.Modal;
    }

    public override object EditValue(
      ITypeDescriptorContext context,
      System.IServiceProvider provider,
      object value)
    {
      FolderBrowserDialogC folderBrowserDialogC = new FolderBrowserDialogC();
      if (value != null)
        folderBrowserDialogC.DirectoryPath = string.Format("{0}", value);
      return folderBrowserDialogC.ShowDialog((IWin32Window) null) == DialogResult.OK ? (object) folderBrowserDialogC.DirectoryPath : value;
    }
  }
}
