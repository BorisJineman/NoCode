using System;
using System.Collections.Generic;
namespace NoCode.FBDCore
{
	[Serializable]
	public class FBDNetwork
	{
		private int _ID;
		private string _label;
		private List<FBD> _FBDList = new List<FBD>();
		private int _columnCount = 32;
		private int _rowCount = 10;
		private List<FBD> sortedFBDList = new List<FBD>();
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
		public string Label
		{
			get
			{
				return this._label;
			}
			set
			{
				this._label = value;
			}
		}
		public List<FBD> FBDList
		{
			get
			{
				return this._FBDList;
			}
		}
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
		public List<FBD> SortedFBDList
		{
			get
			{
				return this.sortedFBDList;
			}
			set
			{
				this.sortedFBDList = value;
			}
		}
		public bool Check()
		{
			bool result = true;
			foreach (FBD current in this._FBDList)
			{
				if (!current.Check())
				{
					result = false;
				}
			}
			return result;
		}
		public List<FBD> Compile()
		{
			if (this.sortedFBDList == null)
			{
				this.sortedFBDList = new List<FBD>();
			}
			this.sortedFBDList.Clear();
			foreach (FBD current in this._FBDList)
			{
				bool flag = true;
				using (List<FBDPara>.Enumerator enumerator2 = current.OutputParaList.GetEnumerator())
				{
					if (enumerator2.MoveNext())
					{
						FBDPara current2 = enumerator2.Current;
						if (current2.LineToParaList.Count != 0)
						{
							flag = false;
						}
					}
				}
				if (flag)
				{
					current.SortOrder(this.sortedFBDList);
				}
			}
			Dictionary<FBDPara, int> dictionary = new Dictionary<FBDPara, int>();
			foreach (FBD current3 in this.sortedFBDList)
			{
				foreach (FBDPara current4 in current3.InputParaList)
				{
					if (current4.LineFromPara != null && dictionary.ContainsKey(current4))
					{
						dictionary.Remove(current4);
					}
				}
				foreach (FBDPara current5 in current3.OutputParaList)
				{
					if (current5.LineToParaList.Count != 0)
					{
						int num = 0;
						while (true)
						{
							IL_13F:
							foreach (int current6 in dictionary.Values)
							{
								if (current6 == num)
								{
									num++;
									goto IL_13F;
								}
							}
							break;
						}
						current5.Value = "TEMP" + (num / 16).ToString() + "." + (num % 16).ToString();
						foreach (FBDPara current7 in current5.LineToParaList)
						{
							current7.Value = current5.Value;
							dictionary.Add(current7, num);
						}
					}
				}
			}
			for (int i = 0; i < this.sortedFBDList.Count; i++)
			{
				this.sortedFBDList[i].Order = i;
			}
			return this.sortedFBDList;
		}
	}
}
