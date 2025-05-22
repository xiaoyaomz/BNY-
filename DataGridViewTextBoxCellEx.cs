// Decompiled with JetBrains decompiler
// Type: BnyEditor.DataGridViewTextBoxCellEx
// Assembly: BnyEditor_Margin, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D603A8BF-7840-4117-8ECA-12CCC35D8950
// Assembly location: E:\架设\梦幻诛仙\笔记\梦诛工具\BnyEditor带搜索\BnyEditor\BnyEditor.exe

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace BnyEditor
{
  public class DataGridViewTextBoxCellEx : DataGridViewTextBoxCell
  {
    private bool _isshow;

    public bool IsShowalias
    {
      get => this._isshow;
      set => this._isshow = value;
    }

    public string Alias { get; set; }

    public Color DrawColor { get; set; } = Color.Red;

    protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates cellState,
      object value,
      object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
    {
      base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
      if (!this._isshow)
        return;
      GraphicsContainer container = graphics.BeginContainer();
      graphics.SetClip(cellBounds);
      SizeF sizeF = graphics.MeasureString(value.ToString(), cellStyle.Font);
      graphics.DrawRectangle(new Pen(Color.White), cellBounds);
      int y = (int) ((double) cellBounds.Height - (double) sizeF.Height) / 2 + cellBounds.Location.Y;
      graphics.DrawString(this.Alias, cellStyle.Font, (Brush) new SolidBrush(this.DrawColor), (PointF) new Point((int) ((double) cellBounds.Location.X + (double) sizeF.Width + 5.0), y));
      graphics.EndContainer(container);
    }
  }
}
