using System;
using UnityEngine;

namespace LevelCreator
{
	[Serializable]
	public class GridIconRow : DataTableRow
	{
		public string name;

		public Sprite Icon;

		public string GetRowName()
		{
			return name;
		}

		public string GetLocalizedRowName()
		{
			return "LC_ITEMGRID_CATEGORY_" + name.ToUpper();
		}
	}
}
