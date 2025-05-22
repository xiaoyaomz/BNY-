// Decompiled with JetBrains decompiler
// Type: BnyEditor.FrmBatchReplace
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace BnyEditor
{
  public class FrmBatchReplace : Form
  {
    private IContainer components;
    private Label label1;
    private ComboBox comboBox1;
    private Label label2;
    private TextBox textBox1;
    private Label label3;
    private Button button1;
    private Button button2;

    public int LineNumber => int.Parse(this.textBox1.Text);

    public int ColumnNmae => this.comboBox1.SelectedIndex;

    public FrmBatchReplace(DataTable dt)
    {
      this.InitializeComponent();
      this.comboBox1.Items.Clear();
      foreach (DataColumn column in (InternalDataCollectionBase) dt.Columns)
        this.comboBox1.Items.Add((object) new ListItem(column.ColumnName.Desc(), (object) column.ColumnName));
      this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
      if (this.comboBox1.Items.Count <= 0)
        return;
      this.comboBox1.SelectedIndex = 0;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.textBox1.Text))
      {
        int num = (int) MessageBox.Show("请输入要批量替换为哪儿一行的行号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmBatchReplace));
      this.label1 = new Label();
      this.comboBox1 = new ComboBox();
      this.label2 = new Label();
      this.textBox1 = new TextBox();
      this.label3 = new Label();
      this.button1 = new Button();
      this.button2 = new Button();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(30, 26);
      this.label1.Name = "label1";
      this.label1.Size = new Size(71, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "选择替换列:";
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Location = new Point(116, 23);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new Size(180, 20);
      this.comboBox1.TabIndex = 1;
      this.label2.AutoSize = true;
      this.label2.Location = new Point(18, 62);
      this.label2.Name = "label2";
      this.label2.Size = new Size(83, 12);
      this.label2.TabIndex = 2;
      this.label2.Text = "替换为与行号:";
      this.textBox1.Location = new Point(116, 58);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(121, 21);
      this.textBox1.TabIndex = 3;
      this.label3.AutoSize = true;
      this.label3.Location = new Point(243, 61);
      this.label3.Name = "label3";
      this.label3.Size = new Size(53, 12);
      this.label3.TabIndex = 4;
      this.label3.Text = "相同的值";
      this.button1.Location = new Point(162, 101);
      this.button1.Name = "button1";
      this.button1.Size = new Size(75, 23);
      this.button1.TabIndex = 5;
      this.button1.Text = "确定";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.button2.DialogResult = DialogResult.Cancel;
      this.button2.Location = new Point(245, 101);
      this.button2.Name = "button2";
      this.button2.Size = new Size(75, 23);
      this.button2.TabIndex = 6;
      this.button2.Text = "取消";
      this.button2.UseVisualStyleBackColor = true;
      this.AcceptButton = (IButtonControl) this.button1;
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.button2;
      this.ClientSize = new Size(334, 136);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.textBox1);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.comboBox1);
      this.Controls.Add((Control) this.label1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Icon = this.Icon;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (FrmBatchReplace);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "批量替换";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
