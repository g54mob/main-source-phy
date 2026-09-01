using Landfall.TABS;
using Landfall.TABS.Workshop;
using UnityEngine;

namespace DM
{
	public class ContentDefaultSpriteIconService : ServicePrefab
	{
		[SerializeField]
		private Sprite m_unknownContentTypeSpriteIcon;

		[SerializeField]
		private Sprite m_unitSpriteIcon;

		[SerializeField]
		private Sprite m_factionSpriteIcon;

		[SerializeField]
		private Sprite m_battleSpriteIcon;

		[SerializeField]
		private Sprite m_campaignSpriteIcon;

		[SerializeField]
		private Sprite m_customMapSpriteIcon;

		public Sprite GetSpriteFromEntity(DatabaseEntity entity)
		{
			return GetSpriteFromContentType(entity.ContentType);
		}

		public Sprite GetSpriteFromContentType(WorkshopContentType contentType)
		{
			switch (contentType)
			{
			case WorkshopContentType.Unit:
				return m_unitSpriteIcon;
			case WorkshopContentType.Layout:
			case WorkshopContentType.Battle:
				return m_battleSpriteIcon;
			case WorkshopContentType.Campaign:
				return m_campaignSpriteIcon;
			case WorkshopContentType.Faction:
				return m_factionSpriteIcon;
			case WorkshopContentType.Map:
				return m_customMapSpriteIcon;
			default:
				return m_unknownContentTypeSpriteIcon;
			}
		}
	}
}
