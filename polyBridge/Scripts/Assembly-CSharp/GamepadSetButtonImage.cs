using UnityEngine;
using UnityEngine.UI;

public class GamepadSetButtonImage : MonoBehaviour
{
	public GamepadButtonType m_GamepadButtonType;

	public void OnEnable()
	{
		Image component = GetComponent<Image>();
		if (component != null)
		{
			Sprite icon = GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), m_GamepadButtonType);
			if (icon != null)
			{
				component.sprite = icon;
			}
		}
	}
}
