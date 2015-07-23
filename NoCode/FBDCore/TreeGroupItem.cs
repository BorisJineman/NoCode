using System;
namespace NoCode.FBDCore
{
	[Serializable]
	public class TreeGroupItem
	{
		private int _ID;
		private int _parentID;
		private string _name;
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
		public int ParentID
		{
			get
			{
				return this._parentID;
			}
			set
			{
				this._parentID = value;
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
	}
}
