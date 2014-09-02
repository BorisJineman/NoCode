using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoCode
{
    public partial class NetworkControl : UserControl
    {
        public NetworkControl()
        {
            InitializeComponent();
        }

        private const int PaintStartX = 10;
        private const int PaintStartY = 10;
        private const int RowHeight = 20;
        private const int ColumnWidth = 100;
        private int rowCount=10;
        private int columnCount=32;

        public int RowCount
        {
            get { return rowCount; }
            set
            {
                rowCount = value;
                this.Size = new Size(PaintStartX*2 + ColumnCount*ColumnWidth, PaintStartY*2 + (1 + RowCount)*RowHeight);
            }
        }

        public int ColumnCount
        {
            get { return columnCount; }
            set
            {
                columnCount = value; 
                this.Size = new Size(PaintStartX * 2 + ColumnCount * ColumnWidth, PaintStartY * 2 + (1 + RowCount) * RowHeight);
            }
        }

        private void NetworkControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;






            //Draw Background Grid
            g.DrawRectangle(Pens.Black, PaintStartX, PaintStartY, ColumnWidth * columnCount + 1, RowHeight * (rowCount + 1) + 1);
         
            g.DrawLine(Pens.Black, PaintStartX, PaintStartY + RowHeight,
                    PaintStartX + ColumnWidth * ColumnCount + 1, PaintStartY + RowHeight);
           
            Pen linPen = new Pen(Color.Black);
            linPen.DashStyle = DashStyle.Dot;

            for (int i = 0; i < rowCount; ++i)
            {
                g.DrawLine(linPen, PaintStartX, PaintStartY + (i + 1)*RowHeight,
                    PaintStartX + ColumnWidth*ColumnCount + 1, PaintStartY + (i + 1)*RowHeight);
            }

            for (int i = 0; i < columnCount; ++i)
            {
                g.DrawLine(linPen, PaintStartX+i*ColumnWidth, PaintStartY + RowHeight,
                    PaintStartX + i * ColumnWidth, PaintStartY + (1 + rowCount) * RowHeight + 1);
            }

        }

        private void NetworkControl_Load(object sender, EventArgs e)
        {
            this.Size = new Size(PaintStartX * 2 + ColumnCount * ColumnWidth, PaintStartY * 2 + (1 + RowCount) * RowHeight);
        }
    }
}
