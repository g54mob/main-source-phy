using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class ClipboardOffsetConnection : Connection<float>
{
	private readonly Vector2 _inputRange;

	private readonly Vector2 _outputUnitsRange;

	private readonly string _targetTag;

	private readonly bool _resolveEverySet;

	private readonly bool _logWarnings;

	private ClipboardAspectRatioOffsetFader _cachedFader;

	public ClipboardOffsetConnection(Vector2 inputRange, Vector2 outputUnitsRange, string targetTag, bool resolveEverySet, bool logWarnings)
	{
		object obj = default(object);
		bool flag = System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref inputRange) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
		Vector2 inputRange2 = inputRange;
		Vector2 vector = default(Vector2);
		if (!flag)
		{
			inputRange2 = vector;
		}
		object obj2 = default(object);
		bool flag2 = System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref outputUnitsRange) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2);
		Vector2 outputUnitsRange2 = outputUnitsRange;
		if (!flag2)
		{
			outputUnitsRange2 = vector;
		}
		_inputRange = inputRange2;
		_outputUnitsRange = outputUnitsRange2;
		bool flag3 = string.IsNullOrWhiteSpace(targetTag);
		bool flag4 = !flag3;
		string targetTag2 = targetTag;
		if (!flag4)
		{
			targetTag2 = "Clipboard";
		}
		_targetTag = targetTag2;
		bool resolveEverySet2 = default(bool);
		_resolveEverySet = resolveEverySet2;
		bool logWarnings2 = default(bool);
		_logWarnings = logWarnings2;
	}

	public new void Destroy()
	{
		_cachedFader = null;
	}

	public override float Get()
	{
		if (Application.isPlaying)
		{
			ClipboardAspectRatioOffsetFader clipboardAspectRatioOffsetFader = ResolveFader(allowCache: true);
			if (!(clipboardAspectRatioOffsetFader == null))
			{
				return MapUnitsToUi(clipboardAspectRatioOffsetFader.aspectRatioOffsetAmount);
			}
			return MapUnitsToUi(0f);
		}
		return MapUnitsToUi(0f);
	}

	public override void Set(float uiValue)
	{
		//IL_01fe: Expected F4, but got O
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected O, but got Unknown
		//IL_0177: Invalid comparison between I4 and F4
		//IL_0186: Expected O, but got I4
		//IL_0260: Expected F4, but got I4
		//IL_00cd: Expected O, but got I4
		//IL_026d: Invalid comparison between O and F4
		//IL_01af: Expected O, but got I4
		//IL_01dd: Expected F4, but got I4
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Expected O, but got Unknown
		//IL_0252: Expected O, but got F4
		//IL_01c6: Expected O, but got I4
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Expected O, but got Unknown
		//IL_0132: Expected O, but got F4
		if (!Application.isPlaying)
		{
			return;
		}
		bool allowCache = !_resolveEverySet;
		ClipboardAspectRatioOffsetFader clipboardAspectRatioOffsetFader = ResolveFader(allowCache);
		if (!(clipboardAspectRatioOffsetFader != null))
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj = default(object);
		object obj2;
		float num2;
		if (obj == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804CCFE3h\"");
			Vector2 inputRange = _inputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnection)+2C]");
			if ((object)inputRange == null)
			{
				obj2 = 0;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnection)+2C]");
				object obj3 = 0 - _inputRange;
				float num = uiValue - (float)_inputRange;
				num2 = num / (float)obj3;
				bool flag = 0f > num2;
				obj2 = 0;
				if (!flag)
				{
					bool flag2 = !(num2 > 1f);
					obj2 = 0;
					if (!flag2)
					{
						obj2 = 0;
						num2 = 1f;
					}
					goto IL_0265;
				}
			}
			num2 = 0f;
			goto IL_0265;
		}
		Vector2 vector = _outputUnitsRange;
		goto IL_01f1;
		IL_0265:
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2))
		{
			if (num2 > 1f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnection)+34]");
				object obj4 = 0 - _outputUnitsRange;
				float num3 = (float)obj4 * 1f;
				float num4 = num3 + (float)_outputUnitsRange;
				vector = (Vector2)num4;
				goto IL_01f1;
			}
		}
		else
		{
			num2 = 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnection)+34]");
		object obj5 = 0 - _outputUnitsRange;
		float num5 = (float)obj5 * num2;
		float num6 = num5 + (float)_outputUnitsRange;
		vector = (Vector2)num6;
		goto IL_01f1;
		IL_01f1:
		clipboardAspectRatioOffsetFader.aspectRatioOffsetAmount = (float)vector;
		base.NotifyListenersIfChanged(uiValue);
	}

	private ClipboardAspectRatioOffsetFader ResolveFader(bool allowCache)
	{
		if (allowCache && _cachedFader != null)
		{
			return _cachedFader;
		}
		string text;
		object message;
		if (!string.IsNullOrWhiteSpace(_targetTag))
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag(_targetTag);
			if (gameObject != null)
			{
				if ((object)gameObject != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
					UnityEngine.Object obj = default(UnityEngine.Object);
					if (!(obj == null))
					{
						_cachedFader = (ClipboardAspectRatioOffsetFader)obj;
						return (ClipboardAspectRatioOffsetFader)obj;
					}
					if (!_logWarnings)
					{
						goto IL_023b;
					}
					string[] array = new string[5];
					if (array != null)
					{
						array[0] = "[ClipboardOffsetConnection] GameObject '";
						string name = gameObject.name;
						array[1] = name;
						array[2] = "' (tag '";
						array[3] = _targetTag;
						array[4] = "') has no ClipboardAspectRatioOffsetFader component.";
						text = string.Concat(array);
						goto IL_01bf;
					}
				}
				return (ClipboardAspectRatioOffsetFader)(object)new NullReferenceException();
			}
			if (_logWarnings)
			{
				text = "[ClipboardOffsetConnection] No GameObject found with tag '" + _targetTag + "'.";
				goto IL_01bf;
			}
		}
		else if (_logWarnings)
		{
			message = "[ClipboardOffsetConnection] TargetTag is empty. Cannot resolve target.";
			goto IL_0264;
		}
		goto IL_023b;
		IL_0264:
		Debug.LogWarning(message);
		goto IL_023b;
		IL_023b:
		return null;
		IL_01bf:
		message = text;
		goto IL_0264;
	}

	private float MapUiToUnits(float uiValue)
	{
		//IL_017d: Expected F4, but got O
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Expected O, but got Unknown
		//IL_0108: Invalid comparison between I4 and F4
		//IL_0117: Expected O, but got I4
		//IL_01cc: Expected F4, but got I4
		//IL_0066: Expected O, but got I4
		//IL_01d9: Invalid comparison between O and F4
		//IL_0140: Expected O, but got I4
		//IL_016e: Expected F4, but got I4
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Expected O, but got Unknown
		//IL_0157: Expected O, but got I4
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj = default(object);
		object obj2;
		float num2;
		if (obj == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804CCA55h\"");
			Vector2 inputRange = _inputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnection)+2C]");
			if ((object)inputRange == null)
			{
				obj2 = 0;
			}
			else
			{
				float num = uiValue - (float)_inputRange;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnection)+2C]");
				object obj3 = 0 - _inputRange;
				num2 = num / (float)obj3;
				bool flag = 0f > num2;
				obj2 = 0;
				if (!flag)
				{
					bool flag2 = !(num2 > 1f);
					obj2 = 0;
					if (!flag2)
					{
						obj2 = 0;
						num2 = 1f;
					}
					goto IL_01d1;
				}
			}
			num2 = 0f;
			goto IL_01d1;
		}
		return (float)_outputUnitsRange;
		IL_01d1:
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2))
		{
			if (num2 > 1f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnection)+34]");
				object obj4 = 0 - _outputUnitsRange;
				float num3 = (float)obj4 * 1f;
				return num3 + (float)_outputUnitsRange;
			}
		}
		else
		{
			num2 = 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnection)+34]");
		object obj5 = 0 - _outputUnitsRange;
		float num4 = (float)obj5 * num2;
		return num4 + (float)_outputUnitsRange;
	}

	private float MapUnitsToUi(float units)
	{
		//IL_017d: Expected F4, but got O
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Expected O, but got Unknown
		//IL_0108: Invalid comparison between I4 and F4
		//IL_0117: Expected O, but got I4
		//IL_01cc: Expected F4, but got I4
		//IL_0066: Expected O, but got I4
		//IL_01d9: Invalid comparison between O and F4
		//IL_0140: Expected O, but got I4
		//IL_016e: Expected F4, but got I4
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Expected O, but got Unknown
		//IL_0157: Expected O, but got I4
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj = default(object);
		object obj2;
		float num2;
		if (obj == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804CCB25h\"");
			Vector2 outputUnitsRange = _outputUnitsRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnection)+34]");
			if ((object)outputUnitsRange == null)
			{
				obj2 = 0;
			}
			else
			{
				float num = units - (float)_outputUnitsRange;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnection)+34]");
				object obj3 = 0 - _outputUnitsRange;
				num2 = num / (float)obj3;
				bool flag = 0f > num2;
				obj2 = 0;
				if (!flag)
				{
					bool flag2 = !(num2 > 1f);
					obj2 = 0;
					if (!flag2)
					{
						obj2 = 0;
						num2 = 1f;
					}
					goto IL_01d1;
				}
			}
			num2 = 0f;
			goto IL_01d1;
		}
		return (float)_inputRange;
		IL_01d1:
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2))
		{
			if (num2 > 1f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnection)+2C]");
				object obj4 = 0 - _inputRange;
				float num3 = (float)obj4 * 1f;
				return num3 + (float)_inputRange;
			}
		}
		else
		{
			num2 = 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.ClipboardOffsetConnection)+2C]");
		object obj5 = 0 - _inputRange;
		float num4 = (float)obj5 * num2;
		return num4 + (float)_inputRange;
	}

	private float DefaultUiValue()
	{
		return MapUnitsToUi(0f);
	}
}
