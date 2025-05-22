// Decompiled with JetBrains decompiler
// Type: BnyEditor.FrmEditStruct
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace BnyEditor
{
  public class FrmEditStruct : Form
  {
    private List<DataTable> dtcoll;
    private StructExplain se;
    private SearchInfo fileSearch;
    private bool _isFind;
    private List<int> ButtonIndex = new List<int>();
    private bool bflag;
    private int delcount;
    private string tableSort = string.Empty;
    private IContainer components;
    private ComboBox comboBox1;
    private DataGridView dataGridView1;
    private Button button3;
    private Button btnNew;
    private Button btnDelete;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem clipcopy;
    private ToolStripSeparator toolStripMenuItem8;
    private ToolStripMenuItem pastcover;
    private ToolStripMenuItem pastenew;
    private ToolStripSeparator toolStripMenuItem10;
    private ToolStripMenuItem deleteselect;
    private ToolStripMenuItem toolStripMenuItem1;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem batchReplace;
    private ToolStripSeparator toolStripSeparator2;
    private ToolTip toolTip1;
    private ToolStripSeparator toolStripMenuItem2;
    private ToolStripMenuItem 显示标记ToolStripMenuItem;
    private ToolStripMenuItem 添加编辑标记ToolStripMenuItem;
    private ToolStripMenuItem 删除标记ToolStripMenuItem;
    private ToolStripSeparator toolStripMenuItem3;
    private ToolStripMenuItem showdatamenuitem;

    public List<DataTable> DataTableColl => this.dtcoll;

    public FrmEditStruct() => this.InitializeComponent();

    public string Desc(string name)
    {
      if (this.fileSearch != null)
      {
        FileItem fileDesc = this.fileSearch.FileDesc;
        if (fileDesc != null)
        {
          foreach (FiledItem filedItem in fileDesc.filed)
          {
            if (filedItem.name == name)
              return filedItem.desc.Replace("\r\n", "").Replace("\n", "");
          }
          return name;
        }
      }
      return name;
    }

    public string Tips(string name)
    {
      if (this.fileSearch != null)
      {
        FileItem fileDesc = this.fileSearch.FileDesc;
        if (fileDesc != null)
        {
          foreach (FiledItem filedItem in fileDesc.filed)
          {
            if (filedItem.name == name)
              return filedItem.tip.ToLower().Replace("<br>", "\r\n");
          }
          return name;
        }
      }
      return name;
    }

    public FrmEditStruct(List<DataTable> d, StructExplain ex)
    {
      this.InitializeComponent();
      this.dtcoll = d;
      this.se = ex;
      this.comboBox1.Items.Clear();
      foreach (DataTable dataTable in d)
        this.comboBox1.Items.Add((object) new ListItem(dataTable.TableName.Desc(), (object) dataTable.TableName));
      if (this.comboBox1.Items.Count <= 0)
        return;
      this.comboBox1.SelectedIndex = 0;
    }

    public FrmEditStruct(List<DataTable> d, StructExplain ex, SearchInfo info, bool isFind)
    {
      this.InitializeComponent();
      this.dtcoll = d;
      this.se = ex;
      this._isFind = isFind;
      this.fileSearch = info;
      this.comboBox1.Items.Clear();
      this.dataGridView1.Height = 467;
      foreach (DataTable dataTable in d)
        this.comboBox1.Items.Add((object) new ListItem(this.Desc(dataTable.TableName), (object) dataTable.TableName));
      if (this.comboBox1.Items.Count <= 0)
        return;
      this.comboBox1.SelectedIndex = 0;
    }

    public FrmEditStruct(DataTable d, StructExplain ex, SearchInfo info, bool isFind)
    {
      this.InitializeComponent();
      this.dtcoll = new List<DataTable>();
      this._isFind = isFind;
      this.fileSearch = info;
      this.dtcoll.Add(d);
      this.se = ex;
      this.comboBox1.Items.Clear();
      this.dataGridView1.Height = 467;
      this.comboBox1.Items.Add((object) new ListItem(this.Desc(d.TableName), (object) d.TableName.Desc()));
      this.comboBox1.SelectedIndex = 0;
    }

    public FrmEditStruct(DataTable d, StructExplain ex)
    {
      this.InitializeComponent();
      this.dtcoll = new List<DataTable>();
      this.dtcoll.Add(d);
      this.se = ex;
      this.comboBox1.Items.Clear();
      this.comboBox1.Items.Add((object) new ListItem(d.TableName.Desc(), (object) d.TableName.Desc()));
      this.comboBox1.SelectedIndex = 0;
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
          DataGridViewTextBoxColumnEx viewTextBoxColumnEx = new DataGridViewTextBoxColumnEx();
          if (!this._isFind)
            viewTextBoxColumnEx.HeaderText = dt.Columns[index].ColumnName.Desc();
          else
            viewTextBoxColumnEx.HeaderText = this.Desc(dt.Columns[index].ColumnName);
          viewTextBoxColumnEx.ValueType = dt.Columns[index].GetType();
          viewTextBoxColumnEx.Name = dt.Columns[index].ColumnName;
          viewTextBoxColumnEx.DataPropertyName = dt.Columns[index].ColumnName;
          viewTextBoxColumnEx.ToolTipText = this.Tips(dt.Columns[index].ColumnName);
          grid.Columns.Add((DataGridViewColumn) viewTextBoxColumnEx);
        }
        else
        {
          DataGridViewButtonColumn viewButtonColumn = new DataGridViewButtonColumn();
          if (!this._isFind)
            viewButtonColumn.HeaderText = dt.Columns[index].ColumnName.Desc();
          else
            viewButtonColumn.HeaderText = this.Desc(dt.Columns[index].ColumnName);
          viewButtonColumn.UseColumnTextForButtonValue = false;
          if (!this._isFind)
            viewButtonColumn.DefaultCellStyle.NullValue = (object) "编辑";
          else
            viewButtonColumn.DefaultCellStyle.NullValue = (object) "查看";
          grid.Columns.Add((DataGridViewColumn) viewButtonColumn);
          this.ButtonIndex.Add(index);
        }
      }
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      DataTable dt = this.dtcoll[this.comboBox1.SelectedIndex];
      this.GenerateColumnType(this.dataGridView1, dt);
      this.dataGridView1.DataSource = (object) dt;
    }

    private DataRow CreateRow()
    {
      DataRow row = this.dtcoll[this.comboBox1.SelectedIndex].NewRow();
      foreach (DataColumn column in (InternalDataCollectionBase) row.Table.Columns)
      {
        if (column.DataType == typeof (int) || column.DataType == typeof (byte))
          row[column.ColumnName] = (object) 0;
        else if (column.DataType == typeof (DataTable))
        {
          StructExplain se = BnyUtils.Find(this.se, column.ColumnName);
          if (se != null)
            row[column.ColumnName] = (object) BnyUtils.GenerateSubTable(se);
        }
        else if (column.DataType == typeof (List<DataTable>))
        {
          StructExplain structExplain = BnyUtils.Find(this.se, column.ColumnName);
          if (structExplain != null)
          {
            row[column.ColumnName] = (object) new List<DataTable>();
            if (structExplain.Structs != null)
            {
              foreach (StructExplain se in structExplain.Structs)
                (row[column.ColumnName] as List<DataTable>).Add(BnyUtils.GenerateSubTable(se));
            }
          }
        }
        else
          row[column.ColumnName] = (object) "";
      }
      return row;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      DataTable dataTable = this.dtcoll[this.comboBox1.SelectedIndex];
      DataRow row = this.CreateRow();
      dataTable.Rows.Add(row);
      dataTable.AcceptChanges();
      this.dataGridView1.DataSource = (object) dataTable;
      this.dataGridView1.Sort(this.dataGridView1.Columns[0], ListSortDirection.Descending);
      this.dataGridView1.CurrentCell = this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[0];
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex == -1 || this.ButtonIndex.IndexOf(e.ColumnIndex) <= -1)
        return;
      DataTable dataTable = this.dtcoll[this.comboBox1.SelectedIndex];
      StructExplain structExplain = BnyUtils.Find(this.se, dataTable.Columns[e.ColumnIndex].ColumnName);
      if (structExplain.Type == (byte) 12)
      {
        if (this._isFind)
        {
          DataTable d = dataTable.Rows[e.RowIndex][e.ColumnIndex] as DataTable;
          SearchInfo fileSearch = this.fileSearch;
          StructExplain ex = structExplain;
          SearchInfo info = fileSearch;
          FrmEditStruct frmEditStruct = new FrmEditStruct(d, ex, info, true);
          int num = (int) frmEditStruct.ShowDialog();
          dataTable.Rows[e.RowIndex][e.ColumnIndex] = (object) frmEditStruct.DataTableColl[0];
        }
        else
        {
          FrmEditStruct frmEditStruct = new FrmEditStruct(dataTable.Rows[e.RowIndex][e.ColumnIndex] as DataTable, this.se);
          if (frmEditStruct.ShowDialog() != DialogResult.OK)
            return;
          dataTable.Rows[e.RowIndex][e.ColumnIndex] = (object) frmEditStruct.DataTableColl[0];
          dataTable.AcceptChanges();
        }
      }
      else
      {
        if (structExplain.Type != (byte) 11)
          return;
        if (!this._isFind)
        {
          FrmEditStruct frmEditStruct = new FrmEditStruct(dataTable.Rows[e.RowIndex][e.ColumnIndex] as List<DataTable>, this.se);
          if (frmEditStruct.ShowDialog() != DialogResult.OK)
            return;
          dataTable.Rows[e.RowIndex][e.ColumnIndex] = (object) frmEditStruct.DataTableColl;
          dataTable.AcceptChanges();
        }
        else
        {
          List<DataTable> d = dataTable.Rows[e.RowIndex][e.ColumnIndex] as List<DataTable>;
          SearchInfo fileSearch = this.fileSearch;
          StructExplain ex = structExplain;
          SearchInfo info = fileSearch;
          FrmEditStruct frmEditStruct = new FrmEditStruct(d, ex, info, true);
          int num = (int) frmEditStruct.ShowDialog();
          dataTable.Rows[e.RowIndex][e.ColumnIndex] = (object) frmEditStruct.DataTableColl;
        }
      }
    }

    private void button4_Click(object sender, EventArgs e)
    {
      if (this.dataGridView1.SelectedRows.Count == 0 || MessageBox.Show("您确定要删除吗?", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      List<int> source = new List<int>();
      for (int index = this.dataGridView1.SelectedRows.Count - 1; index >= 0; --index)
        source.Add(this.dataGridView1.SelectedRows[index].Index);
      int index1 = source[0] - 1;
      this.dtcoll[this.comboBox1.SelectedIndex].DefaultView.Sort = this.tableSort;
      this.dtcoll[this.comboBox1.SelectedIndex] = this.dtcoll[this.comboBox1.SelectedIndex].DefaultView.ToTable();
      foreach (int index2 in (IEnumerable<int>) source.OrderByDescending<int, int>((System.Func<int, int>) (x => x)))
        this.dtcoll[this.comboBox1.SelectedIndex].Rows.RemoveAt(index2);
      this.dtcoll[this.comboBox1.SelectedIndex].AcceptChanges();
      if (index1 < 0 && this.dtcoll[this.comboBox1.SelectedIndex].Rows.Count > 0)
        index1 = 0;
      BnyCommon.MainTable.AcceptChanges();
      object sortedColumn = (object) this.dataGridView1.SortedColumn;
      SortOrder sortOrder = this.dataGridView1.SortOrder;
      this.dataGridView1.DataSource = (object) this.dtcoll[this.comboBox1.SelectedIndex];
      BnyCommon.MainTable.AcceptChanges();
      if (sortedColumn != null)
      {
        switch (sortOrder)
        {
          case SortOrder.None:
          case SortOrder.Ascending:
            this.dataGridView1.Sort(this.dataGridView1.Columns[((DataGridViewColumn) sortedColumn).Name], ListSortDirection.Ascending);
            break;
          case SortOrder.Descending:
            this.dataGridView1.Sort(this.dataGridView1.Columns[((DataGridViewColumn) sortedColumn).Name], ListSortDirection.Descending);
            break;
        }
      }
      this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index1].Cells[0];
      this.InvalidFlag();
    }

    private void button3_Click(object sender, EventArgs e)
    {
      BnyCommon.MainTable.AcceptChanges();
      this.InvalidFlag();
      this.DialogResult = DialogResult.OK;
    }

    private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
      int num = (int) MessageBox.Show(this.dtcoll[this.comboBox1.SelectedIndex].Columns[e.ColumnIndex].ColumnName.Desc() + "格式不正确,请您重新输入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            string s = (e.RowIndex + 1).ToString();
            StringFormat format = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // 计算布局矩形
            Rectangle layoutRectangle = new Rectangle(
                e.RowBounds.Left,
                e.RowBounds.Top,
                dataGridView.RowHeadersWidth,
                e.RowBounds.Height
            );

            // 绘制行号
            e.Graphics.DrawString(s, this.Font, SystemBrushes.ControlText, (RectangleF)layoutRectangle, format);
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
      if (this.bflag)
      {
        ++this.delcount;
        if (this.delcount == this.dataGridView1.SelectedRows.Count)
        {
          this.bflag = false;
          this.delcount = 0;
        }
        e.Cancel = true;
      }
      else if (MessageBox.Show("您确定要删除选中的数据吗?", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      {
        List<int> source = new List<int>();
        for (int index = this.dataGridView1.SelectedRows.Count - 1; index >= 0; --index)
          source.Add(this.dataGridView1.SelectedRows[index].Index);
        int index1 = source[0] - 1;
        this.dtcoll[this.comboBox1.SelectedIndex].DefaultView.Sort = this.tableSort;
        this.dtcoll[this.comboBox1.SelectedIndex] = this.dtcoll[this.comboBox1.SelectedIndex].DefaultView.ToTable();
        foreach (int index2 in (IEnumerable<int>) source.OrderByDescending<int, int>((System.Func<int, int>) (x => x)))
          this.dtcoll[this.comboBox1.SelectedIndex].Rows.RemoveAt(index2);
        this.dtcoll[this.comboBox1.SelectedIndex].AcceptChanges();
        if (index1 < 0 && this.dtcoll[this.comboBox1.SelectedIndex].Rows.Count > 0)
          index1 = 0;
        BnyCommon.MainTable.AcceptChanges();
        object sortedColumn = (object) this.dataGridView1.SortedColumn;
        SortOrder sortOrder = this.dataGridView1.SortOrder;
        this.dataGridView1.DataSource = (object) this.dtcoll[this.comboBox1.SelectedIndex];
        BnyCommon.MainTable.AcceptChanges();
        if (sortedColumn != null)
        {
          switch (sortOrder)
          {
            case SortOrder.None:
            case SortOrder.Ascending:
              this.dataGridView1.Sort(this.dataGridView1.Columns[((DataGridViewColumn) sortedColumn).Name], ListSortDirection.Ascending);
              break;
            case SortOrder.Descending:
              this.dataGridView1.Sort(this.dataGridView1.Columns[((DataGridViewColumn) sortedColumn).Name], ListSortDirection.Descending);
              break;
          }
        }
        this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index1].Cells[0];
        this.InvalidFlag();
        e.Cancel = true;
      }
      else
      {
        ++this.delcount;
        this.bflag = true;
        e.Cancel = true;
      }
    }

    private void clipcopy_Click(object sender, EventArgs e)
    {
      if (this.dataGridView1.GetClipboardContent() != null)
      {
        string[] strArray = this.dataGridView1.GetClipboardContent().GetText().Split(new string[1]
        {
          "\r\n"
        }, StringSplitOptions.RemoveEmptyEntries);
        string text = string.Empty;
        foreach (string str in strArray)
          text = !(str.Substring(0, 1) == "\t") ? text + "\r\n" + str : text + "\r\n" + str.Substring(1);
        if (text.Length > 0)
          text = text.Substring(2);
        Clipboard.SetText(text);
      }
      else
        Clipboard.SetText(this.dataGridView1.CurrentCell.Value.ToString());
    }

    private void deleteselect_Click(object sender, EventArgs e)
    {
      this.button4_Click(sender, (EventArgs) null);
    }

    private bool CheckVal(int colindex, string data)
    {
      DataTable dataTable = this.dtcoll[this.comboBox1.SelectedIndex];
      if (dataTable.Columns[colindex].DataType == typeof (int))
        return int.TryParse(data, out int _);
      return !(dataTable.Columns[colindex].DataType == typeof (byte)) || byte.TryParse(data, out byte _);
    }

    private void pastcover_Click(object sender, EventArgs e)
    {
      try
      {
        string text1 = Clipboard.GetText();
        if (string.IsNullOrEmpty(text1))
          return;
        string[] strArray1 = text1.Split(new string[1]
        {
          "\r\n"
        }, StringSplitOptions.RemoveEmptyEntries);
        int index1 = -1;
        int index2 = -1;
        bool flag1 = false;
        foreach (DataGridViewRow row in (IEnumerable) this.dataGridView1.Rows)
        {
          for (int index3 = 0; index3 < row.Cells.Count; ++index3)
          {
            if (row.Cells[index3].Selected)
            {
              index1 = row.Index;
              index2 = index3;
              flag1 = true;
              break;
            }
          }
          if (flag1)
            break;
        }
        if (index1 == -1 || index2 == -1)
          return;
        DataTable dataTable = this.dtcoll[this.comboBox1.SelectedIndex];
        if (strArray1.Length != 0 && index2 != -1)
        {
          int num1 = 0;
          foreach (string str in strArray1)
          {
            if (!string.IsNullOrEmpty(str.Trim()))
            {
              string[] strArray2 = str.Split('\t');
              bool flag2 = false;
              string text2 = string.Empty;
              for (int colindex = index2; colindex < index2 + strArray2.Length; ++colindex)
              {
                if (!this.CheckVal(colindex, strArray2[colindex - index2]))
                {
                  text2 = text2 + dataTable.Columns[index2].ColumnName.Desc() + ",Excel第" + (object) num1 + "行，格式不正确,请您重新输入\r\n";
                  flag2 = true;
                }
              }
              if (flag2)
              {
                int num2 = (int) MessageBox.Show(text2, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
              }
              for (int index4 = index2; index4 < index2 + strArray2.Length; ++index4)
              {
                if (dataTable.Columns[index4].DataType != typeof (List<DataTable>) && dataTable.Columns[index4].DataType != typeof (DataTable))
                  this.dataGridView1.Rows[index1].Cells[index4].Value = (object) strArray2[index4 - index2];
              }
              ++index1;
              ++num1;
            }
          }
        }
        dataTable.AcceptChanges();
        BnyCommon.MainTable.AcceptChanges();
        this.InvalidFlag();
      }
      catch
      {
      }
    }

    private void pastenew_Click(object sender, EventArgs e)
    {
      try
      {
        string text1 = Clipboard.GetText();
        if (string.IsNullOrEmpty(text1))
          return;
        string[] strArray1 = text1.Split(new string[1]
        {
          "\r\n"
        }, StringSplitOptions.RemoveEmptyEntries);
        if (strArray1.Length == 0)
          return;
        DataTable dataTable = this.dtcoll[this.comboBox1.SelectedIndex];
        int num1 = 0;
        foreach (string str in strArray1)
        {
          if (!string.IsNullOrEmpty(str.Trim()))
          {
            string[] strArray2 = str.Split('\t');
            bool flag = false;
            string text2 = string.Empty;
            for (int index = 0; index < strArray2.Length; ++index)
            {
              if (!this.CheckVal(index, strArray2[index]))
              {
                text2 = text2 + dataTable.Columns[index].ColumnName.Desc() + ",Excel第" + (object) num1 + "行，格式不正确,请您重新输入\r\n";
                flag = true;
              }
            }
            if (flag)
            {
              int num2 = (int) MessageBox.Show(text2, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
              return;
            }
            DataRow row = this.CreateRow();
            for (int index = 0; index < strArray2.Length; ++index)
            {
              if (dataTable.Columns[index].DataType != typeof (List<DataTable>) && dataTable.Columns[index].DataType != typeof (DataTable))
                row[index] = (object) strArray2[index];
            }
            dataTable.Rows.Add(row);
            ++num1;
          }
        }
        this.dataGridView1.CurrentCell = this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[0];
        dataTable.AcceptChanges();
        BnyCommon.MainTable.AcceptChanges();
        this.InvalidFlag();
      }
      catch
      {
      }
    }

    private void FrmEditStruct_Load(object sender, EventArgs e)
    {
      if (BnyCommon.ShowData == null)
        return;
      foreach (KeyValuePair<string, CustomShow> keyValuePair in BnyCommon.ShowData)
      {
        ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
        toolStripMenuItem.Text = keyValuePair.Key;
        toolStripMenuItem.Click += new EventHandler(this.Imenu_Click);
        this.showdatamenuitem.DropDownItems.Add((ToolStripItem) toolStripMenuItem);
      }
    }

    private void Imenu_Click(object sender, EventArgs e)
    {
      ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
      if (!BnyCommon.ShowData.ContainsKey(toolStripMenuItem.Text))
        return;
      CustomShow customShow = BnyCommon.ShowData[toolStripMenuItem.Text];
      int columnIndex = this.dataGridView1.CurrentCell.ColumnIndex;
      int num = 0;
      foreach (DataGridViewRow row in (IEnumerable) this.dataGridView1.Rows)
      {
        DataRow[] dataRowArray = customShow.MainTable.Select(customShow.Key + "='" + row.Cells[columnIndex].Value.ToString() + "'");
        if (dataRowArray.Length != 0)
        {
          string str = dataRowArray[0][customShow.ShowName].ToString();
          if (!string.IsNullOrEmpty(str))
          {
            ((DataGridViewTextBoxCellEx) row.Cells[columnIndex]).DrawColor = Color.Green;
            ((DataGridViewTextBoxCellEx) row.Cells[columnIndex]).Alias = str;
            ((DataGridViewTextBoxCellEx) row.Cells[columnIndex]).IsShowalias = true;
          }
          else
            ((DataGridViewTextBoxCellEx) row.Cells[columnIndex]).IsShowalias = false;
        }
        ++num;
      }
      this.dataGridView1.Invalidate();
    }

    private void toolStripMenuItem1_Click(object sender, EventArgs e)
    {
      if (this.dataGridView1.CurrentCell == null)
        return;
      int result;
      if (int.TryParse(this.dataGridView1.CurrentCell.Value.ToString(), out result))
      {
        int num1 = (int) new FrmI2F(result).ShowDialog();
      }
      else
      {
        int num2 = (int) MessageBox.Show("无法转换为浮点数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }

    private void batchReplace_Click(object sender, EventArgs e)
    {
      if (this.dataGridView1.SelectedRows.Count == 0)
        return;
      DataTable dt = this.dtcoll[this.comboBox1.SelectedIndex];
      FrmBatchReplace frmBatchReplace = new FrmBatchReplace(dt);
      if (frmBatchReplace.ShowDialog() != DialogResult.OK || frmBatchReplace.LineNumber == 0)
        return;
      foreach (DataGridViewRow selectedRow in (BaseCollection) this.dataGridView1.SelectedRows)
      {
        object[] itemArray = dt.Rows[selectedRow.Index].ItemArray;
        if (itemArray[frmBatchReplace.ColumnNmae] is List<DataTable>)
        {
          List<DataTable> dataTableList1 = itemArray[frmBatchReplace.ColumnNmae] as List<DataTable>;
          List<DataTable> dataTableList2 = dt.Rows[frmBatchReplace.LineNumber - 1].ItemArray[frmBatchReplace.ColumnNmae] as List<DataTable>;
          int count = dataTableList1.Count;
          for (int index1 = 0; index1 < count; ++index1)
          {
            dataTableList1[index1].Rows.Clear();
            for (int index2 = 0; index2 < dataTableList2[index1].Rows.Count; ++index2)
            {
              DataRow row = dataTableList1[index1].NewRow();
              row.ItemArray = dataTableList2[index1].Rows[index2].ItemArray;
              dataTableList1[index1].Rows.Add(row);
            }
          }
          dt.Rows[selectedRow.Index].ItemArray = itemArray;
        }
        else
        {
          itemArray[frmBatchReplace.ColumnNmae] = dt.Rows[frmBatchReplace.LineNumber - 1].ItemArray[frmBatchReplace.ColumnNmae];
          dt.Rows[selectedRow.Index].ItemArray = itemArray;
        }
      }
    }

    private void InvalidFlag()
    {
      if (!(this.显示标记ToolStripMenuItem.Text == "隐藏标记"))
        return;
      this.PaintFlag();
    }

    private void PaintFlag()
    {
      if (FlagUtils.CurrentFlagTable() == null)
        return;
      DataTable dataTable = this.dtcoll[this.comboBox1.SelectedIndex];
      int row1 = 0;
      foreach (DataGridViewRow row2 in (IEnumerable) this.dataGridView1.Rows)
      {
        for (int index = 0; index < dataTable.Columns.Count; ++index)
        {
          if (dataTable.Columns[index].DataType != typeof (DataTable) && dataTable.Columns[index].DataType != typeof (List<DataTable>))
          {
            string flag = FlagUtils.getFlag(dataTable.Columns[index].ColumnName, row2.Cells[index].Value.ToString(), index, row1);
            if (!string.IsNullOrEmpty(flag))
            {
              ((DataGridViewTextBoxCellEx) row2.Cells[index]).Alias = flag;
              ((DataGridViewTextBoxCellEx) row2.Cells[index]).IsShowalias = true;
              ((DataGridViewTextBoxCellEx) row2.Cells[index]).DrawColor = Color.Red;
            }
            else
              ((DataGridViewTextBoxCellEx) row2.Cells[index]).IsShowalias = false;
          }
        }
        ++row1;
      }
      this.dataGridView1.Invalidate();
    }

    private void 显示标记ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.显示标记ToolStripMenuItem.Text == "显示标记")
      {
        this.PaintFlag();
        this.显示标记ToolStripMenuItem.Text = "隐藏标记";
      }
      else
      {
        DataTable dataTable = this.dtcoll[this.comboBox1.SelectedIndex];
        this.显示标记ToolStripMenuItem.Text = "显示标记";
        foreach (DataGridViewRow row in (IEnumerable) this.dataGridView1.Rows)
        {
          for (int index = 0; index < dataTable.Columns.Count; ++index)
          {
            if (dataTable.Columns[index].DataType != typeof (DataTable) && dataTable.Columns[index].DataType != typeof (List<DataTable>))
              ((DataGridViewTextBoxCellEx) row.Cells[index]).IsShowalias = false;
          }
        }
        this.dataGridView1.Invalidate();
      }
    }

    private void 添加编辑标记ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.dataGridView1.CurrentCell == null || new FrmFlag(this.dataGridView1.CurrentCell.ColumnIndex, this.dataGridView1.CurrentCell.RowIndex, this.dtcoll[this.comboBox1.SelectedIndex].Columns[this.dataGridView1.CurrentCell.ColumnIndex].ColumnName, this.dataGridView1.CurrentCell.Value.ToString()).ShowDialog() != DialogResult.OK)
        return;
      this.InvalidFlag();
      int num = (int) MessageBox.Show("恭喜您，标记添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void 删除标记ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (!FlagUtils.DeleteFlag(this.dtcoll[this.comboBox1.SelectedIndex].Columns[this.dataGridView1.CurrentCell.ColumnIndex].ColumnName, this.dataGridView1.CurrentCell.Value.ToString(), this.dataGridView1.CurrentCell.ColumnIndex, this.dataGridView1.CurrentCell.RowIndex))
        return;
      FlagUtils.WriteFlags();
      int num = (int) MessageBox.Show("恭喜您，标记删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      this.InvalidFlag();
    }

    private void dataGridView1_Sorted(object sender, EventArgs e)
    {
      string name = this.dataGridView1.SortedColumn.Name;
      switch (this.dataGridView1.SortOrder)
      {
        case SortOrder.None:
        case SortOrder.Ascending:
          this.tableSort = name + " asc";
          break;
        case SortOrder.Descending:
          this.tableSort = name + " desc";
          break;
      }
      this.InvalidFlag();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      DataGridViewCellStyle gridViewCellStyle = new DataGridViewCellStyle();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmEditStruct));
      this.comboBox1 = new ComboBox();
      this.dataGridView1 = new DataGridView();
      this.contextMenuStrip1 = new ContextMenuStrip(this.components);
      this.clipcopy = new ToolStripMenuItem();
      this.toolStripMenuItem8 = new ToolStripSeparator();
      this.pastcover = new ToolStripMenuItem();
      this.pastenew = new ToolStripMenuItem();
      this.toolStripMenuItem10 = new ToolStripSeparator();
      this.toolStripMenuItem1 = new ToolStripMenuItem();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.batchReplace = new ToolStripMenuItem();
      this.toolStripSeparator2 = new ToolStripSeparator();
      this.deleteselect = new ToolStripMenuItem();
      this.toolStripMenuItem2 = new ToolStripSeparator();
      this.显示标记ToolStripMenuItem = new ToolStripMenuItem();
      this.添加编辑标记ToolStripMenuItem = new ToolStripMenuItem();
      this.删除标记ToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripMenuItem3 = new ToolStripSeparator();
      this.showdatamenuitem = new ToolStripMenuItem();
      this.button3 = new Button();
      this.btnNew = new Button();
      this.btnDelete = new Button();
      this.toolTip1 = new ToolTip(this.components);
      ((ISupportInitialize) this.dataGridView1).BeginInit();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Location = new Point(5, 8);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new Size(864, 20);
      this.comboBox1.TabIndex = 0;
      this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToResizeRows = false;
      gridViewCellStyle.BackColor = Color.FromArgb(238, 242, 253);
      this.dataGridView1.AlternatingRowsDefaultCellStyle = gridViewCellStyle;
      this.dataGridView1.BackgroundColor = Color.White;
      this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
      this.dataGridView1.Location = new Point(5, 34);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.RowTemplate.Height = 23;
      this.dataGridView1.Size = new Size(864, 467);
      this.dataGridView1.TabIndex = 1;
      this.dataGridView1.CellContentClick += new DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
      this.dataGridView1.DataError += new DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
      this.dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
      this.dataGridView1.Sorted += new EventHandler(this.dataGridView1_Sorted);
      this.dataGridView1.UserDeletingRow += new DataGridViewRowCancelEventHandler(this.dataGridView1_UserDeletingRow);
      this.contextMenuStrip1.Items.AddRange(new ToolStripItem[16]
      {
        (ToolStripItem) this.clipcopy,
        (ToolStripItem) this.toolStripMenuItem8,
        (ToolStripItem) this.pastcover,
        (ToolStripItem) this.pastenew,
        (ToolStripItem) this.toolStripMenuItem10,
        (ToolStripItem) this.toolStripMenuItem1,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.batchReplace,
        (ToolStripItem) this.toolStripSeparator2,
        (ToolStripItem) this.deleteselect,
        (ToolStripItem) this.toolStripMenuItem2,
        (ToolStripItem) this.显示标记ToolStripMenuItem,
        (ToolStripItem) this.添加编辑标记ToolStripMenuItem,
        (ToolStripItem) this.删除标记ToolStripMenuItem,
        (ToolStripItem) this.toolStripMenuItem3,
        (ToolStripItem) this.showdatamenuitem
      });
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new Size(216, 260);
      this.clipcopy.Name = "clipcopy";
      this.clipcopy.ShortcutKeys = Keys.C | Keys.Control;
      this.clipcopy.Size = new Size(215, 22);
      this.clipcopy.Text = "复制";
      this.clipcopy.Click += new EventHandler(this.clipcopy_Click);
      this.toolStripMenuItem8.Name = "toolStripMenuItem8";
      this.toolStripMenuItem8.Size = new Size(212, 6);
      this.pastcover.Name = "pastcover";
      this.pastcover.ShortcutKeys = Keys.V | Keys.Control;
      this.pastcover.Size = new Size(215, 22);
      this.pastcover.Text = "粘贴";
      this.pastcover.Click += new EventHandler(this.pastcover_Click);
      this.pastenew.Name = "pastenew";
      this.pastenew.ShortcutKeys = Keys.C | Keys.Shift | Keys.Control;
      this.pastenew.Size = new Size(215, 22);
      this.pastenew.Text = "粘贴为新建";
      this.pastenew.Click += new EventHandler(this.pastenew_Click);
      this.toolStripMenuItem10.Name = "toolStripMenuItem10";
      this.toolStripMenuItem10.Size = new Size(212, 6);
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new Size(215, 22);
      this.toolStripMenuItem1.Text = "转换为浮点数";
      this.toolStripMenuItem1.Click += new EventHandler(this.toolStripMenuItem1_Click);
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(212, 6);
      this.batchReplace.Name = "batchReplace";
      this.batchReplace.Size = new Size(215, 22);
      this.batchReplace.Text = "批量替换";
      this.batchReplace.Click += new EventHandler(this.batchReplace_Click);
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new Size(212, 6);
      this.deleteselect.Name = "deleteselect";
      this.deleteselect.ShortcutKeys = Keys.D | Keys.Control;
      this.deleteselect.Size = new Size(215, 22);
      this.deleteselect.Text = "删除选中";
      this.deleteselect.Click += new EventHandler(this.deleteselect_Click);
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new Size(212, 6);
      this.显示标记ToolStripMenuItem.Name = "显示标记ToolStripMenuItem";
      this.显示标记ToolStripMenuItem.Size = new Size(215, 22);
      this.显示标记ToolStripMenuItem.Text = "显示标记";
      this.显示标记ToolStripMenuItem.Click += new EventHandler(this.显示标记ToolStripMenuItem_Click);
      this.添加编辑标记ToolStripMenuItem.Name = "添加编辑标记ToolStripMenuItem";
      this.添加编辑标记ToolStripMenuItem.Size = new Size(215, 22);
      this.添加编辑标记ToolStripMenuItem.Text = "添加编辑标记";
      this.添加编辑标记ToolStripMenuItem.Click += new EventHandler(this.添加编辑标记ToolStripMenuItem_Click);
      this.删除标记ToolStripMenuItem.Name = "删除标记ToolStripMenuItem";
      this.删除标记ToolStripMenuItem.Size = new Size(215, 22);
      this.删除标记ToolStripMenuItem.Text = "删除标记";
      this.删除标记ToolStripMenuItem.Click += new EventHandler(this.删除标记ToolStripMenuItem_Click);
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new Size(212, 6);
      this.showdatamenuitem.Name = "showdatamenuitem";
      this.showdatamenuitem.Size = new Size(215, 22);
      this.showdatamenuitem.Text = "将此列数据显示为";
      this.button3.Location = new Point(745, 505);
      this.button3.Name = "button3";
      this.button3.Size = new Size(124, 33);
      this.button3.TabIndex = 4;
      this.button3.Text = "关闭";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new EventHandler(this.button3_Click);
      this.btnNew.Location = new Point(12, 507);
      this.btnNew.Name = "btnNew";
      this.btnNew.Size = new Size(71, 31);
      this.btnNew.TabIndex = 7;
      this.btnNew.Text = "新建";
      this.btnNew.UseVisualStyleBackColor = true;
      this.btnNew.Click += new EventHandler(this.button1_Click);
      this.btnDelete.Location = new Point(89, 507);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new Size(64, 31);
      this.btnDelete.TabIndex = 9;
      this.btnDelete.Text = "删除";
      this.btnDelete.UseVisualStyleBackColor = true;
      this.btnDelete.Click += new EventHandler(this.button4_Click);
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(871, 550);
      this.Controls.Add((Control) this.dataGridView1);
      this.Controls.Add((Control) this.btnDelete);
      this.Controls.Add((Control) this.btnNew);
      this.Controls.Add((Control) this.button3);
      this.Controls.Add((Control) this.comboBox1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Icon = this.Icon;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (FrmEditStruct);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "双击数据到编辑区可进行修改";
      this.Load += new EventHandler(this.FrmEditStruct_Load);
      ((ISupportInitialize) this.dataGridView1).EndInit();
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
