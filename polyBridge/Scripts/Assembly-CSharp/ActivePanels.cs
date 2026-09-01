using System.Collections.Generic;
using UnityEngine;

public class ActivePanels
{
	public static List<GameObject> m_Panels = new List<GameObject>();

	public static void Add(GameObject panel)
	{
		if (!m_Panels.Contains(panel))
		{
			m_Panels.Add(panel);
		}
		if (GameStateManager.GetState() == GameState.BUILD && GameToolMode.GetMode() == GameToolModeType.ERASE && GameInput.IsDown(BindingType.ERASE))
		{
			GameToolMode.EraseModeActivate(on: false);
		}
		if (GameStateManager.GetState() == GameState.BUILD && GameToolMode.GetMode() == GameToolModeType.MOVE && (GameInput.IsDown(BindingType.MOVE) || GameInput.IsDown(BindingType.VERTICAL_CONSTRAINT_UNIVERSAL) || GameInput.IsDown(BindingType.HORIZONTAL_CONSTRAINT_UNIVERSAL) || GamepadManager.ButtonIsDown(GamepadButtonType.NORTH)))
		{
			GameToolMode.MoveModeActivate(on: false);
		}
	}

	public static void Remove(GameObject panel)
	{
		if (m_Panels.Contains(panel))
		{
			m_Panels.Remove(panel);
		}
	}

	public static void Insert(GameObject panel)
	{
		if (!m_Panels.Contains(panel))
		{
			m_Panels.Insert(0, panel);
		}
	}

	public static bool None()
	{
		return m_Panels.Count == 0;
	}

	public static bool IsTopPanel(GameObject panel)
	{
		if (m_Panels.Count != 0)
		{
			return m_Panels[m_Panels.Count - 1] == panel;
		}
		return false;
	}
}
