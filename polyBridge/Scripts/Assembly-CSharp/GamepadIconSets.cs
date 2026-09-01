using UnityEngine;

public class GamepadIconSets : MonoBehaviour
{
	public GamepadIconSet[] m_GamepadIconSets;

	public Sprite GetIcon(GamepadType gamepadType, GamepadButtonType buttonType)
	{
		GamepadIconSet[] gamepadIconSets = m_GamepadIconSets;
		foreach (GamepadIconSet gamepadIconSet in gamepadIconSets)
		{
			if (gamepadIconSet.m_GamepadType == gamepadType)
			{
				return buttonType switch
				{
					GamepadButtonType.SELECT => gamepadIconSet.m_Select, 
					GamepadButtonType.START => gamepadIconSet.m_Start, 
					GamepadButtonType.SOUTH => gamepadIconSet.m_South, 
					GamepadButtonType.NORTH => gamepadIconSet.m_North, 
					GamepadButtonType.WEST => gamepadIconSet.m_West, 
					GamepadButtonType.EAST => gamepadIconSet.m_East, 
					GamepadButtonType.DPAD_DOWN => gamepadIconSet.m_DpadDown, 
					GamepadButtonType.DPAD_UP => gamepadIconSet.m_DpadUp, 
					GamepadButtonType.DPAD_LEFT => gamepadIconSet.m_DpadLeft, 
					GamepadButtonType.DPAD_RIGHT => gamepadIconSet.m_DpadRight, 
					GamepadButtonType.SHOULDER_LEFT => gamepadIconSet.m_ShoulderLeft, 
					GamepadButtonType.SHOULDER_RIGHT => gamepadIconSet.m_ShoulderRight, 
					GamepadButtonType.TRIGGER_LEFT => gamepadIconSet.m_TriggerLeft, 
					GamepadButtonType.TRIGGER_RIGHT => gamepadIconSet.m_TriggerRight, 
					GamepadButtonType.LEFTSTICK_PRESSED => gamepadIconSet.m_LeftStickPressed, 
					GamepadButtonType.RIGHTSTICK_PRESSED => gamepadIconSet.m_RightStickPressed, 
					GamepadButtonType.LEFTSTICK => gamepadIconSet.m_LeftStick, 
					GamepadButtonType.RIGHTSTICK => gamepadIconSet.m_RightStick, 
					GamepadButtonType.DPAD_ALL => gamepadIconSet.m_DpadAll, 
					GamepadButtonType.DPAD_VERTICAL => gamepadIconSet.m_DpadVertical, 
					GamepadButtonType.DPAD_HORIZONTAL => gamepadIconSet.m_DpadHorizontal, 
					_ => null, 
				};
			}
		}
		return null;
	}
}
