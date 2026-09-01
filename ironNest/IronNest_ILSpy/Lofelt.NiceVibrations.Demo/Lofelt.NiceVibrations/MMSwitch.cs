using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Lofelt.NiceVibrations;

public class MMSwitch : MMTouchButton
{
	public enum SwitchStates
	{
		Off,
		On
	}

	public Image SwitchKnob;

	private SwitchStates _003CCurrentSwitchState_003Ek__BackingField;

	public SwitchStates InitialState;

	public Transform OffPosition;

	public Transform OnPosition;

	public AnimationCurve KnobMovementCurve;

	public float KnobMovementDuration;

	public UnityEvent SwitchOn;

	public UnityEvent SwitchOff;

	protected float _knobMovementStartedAt;

	public SwitchStates CurrentSwitchState
	{
		get
		{
			return _003CCurrentSwitchState_003Ek__BackingField;
		}
		set
		{
			_003CCurrentSwitchState_003Ek__BackingField = value;
		}
	}

	protected override void Initialization()
	{
		//IL_000b: Expected I, but got O
		//IL_0027: Expected O, but got I
		//IL_0037: Expected O, but got I
		base.Initialization();
		nint num = (nint)this;
		_003CCurrentSwitchState_003Ek__BackingField = InitialState;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rdx_v2 (Il2CppClass<Lofelt.NiceVibrations.MMSwitch>)+2F8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rdx_v2 (Il2CppClass<Lofelt.NiceVibrations.MMSwitch>)+300]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v8 @ rax_v3 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public unsafe virtual void InitializeState()
	{
		//IL_009f: Expected O, but got Ref
		Component component;
		Transform transform2;
		if (_003CCurrentSwitchState_003Ek__BackingField != SwitchStates.Off)
		{
			if (_animator != null)
			{
				_animator.Play("RollRight");
			}
			Transform transform = SwitchKnob.transform;
			component = OnPosition;
			transform2 = transform;
		}
		else
		{
			if (_animator != null)
			{
				_animator.Play("RollLeft");
			}
			Transform transform3 = SwitchKnob.transform;
			component = OffPosition;
			transform2 = transform3;
		}
		Transform transform4 = component.transform;
		Vector3 position = transform4.position;
		object obj = default(object);
		transform2.position = (Vector3)(&obj);
	}

	protected unsafe override void Update()
	{
		//IL_01c0: Invalid comparison between I4 and F4
		//IL_0123: Invalid comparison between I4 and F4
		//IL_0206: Expected O, but got Ref
		base.Update();
		float time = Time.time;
		float num = time - _knobMovementStartedAt;
		if (!(KnobMovementDuration > num))
		{
			return;
		}
		float time2 = Time.time;
		float num2 = time2 - _knobMovementStartedAt;
		float num3 = base.Remap(num2, 0f, KnobMovementDuration, 0f, 0f);
		float num4 = KnobMovementCurve.Evaluate(num2);
		float x;
		Transform transform4;
		if (_003CCurrentSwitchState_003Ek__BackingField != SwitchStates.Off)
		{
			Transform transform = SwitchKnob.transform;
			Transform transform2 = OffPosition.transform;
			Vector3 position = transform2.position;
			Transform transform3 = OnPosition.transform;
			Vector3 position2 = transform3.position;
			if (0f > num4 || num4 > 1f)
			{
			}
			x = position.x;
			transform4 = transform;
		}
		else
		{
			Transform transform5 = SwitchKnob.transform;
			Transform transform6 = OnPosition.transform;
			Vector3 position3 = transform6.position;
			Transform transform7 = OffPosition.transform;
			Vector3 position4 = transform7.position;
			if (0f > num4 || num4 > 1f)
			{
			}
			x = position4.x;
			transform4 = transform5;
		}
		transform4.position = (Vector3)(&x);
	}

	public virtual void SwitchState()
	{
		float time = Time.time;
		bool flag = _003CCurrentSwitchState_003Ek__BackingField == SwitchStates.Off;
		_knobMovementStartedAt = time;
		if (!flag)
		{
			_003CCurrentSwitchState_003Ek__BackingField = SwitchStates.Off;
			if (_animator != null && (object)_animator != null)
			{
				_animator.SetTrigger("Left");
			}
			if (SwitchOff != null)
			{
				SwitchOff.Invoke();
			}
		}
		else
		{
			_003CCurrentSwitchState_003Ek__BackingField = SwitchStates.On;
			if (_animator != null && (object)_animator != null)
			{
				_animator.SetTrigger("Right");
			}
			if (SwitchOn != null)
			{
				SwitchOn.Invoke();
			}
		}
	}

	public MMSwitch()
	{
		//IL_0099: Expected O, but got I
		KnobMovementCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
		KnobMovementDuration = 0.2f;
		_knobMovementStartedAt = -50f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3F557]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		PressedColor = (Color)0;
		LerpColor = true;
		LerpColorDuration = 0.2f;
		PressedOpacity = 1f;
		IdleOpacity = 1f;
		DisabledOpacity = 1f;
		IdleAnimationParameterName = "Idle";
		DisabledAnimationParameterName = "Disabled";
		PressedAnimationParameterName = "Pressed";
		_lastStateChangeAt = -50f;
		((MonoBehaviour)this)._002Ector();
	}
}
