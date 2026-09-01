using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class DialToAnimatorBridge : MonoBehaviour
{
	public enum AnimatorValueType
	{
		Float,
		Int
	}

	public enum IntRoundingMode
	{
		Nearest,
		Floor,
		Ceil
	}

	private DialInteractable sourceDial;

	private Animator targetAnimator;

	private string animatorParameter;

	private AnimatorValueType parameterType;

	private float valueScale;

	private float valueOffset;

	private bool useSmoothing;

	private float smoothTime;

	private IntRoundingMode intRounding;

	private bool clampIntOutput;

	private int minIntOutput;

	private int maxIntOutput;

	private bool syncOnEnable;

	private UnityEvent onIntActivated;

	private UnityEvent onIntDeactivated;

	private bool _subscribed;

	private int _paramHash;

	private bool _paramExists;

	private AnimatorControllerParameterType _animReportedType;

	private float _currentValue;

	private float _targetValue;

	private float _smoothVelocity;

	private int _lastAppliedInt;

	private bool _thresholdEventsArmed;

	private void Awake()
	{
		if (sourceDial == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			DialInteractable dialInteractable = default(DialInteractable);
			sourceDial = dialInteractable;
			if (sourceDial == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696290");
				sourceDial = dialInteractable;
			}
		}
		if (targetAnimator == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Animator animator = default(Animator);
			targetAnimator = animator;
			if (targetAnimator == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696290");
				Animator animator2 = default(Animator);
				targetAnimator = animator2;
			}
		}
		ValidateAnimatorParameter();
	}

	private void OnEnable()
	{
		//IL_00c2: Expected F4, but got I4
		//IL_0245: Invalid comparison between F4 and I4
		//IL_031a: Invalid comparison between F4 and I4
		//IL_0278: Invalid comparison between F4 and I4
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Expected O, but got Unknown
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Expected O, but got Unknown
		//IL_0135: Expected F4, but got I4
		//IL_022b: Expected F4, but got I4
		//IL_0157: Expected F4, but got I4
		//IL_021d: Expected F4, but got I4
		_thresholdEventsArmed = false;
		if (!_subscribed && sourceDial != null)
		{
			DialInteractable dialInteractable = sourceDial;
			UnityAction<float> call = HandleDialValueChanged;
			dialInteractable.OnValueChanged.AddListener(call);
			_subscribed = true;
		}
		float num;
		if (sourceDial != null)
		{
			DialInteractable dialInteractable2 = sourceDial;
			num = dialInteractable2.accumulatedValue;
		}
		else
		{
			num = 0f;
		}
		bool flag = !useSmoothing;
		float num2 = num * valueScale;
		float valueAsFloat = (_targetValue = num2 + valueOffset);
		float num3;
		float num5;
		if (!flag)
		{
			if (parameterType == AnimatorValueType.Float)
			{
				if (targetAnimator != null)
				{
					bool flag2 = targetAnimator != null;
					num3 = 0f;
					if (flag2)
					{
						bool flag3 = (byte)(~(_paramExists ? 1u : 0u)) != 0;
						num3 = 0f;
						if (!flag3)
						{
							float num4 = targetAnimator.GetFloat(_paramHash);
							num3 = num4;
						}
					}
				}
				else
				{
					num3 = _targetValue;
				}
			}
			else if (targetAnimator != null)
			{
				if (targetAnimator != null && ~(_paramExists ? 1u : 0u) == 0)
				{
					int integer = targetAnimator.GetInteger(_paramHash);
					num3 = integer;
				}
				else
				{
					num3 = 0f;
				}
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EEB0");
				float num6 = default(float);
				if (!(_targetValue < 0f))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,qword ptr [182206D18h]\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803E87C4h\"");
					if (_targetValue != 0f)
					{
						float x = _targetValue + 0.5f;
						num5 = MathF.Floor(x);
						goto IL_03a2;
					}
					object obj = num6 & 1;
					bool flag4 = obj == null;
					num3 = num6;
					if (!flag4)
					{
						num3 = num6 + 1f;
					}
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,qword ptr [182206D70h]\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803E87FFh\"");
					if (_targetValue != 0f)
					{
						float num7 = _targetValue - 0.5f;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F1C0");
						num5 = num7;
						goto IL_03a2;
					}
					object obj2 = num6 & 1;
					bool flag5 = obj2 == null;
					num3 = num6;
					if (!flag5)
					{
						num3 = num6 - 1f;
					}
				}
			}
			goto IL_0414;
		}
		goto IL_045d;
		IL_03a2:
		num3 = num5;
		goto IL_0414;
		IL_0414:
		_currentValue = num3;
		_smoothVelocity = 0f;
		valueAsFloat = num3;
		goto IL_045d;
		IL_045d:
		ApplyToAnimatorImmediate(valueAsFloat);
		_thresholdEventsArmed = true;
	}

	private void OnDisable()
	{
		_thresholdEventsArmed = false;
		if (_subscribed)
		{
			if (sourceDial != null)
			{
				DialInteractable dialInteractable = sourceDial;
				UnityAction<float> call = HandleDialValueChanged;
				dialInteractable.OnValueChanged.RemoveListener(call);
			}
			_subscribed = false;
		}
	}

	private unsafe void Update()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Expected Ref, but got Unknown
		if (useSmoothing)
		{
			bool flag = targetAnimator == null;
			if (!flag && _paramExists != flag)
			{
				float unscaledDeltaTime = Time.unscaledDeltaTime;
				float maxSpeed = default(float);
				float deltaTime = default(float);
				ApplyToAnimatorImmediate(_currentValue = Mathf.SmoothDamp(_currentValue, _targetValue, ref *(float*)(this + 136), smoothTime, maxSpeed, deltaTime));
			}
		}
	}

	private void OnValidate()
	{
		if (maxIntOutput < minIntOutput)
		{
			maxIntOutput = minIntOutput;
		}
		ValidateAnimatorParameter();
	}

	private void TrySubscribe()
	{
		if (!_subscribed && sourceDial != null)
		{
			DialInteractable dialInteractable = sourceDial;
			UnityAction<float> call = HandleDialValueChanged;
			dialInteractable.OnValueChanged.AddListener(call);
			_subscribed = true;
		}
	}

	private void TryUnsubscribe()
	{
		if (_subscribed)
		{
			if (sourceDial != null)
			{
				DialInteractable dialInteractable = sourceDial;
				UnityAction<float> call = HandleDialValueChanged;
				dialInteractable.OnValueChanged.RemoveListener(call);
			}
			_subscribed = false;
		}
	}

	private void HandleDialValueChanged(float dialValue)
	{
		float num = dialValue * valueScale;
		float valueAsFloat = (_targetValue = num + valueOffset);
		if (!useSmoothing)
		{
			ApplyToAnimatorImmediate(valueAsFloat);
		}
	}

	private float MapValue(float dialValue)
	{
		float num = dialValue * valueScale;
		return num + valueOffset;
	}

	private void ApplyToAnimatorImmediate(float valueAsFloat)
	{
		//IL_02ef: Expected I4, but got F8
		//IL_014c: Invalid comparison between F8 and I4
		//IL_0174: Invalid comparison between F8 and I4
		//IL_0185: Expected I4, but got F8
		bool flag = targetAnimator == null;
		if (flag || _paramExists == flag)
		{
			return;
		}
		double num2;
		if (parameterType != AnimatorValueType.Float)
		{
			IntRoundingMode intRoundingMode = intRounding;
			bool flag2 = intRounding == IntRoundingMode.Nearest;
			if (flag2)
			{
				goto IL_0121;
			}
			intRoundingMode--;
			if (!flag2)
			{
				if (intRoundingMode != IntRoundingMode.Floor)
				{
					goto IL_0121;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
				double num = Math.Ceiling(0.0);
				num2 = num;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
				double num3 = Math.Floor(0.0);
				num2 = num3;
			}
			goto IL_02d6;
		}
		targetAnimator.SetFloat(_paramHash, valueAsFloat);
		return;
		IL_0291:
		int num4;
		targetAnimator.SetInteger(_paramHash, num4);
		_lastAppliedInt = num4;
		return;
		IL_0302:
		if (num4 == _lastAppliedInt)
		{
			return;
		}
		if (_thresholdEventsArmed && parameterType == AnimatorValueType.Int)
		{
			UnityEvent unityEvent;
			if (_lastAppliedInt == 0)
			{
				if (num4 != 1)
				{
					goto IL_0291;
				}
				unityEvent = onIntActivated;
			}
			else
			{
				if (_lastAppliedInt != 1 || num4 != 0)
				{
					goto IL_0291;
				}
				unityEvent = onIntDeactivated;
			}
			unityEvent?.Invoke();
		}
		goto IL_0291;
		IL_0121:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
		double num5 = default(double);
		num2 = num5;
		goto IL_02d6;
		IL_02d6:
		bool flag3 = !clampIntOutput;
		num4 = (int)num2;
		if (!flag3)
		{
			int num6 = minIntOutput;
			if (!(num2 < (double)minIntOutput))
			{
				num6 = maxIntOutput;
				bool flag4 = !(num2 > (double)maxIntOutput);
				num4 = (int)num2;
				if (flag4)
				{
					goto IL_0302;
				}
			}
			num4 = num6;
		}
		goto IL_0302;
	}

	private void CheckThresholdEvents(int previousInt, int nextInt)
	{
		if (!_thresholdEventsArmed || parameterType != AnimatorValueType.Int)
		{
			return;
		}
		UnityEvent unityEvent;
		switch (previousInt)
		{
		case 0:
			if (nextInt == 1)
			{
				unityEvent = onIntActivated;
				break;
			}
			return;
		case 1:
			if (nextInt == 0)
			{
				unityEvent = onIntDeactivated;
				break;
			}
			return;
		default:
			return;
		}
		unityEvent?.Invoke();
	}

	private int RoundToInt(float value, IntRoundingMode mode)
	{
		//IL_00a5: Expected I4, but got F8
		//IL_007f: Expected I4, but got F8
		IntRoundingMode intRoundingMode = default(IntRoundingMode);
		bool flag = intRoundingMode == IntRoundingMode.Nearest;
		if (!flag)
		{
			intRoundingMode--;
			if (flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
				double num = Math.Floor(0.0);
				return (int)num;
			}
			if (intRoundingMode == IntRoundingMode.Floor)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
				double num2 = Math.Ceiling(0.0);
				return (int)num2;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
		int result = default(int);
		return result;
	}

	private void ValidateAnimatorParameter()
	{
		_paramHash = 0;
		_paramExists = false;
		_animReportedType = AnimatorControllerParameterType.Float;
		if (!(targetAnimator != null) || string.IsNullOrEmpty(animatorParameter))
		{
			return;
		}
		int paramHash = Animator.StringToHash(animatorParameter);
		_paramHash = paramHash;
		AnimatorControllerParameter[] parameters = targetAnimator.parameters;
		for (int i = 0; i < parameters.Length; i++)
		{
			int nameHash = parameters[i].nameHash;
			if (nameHash == _paramHash)
			{
				_paramExists = true;
				AnimatorControllerParameterType type = parameters[i].type;
				_animReportedType = type;
				break;
			}
		}
	}

	public DialToAnimatorBridge()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39FA9]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		animatorParameter = "DialValue";
		valueScale = 1f;
		smoothTime = 0.08f;
		maxIntOutput = 10;
		syncOnEnable = true;
		base._002Ector();
	}
}
