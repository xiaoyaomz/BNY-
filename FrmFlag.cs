// Decompiled with JetBrains decompiler
// Type: BnyEditor.FrmFlag
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
  public class FrmFlag : Form
  {
    private int col;
    private int row;
    private string columnName;
    private string value;
    private IContainer components;
    private GroupBox groupBox1;
    private RadioButton radioButton2;
    private RadioButton radioButton1;
    private TextBox textBox1;
    private Label label1;
    private Button button1;

    public FrmFlag(int c, int r, string name, string val)
    {
      this.col = c;
      this.row = r;
      this.columnName = name;
      this.value = val;
      this.InitializeComponent();
      FlagInfo flagInfo = FlagUtils.getFlagInfo(this.columnName, val, this.col, this.row);
      if (flagInfo == null)
        return;
      this.textBox1.Text = flagInfo.FlagName;
      if (!flagInfo.isValueType)
        this.radioButton2.Checked = true;
      else
        this.radioButton1.Checked = true;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.textBox1.Text))
      {
        int num1 = (int) MessageBox.Show("请输入标记名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else if (!FlagUtils.AddOrEditFlag(new FlagInfo()
      {
        isValueType = this.radioButton1.Checked,
        ColumnName = this.columnName,
        Value = this.value,
        ColumnIndex = this.col,
        RowIndex = this.row,
        FlagName = this.textBox1.Text
      }))
      {
        int num2 = (int) MessageBox.Show("添加保存标记失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else if (FlagUtils.WriteFlags())
      {
        this.DialogResult = DialogResult.OK;
      }
      else
      {
        int num3 = (int) MessageBox.Show("写入标记失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmFlag));
      this.groupBox1 = new GroupBox();
      this.label1 = new Label();
      this.textBox1 = new TextBox();
      this.radioButton1 = new RadioButton();
      this.radioButton2 = new RadioButton();
      this.button1 = new Button();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      this.groupBox1.Controls.Add((Control) this.radioButton2);
      this.groupBox1.Controls.Add((Control) this.radioButton1);
      this.groupBox1.Controls.Add((Control) this.textBox1);
      this.groupBox1.Controls.Add((Control) this.label1);
      this.groupBox1.Location = new Point(5, 2);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(328, 114);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(13, 28);
      this.label1.Name = "label1";
      this.label1.Size = new Size(59, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "标记名称:";
      this.textBox1.Location = new Point(78, 25);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(229, 21);
      this.textBox1.TabIndex = 1;
      this.radioButton1.AutoSize = true;
      this.radioButton1.Checked = true;
      this.radioButton1.Location = new Point(78, 62);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new Size(215, 16);
      this.radioButton1.TabIndex = 2;
      this.radioButton1.TabStop = true;
      this.radioButton1.Text = "值类型(所有相同值使用同一个标记)";
      this.radioButton1.UseVisualStyleBackColor = true;
      this.radioButton2.AutoSize = true;
      this.radioButton2.Location = new Point(78, 84);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.Size = new Size(215, 16);
      this.radioButton2.TabIndex = 3;
      this.radioButton2.Text = "位置类型(根据行列的位置进行标记)";
      this.radioButton2.UseVisualStyleBackColor = true;
      this.button1.Location = new Point(235, 123);
      this.button1.Name = "button1";
      this.button1.Size = new Size(93, 30);
      this.button1.TabIndex = 1;
      this.button1.Text = "添加标记(&A)";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(339, 161);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.groupBox1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Icon = this.Icon;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (FrmFlag);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "添加标记";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
