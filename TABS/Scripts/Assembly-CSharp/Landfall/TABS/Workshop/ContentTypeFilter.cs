using System;

namespace Landfall.TABS.Workshop
{
	[Flags]
	public enum ContentTypeFilter
	{
		None = 0,
		Battles = 1,
		Campaigns = 2,
		Units = 4,
		Factions = 8,
		Any = 0x10,
		Maps = 0x20
	}
}
