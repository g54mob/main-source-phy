using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public sealed class DialToSplitFlipDisplayBinder : MonoBehaviour
{
	public enum IndexRoundingMode
	{
		Floor,
		Round,
		Ceil
	}

	private DialInteractable dial;

	private SplitFlipDisplay splitFlipDisplay;

	private string orderedSymbols;

	private float outputRangeMin;

	private float outputRangeMax;

	private IndexRoundingMode indexRounding;

	private bool useResponseCurve;

	private AnimationCurve responseCurve;

	private bool applyOnEnable;

	private float minUpdateIntervalSeconds;

	private bool onlySendOnIndexChange;

	private UnityEvent<int> onSelectedIndexChanged;

	private UnityEvent<string> onSelectedSymbolChanged;

	private int _lastIndex;

	private float _lastSentTimeUnscaled;

	private unsafe void OnEnable()
	{
		//IL_0158: Invalid comparison between I4 and F4
		//IL_016a: Expected F4, but got I4
		//IL_02c8: Invalid comparison between F4 and I4
		if (dial != null)
		{
			DialInteractable dialInteractable = dial;
			if (dialInteractable.OnValueChanged != null)
			{
				UnityAction<float> call = HandleDialValueChanged;
				dialInteractable.OnValueChanged.AddListener(call);
			}
		}
		if (!applyOnEnable)
		{
			return;
		}
		float accumulatedValue;
		if (dial != null)
		{
			DialInteractable dialInteractable2 = dial;
			accumulatedValue = dialInteractable2.accumulatedValue;
		}
		else
		{
			accumulatedValue = outputRangeMin;
		}
		if (!(splitFlipDisplay != null) || string.IsNullOrEmpty(orderedSymbols))
		{
			return;
		}
		float unscaledTime = Time.unscaledTime;
		bool flag = !(0f < minUpdateIntervalSeconds);
		float num = 0f;
		if (!flag)
		{
			num = minUpdateIntervalSeconds;
		}
		if (num > 0f)
		{
			float num2 = unscaledTime - _lastSentTimeUnscaled;
			if (num > num2)
			{
				return;
			}
		}
		int num3 = MapDialValueToSymbolIndex(accumulatedValue);
		if (!onlySendOnIndexChange || num3 != _lastIndex)
		{
			_lastSentTimeUnscaled = unscaledTime;
			_lastIndex = num3;
			char c = orderedSymbols.get_Chars(num3);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D4CC80");
			string text = default(string);
			splitFlipDisplay.SetDesiredValueAndApply(text);
			if (onSelectedIndexChanged != null)
			{
				object obj = default(object);
				onSelectedIndexChanged.Invoke((int)(&obj));
			}
			if (onSelectedSymbolChanged != null)
			{
				onSelectedSymbolChanged.Invoke(text);
			}
		}
	}

	private void OnDisable()
	{
		if (dial != null)
		{
			DialInteractable dialInteractable = dial;
			if (dialInteractable.OnValueChanged != null)
			{
				UnityAction<float> call = HandleDialValueChanged;
				dialInteractable.OnValueChanged.RemoveListener(call);
			}
		}
	}

	private unsafe void HandleDialValueChanged(float dialOutputValue)
	{
		//IL_0054: Invalid comparison between I4 and F4
		//IL_0066: Expected F4, but got I4
		//IL_01bf: Invalid comparison between F4 and I4
		if (!(splitFlipDisplay != null) || string.IsNullOrEmpty(orderedSymbols))
		{
			return;
		}
		float unscaledTime = Time.unscaledTime;
		bool flag = !(0f < minUpdateIntervalSeconds);
		float num = 0f;
		if (!flag)
		{
			num = minUpdateIntervalSeconds;
		}
		if (num > 0f)
		{
			float num2 = unscaledTime - _lastSentTimeUnscaled;
			if (num > num2)
			{
				return;
			}
		}
		int num3 = MapDialValueToSymbolIndex(dialOutputValue);
		if (!onlySendOnIndexChange || num3 != _lastIndex)
		{
			_lastSentTimeUnscaled = unscaledTime;
			_lastIndex = num3;
			char c = orderedSymbols.get_Chars(num3);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D4CC80");
			string text = default(string);
			splitFlipDisplay.SetDesiredValueAndApply(text);
			if (onSelectedIndexChanged != null)
			{
				object obj = default(object);
				onSelectedIndexChanged.Invoke((int)(&obj));
			}
			if (onSelectedSymbolChanged != null)
			{
				onSelectedSymbolChanged.Invoke(text);
			}
		}
	}

	private int MapDialValueToSymbolIndex(float dialOutputValue)
	{
		//IL_03b1: Expected I4, but got O
		//IL_011d: Invalid comparison between I4 and F4
		//IL_012c: Expected O, but got I4
		//IL_03f9: Expected F4, but got I4
		//IL_00ae: Expected O, but got I4
		//IL_0406: Invalid comparison between O and F4
		//IL_0155: Expected O, but got I4
		//IL_0183: Expected F4, but got I4
		//IL_03da: Expected O, but got I4
		//IL_016c: Expected O, but got I4
		//IL_042d: Expected O, but got I4
		//IL_01b1: Expected O, but got I4
		//IL_0205: Expected O, but got I4
		//IL_025f: Invalid comparison between O and F4
		//IL_02dc: Expected O, but got I4
		//IL_02e5: Expected F4, but got I4
		//IL_0296: Expected O, but got I4
		//IL_02bd: Expected O, but got I4
		string text = orderedSymbols;
		object obj2;
		float num3;
		if (orderedSymbols != null)
		{
			if (text._stringLength > 1)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
				object obj = default(object);
				if (obj == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804080E9h\"");
					if (outputRangeMin == outputRangeMax)
					{
						obj2 = 0;
					}
					else
					{
						float num = outputRangeMax - outputRangeMin;
						float num2 = dialOutputValue - outputRangeMin;
						num3 = num2 / num;
						bool flag = 0f > num3;
						obj2 = 0;
						if (!flag)
						{
							bool flag2 = !(num3 > 1f);
							obj2 = 0;
							if (!flag2)
							{
								obj2 = 0;
								num3 = 1f;
							}
							goto IL_03fe;
						}
					}
					num3 = 0f;
					goto IL_03fe;
				}
			}
			goto IL_0395;
		}
		goto IL_03a3;
		IL_03fe:
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3))
		{
			if (num3 > 1f)
			{
				num3 = 1f;
			}
		}
		else
		{
			num3 = 0f;
		}
		bool flag3 = !useResponseCurve;
		AnimationCurve animationCurve = (AnimationCurve)(object)this;
		float num4 = outputRangeMax;
		object obj3 = 0;
		float num5 = num3;
		if (!flag3)
		{
			bool flag4 = responseCurve == null;
			animationCurve = (AnimationCurve)(object)this;
			num4 = outputRangeMax;
			obj3 = 0;
			num5 = num3;
			if (!flag4)
			{
				animationCurve = responseCurve;
				int length = responseCurve.length;
				bool flag5 = length <= 0;
				num4 = outputRangeMax;
				obj3 = 0;
				num5 = num3;
				if (!flag5)
				{
					animationCurve = responseCurve;
					if (responseCurve == null)
					{
						goto IL_03a3;
					}
					float num6 = responseCurve.Evaluate(num3);
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num6))
					{
						bool flag6 = !(num6 > 1f);
						num4 = num3;
						obj3 = 0;
						num5 = num6;
						if (!flag6)
						{
							num4 = num3;
							obj3 = 0;
							num5 = 1f;
						}
					}
					else
					{
						num4 = num3;
						obj3 = 0;
						num5 = 0f;
					}
				}
			}
		}
		object obj4 = text._stringLength - 1;
		float num7 = (float)obj4 * num5;
		if (indexRounding == IndexRoundingMode.Floor)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803DB600");
		}
		else if (indexRounding == IndexRoundingMode.Ceil)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803DB5A0");
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
		}
		int num8 = text._stringLength - 1;
		int num9 = default(int);
		if (num9 >= 0)
		{
			if (num9 > num8)
			{
				return num8;
			}
			return num9;
		}
		goto IL_0395;
		IL_03a3:
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
		IL_0395:
		return 0;
	}

	public DialToSplitFlipDisplayBinder()
	{
		//IL_0063: Expected I4, but got I8
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A096]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		orderedSymbols = " ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		outputRangeMax = 25f;
		indexRounding = IndexRoundingMode.Round;
		AnimationCurve animationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
		responseCurve = animationCurve;
		applyOnEnable = true;
		minUpdateIntervalSeconds = 0.02f;
		onlySendOnIndexChange = true;
		_lastIndex = -2147483648;
		_lastSentTimeUnscaled = -1f / 0f;
		base._002Ector();
	}
}
