using UnityEngine;
using UnityEngine.UI;

public class ToolBar : MonoBehaviour
{
	[Header("Images")]
	public Image m_Background;

	public Image m_Foreground;

	private void OnEnable()
	{
		if (m_Background != null && GameUI.m_Instance != null)
		{
			m_Background.color = GameUI.m_Instance.m_ToolBarBackgroundColor;
		}
		if (m_Foreground != null && GameUI.m_Instance != null)
		{
			m_Foreground.color = GameUI.m_Instance.m_ToolBarForegroundColor;
		}
	}
}
