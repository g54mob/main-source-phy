using System;
using Cpp2ILInjected;
using UnityEngine;

public sealed class SwingLightFlickerRelay : MonoBehaviour
{
	private enum OnEnableAction
	{
		None,
		PowerOn,
		PowerOff,
		Toggle
	}

	private string controllerTag;

	private OnEnableAction onEnableAction;

	private SwingLightFlickerController _controller;

	private void OnEnable()
	{
		SwingLightFlickerController controller = FindController();
		_controller = controller;
	}

	private void Start()
	{
		//IL_0010: Expected O, but got I4
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		object obj = onEnableAction - 1;
		object obj2 = default(object);
		if (obj2 == null)
		{
			object obj3 = obj - 1;
			if (obj2 == null)
			{
				if ((nint)obj3 == 1 && Resolve())
				{
					SwingLightFlickerController controller = _controller;
					bool switchOn = !controller._switchOn;
					bool flag = ~(controller._switchOn ? 1u : 0u) != 0 && controller.restoreUsesSequence;
					bool flag2 = !flag;
					controller._switchOn = switchOn;
					bool playRestoreSequence = !flag2;
					controller.ApplyEffectivePower(playRestoreSequence);
				}
			}
			else if (Resolve())
			{
				SwingLightFlickerController controller2 = _controller;
				controller2._switchOn = false;
				controller2.ApplyEffectivePower(false);
			}
		}
		else if (Resolve())
		{
			SwingLightFlickerController controller3 = _controller;
			controller3._switchOn = true;
			controller3.ApplyEffectivePower(controller3.restoreUsesSequence);
		}
	}

	public void PowerOn()
	{
		if (Resolve())
		{
			SwingLightFlickerController controller = _controller;
			controller._switchOn = true;
			controller.ApplyEffectivePower(controller.restoreUsesSequence);
		}
	}

	public void PowerOff()
	{
		if (Resolve())
		{
			SwingLightFlickerController controller = _controller;
			controller._switchOn = false;
			controller.ApplyEffectivePower(false);
		}
	}

	public void Toggle()
	{
		if (Resolve())
		{
			SwingLightFlickerController controller = _controller;
			bool switchOn = !controller._switchOn;
			bool flag = ~(controller._switchOn ? 1u : 0u) != 0 && controller.restoreUsesSequence;
			bool flag2 = !flag;
			controller._switchOn = switchOn;
			bool playRestoreSequence = !flag2;
			controller.ApplyEffectivePower(playRestoreSequence);
		}
	}

	public void SetPower(bool powerOn)
	{
		if (Resolve())
		{
			SwingLightFlickerController controller = _controller;
			bool flag = powerOn && controller.restoreUsesSequence;
			bool flag2 = !flag;
			controller._switchOn = powerOn;
			bool playRestoreSequence = !flag2;
			controller.ApplyEffectivePower(playRestoreSequence);
		}
	}

	private SwingLightFlickerController FindController()
	{
		GameObject gameObject = GameObject.FindWithTag(controllerTag);
		if (gameObject != null)
		{
			if ((object)gameObject != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				UnityEngine.Object obj = default(UnityEngine.Object);
				if (obj == null)
				{
					string[] array = new string[5];
					if (array == null)
					{
						goto IL_0188;
					}
					array[0] = "[SwingLightFlickerRelay] GameObject \"";
					string text = gameObject.name;
					array[1] = text;
					array[2] = "\" has tag \"";
					array[3] = controllerTag;
					array[4] = "\" but no SwingLightFlickerController component was found on it.";
					string message = string.Concat(array);
					Debug.LogWarning(message, this);
				}
				return (SwingLightFlickerController)obj;
			}
			goto IL_0188;
		}
		string message2 = "[SwingLightFlickerRelay] No GameObject found with tag \"" + controllerTag + "\". Assign the tag to the GameObject that holds SwingLightFlickerController.";
		Debug.LogWarning(message2, this);
		return null;
		IL_0188:
		return (SwingLightFlickerController)(object)new NullReferenceException();
	}

	private bool Resolve()
	{
		if (_controller == null)
		{
			SwingLightFlickerController controller = FindController();
			_controller = controller;
			return _controller != null;
		}
		return true;
	}

	public SwingLightFlickerRelay()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AD59]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		controllerTag = "SwingLightController";
		base._002Ector();
	}
}
