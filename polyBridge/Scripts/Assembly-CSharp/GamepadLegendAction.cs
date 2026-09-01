using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamepadLegendAction : MonoBehaviour
{
	public Image m_Icon;

	public Image m_Icon2;

	public TextMeshProUGUI m_Label;

	public void Show(Sprite icon, string label)
	{
		m_Icon.sprite = icon;
		m_Label.text = label;
		if (!base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(value: true);
		}
		if (m_Icon2.gameObject.activeInHierarchy)
		{
			m_Icon2.gameObject.SetActive(value: false);
		}
	}

	public void Show2(Sprite icon, Sprite icon2, string label)
	{
		m_Icon.sprite = icon;
		m_Icon2.sprite = icon2;
		m_Label.text = label;
		if (!base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(value: true);
		}
		if (!m_Icon2.gameObject.activeInHierarchy)
		{
			m_Icon2.gameObject.SetActive(value: true);
		}
	}

	public void Hide()
	{
		if (base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
