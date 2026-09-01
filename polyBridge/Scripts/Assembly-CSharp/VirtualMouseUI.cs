using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;

public class VirtualMouseUI : MonoBehaviour
{
	[SerializeField]
	private RectTransform m_CanvasRectTransform;

	[SerializeField]
	private RectTransform m_MouseVisual;

	[SerializeField]
	private RectTransform m_MouseVisualNormal;

	[SerializeField]
	private RectTransform m_MouseVisualErase;

	[SerializeField]
	private RectTransform m_MouseVisualMove;

	private VirtualMouseInput m_VirtualMouseInput;

	private VirtualMouseCursorState m_CursorState;

	private static readonly float EDGE_BUFFER = 10f;

	private static bool m_SuppressMouseVisual;

	private void Awake()
	{
		m_VirtualMouseInput = GetComponent<VirtualMouseInput>();
		m_SuppressMouseVisual = true;
	}

	private void Start()
	{
		SetCursorNormal();
	}

	private void Update()
	{
		base.transform.localScale = Vector3.one * (1f / m_CanvasRectTransform.localScale.x);
		base.transform.SetAsLastSibling();
		m_MouseVisual.transform.localScale = m_CanvasRectTransform.localScale;
		m_VirtualMouseInput.cursorSpeed = GamepadManager.GetCursorSpeed();
		m_VirtualMouseInput.minCursorSpeed = GamepadManager.CURSOR_SPEED_MIN;
		m_VirtualMouseInput.cursorAcceleration = GamepadManager.GetCursorAcceleration();
		m_VirtualMouseInput.cursorAccelerationEnabled = Profiles.m_ActiveProfile.m_GamepadAcceleration;
		UpdateVisibility();
	}

	private void LateUpdate()
	{
		SyncVirtualMouseToInput();
	}

	public void SyncVirtualMouseToInput()
	{
		try
		{
			Vector2 value = m_VirtualMouseInput.virtualMouse.position.value;
			value.x = Mathf.Clamp(value.x, EDGE_BUFFER, (float)Screen.width - EDGE_BUFFER);
			value.y = Mathf.Clamp(value.y, EDGE_BUFFER, (float)Screen.height - EDGE_BUFFER);
			InputState.Change(m_VirtualMouseInput.virtualMouse.position, value);
			m_MouseVisual.anchoredPosition = value;
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Exception in SyncVirtualMouseToInput: " + ex.Message);
		}
	}

	public void SetCursorNormal()
	{
		if (m_CursorState != VirtualMouseCursorState.NORMAL)
		{
			m_MouseVisualNormal.gameObject.SetActive(value: true);
			m_MouseVisualErase.gameObject.SetActive(value: false);
			m_MouseVisualMove.gameObject.SetActive(value: false);
			m_CursorState = VirtualMouseCursorState.NORMAL;
		}
	}

	public void SetCursorErase()
	{
		if (m_CursorState != VirtualMouseCursorState.ERASE)
		{
			m_MouseVisualNormal.gameObject.SetActive(value: false);
			m_MouseVisualErase.gameObject.SetActive(value: true);
			m_MouseVisualMove.gameObject.SetActive(value: false);
			m_CursorState = VirtualMouseCursorState.ERASE;
		}
	}

	public void SetCursorMove()
	{
		if (m_CursorState != VirtualMouseCursorState.MOVE)
		{
			m_MouseVisualNormal.gameObject.SetActive(value: false);
			m_MouseVisualErase.gameObject.SetActive(value: false);
			m_MouseVisualMove.gameObject.SetActive(value: true);
			m_CursorState = VirtualMouseCursorState.MOVE;
		}
	}

	public bool CursorPeggedToLeftSideOfScreen()
	{
		return m_VirtualMouseInput.virtualMouse.position.value.x <= EDGE_BUFFER + 0.001f;
	}

	public bool CursorPeggedToRightSideOfScreen()
	{
		return m_VirtualMouseInput.virtualMouse.position.value.x >= (float)Screen.width - EDGE_BUFFER + -0.001f;
	}

	public bool CursorPeggedToTopOfScreen()
	{
		return m_VirtualMouseInput.virtualMouse.position.value.y >= (float)Screen.height - EDGE_BUFFER - 0.001f;
	}

	public bool CursorPeggedToBottomOfScreen()
	{
		return m_VirtualMouseInput.virtualMouse.position.value.y <= EDGE_BUFFER + 0.001f;
	}

	public void ResetMouseToCenter()
	{
		Vector2 virtualMousePosition = new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);
		SetVirtualMousePosition(virtualMousePosition);
	}

	public Vector2 GetVirtualMousePosition()
	{
		return m_MouseVisual.anchoredPosition;
	}

	public void SetVirtualMousePosition(Vector2 pos)
	{
		InputState.Change(m_VirtualMouseInput.virtualMouse.position, pos);
		m_MouseVisual.anchoredPosition = pos;
	}

	public void UpdateVisibility()
	{
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad && !GamepadVirtualKeyboard.m_Active)
		{
			Show();
		}
		else
		{
			Hide();
		}
	}

	public void SuppressMouseVisual(bool suppress)
	{
		m_SuppressMouseVisual = suppress;
		if (!m_SuppressMouseVisual)
		{
			Show();
		}
	}

	private void Show()
	{
		if (!m_MouseVisual.gameObject.activeInHierarchy)
		{
			m_MouseVisual.gameObject.SetActive(!m_SuppressMouseVisual && !GamepadVirtualKeyboard.m_Active);
		}
	}

	private void Hide()
	{
		if (m_MouseVisual.gameObject.activeInHierarchy)
		{
			m_MouseVisual.gameObject.SetActive(value: false);
		}
	}
}
