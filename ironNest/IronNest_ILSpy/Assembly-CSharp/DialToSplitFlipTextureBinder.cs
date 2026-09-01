using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public sealed class DialToSplitFlipTextureBinder : MonoBehaviour
{
	public enum DialMode
	{
		Limited,
		Unlimited
	}

	public enum IndexRoundingMode
	{
		Floor,
		Round,
		Ceil
	}

	private DialInteractable dial;

	private SplitFlipTextureDisplay splitFlipTextureDisplay;

	private DialMode dialMode;

	private float outputRangeMin;

	private float outputRangeMax;

	private float stepsPerTexture;

	private float unlimitedIndexOffset;

	private IndexRoundingMode indexRounding;

	private bool useResponseCurve;

	private AnimationCurve responseCurve;

	private bool applyOnEnable;

	private float minUpdateIntervalSeconds;

	private bool onlySendOnIndexChange;

	private UnityEvent<int> onSelectedIndexChanged;

	private int _lastIndex;

	private float _lastSentTimeUnscaled;

	public int CurrentIndex => _lastIndex;

	public int TextureCount
	{
		get
		{
			//IL_007a: Expected I4, but got O
			if (this.splitFlipTextureDisplay != null)
			{
				SplitFlipTextureDisplay splitFlipTextureDisplay = this.splitFlipTextureDisplay;
				if ((object)this.splitFlipTextureDisplay == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (int)ex;
				}
				List<Texture> orderedTextures = splitFlipTextureDisplay.orderedTextures;
				if (splitFlipTextureDisplay.orderedTextures != null)
				{
					return orderedTextures._size;
				}
			}
			return 0;
		}
	}

	private void OnEnable()
	{
		if (dial != null)
		{
			DialInteractable dialInteractable = dial;
			if (dialInteractable.OnValueChanged != null)
			{
				UnityAction<float> call = HandleDialValueChanged;
				dialInteractable.OnValueChanged.AddListener(call);
			}
		}
		if (applyOnEnable)
		{
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
			HandleDialValueChanged(accumulatedValue);
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
		//IL_0070: Invalid comparison between I4 and F4
		//IL_0082: Expected F4, but got I4
		//IL_0251: Invalid comparison between F4 and I4
		if (!(this.splitFlipTextureDisplay != null))
		{
			return;
		}
		SplitFlipTextureDisplay splitFlipTextureDisplay = this.splitFlipTextureDisplay;
		List<Texture> orderedTextures = splitFlipTextureDisplay.orderedTextures;
		if (splitFlipTextureDisplay.orderedTextures == null || orderedTextures._size <= 0)
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
		int num4;
		if (orderedTextures._size > 1)
		{
			if (dialMode != DialMode.Unlimited)
			{
				int num3 = MapLimitedMode(dialOutputValue, orderedTextures._size);
				num4 = num3;
			}
			else
			{
				float num5 = dialOutputValue - unlimitedIndexOffset;
				bool flag2 = !(0.0001f < stepsPerTexture);
				float num6 = 0.0001f;
				if (!flag2)
				{
					num6 = stepsPerTexture;
				}
				float rawIndex = num5 / num6;
				int num7 = ApplyRounding(rawIndex);
				int num8 = num7 % orderedTextures._size;
				num4 = orderedTextures._size + num8;
				if (num8 >= 0)
				{
					num4 = num8;
				}
			}
		}
		else
		{
			num4 = 0;
		}
		if (!onlySendOnIndexChange || num4 != _lastIndex)
		{
			_lastSentTimeUnscaled = unscaledTime;
			_lastIndex = num4;
			this.splitFlipTextureDisplay.SetDesiredIndexAndApply(num4);
			if (onSelectedIndexChanged != null)
			{
				object obj = default(object);
				onSelectedIndexChanged.Invoke((int)(&obj));
			}
		}
	}

	private int MapDialValueToTextureIndex(float dialOutputValue, int textureCount)
	{
		if (textureCount > 1)
		{
			bool flag = dialMode == DialMode.Unlimited;
			int num = textureCount;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 33 Invalid \"Jump target not found in method: 0x180408940\"");
				int num2 = default(int);
				num = num2;
			}
			bool flag2 = !(0.0001f < stepsPerTexture);
			float num3 = 0.0001f;
			if (!flag2)
			{
				num3 = stepsPerTexture;
			}
			float num4 = dialOutputValue - unlimitedIndexOffset;
			float rawIndex = num4 / num3;
			int num5 = ApplyRounding(rawIndex);
			int num6 = num5 % num;
			bool flag3 = num6 < 0;
			int result = num6 + num;
			if (!flag3)
			{
				result = num6;
			}
			return result;
		}
		return 0;
	}

	private int MapLimitedMode(float dialOutputValue, int textureCount)
	{
		//IL_00cf: Invalid comparison between I4 and F4
		//IL_00de: Expected O, but got I4
		//IL_0298: Expected F4, but got I4
		//IL_0060: Expected O, but got I4
		//IL_02a5: Invalid comparison between O and F4
		//IL_0107: Expected O, but got I4
		//IL_0135: Expected F4, but got I4
		//IL_011e: Expected O, but got I4
		//IL_02c7: Expected O, but got I4
		//IL_0270: Expected I4, but got O
		//IL_01c2: Invalid comparison between O and F4
		//IL_0241: Expected F4, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj = default(object);
		object obj2;
		float num3;
		if (obj == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804089B3h\"");
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
					goto IL_029d;
				}
			}
			num3 = 0f;
			goto IL_029d;
		}
		int num4 = 0;
		goto IL_030f;
		IL_029d:
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
		if (useResponseCurve && responseCurve != null)
		{
			int length = responseCurve.length;
			if (length > 0)
			{
				if (responseCurve == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (int)ex;
				}
				float num5 = responseCurve.Evaluate(num3);
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num5))
				{
					bool flag3 = !(num5 > 1f);
					num3 = num5;
					if (!flag3)
					{
						num3 = 1f;
					}
				}
				else
				{
					num3 = 0f;
				}
			}
		}
		object obj3 = textureCount - 1;
		int num6 = textureCount - 1;
		float rawIndex = (float)obj3 * num3;
		num4 = ApplyRounding(rawIndex);
		if (num4 >= 0)
		{
			if (num4 > num6)
			{
				num4 = num6;
			}
		}
		else
		{
			num4 = 0;
		}
		goto IL_030f;
		IL_030f:
		return num4;
	}

	private int MapUnlimitedMode(float dialOutputValue, int textureCount)
	{
		bool flag = !(0.0001f < stepsPerTexture);
		float num = 0.0001f;
		if (!flag)
		{
			num = stepsPerTexture;
		}
		float num2 = dialOutputValue - unlimitedIndexOffset;
		float rawIndex = num2 / num;
		int num3 = ApplyRounding(rawIndex);
		int num4 = num3 % textureCount;
		bool flag2 = num4 < 0;
		int result = num4 + textureCount;
		if (!flag2)
		{
			result = num4;
		}
		return result;
	}

	private int ApplyRounding(float rawIndex)
	{
		//IL_0048: Expected I4, but got F8
		//IL_0090: Expected I4, but got F8
		if (indexRounding == IndexRoundingMode.Floor)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
			double num = Math.Floor(0.0);
			return (int)num;
		}
		if (indexRounding == IndexRoundingMode.Ceil)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
			double num2 = Math.Ceiling(0.0);
			return (int)num2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
		int result = default(int);
		return result;
	}

	public unsafe void SetDesiredIndex(int index)
	{
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Expected I4, but got Unknown
		//IL_0044: Expected O, but got I4
		if (!(this.splitFlipTextureDisplay != null))
		{
			return;
		}
		SplitFlipTextureDisplay splitFlipTextureDisplay = this.splitFlipTextureDisplay;
		List<Texture> list = splitFlipTextureDisplay.orderedTextures;
		if (splitFlipTextureDisplay.orderedTextures != null)
		{
			list = (List<Texture>)list._size;
		}
		int num = list - 1;
		int num2;
		if (index >= 0)
		{
			bool flag = index <= num;
			num2 = index;
			if (!flag)
			{
				num2 = num;
			}
		}
		else
		{
			num2 = 0;
		}
		_lastIndex = num2;
		float unscaledTime = Time.unscaledTime;
		_lastSentTimeUnscaled = unscaledTime;
		this.splitFlipTextureDisplay.SetDesiredIndexAndApply(num2);
		if (onSelectedIndexChanged != null)
		{
			object obj = default(object);
			onSelectedIndexChanged.Invoke((int)(&obj));
		}
	}

	public void ForceRefresh()
	{
		if (dial != null)
		{
			DialInteractable dialInteractable = dial;
			_lastSentTimeUnscaled = -1f / 0f;
			HandleDialValueChanged(dialInteractable.accumulatedValue);
		}
	}

	public DialToSplitFlipTextureBinder()
	{
		//IL_007d: Expected I4, but got I8
		outputRangeMax = 26f;
		stepsPerTexture = 1f;
		indexRounding = IndexRoundingMode.Round;
		responseCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
		applyOnEnable = true;
		minUpdateIntervalSeconds = 0.02f;
		onlySendOnIndexChange = true;
		_lastIndex = -2147483648;
		_lastSentTimeUnscaled = -1f / 0f;
		base._002Ector();
	}
}
