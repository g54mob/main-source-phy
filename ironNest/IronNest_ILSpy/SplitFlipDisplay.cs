using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public sealed class SplitFlipDisplay : MonoBehaviour, IFloatValueProvider, ISplitFlipDisplay
{
	public enum FlipDirection
	{
		Up,
		Down
	}

	public enum DirectionMode
	{
		AutoShortest,
		ForceUp,
		ForceDown
	}

	public enum DesiredChangeDetection
	{
		EveryFrame,
		PollInterval
	}

	public enum AdaptiveSpeedMapping
	{
		Linear,
		EaseIn,
		EaseOut,
		EaseInOut
	}

	private List<TMP_Text> oldTexts;

	private List<TMP_Text> newTexts;

	private Animator animator;

	private string flipUpTrigger;

	private string flipDownTrigger;

	private string initialValue;

	private string desiredValue;

	private string orderedSymbols;

	private DirectionMode directionMode;

	private bool preferDownOnTie;

	private bool autoApplyDesiredValue;

	private bool applyDesiredOnEnable;

	private DesiredChangeDetection desiredChangeDetection;

	private float pollIntervalSeconds;

	private bool adaptiveFlipSpeed;

	private float baselineAnimatorSpeedOverride;

	private float adaptiveMinSpeedMultiplier;

	private float adaptiveMaxSpeedMultiplier;

	private int adaptiveMinDistanceSteps;

	private int adaptiveMaxDistanceSteps;

	private AdaptiveSpeedMapping adaptiveSpeedMapping;

	private bool clearNewTextsWhenIdle;

	private bool exposeReadOnlyDebugProperties;

	private UnityEvent onFlip;

	private string _currentCommittedValue;

	private string _pendingDesiredValue;

	private bool _isFlipping;

	private char _stagedNextChar;

	private string _stagedNextValueString;

	private string _lastObservedDesiredValue;

	private float _pollTimer;

	private float _baselineAnimatorSpeed;

	private float _003CNormalizedSpeed_003Ek__BackingField;

	public string CurrentCommittedValue
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A09D]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			bool flag = _currentCommittedValue == null;
			string result = "";
			if (!flag)
			{
				result = _currentCommittedValue;
			}
			return result;
		}
	}

	public string PendingDesiredValue
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A09E]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			bool flag = _pendingDesiredValue == null;
			string result = "";
			if (!flag)
			{
				result = _pendingDesiredValue;
			}
			return result;
		}
	}

	public string OrderedSymbols
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A09F]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			bool flag = orderedSymbols == null;
			string result = "";
			if (!flag)
			{
				result = orderedSymbols;
			}
			return result;
		}
	}

	public bool IsFlipping => _isFlipping;

	public char CurrentCommittedChar
	{
		get
		{
			//IL_0069: Expected I4, but got O
			if (!string.IsNullOrEmpty(_currentCommittedValue))
			{
				if (_currentCommittedValue != null)
				{
					return _currentCommittedValue.get_Chars(0);
				}
				NullReferenceException ex = new NullReferenceException();
				return (char)(int)ex;
			}
			return '\0';
		}
	}

	public char PendingDesiredChar
	{
		get
		{
			//IL_0069: Expected I4, but got O
			if (!string.IsNullOrEmpty(_pendingDesiredValue))
			{
				if (_pendingDesiredValue != null)
				{
					return _pendingDesiredValue.get_Chars(0);
				}
				NullReferenceException ex = new NullReferenceException();
				return (char)(int)ex;
			}
			return '\0';
		}
	}

	public float NormalizedSpeed
	{
		get
		{
			return _003CNormalizedSpeed_003Ek__BackingField;
		}
		private set
		{
			_003CNormalizedSpeed_003Ek__BackingField = value;
		}
	}

	float IFloatValueProvider.GetFloatValue()
	{
		return _003CNormalizedSpeed_003Ek__BackingField;
	}

	private void Awake()
	{
		//IL_007f: Invalid comparison between F4 and I4
		bool flag = initialValue == null;
		string currentCommittedValue = "";
		if (!flag)
		{
			currentCommittedValue = initialValue;
		}
		_currentCommittedValue = currentCommittedValue;
		bool flag2 = desiredValue == null;
		string pendingDesiredValue = "";
		if (!flag2)
		{
			pendingDesiredValue = desiredValue;
		}
		_pendingDesiredValue = pendingDesiredValue;
		_isFlipping = false;
		_stagedNextChar = '\0';
		_stagedNextValueString = "";
		bool flag3 = desiredValue == null;
		string lastObservedDesiredValue = "";
		if (!flag3)
		{
			lastObservedDesiredValue = desiredValue;
		}
		_lastObservedDesiredValue = lastObservedDesiredValue;
		_pollTimer = 0f;
		if (animator != null)
		{
			float speed = animator.speed;
			_baselineAnimatorSpeed = speed;
			if (baselineAnimatorSpeedOverride > 0f)
			{
				_baselineAnimatorSpeed = baselineAnimatorSpeedOverride;
			}
			animator.speed = _baselineAnimatorSpeed;
		}
		CommitOld(_currentCommittedValue);
		if (clearNewTextsWhenIdle)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0AD]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			StageNew("");
		}
	}

	private void OnEnable()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0A1]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (!autoApplyDesiredValue)
		{
			return;
		}
		bool flag = desiredValue == null;
		string lastObservedDesiredValue = "";
		if (!flag)
		{
			lastObservedDesiredValue = desiredValue;
		}
		_lastObservedDesiredValue = lastObservedDesiredValue;
		if (applyDesiredOnEnable)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0A3]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			bool flag2 = desiredValue == null;
			string pendingDesiredValue = "";
			if (!flag2)
			{
				pendingDesiredValue = desiredValue;
			}
			_pendingDesiredValue = pendingDesiredValue;
			TryStartNextFlipStep();
		}
	}

	private void Update()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0A2]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (!autoApplyDesiredValue)
		{
			return;
		}
		if (desiredChangeDetection != DesiredChangeDetection.EveryFrame)
		{
			bool flag = !(0.02f < pollIntervalSeconds);
			float num = 0.02f;
			if (!flag)
			{
				num = pollIntervalSeconds;
			}
			float unscaledDeltaTime = Time.unscaledDeltaTime;
			if (num > (_pollTimer = unscaledDeltaTime + _pollTimer))
			{
				return;
			}
			_pollTimer = 0f;
		}
		bool flag2 = desiredValue == null;
		string text = "";
		if (!flag2)
		{
			text = desiredValue;
		}
		if (text != _lastObservedDesiredValue)
		{
			_lastObservedDesiredValue = text;
			ApplyDesiredValueNow();
		}
	}

	public void ApplyDesiredValueNow()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0A3]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		bool flag = desiredValue == null;
		string pendingDesiredValue = "";
		if (!flag)
		{
			pendingDesiredValue = desiredValue;
		}
		_pendingDesiredValue = pendingDesiredValue;
		TryStartNextFlipStep();
	}

	public void SetDesiredValueAndApply(string value)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0A4]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		bool flag = value == null;
		string text = "";
		if (!flag)
		{
			text = value;
		}
		desiredValue = text;
		_lastObservedDesiredValue = desiredValue;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0A3]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		bool flag2 = desiredValue == null;
		string pendingDesiredValue = "";
		if (!flag2)
		{
			pendingDesiredValue = desiredValue;
		}
		_pendingDesiredValue = pendingDesiredValue;
		TryStartNextFlipStep();
	}

	public void SetDesiredCharAndApply(char c)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0A5]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		char c2 = default(char);
		string text;
		string text3;
		string text4;
		if (c2 != 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D4CC80");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0A4]");
			bool flag = (nint)0 != 0;
			string text2 = default(string);
			text = text2;
			if (flag)
			{
				goto IL_0154;
			}
			text = text2;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0A4]");
			bool flag2 = (nint)0 != 0;
			text = "";
			text3 = "";
			text4 = "";
			if (flag2)
			{
				goto IL_016a;
			}
		}
		_ = 1;
		goto IL_0154;
		IL_016a:
		if (text3 != null)
		{
			text4 = text3;
		}
		desiredValue = text4;
		_lastObservedDesiredValue = desiredValue;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0A3]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		bool flag3 = desiredValue == null;
		string pendingDesiredValue = "";
		if (!flag3)
		{
			pendingDesiredValue = desiredValue;
		}
		_pendingDesiredValue = pendingDesiredValue;
		TryStartNextFlipStep();
		return;
		IL_0154:
		text3 = text;
		text4 = "";
		goto IL_016a;
	}

	public void OnFlipAnimationFinished()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0A6]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (_stagedNextChar != 0)
		{
			object obj = this + 170;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D4CC80");
			string currentCommittedValue = default(string);
			_currentCommittedValue = currentCommittedValue;
		}
		CommitOld(_currentCommittedValue);
		if (clearNewTextsWhenIdle)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0AD]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			StageNew("");
		}
		_isFlipping = false;
		_stagedNextChar = '\0';
		_stagedNextValueString = "";
		_003CNormalizedSpeed_003Ek__BackingField = 0f;
		TryStartNextFlipStep();
	}

	public void OnFlip()
	{
		if (onFlip != null)
		{
			onFlip.Invoke();
		}
	}

	public void SetValueInstant(string value)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0A7]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		bool flag = value != null;
		string currentCommittedValue = value;
		if (!flag)
		{
			currentCommittedValue = "";
		}
		_currentCommittedValue = currentCommittedValue;
		CommitOld(_currentCommittedValue);
		if (clearNewTextsWhenIdle)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0AD]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			StageNew("");
		}
		_isFlipping = false;
		_stagedNextChar = '\0';
		_stagedNextValueString = "";
		ApplyAnimatorSpeed(_baselineAnimatorSpeed);
	}

	private void SnapToDesired()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0A8]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		bool flag = _pendingDesiredValue == null;
		string text = "";
		if (!flag)
		{
			text = _pendingDesiredValue;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0A7]");
		bool flag2 = (nint)0 != 0;
		string text2 = "";
		if (!flag2)
		{
			_ = 1;
			text2 = "";
		}
		if (text == null)
		{
			text = text2;
		}
		_currentCommittedValue = text;
		CommitOld(_currentCommittedValue);
		if (clearNewTextsWhenIdle)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0AD]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			StageNew("");
		}
		_isFlipping = false;
		_stagedNextChar = '\0';
		_stagedNextValueString = "";
		ApplyAnimatorSpeed(_baselineAnimatorSpeed);
	}

	private void TryStartNextFlipStep()
	{
		//IL_01ed: Expected O, but got I4
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Expected O, but got Unknown
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Expected I4, but got Unknown
		//IL_021e: Expected O, but got I4
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Expected O, but got Unknown
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Expected I4, but got Unknown
		//IL_030e: Expected O, but got I4
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Expected I4, but got Unknown
		//IL_02e5: Expected O, but got I4
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Expected O, but got Unknown
		//IL_039d: Expected O, but got I4
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Expected O, but got Unknown
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c6: Expected I4, but got Unknown
		//IL_0379: Expected O, but got I4
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0A9]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (_isFlipping)
		{
			return;
		}
		bool flag = _pendingDesiredValue == null;
		string text = "";
		if (!flag)
		{
			text = _pendingDesiredValue;
		}
		if (text != _currentCommittedValue)
		{
			if (!string.IsNullOrEmpty(text))
			{
				char c = text.get_Chars(0);
				if (c != 0 && !string.IsNullOrEmpty(orderedSymbols))
				{
					int num = orderedSymbols.IndexOf(c);
					if (num >= 0)
					{
						char value = ((!string.IsNullOrEmpty(_currentCommittedValue)) ? _currentCommittedValue.get_Chars(0) : '\0');
						int num2 = orderedSymbols.IndexOf(value);
						bool flag2 = num2 >= 0;
						int num3 = num2;
						if (!flag2)
						{
							char c2 = orderedSymbols.get_Chars(0);
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D4CC80");
							string currentCommittedValue = default(string);
							_currentCommittedValue = currentCommittedValue;
							CommitOld(_currentCommittedValue);
							num3 = 0;
						}
						if (num3 != num)
						{
							string text2 = orderedSymbols;
							object obj = text2._stringLength - num3;
							object obj2 = obj + num;
							int num4 = obj2 % text2._stringLength;
							object obj3 = text2._stringLength - num;
							object obj4 = obj3 + num3;
							int num5 = obj4 % text2._stringLength;
							object obj6;
							int num6;
							if (directionMode != DirectionMode.ForceUp && (directionMode == DirectionMode.ForceDown || (num4 >= num5 && (num4 > num5 || preferDownOnTie))))
							{
								object obj5 = text2._stringLength - 1;
								obj6 = obj5 + num3;
								num6 = 1;
							}
							else
							{
								obj6 = num3 + 1;
								num6 = 0;
							}
							int index = obj6 % text2._stringLength;
							char stagedNextChar = orderedSymbols.get_Chars(index);
							string text3 = orderedSymbols;
							bool flag3 = text3._stringLength <= 0;
							int remainingSteps = 0;
							if (!flag3)
							{
								object obj8;
								if (num6 != 0)
								{
									object obj7 = text3._stringLength - num;
									obj8 = obj7 + num3;
								}
								else
								{
									object obj9 = text3._stringLength - num3;
									obj8 = obj9 + num;
								}
								int num7 = obj8 % text3._stringLength;
								remainingSteps = num7;
							}
							UpdateAnimatorSpeedForRemainingSteps(remainingSteps);
							_stagedNextChar = stagedNextChar;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D4CC80");
							string stagedNextValueString = default(string);
							_stagedNextValueString = stagedNextValueString;
							StageNew(_stagedNextValueString);
							if (this.animator != null)
							{
								Animator animator;
								string trigger;
								if (num6 != 0)
								{
									if (string.IsNullOrEmpty(flipDownTrigger))
									{
										goto IL_048a;
									}
									animator = this.animator;
									trigger = flipDownTrigger;
								}
								else
								{
									if (string.IsNullOrEmpty(flipUpTrigger))
									{
										goto IL_048a;
									}
									animator = this.animator;
									trigger = flipUpTrigger;
								}
								animator.SetTrigger(trigger);
							}
							goto IL_048a;
						}
						_currentCommittedValue = text;
						CommitOld(_currentCommittedValue);
						if (clearNewTextsWhenIdle)
						{
							ClearNew();
						}
						ApplyAnimatorSpeed(_baselineAnimatorSpeed);
						return;
					}
				}
			}
			SnapToDesired();
		}
		else
		{
			ApplyAnimatorSpeed(_baselineAnimatorSpeed);
		}
		return;
		IL_048a:
		_isFlipping = true;
	}

	private void Trigger(FlipDirection direction)
	{
		if (!(this.animator != null))
		{
			return;
		}
		Animator animator;
		string trigger;
		if (direction != FlipDirection.Up)
		{
			if (string.IsNullOrEmpty(flipDownTrigger))
			{
				return;
			}
			animator = this.animator;
			trigger = flipDownTrigger;
		}
		else
		{
			if (string.IsNullOrEmpty(flipUpTrigger))
			{
				return;
			}
			animator = this.animator;
			trigger = flipUpTrigger;
		}
		animator.SetTrigger(trigger);
	}

	private void CommitOld(string value)
	{
		//IL_0018: Expected O, but got I4
		//IL_0021: Expected O, but got I4
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_0064: Expected I, but got O
		if (oldTexts == null)
		{
			return;
		}
		List<TMP_Text> list = oldTexts;
		object obj = 0;
		object obj2 = 0;
		UnityEngine.Object obj3 = default(UnityEngine.Object);
		while ((nint)obj2 < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj3 != null)
			{
				nint num = (nint)obj3;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v238 @ r8_v7 (Il2CppClass<UnityEngine.Object>)+558] (should have been resolved before IL gen)");
			}
			list = oldTexts;
			obj++;
			obj2 = obj;
		}
	}

	private void StageNew(string value)
	{
		//IL_0018: Expected O, but got I4
		//IL_0021: Expected O, but got I4
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_0064: Expected I, but got O
		if (newTexts == null)
		{
			return;
		}
		List<TMP_Text> list = newTexts;
		object obj = 0;
		object obj2 = 0;
		UnityEngine.Object obj3 = default(UnityEngine.Object);
		while ((nint)obj2 < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj3 != null)
			{
				nint num = (nint)obj3;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v238 @ r8_v7 (Il2CppClass<UnityEngine.Object>)+558] (should have been resolved before IL gen)");
			}
			list = newTexts;
			obj++;
			obj2 = obj;
		}
	}

	private void ClearNew()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A0AD]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		StageNew("");
	}

	private static char FirstCharOrNull(string s)
	{
		//IL_0063: Expected I4, but got O
		if (!string.IsNullOrEmpty(s))
		{
			if (s != null)
			{
				return s.get_Chars(0);
			}
			NullReferenceException ex = new NullReferenceException();
			return (char)(int)ex;
		}
		return '\0';
	}

	private unsafe void ChooseDirectionAndNext(int currentIndex, int desiredIndex, out FlipDirection direction, out char nextChar)
	{
		//IL_0021: Expected O, but got I4
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected I4, but got Unknown
		//IL_0052: Expected O, but got I4
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected I4, but got Unknown
		//IL_0169: Expected O, but got I4
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Expected I4, but got Unknown
		//IL_0199: Expected O, but got I4
		//IL_012c: Expected O, but got I4
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
		string text = orderedSymbols;
		object obj = text._stringLength - currentIndex;
		object obj2 = obj + desiredIndex;
		int num = obj2 % text._stringLength;
		object obj3 = text._stringLength - desiredIndex;
		object obj4 = obj3 + currentIndex;
		int num2 = obj4 % text._stringLength;
		string text2;
		object obj6;
		if (directionMode != DirectionMode.ForceUp && (directionMode == DirectionMode.ForceDown || (num >= num2 && (num > num2 || preferDownOnTie))))
		{
			ref FlipDirection reference = ref *(FlipDirection*)1;
			text2 = orderedSymbols;
			object obj5 = currentIndex - 1;
			obj6 = obj5 + text._stringLength;
		}
		else
		{
			ref FlipDirection reference = ref *(FlipDirection*)null;
			text2 = orderedSymbols;
			obj6 = currentIndex + 1;
		}
		int index = obj6 % text._stringLength;
		char c = text2.get_Chars(index);
		object obj7 = c;
	}

	private int ComputeRemainingStepsInDirection(int currentIndex, int desiredIndex, FlipDirection direction)
	{
		//IL_00eb: Expected I4, but got O
		//IL_00b3: Expected O, but got I4
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected I4, but got Unknown
		//IL_007d: Expected O, but got I4
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected I4, but got Unknown
		string text = orderedSymbols;
		if (orderedSymbols != null)
		{
			if (text._stringLength > 0)
			{
				if (direction != FlipDirection.Up)
				{
					object obj = text._stringLength - desiredIndex;
					object obj2 = obj + currentIndex;
					return obj2 % text._stringLength;
				}
				object obj3 = text._stringLength - currentIndex;
				object obj4 = obj3 + desiredIndex;
				return obj4 % text._stringLength;
			}
			return 0;
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	private void UpdateAnimatorSpeedForRemainingSteps(int remainingSteps)
	{
		//IL_0348: Invalid comparison between I4 and F4
		//IL_0166: Expected F4, but got I4
		//IL_00c8: Expected O, but got I4
		//IL_00d5: Expected O, but got I4
		//IL_00ed: Invalid comparison between I4 and F4
		//IL_00b6: Expected F4, but got I4
		//IL_017b: Expected O, but got I4
		//IL_03af: Invalid comparison between I4 and F4
		//IL_02a7: Expected F4, but got I4
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Expected O, but got Unknown
		//IL_0408: Invalid comparison between F4 and I4
		//IL_041a: Expected F4, but got I4
		if (!adaptiveFlipSpeed || !(animator != null))
		{
			return;
		}
		bool flag = remainingSteps < 0;
		int num = 0;
		if (!flag)
		{
			num = remainingSteps;
		}
		int num2 = adaptiveMinDistanceSteps;
		if (adaptiveMinDistanceSteps < 1)
		{
			num2 = 1;
		}
		int num3 = adaptiveMaxDistanceSteps;
		if (adaptiveMaxDistanceSteps < 1)
		{
			num3 = 1;
		}
		bool flag2 = num3 < num2;
		int num4 = num2;
		if (!flag2)
		{
			num4 = num3;
		}
		if (num4 == num2)
		{
			goto IL_0121;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804198FBh\"");
		float num5;
		if (num2 != num4)
		{
			object obj = num4 - num2;
			object obj2 = num - num2;
			num5 = (float)obj2 / (float)obj;
			if (!(0f > num5))
			{
				if (num5 > 1f)
				{
					goto IL_0121;
				}
				goto IL_033f;
			}
		}
		num5 = 0f;
		goto IL_033f;
		IL_033f:
		if (!(0f > num5))
		{
			if (num5 > 1f)
			{
				num5 = 1f;
			}
		}
		else
		{
			num5 = 0f;
		}
		bool flag3 = adaptiveSpeedMapping == AdaptiveSpeedMapping.Linear;
		if (!flag3)
		{
			object obj3 = adaptiveSpeedMapping - 1;
			if (!flag3)
			{
				object obj4 = obj3 - 1;
				if (!flag3)
				{
					if ((nint)obj4 == 1)
					{
						float num6 = num5 + num5;
						float num7 = 3f - num6;
						float num8 = num5 * num5;
						num5 = num7 * num8;
					}
				}
				else
				{
					float num9 = 1f - num5;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
					num5 = 1f - num9;
				}
			}
			else
			{
				float num10 = num5 * num5;
				num5 = num10;
			}
		}
		bool flag4 = !(0.0001f < adaptiveMinSpeedMultiplier);
		float num11 = 0.0001f;
		if (!flag4)
		{
			num11 = adaptiveMinSpeedMultiplier;
		}
		bool flag5 = !(0.0001f < adaptiveMaxSpeedMultiplier);
		float num12 = 0.0001f;
		if (!flag5)
		{
			num12 = adaptiveMaxSpeedMultiplier;
		}
		if (!(0f > num5))
		{
			if (num5 > 1f)
			{
				num5 = 1f;
			}
		}
		else
		{
			num5 = 0f;
		}
		float num13 = num12 - num11;
		float num14 = num12 - num11;
		float num15 = num13 * num5;
		float num16 = num15 + num11;
		bool flag6 = !(num14 > 0f);
		float num17 = 0f;
		if (!flag6)
		{
			float num18 = num16 - num11;
			num17 = num18 / num14;
		}
		_003CNormalizedSpeed_003Ek__BackingField = num17;
		float speed = num16 * _baselineAnimatorSpeed;
		ApplyAnimatorSpeed(speed);
		return;
		IL_0121:
		num5 = 1f;
		goto IL_033f;
	}

	private static float ApplyMapping(float t01, AdaptiveSpeedMapping mapping)
	{
		//IL_0009: Invalid comparison between I4 and F4
		//IL_005c: Expected F4, but got I4
		//IL_006f: Expected O, but got I4
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		float num;
		if (!(0f > t01))
		{
			bool flag = !(t01 > 1f);
			num = t01;
			if (!flag)
			{
				num = 1f;
			}
		}
		else
		{
			num = 0f;
		}
		bool flag2 = mapping == AdaptiveSpeedMapping.Linear;
		if (!flag2)
		{
			object obj = mapping - 1;
			if (!flag2)
			{
				object obj2 = obj - 1;
				if (flag2)
				{
					float num2 = 1f - num;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
					return 1f - num2;
				}
				if ((nint)obj2 == 1)
				{
					float num3 = num + num;
					float num4 = num * num;
					float num5 = 3f - num3;
					return num5 * num4;
				}
			}
			else
			{
				num *= num;
			}
		}
		return num;
	}

	private void ApplyAnimatorSpeed(float speed)
	{
		if (animator != null)
		{
			bool flag = !(0.0001f < speed);
			float speed2 = 0.0001f;
			if (!flag)
			{
				speed2 = speed;
			}
			animator.speed = speed2;
		}
	}

	public SplitFlipDisplay()
	{
		List<TMP_Text> list = new List<TMP_Text>();
		oldTexts = list;
		newTexts = new List<TMP_Text>();
		flipUpTrigger = "FlipUp";
		flipDownTrigger = "FlipDown";
		initialValue = "A";
		desiredValue = "A";
		orderedSymbols = " ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		preferDownOnTie = true;
		applyDesiredOnEnable = true;
		pollIntervalSeconds = 0.05f;
		adaptiveFlipSpeed = true;
		adaptiveMinSpeedMultiplier = 1f;
		adaptiveMaxSpeedMultiplier = 3f;
		adaptiveMinDistanceSteps = 1;
		adaptiveMaxDistanceSteps = 12;
		adaptiveSpeedMapping = AdaptiveSpeedMapping.EaseOut;
		clearNewTextsWhenIdle = true;
		_baselineAnimatorSpeed = 1f;
		base._002Ector();
	}
}
