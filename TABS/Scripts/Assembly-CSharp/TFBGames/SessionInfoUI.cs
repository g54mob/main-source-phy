using Landfall.TABS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	public class SessionInfoUI : MonoBehaviour
	{
		[SerializeField]
		private Image mapImage;

		[SerializeField]
		private LocalizeText mapNameText;

		[SerializeField]
		private TMP_Text hostInfoText;

		[SerializeField]
		private Image hostPlatformImage;

		public void SetMapInfo(MapAsset mapData)
		{
			if (mapImage != null)
			{
				mapData.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
				{
					if (sprite != null && mapImage != null)
					{
						mapImage.sprite = sprite;
					}
				});
			}
			if (mapNameText != null)
			{
				mapNameText.LocaleID = mapData.Entity.Name;
			}
		}

		public void SetHostInfo(string hostName, MultiplayerPlatform hostPlatform)
		{
			hostInfoText.text = hostName;
			MultiplayerPlatformIconsController service = ServiceLocator.GetService<MultiplayerPlatformIconsController>();
			hostPlatformImage.sprite = service.GetIcon(hostPlatform);
		}
	}
}
