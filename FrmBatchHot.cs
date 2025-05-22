// Decompiled with JetBrains decompiler
// Type: BnyEditor.FrmBatchHot
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace BnyEditor
{
  public class FrmBatchHot : Form
  {
    private IContainer components;
    private Label label1;
    private TextBox textBox1;
    private Label label2;
    private TextBox textBox2;
    private Button button1;
    private Button button2;
    private Button button3;
    private Button button4;
    private ColumnHeader columnHeader1;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel lbmsg;
    private ToolStripProgressBar pro1;
    private ListViewEx listView1;

    public FrmBatchHot() => this.InitializeComponent();

    private void button1_Click(object sender, EventArgs e)
    {
      FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
      {
        Description = "请选择待转换文件目录"
      };
      if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
        return;
      this.textBox1.Text = folderBrowserDialog.SelectedPath;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
      {
        Description = "请选择转换后文件保存目录"
      };
      if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
        return;
      this.textBox2.Text = folderBrowserDialog.SelectedPath;
    }

    private void button3_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.textBox1.Text))
      {
        int num1 = (int) MessageBox.Show("请选择待转换的BNY文件目录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else if (string.IsNullOrEmpty(this.textBox2.Text))
      {
        int num2 = (int) MessageBox.Show("请选择转换后保存BNY文件目录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        FileInfo[] files = new DirectoryInfo(this.textBox1.Text).GetFiles("*.bny");
        this.pro1.Value = 0;
        this.pro1.Maximum = files.Length;
        this.listView1.Items.Clear();
        foreach (FileInfo fileInfo in files)
        {
          this.lbmsg.Text = "正在转换【" + Path.GetFileName(fileInfo.FullName) + "】";
          Application.DoEvents();
          if (!BnyUtils.ReadFileIsHot(fileInfo.FullName))
          {
            HotUpdate.ToHotBnyFile(fileInfo.FullName, this.textBox2.Text + "\\" + Path.GetFileName(fileInfo.FullName));
            this.listView1.Items.Add(new ListViewItem("转换完成==> " + Path.GetFileName(fileInfo.FullName)));
            this.listView1.Items[this.listView1.Items.Count - 1].EnsureVisible();
          }
          else
          {
            this.listView1.Items.Add(new ListViewItem("热更新文件，无需转换==> " + Path.GetFileName(fileInfo.FullName)));
            this.listView1.Items[this.listView1.Items.Count - 1].EnsureVisible();
          }
          ++this.pro1.Value;
        }
        this.lbmsg.Text = "转换完成";
      }
    }

private void button4_Click(object sender, EventArgs e)
{
  if (string.IsNullOrEmpty(this.textBox1.Text))
  {
    int num1 = (int) MessageBox.Show("请选择待转换的BNY文件目录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }
  else if (string.IsNullOrEmpty(this.textBox2.Text))
  {
    int num2 = (int) MessageBox.Show("请选择转换后保存BNY文件目录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }
  else
  {
    // 定义要处理的文件扩展名
    string[] extensions = { "*.bny", "*.u3dext", "*.lua" };

    // 使用FileInfo数组存储所有要处理的文件
    FileInfo[] files = null;
    foreach (var extension in extensions)
    {
      // 如果是第一次循环，则直接赋值给files
      if (files == null)
      {
        files = new DirectoryInfo(this.textBox1.Text).GetFiles(extension);
      }
      else
      {
        // 否则合并现有文件和新找到的文件
        FileInfo[] newFiles = new DirectoryInfo(this.textBox1.Text).GetFiles(extension);
        Array.Resize(ref files, files.Length + newFiles.Length);
        Array.Copy(newFiles, 0, files, files.Length - newFiles.Length, newFiles.Length);
      }
    }

    this.pro1.Value = 0;
    this.pro1.Maximum = files.Length;
    this.listView1.Items.Clear();
    foreach (FileInfo fileInfo in files)
    {
      this.lbmsg.Text = "正在转换【" + Path.GetFileName(fileInfo.FullName) + "】";
      Application.DoEvents();
      HotUpdate.ToSourceBnyFile(fileInfo.FullName, this.textBox2.Text + "\\" + Path.GetFileName(fileInfo.FullName));
      this.listView1.Items.Add(new ListViewItem("转换完成==> " + Path.GetFileName(fileInfo.FullName)));
      this.listView1.Items[this.listView1.Items.Count - 1].EnsureVisible();
      ++this.pro1.Value;
    }
    this.lbmsg.Text = "转换完成";
  }
}

    private void textBox1_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
        e.Effect = DragDropEffects.Link;
      else
        e.Effect = DragDropEffects.None;
    }

    public bool IsDirectory(string path)
    {
      return (new FileInfo(path).Attributes & FileAttributes.Directory) > (FileAttributes) 0;
    }

    private void textBox1_DragDrop(object sender, DragEventArgs e)
    {
      string[] data = (string[]) e.Data.GetData(DataFormats.FileDrop);
      if (!this.IsDirectory(data[0]))
      {
        int num = (int) MessageBox.Show("对不起，只能拖拽文件夹", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
        (sender as TextBox).Text = data[0];
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmBatchHot));
      this.label1 = new Label();
      this.textBox1 = new TextBox();
      this.label2 = new Label();
      this.textBox2 = new TextBox();
      this.button1 = new Button();
      this.button2 = new Button();
      this.button3 = new Button();
      this.button4 = new Button();
      this.listView1 = new ListViewEx();
      this.columnHeader1 = new ColumnHeader();
      this.statusStrip1 = new StatusStrip();
      this.lbmsg = new ToolStripStatusLabel();
      this.pro1 = new ToolStripProgressBar();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(22, 20);
      this.label1.Name = "label1";
      this.label1.Size = new Size(83, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "待转文件目录:";
      this.textBox1.AllowDrop = true;
      this.textBox1.Location = new Point(111, 17);
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = false;
      this.textBox1.Size = new Size(307, 21);
      this.textBox1.TabIndex = 1;
      this.textBox1.DragDrop += new DragEventHandler(this.textBox1_DragDrop);
      this.textBox1.DragEnter += new DragEventHandler(this.textBox1_DragEnter);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(10, 53);
      this.label2.Name = "label2";
      this.label2.Size = new Size(95, 12);
      this.label2.TabIndex = 2;
      this.label2.Text = "转换后保存目录:";
      this.textBox2.AllowDrop = true;
      this.textBox2.Location = new Point(111, 50);
      this.textBox2.Name = "textBox2";
      this.textBox2.ReadOnly = false;
      this.textBox2.Size = new Size(307, 21);
      this.textBox2.TabIndex = 3;
      this.textBox2.DragDrop += new DragEventHandler(this.textBox1_DragDrop);
      this.textBox2.DragEnter += new DragEventHandler(this.textBox1_DragEnter);
      this.button1.Location = new Point(428, 15);
      this.button1.Name = "button1";
      this.button1.Size = new Size(31, 23);
      this.button1.TabIndex = 4;
      this.button1.Text = "...";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.button2.Location = new Point(428, 48);
      this.button2.Name = "button2";
      this.button2.Size = new Size(31, 23);
      this.button2.TabIndex = 5;
      this.button2.Text = "...";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.button3.Location = new Point(465, 15);
      this.button3.Name = "button3";
      this.button3.Size = new Size(153, 23);
      this.button3.TabIndex = 6;
      this.button3.Text = "源文件转为热更新";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new EventHandler(this.button3_Click);
      this.button4.Location = new Point(465, 48);
      this.button4.Name = "button4";
      this.button4.Size = new Size(153, 23);
      this.button4.TabIndex = 7;
      this.button4.Text = "热更新转为源文件";
      this.button4.UseVisualStyleBackColor = true;
      this.button4.Click += new EventHandler(this.button4_Click);
      this.listView1.Columns.AddRange(new ColumnHeader[1]
      {
        this.columnHeader1
      });
      this.listView1.FullRowSelect = true;
      this.listView1.GridLines = true;
      this.listView1.Location = new Point(12, 77);
      this.listView1.Name = "listView1";
      this.listView1.Size = new Size(606, 300);
      this.listView1.TabIndex = 8;
      this.listView1.UseCompatibleStateImageBehavior = false;
      this.listView1.View = View.Details;
      this.columnHeader1.Text = "已转文件";
      this.columnHeader1.Width = 580;
      this.statusStrip1.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.lbmsg,
        (ToolStripItem) this.pro1
      });
      this.statusStrip1.Location = new Point(0, 381);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new Size(628, 26);
      this.statusStrip1.TabIndex = 9;
      this.statusStrip1.Text = "statusStrip1";
      this.lbmsg.AutoSize = false;
      this.lbmsg.BorderSides = ToolStripStatusLabelBorderSides.Right;
      this.lbmsg.Name = "lbmsg";
      this.lbmsg.Size = new Size(300, 21);
      this.lbmsg.Text = "请选择后点击按钮转换";
      this.lbmsg.TextAlign = ContentAlignment.MiddleLeft;
      this.pro1.Name = "pro1";
      this.pro1.Size = new Size(300, 20);
      this.pro1.Step = 1;
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(628, 407);
      this.Controls.Add((Control) this.statusStrip1);
      this.Controls.Add((Control) this.listView1);
      this.Controls.Add((Control) this.button4);
      this.Controls.Add((Control) this.button3);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.textBox2);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.textBox1);
      this.Controls.Add((Control) this.label1);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Icon = this.Icon;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (FrmBatchHot);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "热更新<==>源文件";
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
