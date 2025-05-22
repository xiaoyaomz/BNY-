// Decompiled with JetBrains decompiler
// Type: BnyEditor.FrmDescEditor
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace BnyEditor
{
  public class FrmDescEditor : Form
  {
    private string FileName;
    private IContainer components;
    private TextBox textBox1;
    private Label label1;
    private Button button1;
    private DataGridView dataGridView1;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel toolStripStatusLabel1;

    public FrmDescEditor(string name)
    {
      this.InitializeComponent();
      if (name.IndexOf(".") > -1)
        this.FileName = name.Substring(name.LastIndexOf(".")).Replace(".", "");
      else
        this.FileName = name;
    }

    private void AddFiled(StructExplain se, DataTable dt)
    {
      int num = 0;
      foreach (StructExplain se1 in se.Structs)
      {
        DataRow row = dt.NewRow();
        if (BnyCommon.FileDesc != null)
        {
          row[0] = (object) se1.Name;
          row[1] = (object) se1.Name.Desc();
          row[2] = (object) se1.Name.Tips();
          dt.Rows.Add(row);
          dt.AcceptChanges();
          ++num;
        }
        else
        {
          row[0] = (object) se1.Name;
          row[1] = (object) "";
          row[2] = (object) "";
          dt.Rows.Add(row);
          dt.AcceptChanges();
        }
        if (se1.Structs != null)
          this.AddFiled(se1, dt);
      }
    }

    private void GeneratteTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("名称");
      dt.Columns.Add("说明");
      dt.Columns.Add("提示");
      dt.Columns[0].ReadOnly = true;
      int num = 0;
      foreach (StructExplain explain in BnyCommon.BnyData.Explains)
      {
        DataRow row = dt.NewRow();
        if (BnyCommon.FileDesc != null)
        {
          row[0] = (object) explain.Name;
          row[1] = (object) explain.Name.Desc();
          row[2] = (object) explain.Name.Tips();
          dt.Rows.Add(row);
          ++num;
        }
        else
        {
          row[0] = (object) explain.Name;
          row[1] = (object) "";
          row[2] = (object) "";
          dt.Rows.Add(row);
        }
        if (explain.Structs != null)
          this.AddFiled(explain, dt);
        dt.AcceptChanges();
      }
      if (BnyCommon.FileDesc != null)
        this.textBox1.Text = BnyCommon.FileDesc.fileDesc;
      this.dataGridView1.DataSource = (object) dt;
    }

    private void FrmDescEditor_Load(object sender, EventArgs e) => this.GeneratteTable();

    private void button1_Click(object sender, EventArgs e)
    {
      RootData rootData = (RootData) null;
      string empty = string.Empty;
      string path = AppDomain.CurrentDomain.BaseDirectory + "\\data.desc";
      if (File.Exists(path))
        rootData = JsonConvert.DeserializeObject<RootData>(EncryptHelper.DESDecrypt(File.ReadAllText(path, Encoding.UTF8)));
      if (rootData == null)
        rootData = new RootData();
      if (rootData.datas == null)
        rootData.datas = new List<FileItem>();
      FileItem fileItem = BnyCommon.FileDesc == null ? new FileItem() : BnyCommon.FileDesc;
      fileItem.name = this.FileName;
      fileItem.fileDesc = this.textBox1.Text;
      fileItem.filed = new List<FiledItem>();
      for (int index = 0; index < this.dataGridView1.Rows.Count; ++index)
      {
        FiledItem filedItem = new FiledItem()
        {
          name = this.dataGridView1.Rows[index].Cells[0].Value.ToString()
        };
        filedItem.desc = !(this.dataGridView1.Rows[index].Cells[1].Value.ToString() == "") ? this.dataGridView1.Rows[index].Cells[1].Value.ToString() : filedItem.name;
        filedItem.tip = this.dataGridView1.Rows[index].Cells[2].Value.ToString();
        fileItem.filed.Add(filedItem);
      }
      if (BnyCommon.FileDesc == null)
        rootData.datas.Add(fileItem);
      else if (!string.IsNullOrEmpty(BnyCommon.CurrFileName))
      {
        for (int index = 0; index < rootData.datas.Count; ++index)
        {
          if (rootData.datas[index].name.ToLower() == BnyCommon.CurrFileName.ToLower())
          {
            rootData.datas[index] = fileItem;
            break;
          }
        }
      }
      string contents = EncryptHelper.DESEncrypt(JsonConvert.SerializeObject((object) rootData));
      File.WriteAllText(path, contents, Encoding.UTF8);
      BnyCommon.Root = rootData;
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmDescEditor));
      this.textBox1 = new TextBox();
      this.label1 = new Label();
      this.button1 = new Button();
      this.dataGridView1 = new DataGridView();
      this.statusStrip1 = new StatusStrip();
      this.toolStripStatusLabel1 = new ToolStripStatusLabel();
      ((ISupportInitialize) this.dataGridView1).BeginInit();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      this.textBox1.Location = new Point(14, 468);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(387, 21);
      this.textBox1.TabIndex = 7;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(12, 453);
      this.label1.Name = "label1";
      this.label1.Size = new Size(239, 12);
      this.label1.TabIndex = 6;
      this.label1.Text = "自定义显示文件的说明，比如【物品编辑】:";
      this.button1.Location = new Point(407, 467);
      this.button1.Name = "button1";
      this.button1.Size = new Size(75, 23);
      this.button1.TabIndex = 5;
      this.button1.Text = "保存";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.BackgroundColor = SystemColors.ButtonHighlight;
      this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Dock = DockStyle.Top;
      this.dataGridView1.Location = new Point(0, 0);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.RowTemplate.Height = 23;
      this.dataGridView1.Size = new Size(493, 438);
      this.dataGridView1.TabIndex = 4;
      this.statusStrip1.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.toolStripStatusLabel1
      });
      this.statusStrip1.Location = new Point(0, 495);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.RenderMode = ToolStripRenderMode.Professional;
      this.statusStrip1.Size = new Size(493, 22);
      this.statusStrip1.SizingGrip = false;
      this.statusStrip1.TabIndex = 8;
      this.statusStrip1.Text = "statusStrip1";
      this.toolStripStatusLabel1.Font = new Font("微软雅黑", 9f, FontStyle.Bold);
      this.toolStripStatusLabel1.ForeColor = Color.Red;
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new Size(478, 17);
      this.toolStripStatusLabel1.Spring = true;
      this.toolStripStatusLabel1.Text = "请注意：保存后会自动重新加载文件，在编辑自定义提示前，请保存好文件!!!!";
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(493, 517);
      this.Controls.Add((Control) this.statusStrip1);
      this.Controls.Add((Control) this.dataGridView1);
      this.Controls.Add((Control) this.textBox1);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.button1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Icon = this.Icon;
      this.MaximizeBox = false;
      this.Name = nameof (FrmDescEditor);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "文件字段说明编辑器";
      this.Load += new EventHandler(this.FrmDescEditor_Load);
      ((ISupportInitialize) this.dataGridView1).EndInit();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
