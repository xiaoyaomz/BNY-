// Decompiled with JetBrains decompiler
// Type: BnyEditor.FrmGotoLine
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
  public class FrmGotoLine : Form
  {
    private IContainer components;
    private Label label1;
    private TextBox textBox1;
    private Button button1;

    public FrmGotoLine() => this.InitializeComponent();

    public int line => int.Parse(this.textBox1.Text);

    private void button1_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.textBox1.Text))
      {
        int num = (int) MessageBox.Show("请输入行号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
        this.DialogResult = DialogResult.OK;
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
      this.textBox1 = new TextBox();
      this.button1 = new Button();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(28, 21);
      this.label1.Name = "label1";
      this.label1.Size = new Size(35, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "行号:";
      this.textBox1.Location = new Point(69, 17);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(58, 21);
      this.textBox1.TabIndex = 1;
      this.textBox1.Text = "1";
      this.textBox1.TextAlign = HorizontalAlignment.Center;
      this.button1.Location = new Point(133, 15);
      this.button1.Name = "button1";
      this.button1.Size = new Size(75, 23);
      this.button1.TabIndex = 2;
      this.button1.Text = "转到";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.AcceptButton = (IButtonControl) this.button1;
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(221, 61);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.textBox1);
      this.Controls.Add((Control) this.label1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (FrmGotoLine);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "输入要跳转到的行号";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
