using System;

namespace Landfall.TABS.Workshop
{
	[Flags]
	public enum WorkshopTypeFilter
	{
		Local = 0,
		Workshop = 1,
		WorkshopSelf = 2,
		AllWorkshop = 3,
		All = 4
	}
}
