using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace NoCode.FBDCore
{
	[Serializable]
	public class FBD
	{
		private int _ID;
		private int _code;
		private List<FBDPara> _inputParaList = new List<FBDPara>();
		private List<FBDPara> _outputParaList = new List<FBDPara>();
		private FBDPara _headPara;
		private string _name;
		private TreeGroupItem _group;
		private string _illustration;
		private Size _size;
		private Point _location;
		private bool _selected;
		private bool inCompile;
		private Color _FBDColor;
		private int _order;
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}
		public int Code
		{
			get
			{
				return this._code;
			}
			set
			{
				this._code = value;
			}
		}
		public List<FBDPara> InputParaList
		{
			get
			{
				return this._inputParaList;
			}
			set
			{
				this._inputParaList = value;
			}
		}
		public List<FBDPara> OutputParaList
		{
			get
			{
				return this._outputParaList;
			}
			set
			{
				this._outputParaList = value;
			}
		}
		public FBDPara HeadPara
		{
			get
			{
				return this._headPara;
			}
			set
			{
				this._headPara = value;
			}
		}
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}
		public TreeGroupItem Group
		{
			get
			{
				return this._group;
			}
			set
			{
				this._group = value;
			}
		}
		public string Illustration
		{
			get
			{
				return this._illustration;
			}
			set
			{
				this._illustration = value;
			}
		}
		public Size Size
		{
			get
			{
				return this._size;
			}
			set
			{
				this._size = value;
			}
		}
		public Point Location
		{
			get
			{
				return this._location;
			}
			set
			{
				this._location = value;
			}
		}
		public bool Selected
		{
			get
			{
				return this._selected;
			}
			set
			{
				this._selected = value;
			}
		}
		public Color FBDColor
		{
			get
			{
				return this._FBDColor;
			}
			set
			{
				this._FBDColor = value;
			}
		}
		public int Order
		{
			get
			{
				return this._order;
			}
			set
			{
				this._order = value;
			}
		}
		public FBD Clone()
		{
			FBD result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, this);
				using (MemoryStream memoryStream2 = new MemoryStream(memoryStream.ToArray()))
				{
					FBD fBD = (FBD)binaryFormatter.Deserialize(memoryStream2);
					result = fBD;
				}
			}
			return result;
		}
		public Image DrawMe()
		{
			Bitmap bitmap = new Bitmap(100 * this._size.Width + 1, 20 * this._size.Height + 1);
			Graphics graphics = Graphics.FromImage(bitmap);
			Rectangle rect = new Rectangle(100 * ((this.InputParaList.Count == 0) ? 0 : 1), 20 * ((this.HeadPara == null) ? 0 : 1), (this._size.Width - ((this.InputParaList.Count == 0) ? 0 : 1) - ((this.OutputParaList.Count == 0) ? 0 : 1)) * 100, (this._size.Height - ((this.HeadPara == null) ? 0 : 1)) * 20);
			LinearGradientBrush brush = new LinearGradientBrush(rect, Color.White, this._FBDColor, 75f);
			graphics.FillRectangle(brush, rect);
			Pen pen;
			if (this._selected)
			{
				pen = new Pen(Color.Red, 2f);
			}
			else
			{
				pen = new Pen(Color.Black, 0f);
			}
			graphics.DrawRectangle(pen, rect);
			SizeF sizeF = graphics.MeasureString(this._name + ":" + this._order.ToString(), SystemFonts.DefaultFont);
			graphics.DrawString(this._name + ":" + this._order.ToString(), SystemFonts.DefaultFont, Brushes.Black, (float)(rect.X + rect.Width / 2) - sizeF.Width / 2f, (float)(rect.Y + 10) - sizeF.Height / 2f);
			if (this._headPara != null)
			{
				Image image = this._headPara.DrawMe();
				graphics.DrawImage(image, rect.X + rect.Width / 2 - image.Width / 2, rect.Y - 10 - image.Height / 2);
			}
			foreach (FBDPara current in this.InputParaList)
			{
				graphics.DrawImage(current.DrawMe(), 0, 20 * current.Index);
			}
			foreach (FBDPara current2 in this.OutputParaList)
			{
				graphics.DrawImage(current2.DrawMe(), rect.X + rect.Width - 100, 20 * current2.Index);
			}
			return bitmap;
		}
		public FBDHitTestResultEnum HitTest(Point p)
		{
			Rectangle rect = new Rectangle(100 * ((this.InputParaList.Count == 0) ? 0 : 1), 20 * ((this.HeadPara == null) ? 0 : 1), (this._size.Width - ((this.InputParaList.Count == 0) ? 0 : 1) - ((this.OutputParaList.Count == 0) ? 0 : 1)) * 100, (this._size.Height - ((this.HeadPara == null) ? 0 : 1)) * 20);
			Region region = new Region(rect);
			if (region.IsVisible(p))
			{
				return FBDHitTestResultEnum.FBD;
			}
			return FBDHitTestResultEnum.None;
		}
		public void SortOrder(List<FBD> sortedFBDList)
		{
			if (this.inCompile || sortedFBDList.Contains(this))
			{
				return;
			}
			this.inCompile = true;
			foreach (FBDPara current in this.InputParaList)
			{
				if (current.LineFromPara != null)
				{
					current.LineFromPara.Owner.SortOrder(sortedFBDList);
				}
			}
			sortedFBDList.Add(this);
			this.inCompile = false;
		}
		public bool Check()
		{
			bool result = true;
			foreach (FBDPara current in this.InputParaList)
			{
				if (!current.Check())
				{
					result = false;
				}
			}
			foreach (FBDPara current2 in this.OutputParaList)
			{
				if (!current2.Check())
				{
					result = false;
				}
			}
			if (this._headPara != null && !this._headPara.Check())
			{
				result = false;
			}
			return result;
		}
		public void Compile()
		{
		}
	}
}
