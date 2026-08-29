using System;
using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class CustomInput : BaseInput
{
	public enum JoystickUsedForInput
	{
		Both = 0,
		JustPrimary = 1
	}

	[NonSerialized]
	public JoystickUsedForInput horizontalJoystick;

	[NonSerialized]
	public JoystickUsedForInput verticalJoystick;

	protected override void Awake()
	{
		StandaloneInputModule component = GetComponent<StandaloneInputModule>();
		if ((bool)component)
		{
			component.inputOverride = this;
		}
		base.Awake();
	}

	public override float GetAxisRaw(string axisName)
	{
		if (!Controller.isActive())
		{
			return base.GetAxisRaw(axisName);
		}
		if (axisName == "Horizontal")
		{
			if (horizontalJoystick == JoystickUsedForInput.Both)
			{
				return Controller.getUIAxisForChangeValue();
			}
			return Controller.getAxis(ControllerAxisActionType.UIMoveAndRotate).x;
		}
		if (axisName == "Vertical")
		{
			if (verticalJoystick == JoystickUsedForInput.Both)
			{
				return Controller.getUIAxisForNavigation();
			}
			return Controller.getAxis(ControllerAxisActionType.UIMoveAndRotate).y;
		}
		return base.GetAxisRaw(axisName);
	}

	public override bool GetButtonDown(string buttonName)
	{
		if (!Controller.isActive())
		{
			return base.GetButtonDown(buttonName);
		}
		if (buttonName == "Submit")
		{
			return Controller.getUIConfirmDown();
		}
		if (buttonName == "Cancel")
		{
			return Controller.getButtonDown(ControllerButtonActionType.UIBack);
		}
		return base.GetButtonDown(buttonName);
	}
}
