using Landfall.TABS;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapGrid : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public Image fadeImage;

	[SerializeField]
	private LocalizeText m_hoverText;

	[SerializeField]
	private LocalizeText m_normalText;

	public Image m_Image;

	public GameObject LockImage;

	private bool hoverTextEnabled;

	private Color color;

	private bool locked;

	public MapGridSelectedColorChanger selected;

	public Image m_WinStreakImage;

	private bool isHovering;

	public void Setup(Sprite sprite, string text, bool localized, string hoverText)
	{
		if (!string.IsNullOrEmpty(hoverText))
		{
			fadeImage.enabled = true;
			hoverTextEnabled = true;
			m_hoverText.Localized = localized;
			m_hoverText.LocaleID = hoverText;
			color = fadeImage.color;
		}
		Setup(sprite, text, localized);
	}

	public void Setup(Sprite sprite, string text, bool localized)
	{
		if (sprite != null)
		{
			m_Image.sprite = sprite;
		}
		m_normalText.Localized = localized;
		m_normalText.LocaleID = text;
	}

	public void Selected()
	{
		selected.Selected();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isHovering = true;
		if (hoverTextEnabled)
		{
			m_hoverText.Text.enabled = true;
			m_normalText.Text.enabled = false;
			if (locked)
			{
				LockImage.SetActive(value: false);
			}
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isHovering = false;
		if (hoverTextEnabled)
		{
			m_hoverText.Text.enabled = false;
			m_normalText.Text.enabled = true;
			if (locked)
			{
				LockImage.SetActive(value: true);
			}
		}
	}

	private void Update()
	{
		if (!locked && hoverTextEnabled)
		{
			float b = 0f;
			if (isHovering)
			{
				b = 0.99f;
			}
			fadeImage.color = new Color(color.r, color.g, color.b, Mathf.Lerp(fadeImage.color.a, b, Time.unscaledDeltaTime * 20f));
		}
	}

	public void Lock()
	{
		color = fadeImage.color;
		locked = true;
		fadeImage.color = new Color(color.r, color.g, color.b, 0.98f);
		LockImage.SetActive(value: true);
		fadeImage.enabled = true;
	}

	public void CheckLevelWinStreak(DatabaseID lvl, DatabaseID campaignID)
	{
		bool active = ServiceLocator.GetService<ISaveLoaderService>().LevelIsInWinStreak(lvl, campaignID);
		m_WinStreakImage.gameObject.SetActive(active);
	}

	public void CheckCamapaignWinStreak(DatabaseID campaignID)
	{
		bool active = ServiceLocator.GetService<ISaveLoaderService>().HasBeatenCampaignWithoutLosing(campaignID);
		m_WinStreakImage.gameObject.SetActive(active);
	}
}
