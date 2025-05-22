// Decompiled with JetBrains decompiler
// Type: BnyEditor.FrmI2F
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace BnyEditor
{
  public class FrmI2F : Form
  {
    private IContainer components;
    private Button button2;
    private Button button1;
    private TextBox textBox2;
    private TextBox textBox1;
    private Label label2;
    private Label label1;

    public FrmI2F() => this.InitializeComponent();

    public FrmI2F(int val)
    {
      this.InitializeComponent();
      this.textBox1.Text = val.ToString();
      this.button2_Click((object) null, (EventArgs) null);
    }

    private void button2_Click(object sender, EventArgs e)
    {
      try
      {
        this.textBox2.Text = BitConverter.ToSingle(BitConverter.GetBytes(int.Parse(this.textBox1.Text)), 0).ToString();
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      try
      {
        this.textBox2.Text = BitConverter.ToInt32(BitConverter.GetBytes(float.Parse(this.textBox1.Text)), 0).ToString();
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmI2F));
      this.button2 = new Button();
      this.button1 = new Button();
      this.textBox2 = new TextBox();
      this.textBox1 = new TextBox();
      this.label2 = new Label();
      this.label1 = new Label();
      this.SuspendLayout();
      this.button2.Location = new Point(65, 89);
      this.button2.Name = "button2";
      this.button2.Size = new Size(75, 23);
      this.button2.TabIndex = 11;
      this.button2.Text = "转浮点";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.button1.Location = new Point(146, 89);
      this.button1.Name = "button1";
      this.button1.Size = new Size(75, 23);
      this.button1.TabIndex = 10;
      this.button1.Text = "转整形";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.textBox2.Location = new Point(98, 55);
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new Size(123, 21);
      this.textBox2.TabIndex = 9;
      this.textBox1.Location = new Point(98, 12);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(123, 21);
      this.textBox1.TabIndex = 8;
      this.label2.AutoSize = true;
      this.label2.Location = new Point(18, 58);
      this.label2.Name = "label2";
      this.label2.Size = new Size(71, 12);
      this.label2.TabIndex = 7;
      this.label2.Text = "转换后数值:";
      this.label1.AutoSize = true;
      this.label1.Location = new Point(30, 15);
      this.label1.Name = "label1";
      this.label1.Size = new Size(59, 12);
      this.label1.TabIndex = 6;
      this.label1.Text = "输入数值:";
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(248, (int) sbyte.MaxValue);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.textBox2);
      this.Controls.Add((Control) this.textBox1);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Icon = this.Icon;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (FrmI2F);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "数值转换";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
