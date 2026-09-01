using Landfall.TABS.Workshop;

namespace DM
{
	public struct Filter
	{
		public string NamePart;

		public bool ExactNameMatch;

		public WorkshopTypeFilter WorkshopTypeFilter;

		public static Filter CreateMatchNamePartAndTypeFilter(string namePart, WorkshopTypeFilter workshopTypeFilter)
		{
			return new Filter
			{
				NamePart = namePart,
				ExactNameMatch = false,
				WorkshopTypeFilter = workshopTypeFilter
			};
		}

		public static Filter CreateMatchNamePartFilter(string namePart)
		{
			return new Filter
			{
				NamePart = namePart,
				ExactNameMatch = false,
				WorkshopTypeFilter = WorkshopTypeFilter.All
			};
		}

		public static Filter CreateMatchLocalAndExactNameFilter(string name)
		{
			return new Filter
			{
				NamePart = name,
				ExactNameMatch = true,
				WorkshopTypeFilter = WorkshopTypeFilter.Local
			};
		}
	}
}
