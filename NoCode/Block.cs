using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NoCode
{
    public class Block
    {
        private Point location;
        private Size size;
        private string name="";


        private List<Parameter> inputParaList = new List<Parameter>();
        private List<Parameter> outPutParaList = new List<Parameter>();

        public List<Parameter> InputParaList
        {
            get { return inputParaList; }
        }

        public List<Parameter> OutPutParaList
        {
            get { return outPutParaList; }
        }

        public Size Size
        {
            get { return size; }
            set { size = value; }
        }
        

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Point Location
        {
            get { return location; }
            set { location = value; }
        }


        public Image Paint()
        {
            Bitmap bmp = new Bitmap(NetworkControl.ColumnWidth*(this.size.Width+2) + 1,
                NetworkControl.RowHeight*this.size.Height + 1);
           
            Graphics g = Graphics.FromImage(bmp);

            g.FillRectangle(Brushes.White, NetworkControl.ColumnWidth, 0, NetworkControl.ColumnWidth*this.size.Width + 1,
                NetworkControl.RowHeight*this.size.Height + 1);
            g.DrawRectangle(Pens.Black, NetworkControl.ColumnWidth, 0, NetworkControl.ColumnWidth*this.size.Width,
                NetworkControl.RowHeight*this.size.Height);

            SizeF namesize = g.MeasureString(this.name, SystemFonts.DefaultFont);

            g.DrawString(this.name, SystemFonts.DefaultFont, Brushes.Black,
                new PointF(NetworkControl.ColumnWidth * (this.size.Width + 2) / 2.0f - namesize.Width / 2.0f, NetworkControl.RowHeight / 2.0f - namesize.Height / 2.0f));

            for(int i=0;i<inputParaList.Count;i++)
            {
                g.DrawImageUnscaled(inputParaList[i].Paint(), 0, NetworkControl.RowHeight*(i + 1));
            }

            for (int i = 0; i < outPutParaList.Count; i++)
            {
                g.DrawImageUnscaled(outPutParaList[i].Paint(), NetworkControl.ColumnWidth * (this.size.Width + 1 -1), NetworkControl.RowHeight * (i + 1));
            }

            return bmp;

        }
    }
}
