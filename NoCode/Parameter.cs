using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoCode
{
    public class Parameter
    {
        private string name="";
        private ParameterState state;
        private ParameterType type;
        private ParameterDataType dataType;
        private string dataValue = "";
        private Parameter lineTo = null;
        private Block parent;

        public ParameterState State
        {
            get { return state; }
            set { state = value; }
        }

        public ParameterType Type
        {
            get { return type; }
            set { type = value; }
        }

        public ParameterDataType DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Value
        {
            get { return dataValue; }
            set { dataValue = value; }
        }

        public Parameter LineTo
        {
            get { return lineTo; }
            set
            {
                lineTo = value;
                if (lineTo != null)
                {
                    dataValue = "";
                }
            }
        }

        public Block Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public Image Paint()
        {
            Bitmap bmp = new Bitmap(NetworkControl.ColumnWidth * 2 + 1,
                NetworkControl.RowHeight + 1);
            
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            
            SizeF nameSize= g.MeasureString(name, SystemFonts.DefaultFont);
            SizeF valueSize =g.MeasureString(dataValue,SystemFonts.DefaultFont);

            if (type == ParameterType.Input)
            {
                if (state == ParameterState.Forward)
                {
                    g.DrawLine(Pens.Black, NetworkControl.ColumnWidth - 5, NetworkControl.RowHeight/2,
                        NetworkControl.ColumnWidth, NetworkControl.RowHeight/2);
                }
                else if (state == ParameterState.Reverse)
                {
                    g.DrawEllipse(Pens.Black, NetworkControl.ColumnWidth-5, NetworkControl.RowHeight/2.0f - 2.5f, 5, 5);
                }

                g.DrawString(dataValue, SystemFonts.DefaultFont, Brushes.Black,
                    NetworkControl.ColumnWidth - 5 - 2 - valueSize.Width,
                    NetworkControl.RowHeight/2.0f - nameSize.Height/2.0f);

                g.DrawString(name, SystemFonts.DefaultFont, Brushes.Black, NetworkControl.ColumnWidth + 2,
                    NetworkControl.RowHeight/2.0f - nameSize.Height/2.0f);

            }
            else if (type == ParameterType.Output)
            {
                g.DrawLine(Pens.Black, NetworkControl.ColumnWidth, NetworkControl.RowHeight/2,
                    NetworkControl.ColumnWidth + 5, NetworkControl.RowHeight/2);

                g.DrawString(dataValue, SystemFonts.DefaultFont, Brushes.Black,
                    NetworkControl.ColumnWidth +5+2,
                    NetworkControl.RowHeight/2.0f - nameSize.Height/2.0f);

                g.DrawString(name, SystemFonts.DefaultFont, Brushes.Black, NetworkControl.ColumnWidth -2-nameSize.Width,
                    NetworkControl.RowHeight/2.0f - nameSize.Height/2.0f);
            }

            bmp.MakeTransparent();
            return bmp;
        }
    }
}
