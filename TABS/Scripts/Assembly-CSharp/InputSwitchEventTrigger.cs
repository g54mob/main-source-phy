using System;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;
using UnityEngine.Events;

public class InputSwitchEventTrigger : MonoBehaviour
{
	[SerializeField]
	private UnityEvent onControllerSwitch;

	[SerializeField]
	private UnityEvent onKeyboardSwitch;

	private InputService inputService;

	private void Awake()
	{
		inputService = ServiceLocator.GetService<InputService>();
	}

	private void Start()
	{
		OnInputChange(PlayerActions.Instance.InputType);
	}

	private void OnEnable()
	{
		inputService.InputChanged += OnInputChange;
	}

	private void OnDisable()
	{
		inputService.InputChanged -= OnInputChange;
	}

	private void OnInputChange(InputType inputType)
	{
		switch (inputType)
		{
		case InputType.Controller:
			onControllerSwitch.Invoke();
			break;
		case InputType.Keyboard:
		case InputType.Any:
			onKeyboardSwitch.Invoke();
			break;
		default:
			throw new ArgumentOutOfRangeException("inputType", inputType, null);
		}
	}
}
