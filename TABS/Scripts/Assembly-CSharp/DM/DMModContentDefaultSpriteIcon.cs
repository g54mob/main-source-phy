using Landfall.TABS.Workshop;
using ModIO;
using ModIO.UI;
using UnityEngine;
using UnityEngine.UI;

namespace DM
{
	public class DMModContentDefaultSpriteIcon : MonoBehaviour, IModViewElement
	{
		public Image image;

		private ModView m_view;

		GameObject IModViewElement.gameObject => base.gameObject;

		private void Awake()
		{
			base.gameObject.SetActive(value: false);
		}

		public void SetModView(ModView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onProfileChanged.RemoveListener(DisplayDefaultSpriteIcon);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onProfileChanged.AddListener(DisplayDefaultSpriteIcon);
					DisplayDefaultSpriteIcon(m_view.profile);
				}
				else
				{
					DisplayDefaultSpriteIcon(null);
				}
			}
		}

		private void DisplayDefaultSpriteIcon(ModProfile profile)
		{
			if (profile == null)
			{
				image.enabled = false;
				return;
			}
			image.enabled = true;
			ContentDefaultSpriteIconService service = ServiceLocator.GetService<ContentDefaultSpriteIconService>();
			WorkshopContentType contentTypeFromModProfile = DMWorkshopUtility.GetContentTypeFromModProfile(profile);
			Sprite spriteFromContentType = service.GetSpriteFromContentType(contentTypeFromModProfile);
			if (spriteFromContentType != null)
			{
				image.sprite = spriteFromContentType;
			}
		}
	}
}
