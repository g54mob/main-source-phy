using UnityEngine;
using UnityEngine.UI;

public class SandboxPanelColor : MonoBehaviour
{
	public Image m_Panel;

	private void OnEnable()
	{
		if (GameUI.m_Instance != null)
		{
			m_Panel.color = GameUI.m_Instance.m_ToolBarForegroundColor;
		}
	}
}
