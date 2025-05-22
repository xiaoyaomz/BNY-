// Decompiled with JetBrains decompiler
// Type: BnyEditor.FrmMain
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using YourNamespace; 

#nullable disable
namespace BnyEditor
{
  public class FrmMain : Form
  {
    private string _fileName;
    private FindFile findfrm;
    private TimeSpan oldTime;
    private List<int> ButtonIndex = new List<int>();
    private bool bflag;
    private int delcount;
    private string tableSort = string.Empty;
    private string oldText = string.Empty;
    private string miniText = string.Empty;
    private List<int> searchResult = new List<int>();
    private List<int> searchResult2 = new List<int>();
    private int LastSearchIndex = -1;
    private int searchColIndex = -1;
    private IContainer components;
    private ToolTip toolTip1;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem 文件FToolStripMenuItem;
    private ToolStripMenuItem 打开OToolStripMenuItem;
    private ToolStripSeparator toolStripMenuItem1;
    private ToolStripMenuItem btnSave;
    private ToolStripMenuItem btnSaveAs;
    private ToolStripSeparator toolStripMenuItem2;
    private ToolStripMenuItem 退出ToolStripMenuItem;
    private ToolStripMenuItem 工具ToolStripMenuItem;
    private ToolStripMenuItem 热更新补丁包制作ToolStripMenuItem;
    private ToolStripMenuItem 关于AToolStripMenuItem;
    private ToolStripMenuItem 免责声明MToolStripMenuItem;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel msg;
    private Label notice;
    private ToolStripMenuItem MenuItemSearch;
    private ToolStripStatusLabel filelb;
    private ToolStripMenuItem 查找文件FToolStripMenuItem;
    private ToolStripSeparator toolStripMenuItem4;
    private ToolStripMenuItem 查找内容WToolStripMenuItem;
    private ToolStripMenuItem newgeshi;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem toolStripMenuItem6;
    private ToolStripMenuItem exportexcel;
    private ToolStripSeparator toolStripMenuItem7;
    private ToolStripMenuItem importexcel;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem pastenew;
    private ToolStripMenuItem pastcover;
    private ToolStripSeparator toolStripMenuItem10;
    private ToolStripMenuItem deleteselect;
    private DataGridView dataGridView1;
    private ToolStripStatusLabel lbfiledesc;
    private ToolStrip toolStrip1;
    private ToolStripButton 打开OToolStripButton;
    private ToolStripButton btnSaveT2;
    private ToolStripSeparator toolStripSeparator;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripButton btnServer;
    private ToolStripButton btnSaveAst;
    private ToolStripButton btnNew;
    private ToolStripButton btnDelete;
    private ToolStripSeparator toolStripMenuItem5;
    private ToolStripMenuItem 转到行ToolStripMenuItem;
    private ToolStripMenuItem clipcopy;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripSeparator toolStripMenuItem8;
    private ToolStripMenuItem toolStripMenuItem9;
    private ToolStripMenuItem toolStripMenuItem11;
    private ToolStripMenuItem toolStripMenuItem12;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStripMenuItem hotmenu;
    private ToolStripSeparator toolStripMenuItem13;
    private ToolStripMenuItem 数值转换ToolStripMenuItem;
    private ToolStripMenuItem toolStripMenuItem14;
    private ToolStripMenuItem toolStripMenuItem15;
    private ToolStripSeparator toolStripSeparator7;
    private ToolStripMenuItem piliangzhuan;
    private ToolStripSeparator toolStripSeparator8;
    private ToolStripSeparator toolStripSeparator9;
    private ToolStripButton toolStripButton1;
    private ToolStripSeparator toolStripMenuItem16;
    private ToolStripMenuItem 显示别名ToolStripMenuItem;
    private ToolStripMenuItem 添加标记ToolStripMenuItem;
    private ToolStripMenuItem 移除标记ToolStripMenuItem;
    private ToolStripSeparator toolStripMenuItem17;
    private ToolStripMenuItem showdatamenuitem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton btnSaveT;
    private ToolStripButton cleanOrderBtn;
    private ToolStripLabel toolStripLabel1;
    private ToolStripComboBox cboSearch;
    private ToolStripTextBox txtKey;
    private ToolStripButton toolStripButton2;
    private ToolStripButton searchNext;
    private ToolStripButton toolStripButton3;
    private ToolStripButton btnReplace;
    private ToolStripButton btnReplaceNext;
	
// 新加
    private ToolStripTextBox txtReplaceWith;
	

    private void SetShowFind(FindFile form)
    {
      try
      {
        FindFile findFile = (FindFile) null;
        if (this.findfrm != null)
          findFile = (FindFile) Application.OpenForms[this.findfrm.Name];
        if (findFile == null)
        {
          this.findfrm = (FindFile) null;
          this.findfrm = form;
          this.findfrm.Show();
        }
        else
          findFile.Activate();
      }
      catch
      {
      }
    }

    public FrmMain(string file)
    {
      AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(this.CurrentDomain_AssemblyResolve);
      this.InitializeComponent();
      this.oldTime = new TimeSpan(DateTime.Now.Ticks);
      try
      {
        this.dataGridView1.DoubleBufferedDataGirdView(true);
        BnyCommon.MainForm = this;
        if (new FrmSplash().ShowDialog() == DialogResult.OK)
        {
          this.oldText = this.Text;
        }
        else
        {
          Application.Exit();
          Environment.Exit(0);
        }
        this.OpenFile(file);
        this.msg.Text = "软件初始化成功！";
      }
      catch (Exception ex)
      {
        Application.Exit();
        Environment.Exit(0);
      }
    }

    public FrmMain()
    {
      try
      {
        this.InitializeComponent();
        this.oldTime = new TimeSpan(DateTime.Now.Ticks);
        this.dataGridView1.DoubleBufferedDataGirdView(true);
        BnyCommon.MainForm = this;
        if (new FrmSplash().ShowDialog() == DialogResult.OK)
        {
          this.oldText = this.Text;
        }
        else
        {
          Application.Exit();
          Environment.Exit(0);
        }
        this.msg.Text = "软件初始化成功！";
      }
      catch (Exception ex)
      {
        Application.Exit();
        Environment.Exit(0);
      }
    }

    private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
    {
      string name = (args.Name.Contains(",") ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "")).Replace(".", "_");
      return name.EndsWith("_resources") ? (Assembly) null : Assembly.Load((byte[]) new ResourceManager("BnyEditor_Margin.Properties.Resources", Assembly.GetExecutingAssembly()).GetObject(name));
    }

    private void CheckNumber()
    {
      if (CheckSerialNumber.CheckCode())
        return;
      new Thread(new ThreadStart(this.ExitApp)).Start();
    }

    private void ExitApp()
    {
      Application.ExitThread();
      Application.Exit();
      Environment.Exit(0);
    }

    protected override void WndProc(ref Message m)
    {
      TimeSpan timeSpan = this.oldTime.Subtract(new TimeSpan(DateTime.Now.Ticks));
      timeSpan = timeSpan.Duration();
      if (timeSpan.Minutes == 1)
      {
        this.oldTime = new TimeSpan(DateTime.Now.Ticks);
        new Thread(new ThreadStart(this.CheckNumber)).Start();
      }
      base.WndProc(ref m);
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
          viewTextBoxColumnEx.HeaderText = dt.Columns[index].ColumnName.Desc();
          viewTextBoxColumnEx.ValueType = dt.Columns[index].GetType();
          viewTextBoxColumnEx.DataPropertyName = dt.Columns[index].ColumnName;
          viewTextBoxColumnEx.Name = dt.Columns[index].ColumnName;
          viewTextBoxColumnEx.ToolTipText = dt.Columns[index].ColumnName.Tips();
          grid.Columns.Add((DataGridViewColumn) viewTextBoxColumnEx);
        }
        else
        {
          DataGridViewButtonColumn viewButtonColumn = new DataGridViewButtonColumn();
          viewButtonColumn.HeaderText = dt.Columns[index].ColumnName.Desc();
          viewButtonColumn.UseColumnTextForButtonValue = false;
          viewButtonColumn.Name = dt.Columns[index].ColumnName;
          viewButtonColumn.DefaultCellStyle.NullValue = (object) dt.Columns[index].ColumnName.Desc();
          grid.Columns.Add((DataGridViewColumn) viewButtonColumn);
          this.ButtonIndex.Add(index);
        }
      }
    }

    private void FillListView()
    {
      this.msg.Text = "共计:" + (object) BnyCommon.MainTable.Rows.Count + "条记录";
      this.GenerateColumnType(this.dataGridView1, BnyCommon.MainTable);
      this.dataGridView1.DataSource = (object) BnyCommon.MainTable;
      foreach (DataGridViewRow row in (IEnumerable) this.dataGridView1.Rows)
      {
        for (int index = 0; index < BnyCommon.MainTable.Columns.Count; ++index)
        {
          if (BnyCommon.MainTable.Columns[index].DataType != typeof (DataTable) && BnyCommon.MainTable.Columns[index].DataType != typeof (List<DataTable>))
            row.Cells[index].ToolTipText = BnyCommon.MainTable.Columns[index].ColumnName.Tips().ToLower().Replace("<br>", "\r\n");
        }
      }
    }

    public void OpenFile(string fileName)
    {
      DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(fileName));
      this._fileName = fileName;
      if (!BnyUtils.ReadFileIsHot(this._fileName))
        this.hotmenu.Enabled = true;
      else
        this.hotmenu.Enabled = false;
      this.filelb.Text = "当前编辑文件====> " + this._fileName.Substring(this._fileName.LastIndexOf("\\") + 1);
      this.filelb.ToolTipText = fileName;
      this.miniText = this._fileName.Substring(this._fileName.LastIndexOf("\\") + 1);
      BnyCommon.GetSearchData();
      BnyUtils.ReadFile(fileName);
      this.btnDelete.Enabled = true;
      this.btnSaveAst.Enabled = true;
      this.btnSaveT.Enabled = true;
      this.btnSaveT2.Enabled = true;
      this.btnSave.Enabled = true;
      this.btnDelete.Enabled = true;
      this.btnSaveAs.Enabled = true;
      this.importexcel.Enabled = true;
      this.exportexcel.Enabled = true;
      this.btnNew.Enabled = true;
      this.newgeshi.Enabled = true;
      this.toolStripButton1.Enabled = true;
      string str = Path.GetFileName(fileName).Replace(".bny", "");
      BnyCommon.CurrFileName = str.IndexOf(".") <= -1 ? str : str.Substring(str.LastIndexOf(".") + 1);
      BnyCommon.FindFileDesc();
      if (BnyCommon.FileDesc != null && !string.IsNullOrEmpty(BnyCommon.FileDesc.fileDesc))
      {
        this.lbfiledesc.Visible = true;
        this.lbfiledesc.Text = BnyCommon.FileDesc.fileDesc;
      }
      else
        this.lbfiledesc.Visible = false;
      this.FillListView();
      if (this._fileName.IndexOf("mzm.gsp.mall.confbean.CItemPriceCfg.bny") > -1 || this._fileName.IndexOf("mzm.gsp.item.confbean.CItemCfg.bny") > -1)
        this.btnServer.Visible = true;
      else
        this.btnServer.Visible = false;
      this.cboSearch.Items.Clear();
      this.cboSearch.Items.Add((object) "全部");
      foreach (DataColumn column in (InternalDataCollectionBase) BnyCommon.MainTable.Columns)
        this.cboSearch.Items.Add((object) column.ColumnName.Desc());
      if (this.cboSearch.Items.Count <= 0)
        return;
      this.cboSearch.SelectedIndex = 0;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog1 = new OpenFileDialog();
      openFileDialog1.Title = "请选择BNY文件";
      openFileDialog1.Filter = "BNY文件|*.bny|所有文件|*.*";
      OpenFileDialog openFileDialog2 = openFileDialog1;
      if (openFileDialog2.ShowDialog() != DialogResult.OK)
        return;
      this.OpenFile(openFileDialog2.FileName);
    }

    private void FrmMain_Load(object sender, EventArgs e)
    {
      AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(this.CurrentDomain_AssemblyResolve);
      try
      {
        FileTypeRegister.RegisterFileType(new FileTypeRegInfo()
        {
          Description = "BNY通杀编辑器",
          ExePath = Application.StartupPath + "\\" + Application.ProductName.ToString() + ".exe",
          ExtendName = ".bny",
          IcoPath = Application.StartupPath + "\\" + Application.ProductName.ToString() + ".exe,0"
        });
      }
      catch
      {
      }
      BnyCommon.GetSearchData();
      BnyCommon.GetShowData();
      FlagUtils.LoadFlags();
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
private void txtKey_Enter(object sender, EventArgs e)
{
    if (txtKey.Text == "查找的文本")
    {
        txtKey.Text = string.Empty;
    }
}

private void txtKey_Leave(object sender, EventArgs e)
{
    if (string.IsNullOrWhiteSpace(txtKey.Text))
    {
        txtKey.Text = "查找的文本";
    }
}
private void txtReplaceWith_Enter(object sender, EventArgs e)
{
    if (txtReplaceWith.Text == "替换后的文本")
    {
        txtReplaceWith.Text = string.Empty;
    }
}

private void txtReplaceWith_Leave(object sender, EventArgs e)
{
    if (string.IsNullOrWhiteSpace(txtReplaceWith.Text))
    {
        txtReplaceWith.Text = "替换后的文本";
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

    private void btnSaveAs_Click(object sender, EventArgs e)
    {
      SaveFileDialog saveFileDialog1 = new SaveFileDialog();
      saveFileDialog1.Title = "请选择保存地址";
      saveFileDialog1.Filter = "*.bny|*.bny|*.*|*.*";
      saveFileDialog1.FileName = this._fileName;
      SaveFileDialog saveFileDialog2 = saveFileDialog1;
      if (saveFileDialog2.ShowDialog() != DialogResult.OK)
        return;
      BnyUtils.WriteFile(saveFileDialog2.FileName);
      int num = (int) MessageBox.Show("恭喜!保存文件成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void button4_Click_1(object sender, EventArgs e)
    {
      HotUpdateFrm hotUpdateFrm = new HotUpdateFrm();
      this.Hide();
      int num = (int) hotUpdateFrm.ShowDialog();
      this.Show();
    }

    public static long ConvertDateTimeToInt()
    {
      return (DateTime.Now.Ticks - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0)).Ticks) / 10000L;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      string fileName = this._fileName;
      File.Move(fileName, fileName.Replace(".bny", "_" + (object) FrmMain.ConvertDateTimeToInt() + ".bak"));
      BnyUtils.WriteFile(fileName);
      int num = (int) MessageBox.Show("恭喜!保存文件成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void btnSave2_Click(object sender, EventArgs e)
    {
      string fileName = this._fileName;
      File.Move(fileName, fileName.Replace(".bny", "_" + (object) FrmMain.ConvertDateTimeToInt() + ".bak"));
      BnyUtils.WriteFile(fileName, this.dataGridView1);
      int num = (int) MessageBox.Show("恭喜!保存文件成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void cleanOrderBtn_Click(object sender, EventArgs e)
    {
    }

    private void 免责声明MToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int num = (int) MessageBox.Show("软件仅供技术交流，请勿用于商业及非法用途，如产生法律纠纷与请找默念！\n默念狗出尔反尔不给更新，所以发出此版本，让大家铭记。\n加群送源码：961553363", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void btnServer_Click(object sender, EventArgs e)
    {
      string str1 = string.Empty;
      StringBuilder stringBuilder1 = new StringBuilder();
      if (this._fileName.IndexOf("mzm.gsp.mall.confbean.CItemPriceCfg.bny") > -1)
      {
        str1 = "mzm.gsp.mall.confbean.SMallItemCfg.xml";
        stringBuilder1.AppendLine("<data>");
        Dictionary<string, int> dictionary = new Dictionary<string, int>();
        dictionary.Add("1", 530100000);
        dictionary.Add("3", 530101000);
        dictionary.Add("2", 530102000);
        dictionary.Add("4", 530103000);
        dictionary.Add("6", 530105000);
        foreach (DataRow row in (InternalDataCollectionBase) BnyCommon.MainTable.Rows)
        {
          string[] strArray = row["key"].ToString().Split('_');
          string str2 = strArray[0];
          string key = strArray[1];
          string str3 = row["price"].ToString();
          string str4 = row["primeprice"].ToString();
          string str5 = row["maxbuynum"].ToString();
          string str6 = row["sort"].ToString();
          int num = dictionary[key];
          stringBuilder1.AppendLine("  <mzm.gsp.mall.confbean.SMallItemCfg id=\"" + (object) num + "\" itemid=\"" + str2 + "\" mallType=\"" + key + "\" maxbuynum=\"" + str5 + "\" price=\"" + str3 + "\" primeprice=\"" + str4 + "\" sort=\"" + str6 + "\"/>");
          dictionary[key]++;
        }
        stringBuilder1.AppendLine("</data>");
        string str7 = "mzm.gsp.mall.confbean.SMalltype2Item.xml";
        StringBuilder stringBuilder2 = new StringBuilder();
        stringBuilder2.AppendLine("<data>");
        for (int index = 1; index <= 6; ++index)
        {
          if (index != 5)
          {
            stringBuilder2.AppendLine("<mzm.gsp.mall.confbean.SMalltype2Item malltype=\"" + (object) index + "\">");
            stringBuilder2.AppendLine("<itemid2pricenum>");
            stringBuilder2.AppendLine("<no-comparator/>");
            foreach (DataRow dataRow in BnyCommon.MainTable.Select("key like'%_" + (object) index + "'"))
            {
              string str8 = dataRow["key"].ToString().Split('_')[0];
              stringBuilder2.AppendLine("<entry>");
              stringBuilder2.AppendLine("<int>" + str8 + "</int>");
              stringBuilder2.AppendLine("<mzm.gsp.mall.confbean.PriceMaxbuynum maxbuynum=\"" + dataRow["maxbuynum"].ToString() + "\" price=\"" + dataRow["price"].ToString() + "\"/>");
              stringBuilder2.AppendLine("</entry>");
            }
            stringBuilder2.AppendLine("</itemid2pricenum>");
            stringBuilder2.AppendLine("</mzm.gsp.mall.confbean.SMalltype2Item>");
          }
        }
        stringBuilder2.AppendLine("</data>");
        using (FileStream fileStream = new FileStream(Path.GetDirectoryName(this._fileName) + "\\" + str7, FileMode.Create, FileAccess.Write))
        {
          using (StreamWriter streamWriter = new StreamWriter((Stream) fileStream, Encoding.UTF8))
          {
            streamWriter.Write(stringBuilder2.ToString());
            streamWriter.Flush();
          }
        }
      }
      else if (this._fileName.IndexOf("mzm.gsp.item.confbean.CItemCfg.bny") > -1)
      {
        str1 = "mzm.gsp.item.confbean.SItemCfg.xml";
        stringBuilder1.AppendLine("<data>");
        foreach (DataRow row in (InternalDataCollectionBase) BnyCommon.MainTable.Rows)
          stringBuilder1.AppendLine("  <mzm.gsp.item.confbean.SItemCfg awardRoleLevelMax=\"" + row["awardRoleLevelMax"].ToString() + "\" awardRoleLevelMin=\"" + row["awardRoleLevelMin"].ToString() + "\" canSellAndThrow=\"" + (row["canSellAndThrow"].ToString() == "1" ? "true" : "false") + "\" icon=\"" + row["icon"].ToString() + "\" id=\"" + row["itemid"].ToString() + "\" isProprietary=\"" + (row["isProprietary"].ToString() == "0" ? "false" : "true") + "\" name=\"" + row["name"].ToString() + "\" namecolor=\"" + row["namecolor"].ToString() + "\" pilemax=\"" + row["pilemax"].ToString() + "\" sellSilver=\"" + row["sellSilver"].ToString() + "\" sort=\"" + row["sort"].ToString() + "\" type=\"" + row["type"].ToString() + "\" useLevel=\"" + row["useLevel"].ToString() + "\"/>");
        stringBuilder1.AppendLine("</data>");
      }
      using (FileStream fileStream = new FileStream(Path.GetDirectoryName(this._fileName) + "\\" + str1, FileMode.Create, FileAccess.Write))
      {
        using (StreamWriter streamWriter = new StreamWriter((Stream) fileStream, Encoding.UTF8))
        {
          streamWriter.Write(stringBuilder1.ToString());
          streamWriter.Flush();
        }
      }
      int num1 = (int) MessageBox.Show("服务端XML文件保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void 查找内容WToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int num = (int) new FrmSearch().ShowDialog();
    }

    private void 查找文件FToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.SetShowFind(new FindFile());
    }

    private void newgeshi_Click(object sender, EventArgs e)
    {
      if (new FrmSaveNew(this._fileName).ShowDialog() != DialogResult.OK)
        return;
      int num = (int) MessageBox.Show("文件保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void exportexcel_Click(object sender, EventArgs e)
    {
      SaveFileDialog saveFileDialog1 = new SaveFileDialog();
      saveFileDialog1.Title = "请选择保存Excel的地址";
      saveFileDialog1.Filter = "*.xls|*.xls";
      saveFileDialog1.FileName = this._fileName.Substring(this._fileName.LastIndexOf("\\") + 1).Replace(".bny", ".xls");
      SaveFileDialog saveFileDialog2 = saveFileDialog1;
      if (saveFileDialog2.ShowDialog() != DialogResult.OK)
        return;
      if (NPOIUtils.ExportExcel(saveFileDialog2.FileName))
      {
        string empty = string.Empty;
        int num = (int) MessageBox.Show(!BnyCommon.IsMoreInfo() ? "导出EXCEL成功，请您编辑后导入即可\r\n" + "您可以进行如下操作:\r\n" + "1.编辑内容!\r\n" + "2.删除内容!\r\n" + "3.添加新内容!" : "导出EXCEL成功，请您编辑后导入即可\r\n" + "★★请注意★★！！！！\r\n" + "★★您不可以删除记录信息★★，可以到软件中删除！！！！\r\n" + "因为这个文件包含了复杂结构，您可以进行如下操作:\r\n" + "1.编辑内容\r\n" + "2.在★最下方★添加新信息后软件中编辑新信息的结构信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        int num1 = (int) MessageBox.Show("导出EXCEL失败，请通知作者!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }

    private void GenerateTable(StructExplain se, DataTable dt, DataRow dr)
    {
      foreach (DataColumn column in (InternalDataCollectionBase) dt.Columns)
      {
        if (column.DataType == typeof (DataTable))
        {
          StructExplain se1 = BnyUtils.Find(se, column.ColumnName);
          if (se1 != null)
            dr[column.ColumnName] = (object) BnyUtils.GenerateSubTable(se1);
        }
        else if (column.DataType == typeof (List<DataTable>))
        {
          StructExplain structExplain = BnyUtils.Find(se, column.ColumnName);
          if (structExplain != null)
          {
            dr[column.ColumnName] = (object) new List<DataTable>();
            if (structExplain.Structs != null)
            {
              foreach (StructExplain se2 in structExplain.Structs)
                (dr[column.ColumnName] as List<DataTable>).Add(BnyUtils.GenerateSubTable(se2));
            }
          }
        }
      }
      dt.Rows.Add(dr);
      dt.AcceptChanges();
      dt.Rows.RemoveAt(0);
      dt.AcceptChanges();
    }

    private void importexcel_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog1 = new OpenFileDialog();
      openFileDialog1.Title = "请选择保存Excel的地址";
      openFileDialog1.Filter = "*.xls|*.xls";
      openFileDialog1.FileName = this._fileName.Substring(this._fileName.LastIndexOf("\\") + 1).Replace(".bny", ".xls");
      OpenFileDialog openFileDialog2 = openFileDialog1;
      if (openFileDialog2.ShowDialog() != DialogResult.OK)
        return;
      try
      {
        DataTable dataTable = NPOIUtils.ImportExcel(openFileDialog2.FileName);
        if (!BnyCommon.IsMoreInfo())
        {
          BnyCommon.MainTable.Rows.Clear();
          foreach (DataRow row1 in (InternalDataCollectionBase) dataTable.Rows)
          {
            DataRow row2 = BnyCommon.MainTable.NewRow();
            row2.ItemArray = row1.ItemArray;
            BnyCommon.MainTable.Rows.Add(row2);
          }
          BnyCommon.MainTable.AcceptChanges();
          this.FillListView();
          this.InvalidFlag();
        }
        else
        {
          int index1 = 0;
          foreach (DataRow row in (InternalDataCollectionBase) BnyCommon.MainTable.Rows)
          {
            foreach (DataColumn column in (InternalDataCollectionBase) BnyCommon.MainTable.Columns)
            {
              if (column.DataType != typeof (List<DataTable>) && column.DataType != typeof (DataTable))
                row[column.ColumnName] = dataTable.Rows[index1][column.ColumnName];
            }
            ++index1;
          }
          if (dataTable.Rows.Count > index1)
          {
            for (int index2 = index1; index2 < dataTable.Rows.Count; ++index2)
            {
              DataRow row = BnyCommon.MainTable.NewRow();
              foreach (DataColumn column in (InternalDataCollectionBase) BnyCommon.MainTable.Columns)
              {
                if (column.DataType != typeof (List<DataTable>) && column.DataType != typeof (DataTable))
                {
                  row[column.ColumnName] = dataTable.Rows[index2][column.ColumnName];
                }
                else
                {
                  StructExplain se1 = BnyUtils.Find(column.ColumnName.ToString());
                  if (se1.Type == (byte) 12)
                  {
                    DataTable subTable = BnyUtils.GenerateSubTable(se1);
                    DataRow dr = subTable.NewRow();
                    this.GenerateTable(se1, subTable, dr);
                    row[column.ColumnName] = (object) subTable;
                  }
                  else if (se1.Type == (byte) 11)
                  {
                    row[column.ColumnName] = (object) new List<DataTable>();
                    if (se1.Structs != null)
                    {
                      foreach (StructExplain se2 in se1.Structs)
                      {
                        DataTable subTable = BnyUtils.GenerateSubTable(se2);
                        DataRow dr = subTable.NewRow();
                        this.GenerateTable(se1, subTable, dr);
                        (row[column.ColumnName] as List<DataTable>).Add(subTable);
                      }
                    }
                  }
                }
              }
              BnyCommon.MainTable.Rows.Add(row);
            }
          }
          BnyCommon.MainTable.AcceptChanges();
          this.FillListView();
          this.InvalidFlag();
        }
      }
      catch (NPOIException ex)
      {
        int num = (int) MessageBox.Show(ex.Source, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }

    private void deleteselect_Click(object sender, EventArgs e) => this.btnDelete_Click(sender, e);

    private void pastcover_Click(object sender, EventArgs e) => this.DataGirdViewCellPaste();

    private void pastenew_Click(object sender, EventArgs e) => this.DataGirdViewCellNewPaste();

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex == -1 || this.ButtonIndex.IndexOf(e.ColumnIndex) <= -1)
        return;
      DataTable mainTable = BnyCommon.MainTable;
      StructExplain structExplain = BnyUtils.Find(mainTable.Columns[e.ColumnIndex].ColumnName);
      if (structExplain.Type == (byte) 12)
      {
        FrmEditStruct frmEditStruct = new FrmEditStruct(mainTable.Rows[e.RowIndex][e.ColumnIndex] as DataTable, BnyCommon.BnyData.Explains[e.ColumnIndex]);
        int num = (int) frmEditStruct.ShowDialog();
        BnyCommon.MainTable.Rows[e.RowIndex][e.ColumnIndex] = (object) frmEditStruct.DataTableColl[0];
      }
      else
      {
        if (structExplain.Type != (byte) 11)
          return;
        FrmEditStruct frmEditStruct = new FrmEditStruct(mainTable.Rows[e.RowIndex][e.ColumnIndex] as List<DataTable>, BnyCommon.BnyData.Explains[e.ColumnIndex]);
        int num = (int) frmEditStruct.ShowDialog();
        BnyCommon.MainTable.Rows[e.RowIndex][e.ColumnIndex] = (object) frmEditStruct.DataTableColl;
      }
    }

    private void FrmMain_SizeChanged(object sender, EventArgs e)
    {
      this.notice.Left = this.Width - this.notice.Width - 30;
    }

    private void FrmMain_ResizeEnd(object sender, EventArgs e)
    {
      if (this.Width > 832)
        return;
      this.Width = 832;
    }

    private DataRow CreateRow()
    {
      DataRow row = BnyCommon.MainTable.NewRow();
      foreach (DataColumn column in (InternalDataCollectionBase) BnyCommon.MainTable.Columns)
      {
        StructExplain se1 = BnyUtils.Find(column.ColumnName);
        if (column.DataType == typeof (int))
          row[column.ColumnName] = (object) 0;
        else if (column.DataType == typeof (string))
          row[column.ColumnName] = (object) "";
        else if (column.DataType == typeof (byte))
          row[column.ColumnName] = (object) 0;
        else if (column.DataType == typeof (DataTable))
        {
          if (se1 != null)
          {
            DataTable subTable = BnyUtils.GenerateSubTable(se1);
            DataRow dr = subTable.NewRow();
            this.GenerateTable(se1, subTable, dr);
            row[column.ColumnName] = (object) subTable;
          }
        }
        else if (column.DataType == typeof (List<DataTable>) && se1 != null && se1.Structs != null)
        {
          List<DataTable> dataTableList = new List<DataTable>();
          foreach (StructExplain se2 in se1.Structs)
          {
            DataTable subTable = BnyUtils.GenerateSubTable(se2);
            DataRow dr = subTable.NewRow();
            this.GenerateTable(se1, subTable, dr);
            dataTableList.Add(subTable);
          }
          row[column.ColumnName] = (object) dataTableList;
        }
      }
      return row;
    }

    private void btnNew_Click(object sender, EventArgs e)
    {
      DataRow row = this.CreateRow();
      BnyCommon.MainTable.Rows.Add(row);
      BnyCommon.MainTable.AcceptChanges();
      this.dataGridView1.Sort(this.dataGridView1.Columns[0], ListSortDirection.Descending);
      this.dataGridView1.CurrentCell = this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[0];
      this.msg.Text = "共计:" + (object) BnyCommon.MainTable.Rows.Count + "条记录";
    }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 检查是否有选中的行，并且确认是否要删除
            if (this.dataGridView1.SelectedRows == null || this.dataGridView1.SelectedRows.Count <= 0 ||
                MessageBox.Show("您确定要删除选中的数据吗?", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            // 获取排序后的数据视图
            DataView defaultView = BnyCommon.MainTable.DefaultView;
            defaultView.Sort = this.tableSort;
            DataTable table = defaultView.ToTable();

            // 收集选中的行索引
            List<int> source = new List<int>();
            for (int index = this.dataGridView1.SelectedRows.Count - 1; index >= 0; --index)
            {
                source.Add(this.dataGridView1.SelectedRows[index].Index);
            }

            // 计算删除后的当前行索引
            int index1 = -1;
            if (source.Count > 0)
            {
                index1 = source[0] - 1;
            }

            // 删除选中的行
            foreach (int index2 in (IEnumerable<int>)source.OrderByDescending<int, int>((System.Func<int, int>)(x => x)))
            {
                if (index2 >= 0 && index2 < table.Rows.Count)
                {
                    table.Rows.RemoveAt(index2);
                }
            }

            // 调整 index1 以确保其有效性
            if (index1 < 0 && table.Rows.Count > 0)
            {
                index1 = 0;
            }
            else if (index1 >= table.Rows.Count)
            {
                index1 = table.Rows.Count - 1;
            }

            // 更新主表
            BnyCommon.MainTable = table;

            // 重新设置数据源
            object sortedColumn = (object)this.dataGridView1.SortedColumn;
            SortOrder sortOrder = this.dataGridView1.SortOrder;
            this.dataGridView1.DataSource = (object)BnyCommon.MainTable;

            // 重新应用排序
            if (sortedColumn != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.None:
                    case SortOrder.Ascending:
                        this.dataGridView1.Sort(this.dataGridView1.Columns[((DataGridViewColumn)sortedColumn).Name], ListSortDirection.Ascending);
                        break;
                    case SortOrder.Descending:
                        this.dataGridView1.Sort(this.dataGridView1.Columns[((DataGridViewColumn)sortedColumn).Name], ListSortDirection.Descending);
                        break;
                }
            }

            // 设置当前单元格
            if (index1 >= 0 && index1 < this.dataGridView1.Rows.Count)
            {
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index1].Cells[0];
            }

            // 更新消息文本
            this.msg.Text = "共计:" + (object)BnyCommon.MainTable.Rows.Count + "条记录";

            // 无效标记
            this.InvalidFlag();
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
        DataView defaultView = BnyCommon.MainTable.DefaultView;
        defaultView.Sort = this.tableSort;
        DataTable table = defaultView.ToTable();
        List<int> source = new List<int>();
        for (int index = this.dataGridView1.SelectedRows.Count - 1; index >= 0; --index)
          source.Add(this.dataGridView1.SelectedRows[index].Index);
        int index1 = source[0] - 1;
        foreach (int index2 in (IEnumerable<int>) source.OrderByDescending<int, int>((System.Func<int, int>) (x => x)))
          table.Rows.RemoveAt(index2);
        if (index1 < 0 && table.Rows.Count > 0)
          index1 = 0;
        BnyCommon.MainTable = table;
        object sortedColumn = (object) this.dataGridView1.SortedColumn;
        SortOrder sortOrder = this.dataGridView1.SortOrder;
        this.dataGridView1.DataSource = (object) BnyCommon.MainTable;
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
        this.msg.Text = "共计:" + (object) BnyCommon.MainTable.Rows.Count + "条记录";
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

    private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
      int num = (int) MessageBox.Show(BnyCommon.MainTable.Columns[e.ColumnIndex].ColumnName.Desc() + "格式不正确,请您重新输入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private bool CheckVal(int colindex, string data)
    {
      if (BnyCommon.MainTable.Columns[colindex].DataType == typeof (int))
        return int.TryParse(data, out int _);
      return !(BnyCommon.MainTable.Columns[colindex].DataType == typeof (byte)) || byte.TryParse(data, out byte _);
    }

    public void DataGirdViewCellNewPaste()
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
                text2 = text2 + BnyCommon.MainTable.Columns[index].ColumnName.Desc() + ",Excel第" + (object) num1 + "行，格式不正确,请您重新输入\r\n";
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
              if (BnyCommon.MainTable.Columns[index].DataType != typeof (List<DataTable>) && BnyCommon.MainTable.Columns[index].DataType != typeof (DataTable))
                row[index] = (object) strArray2[index];
            }
            BnyCommon.MainTable.Rows.Add(row);
            ++num1;
          }
        }
        this.dataGridView1.CurrentCell = this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[0];
      }
      catch
      {
      }
    }

    public void DataGirdViewCellPaste()
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
        if (index1 == -1 || index2 == -1 || strArray1.Length == 0 || index2 == -1)
          return;
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
                text2 = text2 + BnyCommon.MainTable.Columns[index2].ColumnName.Desc() + ",Excel第" + (object) num1 + "行，格式不正确,请您重新输入\r\n";
                flag2 = true;
              }
            }
            if (flag2)
            {
              int num2 = (int) MessageBox.Show(text2, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
              break;
            }
            for (int index4 = index2; index4 < index2 + strArray2.Length; ++index4)
            {
              if (BnyCommon.MainTable.Columns[index4].DataType != typeof (List<DataTable>) && BnyCommon.MainTable.Columns[index4].DataType != typeof (DataTable))
                this.dataGridView1.Rows[index1].Cells[index4].Value = (object) strArray2[index4 - index2];
            }
            ++index1;
            ++num1;
          }
        }
      }
      catch
      {
      }
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

        private void 转到行ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      FrmGotoLine frmGotoLine = new FrmGotoLine();
      if (frmGotoLine.ShowDialog() != DialogResult.OK || frmGotoLine.line >= this.dataGridView1.Rows.Count)
        return;
      this.dataGridView1.CurrentCell = this.dataGridView1.Rows[frmGotoLine.line - 1].Cells[0];
    }

    private void dataGridView1_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
        e.Effect = DragDropEffects.All;
      else
        e.Effect = DragDropEffects.None;
    }

    private void dataGridView1_DragDrop(object sender, DragEventArgs e)
    {
      this.OpenFile(((Array) e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString());
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
      {
        if (!this.dataGridView1.IsCurrentCellInEditMode)
          return;
        Clipboard.SetText(this.dataGridView1.CurrentCell.Value.ToString());
      }
    }

    private void toolStripMenuItem9_Click(object sender, EventArgs e)
    {
      if (this.dataGridView1.SelectedRows.Count == 0)
        return;
      int index = this.dataGridView1.Rows.Count - 1;
      foreach (DataGridViewRow selectedRow in (BaseCollection) this.dataGridView1.SelectedRows)
      {
        DataRow row = BnyCommon.MainTable.NewRow();
        row.ItemArray = BnyCommon.MainTable.Rows[selectedRow.Index].ItemArray;
        BnyCommon.MainTable.Rows.Add(row);
      }
      this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index].Cells[0];
    }

    private void toolStripMenuItem11_Click(object sender, EventArgs e)
    {
      if (this.dataGridView1.SelectedRows.Count == 0)
        return;
      DataTable data = BnyCommon.MainTable.Clone();
      foreach (DataGridViewRow selectedRow in (BaseCollection) this.dataGridView1.SelectedRows)
      {
        DataRow row = data.NewRow();
        row.ItemArray = BnyCommon.MainTable.Rows[selectedRow.Index].ItemArray;
        data.Rows.Add(row);
      }
      Clipboard.SetData("DataTable", (object) data);
    }

    private void toolStripMenuItem12_Click(object sender, EventArgs e)
    {
      DataTable data = Clipboard.GetData("DataTable") as DataTable;
      if (BnyCommon.MainTable == null || data == null)
        return;
      if (data.Columns.Count != BnyCommon.MainTable.Columns.Count)
      {
        int num1 = (int) MessageBox.Show("您复制的数据与当前打开文件不一致!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        bool flag = true;
        for (int index = 0; index < BnyCommon.MainTable.Columns.Count; ++index)
        {
          if (data.Columns[index].ColumnName != BnyCommon.MainTable.Columns[index].ColumnName || data.Columns[index].DataType != BnyCommon.MainTable.Columns[index].DataType)
          {
            flag = false;
            break;
          }
        }
        if (flag)
        {
          int index1 = this.dataGridView1.Rows.Count - 1;
          for (int index2 = 0; index2 < data.Rows.Count; ++index2)
          {
            DataRow row = BnyCommon.MainTable.NewRow();
            row.ItemArray = data.Rows[index2].ItemArray;
            BnyCommon.MainTable.Rows.Add(row);
          }
          this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index1].Cells[0];
        }
        else
        {
          int num2 = (int) MessageBox.Show("您复制的数据与当前打开文件不一致!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
      }
    }

    private void dataGridView1_ColumnHeaderMouseClick(
      object sender,
      DataGridViewCellMouseEventArgs e)
    {
    }

    private void hotmenu_Click(object sender, EventArgs e)
    {
      SaveFileDialog saveFileDialog1 = new SaveFileDialog();
      saveFileDialog1.Title = "请选择保存地址";
      saveFileDialog1.Filter = "*.bny|*.bny|*.*|*.*";
      saveFileDialog1.FileName = this._fileName;
      SaveFileDialog saveFileDialog2 = saveFileDialog1;
      if (saveFileDialog2.ShowDialog() != DialogResult.OK)
        return;
      BnyUtils.WriteFile(saveFileDialog2.FileName);
      string str = saveFileDialog2.FileName + ".tmp";
      string fileName = saveFileDialog2.FileName;
      HotUpdate.ToHotBnyFile(fileName, str);
      File.Delete(fileName);
      File.Move(str, fileName);
      Application.DoEvents();
      int num = (int) MessageBox.Show("恭喜!保存文件成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void 数值转换ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int num = (int) new FrmI2F().ShowDialog();
    }

    private void toolStripMenuItem14_Click(object sender, EventArgs e)
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

    private void toolStripMenuItem15_Click(object sender, EventArgs e)
    {
      if (this.dataGridView1.SelectedRows.Count == 0)
        return;
      FrmBatchReplace frmBatchReplace = new FrmBatchReplace(BnyCommon.MainTable);
      if (frmBatchReplace.ShowDialog() != DialogResult.OK || frmBatchReplace.LineNumber == 0)
        return;
      foreach (DataGridViewRow selectedRow in (BaseCollection) this.dataGridView1.SelectedRows)
      {
        object[] itemArray = BnyCommon.MainTable.Rows[selectedRow.Index].ItemArray;
        if (itemArray[frmBatchReplace.ColumnNmae] is List<DataTable>)
        {
          List<DataTable> dataTableList1 = itemArray[frmBatchReplace.ColumnNmae] as List<DataTable>;
          List<DataTable> dataTableList2 = BnyCommon.MainTable.Rows[frmBatchReplace.LineNumber - 1].ItemArray[frmBatchReplace.ColumnNmae] as List<DataTable>;
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
          BnyCommon.MainTable.Rows[selectedRow.Index].ItemArray = itemArray;
        }
        else
        {
          itemArray[frmBatchReplace.ColumnNmae] = BnyCommon.MainTable.Rows[frmBatchReplace.LineNumber - 1].ItemArray[frmBatchReplace.ColumnNmae];
          BnyCommon.MainTable.Rows[selectedRow.Index].ItemArray = itemArray;
        }
      }
    }

    private void piliangzhuan_Click(object sender, EventArgs e)
    {
      int num = (int) new FrmBatchHot().ShowDialog();
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      if (new FrmDescEditor(Path.GetFileName(this._fileName).ToLower().Replace(".bny", "")).ShowDialog() != DialogResult.OK)
        return;
      this.OpenFile(this._fileName);
      int num = (int) MessageBox.Show("恭喜您，保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void InvalidFlag()
    {
      if (!(this.显示别名ToolStripMenuItem.Text == "隐藏标记"))
        return;
      this.PaintFlag();
    }

    private void PaintFlag()
    {
      if (FlagUtils.CurrentFlagTable() == null)
        return;
      int row1 = 0;
      foreach (DataGridViewRow row2 in (IEnumerable) this.dataGridView1.Rows)
      {
        for (int index = 0; index < BnyCommon.MainTable.Columns.Count; ++index)
        {
          if (BnyCommon.MainTable.Columns[index].DataType != typeof (DataTable) && BnyCommon.MainTable.Columns[index].DataType != typeof (List<DataTable>))
          {
            string flag = FlagUtils.getFlag(BnyCommon.MainTable.Columns[index].ColumnName, row2.Cells[index].Value.ToString(), index, row1);
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

    private void 显示别名ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.显示别名ToolStripMenuItem.Text == "显示标记")
      {
        this.PaintFlag();
        this.显示别名ToolStripMenuItem.Text = "隐藏标记";
      }
      else
      {
        this.显示别名ToolStripMenuItem.Text = "显示标记";
        foreach (DataGridViewRow row in (IEnumerable) this.dataGridView1.Rows)
        {
          for (int index = 0; index < BnyCommon.MainTable.Columns.Count; ++index)
          {
            if (BnyCommon.MainTable.Columns[index].DataType != typeof (DataTable) && BnyCommon.MainTable.Columns[index].DataType != typeof (List<DataTable>))
              ((DataGridViewTextBoxCellEx) row.Cells[index]).IsShowalias = false;
          }
        }
        this.dataGridView1.Invalidate();
      }
    }

    private void 添加标记ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.dataGridView1.CurrentCell == null || new FrmFlag(this.dataGridView1.CurrentCell.ColumnIndex, this.dataGridView1.CurrentCell.RowIndex, BnyCommon.MainTable.Columns[this.dataGridView1.CurrentCell.ColumnIndex].ColumnName, this.dataGridView1.CurrentCell.Value.ToString()).ShowDialog() != DialogResult.OK)
        return;
      this.InvalidFlag();
      int num = (int) MessageBox.Show("恭喜您，标记添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void 移除标记ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (!FlagUtils.DeleteFlag(BnyCommon.MainTable.Columns[this.dataGridView1.CurrentCell.ColumnIndex].ColumnName, this.dataGridView1.CurrentCell.Value.ToString(), this.dataGridView1.CurrentCell.ColumnIndex, this.dataGridView1.CurrentCell.RowIndex))
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
private void btnReplace_Click(object sender, EventArgs e)
{
    if (string.IsNullOrEmpty(this.txtKey.Text))
    {
        MessageBox.Show("请输入要替换的内容", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }

    string replaceText = this.txtReplaceWith.Text.Trim();
    if (string.IsNullOrEmpty(replaceText))
    {
        MessageBox.Show("请输入替换后的文本", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }

    int searchColIndex = -1;
    if (!(this.cboSearch.Text == "全部"))
    {
        for (int index = 0; index < this.dataGridView1.Columns.Count; ++index)
        {
            if (this.dataGridView1.Columns[index].HeaderText == this.cboSearch.Text)
            {
                searchColIndex = index;
                break;
            }
        }
    }

    for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
    {
        for (int j = 0; j < this.dataGridView1.Columns.Count; j++)
        {
            if (searchColIndex >= 0 && j != searchColIndex)
            {
                continue; // 跳过非目标列
            }

            if (this.dataGridView1.Rows[i].Cells[j].Value != null)
            {
                string cellValue = this.dataGridView1.Rows[i].Cells[j].Value.ToString();
                if (cellValue.Contains(this.txtKey.Text))
                {
                    this.dataGridView1.Rows[i].Cells[j].Value = cellValue.Replace(this.txtKey.Text, replaceText);
                }
            }
        }
    }
}



private void btnReplaceNext_Click(object sender, EventArgs e)
{
    if (string.IsNullOrEmpty(this.txtKey.Text))
    {
        MessageBox.Show("请输入要替换的内容", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }

    string replaceText = this.txtReplaceWith.Text.Trim();
    if (string.IsNullOrEmpty(replaceText))
    {
        MessageBox.Show("请输入替换后的文本", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }

    int searchColIndex = -1;
    if (!(this.cboSearch.Text == "全部"))
    {
        for (int index = 0; index < this.dataGridView1.Columns.Count; ++index)
        {
            if (this.dataGridView1.Columns[index].HeaderText == this.cboSearch.Text)
            {
                searchColIndex = index;
                break;
            }
        }
    }

    if (searchResult.Count == 0)
    {
        for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
        {
            for (int j = 0; j < this.dataGridView1.Columns.Count; j++)
            {
                if (searchColIndex >= 0 && j != searchColIndex)
                {
                    continue; // 跳过非目标列
                }

                if (this.dataGridView1.Rows[i].Cells[j].Value != null)
                {
                    string cellValue = this.dataGridView1.Rows[i].Cells[j].Value.ToString();
                    if (cellValue.Contains(this.txtKey.Text))
                    {
                        searchResult.Add(j);
                        searchResult2.Add(i);
                    }
                }
            }
        }
    }

    if (this.LastSearchIndex >= 0 && this.LastSearchIndex < this.searchResult.Count)
    {
        this.dataGridView1.Rows[this.searchResult2[this.LastSearchIndex]].Cells[this.searchResult[this.LastSearchIndex]].Value =
            this.dataGridView1.Rows[this.searchResult2[this.LastSearchIndex]].Cells[this.searchResult[this.LastSearchIndex]].Value.ToString().Replace(this.txtKey.Text, replaceText);

        if (this.LastSearchIndex == this.searchResult.Count - 1)
        {
            MessageBox.Show("已经替换到了最后一个匹配项", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.LastSearchIndex = 0;
        }
        else
        {
            this.LastSearchIndex++;
        }
    }
    else
    {
        MessageBox.Show("没有找到匹配项", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
    private void FrmMain_Deactivate(object sender, EventArgs e) => this.Text = this.miniText;

    private void FrmMain_Activated(object sender, EventArgs e) => this.Text = this.oldText;

    private void toolStripButton2_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.txtKey.Text))
      {
        int num = (int) MessageBox.Show("请输入搜索条件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        this.searchResult.Clear();
        this.searchResult2.Clear();
        this.searchColIndex = -1;
        this.searchNext.Enabled = false;
        if (!(this.cboSearch.Text == "全部"))
        {
          for (int index = 0; index < this.dataGridView1.Columns.Count; ++index)
          {
            if (this.dataGridView1.Columns[index].HeaderText == this.cboSearch.Text)
            {
              this.searchColIndex = index;
              break;
            }
          }
        }
        for (int index1 = 0; index1 < this.dataGridView1.Rows.Count; ++index1)
        {
          if (this.searchColIndex != -1)
          {
            if (this.dataGridView1.Rows[index1].Cells[this.searchColIndex].Value != null && this.dataGridView1.Rows[index1].Cells[this.searchColIndex].Value.ToString().IndexOf(this.txtKey.Text) != -1)
            {
              this.searchResult.Add(this.searchColIndex);
              this.searchResult2.Add(index1);
            }
          }
          else
          {
            for (int index2 = 0; index2 < this.dataGridView1.Rows[index1].Cells.Count; ++index2)
            {
              if (this.dataGridView1.Rows[index1].Cells[index2].Value != null && this.dataGridView1.Rows[index1].Cells[index2].Value.ToString().IndexOf(this.txtKey.Text) != -1)
              {
                this.searchResult.Add(index2);
                this.searchResult2.Add(index1);
              }
            }
          }
        }
        if (this.searchResult.Count > 0)
        {
          this.LastSearchIndex = 1;
          this.dataGridView1.CurrentCell = this.dataGridView1.Rows[this.searchResult2[0]].Cells[this.searchResult[0]];
        }
        if (this.searchResult.Count <= 1)
          return;
        this.searchNext.Enabled = true;
      }
    }

    private void searchNext_Click(object sender, EventArgs e)
    {
      this.dataGridView1.CurrentCell = this.dataGridView1.Rows[this.searchResult2[this.LastSearchIndex]].Cells[this.searchResult[this.LastSearchIndex]];
      if (this.LastSearchIndex == this.searchResult.Count - 1)
      {
        int num = (int) MessageBox.Show("已经搜索到了最后一个", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        this.LastSearchIndex = 0;
      }
      else
        ++this.LastSearchIndex;
    }

    private void toolStripButton3_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.txtKey.Text))
      {
        int num = (int) MessageBox.Show("请输入搜索条件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        this.searchResult.Clear();
        this.searchColIndex = -1;
        this.searchNext.Enabled = false;
        if (!(this.cboSearch.Text == "全部"))
        {
          for (int index = 0; index < this.dataGridView1.Columns.Count; ++index)
          {
            if (this.dataGridView1.Columns[index].HeaderText == this.cboSearch.Text)
            {
              this.searchColIndex = index;
              break;
            }
          }
        }
        for (int index1 = 0; index1 < this.dataGridView1.Rows.Count; ++index1)
        {
          if (this.searchColIndex != -1)
          {
            if (this.dataGridView1.Rows[index1].Cells[this.searchColIndex].Value != null && this.dataGridView1.Rows[index1].Cells[this.searchColIndex].Value.ToString() == this.txtKey.Text)
            {
              this.searchResult.Add(this.searchColIndex);
              this.searchResult2.Add(index1);
            }
          }
          else
          {
            for (int index2 = 0; index2 < this.dataGridView1.Rows[index1].Cells.Count; ++index2)
            {
              if (this.dataGridView1.Rows[index1].Cells[index2].Value != null && this.dataGridView1.Rows[index1].Cells[index2].Value.ToString() == this.txtKey.Text)
              {
                this.searchResult.Add(index2);
                this.searchResult2.Add(index1);
              }
            }
          }
        }
        if (this.searchResult.Count > 0)
        {
          this.LastSearchIndex = 1;
          this.dataGridView1.CurrentCell = this.dataGridView1.Rows[this.searchResult2[0]].Cells[this.searchResult[0]];
        }
        if (this.searchResult.Count <= 1)
          return;
        this.searchNext.Enabled = true;
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
      this.components = (IContainer) new System.ComponentModel.Container();
      DataGridViewCellStyle gridViewCellStyle = new DataGridViewCellStyle();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FrmMain));
      this.dataGridView1 = new DataGridView();
      this.contextMenuStrip1 = new ContextMenuStrip(this.components);
      this.clipcopy = new ToolStripMenuItem();
      this.pastcover = new ToolStripMenuItem();
      this.pastenew = new ToolStripMenuItem();
      this.toolStripSeparator4 = new ToolStripSeparator();
      this.toolStripMenuItem9 = new ToolStripMenuItem();
      this.toolStripMenuItem11 = new ToolStripMenuItem();
      this.toolStripMenuItem12 = new ToolStripMenuItem();
      this.toolStripSeparator5 = new ToolStripSeparator();
      this.toolStripMenuItem14 = new ToolStripMenuItem();
      this.toolStripMenuItem10 = new ToolStripSeparator();
      this.toolStripMenuItem15 = new ToolStripMenuItem();
      this.toolStripSeparator7 = new ToolStripSeparator();
      this.deleteselect = new ToolStripMenuItem();
      this.toolStripMenuItem16 = new ToolStripSeparator();
      this.显示别名ToolStripMenuItem = new ToolStripMenuItem();
      this.添加标记ToolStripMenuItem = new ToolStripMenuItem();
      this.移除标记ToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripMenuItem17 = new ToolStripSeparator();
      this.showdatamenuitem = new ToolStripMenuItem();
      this.toolTip1 = new ToolTip(this.components);
      this.menuStrip1 = new MenuStrip();
      this.文件FToolStripMenuItem = new ToolStripMenuItem();
      this.打开OToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripMenuItem1 = new ToolStripSeparator();
      this.btnSave = new ToolStripMenuItem();
      this.btnSaveAs = new ToolStripMenuItem();
      this.toolStripSeparator6 = new ToolStripSeparator();
      this.hotmenu = new ToolStripMenuItem();
      this.toolStripMenuItem2 = new ToolStripSeparator();
      this.newgeshi = new ToolStripMenuItem();
      this.toolStripSeparator2 = new ToolStripSeparator();
      this.退出ToolStripMenuItem = new ToolStripMenuItem();
      this.MenuItemSearch = new ToolStripMenuItem();
      this.查找文件FToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripMenuItem4 = new ToolStripSeparator();
      this.查找内容WToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripMenuItem5 = new ToolStripSeparator();
      this.转到行ToolStripMenuItem = new ToolStripMenuItem();
      this.工具ToolStripMenuItem = new ToolStripMenuItem();
      this.热更新补丁包制作ToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripMenuItem13 = new ToolStripSeparator();
      this.piliangzhuan = new ToolStripMenuItem();
      this.toolStripSeparator8 = new ToolStripSeparator();
      this.数值转换ToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripMenuItem6 = new ToolStripMenuItem();
      this.exportexcel = new ToolStripMenuItem();
      this.toolStripMenuItem7 = new ToolStripSeparator();
      this.importexcel = new ToolStripMenuItem();
      this.关于AToolStripMenuItem = new ToolStripMenuItem();
      this.免责声明MToolStripMenuItem = new ToolStripMenuItem();
      this.statusStrip1 = new StatusStrip();
      this.msg = new ToolStripStatusLabel();
      this.filelb = new ToolStripStatusLabel();
      this.lbfiledesc = new ToolStripStatusLabel();
      this.notice = new Label();
      this.toolStrip1 = new ToolStrip();
      this.打开OToolStripButton = new ToolStripButton();
      this.btnSaveT = new ToolStripButton();
      this.btnSaveAst = new ToolStripButton();
      this.btnSaveT2 = new ToolStripButton();
      this.toolStripSeparator = new ToolStripSeparator();
      this.btnNew = new ToolStripButton();
      this.btnDelete = new ToolStripButton();
      this.cleanOrderBtn = new ToolStripButton();
      this.toolStripSeparator9 = new ToolStripSeparator();
      this.toolStripButton1 = new ToolStripButton();
      this.toolStripSeparator3 = new ToolStripSeparator();
      this.btnServer = new ToolStripButton();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.toolStripLabel1 = new ToolStripLabel();
      this.cboSearch = new ToolStripComboBox();
      this.txtKey = new ToolStripTextBox();
      this.toolStripButton2 = new ToolStripButton();
      this.searchNext = new ToolStripButton();
      this.toolStripMenuItem8 = new ToolStripSeparator();
      this.toolStripButton3 = new ToolStripButton();
// 新加
      this.txtReplaceWith = new ToolStripTextBox();

this.btnReplace = new ToolStripButton();
this.btnReplaceNext = new ToolStripButton();
	  
      ((ISupportInitialize) this.dataGridView1).BeginInit();
      this.contextMenuStrip1.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      this.dataGridView1.AllowDrop = true;
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToOrderColumns = true;
      gridViewCellStyle.BackColor = Color.FromArgb(238, 242, 253);
      this.dataGridView1.AlternatingRowsDefaultCellStyle = gridViewCellStyle;
      this.dataGridView1.BackgroundColor = Color.Gainsboro;
      this.dataGridView1.ColumnHeadersHeight = 35;
      this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
      this.dataGridView1.Dock = DockStyle.Fill;
      this.dataGridView1.Location = new Point(0, 50);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.RowHeadersWidth = 55;
      this.dataGridView1.RowTemplate.Height = 23;
      this.dataGridView1.Size = new Size(1303, 636);
      this.dataGridView1.TabIndex = 18;
      this.dataGridView1.CellContentClick += new DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
      this.dataGridView1.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
      this.dataGridView1.DataError += new DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
      this.dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
      this.dataGridView1.Sorted += new EventHandler(this.dataGridView1_Sorted);
      this.dataGridView1.UserDeletingRow += new DataGridViewRowCancelEventHandler(this.dataGridView1_UserDeletingRow);
      this.dataGridView1.DragDrop += new DragEventHandler(this.dataGridView1_DragDrop);
      this.dataGridView1.DragEnter += new DragEventHandler(this.dataGridView1_DragEnter);
      this.contextMenuStrip1.Items.AddRange(new ToolStripItem[19]
      {
        (ToolStripItem) this.clipcopy,
        (ToolStripItem) this.pastcover,
        (ToolStripItem) this.pastenew,
        (ToolStripItem) this.toolStripSeparator4,
        (ToolStripItem) this.toolStripMenuItem9,
        (ToolStripItem) this.toolStripMenuItem11,
        (ToolStripItem) this.toolStripMenuItem12,
        (ToolStripItem) this.toolStripSeparator5,
        (ToolStripItem) this.toolStripMenuItem14,
        (ToolStripItem) this.toolStripMenuItem10,
        (ToolStripItem) this.toolStripMenuItem15,
        (ToolStripItem) this.toolStripSeparator7,
        (ToolStripItem) this.deleteselect,
        (ToolStripItem) this.toolStripMenuItem16,
        (ToolStripItem) this.显示别名ToolStripMenuItem,
        (ToolStripItem) this.添加标记ToolStripMenuItem,
        (ToolStripItem) this.移除标记ToolStripMenuItem,
        (ToolStripItem) this.toolStripMenuItem17,
        (ToolStripItem) this.showdatamenuitem
      });
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new Size(276, 326);
      this.clipcopy.Name = "clipcopy";
      this.clipcopy.ShortcutKeys = Keys.C | Keys.Control;
      this.clipcopy.Size = new Size(275, 22);
      this.clipcopy.Text = "复制";
      this.clipcopy.Click += new EventHandler(this.clipcopy_Click);
      this.pastcover.Name = "pastcover";
      this.pastcover.ShortcutKeys = Keys.V | Keys.Control;
      this.pastcover.Size = new Size(275, 22);
      this.pastcover.Text = "粘贴";
      this.pastcover.Click += new EventHandler(this.pastcover_Click);
      this.pastenew.Name = "pastenew";
      this.pastenew.ShortcutKeys = Keys.V | Keys.Shift | Keys.Control;
      this.pastenew.Size = new Size(275, 22);
      this.pastenew.Text = "粘贴为新建";
      this.pastenew.Click += new EventHandler(this.pastenew_Click);
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new Size(272, 6);
      this.toolStripMenuItem9.Name = "toolStripMenuItem9";
      this.toolStripMenuItem9.ShortcutKeys = Keys.C | Keys.Shift | Keys.Control;
      this.toolStripMenuItem9.Size = new Size(275, 22);
      this.toolStripMenuItem9.Text = "复制带结构内容到新建";
      this.toolStripMenuItem9.Click += new EventHandler(this.toolStripMenuItem9_Click);
      this.toolStripMenuItem11.Name = "toolStripMenuItem11";
      this.toolStripMenuItem11.Size = new Size(275, 22);
      this.toolStripMenuItem11.Text = "复制带结构内容";
      this.toolStripMenuItem11.Click += new EventHandler(this.toolStripMenuItem11_Click);
      this.toolStripMenuItem12.Name = "toolStripMenuItem12";
      this.toolStripMenuItem12.Size = new Size(275, 22);
      this.toolStripMenuItem12.Text = "粘贴带结构内容";
      this.toolStripMenuItem12.Click += new EventHandler(this.toolStripMenuItem12_Click);
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new Size(272, 6);
      this.toolStripMenuItem14.Name = "toolStripMenuItem14";
      this.toolStripMenuItem14.Size = new Size(275, 22);
      this.toolStripMenuItem14.Text = "转换为浮点数";
      this.toolStripMenuItem14.Click += new EventHandler(this.toolStripMenuItem14_Click);
      this.toolStripMenuItem10.Name = "toolStripMenuItem10";
      this.toolStripMenuItem10.Size = new Size(272, 6);
      this.toolStripMenuItem15.Name = "toolStripMenuItem15";
      this.toolStripMenuItem15.Size = new Size(275, 22);
      this.toolStripMenuItem15.Text = "批量替换";
      this.toolStripMenuItem15.Click += new EventHandler(this.toolStripMenuItem15_Click);
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new Size(272, 6);
      this.deleteselect.Name = "deleteselect";
      this.deleteselect.ShortcutKeys = Keys.D | Keys.Control;
      this.deleteselect.Size = new Size(275, 22);
      this.deleteselect.Text = "删除选中";
      this.deleteselect.Click += new EventHandler(this.deleteselect_Click);
      this.toolStripMenuItem16.Name = "toolStripMenuItem16";
      this.toolStripMenuItem16.Size = new Size(272, 6);
      this.显示别名ToolStripMenuItem.Name = "显示别名ToolStripMenuItem";
      this.显示别名ToolStripMenuItem.ShortcutKeys = Keys.B | Keys.Control;
      this.显示别名ToolStripMenuItem.Size = new Size(275, 22);
      this.显示别名ToolStripMenuItem.Text = "显示标记";
      this.显示别名ToolStripMenuItem.Click += new EventHandler(this.显示别名ToolStripMenuItem_Click);
      this.添加标记ToolStripMenuItem.Name = "添加标记ToolStripMenuItem";
      this.添加标记ToolStripMenuItem.Size = new Size(275, 22);
      this.添加标记ToolStripMenuItem.Text = "添加编辑标记";
      this.添加标记ToolStripMenuItem.Click += new EventHandler(this.添加标记ToolStripMenuItem_Click);
      this.移除标记ToolStripMenuItem.Name = "移除标记ToolStripMenuItem";
      this.移除标记ToolStripMenuItem.Size = new Size(275, 22);
      this.移除标记ToolStripMenuItem.Text = "移除标记";
      this.移除标记ToolStripMenuItem.Click += new EventHandler(this.移除标记ToolStripMenuItem_Click);
      this.toolStripMenuItem17.Name = "toolStripMenuItem17";
      this.toolStripMenuItem17.Size = new Size(272, 6);
      this.showdatamenuitem.Name = "showdatamenuitem";
      this.showdatamenuitem.Size = new Size(275, 22);
      this.showdatamenuitem.Text = "此列数据显示为";
      this.menuStrip1.Items.AddRange(new ToolStripItem[5]
      {
        (ToolStripItem) this.文件FToolStripMenuItem,
        (ToolStripItem) this.MenuItemSearch,
        (ToolStripItem) this.工具ToolStripMenuItem,
        (ToolStripItem) this.toolStripMenuItem6,
        (ToolStripItem) this.关于AToolStripMenuItem
      });
      this.menuStrip1.Location = new Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new Size(1303, 25);
      this.menuStrip1.TabIndex = 11;
      this.menuStrip1.Text = "menuStrip1";
      this.文件FToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[10]
      {
        (ToolStripItem) this.打开OToolStripMenuItem,
        (ToolStripItem) this.toolStripMenuItem1,
        (ToolStripItem) this.btnSave,
        (ToolStripItem) this.btnSaveAs,
        (ToolStripItem) this.toolStripSeparator6,
        (ToolStripItem) this.hotmenu,
        (ToolStripItem) this.toolStripMenuItem2,
        (ToolStripItem) this.newgeshi,
        (ToolStripItem) this.toolStripSeparator2,
        (ToolStripItem) this.退出ToolStripMenuItem
      });
      this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
      this.文件FToolStripMenuItem.Size = new Size(58, 21);
      this.文件FToolStripMenuItem.Text = "文件(&F)";
      this.打开OToolStripMenuItem.Name = "打开OToolStripMenuItem";
      this.打开OToolStripMenuItem.ShortcutKeys = Keys.O | Keys.Control;
      this.打开OToolStripMenuItem.Size = new Size(190, 22);
      this.打开OToolStripMenuItem.Text = "打开";
      this.打开OToolStripMenuItem.Click += new EventHandler(this.button1_Click);
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new Size(187, 6);
      this.btnSave.Enabled = false;
      this.btnSave.Name = "btnSave";
      this.btnSave.ShortcutKeys = Keys.S | Keys.Control;
      this.btnSave.Size = new Size(190, 22);
      this.btnSave.Text = "保存";
      this.btnSave.Click += new EventHandler(this.btnSave_Click);
      this.btnSaveAs.Enabled = false;
      this.btnSaveAs.Name = "btnSaveAs";
      this.btnSaveAs.ShortcutKeys = Keys.S | Keys.Shift | Keys.Control;
      this.btnSaveAs.Size = new Size(190, 22);
      this.btnSaveAs.Text = "另存为";
      this.btnSaveAs.Click += new EventHandler(this.btnSaveAs_Click);
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new Size(187, 6);
      this.hotmenu.Enabled = false;
      this.hotmenu.Name = "hotmenu";
      this.hotmenu.Size = new Size(190, 22);
      this.hotmenu.Text = "保存为热更新";
      this.hotmenu.Click += new EventHandler(this.hotmenu_Click);
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new Size(187, 6);
      this.newgeshi.Enabled = false;
      this.newgeshi.Name = "newgeshi";
      this.newgeshi.Size = new Size(190, 22);
      this.newgeshi.Text = "保存为老版本";
      this.newgeshi.Click += new EventHandler(this.newgeshi_Click);
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new Size(187, 6);
      this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
      this.退出ToolStripMenuItem.ShortcutKeys = Keys.F4 | Keys.Alt;
      this.退出ToolStripMenuItem.Size = new Size(190, 22);
      this.退出ToolStripMenuItem.Text = "退出";
      this.MenuItemSearch.DropDownItems.AddRange(new ToolStripItem[5]
      {
        (ToolStripItem) this.查找文件FToolStripMenuItem,
        (ToolStripItem) this.toolStripMenuItem4,
        (ToolStripItem) this.查找内容WToolStripMenuItem,
        (ToolStripItem) this.toolStripMenuItem5,
        (ToolStripItem) this.转到行ToolStripMenuItem
      });
      this.MenuItemSearch.Name = "MenuItemSearch";
      this.MenuItemSearch.Size = new Size(59, 21);
      this.MenuItemSearch.Text = "搜索(&S)";
      this.查找文件FToolStripMenuItem.Name = "查找文件FToolStripMenuItem";
      this.查找文件FToolStripMenuItem.ShortcutKeys = Keys.F | Keys.Control;
      this.查找文件FToolStripMenuItem.Size = new Size(201, 22);
      this.查找文件FToolStripMenuItem.Text = "查找文件";
      this.查找文件FToolStripMenuItem.Click += new EventHandler(this.查找文件FToolStripMenuItem_Click);
      this.toolStripMenuItem4.Name = "toolStripMenuItem4";
      this.toolStripMenuItem4.Size = new Size(198, 6);
      this.查找内容WToolStripMenuItem.Name = "查找内容WToolStripMenuItem";
      this.查找内容WToolStripMenuItem.ShortcutKeys = Keys.F | Keys.Shift | Keys.Control;
      this.查找内容WToolStripMenuItem.Size = new Size(201, 22);
      this.查找内容WToolStripMenuItem.Text = "查找内容";
      this.查找内容WToolStripMenuItem.Click += new EventHandler(this.查找内容WToolStripMenuItem_Click);
      this.toolStripMenuItem5.Name = "toolStripMenuItem5";
      this.toolStripMenuItem5.Size = new Size(198, 6);
      this.转到行ToolStripMenuItem.Name = "转到行ToolStripMenuItem";
      this.转到行ToolStripMenuItem.ShortcutKeys = Keys.G | Keys.Control;
      this.转到行ToolStripMenuItem.Size = new Size(201, 22);
      this.转到行ToolStripMenuItem.Text = "转到行";
      this.转到行ToolStripMenuItem.Click += new EventHandler(this.转到行ToolStripMenuItem_Click);
      this.工具ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[5]
      {
        (ToolStripItem) this.热更新补丁包制作ToolStripMenuItem,
        (ToolStripItem) this.toolStripMenuItem13,
        (ToolStripItem) this.piliangzhuan,
        (ToolStripItem) this.toolStripSeparator8,
        (ToolStripItem) this.数值转换ToolStripMenuItem
      });
      this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
      this.工具ToolStripMenuItem.Size = new Size(59, 21);
      this.工具ToolStripMenuItem.Text = "工具(&T)";
      this.热更新补丁包制作ToolStripMenuItem.Name = "热更新补丁包制作ToolStripMenuItem";
      this.热更新补丁包制作ToolStripMenuItem.ShortcutKeys = Keys.D | Keys.Control;
      this.热更新补丁包制作ToolStripMenuItem.Size = new Size(247, 22);
      this.热更新补丁包制作ToolStripMenuItem.Text = "热更新包制作";
      this.热更新补丁包制作ToolStripMenuItem.Click += new EventHandler(this.button4_Click_1);
      this.toolStripMenuItem13.Name = "toolStripMenuItem13";
      this.toolStripMenuItem13.Size = new Size(244, 6);
      this.piliangzhuan.Name = "piliangzhuan";
      this.piliangzhuan.Size = new Size(247, 22);
      this.piliangzhuan.Text = "批量转换【热更新<=>源文件】";
      this.piliangzhuan.Click += new EventHandler(this.piliangzhuan_Click);
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new Size(244, 6);
      this.数值转换ToolStripMenuItem.Name = "数值转换ToolStripMenuItem";
      this.数值转换ToolStripMenuItem.ShortcutKeys = Keys.G | Keys.Shift | Keys.Control;
      this.数值转换ToolStripMenuItem.Size = new Size(247, 22);
      this.数值转换ToolStripMenuItem.Text = "数值转换";
      this.数值转换ToolStripMenuItem.Click += new EventHandler(this.数值转换ToolStripMenuItem_Click);
      this.toolStripMenuItem6.DropDownItems.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.exportexcel,
        (ToolStripItem) this.toolStripMenuItem7,
        (ToolStripItem) this.importexcel
      });
      this.toolStripMenuItem6.Name = "toolStripMenuItem6";
      this.toolStripMenuItem6.Size = new Size(83, 21);
      this.toolStripMenuItem6.Text = "批量编辑(&P)";
      this.exportexcel.Enabled = false;
      this.exportexcel.Name = "exportexcel";
      this.exportexcel.ShortcutKeys = Keys.O | Keys.Shift | Keys.Control;
      this.exportexcel.Size = new Size(253, 22);
      this.exportexcel.Text = "导出到EXCEL编辑";
      this.exportexcel.Click += new EventHandler(this.exportexcel_Click);
      this.toolStripMenuItem7.Name = "toolStripMenuItem7";
      this.toolStripMenuItem7.Size = new Size(250, 6);
      this.importexcel.Enabled = false;
      this.importexcel.Name = "importexcel";
      this.importexcel.ShortcutKeys = Keys.I | Keys.Shift | Keys.Control;
      this.importexcel.Size = new Size(253, 22);
      this.importexcel.Text = "导入EXCEL内容";
      this.importexcel.Click += new EventHandler(this.importexcel_Click);
      this.关于AToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.免责声明MToolStripMenuItem
      });
      this.关于AToolStripMenuItem.Name = "关于AToolStripMenuItem";
      this.关于AToolStripMenuItem.Size = new Size(60, 21);
      this.关于AToolStripMenuItem.Text = "关于(&A)";
      this.免责声明MToolStripMenuItem.Name = "免责声明MToolStripMenuItem";
      this.免责声明MToolStripMenuItem.ShortcutKeys = Keys.M | Keys.Control;
      this.免责声明MToolStripMenuItem.Size = new Size(173, 22);
      this.免责声明MToolStripMenuItem.Text = "免责声明";
      this.免责声明MToolStripMenuItem.Click += new EventHandler(this.免责声明MToolStripMenuItem_Click);
      this.statusStrip1.Items.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.msg,
        (ToolStripItem) this.filelb,
        (ToolStripItem) this.lbfiledesc
      });
      this.statusStrip1.Location = new Point(0, 686);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.ShowItemToolTips = true;
      this.statusStrip1.Size = new Size(1303, 22);
      this.statusStrip1.TabIndex = 13;
      this.statusStrip1.Text = "statusStrip1";
      this.msg.Name = "msg";
      this.msg.Size = new Size(101, 17);
      this.msg.Text = "正在初始化软件...";
      this.filelb.AutoToolTip = true;
      this.filelb.BorderSides = ToolStripStatusLabelBorderSides.Left;
      this.filelb.ForeColor = Color.Blue;
      this.filelb.Name = "filelb";
      this.filelb.Size = new Size(4, 17);
      this.lbfiledesc.BorderSides = ToolStripStatusLabelBorderSides.Left;
      this.lbfiledesc.ForeColor = Color.Red;
      this.lbfiledesc.Name = "lbfiledesc";
      this.lbfiledesc.Size = new Size(4, 17);
      this.notice.AutoSize = true;
      this.notice.BackColor = Color.FromArgb(248, 248, 248);
      this.notice.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte) 134);
      this.notice.ForeColor = Color.Red;
      this.notice.Location = new Point(1283, 9);
      this.notice.Name = "notice";
      this.notice.Size = new Size(0, 12);
      this.notice.TabIndex = 14;
      this.notice.TextAlign = ContentAlignment.MiddleRight;
      this.toolStrip1.Items.AddRange(new ToolStripItem[22]
      {
        (ToolStripItem) this.打开OToolStripButton,
        (ToolStripItem) this.btnSaveT,
        (ToolStripItem) this.btnSaveAst,
        (ToolStripItem) this.btnSaveT2,
        (ToolStripItem) this.toolStripSeparator,
        (ToolStripItem) this.btnNew,
        (ToolStripItem) this.btnDelete,
        (ToolStripItem) this.cleanOrderBtn,
        (ToolStripItem) this.toolStripSeparator9,
        (ToolStripItem) this.toolStripButton1,
        (ToolStripItem) this.toolStripSeparator3,
        (ToolStripItem) this.btnServer,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.toolStripLabel1,
        (ToolStripItem) this.cboSearch,
        (ToolStripItem) this.txtKey,
        (ToolStripItem) this.toolStripButton3,
        (ToolStripItem) this.toolStripButton2,
		        (ToolStripItem) this.searchNext,
                (ToolStripItem) this.txtReplaceWith, // 添加输入框
        (ToolStripItem) this.btnReplace,
        (ToolStripItem) this.btnReplaceNext

		
      });
      this.toolStrip1.Location = new Point(0, 25);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new Size(1303, 25);
      this.toolStrip1.TabIndex = 15;
      this.toolStrip1.Text = "toolStrip1";
      this.打开OToolStripButton.Image = (Image) BnyEditor_Margin.Properties.Resources.打开OToolStripButton;
      this.打开OToolStripButton.ImageTransparentColor = Color.Magenta;
      this.打开OToolStripButton.Name = "打开OToolStripButton";
      this.打开OToolStripButton.Size = new Size(70, 22);
      this.打开OToolStripButton.Text = "打开(&O)";
      this.打开OToolStripButton.Click += new EventHandler(this.button1_Click);
      this.btnSaveT.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.btnSaveT.Enabled = false;
      this.btnSaveT.Image = (Image) BnyEditor_Margin.Properties.Resources.btnSaveT;
      this.btnSaveT.ImageTransparentColor = Color.Magenta;
      this.btnSaveT.Name = "btnSaveT";
      this.btnSaveT.Size = new Size(23, 22);
      this.btnSaveT.Text = "保存(&S)";
      this.btnSaveT.Click += new EventHandler(this.btnSave_Click);
      this.btnSaveAst.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.btnSaveAst.Enabled = false;
      this.btnSaveAst.Image = (Image) BnyEditor_Margin.Properties.Resources.btnSaveAst;
      this.btnSaveAst.ImageTransparentColor = Color.Magenta;
      this.btnSaveAst.Name = "btnSaveAst";
      this.btnSaveAst.Size = new Size(23, 22);
      this.btnSaveAst.Text = "另存为";
      this.btnSaveAst.Click += new EventHandler(this.btnSaveAs_Click);
      this.btnSaveT2.Enabled = false;
      this.btnSaveT2.Image = (Image) BnyEditor_Margin.Properties.Resources.btnSaveT;
      this.btnSaveT2.ImageTransparentColor = Color.Magenta;
      this.btnSaveT2.Name = "btnSaveT2";
      this.btnSaveT2.Size = new Size(136, 22);
      this.btnSaveT2.Text = "按排序后的顺序保存";
      this.btnSaveT2.ToolTipText = "按排序后的顺序保存";
      this.btnSaveT2.Click += new EventHandler(this.btnSave2_Click);
      this.toolStripSeparator.Name = "toolStripSeparator";
      this.toolStripSeparator.Size = new Size(6, 25);
      this.btnNew.Enabled = false;
      this.btnNew.Image = (Image) BnyEditor_Margin.Properties.Resources.btnNew;
      this.btnNew.ImageTransparentColor = Color.Magenta;
      this.btnNew.Name = "btnNew";
      this.btnNew.Size = new Size(52, 22);
      this.btnNew.Text = "新增";
      this.btnNew.Click += new EventHandler(this.btnNew_Click);
      this.btnDelete.Enabled = false;
      this.btnDelete.Image = (Image) BnyEditor_Margin.Properties.Resources.btnDelete;
      this.btnDelete.ImageTransparentColor = Color.Magenta;
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new Size(52, 22);
      this.btnDelete.Text = "删除";
      this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
      this.cleanOrderBtn.ImageTransparentColor = Color.Magenta;
      this.cleanOrderBtn.Name = "cleanOrderBtn";
      this.cleanOrderBtn.Size = new Size(60, 22);
      this.cleanOrderBtn.Text = "清除排序";
      this.cleanOrderBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
      this.cleanOrderBtn.Visible = false;
      this.cleanOrderBtn.Click += new EventHandler(this.cleanOrderBtn_Click);
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new Size(6, 25);
      this.toolStripButton1.Enabled = false;
      this.toolStripButton1.Image = (Image) BnyEditor_Margin.Properties.Resources.toolStripButton1;
      this.toolStripButton1.ImageTransparentColor = Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new Size(112, 22);
      this.toolStripButton1.Text = "自定义文件提示";
      this.toolStripButton1.Click += new EventHandler(this.toolStripButton1_Click);
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new Size(6, 25);
      this.btnServer.Image = (Image) BnyEditor_Margin.Properties.Resources.btnServer;
      this.btnServer.ImageTransparentColor = Color.Magenta;
      this.btnServer.Name = "btnServer";
      this.btnServer.Size = new Size(114, 22);
      this.btnServer.Text = "导出服务端XML";
      this.btnServer.Visible = false;
      this.btnServer.Click += new EventHandler(this.btnServer_Click);
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(6, 25);
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new Size(59, 22);
      this.toolStripLabel1.Text = "搜索内容:";
      this.cboSearch.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboSearch.Name = "cboSearch";
      this.cboSearch.Size = new Size(130, 25);
      this.txtKey.Name = "txtKey";
      this.txtKey.Size = new Size(100, 25);
this.txtKey.Enter += new EventHandler(txtKey_Enter);
this.txtKey.Leave += new EventHandler(txtKey_Leave);
      this.toolStripButton2.Image = (Image) BnyEditor_Margin.Properties.Resources.toolStripButton2;
      this.toolStripButton2.ImageTransparentColor = Color.Magenta;
      this.toolStripButton2.Name = "toolStripButton2";
      this.toolStripButton2.RightToLeft = RightToLeft.No;
      this.toolStripButton2.Size = new Size(76, 22);
      this.toolStripButton2.Text = "模糊搜索";
      this.toolStripButton2.ToolTipText = "模糊搜索";
      this.toolStripButton2.Click += new EventHandler(this.toolStripButton2_Click);
      this.searchNext.Image = (Image) BnyEditor_Margin.Properties.Resources.toolStripButton2;
      this.searchNext.ImageTransparentColor = Color.Magenta;
      this.searchNext.Name = "searchNext";
      this.searchNext.Size = new Size(88, 22);
      this.searchNext.Text = "搜索下一个";
      this.searchNext.Click += new EventHandler(this.searchNext_Click);
      this.toolStripMenuItem8.Name = "toolStripMenuItem8";
      this.toolStripMenuItem8.Size = new Size(212, 6);
      this.toolStripButton3.Image = (Image) BnyEditor_Margin.Properties.Resources.toolStripButton2;
      this.toolStripButton3.ImageTransparentColor = Color.Magenta;
      this.toolStripButton3.Name = "toolStripButton3";
      this.toolStripButton3.Size = new Size(52, 22);
      this.toolStripButton3.Text = "搜索";
      this.toolStripButton3.Click += new EventHandler(this.toolStripButton3_Click);


      this.txtReplaceWith.Name = "txtReplaceWith";
      this.txtReplaceWith.Size = new Size(100, 25);
this.txtReplaceWith.Enter += new EventHandler(txtReplaceWith_Enter);
this.txtReplaceWith.Leave += new EventHandler(txtReplaceWith_Leave);



this.btnReplace.Image = (Image) BnyEditor_Margin.Properties.Resources.toolStripButton2;
this.btnReplace.ImageTransparentColor = Color.Magenta;
this.btnReplace.Name = "btnReplace";
this.btnReplace.Size = new Size(52, 22);
this.btnReplace.Text = "替换";
this.btnReplace.Click += new EventHandler(this.btnReplace_Click);

// 添加替换下一个按钮

this.btnReplaceNext.Image = (Image) BnyEditor_Margin.Properties.Resources.toolStripButton2;
this.btnReplaceNext.ImageTransparentColor = Color.Magenta;
this.btnReplaceNext.Name = "btnReplaceNext";
this.btnReplaceNext.Size = new Size(88, 22);
this.btnReplaceNext.Text = "替换下一个";
this.btnReplaceNext.Click += new EventHandler(this.btnReplaceNext_Click);



      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(1303, 708);
      this.Controls.Add((Control) this.dataGridView1);
      this.Controls.Add((Control) this.toolStrip1);
      this.Controls.Add((Control) this.notice);
      this.Controls.Add((Control) this.statusStrip1);
      this.Controls.Add((Control) this.menuStrip1);


      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MainMenuStrip = this.menuStrip1;
      this.Name = nameof (FrmMain);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "逍遥BNY文件编辑器 ";
      this.Activated += new EventHandler(this.FrmMain_Activated);
      this.Deactivate += new EventHandler(this.FrmMain_Deactivate);
      this.Load += new EventHandler(this.FrmMain_Load);
      this.ResizeEnd += new EventHandler(this.FrmMain_ResizeEnd);
      this.SizeChanged += new EventHandler(this.FrmMain_SizeChanged);
      ((ISupportInitialize) this.dataGridView1).EndInit();
      this.contextMenuStrip1.ResumeLayout(false);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
