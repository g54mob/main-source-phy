using System.Collections.Generic;
using UnityEngine;

public class GamepadRepeater
{
	private static readonly float INITIAL_DOWN_TIME_SECONDS = 0.4f;

	private static readonly float REPEAT_TIME_SECONDS_START = 0.15f;

	private static readonly float REPEAT_TIME_SECONDS_END = 0.03f;

	private static readonly float SECONDS_TO_REACH_MAX_REPEAT_TIME = 1.5f;

	private static List<GamepadButtonType> m_ButtonsToCheck = new List<GamepadButtonType>();

	private static Dictionary<GamepadButtonType, float> m_NextRepeatTime = new Dictionary<GamepadButtonType, float>();

	private static Dictionary<GamepadButtonType, bool> m_JustRepeated = new Dictionary<GamepadButtonType, bool>();

	private static Dictionary<GamepadButtonType, float> m_TimeRepeating = new Dictionary<GamepadButtonType, float>();

	public static void Init()
	{
		m_ButtonsToCheck.Add(GamepadButtonType.DPAD_DOWN);
		m_ButtonsToCheck.Add(GamepadButtonType.DPAD_UP);
		m_ButtonsToCheck.Add(GamepadButtonType.DPAD_LEFT);
		m_ButtonsToCheck.Add(GamepadButtonType.DPAD_RIGHT);
	}

	public static void UpdateManual()
	{
		foreach (GamepadButtonType item in m_ButtonsToCheck)
		{
			if (GamepadManager.ButtonJustPressed(item))
			{
				if (m_NextRepeatTime.ContainsKey(item))
				{
					m_NextRepeatTime[item] = Time.unscaledTime + INITIAL_DOWN_TIME_SECONDS;
				}
				else
				{
					m_NextRepeatTime.Add(item, Time.unscaledTime + INITIAL_DOWN_TIME_SECONDS);
				}
				if (m_TimeRepeating.ContainsKey(item))
				{
					m_TimeRepeating[item] = 0f;
				}
				else
				{
					m_TimeRepeating.Add(item, 0f);
				}
			}
			if (GamepadManager.ButtonJustReleased(item) || !GamepadManager.ButtonIsDown(item))
			{
				if (m_NextRepeatTime.ContainsKey(item))
				{
					m_NextRepeatTime.Remove(item);
				}
				if (m_JustRepeated.ContainsKey(item))
				{
					m_JustRepeated.Remove(item);
				}
			}
			if (!m_NextRepeatTime.ContainsKey(item))
			{
				continue;
			}
			m_TimeRepeating[item] += Time.unscaledDeltaTime;
			if (Time.unscaledTime > m_NextRepeatTime[item])
			{
				if (m_JustRepeated.ContainsKey(item))
				{
					m_JustRepeated[item] = true;
				}
				else
				{
					m_JustRepeated.Add(item, value: true);
				}
				float num = Time.unscaledTime - m_NextRepeatTime[item];
				float t = Mathf.Clamp01(m_TimeRepeating[item] / SECONDS_TO_REACH_MAX_REPEAT_TIME);
				float num2 = Mathf.SmoothStep(REPEAT_TIME_SECONDS_START, REPEAT_TIME_SECONDS_END, t);
				m_NextRepeatTime[item] = Time.unscaledTime - num + num2;
			}
			else if (m_JustRepeated.ContainsKey(item))
			{
				m_JustRepeated[item] = false;
			}
			else
			{
				m_JustRepeated.Add(item, value: false);
			}
		}
	}

	public static bool JustRepeated(GamepadButtonType buttonType)
	{
		if (!m_JustRepeated.ContainsKey(buttonType))
		{
			return false;
		}
		return m_JustRepeated[buttonType];
	}
}
