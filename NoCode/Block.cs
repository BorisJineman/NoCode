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
            Bitmap bmp = new Bitmap(NetworkControl.ColumnWidth*this.size.Width + 1,
                NetworkControl.RowHeight*this.size.Height + 1);
           
            Graphics g = Graphics.FromImage(bmp);


            g.FillRectangle(Brushes.White, (inputParaList.Count == 0 ? 0 : 1) * NetworkControl.ColumnWidth, 0, NetworkControl.ColumnWidth * (this.size.Width - (inputParaList.Count == 0 ? 0 : 1) - (outPutParaList.Count == 0 ? 0 : 1)),
                NetworkControl.RowHeight*this.size.Height );
            g.DrawRectangle(Pens.Black, (inputParaList.Count == 0 ? 0 : 1) * NetworkControl.ColumnWidth, 0, NetworkControl.ColumnWidth * (this.size.Width - (inputParaList.Count == 0 ? 0 : 1) - (outPutParaList.Count == 0 ? 0 : 1)),
                NetworkControl.RowHeight * this.size.Height);

            SizeF namesize = g.MeasureString(this.name, SystemFonts.DefaultFont);

            g.DrawString(this.name, SystemFonts.DefaultFont, Brushes.Black,
                new PointF((inputParaList.Count == 0 ? 0 : NetworkControl.ColumnWidth) + NetworkControl.ColumnWidth * (this.size.Width - (inputParaList.Count == 0 ? 0 : 1) - (outPutParaList.Count == 0 ? 0 : 1)) / 2.0f - namesize.Width / 2.0f, NetworkControl.RowHeight / 2.0f - namesize.Height / 2.0f));

            for(int i=0;i<inputParaList.Count;i++)
            {
                g.DrawImageUnscaled(inputParaList[i].Paint(), 0, NetworkControl.RowHeight*(i + 1));
            }

            for (int i = 0; i < outPutParaList.Count; i++)
            {
                g.DrawImageUnscaled(outPutParaList[i].Paint(), NetworkControl.ColumnWidth * (this.size.Width -2), NetworkControl.RowHeight * (i + 1));
            }

            return bmp;

        }

        public object HitCheck(Point p)
        {
            foreach (Parameter para in inputParaList)
            {
                if(p.X>=0
                    &&p.X<=NetworkControl.ColumnWidth
                    &&p.Y>=(inputParaList.IndexOf(para)+1)*NetworkControl.RowHeight
                    &&p.Y<=(inputParaList.IndexOf(para)+2)*NetworkControl.RowHeight)
                {
                    return para;
                }
            }

            foreach (Parameter para in outPutParaList)
            {
                if (p.X >= (this.size.Width - 1)*NetworkControl.ColumnWidth
                    && p.X <= this.size.Width*NetworkControl.ColumnWidth
                    && p.Y >= (inputParaList.IndexOf(para) + 1)*NetworkControl.RowHeight
                    && p.Y <= (inputParaList.IndexOf(para) + 2)*NetworkControl.RowHeight)
                {
                    return para;
                }
            }


            Rectangle rect = new Rectangle((inputParaList.Count == 0 ? 0 : 1) * NetworkControl.ColumnWidth, 0, NetworkControl.ColumnWidth * (this.size.Width - (inputParaList.Count == 0 ? 0 : 1) - (outPutParaList.Count == 0 ? 0 : 1)),
                 NetworkControl.RowHeight * this.size.Height);
            if (p.X >= rect.X && p.X <= rect.X + rect.Width
                && p.Y >= rect.Y && p.Y <= rect.Y + rect.Height)
                return this;
            

           return null;
        }

        public Point QueryParameterIndex(Parameter para)
        {
            if (inputParaList.Contains(para))
                return new Point(0,inputParaList.IndexOf(para)+1);
            else if (outPutParaList.Contains(para))
                return new Point(this.size.Width-1, outPutParaList.IndexOf(para) + 1);
            else
                return new Point(0, 0);
        }
    }


}
