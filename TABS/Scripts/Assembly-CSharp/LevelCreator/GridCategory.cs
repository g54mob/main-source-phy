using System.Collections.Generic;
using UnityEngine;

namespace LevelCreator
{
	public class GridCategory
	{
		public string CategoryName;

		public GameObject selectedItem;

		public Dictionary<string, GridGroup> Groups = new Dictionary<string, GridGroup>();
	}
}
