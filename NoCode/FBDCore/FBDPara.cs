using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
namespace NoCode.FBDCore
{
	[Serializable]
	public class FBDPara
	{
		private string _name;
		private string _value;
		private DataTypeEnum _valueType;
		private string _symbol;
		private string _defaultValue;
		private int _index;
		private FBD _owner;
		private FBDParaTypeEnum _paraType;
		private bool _canLine;
		private bool _canEmpty;
		private string _checkRegex;
		private bool checkErr;
		private FBDParaSelectedState _selectState;
		private FBDParaPinState _pinState;
		private List<FBDPara> _lineToParaList = new List<FBDPara>();
		private FBDPara _lineFromPara;
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
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
				this.Check();
			}
		}
		public DataTypeEnum ValueType
		{
			get
			{
				return this._valueType;
			}
			set
			{
				this._valueType = value;
			}
		}
		public string Symbol
		{
			get
			{
				return this._symbol;
			}
			set
			{
				this._symbol = value;
			}
		}
		public string DefaultValue
		{
			get
			{
				return this._defaultValue;
			}
			set
			{
				this._defaultValue = value;
				if (this._value == null)
				{
					this._value = this._defaultValue;
				}
			}
		}
		public int Index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}
		public FBD Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
			}
		}
		public FBDParaTypeEnum ParaType
		{
			get
			{
				return this._paraType;
			}
			set
			{
				this._paraType = value;
			}
		}
		public bool CanLine
		{
			get
			{
				return this._canLine;
			}
			set
			{
				this._canLine = value;
			}
		}
		public bool CanEmpty
		{
			get
			{
				return this._canEmpty;
			}
			set
			{
				this._canEmpty = value;
			}
		}
		public string CheckRegex
		{
			get
			{
				return this._checkRegex;
			}
			set
			{
				this._checkRegex = value;
			}
		}
		public FBDParaSelectedState SelectState
		{
			get
			{
				return this._selectState;
			}
			set
			{
				this._selectState = value;
			}
		}
		public FBDParaPinState PinState
		{
			get
			{
				return this._pinState;
			}
			set
			{
				this._pinState = value;
			}
		}
		public List<FBDPara> LineToParaList
		{
			get
			{
				return this._lineToParaList;
			}
			set
			{
				this._lineToParaList = value;
			}
		}
		public FBDPara LineFromPara
		{
			get
			{
				return this._lineFromPara;
			}
			set
			{
				this._lineFromPara = value;
			}
		}
		public Image DrawMe()
		{
			Brush brush;
			Pen pen;
			if (this.checkErr)
			{
				brush = Brushes.Red;
				pen = Pens.Red;
			}
			else
			{
				brush = Brushes.Black;
				pen = Pens.Black;
			}
			if (this._paraType == FBDParaTypeEnum.In)
			{
				Bitmap bitmap = new Bitmap(200, 20);
				Graphics graphics = Graphics.FromImage(bitmap);
				SizeF sizeF = graphics.MeasureString(this._symbol, SystemFonts.DefaultFont);
				graphics.DrawString(this._symbol, SystemFonts.DefaultFont, Brushes.Black, 105f, 10f - sizeF.Height / 2f);
				if (this._selectState == FBDParaSelectedState.Para)
				{
					graphics.FillRectangle(Brushes.LightGray, 0, 0, 99, 20);
				}
				sizeF = graphics.MeasureString(this._value, SystemFonts.DefaultFont);
				if (!this._value.StartsWith("TEMP"))
				{
					graphics.DrawString(this._value, SystemFonts.DefaultFont, brush, 90f - sizeF.Width, 10f - sizeF.Height / 2f);
				}
				if (this._selectState == FBDParaSelectedState.Pin)
				{
					graphics.FillRectangle(Brushes.LightGray, 95, 0, 4, 20);
				}
				if (this._pinState == FBDParaPinState.Normal)
				{
					graphics.DrawLine(pen, 95, 10, 100, 10);
				}
				else
				{
					graphics.DrawEllipse(pen, 95f, 7.5f, 5f, 5f);
				}
				return bitmap;
			}
			if (this._paraType == FBDParaTypeEnum.Out)
			{
				Bitmap bitmap2 = new Bitmap(200, 20);
				Graphics graphics2 = Graphics.FromImage(bitmap2);
				if (this._selectState == FBDParaSelectedState.Para)
				{
					graphics2.FillRectangle(Brushes.LightGray, 101, 0, 99, 20);
				}
				SizeF sizeF2 = graphics2.MeasureString(this._value, SystemFonts.DefaultFont);
				if (!this._value.StartsWith("TEMP"))
				{
					graphics2.DrawString(this._value, SystemFonts.DefaultFont, brush, 110f, 10f - sizeF2.Height / 2f);
				}
				sizeF2 = graphics2.MeasureString(this._symbol, SystemFonts.DefaultFont);
				graphics2.DrawString(this._symbol, SystemFonts.DefaultFont, Brushes.Black, 95f - sizeF2.Width, 10f - sizeF2.Height / 2f);
				if (this._selectState == FBDParaSelectedState.Pin)
				{
					graphics2.FillRectangle(Brushes.LightGray, 101, 0, 4, 20);
				}
				graphics2.DrawLine(pen, 100, 10, 105, 10);
				return bitmap2;
			}
			if (this._paraType == FBDParaTypeEnum.Head)
			{
				Bitmap image = new Bitmap(100, 20);
				Graphics graphics3 = Graphics.FromImage(image);
				SizeF sizeF3 = graphics3.MeasureString(this._value, SystemFonts.DefaultFont);
				Bitmap bitmap3 = new Bitmap(Convert.ToInt32(sizeF3.Width + 1f), Convert.ToInt32(sizeF3.Height + 1f));
				graphics3 = Graphics.FromImage(bitmap3);
				if (this._selectState == FBDParaSelectedState.Para)
				{
					graphics3.Clear(Color.LightGray);
				}
				graphics3.DrawString(this._value, SystemFonts.DefaultFont, brush, new Point(0, 0));
				return bitmap3;
			}
			throw new Exception("Error FBDParaType");
		}
		public bool Check()
		{
			if (this._canLine)
			{
				if (this.ParaType == FBDParaTypeEnum.In && this._lineFromPara != null)
				{
					this.checkErr = false;
					return true;
				}
				if (this.ParaType == FBDParaTypeEnum.Out && this._lineToParaList.Count != 0)
				{
					this.checkErr = false;
					return true;
				}
			}
			if (this._canEmpty && (this._value == null || this._value == "" || this._value == ">|"))
			{
				return true;
			}
			if (this._checkRegex == null || this._checkRegex == "")
			{
				this.checkErr = false;
				return true;
			}
			Regex regex = new Regex(this._checkRegex);
			this.checkErr = !regex.IsMatch(this._value);
			return !this.checkErr;
		}
		public void Compile()
		{
		}
		public FBDHitTestResultEnum HitTest(Point p)
		{
			if (this._paraType == FBDParaTypeEnum.In)
			{
				if (this._canLine)
				{
					Rectangle rect = new Rectangle(95, 0, 5, 20);
					Region region = new Region(rect);
					if (region.IsVisible(p))
					{
						return FBDHitTestResultEnum.InParaPin;
					}
				}
				Rectangle rect2 = new Rectangle(0, 0, 100, 20);
				Region region2 = new Region(rect2);
				if (region2.IsVisible(p))
				{
					return FBDHitTestResultEnum.InPara;
				}
			}
			else if (this._paraType == FBDParaTypeEnum.Out)
			{
				if (this._canLine)
				{
					Rectangle rect3 = new Rectangle(100, 0, 5, 20);
					Region region3 = new Region(rect3);
					if (region3.IsVisible(p))
					{
						return FBDHitTestResultEnum.OutParaPin;
					}
				}
				Rectangle rect4 = new Rectangle(100, 0, 100, 20);
				Region region4 = new Region(rect4);
				if (region4.IsVisible(p))
				{
					return FBDHitTestResultEnum.OutPara;
				}
			}
			else if (this._paraType == FBDParaTypeEnum.Head)
			{
				Bitmap image = new Bitmap(100, 20);
				Graphics graphics = Graphics.FromImage(image);
				SizeF sizeF = graphics.MeasureString(this._value, SystemFonts.DefaultFont);
				Rectangle rect5 = new Rectangle(-Convert.ToInt32(sizeF.Width / 2f), 0, Convert.ToInt32(sizeF.Width), 20);
				Region region5 = new Region(rect5);
				if (region5.IsVisible(p))
				{
					return FBDHitTestResultEnum.HeadPara;
				}
			}
			return FBDHitTestResultEnum.None;
		}
	}
}
