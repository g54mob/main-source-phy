using System;
using DM;
using Landfall.TABS.Workshop;
using UnityEngine;

namespace Landfall.TABS
{
	[Serializable]
	public class DatabaseEntity
	{
		[SerializeField]
		private string m_name;

		[SerializeField]
		private Sprite m_spriteIcon;

		[SerializeField]
		private string m_spriteIconPath;

		[SerializeField]
		private DatabaseID m_guid;

		[SerializeField]
		private string m_unlockKey;

		private WorkshopContentType m_entityContentType = WorkshopContentType.Any;

		private bool m_contentTypeSet;

		public string Name
		{
			get
			{
				return m_name;
			}
			set
			{
				m_name = value;
			}
		}

		public Sprite LargeSpriteIcon => CacheUtil.GetUnitSpriteLargeIcon(m_spriteIcon, m_spriteIconPath);

		public Sprite SmallSpriteIcon => CacheUtil.GetUnitSpriteSmallIcon(m_spriteIcon, m_spriteIconPath);

		public Sprite LevelCellSpriteIcon => CacheUtil.GetLevelCellIcon(m_spriteIcon, m_spriteIconPath);

		[Obsolete("To guarantee use of correct FileIO systems, replace usage with GetSpriteIconAsync")]
		public Sprite SpriteIcon
		{
			get
			{
				Sprite spriteIcon = CacheUtil.GetSpriteIcon(m_spriteIcon, m_spriteIconPath);
				if (spriteIcon == null)
				{
					return ServiceLocator.GetService<ContentDefaultSpriteIconService>().GetSpriteFromEntity(this);
				}
				return spriteIcon;
			}
			set
			{
				m_spriteIcon = value;
			}
		}

		public DatabaseID GUID
		{
			get
			{
				return m_guid;
			}
			set
			{
				m_guid = value;
			}
		}

		public string UnlockKey => m_unlockKey;

		public WorkshopContentType ContentType
		{
			get
			{
				if (!m_contentTypeSet)
				{
					return WorkshopContentType.Any;
				}
				return m_entityContentType;
			}
		}

		public void GetSpriteIconAsync(Action<Sprite> doneCallback)
		{
			CacheUtil.GetSpriteIconAsync(m_spriteIcon, m_spriteIconPath, delegate(Sprite sprite)
			{
				if (sprite == null)
				{
					sprite = ServiceLocator.GetService<ContentDefaultSpriteIconService>().GetSpriteFromContentType(ContentType);
				}
				doneCallback?.Invoke(sprite);
			});
		}

		public void InvalidateSprite()
		{
			CacheUtil.InvalidateSprite(m_spriteIconPath);
		}

		[Obsolete("To guarantee use of correct FileIO systems, replace usage with GetSpriteIconAsync")]
		public Sprite GetSpriteIcon()
		{
			return m_spriteIcon;
		}

		public void SetSpriteIcon(Sprite newIcon)
		{
			m_spriteIcon = newIcon;
		}

		public string GetSpriteIconPath()
		{
			return m_spriteIconPath;
		}

		public void SetSpriteIconPath(string spriteIconPath)
		{
			m_spriteIcon = null;
			m_spriteIconPath = spriteIconPath;
		}

		public DatabaseEntity(WorkshopContentType contentType)
		{
			m_entityContentType = contentType;
			m_contentTypeSet = true;
			if (m_guid == default(DatabaseID))
			{
				GenerateNewID();
			}
		}

		public void NewModID(int modID)
		{
			m_guid = new DatabaseID(modID, m_guid.m_ID);
		}

		public void GenerateNewID()
		{
			m_guid = DatabaseID.NewID();
		}

		public DatabaseEntity ShallowCopy()
		{
			return (DatabaseEntity)MemberwiseClone();
		}

		public void SetUnlockKey(string unlockKey)
		{
			m_unlockKey = unlockKey;
		}
	}
}
