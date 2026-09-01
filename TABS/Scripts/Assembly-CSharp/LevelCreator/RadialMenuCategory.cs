using System.Collections.Generic;

namespace LevelCreator
{
	public class RadialMenuCategory
	{
		public string CategoryName;

		public Dictionary<string, List<RadialMenuSlot>> Slots = new Dictionary<string, List<RadialMenuSlot>>();
	}
}
