using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSlots
{
	private static PointerEventData m_PointerEventData;

	private static List<RaycastResult> m_RaycastResults = new List<RaycastResult>();

	public static List<MenuSlot> m_Slots = new List<MenuSlot>();

	private static int m_SelectedSlotIndex;

	public static void Init()
	{
		m_Slots.Clear();
	}

	public static void Add(MenuSlot slot)
	{
		m_Slots.Add(slot);
		slot.SetPointerEnterCallback(OnPointerEnterSlot);
	}

	public static void ProcessInput()
	{
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			DoActionForSelectedSlot();
		}
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
		{
			ScrollUp();
			GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_START_DELAY;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
		{
			ScrollDown();
			GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_START_DELAY;
		}
		if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && Time.unscaledTime > GameUI.m_NextAutoScrollTime)
		{
			ScrollUp();
			GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_DELAY;
		}
		if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && Time.unscaledTime > GameUI.m_NextAutoScrollTime)
		{
			ScrollDown();
			GameUI.m_NextAutoScrollTime = Time.unscaledTime + GameUI.AUTOSCROLL_DELAY;
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.SOUTH) && GameUI.PointerOver(typeof(MenuSlot)))
		{
			DoActionForSelectedSlot();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_DOWN) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_DOWN))
		{
			ProcessDpadDown();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_UP) || GamepadRepeater.JustRepeated(GamepadButtonType.DPAD_UP))
		{
			ProcessDpadUp();
		}
	}

	public static void UpdateSelectedSlot()
	{
		for (int i = 0; i < m_Slots.Count; i++)
		{
			m_Slots[i].m_Background.color = ((m_SelectedSlotIndex == i) ? GameUI.m_Instance.m_MenuSlotHoverColor : GameUI.m_Instance.m_MenuSlotColor);
		}
	}

	public static MenuSlot GetSlotUnderPointer()
	{
		if (m_PointerEventData == null)
		{
			m_PointerEventData = new PointerEventData(EventSystem.current);
		}
		m_PointerEventData.position = GameInput.GetMousePosition();
		m_RaycastResults.Clear();
		GameUI.m_Instance.m_Raycaster.Raycast(m_PointerEventData, m_RaycastResults);
		foreach (RaycastResult raycastResult in m_RaycastResults)
		{
			MenuSlot componentInParent = raycastResult.gameObject.GetComponentInParent<MenuSlot>();
			if ((bool)componentInParent)
			{
				return componentInParent;
			}
		}
		return null;
	}

	public static void ResetSelectedSlot()
	{
		if (m_SelectedSlotIndex == -1)
		{
			m_SelectedSlotIndex = 0;
			ForceGamepadCursorToSelecctedSlot();
		}
	}

	public static void ForceClearSelection()
	{
		m_SelectedSlotIndex = -1;
		UpdateSelectedSlot();
	}

	public static void DoActionForSelectedSlot()
	{
		if (m_SelectedSlotIndex >= 0 && m_SelectedSlotIndex < m_Slots.Count)
		{
			m_Slots[m_SelectedSlotIndex].m_Button.onClick.Invoke();
		}
	}

	private static void OnPointerEnterSlot(MenuSlot slot)
	{
		int selectedSlotIndex = m_SelectedSlotIndex;
		m_SelectedSlotIndex = GetSlotIndex(slot);
		if (m_SelectedSlotIndex != selectedSlotIndex)
		{
			InterfaceAudio.Play("ui_menu_hover");
		}
	}

	private static int GetSlotIndex(MenuSlot slot)
	{
		if (!m_Slots.Contains(slot))
		{
			return 0;
		}
		return m_Slots.IndexOf(slot);
	}

	private static void ScrollDown()
	{
		m_SelectedSlotIndex++;
		if (m_SelectedSlotIndex >= m_Slots.Count)
		{
			m_SelectedSlotIndex = 0;
		}
		InterfaceAudio.Play("ui_menu_hover");
	}

	private static void ScrollUp()
	{
		m_SelectedSlotIndex--;
		if (m_SelectedSlotIndex < 0)
		{
			m_SelectedSlotIndex = m_Slots.Count - 1;
		}
		InterfaceAudio.Play("ui_menu_hover");
	}

	private static void ProcessDpadUp()
	{
		ScrollUp();
		ForceGamepadCursorToSelecctedSlot();
	}

	private static void ProcessDpadDown()
	{
		ScrollDown();
		ForceGamepadCursorToSelecctedSlot();
	}

	private static void ForceGamepadCursorToSelecctedSlot()
	{
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad && m_SelectedSlotIndex != -1)
		{
			GameInput.SetVirtualMousePosition(m_Slots[m_SelectedSlotIndex].m_Icon.transform.position);
		}
	}
}
