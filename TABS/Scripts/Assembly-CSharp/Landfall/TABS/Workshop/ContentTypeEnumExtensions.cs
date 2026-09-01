namespace Landfall.TABS.Workshop
{
	public static class ContentTypeEnumExtensions
	{
		public static WorkshopContentType ToWorkshopTypeFilter(this ContentTypeFilter ct)
		{
			switch (ct)
			{
			case ContentTypeFilter.None:
				return WorkshopContentType.Any;
			case ContentTypeFilter.Battles:
				return WorkshopContentType.Battle;
			case ContentTypeFilter.Campaigns:
				return WorkshopContentType.Campaign;
			case ContentTypeFilter.Units:
				return WorkshopContentType.Unit;
			case ContentTypeFilter.Factions:
				return WorkshopContentType.Faction;
			case ContentTypeFilter.Any:
				return WorkshopContentType.Any;
			case ContentTypeFilter.Maps:
				return WorkshopContentType.Map;
			default:
				return WorkshopContentType.Any;
			}
		}

		public static ContentTypeFilter ToContentTypeFilter(this WorkshopContentType w)
		{
			switch (w)
			{
			case WorkshopContentType.Unit:
				return ContentTypeFilter.Units;
			case WorkshopContentType.Layout:
				return ContentTypeFilter.None;
			case WorkshopContentType.Battle:
				return ContentTypeFilter.Battles;
			case WorkshopContentType.Campaign:
				return ContentTypeFilter.Campaigns;
			case WorkshopContentType.Faction:
				return ContentTypeFilter.Factions;
			case WorkshopContentType.Any:
				return ContentTypeFilter.Any;
			case WorkshopContentType.Map:
				return ContentTypeFilter.Maps;
			default:
				return ContentTypeFilter.Any;
			}
		}
	}
}
