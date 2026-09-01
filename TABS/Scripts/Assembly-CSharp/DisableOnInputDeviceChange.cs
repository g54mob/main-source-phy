using System.Collections.Generic;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;

public class DisableOnInputDeviceChange : MonoBehaviour
{
	[SerializeField]
	private List<GameObject> m_disableOnGamepads;

	[SerializeField]
	private List<GameObject> m_disableOnKeyboardMouse;

	[SerializeField]
	[Tooltip("Disables the items when using a gamepad on consoles, but NOT on desktop.")]
	private List<GameObject> m_disableOnConsoleGamepads;

	private InputService m_inputService;

	private bool m_isDesktop;

	private void Awake()
	{
		m_inputService = ServiceLocator.GetService<InputService>();
		m_isDesktop = GlobalSettingsHandler.CurrentPlatform == SettingsInstance.Platform.Desktop;
	}

	private void OnEnable()
	{
		m_inputService.InputChanged += OnInputChanged;
	}

	private void OnDisable()
	{
		if (m_inputService != null)
		{
			m_inputService.InputChanged -= OnInputChanged;
		}
	}

	private void Start()
	{
		OnInputChanged(PlayerActions.Instance.InputType);
	}

	private void OnInputChanged(InputType inputType)
	{
		bool active = true;
		bool active2 = true;
		bool active3 = m_isDesktop;
		switch (inputType)
		{
		case InputType.Controller:
			active = false;
			break;
		case InputType.Keyboard:
		case InputType.Any:
			active2 = false;
			active3 = false;
			break;
		}
		SetEnabled(m_disableOnGamepads, active);
		SetEnabled(m_disableOnKeyboardMouse, active2);
		SetEnabled(m_disableOnConsoleGamepads, active3);
	}

	public void AddToDisableOnGamepad(GameObject newGameObject)
	{
		m_disableOnGamepads.Add(newGameObject);
		OnInputChanged(PlayerActions.Instance.InputType);
	}

	public void AddToDisableOnKeyboardMouse(GameObject newGameObject)
	{
		m_disableOnKeyboardMouse.Add(newGameObject);
		OnInputChanged(PlayerActions.Instance.InputType);
	}

	public void RemoveFromDisableOnGamepad(GameObject newGameObject)
	{
		if (m_disableOnGamepads.Contains(newGameObject))
		{
			m_disableOnGamepads.Remove(newGameObject);
			OnInputChanged(PlayerActions.Instance.InputType);
		}
	}

	public void RemoveFromDisableOnKeyboardMouse(GameObject newGameObject)
	{
		if (m_disableOnKeyboardMouse.Contains(newGameObject))
		{
			m_disableOnKeyboardMouse.Remove(newGameObject);
			OnInputChanged(PlayerActions.Instance.InputType);
		}
	}

	private void SetEnabled(List<GameObject> objects, bool active)
	{
		if (objects == null || objects.Count <= 0)
		{
			return;
		}
		foreach (GameObject @object in objects)
		{
			if (@object != null)
			{
				@object.SetActive(active);
			}
			else
			{
				Debug.LogWarning(base.gameObject.name + " is trying to reference a null object", this);
			}
		}
	}
}
