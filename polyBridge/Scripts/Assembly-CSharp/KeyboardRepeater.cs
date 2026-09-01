using System.Collections.Generic;
using UnityEngine;

public class KeyboardRepeater
{
	private static readonly float INITIAL_DOWN_TIME_SECONDS = 0.4f;

	private static readonly float REPEAT_TIME_SECONDS_START = 0.15f;

	private static readonly float REPEAT_TIME_SECONDS_END = 0.15f;

	private static readonly float SECONDS_TO_REACH_MAX_REPEAT_TIME = 1.5f;

	private static List<KeyCode> m_KeyCodesToCheck = new List<KeyCode>();

	private static Dictionary<KeyCode, float> m_NextRepeatTime = new Dictionary<KeyCode, float>();

	private static Dictionary<KeyCode, bool> m_JustRepeated = new Dictionary<KeyCode, bool>();

	private static Dictionary<KeyCode, float> m_TimeRepeating = new Dictionary<KeyCode, float>();

	public static void UpdateManual()
	{
		m_KeyCodesToCheck.Clear();
		m_KeyCodesToCheck.Add(Bindings.GetBinding(BindingType.PAN_CAMERA_UP).m_KeyCode);
		m_KeyCodesToCheck.Add(Bindings.GetBinding(BindingType.PAN_CAMERA_DOWN).m_KeyCode);
		m_KeyCodesToCheck.Add(Bindings.GetBinding(BindingType.PAN_CAMERA_LEFT).m_KeyCode);
		m_KeyCodesToCheck.Add(Bindings.GetBinding(BindingType.PAN_CAMERA_RIGHT).m_KeyCode);
		m_KeyCodesToCheck.Add(Bindings.GetBinding(BindingType.NUDGE_HYDRO_UP).m_KeyCode);
		m_KeyCodesToCheck.Add(Bindings.GetBinding(BindingType.NUDGE_HYDRO_DOWN).m_KeyCode);
		m_KeyCodesToCheck.Add(Bindings.GetBinding(BindingType.PAN_CAMERA_UP).m_AltKeyCode);
		m_KeyCodesToCheck.Add(Bindings.GetBinding(BindingType.PAN_CAMERA_DOWN).m_AltKeyCode);
		m_KeyCodesToCheck.Add(Bindings.GetBinding(BindingType.PAN_CAMERA_LEFT).m_AltKeyCode);
		m_KeyCodesToCheck.Add(Bindings.GetBinding(BindingType.PAN_CAMERA_RIGHT).m_AltKeyCode);
		m_KeyCodesToCheck.Add(Bindings.GetBinding(BindingType.NUDGE_HYDRO_UP).m_AltKeyCode);
		m_KeyCodesToCheck.Add(Bindings.GetBinding(BindingType.NUDGE_HYDRO_DOWN).m_AltKeyCode);
		foreach (KeyCode item in m_KeyCodesToCheck)
		{
			if (item == KeyCode.None)
			{
				continue;
			}
			if (Input.GetKeyDown(item))
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
			if (Input.GetKeyUp(item) || !Input.GetKey(item))
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

	public static bool JustRepeated(KeyCode keycode)
	{
		if (!m_JustRepeated.ContainsKey(keycode))
		{
			return false;
		}
		return m_JustRepeated[keycode];
	}
}
