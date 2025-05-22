// Decompiled with JetBrains decompiler
// Type: BnyEditor.FrmSearch
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace BnyEditor
{
  public class FrmSearch : Form
  {
    private List<int> ButtonIndex = new List<int>();
    private DataTable ResultTable;
    private IContainer components;
    private GroupBox groupBox1;
    private Button button1;
    private TextBox txtkeyword;
    private Label label2;
    private ComboBox cboCondation;
    private ComboBox cboColumn;
    private ComboBox cboFile;
    private Label label1;
    private DataGridView dataGridView1;
    private CheckBox checkBox1;

    public FrmSearch() => this.InitializeComponent();

    private void FrmSearch_Load(object sender, EventArgs e)
    {
      if (BnyCommon.SearchData != null)
      {
        foreach (KeyValuePair<string, SearchInfo> keyValuePair in BnyCommon.SearchData)
          this.cboFile.Items.Add((object) keyValuePair.Key);
        this.cboFile.Items.Insert(0, (object) "请选择");
        this.cboFile.SelectedIndex = 0;
      }
      this.cboCondation.SelectedIndex = 0;
    }

    private void cboFile_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.cboColumn.Items.Clear();
      if (this.cboFile.SelectedIndex == 0)
        return;
      try
      {
        foreach (DataColumn column in (InternalDataCollectionBase) BnyCommon.SearchData[this.cboFile.Text].MainTable.Columns)
        {
          if (column.DataType != typeof (DataTable) && column.DataType != typeof (List<DataTable>))
            this.cboColumn.Items.Add((object) new ListItem(this.Desc(column.ColumnName), (object) column.ColumnName));
        }
        if (this.cboColumn.Items.Count <= 0)
          return;
        this.cboColumn.SelectedIndex = 0;
      }
      catch
      {
      }
    }

    public string Desc(string name)
    {
      FileItem fileDesc = BnyCommon.SearchData[this.cboFile.Text].FileDesc;
      if (fileDesc == null)
        return name;
      foreach (FiledItem filedItem in fileDesc.filed)
      {
        if (filedItem.name == name)
          return filedItem.desc.Replace("\r\n", "").Replace("\n", "");
      }
      return name;
    }

    private void GenerateColumnType(DataGridView grid, DataTable dt)
    {
      this.ButtonIndex.Clear();
      grid.Columns.Clear();
      grid.AutoGenerateColumns = false;
      for (int index = 0; index < dt.Columns.Count; ++index)
      {
        if (dt.Columns[index].DataType != typeof (DataTable) && dt.Columns[index].DataType != typeof (List<DataTable>))
        {
          DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
          viewTextBoxColumn.HeaderText = this.Desc(dt.Columns[index].ColumnName);
          viewTextBoxColumn.ValueType = dt.Columns[index].GetType();
          viewTextBoxColumn.DataPropertyName = dt.Columns[index].ColumnName;
          grid.Columns.Add((DataGridViewColumn) viewTextBoxColumn);
        }
        else
        {
          DataGridViewButtonColumn viewButtonColumn = new DataGridViewButtonColumn();
          viewButtonColumn.HeaderText = this.Desc(dt.Columns[index].ColumnName);
          viewButtonColumn.UseColumnTextForButtonValue = false;
          viewButtonColumn.DefaultCellStyle.NullValue = (object) "查看";
          grid.Columns.Add((DataGridViewColumn) viewButtonColumn);
          this.ButtonIndex.Add(index);
        }
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (this.txtkeyword.Text == string.Empty)
      {
        int num1 = (int) MessageBox.Show("请输入搜索内容", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        string filterExpression = "";
        string source = this.txtkeyword.Text;
        if (this.checkBox1.Checked)
          source = ChineseStringUtility.ToTraditional(source);
        if (this.cboColumn.Items.Count == 0)
          return;
        string name = ((ListItem) this.cboColumn.SelectedItem).Value.ToString();
        bool flag = BnyCommon.SearchData[this.cboFile.Text].MainTable.Columns[name].DataType != typeof (string);
        switch (this.cboCondation.SelectedIndex)
        {
          case 0:
            filterExpression = !flag ? name + " = '" + source + "'" : name + " = " + source;
            break;
          case 1:
            if (flag)
            {
              int num2 = (int) MessageBox.Show(this.cboColumn.Text + "不能模糊查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
              return;
            }
            filterExpression = name + " like '%" + source + "%'";
            break;
          case 2:
            if (flag)
            {
              int num3 = (int) MessageBox.Show(this.cboColumn.Text + "不能模糊查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
              return;
            }
            filterExpression = name + " like '" + source + "%'";
            break;
          case 3:
            if (flag)
            {
              int num4 = (int) MessageBox.Show(this.cboColumn.Text + "不能模糊查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
              return;
            }
            filterExpression = name + " like '%" + source + "'";
            break;
          case 4:
            filterExpression = !flag ? name + ">'" + source + "'" : name + ">" + source;
            break;
          case 5:
            filterExpression = !flag ? name + "<'" + source + "'" : name + "<" + source;
            break;
          case 6:
            filterExpression = !flag ? name + "<>'" + source + "'" : name + "<>" + source;
            break;
        }
        this.ButtonIndex.Clear();
        this.dataGridView1.Columns.Clear();
        this.ResultTable = new DataTable();
        foreach (DataColumn column in (InternalDataCollectionBase) BnyCommon.SearchData[this.cboFile.Text].MainTable.Columns)
          this.ResultTable.Columns.Add(new DataColumn(column.ColumnName, column.DataType));
        foreach (DataRow dataRow in BnyCommon.SearchData[this.cboFile.Text].MainTable.Select(filterExpression))
        {
          DataRow row = this.ResultTable.NewRow();
          row.ItemArray = dataRow.ItemArray;
          this.ResultTable.Rows.Add(row);
        }
        this.ResultTable.AcceptChanges();
        this.GenerateColumnType(this.dataGridView1, this.ResultTable);
        this.dataGridView1.DataSource = (object) this.ResultTable;
      }
    }

    public StructExplain Find(string name)
    {
      foreach (StructExplain explain in BnyCommon.SearchData[this.cboFile.Text].BnyData.Explains)
      {
        if (explain.Name == name)
          return explain;
      }
      int num = 0;
      foreach (StructExplain explain in BnyCommon.SearchData[this.cboFile.Text].BnyData.Explains)
      {
        if (explain.Name + "_" + (object) num == name)
          return explain;
        if (name.IndexOf(explain.Name) > -1)
          ++num;
      }
      return (StructExplain) null;
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex == -1 || this.ButtonIndex.IndexOf(e.ColumnIndex) <= -1)
        return;
      StructExplain structExplain = this.Find(this.ResultTable.Columns[e.ColumnIndex].ColumnName);
      if (structExplain.Type == (byte) 12)
      {
        DataTable d = this.ResultTable.Rows[e.RowIndex][e.ColumnIndex] as DataTable;
        SearchInfo searchInfo = BnyCommon.SearchData[this.cboFile.Text];
        StructExplain ex = structExplain;
        SearchInfo info = searchInfo;
        int num = (int) new FrmEditStruct(d, ex, info, true).ShowDialog();
      }
      else
      {
        if (structExplain.Type != (byte) 11)
          return;
        List<DataTable> d = this.ResultTable.Rows[e.RowIndex][e.ColumnIndex] as List<DataTable>;
        SearchInfo searchInfo = BnyCommon.SearchData[this.cboFile.Text];
        StructExplain ex = structExplain;
        SearchInfo info = searchInfo;
        int num = (int) new FrmEditStruct(d, ex, info, true).ShowDialog();
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmSearch));
      this.groupBox1 = new GroupBox();
      this.button1 = new Button();
      this.txtkeyword = new TextBox();
      this.label2 = new Label();
      this.cboCondation = new ComboBox();
      this.cboColumn = new ComboBox();
      this.cboFile = new ComboBox();
      this.label1 = new Label();
      this.dataGridView1 = new DataGridView();
      this.checkBox1 = new CheckBox();
      this.groupBox1.SuspendLayout();
      ((ISupportInitialize) this.dataGridView1).BeginInit();
      this.SuspendLayout();
      this.groupBox1.Controls.Add((Control) this.checkBox1);
      this.groupBox1.Controls.Add((Control) this.button1);
      this.groupBox1.Controls.Add((Control) this.txtkeyword);
      this.groupBox1.Controls.Add((Control) this.label2);
      this.groupBox1.Controls.Add((Control) this.cboCondation);
      this.groupBox1.Controls.Add((Control) this.cboColumn);
      this.groupBox1.Controls.Add((Control) this.cboFile);
      this.groupBox1.Controls.Add((Control) this.label1);
      this.groupBox1.Location = new Point(8, 6);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(717, 67);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "搜索选项:";
      this.button1.Location = new Point(632, 26);
      this.button1.Name = "button1";
      this.button1.Size = new Size(55, 23);
      this.button1.TabIndex = 6;
      this.button1.Text = "搜索";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.txtkeyword.Location = new Point(432, 29);
      this.txtkeyword.Name = "txtkeyword";
      this.txtkeyword.Size = new Size(100, 21);
      this.txtkeyword.TabIndex = 5;
      this.label2.AutoSize = true;
      this.label2.Location = new Point(391, 33);
      this.label2.Name = "label2";
      this.label2.Size = new Size(35, 12);
      this.label2.TabIndex = 4;
      this.label2.Text = "内容:";
      this.cboCondation.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboCondation.FormattingEnabled = true;
      this.cboCondation.Items.AddRange(new object[7]
      {
        (object) "完全等于",
        (object) "内容包含",
        (object) "开头等于",
        (object) "结尾等于",
        (object) "大于",
        (object) "小于",
        (object) "不等于"
      });
      this.cboCondation.Location = new Point(281, 29);
      this.cboCondation.Name = "cboCondation";
      this.cboCondation.Size = new Size(103, 20);
      this.cboCondation.TabIndex = 3;
      this.cboColumn.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboColumn.FormattingEnabled = true;
      this.cboColumn.Location = new Point(156, 29);
      this.cboColumn.Name = "cboColumn";
      this.cboColumn.Size = new Size(121, 20);
      this.cboColumn.TabIndex = 2;
      this.cboFile.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboFile.FormattingEnabled = true;
      this.cboFile.Location = new Point(56, 29);
      this.cboFile.Name = "cboFile";
      this.cboFile.Size = new Size(94, 20);
      this.cboFile.TabIndex = 1;
      this.cboFile.SelectedIndexChanged += new EventHandler(this.cboFile_SelectedIndexChanged);
      this.label1.AutoSize = true;
      this.label1.Location = new Point(17, 33);
      this.label1.Name = "label1";
      this.label1.Size = new Size(41, 12);
      this.label1.TabIndex = 0;
      this.label1.Text = "搜索：";
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.AllowUserToResizeRows = false;
      this.dataGridView1.BackgroundColor = Color.White;
      this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Location = new Point(11, 83);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.RowHeadersVisible = false;
      this.dataGridView1.RowTemplate.Height = 23;
      this.dataGridView1.Size = new Size(714, 304);
      this.dataGridView1.TabIndex = 1;
      this.dataGridView1.CellContentClick += new DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
      this.checkBox1.AutoSize = true;
      this.checkBox1.Location = new Point(542, 31);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new Size(84, 16);
      this.checkBox1.TabIndex = 7;
      this.checkBox1.Text = "简体转繁体";
      this.checkBox1.UseVisualStyleBackColor = true;
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(730, 397);
      this.Controls.Add((Control) this.dataGridView1);
      this.Controls.Add((Control) this.groupBox1);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Icon = this.Icon;
      this.MaximizeBox = false;
      this.Name = nameof (FrmSearch);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "搜索内容";
      this.Load += new EventHandler(this.FrmSearch_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((ISupportInitialize) this.dataGridView1).EndInit();
      this.ResumeLayout(false);
    }
  }
}
