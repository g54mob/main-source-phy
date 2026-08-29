using System;
using System.Collections.Generic;
using UnityEngine;

public class InputDispatcher
{
	private class InputData
	{
		public KeyCode keyCode;

		public ControllerButtonActionType controllerButton;

		public KeyBindingAction keyBinding;

		public Enum downContext;

		public InputState state;

		public InputData(KeyCode keyCode)
		{
			this.keyCode = keyCode;
		}

		public InputData(ControllerButtonActionType controllerButton)
		{
			this.controllerButton = controllerButton;
		}

		public InputData(KeyBindingAction keyBinding)
		{
			this.keyBinding = keyBinding;
		}
	}

	public enum AlwaysTrueContext
	{
		True = 0
	}

	public enum InputState
	{
		None = 0,
		Down = 1,
		Hold = 2,
		Up = 3,
		InvalidContext = 4
	}

	private Func<Enum> getContext;

	private MenuOptions menuOptions;

	private List<InputData> allInputs = new List<InputData>();

	public InputDispatcher(MenuOptions menuOptions, Func<Enum> getContext)
	{
		this.menuOptions = menuOptions;
		this.getContext = getContext;
		foreach (object value in Enum.GetValues(typeof(ControllerButtonActionType)))
		{
			allInputs.Add(new InputData((ControllerButtonActionType)value));
		}
		foreach (object value2 in Enum.GetValues(typeof(KeyBindingAction)))
		{
			allInputs.Add(new InputData((KeyBindingAction)value2));
		}
		List<KeyCode> list = new List<KeyCode>
		{
			KeyCode.Escape,
			KeyCode.LeftShift,
			KeyCode.LeftAlt,
			KeyCode.BackQuote,
			KeyCode.Space,
			KeyCode.R,
			KeyCode.W,
			KeyCode.A,
			KeyCode.S,
			KeyCode.D,
			KeyCode.Tab,
			KeyCode.L,
			KeyCode.Q,
			KeyCode.I
		};
		for (int i = 0; i < 9; i++)
		{
			KeyCode item = (KeyCode)(49 + i);
			list.Add(item);
		}
		foreach (KeyCode item2 in list)
		{
			allInputs.Add(new InputData(item2));
		}
	}

	public void update()
	{
		foreach (InputData allInput in allInputs)
		{
			if (isActive(allInput))
			{
				if (allInput.state == InputState.None)
				{
					allInput.state = InputState.Down;
					allInput.downContext = getContext();
				}
				else
				{
					allInput.state = ((!object.Equals(allInput.downContext, getContext())) ? InputState.InvalidContext : InputState.Hold);
				}
			}
			else
			{
				InputState state = allInput.state;
				if (state == InputState.Hold || state == InputState.Down)
				{
					allInput.state = ((!object.Equals(allInput.downContext, getContext())) ? InputState.InvalidContext : InputState.Up);
				}
				else
				{
					allInput.state = InputState.None;
				}
			}
		}
	}

	public bool getDown(KeyCode keyCode)
	{
		return allInputs.Find((InputData x) => x.keyCode == keyCode).state == InputState.Down;
	}

	public bool getDown(params ControllerButtonActionType[] controllerButtons)
	{
		bool result = false;
		foreach (ControllerButtonActionType controllerButton in controllerButtons)
		{
			if (allInputs.Find((InputData x) => x.controllerButton == controllerButton).state == InputState.Down)
			{
				result = true;
			}
		}
		return result;
	}

	public bool getDown(KeyBindingAction keyBinding)
	{
		return allInputs.Find((InputData x) => x.keyBinding == keyBinding).state == InputState.Down;
	}

	public bool get(KeyCode keyCode)
	{
		return allInputs.Find((InputData x) => x.keyCode == keyCode).state == InputState.Hold;
	}

	public bool get(params ControllerButtonActionType[] controllerButtons)
	{
		bool result = false;
		foreach (ControllerButtonActionType controllerButton in controllerButtons)
		{
			if (allInputs.Find((InputData x) => x.controllerButton == controllerButton).state == InputState.Hold)
			{
				result = true;
			}
		}
		return result;
	}

	public bool get(KeyBindingAction keyBinding)
	{
		return allInputs.Find((InputData x) => x.keyBinding == keyBinding).state == InputState.Hold;
	}

	public bool getUp(KeyCode keyCode)
	{
		return allInputs.Find((InputData x) => x.keyCode == keyCode).state == InputState.Up;
	}

	public bool getUp(params ControllerButtonActionType[] controllerButtons)
	{
		bool result = false;
		foreach (ControllerButtonActionType controllerButton in controllerButtons)
		{
			if (allInputs.Find((InputData x) => x.controllerButton == controllerButton).state == InputState.Up)
			{
				result = true;
			}
		}
		return result;
	}

	public bool getUp(KeyBindingAction keyBinding)
	{
		return allInputs.Find((InputData x) => x.keyBinding == keyBinding).state == InputState.Up;
	}

	private bool isActive(InputData inputData)
	{
		if (inputData.keyCode != KeyCode.None)
		{
			if (!Input.GetKey(inputData.keyCode) && !Input.GetKeyDown(inputData.keyCode))
			{
				return Input.GetKeyUp(inputData.keyCode);
			}
			return true;
		}
		if (inputData.controllerButton != ControllerButtonActionType.None)
		{
			return Controller.getButton(inputData.controllerButton);
		}
		if (inputData.keyBinding != KeyBindingAction.None)
		{
			if (!menuOptions.getActionKey(inputData.keyBinding) && !menuOptions.getActionKeyDown(inputData.keyBinding))
			{
				return menuOptions.getActionKeyUp(inputData.keyBinding);
			}
			return true;
		}
		return false;
	}
}
