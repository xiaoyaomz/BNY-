// Decompiled with JetBrains decompiler
// Type: BnyEditor.HotUpdateFrm
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using FileBorserDialog;
using SevenZip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace BnyEditor
{
  public class HotUpdateFrm : Form
  {
    private Ini ini = new Ini(AppDomain.CurrentDomain.BaseDirectory + "\\data.ini");
    private IContainer components;
    private GroupBox groupBox1;
    private TextBox textBox3;
    private Label label3;
    private Button button1;
    private TextBox textBox2;
    private Label label2;
    private TextBox textBox1;
    private Label label1;
    private ListView listView1;
    private ColumnHeader columnHeader1;
    private Button button2;
    private Button button3;
    private Label label4;
    private GroupBox groupBox2;
    private Label label5;
    private Button button4;
    private GroupBox groupBox3;
    private TextBox txtm;
    private Label label7;
    private TextBox txtv;
    private Label label6;
    private ProgressBar progressBar1;
    private Label status;

    public HotUpdateFrm() => this.InitializeComponent();

    private void button3_Click(object sender, EventArgs e) => this.Close();

    private void FillView(DirectoryInfo dir)
    {
      foreach (FileInfo file in dir.GetFiles())
        this.listView1.Items.Add(new ListViewItem(new string[1]
        {
          file.FullName.Replace(this.textBox3.Text, "").Substring(1).Replace("\\", "/")
        }));
      foreach (DirectoryInfo directory in dir.GetDirectories())
        this.FillView(directory);
    }

    private void button1_Click(object sender, EventArgs e)
    {
      FolderBrowserDialogC folderBrowserDialogC = new FolderBrowserDialogC();
      if (folderBrowserDialogC.ShowDialog((IWin32Window) this) != DialogResult.OK)
        return;
      this.listView1.Items.Clear();
      this.textBox3.Text = folderBrowserDialogC.DirectoryPath;
      DirectoryInfo dir = new DirectoryInfo(folderBrowserDialogC.DirectoryPath);
      this.listView1.BeginUpdate();
      this.FillView(dir);
      this.listView1.EndUpdate();
      this.progressBar1.Value = 0;
      this.progressBar1.Maximum = this.listView1.Items.Count + 5;
      this.progressBar1.Minimum = 0;
      this.label4.Text = "共有:" + (object) this.listView1.Items.Count + "个文件";
    }

    public void CopyFolder(string sourcePath, string destPath)
    {
      if (!Directory.Exists(sourcePath))
        throw new DirectoryNotFoundException("源目录不存在！");
      if (!Directory.Exists(destPath))
      {
        try
        {
          Directory.CreateDirectory(destPath);
        }
        catch (Exception ex)
        {
          throw new Exception("创建目标目录失败：" + ex.Message);
        }
      }
      new List<string>((IEnumerable<string>) Directory.GetFiles(sourcePath)).ForEach((Action<string>) (c =>
      {
        string str = Path.Combine(destPath, Path.GetFileName(c));
        if (Path.GetExtension(c).ToLower() == ".bny" || Path.GetExtension(c).ToLower() == ".u3dext")
        {
          if (!BnyUtils.ReadFileIsHot(c))
          {
            HotUpdate.ToHotBnyFile(c, str);
            Application.DoEvents();
          }
          else
            File.Copy(c, str, true);
        }
        else
          File.Copy(c, str, true);
        ++this.progressBar1.Value;
        Application.DoEvents();
      }));
      new List<string>((IEnumerable<string>) Directory.GetDirectories(sourcePath)).ForEach((Action<string>) (c =>
      {
        string destPath1 = Path.Combine(destPath, Path.GetFileName(c));
        this.CopyFolder(c, destPath1);
      }));
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.progressBar1.Value = 0;
      this.status.Text = "正在准备工作...";
      Application.DoEvents();
      string text1 = this.textBox3.Text;
      string text2 = this.textBox1.Text;
      string str1 = (int.Parse(this.textBox2.Text) + 1).ToString();
      string path = new DirectoryInfo(text1).Parent.FullName + "\\MZ" + text2 + "-" + str1 + "\\";
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
      this.button2.Enabled = false;
      this.status.Text = "开始处理文件...";
      Application.DoEvents();
      this.CopyFolder(text1, path + "element");
      string str2 = path;
      ++this.progressBar1.Value;
      Application.DoEvents();
      this.status.Text = "正在生成配置文件...";
      Application.DoEvents();
      string contents = "# " + text2 + " " + str1 + " mhzx " + (object) this.listView1.Items.Count + "\n";
      foreach (ListViewItem listViewItem in this.listView1.Items)
        contents = contents + "+ " + listViewItem.Text + "\n";
      this.status.Text = "正在写入配置文件...";
      Application.DoEvents();
      ++this.progressBar1.Value;
      Application.DoEvents();
      File.WriteAllText(str2 + "\\inc", contents, Encoding.ASCII);
      this.status.Text = "正在写入配置文件...";
      Application.DoEvents();
      this.status.Text = "正在打包文件...";
      Application.DoEvents();
      ++this.progressBar1.Value;
      Application.DoEvents();
      SevenZipBase.SetLibraryPath(AppDomain.CurrentDomain.BaseDirectory + "7z.dll");
      SevenZipCompressor sevenZipCompressor = new SevenZipCompressor();
      ++this.progressBar1.Value;
      sevenZipCompressor.ArchiveFormat = OutArchiveFormat.SevenZip;
      sevenZipCompressor.CompressionMethod = CompressionMethod.Lzma;
      sevenZipCompressor.CompressionMode = CompressionMode.Create;
      sevenZipCompressor.ZipEncryptionMethod = ZipEncryptionMethod.Aes256;
      sevenZipCompressor.PreserveDirectoryRoot = true;
      DirectoryInfo directoryInfo = new DirectoryInfo(str2);
      string str3 = directoryInfo.Parent.FullName + "\\" + directoryInfo.Name + ".7z";
      sevenZipCompressor.CompressDirectory(str2, str3);
      string md5HashFromFile = HotUpdate.GetMD5HashFromFile(str3);
      int fileSize = HotUpdate.GetFileSize(str3);
      string str4 = str1 + "/" + text2;
      string str5 = text2 + "-" + str1 + "    " + md5HashFromFile + "    " + (object) fileSize;
      this.txtv.Text = str4;
      this.txtm.Text = str5;
      File.WriteAllText(directoryInfo.Parent.FullName + "\\版本修改.txt", "版本信息:" + str4 + Environment.NewLine + "MD5信息:" + str5, Encoding.Default);
      File.Move(str3, str3.Replace(".7z", "." + md5HashFromFile.Substring(0, 6) + ".mzp"));
      this.status.Text = "正在清理临时文件...";
      Application.DoEvents();
      Directory.Delete(str2, true);
      this.button2.Enabled = true;
      this.textBox2.Text = str1;
      this.ini.Writue("Version", "Curr", str1);
      ++this.progressBar1.Value;
      Application.DoEvents();
      this.status.Text = "恭喜，热更新文件生成成功!";
      Application.DoEvents();
      int num = (int) MessageBox.Show("恭喜，热更新文件生成成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void HotUpdateFrm_Load(object sender, EventArgs e)
    {
      if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\data.ini"))
      {
        int num = (int) MessageBox.Show("请注意需要填写版本信息!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        this.textBox1.Text = this.ini.ReadValue("Version", "Start");
        this.textBox2.Text = this.ini.ReadValue("Version", "Curr");
      }
    }

    private void HotUpdateFrm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.textBox1.Text != "")
        this.ini.Writue("Version", "Start", this.textBox1.Text);
      if (!(this.textBox2.Text != ""))
        return;
      this.ini.Writue("Version", "Curr", this.textBox2.Text);
    }

    private void button4_Click(object sender, EventArgs e)
    {
      if (this.button4.Text == "帮助>>")
      {
        this.Width = 988;
        this.button4.Text = "<<隐藏";
      }
      else
      {
        this.Width = 680;
        this.button4.Text = "帮助>>";
      }
    }

    private void txtv_DoubleClick(object sender, EventArgs e)
    {
      (sender as TextBox).SelectAll();
      Clipboard.SetText((sender as TextBox).Text);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (HotUpdateFrm));
      this.groupBox1 = new GroupBox();
      this.textBox3 = new TextBox();
      this.label3 = new Label();
      this.button1 = new Button();
      this.textBox2 = new TextBox();
      this.label2 = new Label();
      this.textBox1 = new TextBox();
      this.label1 = new Label();
      this.listView1 = new ListView();
      this.columnHeader1 = new ColumnHeader();
      this.button2 = new Button();
      this.button3 = new Button();
      this.label4 = new Label();
      this.groupBox2 = new GroupBox();
      this.label5 = new Label();
      this.button4 = new Button();
      this.groupBox3 = new GroupBox();
      this.txtm = new TextBox();
      this.label7 = new Label();
      this.txtv = new TextBox();
      this.label6 = new Label();
      this.progressBar1 = new ProgressBar();
      this.status = new Label();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      this.groupBox1.Controls.Add((Control) this.textBox3);
      this.groupBox1.Controls.Add((Control) this.label3);
      this.groupBox1.Controls.Add((Control) this.button1);
      this.groupBox1.Controls.Add((Control) this.textBox2);
      this.groupBox1.Controls.Add((Control) this.label2);
      this.groupBox1.Controls.Add((Control) this.textBox1);
      this.groupBox1.Controls.Add((Control) this.label1);
      this.groupBox1.Location = new Point(12, 11);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(645, 59);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "程序设置";
      this.textBox3.BackColor = Color.White;
      this.textBox3.Location = new Point(448, 25);
      this.textBox3.Name = "textBox3";
      this.textBox3.ReadOnly = true;
      this.textBox3.Size = new Size(92, 21);
      this.textBox3.TabIndex = 6;
      this.label3.AutoSize = true;
      this.label3.Location = new Point(371, 28);
      this.label3.Name = "label3";
      this.label3.Size = new Size(71, 12);
      this.label3.TabIndex = 5;
      this.label3.Text = "补丁包目录:";
      this.button1.Location = new Point(556, 25);
      this.button1.Name = "button1";
      this.button1.Size = new Size(75, 23);
      this.button1.TabIndex = 4;
      this.button1.Text = "选择";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.textBox2.Location = new Point(277, 26);
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new Size(70, 21);
      this.textBox2.TabIndex = 3;
      this.label2.AutoSize = true;
      this.label2.Location = new Point(177, 28);
      this.label2.Name = "label2";
      this.label2.Size = new Size(95, 12);
      this.label2.TabIndex = 2;
      this.label2.Text = "服务器当前版本:";
      this.textBox1.Location = new Point(89, 25);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(70, 21);
      this.textBox1.TabIndex = 1;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(24, 29);
      this.label1.Name = "label1";
      this.label1.Size = new Size(59, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "初始版本:";
      this.listView1.Columns.AddRange(new ColumnHeader[1]
      {
        this.columnHeader1
      });
      this.listView1.FullRowSelect = true;
      this.listView1.GridLines = true;
      this.listView1.Location = new Point(12, 76);
      this.listView1.Name = "listView1";
      this.listView1.Size = new Size(645, 453);
      this.listView1.TabIndex = 1;
      this.listView1.UseCompatibleStateImageBehavior = false;
      this.listView1.View = View.Details;
      this.columnHeader1.Text = "补丁文件";
      this.columnHeader1.Width = 600;
      this.button2.Location = new Point(359, 612);
      this.button2.Name = "button2";
      this.button2.Size = new Size(95, 29);
      this.button2.TabIndex = 3;
      this.button2.Text = "制作补丁包";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.button3.Location = new Point(460, 611);
      this.button3.Name = "button3";
      this.button3.Size = new Size(95, 29);
      this.button3.TabIndex = 4;
      this.button3.Text = "退出";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new EventHandler(this.button3_Click);
      this.label4.AutoSize = true;
      this.label4.Location = new Point(18, 620);
      this.label4.Name = "label4";
      this.label4.Size = new Size(0, 12);
      this.label4.TabIndex = 5;
      this.groupBox2.Controls.Add((Control) this.label5);
      this.groupBox2.Location = new Point(667, 11);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(293, 629);
      this.groupBox2.TabIndex = 6;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "使用帮助";
      this.label5.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134, true);
      this.label5.ForeColor = Color.Red;
      this.label5.Location = new Point(6, 25);
      this.label5.Name = "label5";
      this.label5.Size = new Size(281, 552);
      this.label5.TabIndex = 0;
      this.label5.Text = "请注意，第一次使用需要设置版本信息\r\n\r\n设置后，每次生成热更新会自动增加服务器当前版本\r\n\r\n请在存储时按照element目录结构存储\r\n\r\n例子:\r\n\r\n element\\data\\cfg\\\r\n\r\n\r\n";
      this.button4.Location = new Point(560, 611);
      this.button4.Name = "button4";
      this.button4.Size = new Size(92, 29);
      this.button4.TabIndex = 7;
      this.button4.Text = "帮助>>";
      this.button4.UseVisualStyleBackColor = true;
      this.button4.Click += new EventHandler(this.button4_Click);
      this.groupBox3.Controls.Add((Control) this.txtm);
      this.groupBox3.Controls.Add((Control) this.label7);
      this.groupBox3.Controls.Add((Control) this.txtv);
      this.groupBox3.Controls.Add((Control) this.label6);
      this.groupBox3.Location = new Point(12, 535);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new Size(645, 61);
      this.groupBox3.TabIndex = 8;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "版本信息 [双击内容会自动复制到剪贴板]";
      this.txtm.BackColor = Color.White;
      this.txtm.Location = new Point(218, 25);
      this.txtm.Name = "txtm";
      this.txtm.ReadOnly = true;
      this.txtm.Size = new Size(413, 21);
      this.txtm.TabIndex = 3;
      this.txtm.DoubleClick += new EventHandler(this.txtv_DoubleClick);
      this.label7.AutoSize = true;
      this.label7.Location = new Point(183, 29);
      this.label7.Name = "label7";
      this.label7.Size = new Size(29, 12);
      this.label7.TabIndex = 2;
      this.label7.Text = "MD5:";
      this.txtv.BackColor = Color.White;
      this.txtv.Location = new Point(52, 25);
      this.txtv.Name = "txtv";
      this.txtv.ReadOnly = true;
      this.txtv.Size = new Size(125, 21);
      this.txtv.TabIndex = 1;
      this.txtv.DoubleClick += new EventHandler(this.txtv_DoubleClick);
      this.label6.AutoSize = true;
      this.label6.Location = new Point(11, 28);
      this.label6.Name = "label6";
      this.label6.Size = new Size(35, 12);
      this.label6.TabIndex = 0;
      this.label6.Text = "版本:";
      this.progressBar1.Location = new Point(12, 647);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new Size(645, 15);
      this.progressBar1.TabIndex = 9;
      this.status.AutoSize = true;
      this.status.ForeColor = Color.ForestGreen;
      this.status.Location = new Point(18, 620);
      this.status.Name = "status";
      this.status.Size = new Size(0, 12);
      this.status.TabIndex = 10;
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(663, 665);
      this.Controls.Add((Control) this.status);
      this.Controls.Add((Control) this.progressBar1);
      this.Controls.Add((Control) this.groupBox3);
      this.Controls.Add((Control) this.button4);
      this.Controls.Add((Control) this.groupBox2);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.button3);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.listView1);
      this.Controls.Add((Control) this.groupBox1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Icon = this.Icon;
      this.MaximizeBox = false;
      this.Name = nameof (HotUpdateFrm);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "热更新补丁包制作";
      this.FormClosing += new FormClosingEventHandler(this.HotUpdateFrm_FormClosing);
      this.Load += new EventHandler(this.HotUpdateFrm_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
