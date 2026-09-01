using System;
using UnityEngine;

namespace LevelCreator
{
	[Serializable]
	public class BindingIconRow : DataTableRow
	{
		[SerializeField]
		public string BindingName;

		[SerializeField]
		public Sprite Icon;

		[SerializeField]
		public int SpriteSheetIndex;

		public string GetRowName()
		{
			return BindingName;
		}
	}
}
