// Decompiled with JetBrains decompiler
// Type: BnyEditor.FrmRegister
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
  public class FrmRegister : Form
  {
    private IContainer components;
    private Label label2;
    private Label label4;
    private Button button3;
    private Button button1;
    private TextBox textBox1;
    private Label label1;

    public FrmRegister()
    {
      this.InitializeComponent();
      this.textBox1.Text = BnyCommon.machineCode;
    }

    private void button1_Click(object sender, EventArgs e) => Clipboard.SetText(this.textBox1.Text);

    private void button3_Click(object sender, EventArgs e)
    {
      Application.Exit();
      Environment.Exit(0);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.label2 = new Label();
      this.label4 = new Label();
      this.button3 = new Button();
      this.button1 = new Button();
      this.textBox1 = new TextBox();
      this.label1 = new Label();
      this.SuspendLayout();
      this.label2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.label2.ForeColor = Color.Red;
      this.label2.Location = new Point(34, 88);
      this.label2.Name = "label2";
      this.label2.Size = new Size(380, 18);
      this.label2.TabIndex = 16;
      this.label2.Text = "请将上面的机器码和用户名，以及本软件的名称，发送给群主进行注册";
      this.label4.AutoSize = true;
      this.label4.Font = new Font("微软雅黑", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 134);
      this.label4.ForeColor = Color.Blue;
      this.label4.Location = new Point(12, 12);
      this.label4.Name = "label4";
      this.label4.Size = new Size(122, 22);
      this.label4.TabIndex = 15;
      this.label4.Text = "出尔反尔默念狗";
      this.button3.Location = new Point(340, 121);
      this.button3.Name = "button3";
      this.button3.Size = new Size(84, 34);
      this.button3.TabIndex = 13;
      this.button3.Text = "关闭";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new EventHandler(this.button3_Click);
      this.button1.Location = new Point(376, 46);
      this.button1.Name = "button1";
      this.button1.Size = new Size(48, 23);
      this.button1.TabIndex = 12;
      this.button1.Text = "复制";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.textBox1.Location = new Point(88, 48);
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new Size(284, 21);
      this.textBox1.TabIndex = 11;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(11, 52);
      this.label1.Name = "label1";
      this.label1.Size = new Size(71, 12);
      this.label1.TabIndex = 10;
      this.label1.Text = "您的机器码:";
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(435, 167);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.button3);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.textBox1);
      this.Controls.Add((Control) this.label1);
      this.Icon = this.Icon;
      this.Name = nameof (FrmRegister);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "软件注册";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
