using ModIO;
using ModIO.UI;
using TFBGames;
using UnityEngine;
using UnityEngine.UI;

public class DMPlatformIconViewer : MonoBehaviour, IModViewElement
{
	private Image m_image;

	private MultiplayerPlatformIconsController m_platformIconsController;

	GameObject IModViewElement.gameObject => base.gameObject;

	public void SetModView(ModView view)
	{
		m_image = GetComponent<Image>();
		m_platformIconsController = ServiceLocator.GetService<MultiplayerPlatformIconsController>();
		view.onProfileChanged.AddListener(delegate(ModProfile modProfile)
		{
			UpdateIcon(modProfile);
		});
		UpdateIcon(view.profile);
	}

	private void UpdateIcon(ModProfile modProfile)
	{
	}
}
