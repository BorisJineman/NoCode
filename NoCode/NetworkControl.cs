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

            //ONLY FOR TEST CODE BEGIN
            Block b = new Block();
            b.Name = "AND";
            b.Size = new Size(3, 3);
            Parameter p = new Parameter();
            p.Name = "In0";
            p.Value = "AAA";
            p.Type = ParameterType.Input;
            p.Parent = b;
            b.InputParaList.Add(p);
            p = new Parameter();
            p.Name = "In1";
            p.Value = "BBB";
            p.Type = ParameterType.Input;
            p.Parent = b;
            b.InputParaList.Add(p);
           Parameter pf = new Parameter();
            pf.Name = "Out0";

            pf.Type = ParameterType.Output;
            pf.Parent = b;
            b.OutPutParaList.Add(pf);


            b.Location = new Point(2, 2);
            network.BlockLists.Add(b);


            b = new Block();
            b.Name = "AND";
            b.Size = new Size(3, 3);
            p = new Parameter();
            p.Name = "In0";
            p.Value = "AAA";
            p.Type = ParameterType.Input;
            p.Parent = b;
            b.InputParaList.Add(p);
            Parameter pf2 = new Parameter();
            pf2.Name = "In1";
            pf2.Type = ParameterType.Input;
            pf2.Parent = b;
            b.InputParaList.Add(pf2);


            p = new Parameter();
            p.Name = "Out0";
            p.Value = "CCC";
            p.Type = ParameterType.Output;
            p.Parent = b;
            b.OutPutParaList.Add(p);


            b.Location = new Point(7,2);
            network.BlockLists.Add(b);

            pf.LineTo = pf2;
            pf2.LineTo = pf;


            //ONLY FOR TEST CODE END
        }

        enum OperationState
        {
            Normal,
            GridSelected,
            BlockSelected,
            BlockMoving,
            LineSelected,
            LineHeadSelected,
            LineTailSelected,
            BlockDragDrop,
        }

        
        


        private OperationState operationState; 
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

            if (currentOperation == OperationState.GridSelected)
            {

                Point temp0 = (Point)LastOperationObjects[0];
                Point temp1 = (Point)LastOperationObjects[1];

                Point gridLocation0 = new Point(temp0.X < temp1.X ? temp0.X : temp1.X,
                    temp0.Y < temp1.Y ? temp0.Y : temp1.Y);
                Point gridLocation1 = new Point(temp0.X > temp1.X ? temp0.X : temp1.X,
                   temp0.Y > temp1.Y ? temp0.Y : temp1.Y);

                
                g.FillRectangle(Brushes.LightBlue, PaintStartX + ColumnWidth*gridLocation0.X,
                    PaintStartY + RowHeight*(gridLocation0.Y + 1), ColumnWidth*(Math.Abs(gridLocation1.X - gridLocation0.X+1)),
                    RowHeight*(Math.Abs(gridLocation1.Y - gridLocation0.Y+1)));

            }



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

            foreach (Line line in lines)
            {


                Block block = line.Begin.Parent;
                Parameter para = line.Begin;

                Point begin = block.QueryParameterIndex(para);
                begin = new Point(begin.X + block.Location.X, begin.Y + block.Location.Y);
                Block targetBlock = para.LineTo.Parent;
                Point end = targetBlock.QueryParameterIndex(para.LineTo);
                end = new Point(end.X + targetBlock.Location.X, end.Y + targetBlock.Location.Y);

                Pen linePen = null;

                if (currentOperation == OperationState.LineSelected && line == LastOperationObjects[0])
                {
                    linePen=new Pen(Color.Black,2);
                }
                else
                {

                    linePen = new Pen(Color.Black, 0);
                }
                g.DrawLines(linePen, new PointF[]
                                               {
                                                   new PointF(PaintStartX+begin.X*ColumnWidth,PaintStartY+(begin.Y+1+0.5f)*RowHeight), 
                                                     new PointF(PaintStartX+(begin.X+end.X+1)/2.0f*ColumnWidth,PaintStartY+(begin.Y+1+0.5f)*RowHeight), 
                                                   new PointF(PaintStartX+(begin.X+end.X+1)/2.0f*ColumnWidth,PaintStartY+(end.Y+1+0.5f)*RowHeight), 
                                                     new PointF(PaintStartX+(end.X+1)*ColumnWidth,PaintStartY+(end.Y+1+0.5f)*RowHeight)
                                               });
            }

            foreach (Block block in network.BlockLists)
            {
                foreach (Parameter para in block.OutPutParaList)
                {
                    if (para.LineTo != null)
                    {
                       
                    }
                }
            }

            foreach (Block block in network.BlockLists)
            {
                g.DrawImageUnscaled(block.Paint(), PaintStartX+block.Location.X * ColumnWidth,
                   PaintStartY+ (block.Location.Y+1)*RowHeight);
            }

        }

        private List<Line> lines=new List<Line>(); 

        private void NetworkControl_Load(object sender, EventArgs e)
        {
            this.Size = new Size(PaintStartX * 2 + ColumnCount * ColumnWidth, PaintStartY * 2 + (1 + RowCount) * RowHeight);

            lines.Clear();
            foreach (Block block in network.BlockLists)
            {
                foreach (Parameter para in block.OutPutParaList)
                {
                    if (para.LineTo != null)
                    {
                        Line line=new Line();
                        line.Begin = para;
                        line.End = para.LineTo;

                        lines.Add(line);

                      
                    }
                }
            }
        }

        private Point mouseDownPoint;
        private Point mouseLastPoint;

        private Point mouseDownLocation;
        private Point mouseLastLocation;

        private OperationState currentOperation;

        private object[] LastOperationObjects;

        private void NetworkControl_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownLocation = new Point((e.X - PaintStartX) / ColumnWidth, (e.Y - PaintStartY) / RowHeight - 1);
        
            mouseDownPoint = e.Location;

            object currentHit=null;
            for (int i = network.BlockLists.Count; i > 0; i--)
            {
                Block b = network.BlockLists[i - 1];
                currentHit = b.HitCheck(new Point(e.Location.X - PaintStartX - ColumnWidth * b.Location.X, e.Location.Y - PaintStartY - RowHeight * (b.Location.Y+1)));
                if (currentHit != null)
                {
                    break;
                }
            }

           if (currentHit == null)
           {
                foreach (Line line in lines)
                {
                    Block block = line.Begin.Parent;
                    Parameter para = line.Begin;

                    Point begin = block.QueryParameterIndex(para);
                    begin = new Point(begin.X + block.Location.X, begin.Y + block.Location.Y);
                    Block targetBlock = para.LineTo.Parent;
                    Point end = targetBlock.QueryParameterIndex(para.LineTo);
                    end = new Point(end.X + targetBlock.Location.X, end.Y + targetBlock.Location.Y);

                    if ((mouseDownLocation.Y == begin.Y && mouseDownLocation.X <= (begin.X + end.X) / 2 && mouseDownLocation.X >= begin.X)
                                  || (mouseDownLocation.X == (begin.X + end.X) / 2 && mouseDownLocation.Y >= (begin.Y < end.Y ? begin.Y : end.Y) && mouseDownLocation.Y <= (begin.Y > end.Y ? begin.Y : end.Y))
                                  || (mouseDownLocation.Y == end.Y && mouseDownLocation.X >= (begin.X + end.X) / 2 && mouseDownLocation.X <=  end.X))
                    {
                        currentHit = line;
                        break;
                    }
                }
            }


            if (currentHit == null)
            {
                if (mouseDownLocation.X >= 0 && mouseDownLocation.Y >= 0 && mouseDownLocation.X < columnCount && mouseDownLocation.Y < rowCount)
                {
                    LastOperationObjects = new object[] { mouseDownLocation, mouseDownLocation };
                    currentOperation = OperationState.GridSelected;
                }
            }
            else if (currentHit is Parameter)
            {
                LastOperationObjects = new object[] { currentHit };

                Parameter para = (Parameter) currentHit;
                if (para.Type == ParameterType.Input)
                {
                    currentOperation = OperationState.LineTailSelected;
                } 
                else if (para.Type == ParameterType.Output)
                {
                    currentOperation = OperationState.LineHeadSelected;
                }
            }
            else if (currentHit is Block)
            {
                LastOperationObjects = new object[] {currentHit};
                currentOperation = OperationState.BlockSelected;
            }
            else if (currentHit is Line)
            {
                LastOperationObjects = new object[] { currentHit };
                currentOperation = OperationState.LineSelected;
            }


            mouseLastLocation = new Point((e.X - PaintStartX)/ColumnWidth, (e.Y - PaintStartY)/RowHeight - 1);
            mouseLastPoint = e.Location;
            this.Invalidate();
        }

        private void NetworkControl_MouseMove(object sender, MouseEventArgs e)
        {

            Point mouseCurrentLocation=new Point((e.X - PaintStartX)/ColumnWidth, (e.Y - PaintStartY)/RowHeight - 1);
            do
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (currentOperation == OperationState.GridSelected)
                    {
                        int x = (e.X - PaintStartX)/ColumnWidth;
                        int y = (e.Y - PaintStartY)/RowHeight - 1;
                        if (x >= 0 && y >= 0 && x < columnCount && y < rowCount)
                        {
                            LastOperationObjects[1] = new Point(x, y);
                            this.Invalidate();
                        }
                        break;
                    }
                    else if (currentOperation == OperationState.BlockSelected)
                    {
                        Block b = LastOperationObjects[0] as Block;
                        b.Location = new Point(b.Location.X + mouseCurrentLocation.X - mouseLastLocation.X,
                            b.Location.Y + mouseCurrentLocation.Y - mouseLastLocation.Y);
                        this.Invalidate();
                    }
                }
            } while (false);

            mouseLastLocation = mouseCurrentLocation;
 
            mouseLastPoint = e.Location;
         
        }

        private void NetworkControl_MouseUp(object sender, MouseEventArgs e)
        {
            do
            {
                if (currentOperation == OperationState.GridSelected)
                {
                    currentOperation = OperationState.Normal;
                    break;
                }
                if (currentOperation == OperationState.BlockSelected)
                {
                    currentOperation = OperationState.BlockSelected;
                    break;
                }
                if (currentOperation == OperationState.LineSelected)
                {
                    currentOperation = OperationState.LineSelected;
                }
            } while (false);


            mouseLastPoint = e.Location;
            this.Invalidate();
        }


    }
}
