using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_TabButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	public Button m_Button;

	public Image m_Background;

	public GameObject m_Separator;

	public TMP_Text m_Text;

	private bool m_Selected;

	public void SetSelected(bool selected)
	{
		if (selected)
		{
			Select();
		}
		else
		{
			UnSelect();
		}
	}

	public void Select()
	{
		m_Background.color = GameUI.m_Instance.m_TabSelectedColor;
		m_Separator.SetActive(value: false);
		m_Text.color = GameUI.m_Instance.m_YellowTextColor;
		m_Selected = true;
	}

	public void UnSelect()
	{
		m_Background.color = GameUI.m_Instance.m_TabDefaultColor;
		m_Separator.SetActive(value: true);
		m_Text.color = Color.white;
		m_Selected = false;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!m_Selected)
		{
			m_Background.color = GameUI.m_Instance.m_TabHoverColor;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!m_Selected)
		{
			m_Background.color = GameUI.m_Instance.m_TabDefaultColor;
		}
	}
}
