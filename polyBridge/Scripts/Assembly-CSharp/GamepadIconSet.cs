using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "GamepadIconSet", menuName = "Game/GamepadIconSet", order = 1)]
public class GamepadIconSet : ScriptableObject
{
	public GamepadType m_GamepadType;

	public Sprite m_Select;

	public Sprite m_Start;

	public Sprite m_South;

	public Sprite m_North;

	public Sprite m_West;

	public Sprite m_East;

	public Sprite m_DpadDown;

	public Sprite m_DpadUp;

	public Sprite m_DpadLeft;

	public Sprite m_DpadRight;

	public Sprite m_ShoulderLeft;

	public Sprite m_ShoulderRight;

	public Sprite m_TriggerLeft;

	public Sprite m_TriggerRight;

	public Sprite m_LeftStickPressed;

	public Sprite m_RightStickPressed;

	public Sprite m_LeftStick;

	public Sprite m_RightStick;

	public Sprite m_DpadAll;

	public Sprite m_DpadVertical;

	public Sprite m_DpadHorizontal;
}
