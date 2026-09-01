using System.Collections.Generic;
using UnityEngine;

public class UIScreenInputBlocker
{
	private static List<ICampaignMenu> m_MenuesOpen = new List<ICampaignMenu>();

	public static bool BlockInput { get; private set; }

	public static bool BlockCameraMovement { get; private set; }

	public static bool IsAnimatedMenuChangingState { get; private set; }

	public static void DoBlockInput(bool open)
	{
		BlockInput = (BlockCameraMovement = open);
		if (!BlockInput)
		{
			m_MenuesOpen = new List<ICampaignMenu>();
		}
	}

	public static void ScreenOpen(ICampaignMenu menu)
	{
		if (m_MenuesOpen.Contains(menu))
		{
			Debug.LogError("Open same menu several times? " + menu.ToString());
			return;
		}
		m_MenuesOpen.Add(menu);
		CheckBlockInput();
	}

	public static void ScreenClose(ICampaignMenu menu)
	{
		if (m_MenuesOpen.Contains(menu))
		{
			m_MenuesOpen.Remove(menu);
		}
		CheckBlockInput();
	}

	public static void SetBlockCameraMovement(bool blockCameraMovement)
	{
		BlockCameraMovement = blockCameraMovement;
	}

	private static void CheckBlockInput()
	{
		BlockInput = m_MenuesOpen.Count > 0;
	}

	public static void AnimatedMenuTransitionStart()
	{
		IsAnimatedMenuChangingState = true;
	}

	public static void AnimatedMenuTransitionEnd()
	{
		IsAnimatedMenuChangingState = false;
	}
}
