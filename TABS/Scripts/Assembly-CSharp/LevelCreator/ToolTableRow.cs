using System;
using UnityEngine;

namespace LevelCreator
{
	[Serializable]
	public class ToolTableRow : DataTableRow
	{
		public enum ToolTableCategory
		{
			Tools = 0,
			Hidden = 1,
			Experimental = 2
		}

		public string name;

		public Sprite icon;

		public GameObject toolPrefab;

		public string tutorialMessage;

		[Tooltip("This is used for tools that open from other panels, for example the screenshottool,which returns to the save menu and should not interact with the escape menu while in the stateof taking a thumbnail")]
		public bool blockEscapeMenu;

		[Tooltip("Blocks the grid menu that pops up from the bottom of the screen.")]
		public bool blocksGridMenu;

		[Space]
		public ToolTableCategory category;

		public string group;

		public string slot;

		public string Path => category.ToString() + "/" + ((group != "") ? group : "None") + "/" + ((slot != "") ? slot : "None");

		public string GetRowName()
		{
			return name;
		}
	}
}
