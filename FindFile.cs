// Decompiled with JetBrains decompiler
// Type: BnyEditor.FindFile
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace BnyEditor
{
  public class FindFile : Form
  {
    private bool isFindString = true;
    private bool isJingQue = true;
    private Ini ini = new Ini(AppDomain.CurrentDomain.BaseDirectory + "\\data.ini");
    private Dictionary<string, bool> dict = new Dictionary<string, bool>();
    private Thread[] findthreads;
    private static object _lockobject = new object();
    private IContainer components;
    private StatusStrip statusStrip1;
    private ToolStripProgressBar probar;
    private ToolStripStatusLabel msg;
    private ListView listView1;
    private ColumnHeader 文件名;
    private ColumnHeader columnHeader1;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem 在编辑器中打开ToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem 复制文件名称ToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem 复制文件路径ToolStripMenuItem;
    private ToolStripSeparator toolStripMenuItem1;
    private ToolStripMenuItem 导出搜索记录ToolStripMenuItem;
    private Label label2;
    private Label label1;
    private TextBox textBox2;
    private TextBox textBox1;
    private Button button1;
    private RadioButton radioButton2;
    private Button button2;
    private RadioButton radioButton3;
    private GroupBox groupBox1;
    private CheckBox checkBox1;
    private RadioButton radioButton1;

    public FindFile()
    {
      this.InitializeComponent();
      Control.CheckForIllegalCrossThreadCalls = false;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
      {
        Description = "请选择BNY文件目录"
      };
      if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
        return;
      this.textBox1.Text = folderBrowserDialog.SelectedPath;
      this.ini.Writue("LastSearch", "Folder", this.textBox1.Text);
    }

    private void FindFile_Load(object sender, EventArgs e)
    {
      if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\data.ini"))
        return;
      this.textBox1.Text = this.ini.ReadValue("LastSearch", "Folder");
    }

    private bool ReadTableData(
      StructExplain se,
      BinaryReader br,
      string finddata,
      ref string location,
      FileItem desc)
    {
      int serverI1 = BnyUtils.GetServerI(br);
      for (int index1 = 0; index1 < serverI1; ++index1)
      {
        for (int index2 = 0; index2 < se.Structs.Count; ++index2)
        {
          switch (se.Structs[index2].Type)
          {
            case 0:
            case 6:
              int serverI2 = BnyUtils.GetServerI(br);
              if (!this.isFindString && finddata.Trim() == serverI2.ToString())
              {
                location = this.getDesc(se.Name, desc) + " [第" + (object) (index1 + 1) + "行 , 第" + (object) (index2 + 1) + "列]";
                return true;
              }
              break;
            case 4:
            case 13:
              byte num = br.ReadByte();
              if (!this.isFindString && finddata.Trim() == num.ToString())
              {
                location = this.getDesc(se.Name, desc) + " [第" + (object) (index1 + 1) + "行]";
                return true;
              }
              break;
            case 9:
              int serverI3 = BnyUtils.GetServerI(br);
              if (serverI3 > 0)
              {
                string str = Encoding.UTF8.GetString(br.ReadBytes(serverI3));
                if (this.isFindString)
                {
                  if (this.isJingQue && str.Trim() == finddata.Trim())
                  {
                    location = this.getDesc(se.Name, desc) + " [第" + (object) (index1 + 1) + "行 , 第[" + (object) (index2 + 1) + "列]";
                    return true;
                  }
                  if (!this.isJingQue && str.IndexOf(finddata.Trim()) > -1)
                  {
                    location = this.getDesc(se.Name, desc) + " [第" + (object) (index1 + 1) + "行 , 第" + (object) (index2 + 1) + "列]";
                    return true;
                  }
                  break;
                }
                break;
              }
              break;
            case 11:
              if (se.Structs[index2].Structs != null && se.Structs[index2].Structs.Count > 0)
              {
                using (List<StructExplain>.Enumerator enumerator = se.Structs[index2].Structs.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    StructExplain current = enumerator.Current;
                    string empty = string.Empty;
                    if (this.ReadTableData(current, br, finddata, ref empty, desc))
                    {
                      location = this.getDesc(se.Name, desc) + "==>" + this.getDesc(se.Structs[index2].Name, desc) + "==> [第" + (object) (index1 + 1) + "行 , 第" + (object) (index2 + 1) + "列] " + empty;
                      return true;
                    }
                  }
                  break;
                }
              }
              else
                break;
            case 12:
              string empty1 = string.Empty;
              if (this.ReadTableData(se.Structs[index2], br, finddata, ref empty1, desc))
              {
                location = this.getDesc(se.Name, desc) + "==> [第" + (object) (index1 + 1) + "行 , 第" + (object) (index2 + 1) + "列] " + empty1;
                return true;
              }
              break;
          }
        }
      }
      return false;
    }

    private bool FindInfo(string finddata, BinaryReader br, ref string location, FileItem desc)
    {
      BnyStruct bnyStruct = new BnyStruct();
      br.BaseStream.Position = br.BaseStream.Length - 20L;
      bnyStruct.StructCount = br.ReadInt32();
      bnyStruct.OffsetStart = br.ReadInt32();
      bnyStruct.OffsetSize = br.ReadInt32();
      bnyStruct.StructExPlainStart = br.ReadInt32();
      bnyStruct.StructExPlainSize = br.ReadInt32();
      br.BaseStream.Position = (long) bnyStruct.StructExPlainStart;
      bnyStruct.StructExplainCount = BnyUtils.GetServerI(br);
      bnyStruct.Explains = new StructExplain[bnyStruct.StructExplainCount];
      for (int index = 0; index < bnyStruct.StructExplainCount; ++index)
      {
        bnyStruct.Explains[index] = new StructExplain();
        BnyUtils.ReadStructExplain(br, bnyStruct.Explains[index]);
      }
      bnyStruct.Type = br.ReadByte();
      br.BaseStream.Position = 0L;
      for (int index1 = 0; index1 < bnyStruct.StructCount && br.BaseStream.Position <= (long) bnyStruct.StructExPlainStart; ++index1)
      {
        for (int index2 = 0; index2 < bnyStruct.Explains.Length; ++index2)
        {
          switch (bnyStruct.Explains[index2].Type)
          {
            case 0:
            case 6:
              int serverI1 = BnyUtils.GetServerI(br);
              if (!this.isFindString && finddata.Trim() == serverI1.ToString())
              {
                location = this.getDesc(bnyStruct.Explains[index2].Name, desc) + "  [第" + (object) (index1 + 1) + "行]";
                return true;
              }
              break;
            case 4:
            case 13:
              byte num = br.ReadByte();
              if (!this.isFindString && finddata.Trim() == num.ToString())
              {
                location = this.getDesc(bnyStruct.Explains[index2].Name, desc) + "  [第" + (object) (index1 + 1) + "行]";
                return true;
              }
              break;
            case 9:
              int serverI2 = BnyUtils.GetServerI(br);
              if (serverI2 > 0)
              {
                string str = Encoding.UTF8.GetString(br.ReadBytes(serverI2));
                if (this.isFindString)
                {
                  if (this.isJingQue && str.Trim() == finddata.Trim())
                  {
                    location = this.getDesc(bnyStruct.Explains[index2].Name, desc) + "  [第" + (object) (index1 + 1) + "行]";
                    return true;
                  }
                  if (!this.isJingQue && str.IndexOf(finddata.Trim()) > -1)
                  {
                    location = this.getDesc(bnyStruct.Explains[index2].Name, desc) + "  [第" + (object) (index1 + 1) + "行]";
                    return true;
                  }
                  break;
                }
                break;
              }
              break;
            case 11:
              if (bnyStruct.Explains[index2].Structs.Count > 0)
              {
                using (List<StructExplain>.Enumerator enumerator = bnyStruct.Explains[index2].Structs.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    StructExplain current = enumerator.Current;
                    string empty = string.Empty;
                    if (this.ReadTableData(current, br, finddata, ref empty, desc))
                    {
                      location = this.getDesc(bnyStruct.Explains[index2].Name, desc) + " [第" + (object) (index1 + 1) + "行] ==>" + empty;
                      return true;
                    }
                  }
                  break;
                }
              }
              else
                break;
            case 12:
              string empty1 = string.Empty;
              if (this.ReadTableData(bnyStruct.Explains[index2], br, finddata, ref empty1, desc))
              {
                location = this.getDesc(bnyStruct.Explains[index2].Name, desc) + " [第" + (object) (index1 + 1) + "行]==>" + empty1;
                return true;
              }
              break;
          }
        }
      }
      return false;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (!(this.button2.Text == "开始搜索"))
      {
        if (this.findthreads != null)
        {
          for (int index = 0; index < this.findthreads.Length; ++index)
          {
            if (this.findthreads[index].IsAlive)
              this.findthreads[index].Abort();
          }
          this.findthreads = (Thread[]) null;
        }
        this.msg.Text = "停止查找";
        this.button2.Text = "开始搜索";
      }
      else
      {
        this.listView1.Items.Clear();
        if (string.IsNullOrEmpty(this.textBox1.Text))
        {
          int num1 = (int) MessageBox.Show("请选择要搜索的目录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        else if (this.textBox2.Text == string.Empty)
        {
          int num2 = (int) MessageBox.Show("请输入搜索内容", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        else
        {
          this.button2.Text = "停止搜索";
          this.isFindString = this.radioButton1.Checked || this.radioButton3.Checked;
          if (this.radioButton1.Checked)
            this.isJingQue = true;
          else if (this.radioButton3.Checked)
            this.isJingQue = false;
          this.dict.Clear();
          FileInfo[] files = new DirectoryInfo(this.textBox1.Text).GetFiles("*.bny");
          this.probar.Maximum = files.Length;
          foreach (FileInfo fileInfo in files)
          {
            if (!this.dict.ContainsKey(fileInfo.FullName))
              this.dict.Add(fileInfo.FullName, false);
          }
          this.probar.Value = 0;
          this.msg.Text = "正在查找";
          this.findthreads = new Thread[5];
          for (int index = 0; index < 5; ++index)
          {
            this.findthreads[index] = new Thread(new ThreadStart(this.RunThread));
            this.findthreads[index].Start();
          }
        }
      }
    }

    private string getFile()
    {
      string key = string.Empty;
      lock (FindFile._lockobject)
      {
        foreach (KeyValuePair<string, bool> keyValuePair in this.dict)
        {
          if (!keyValuePair.Value)
          {
            key = keyValuePair.Key;
            break;
          }
        }
        if (!string.IsNullOrEmpty(key))
          this.dict[key] = true;
      }
      return key;
    }

    private void RunThread()
    {
      string empty = string.Empty;
      string file;
      do
      {
        file = this.getFile();
        if (!string.IsNullOrEmpty(file))
        {
          try
          {
            this.isFinded(file);
          }
          catch
          {
          }
        }
      }
      while (!string.IsNullOrEmpty(file));
    }

    public string getDesc(string name, FileItem filedesc)
    {
      if (filedesc == null)
        return name;
      foreach (FiledItem filedItem in filedesc.filed)
      {
        if (filedItem.name == name)
          return filedItem.desc.Replace("\r\n", "").Replace("\n", "");
      }
      return name;
    }

    private FileItem FindFileDesc(string fileName)
    {
      if (!string.IsNullOrEmpty(fileName) && BnyCommon.Root != null)
      {
        foreach (FileItem data in BnyCommon.Root.datas)
        {
          if (data.name == fileName)
            return data;
        }
      }
      return (FileItem) null;
    }

    private void isFinded(string file)
    {
      string fileName = Path.GetFileName(file).Replace(".bny", "");
      if (fileName.IndexOf(".") > -1)
        fileName = fileName.Substring(fileName.LastIndexOf(".") + 1);
      FileItem fileDesc = this.FindFileDesc(fileName);
      string str = this.textBox2.Text;
      if ((this.radioButton1.Checked || this.radioButton3.Checked) && this.checkBox1.Checked)
        str = ChineseStringUtility.ToTraditional(str);
      if (BnyUtils.ReadFileIsHot(file))
      {
        using (MemoryStream input = new MemoryStream(HotUpdate.DecompressBny(file)))
        {
          using (BinaryReader br = new BinaryReader((Stream) input, Encoding.UTF8))
          {
            string empty = string.Empty;
            if (this.FindInfo(str, br, ref empty, fileDesc))
            {
              this.listView1.BeginUpdate();
              this.listView1.Items.Add(new ListViewItem(new string[2]
              {
                file,
                empty
              }));
              this.listView1.EndUpdate();
            }
          }
        }
      }
      else
      {
        using (FileStream input = new FileStream(file, FileMode.Open, FileAccess.Read))
        {
          using (BinaryReader br = new BinaryReader((Stream) input, Encoding.UTF8))
          {
            string empty = string.Empty;
            if (this.FindInfo(str, br, ref empty, fileDesc))
            {
              this.listView1.BeginUpdate();
              this.listView1.Items.Add(new ListViewItem(new string[2]
              {
                file,
                empty
              }));
              this.listView1.EndUpdate();
            }
          }
        }
      }
      int num = 0;
      foreach (KeyValuePair<string, bool> keyValuePair in this.dict)
      {
        if (keyValuePair.Value)
          ++num;
      }
      if (this.probar.Value < this.probar.Maximum)
        ++this.probar.Value;
      if (num != this.dict.Count)
        return;
      this.probar.Value = this.probar.Maximum;
      this.msg.Text = "查找完成";
      this.button2.Text = "开始搜索";
    }

    private void FindFile_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.findthreads == null)
        return;
      for (int index = 0; index < this.findthreads.Length; ++index)
      {
        if (this.findthreads[index].IsAlive)
          this.findthreads[index].Abort();
      }
      this.findthreads = (Thread[]) null;
    }

    private void 在编辑器中打开ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.listView1.SelectedItems.Count <= 0 || BnyCommon.MainForm == null)
        return;
      BnyCommon.MainForm.OpenFile(this.listView1.SelectedItems[0].SubItems[0].Text);
    }

    private void 复制文件名称ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.listView1.SelectedItems.Count <= 0 || BnyCommon.MainForm == null)
        return;
      Clipboard.SetText(Path.GetFileName(this.listView1.SelectedItems[0].SubItems[0].Text));
    }

    private void 复制文件路径ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.listView1.SelectedItems.Count <= 0 || BnyCommon.MainForm == null)
        return;
      Clipboard.SetText(this.listView1.SelectedItems[0].SubItems[0].Text);
    }

    private void 导出搜索记录ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.listView1.Items.Count <= 0)
        return;
      SaveFileDialog saveFileDialog1 = new SaveFileDialog();
      saveFileDialog1.Title = "请选择保存地址";
      saveFileDialog1.Filter = "*.txt|*.txt";
      saveFileDialog1.FileName = this.textBox2.Text + "_搜索结果.txt";
      SaveFileDialog saveFileDialog2 = saveFileDialog1;
      if (saveFileDialog2.ShowDialog() != DialogResult.OK)
        return;
      using (FileStream fileStream = new FileStream(saveFileDialog2.FileName, FileMode.Create, FileAccess.Write))
      {
        using (StreamWriter streamWriter = new StreamWriter((Stream) fileStream, Encoding.Default))
        {
          streamWriter.WriteLine(this.textBox2.Text + "的搜索结果");
          streamWriter.WriteLine("================================================================================");
          foreach (ListViewItem listViewItem in this.listView1.Items)
          {
            streamWriter.WriteLine(listViewItem.SubItems[0].Text);
            streamWriter.WriteLine(listViewItem.SubItems[1].Text);
            streamWriter.WriteLine("================================================================================");
            streamWriter.WriteLine();
          }
          streamWriter.Flush();
        }
      }
      int num = (int) MessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FindFile));
      this.statusStrip1 = new StatusStrip();
      this.probar = new ToolStripProgressBar();
      this.msg = new ToolStripStatusLabel();
      this.listView1 = new ListView();
      this.文件名 = new ColumnHeader();
      this.columnHeader1 = new ColumnHeader();
      this.contextMenuStrip1 = new ContextMenuStrip(this.components);
      this.在编辑器中打开ToolStripMenuItem = new ToolStripMenuItem();
      this.复制文件名称ToolStripMenuItem = new ToolStripMenuItem();
      this.复制文件路径ToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.toolStripSeparator2 = new ToolStripSeparator();
      this.toolStripMenuItem1 = new ToolStripSeparator();
      this.导出搜索记录ToolStripMenuItem = new ToolStripMenuItem();
      this.label2 = new Label();
      this.label1 = new Label();
      this.textBox2 = new TextBox();
      this.textBox1 = new TextBox();
      this.button1 = new Button();
      this.radioButton2 = new RadioButton();
      this.button2 = new Button();
      this.radioButton3 = new RadioButton();
      this.groupBox1 = new GroupBox();
      this.radioButton1 = new RadioButton();
      this.checkBox1 = new CheckBox();
      this.statusStrip1.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      this.statusStrip1.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.probar,
        (ToolStripItem) this.msg
      });
      this.statusStrip1.Location = new Point(0, 485);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new Size(925, 22);
      this.statusStrip1.TabIndex = 5;
      this.statusStrip1.Text = "statusStrip1";
      this.probar.Name = "probar";
      this.probar.Size = new Size(300, 16);
      this.msg.BorderSides = ToolStripStatusLabelBorderSides.Left;
      this.msg.Name = "msg";
      this.msg.Size = new Size(608, 17);
      this.msg.Spring = true;
      this.msg.TextAlign = ContentAlignment.MiddleLeft;
      this.listView1.Columns.AddRange(new ColumnHeader[2]
      {
        this.文件名,
        this.columnHeader1
      });
      this.listView1.ContextMenuStrip = this.contextMenuStrip1;
      this.listView1.FullRowSelect = true;
      this.listView1.GridLines = true;
      this.listView1.Location = new Point(12, 64);
      this.listView1.Name = "listView1";
      this.listView1.Size = new Size(905, 418);
      this.listView1.TabIndex = 6;
      this.listView1.UseCompatibleStateImageBehavior = false;
      this.listView1.View = View.Details;
      this.文件名.Text = "文件名";
      this.文件名.Width = 555;
      this.columnHeader1.Text = "位置";
      this.columnHeader1.Width = 200;
      this.contextMenuStrip1.Items.AddRange(new ToolStripItem[7]
      {
        (ToolStripItem) this.在编辑器中打开ToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.复制文件名称ToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator2,
        (ToolStripItem) this.复制文件路径ToolStripMenuItem,
        (ToolStripItem) this.toolStripMenuItem1,
        (ToolStripItem) this.导出搜索记录ToolStripMenuItem
      });
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new Size(161, 110);
      this.在编辑器中打开ToolStripMenuItem.Name = "在编辑器中打开ToolStripMenuItem";
      this.在编辑器中打开ToolStripMenuItem.Size = new Size(160, 22);
      this.在编辑器中打开ToolStripMenuItem.Text = "在编辑器中打开";
      this.在编辑器中打开ToolStripMenuItem.Click += new EventHandler(this.在编辑器中打开ToolStripMenuItem_Click);
      this.复制文件名称ToolStripMenuItem.Name = "复制文件名称ToolStripMenuItem";
      this.复制文件名称ToolStripMenuItem.Size = new Size(160, 22);
      this.复制文件名称ToolStripMenuItem.Text = "复制文件名称";
      this.复制文件名称ToolStripMenuItem.Click += new EventHandler(this.复制文件名称ToolStripMenuItem_Click);
      this.复制文件路径ToolStripMenuItem.Name = "复制文件路径ToolStripMenuItem";
      this.复制文件路径ToolStripMenuItem.Size = new Size(160, 22);
      this.复制文件路径ToolStripMenuItem.Text = "复制文件路径";
      this.复制文件路径ToolStripMenuItem.Click += new EventHandler(this.复制文件路径ToolStripMenuItem_Click);
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(157, 6);
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new Size(157, 6);
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new Size(157, 6);
      this.导出搜索记录ToolStripMenuItem.Name = "导出搜索记录ToolStripMenuItem";
      this.导出搜索记录ToolStripMenuItem.Size = new Size(160, 22);
      this.导出搜索记录ToolStripMenuItem.Text = "导出搜索记录";
      this.导出搜索记录ToolStripMenuItem.Click += new EventHandler(this.导出搜索记录ToolStripMenuItem_Click);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(361, 24);
      this.label2.Name = "label2";
      this.label2.Size = new Size(35, 12);
      this.label2.TabIndex = 2;
      this.label2.Text = "搜索:";
      this.label1.AutoSize = true;
      this.label1.Location = new Point(11, 23);
      this.label1.Name = "label1";
      this.label1.Size = new Size(59, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "选择目录:";
      this.textBox2.Location = new Point(402, 20);
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new Size(100, 21);
      this.textBox2.TabIndex = 3;
      this.textBox1.Location = new Point(76, 20);
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new Size(237, 21);
      this.textBox1.TabIndex = 1;
      this.button1.Location = new Point(319, 18);
      this.button1.Name = "button1";
      this.button1.Size = new Size(36, 23);
      this.button1.TabIndex = 4;
      this.button1.Text = "...";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.radioButton2.AutoSize = true;
      this.radioButton2.Location = new Point(662, 22);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.Size = new Size(47, 16);
      this.radioButton2.TabIndex = 6;
      this.radioButton2.Text = "数值";
      this.radioButton2.UseVisualStyleBackColor = true;
      this.button2.Location = new Point(830, 19);
      this.button2.Name = "button2";
      this.button2.Size = new Size(61, 23);
      this.button2.TabIndex = 7;
      this.button2.Text = "开始搜索";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.radioButton3.AutoSize = true;
      this.radioButton3.Location = new Point(585, 22);
      this.radioButton3.Name = "radioButton3";
      this.radioButton3.Size = new Size(71, 16);
      this.radioButton3.TabIndex = 8;
      this.radioButton3.Text = "模糊文本";
      this.radioButton3.UseVisualStyleBackColor = true;
      this.groupBox1.Controls.Add((Control) this.checkBox1);
      this.groupBox1.Controls.Add((Control) this.radioButton3);
      this.groupBox1.Controls.Add((Control) this.button2);
      this.groupBox1.Controls.Add((Control) this.radioButton2);
      this.groupBox1.Controls.Add((Control) this.radioButton1);
      this.groupBox1.Controls.Add((Control) this.button1);
      this.groupBox1.Controls.Add((Control) this.textBox1);
      this.groupBox1.Controls.Add((Control) this.textBox2);
      this.groupBox1.Controls.Add((Control) this.label1);
      this.groupBox1.Controls.Add((Control) this.label2);
      this.groupBox1.Location = new Point(12, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(905, 55);
      this.groupBox1.TabIndex = 4;
      this.groupBox1.TabStop = false;
      this.radioButton1.AutoSize = true;
      this.radioButton1.Checked = true;
      this.radioButton1.Location = new Point(508, 22);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new Size(71, 16);
      this.radioButton1.TabIndex = 5;
      this.radioButton1.TabStop = true;
      this.radioButton1.Text = "精确文本";
      this.radioButton1.UseVisualStyleBackColor = true;
      this.checkBox1.AutoSize = true;
      this.checkBox1.Location = new Point(716, 23);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new Size(108, 16);
      this.checkBox1.TabIndex = 9;
      this.checkBox1.Text = "文本简体转繁体";
      this.checkBox1.UseVisualStyleBackColor = true;
      this.AcceptButton = (IButtonControl) this.button2;
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(925, 507);
      this.Controls.Add((Control) this.listView1);
      this.Controls.Add((Control) this.statusStrip1);
      this.Controls.Add((Control) this.groupBox1);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Icon = this.Icon;
      this.MaximizeBox = false;
      this.Name = nameof (FindFile);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "查找文件";
      this.FormClosing += new FormClosingEventHandler(this.FindFile_FormClosing);
      this.Load += new EventHandler(this.FindFile_Load);
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.contextMenuStrip1.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
