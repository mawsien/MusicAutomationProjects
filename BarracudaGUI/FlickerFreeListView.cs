using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
// GListBoxItem class 
public class GListBoxItem
{
    private string _myText;
    private int _myImageIndex;
    // properties 
    public string Text
    {
        get { return _myText; }
        set { _myText = value; }
    }
    public int ImageIndex
    {
        get { return _myImageIndex; }
        set { _myImageIndex = value; }
    }
    //constructor
    public GListBoxItem(string text, int index)
    {
        _myText = text;
        _myImageIndex = index;
    }
    public GListBoxItem(string text) : this(text, -1) { }
    public GListBoxItem() : this("") { }
    public override string ToString()
    {
        return _myText;
    }
}//End of GListBoxItem class

public class FlickerFreeListView : ListView 
{
    private ImageList _myImageList;
    public ImageList ImageList
    {
        get { return _myImageList; }
        set { _myImageList = value; }
    }

    public FlickerFreeListView() : base() 
    { 
        this.DoubleBuffered = true;
        //this.DrawMode = DrawMode.OwnerDrawFixed;

    }
  /*  protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
    {
        e.DrawBackground();
        e.DrawFocusRectangle();
        GListBoxItem item;
        Rectangle bounds = e.Bounds;
        Size imageSize = _myImageList.ImageSize;
        try
        {
            item = (GListBoxItem)Items[e.Index];
            if (item.ImageIndex != -1)
            {
                imageList.Draw(e.Graphics, bounds.Left, bounds.Top, item.ImageIndex);
                e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(e.ForeColor),
                    bounds.Left + imageSize.Width, bounds.Top);
            }
            else
            {
                e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(e.ForeColor),
                    bounds.Left, bounds.Top);
            }
        }
        catch
        {
            if (e.Index != -1)
            {
                e.Graphics.DrawString(Items[e.Index].ToString(), e.Font,
                    new SolidBrush(e.ForeColor), bounds.Left, bounds.Top);
            }
            else
            {
                e.Graphics.DrawString(Text, e.Font, new SolidBrush(e.ForeColor),
                    bounds.Left, bounds.Top);
            }
        }
        base.OnDrawItem(e);
    }*/

}