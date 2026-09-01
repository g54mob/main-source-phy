using System;

namespace MoreMountains.Tools
{
	[Serializable]
	public class MMDebugMenuTabData
	{
		public string Name;

		public bool Active;

		[MMReorderableAttribute]
		public MMDebugMenuItemList MenuItems;
	}
}
