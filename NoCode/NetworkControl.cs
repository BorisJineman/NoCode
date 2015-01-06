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

        private Network network=new Network();


        private const int PaintStartX = 10;
        private const int PaintStartY = 10;
        public static int RowHeight = 20;
        public static int ColumnWidth = 100;
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

        public Network Network
        {
            get { return network; }
            set { network = value; }
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

            foreach (Block block in network.BlockLists)
            {
                g.DrawImageUnscaled(block.Paint(), PaintStartX+(block.Location.X - 1) * ColumnWidth,
                   PaintStartY+ (block.Location.Y+1)*RowHeight);
            }

        }

        private void NetworkControl_Load(object sender, EventArgs e)
        {
            this.Size = new Size(PaintStartX * 2 + ColumnCount * ColumnWidth, PaintStartY * 2 + (1 + RowCount) * RowHeight);
           
            //ONLY FOR TEST CODE BEGIN
            Block b=new Block();
            b.Name = "AND";
            b.Size=new Size(1,3);
            Parameter p=new Parameter();
            p.Name = "In0";
            p.Value = "AAA";
            p.Type = ParameterType.Input;
            b.InputParaList.Add(p);
            p = new Parameter();
            p.Name = "In1";
            p.Value = "BBB";
            p.Type = ParameterType.Input;
            b.InputParaList.Add(p);
            p = new Parameter();
            p.Name = "Out0";
            p.Value = "CCC";
            p.Type = ParameterType.Output;
            b.OutPutParaList.Add(p);


            b.Location = new Point(2, 2);
            network.BlockLists.Add(b);


            //ONLY FOR TEST CODE END
        }

        
        private void NetworkControl_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void NetworkControl_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void NetworkControl_MouseUp(object sender, MouseEventArgs e)
        {

        }

     
    }
}
