// Decompiled with JetBrains decompiler
// Type: BnyEditor.FrmSplash
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using BnyEditor_Margin.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace BnyEditor
{
  public class FrmSplash : Form
  {
    private IContainer components;
    private Label msg;
    private Timer timer1;
    private Timer timer2;
    private Label label1;

    public string Notice { get; set; }

    public string Result { get; set; }

    public FrmSplash() => this.InitializeComponent();

    private void Storm()
    {
      this.msg.Text = "正在加载软件信息...";
      Application.DoEvents();
      try
      {
        ComputerInfo.GetComputerInfo();
        BnyCommon.machineCode = CheckSerialNumber.machineCode;
        if (!CheckSerialNumber.CheckCode())
        {
          FrmRegister frmRegister = new FrmRegister();
          frmRegister.TopMost = true;
          frmRegister.TopLevel = true;
          int num = (int) frmRegister.ShowDialog();
          Application.Exit();
          Environment.Exit(0);
        }
        else
          this.DialogResult = DialogResult.OK;
      }
      catch
      {
        Application.Exit();
        Environment.Exit(0);
      }
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      this.timer1.Enabled = false;
      this.Storm();
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
      this.msg = new Label();
      this.timer1 = new Timer(this.components);
      this.timer2 = new Timer(this.components);
      this.label1 = new Label();
      this.SuspendLayout();
      this.msg.AutoSize = true;
      this.msg.BackColor = Color.Transparent;
      this.msg.Font = new Font("宋体", 10f, FontStyle.Bold, GraphicsUnit.Point, (byte) 134);
      this.msg.ForeColor = Color.White;
      this.msg.Location = new Point(12, 234);
      this.msg.Name = "msg";
      this.msg.Size = new Size(0, 14);
      this.msg.TabIndex = 0;
      this.timer1.Enabled = true;
      this.timer1.Interval = 1000;
      this.timer1.Tick += new EventHandler(this.timer1_Tick);
      this.timer2.Interval = 500;
      this.label1.AutoSize = true;
      this.label1.Font = new Font("宋体", 16f);
      this.label1.Location = new Point(13, 12);
      this.label1.Name = "label1";
      this.label1.Size = new Size(472, 242);
      this.label1.TabIndex = 1;
      this.label1.Text = "买的时候说是包更新的\r\n现在不给更新只能发出来大家一起用了\r\n牛说默念是狗还不信，现在发现默念果然是狗\r\n\r\n买的时候说的好好的，现在说\r\n“难道你去4s店买车有新款了还给你换新的吗”\r\n\r\n有其他默念软件的可以发出来\r\n帮你们破解成无限制版本。\r\n需要源码的可以加群，有开发能力者送源码\r\n加群送源码：961553363";
      this.label1.TextAlign = ContentAlignment.TopCenter;
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackgroundImage = (Image) Resources.Background;
      this.ClientSize = new Size(498, 266);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.msg);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (FrmSplash);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
