using NoCode.FBDCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace NoCode.FBDCore
{
	public class FBDNetworkControl : UserControl
	{
		private const int LabelAreaHeight = 25;
		private int _columnCount = 32;
		private int _rowCount = 10;
		private string _label;
		private int _ID;
		private FBDNetwork _FBDNetwork = new FBDNetwork();
		private List<FBD> _selectedFBDList = new List<FBD>();
		private Point _currentBeginCell = Point.Empty;
		private Point _currentEndCell = Point.Empty;
		private FBDNetworkOperationState _operationState;
		private FBD _currentDragFBD;
		private FBDPara _selectedPara;
		private IContainer components;
		private Label label1;
		public int ColumnCount
		{
			get
			{
				return this._columnCount;
			}
			set
			{
				this._columnCount = value;
			}
		}
		public int RowCount
		{
			get
			{
				return this._rowCount;
			}
			set
			{
				this._rowCount = value;
			}
		}
		public string Label
		{
			get
			{
				return this._label;
			}
			set
			{
				this._label = value;
				this.label1.Text = "Network" + this._ID.ToString().PadLeft(3) + ":";
				if (this._label != null && this._label != "")
				{
					Label expr_52 = this.label1;
					expr_52.Text += this._label;
					return;
				}
				Label expr_6F = this.label1;
                expr_6F.Text += "Double click to add comment...";
			}
		}
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
				this.label1.Text = "Network" + this._ID.ToString().PadLeft(3) + ":";
				if (this._label != null && this._label != "")
				{
					Label expr_52 = this.label1;
					expr_52.Text += this._label;
					return;
				}
				Label expr_6F = this.label1;
                expr_6F.Text += "Double click to add comment...";
			}
		}
		public FBDNetwork FBDNetwork
		{
			get
			{
				return this._FBDNetwork;
			}
			set
			{
				if (value != null)
				{
					this._FBDNetwork = value;
				}
				else
				{
					this._FBDNetwork = new FBDNetwork();
				}
				this.ReloadNetwork();
			}
		}
		public List<FBD> SelectedFBDList
		{
			get
			{
				return this._selectedFBDList;
			}
		}
		public Point CurrentBeginCell
		{
			get
			{
				return this._currentBeginCell;
			}
			set
			{
				if (this._currentBeginCell != value)
				{
					this._currentBeginCell = value;
					base.Invalidate();
				}
			}
		}
		public Point CurrentEndCell
		{
			get
			{
				return this._currentEndCell;
			}
			set
			{
				if (this._currentEndCell != value)
				{
					this._currentEndCell = value;
					base.Invalidate();
				}
			}
		}
		public FBDNetworkOperationState OperationState
		{
			get
			{
				return this._operationState;
			}
			set
			{
				this._operationState = value;
			}
		}
		public FBDNetworkControl()
		{
			this.InitializeComponent();
		}
		public void ReloadNetwork()
		{
			this.ColumnCount = this._FBDNetwork.ColumnCount;
			this.RowCount = this._FBDNetwork.RowCount;
			this.Label = this._FBDNetwork.Label;
			base.Width = this.ColumnCount * 100 + 1;
			base.Height = this.RowCount * 20 + 25 + 1;
		}
		private void FBDNetworkControl_Paint(object sender, PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			if (this.Focused && this._operationState == FBDNetworkOperationState.Selecting)
			{
				Point point = new Point((this.CurrentBeginCell.X < this.CurrentEndCell.X) ? this.CurrentBeginCell.X : this.CurrentEndCell.X, (this.CurrentBeginCell.Y < this.CurrentEndCell.Y) ? this.CurrentBeginCell.Y : this.CurrentEndCell.Y);
				Point point2 = new Point((this.CurrentBeginCell.X > this.CurrentEndCell.X) ? this.CurrentBeginCell.X : this.CurrentEndCell.X, (this.CurrentBeginCell.Y > this.CurrentEndCell.Y) ? this.CurrentBeginCell.Y : this.CurrentEndCell.Y);
				graphics.FillRectangle(Brushes.SkyBlue, new Rectangle(point.X * 100, point.Y * 20 + 25, (point2.X - point.X + 1) * 100, (point2.Y - point.Y + 1) * 20));
			}
			graphics.DrawLine(Pens.Black, new Point(0, 25), new Point(base.Width, 25));
			Pen pen = new Pen(Brushes.DarkGray);
			pen.DashStyle = DashStyle.Dot;
			for (int i = 1; i < this.RowCount; i++)
			{
				graphics.DrawLine(pen, new Point(0, i * 20 + 25), new Point(base.Width, i * 20 + 25));
			}
			for (int j = 1; j < this.ColumnCount; j++)
			{
				graphics.DrawLine(pen, new Point(j * 100, 25), new Point(j * 100, base.Height));
			}
			foreach (FBD current in this.FBDNetwork.FBDList)
			{
				graphics.DrawImage(current.DrawMe(), current.Location.X * 100, current.Location.Y * 20 + 25);
			}
			foreach (FBD current2 in this.FBDNetwork.FBDList)
			{
				foreach (FBDPara current3 in current2.OutputParaList)
				{
					foreach (FBD current4 in this.FBDNetwork.FBDList)
					{
						foreach (FBDPara current5 in current4.InputParaList)
						{
							if (current5.LineFromPara == current3)
							{
								graphics.DrawLines(Pens.Black, new Point[]
								{
									new Point((current2.Location.X + current2.Size.Width - 1) * 100, current2.Location.Y * 20 + current3.Index * 20 + 10 + 25),
									new Point(((current2.Location.X + current2.Size.Width - 1) * 100 + (current4.Location.X + 1) * 100) / 2, current2.Location.Y * 20 + current3.Index * 20 + 10 + 25),
									new Point(((current2.Location.X + current2.Size.Width - 1) * 100 + (current4.Location.X + 1) * 100) / 2, current4.Location.Y * 20 + current5.Index * 20 + 10 + 25),
									new Point((current4.Location.X + 1) * 100, current4.Location.Y * 20 + current5.Index * 20 + 10 + 25)
								});
							}
						}
					}
				}
			}
			if (this._operationState == FBDNetworkOperationState.NewFBDDraging)
			{
				graphics.DrawImage(this._currentDragFBD.DrawMe(), this._currentBeginCell.X * 100, this._currentBeginCell.Y * 20 + 25);
			}
			if (this._operationState == FBDNetworkOperationState.ParaOutPinSelected || this._operationState == FBDNetworkOperationState.ParaOutSelected)
			{
				graphics.DrawLines(Pens.Blue, new Point[]
				{
					new Point(this._currentBeginCell.X * 100, this._currentBeginCell.Y * 20 + 10 + 25),
					new Point((this._currentBeginCell.X * 100 + (this._currentEndCell.X + 1) * 100) / 2, this._currentBeginCell.Y * 20 + 10 + 25),
					new Point((this._currentBeginCell.X * 100 + (this._currentEndCell.X + 1) * 100) / 2, this._currentEndCell.Y * 20 + 10 + 25),
					new Point((this._currentEndCell.X + 1) * 100, this._currentEndCell.Y * 20 + 10 + 25)
				});
			}
			if (this._operationState == FBDNetworkOperationState.ParaInPinSelected || this._operationState == FBDNetworkOperationState.ParaInSelected)
			{
				graphics.DrawLines(Pens.Blue, new Point[]
				{
					new Point((this._currentBeginCell.X + 1) * 100, this._currentBeginCell.Y * 20 + 10 + 25),
					new Point(((this._currentBeginCell.X + 1) * 100 + this._currentEndCell.X * 100) / 2, this._currentBeginCell.Y * 20 + 10 + 25),
					new Point(((this._currentBeginCell.X + 1) * 100 + this._currentEndCell.X * 100) / 2, this._currentEndCell.Y * 20 + 10 + 25),
					new Point(this._currentEndCell.X * 100, this._currentEndCell.Y * 20 + 10 + 25)
				});
			}
		}
		private void FBDNetworkControl_MouseDown(object sender, MouseEventArgs e)
		{
			base.Focus();
			this._operationState = FBDNetworkOperationState.Default;
			if (this._operationState == FBDNetworkOperationState.Default)
			{
				bool flag = false;
				foreach (FBD current in this.FBDNetwork.FBDList)
				{
					foreach (FBDPara current2 in current.InputParaList)
					{
						Point p = new Point(e.X - current.Location.X * 100, e.Y - 25 - current.Location.Y * 20 - current2.Index * 20);
						FBDHitTestResultEnum fBDHitTestResultEnum = current2.HitTest(p);
						if (!flag)
						{
							if (fBDHitTestResultEnum == FBDHitTestResultEnum.InParaPin)
							{
								current2.SelectState = FBDParaSelectedState.Pin;
								if (current2.CanLine)
								{
									this._operationState = FBDNetworkOperationState.ParaInPinSelected;
								}
								this._selectedPara = current2;
								flag = true;
							}
							else if (fBDHitTestResultEnum == FBDHitTestResultEnum.InPara)
							{
								current2.SelectState = FBDParaSelectedState.Para;
								if (current2.CanLine)
								{
									this._operationState = FBDNetworkOperationState.ParaInSelected;
								}
								this._selectedPara = current2;
								flag = true;
							}
							else
							{
								current2.SelectState = FBDParaSelectedState.None;
							}
						}
						else
						{
							current2.SelectState = FBDParaSelectedState.None;
						}
					}
					foreach (FBDPara current3 in current.OutputParaList)
					{
						Point p2 = new Point(e.X - current.Location.X * 100 - current.Size.Width * 100 + 200, e.Y - 25 - current.Location.Y * 20 - current3.Index * 20);
						FBDHitTestResultEnum fBDHitTestResultEnum2 = current3.HitTest(p2);
						if (!flag)
						{
							if (fBDHitTestResultEnum2 == FBDHitTestResultEnum.OutParaPin)
							{
								current3.SelectState = FBDParaSelectedState.Pin;
								if (current3.CanLine)
								{
									this._operationState = FBDNetworkOperationState.ParaOutPinSelected;
								}
								this._selectedPara = current3;
								flag = true;
							}
							else if (fBDHitTestResultEnum2 == FBDHitTestResultEnum.OutPara)
							{
								current3.SelectState = FBDParaSelectedState.Para;
								if (current3.CanLine)
								{
									this._operationState = FBDNetworkOperationState.ParaOutSelected;
								}
								this._selectedPara = current3;
								flag = true;
							}
							else
							{
								current3.SelectState = FBDParaSelectedState.None;
							}
						}
						else
						{
							current3.SelectState = FBDParaSelectedState.None;
						}
					}
					if (current.HeadPara != null)
					{
						Point p3 = new Point(e.X - current.Location.X * 100 - (current.Size.Width + ((current.OutputParaList.Count == 0) ? 1 : 0) + ((current.InputParaList.Count == 0) ? 1 : 0)) * 50, e.Y - 25 - current.Location.Y * 20);
						FBDHitTestResultEnum fBDHitTestResultEnum3 = current.HeadPara.HitTest(p3);
						if (!flag)
						{
							if (fBDHitTestResultEnum3 == FBDHitTestResultEnum.HeadPara)
							{
								current.HeadPara.SelectState = FBDParaSelectedState.Para;
								this._operationState = FBDNetworkOperationState.ParaSelected;
								this._selectedPara = current.HeadPara;
								flag = true;
							}
							else
							{
								current.HeadPara.SelectState = FBDParaSelectedState.None;
							}
						}
						else
						{
							current.HeadPara.SelectState = FBDParaSelectedState.None;
						}
					}
				}
				if (flag)
				{
					foreach (FBD current4 in this.FBDNetwork.FBDList)
					{
						current4.Selected = false;
					}
					this._selectedFBDList.Clear();
					if (e.Y >= 25)
					{
						int x = e.X / 100;
						int y = (e.Y - 25) / 20;
						this._currentBeginCell = new Point(x, y);
						this._currentEndCell = new Point(x, y);
					}
				}
				else
				{
					this._selectedPara = null;
				}
				base.Invalidate();
			}
			if (this._operationState == FBDNetworkOperationState.Default)
			{
				if (e.Y < 25)
				{
					return;
				}
				int x2 = e.X / 100;
				int y2 = (e.Y - 25) / 20;
				this._currentBeginCell = new Point(x2, y2);
				if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
				{
					bool flag2 = false;
					foreach (FBD current5 in this._selectedFBDList)
					{
						Point p4 = new Point(e.X - current5.Location.X * 100, e.Y - current5.Location.Y * 20 - 25);
						if (current5.HitTest(p4) == FBDHitTestResultEnum.FBD)
						{
							current5.Selected = false;
							if (this._selectedFBDList.Contains(current5))
							{
								this._selectedFBDList.Remove(current5);
							}
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						foreach (FBD current6 in this._selectedFBDList)
						{
							current6.Selected = false;
						}
						this._selectedFBDList.Clear();
					}
				}
				foreach (FBD current7 in this.FBDNetwork.FBDList)
				{
					Point p5 = new Point(e.X - current7.Location.X * 100, e.Y - current7.Location.Y * 20 - 25);
					if (current7.HitTest(p5) == FBDHitTestResultEnum.FBD)
					{
						if (!this._selectedFBDList.Contains(current7))
						{
							current7.Selected = true;
							this._selectedFBDList.Add(current7);
							this._operationState = FBDNetworkOperationState.FBDSelected;
						}
						else
						{
							current7.Selected = false;
							if (this._selectedFBDList.Contains(current7))
							{
								this._selectedFBDList.Remove(current7);
							}
						}
						base.Invalidate();
						break;
					}
				}
			}
			if (this._operationState == FBDNetworkOperationState.Default)
			{
				if (e.Y < 25)
				{
					return;
				}
				int x3 = e.X / 100;
				int y3 = (e.Y - 25) / 20;
				this._currentBeginCell = new Point(x3, y3);
				this._currentEndCell = new Point(x3, y3);
				this._operationState = FBDNetworkOperationState.Selecting;
				base.Invalidate();
			}
		}
		private void label1_DoubleClick(object sender, EventArgs e)
		{
			EditNetworkLabelForm editNetworkLabelForm = new EditNetworkLabelForm();
			editNetworkLabelForm.Value = this.Label;
			if (editNetworkLabelForm.ShowDialog() == DialogResult.OK)
			{
				this.Label = editNetworkLabelForm.Value;
				this.FBDNetwork.Label = editNetworkLabelForm.Value;
			}
		}
		private void FBDNetworkControl_MouseClick(object sender, MouseEventArgs e)
		{
		}
		private void FBDNetworkControl_Enter(object sender, EventArgs e)
		{
		}
		private void FBDNetworkControl_Leave(object sender, EventArgs e)
		{
			base.Invalidate();
		}
		private void FBDNetworkControl_MouseMove(object sender, MouseEventArgs e)
		{
			if (this._operationState == FBDNetworkOperationState.ParaOutPinSelected || this._operationState == FBDNetworkOperationState.ParaOutSelected || this._operationState == FBDNetworkOperationState.ParaInPinSelected || this._operationState == FBDNetworkOperationState.ParaInSelected)
			{
				if (e.Y < 25)
				{
					return;
				}
				int x = e.X / 100;
				int y = (e.Y - 25) / 20;
				this._currentEndCell = new Point(x, y);
				base.Invalidate();
			}
			if (this._operationState == FBDNetworkOperationState.FBDSelected)
			{
				if (e.Y < 25)
				{
					return;
				}
				int x2 = e.X / 100;
				int y2 = (e.Y - 25) / 20;
				this._currentEndCell = new Point(x2, y2);
				bool flag = true;
				foreach (FBD current in this._selectedFBDList)
				{
					Point pt = current.Location;
					pt += new Size(this._currentEndCell.X - this._currentBeginCell.X, this._currentEndCell.Y - this._currentBeginCell.Y);
					if (pt.X < 0 || pt.Y < 0 || pt.X + current.Size.Width > this.ColumnCount || pt.Y + current.Size.Height > this.RowCount)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					foreach (FBD current2 in this._selectedFBDList)
					{
						current2.Location += new Size(this._currentEndCell.X - this._currentBeginCell.X, this._currentEndCell.Y - this._currentBeginCell.Y);
					}
					this._currentBeginCell = this._currentEndCell;
					base.Invalidate();
				}
			}
			if (this._operationState == FBDNetworkOperationState.Selecting)
			{
				if (e.Y < 25)
				{
					return;
				}
				int x3 = e.X / 100;
				int y3 = (e.Y - 25) / 20;
				this._currentEndCell = new Point(x3, y3);
				base.Invalidate();
			}
		}
		private void FBDNetworkControl_MouseUp(object sender, MouseEventArgs e)
		{
			if (this._operationState == FBDNetworkOperationState.ParaOutPinSelected || this._operationState == FBDNetworkOperationState.ParaOutSelected)
			{
				foreach (FBD current in this.FBDNetwork.FBDList)
				{
					foreach (FBDPara current2 in current.InputParaList)
					{
						if (current2.CanLine)
						{
							Point p = new Point(e.X - current.Location.X * 100, e.Y - 25 - current.Location.Y * 20 - current2.Index * 20);
							FBDHitTestResultEnum fBDHitTestResultEnum = current2.HitTest(p);
							if (fBDHitTestResultEnum == FBDHitTestResultEnum.InParaPin || fBDHitTestResultEnum == FBDHitTestResultEnum.InPara)
							{
								if (!this._selectedPara.LineToParaList.Contains(current2))
								{
									this._selectedPara.LineToParaList.Add(current2);
								}
								if (current2.LineFromPara != null && current2.LineFromPara.LineToParaList.Contains(current2))
								{
									current2.LineFromPara.LineToParaList.Remove(current2);
								}
								current2.LineFromPara = this._selectedPara;
								this._selectedPara.Value = "";
								current2.Value = "";
								break;
							}
						}
					}
				}
			}
			if (this._operationState == FBDNetworkOperationState.ParaInPinSelected || this._operationState == FBDNetworkOperationState.ParaInSelected)
			{
				foreach (FBD current3 in this.FBDNetwork.FBDList)
				{
					foreach (FBDPara current4 in current3.OutputParaList)
					{
						if (current4.CanLine)
						{
							Point p2 = new Point(e.X - current3.Location.X * 100 - current3.Size.Width * 100 + 200, e.Y - 25 - current3.Location.Y * 20 - current4.Index * 20);
							FBDHitTestResultEnum fBDHitTestResultEnum2 = current4.HitTest(p2);
							if (fBDHitTestResultEnum2 == FBDHitTestResultEnum.OutParaPin || fBDHitTestResultEnum2 == FBDHitTestResultEnum.OutPara)
							{
								if (this._selectedPara.LineFromPara != null && this._selectedPara.LineFromPara.LineToParaList.Contains(this._selectedPara))
								{
									this._selectedPara.LineFromPara.LineToParaList.Remove(this._selectedPara);
								}
								this._selectedPara.LineFromPara = current4;
								if (!current4.LineToParaList.Contains(this._selectedPara))
								{
									current4.LineToParaList.Add(this._selectedPara);
								}
								this._selectedPara.Value = "";
								current4.Value = "";
								break;
							}
						}
					}
				}
			}
			if (this._operationState == FBDNetworkOperationState.Selecting)
			{
				Point point = new Point((this.CurrentBeginCell.X < this.CurrentEndCell.X) ? this.CurrentBeginCell.X : this.CurrentEndCell.X, (this.CurrentBeginCell.Y < this.CurrentEndCell.Y) ? this.CurrentBeginCell.Y : this.CurrentEndCell.Y);
				Point point2 = new Point(((this.CurrentBeginCell.X > this.CurrentEndCell.X) ? this.CurrentBeginCell.X : this.CurrentEndCell.X) + 1, ((this.CurrentBeginCell.Y > this.CurrentEndCell.Y) ? this.CurrentBeginCell.Y : this.CurrentEndCell.Y) + 1);
				foreach (FBD current5 in this._FBDNetwork.FBDList)
				{
					Rectangle rectangle = new Rectangle(((current5.InputParaList.Count == 0) ? 0 : 1) + current5.Location.X, ((current5.HeadPara == null) ? 0 : 1) + current5.Location.Y, current5.Size.Width - ((current5.InputParaList.Count == 0) ? 0 : 1) - ((current5.OutputParaList.Count == 0) ? 0 : 1), current5.Size.Height - ((current5.HeadPara == null) ? 0 : 1));
					if (rectangle.X >= point.X && rectangle.Y >= point.Y && rectangle.X + rectangle.Width <= point2.X && rectangle.Y + rectangle.Height <= point2.Y)
					{
						current5.Selected = true;
						this._selectedFBDList.Add(current5);
					}
				}
			}
			this._operationState = FBDNetworkOperationState.Default;
			base.Invalidate();
		}
		private void FBDNetworkControl_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(FBD)))
			{
				e.Effect = e.AllowedEffect;
				this._operationState = FBDNetworkOperationState.NewFBDDraging;
				this._currentDragFBD = (FBD)e.Data.GetData(typeof(FBD));
				Point point = base.PointToClient(new Point(e.X, e.Y));
				int num = point.X / 100;
				int num2 = (point.Y - 25) / 20;
				num--;
				num2--;
				if (num < 0 || num2 < 0)
				{
					return;
				}
				this._currentBeginCell = new Point(num, num2);
				base.Invalidate();
			}
		}
		private void FBDNetworkControl_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(FBD)))
			{
				Point point = base.PointToClient(new Point(e.X, e.Y));
				int num = point.X / 100;
				int num2 = (point.Y - 25) / 20;
				num--;
				num2--;
				if (num < 0 || num2 < 0)
				{
					return;
				}
				this._currentBeginCell = new Point(num, num2);
				base.Invalidate();
			}
		}
		private void FBDNetworkControl_DragLeave(object sender, EventArgs e)
		{
			if (this._currentDragFBD != null)
			{
				this._currentDragFBD = null;
				this._operationState = FBDNetworkOperationState.Default;
				base.Invalidate();
			}
		}
		private void FBDNetworkControl_DragDrop(object sender, DragEventArgs e)
		{
			if (this._currentDragFBD != null)
			{
				string name;
				FBD fBD;
                //if ((name = this._currentDragFBD.Name) != null && name == "CALL")
                //{
                //    EventEditSelectMethod eventEditSelectMethod = new EventEditSelectMethod();
                //    if (eventEditSelectMethod.ShowDialog() != DialogResult.OK)
                //    {
                //        return;
                //    }
                //    fBD = eventEditSelectMethod.ResultFBD;
                //}
                //else
                //{
					fBD = this._currentDragFBD.Clone();
				//}
				this._currentDragFBD = null;
				this._operationState = FBDNetworkOperationState.Default;
				fBD.Location = this._currentBeginCell;
				this.FBDNetwork.FBDList.Add(fBD);
				if (fBD.Location.X + fBD.Size.Width > this.ColumnCount)
				{
					this._FBDNetwork.ColumnCount = fBD.Location.X + fBD.Size.Width;
					this.ColumnCount = this._FBDNetwork.ColumnCount;
					base.Height = this.ColumnCount * 100 + 1;
				}
				if (fBD.Location.Y + fBD.Size.Height > this.RowCount)
				{
					this._FBDNetwork.RowCount = fBD.Location.Y + fBD.Size.Height;
					this.RowCount = this._FBDNetwork.RowCount;
					base.Height = this.RowCount * 20 + 25 + 1;
				}
				base.Invalidate();
			}
		}
		private void FBDNetworkControl_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.End)
			{
				this.AddRow();
				return;
			}
			if (e.KeyCode == Keys.Home)
			{
				this.RemoveRow();
				return;
			}
			if (e.KeyCode == Keys.Delete)
			{
				if (this._selectedFBDList.Count != 0)
				{
					foreach (FBD current in this._selectedFBDList)
					{
						this._FBDNetwork.FBDList.Remove(current);
					}
					foreach (FBD current2 in this._selectedFBDList)
					{
						foreach (FBDPara current3 in current2.InputParaList)
						{
							foreach (FBD current4 in this._FBDNetwork.FBDList)
							{
								foreach (FBDPara current5 in current4.OutputParaList)
								{
									if (current5.LineToParaList.Contains(current3))
									{
										current5.LineToParaList.Remove(current3);
									}
								}
							}
						}
						foreach (FBDPara current6 in current2.OutputParaList)
						{
							foreach (FBD current7 in this._FBDNetwork.FBDList)
							{
								foreach (FBDPara current8 in current7.InputParaList)
								{
									if (current8.LineFromPara == current6)
									{
										current8.LineFromPara = null;
									}
								}
							}
						}
					}
					this._selectedFBDList.Clear();
					base.Invalidate();
				}
				if (this._selectedPara != null)
				{
					if (this._selectedPara.ParaType == FBDParaTypeEnum.In)
					{
						if (this._selectedPara.LineFromPara.LineToParaList.Contains(this._selectedPara))
						{
							this._selectedPara.LineFromPara.LineToParaList.Remove(this._selectedPara);
						}
						this._selectedPara.LineFromPara = null;
					}
					else if (this._selectedPara.ParaType == FBDParaTypeEnum.Out)
					{
						foreach (FBDPara current9 in this._selectedPara.LineToParaList)
						{
							current9.LineFromPara = null;
						}
						this._selectedPara.LineToParaList.Clear();
					}
					base.Invalidate();
				}
			}
		}
		public void RemoveRow()
		{
			if (this.FBDNetwork.RowCount - 1 < 0)
			{
				return;
			}
			foreach (FBD current in this._FBDNetwork.FBDList)
			{
				if (current.Location.Y + current.Size.Height > this.FBDNetwork.RowCount - 1)
				{
					return;
				}
			}
			this.FBDNetwork.RowCount--;
			this.RowCount = this._FBDNetwork.RowCount;
			base.Height = this.RowCount * 20 + 25 + 1;
		}
		public void AddRow()
		{
			this.FBDNetwork.RowCount++;
			this.RowCount = this._FBDNetwork.RowCount;
			base.Height = this.RowCount * 20 + 25 + 1;
		}
		private void FBDNetworkControl_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (this._selectedPara != null)
			{
				if (this._selectedPara.ParaType == FBDParaTypeEnum.In || this._selectedPara.ParaType == FBDParaTypeEnum.Out)
				{
					TextBox textBox = new TextBox();
					textBox.LostFocus += new EventHandler(this.paraSetTextBox_LostFocus);
					textBox.KeyDown += new KeyEventHandler(this.paraSetTextBox_KeyDown);
					textBox.Text = this._selectedPara.Value;
					textBox.Tag = this._selectedPara;
					int num = e.X / 100;
					int num2 = (e.Y - 25) / 20;
					textBox.Location = new Point(num * 100, num2 * 20 + 25);
					textBox.Size = new Size(100, 20);
					base.Controls.Add(textBox);
					textBox.Show();
					textBox.Focus();
					return;
				}
				if (this._selectedPara.ParaType == FBDParaTypeEnum.Head)
				{
					foreach (FBD current in this._FBDNetwork.FBDList)
					{
						if (current.HeadPara != null && current.HeadPara == this._selectedPara)
						{
							TextBox textBox2 = new TextBox();
							textBox2.TextAlign = HorizontalAlignment.Center;
							textBox2.LostFocus += new EventHandler(this.paraSetTextBox_LostFocus);
							textBox2.KeyDown += new KeyEventHandler(this.paraSetTextBox_KeyDown);
							textBox2.Text = this._selectedPara.Value;
							textBox2.Tag = this._selectedPara;
							textBox2.Location = new Point(100 * (current.Location.X + ((current.InputParaList.Count == 0) ? 0 : 1)), 20 * current.Location.Y + 25);
							textBox2.Size = new Size((current.Size.Width - ((current.InputParaList.Count == 0) ? 0 : 1) - ((current.OutputParaList.Count == 0) ? 0 : 1)) * 100, 20);
							base.Controls.Add(textBox2);
							textBox2.Show();
							textBox2.Focus();
							break;
						}
					}
				}
			}
		}
		private void paraSetTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				TextBox textBox = sender as TextBox;
				FBDPara fBDPara = textBox.Tag as FBDPara;
				if (fBDPara.ParaType == FBDParaTypeEnum.Head && textBox.Text == "")
				{
					textBox.Text = "[Head]";
				}
				if (fBDPara.Value != null && fBDPara.Value != "")
				{
					if (fBDPara.CanLine && fBDPara.ParaType == FBDParaTypeEnum.In && fBDPara.LineFromPara != null)
					{
						fBDPara.LineFromPara.LineToParaList.Remove(fBDPara.LineFromPara);
						fBDPara.LineFromPara = null;
					}
					else if (fBDPara.CanLine && fBDPara.ParaType == FBDParaTypeEnum.Out && fBDPara.LineToParaList.Count != 0)
					{
						foreach (FBDPara current in fBDPara.LineToParaList)
						{
							current.LineFromPara = null;
						}
						fBDPara.LineToParaList.Clear();
					}
				}
				base.Focus();
				base.Invalidate();
				return;
			}
			if (e.KeyCode == Keys.Escape)
			{
				TextBox textBox2 = sender as TextBox;
				FBDPara fBDPara2 = textBox2.Tag as FBDPara;
				textBox2.Text = fBDPara2.Value;
				base.Focus();
			}
		}
		private void paraSetTextBox_LostFocus(object sender, EventArgs e)
		{
			TextBox textBox = sender as TextBox;
			FBDPara fBDPara = textBox.Tag as FBDPara;
			if (fBDPara.ParaType == FBDParaTypeEnum.Head && textBox.Text == "")
			{
				textBox.Text = "[Head]";
			}
			fBDPara.Value = textBox.Text;
			if (fBDPara.Value != null && fBDPara.Value != "")
			{
				if (fBDPara.CanLine && fBDPara.ParaType == FBDParaTypeEnum.In && fBDPara.LineFromPara != null)
				{
					fBDPara.LineFromPara.LineToParaList.Remove(fBDPara.LineFromPara);
					fBDPara.LineFromPara = null;
				}
				else if (fBDPara.CanLine && fBDPara.ParaType == FBDParaTypeEnum.Out && fBDPara.LineToParaList.Count != 0)
				{
					foreach (FBDPara current in fBDPara.LineToParaList)
					{
						current.LineFromPara = null;
					}
					fBDPara.LineToParaList.Clear();
				}
			}
			textBox.Hide();
			base.Controls.Remove(textBox);
			base.Focus();
			base.Invalidate();
		}
		public void ReverseSelectedParaPin()
		{
			if (this._selectedPara != null && this._selectedPara.ParaType == FBDParaTypeEnum.In)
			{
				if (this._selectedPara.PinState == FBDParaPinState.Normal)
				{
					this._selectedPara.PinState = FBDParaPinState.Reverse;
				}
				else
				{
					this._selectedPara.PinState = FBDParaPinState.Normal;
				}
				base.Invalidate();
			}
		}
		public void AddInputParaPin()
		{
			if (this._selectedFBDList.Count == 1 && (this._selectedFBDList[0].Name == "AND" || this._selectedFBDList[0].Name == "OR"))
			{
				FBD fBD = this._selectedFBDList[0];
				FBDPara fBDPara = new FBDPara();
				fBDPara.Owner = fBD;
				fBDPara.ParaType = FBDParaTypeEnum.In;
				fBDPara.Name = "Input";
				fBDPara.Symbol = "";
				fBDPara.Index = fBD.InputParaList.Count;
				fBDPara.CanLine = true;
				fBDPara.DefaultValue = "";
				fBD.InputParaList.Add(fBDPara);
				fBD.Size = new Size(fBD.Size.Width, fBD.Size.Height + 1);
				base.Invalidate();
			}
		}
		public void RemoveInputParaPin()
		{
			if (this._selectedFBDList.Count == 1 && (this._selectedFBDList[0].Name == "AND" || this._selectedFBDList[0].Name == "OR"))
			{
				FBD fBD = this._selectedFBDList[0];
				if (fBD.InputParaList.Count - 1 < 1)
				{
					return;
				}
				fBD.InputParaList.RemoveAt(fBD.InputParaList.Count - 1);
				fBD.Size = new Size(fBD.Size.Width, fBD.Size.Height - 1);
				base.Invalidate();
			}
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			this.label1 = new Label();
			base.SuspendLayout();
			this.label1.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.label1.BackColor = Color.Transparent;
			this.label1.Location = new Point(4, 4);
			this.label1.Name = "label1";
			this.label1.Size = new Size(687, 14);
			this.label1.TabIndex = 1;
			this.label1.Text = "Network{0} Double click to add comment...";
			this.label1.DoubleClick += new EventHandler(this.label1_DoubleClick);
			this.AllowDrop = true;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.White;
			base.BorderStyle = BorderStyle.FixedSingle;
			base.Controls.Add(this.label1);
			this.DoubleBuffered = true;
			base.Name = "FBDNetworkControl";
			base.Size = new Size(694, 181);
			base.DragDrop += new DragEventHandler(this.FBDNetworkControl_DragDrop);
			base.DragEnter += new DragEventHandler(this.FBDNetworkControl_DragEnter);
			base.DragOver += new DragEventHandler(this.FBDNetworkControl_DragOver);
			base.DragLeave += new EventHandler(this.FBDNetworkControl_DragLeave);
			base.Paint += new PaintEventHandler(this.FBDNetworkControl_Paint);
			base.Enter += new EventHandler(this.FBDNetworkControl_Enter);
			base.KeyDown += new KeyEventHandler(this.FBDNetworkControl_KeyDown);
			base.Leave += new EventHandler(this.FBDNetworkControl_Leave);
			base.MouseClick += new MouseEventHandler(this.FBDNetworkControl_MouseClick);
			base.MouseDoubleClick += new MouseEventHandler(this.FBDNetworkControl_MouseDoubleClick);
			base.MouseDown += new MouseEventHandler(this.FBDNetworkControl_MouseDown);
			base.MouseMove += new MouseEventHandler(this.FBDNetworkControl_MouseMove);
			base.MouseUp += new MouseEventHandler(this.FBDNetworkControl_MouseUp);
			base.ResumeLayout(false);

            this.ReloadNetwork();
		}
	}
}
