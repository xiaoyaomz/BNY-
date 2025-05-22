// Decompiled with JetBrains decompiler
// Type: BnyEditor.FrmSaveNew
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace BnyEditor
{
  public class FrmSaveNew : Form
  {
    private string _fileName;
    private BnyStruct bny;
    private IContainer components;
    private Label label1;
    private Label label2;
    private TextBox textBox1;
    private TextBox textBox2;
    private Button button1;
    private Button button2;
    private Button button3;

    public FrmSaveNew(string filename)
    {
      this.InitializeComponent();
      this._fileName = filename;
    }

    private BnyStruct ReadStruct(BinaryReader br)
    {
      BnyStruct bnyStruct = new BnyStruct();
      br.BaseStream.Position = br.BaseStream.Length - 20L;
      bnyStruct.StructCount = br.ReadInt32();
      bnyStruct.OffsetStart = br.ReadInt32();
      bnyStruct.OffsetSize = br.ReadInt32();
      bnyStruct.StructExPlainStart = br.ReadInt32();
      bnyStruct.StructExPlainSize = br.ReadInt32();
      br.BaseStream.Position = 0L;
      bnyStruct.Datas = br.ReadBytes(bnyStruct.StructExPlainStart);
      bnyStruct.StructExplainBackUpData = br.ReadBytes(bnyStruct.StructExPlainSize);
      br.BaseStream.Position = (long) bnyStruct.StructExPlainStart;
      bnyStruct.StructExplainCount = BnyUtils.GetServerI(br);
      bnyStruct.Explains = new StructExplain[bnyStruct.StructExplainCount];
      for (int index = 0; index < bnyStruct.StructExplainCount; ++index)
      {
        bnyStruct.Explains[index] = new StructExplain();
        BnyUtils.ReadStructExplain(br, bnyStruct.Explains[index]);
      }
      bnyStruct.Type = br.ReadByte();
      return bnyStruct;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog1 = new OpenFileDialog();
      openFileDialog1.Title = "请选择要保存为新格式的文件，用于读取架构";
      openFileDialog1.Filter = "*.bny|*.bny";
      openFileDialog1.FileName = this._fileName;
      OpenFileDialog openFileDialog2 = openFileDialog1;
      if (openFileDialog2.ShowDialog() != DialogResult.OK)
        return;
      this.textBox1.Text = openFileDialog2.FileName;
      if (BnyUtils.ReadFileIsHot(openFileDialog2.FileName))
      {
        using (MemoryStream input = new MemoryStream(HotUpdate.DecompressBny(openFileDialog2.FileName)))
        {
          using (BinaryReader br = new BinaryReader((Stream) input, Encoding.UTF8))
            this.bny = this.ReadStruct(br);
        }
      }
      else
      {
        using (FileStream input = new FileStream(openFileDialog2.FileName, FileMode.Open, FileAccess.Read))
        {
          using (BinaryReader br = new BinaryReader((Stream) input, Encoding.UTF8))
            this.bny = this.ReadStruct(br);
        }
      }
    }

    private void button3_Click(object sender, EventArgs e)
    {
      if (this.bny != null && !string.IsNullOrEmpty(this.textBox2.Text))
      {
        BnyUtils.WriteNewFile(this.textBox2.Text, this.bny);
        this.DialogResult = DialogResult.OK;
      }
      else
      {
        int num = (int) MessageBox.Show("请选择新结构文件，选择保存地址!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      SaveFileDialog saveFileDialog1 = new SaveFileDialog();
      saveFileDialog1.Title = "请选择保存地址";
      saveFileDialog1.FileName = this._fileName;
      SaveFileDialog saveFileDialog2 = saveFileDialog1;
      if (saveFileDialog2.ShowDialog() != DialogResult.OK)
        return;
      this.textBox2.Text = saveFileDialog2.FileName;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.label1 = new Label();
      this.label2 = new Label();
      this.textBox1 = new TextBox();
      this.textBox2 = new TextBox();
      this.button1 = new Button();
      this.button2 = new Button();
      this.button3 = new Button();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(34, 19);
      this.label1.Name = "label1";
      this.label1.Size = new Size(71, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "老版本文件:";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(22, 52);
      this.label2.Name = "label2";
      this.label2.Size = new Size(83, 12);
      this.label2.TabIndex = 1;
      this.label2.Text = "文件保存地址:";
      this.textBox1.Location = new Point(111, 16);
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new Size(260, 21);
      this.textBox1.TabIndex = 2;
      this.textBox2.Location = new Point(111, 49);
      this.textBox2.Name = "textBox2";
      this.textBox2.ReadOnly = true;
      this.textBox2.Size = new Size(260, 21);
      this.textBox2.TabIndex = 3;
      this.button1.Location = new Point(377, 15);
      this.button1.Name = "button1";
      this.button1.Size = new Size(41, 23);
      this.button1.TabIndex = 4;
      this.button1.Text = "...";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.button2.Location = new Point(377, 47);
      this.button2.Name = "button2";
      this.button2.Size = new Size(41, 23);
      this.button2.TabIndex = 5;
      this.button2.Text = "...";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.button3.Location = new Point(346, 86);
      this.button3.Name = "button3";
      this.button3.Size = new Size(75, 23);
      this.button3.TabIndex = 6;
      this.button3.Text = "保存文件";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new EventHandler(this.button3_Click);
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(433, 123);
      this.Controls.Add((Control) this.button3);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.textBox2);
      this.Controls.Add((Control) this.textBox1);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (FrmSaveNew);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "保存为老版本结构";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
