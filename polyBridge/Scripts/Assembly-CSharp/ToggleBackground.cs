using UnityEngine;
using UnityEngine.UI;

public class ToggleBackground : MonoBehaviour
{
	public Toggle m_Toggle;

	public Image m_Background;

	public Sprite m_BackgroundNormal;

	public Sprite m_BackgroundDisabled;

	public ToolTipText m_ToolTipText;

	public void ManualUpdate()
	{
		m_Background.sprite = (m_Toggle.interactable ? m_BackgroundNormal : m_BackgroundDisabled);
	}
}
