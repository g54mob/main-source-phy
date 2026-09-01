using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
	public TextMeshProUGUI m_Text;

	public Image m_Icon;

	public RectTransform m_RectTransform;

	public ContentSizeFitter m_ContentSizeFitter;

	public Image m_Background;

	public Image m_Outline;

	public GameObject m_MinHeight;

	private int m_MaxWidthOverride = -1;

	public void Awake()
	{
		if (GameUI.m_Instance != null)
		{
			SetColors(GameUI.m_Instance.m_TooltipColor, GameUI.m_Instance.m_TooltipOutlineColor);
		}
	}

	public void Start()
	{
		if (Game.IsRunningOnSteamDeck())
		{
			m_Text.fontSize = 16f;
			base.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else
		{
			m_Text.fontSize = 14f;
			base.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
		}
	}

	public void Set(string text, Sprite sprite)
	{
		if (m_Text.text != text)
		{
			GameUI.SetAndEnableText(m_Text, text);
			m_Text.ForceMeshUpdate();
			float num = 250f;
			if (m_MaxWidthOverride > 0)
			{
				num = m_MaxWidthOverride;
			}
			if (m_Text.preferredWidth > num)
			{
				m_ContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
				m_RectTransform.sizeDelta = new Vector2(num, m_RectTransform.sizeDelta.y);
			}
			else
			{
				m_ContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			}
			if (m_Icon != null)
			{
				m_Icon.sprite = sprite;
				m_Icon.gameObject.SetActive(sprite != null);
			}
			if (m_MinHeight != null && m_Icon != null)
			{
				m_MinHeight.SetActive(m_Icon.gameObject.activeInHierarchy);
			}
			LayoutRebuilder.ForceRebuildLayoutImmediate(m_RectTransform);
		}
	}

	public void SetMaxWidthOverride(int maxWidth)
	{
		m_MaxWidthOverride = maxWidth;
		string text = m_Text.text;
		m_Text.text = "";
		Set(text, null);
	}

	public void Enable()
	{
		if (Profiles.m_ActiveProfile.m_DisableTooltips || (Game.IsRunningOnSteamDeck() && GameInput.GetActiveGameDevice() == GameDevice.KeyboardAndMouse))
		{
			base.gameObject.SetActive(value: false);
		}
		else
		{
			base.gameObject.SetActive(value: true);
		}
	}

	public void ForceEnable()
	{
		base.gameObject.SetActive(value: true);
	}

	public void Disable()
	{
		base.gameObject.SetActive(value: false);
	}

	public void SetColors(Color backgroundColor, Color outlineColor)
	{
		m_Background.color = backgroundColor;
		m_Outline.color = outlineColor;
	}
}
