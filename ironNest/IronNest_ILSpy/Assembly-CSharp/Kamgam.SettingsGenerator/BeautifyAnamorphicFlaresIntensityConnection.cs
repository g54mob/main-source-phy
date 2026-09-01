using System;
using System.Runtime.CompilerServices;
using Beautify.Universal;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace Kamgam.SettingsGenerator;

public class BeautifyAnamorphicFlaresIntensityConnection : Connection<float>
{
	private readonly Vector2 _inputRange;

	private readonly Vector2 _outputRange;

	private readonly BeautifyConnectionResolver _resolver;

	public BeautifyAnamorphicFlaresIntensityConnection(Vector2 inputRange, Vector2 outputRange, bool resolveEveryAccess, bool logWarnings)
	{
		//IL_0096: Expected I4, but got O
		base._002Ector();
		object obj = default(object);
		bool flag = System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref inputRange) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj);
		Vector2 inputRange2 = inputRange;
		Vector2 vector = default(Vector2);
		if (!flag)
		{
			inputRange2 = vector;
		}
		object obj2 = default(object);
		bool flag2 = System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref outputRange) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2);
		Vector2 outputRange2 = outputRange;
		if (!flag2)
		{
			outputRange2 = vector;
		}
		_inputRange = inputRange2;
		_outputRange = outputRange2;
		bool logWarnings2 = default(bool);
		_resolver = new BeautifyConnectionResolver(resolveEveryAccess: false, (byte)(int)outputRange != 0)
		{
			_logWarnings = logWarnings2,
			_resolveEveryAccess = resolveEveryAccess
		};
	}

	public new void Destroy()
	{
		BeautifyConnectionResolver resolver = _resolver;
		resolver._cached = null;
	}

	public override float Get()
	{
		//IL_004f: Expected I, but got O
		//IL_01dd: Expected F4, but got O
		//IL_01d6: Expected F4, but got O
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Expected O, but got Unknown
		//IL_0164: Invalid comparison between I4 and F4
		//IL_0173: Expected O, but got I4
		//IL_0224: Expected F4, but got I4
		//IL_00c2: Expected O, but got I4
		//IL_0231: Invalid comparison between O and F4
		//IL_019c: Expected O, but got I4
		//IL_01ca: Expected F4, but got I4
		//IL_01bc: Expected O, but got I4
		Beautify.Universal.Beautify beautify = _resolver.Resolve();
		object obj2;
		float num2;
		if (beautify != null)
		{
			FloatParameter anamorphicFlaresIntensity = beautify.anamorphicFlaresIntensity;
			nint num = (nint)anamorphicFlaresIntensity;
			float value = anamorphicFlaresIntensity.value;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj = default(object);
			if (obj == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804B422Bh\"");
				Vector2 outputRange = _outputRange;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyAnamorphicFlaresIntensityConnection)+34]");
				if ((object)outputRange == null)
				{
					obj2 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyAnamorphicFlaresIntensityConnection)+34]");
					object obj3 = 0 - _outputRange;
					object obj5 = default(object);
					object obj4 = obj5 - (object)_outputRange;
					num2 = (float)obj4 / (float)obj3;
					bool flag = 0f > num2;
					obj2 = 0;
					if (!flag)
					{
						bool flag2 = !(num2 > 1f);
						obj2 = 0;
						if (!flag2)
						{
							num2 = 1f;
							obj2 = 0;
						}
						goto IL_0229;
					}
				}
				num2 = 0f;
				goto IL_0229;
			}
			return (float)_inputRange;
		}
		return (float)_inputRange;
		IL_0229:
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2))
		{
			if (num2 > 1f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyAnamorphicFlaresIntensityConnection)+2C]");
				float num3 = 0f - (float)_inputRange;
				float num4 = num3 * 1f;
				return num4 + (float)_inputRange;
			}
		}
		else
		{
			num2 = 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyAnamorphicFlaresIntensityConnection)+2C]");
		float num5 = 0f - (float)_inputRange;
		float num6 = num5 * num2;
		return num6 + (float)_inputRange;
	}

	public override void Set(float uiValue)
	{
		//IL_01d1: Expected F4, but got O
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Expected O, but got Unknown
		//IL_0145: Invalid comparison between I4 and F4
		//IL_0154: Expected O, but got I4
		//IL_0246: Expected F4, but got I4
		//IL_009b: Expected O, but got I4
		//IL_0253: Invalid comparison between O and F4
		//IL_017d: Expected O, but got I4
		//IL_01ab: Expected F4, but got I4
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Expected O, but got Unknown
		//IL_0238: Expected O, but got F4
		//IL_0194: Expected O, but got I4
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_0100: Expected O, but got F4
		Beautify.Universal.Beautify beautify = _resolver.Resolve();
		if (!(beautify != null))
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj = default(object);
		object obj2;
		float num2;
		if (obj == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804B4502h\"");
			Vector2 inputRange = _inputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyAnamorphicFlaresIntensityConnection)+2C]");
			if ((object)inputRange == null)
			{
				obj2 = 0;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyAnamorphicFlaresIntensityConnection)+2C]");
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
					goto IL_024b;
				}
			}
			num2 = 0f;
			goto IL_024b;
		}
		Vector2 vector = _outputRange;
		goto IL_01bf;
		IL_01bf:
		beautify.anamorphicFlaresIntensity.value = (float)vector;
		beautify.anamorphicFlaresIntensity.overrideState = true;
		base.NotifyListenersIfChanged(uiValue);
		return;
		IL_024b:
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2))
		{
			if (num2 > 1f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyAnamorphicFlaresIntensityConnection)+34]");
				object obj4 = 0 - _outputRange;
				float num3 = (float)obj4 * 1f;
				float num4 = num3 + (float)_outputRange;
				vector = (Vector2)num4;
				goto IL_01bf;
			}
		}
		else
		{
			num2 = 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyAnamorphicFlaresIntensityConnection)+34]");
		object obj5 = 0 - _outputRange;
		float num5 = (float)obj5 * num2;
		float num6 = num5 + (float)_outputRange;
		vector = (Vector2)num6;
		goto IL_01bf;
	}

	private float MapInputToOutput(float v)
	{
		//IL_017c: Expected F4, but got O
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_010a: Invalid comparison between I4 and F4
		//IL_0119: Expected O, but got I4
		//IL_01c3: Expected F4, but got I4
		//IL_0066: Expected O, but got I4
		//IL_01d0: Invalid comparison between O and F4
		//IL_0142: Expected O, but got I4
		//IL_0170: Expected F4, but got I4
		//IL_0159: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj = default(object);
		object obj2;
		float num2;
		if (obj == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804B4318h\"");
			Vector2 inputRange = _inputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyAnamorphicFlaresIntensityConnection)+2C]");
			if ((object)inputRange == null)
			{
				obj2 = 0;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyAnamorphicFlaresIntensityConnection)+2C]");
				object obj3 = 0 - _inputRange;
				float num = v - (float)_inputRange;
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
					goto IL_01c8;
				}
			}
			num2 = 0f;
			goto IL_01c8;
		}
		return (float)_outputRange;
		IL_01c8:
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2))
		{
			if (num2 > 1f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyAnamorphicFlaresIntensityConnection)+34]");
				float num3 = 0f - (float)_outputRange;
				float num4 = num3 * 1f;
				return num4 + (float)_outputRange;
			}
		}
		else
		{
			num2 = 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyAnamorphicFlaresIntensityConnection)+34]");
		float num5 = 0f - (float)_outputRange;
		float num6 = num5 * num2;
		return num6 + (float)_outputRange;
	}

	private float MapOutputToInput(float v)
	{
		//IL_017c: Expected F4, but got O
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_010a: Invalid comparison between I4 and F4
		//IL_0119: Expected O, but got I4
		//IL_01c3: Expected F4, but got I4
		//IL_0066: Expected O, but got I4
		//IL_01d0: Invalid comparison between O and F4
		//IL_0142: Expected O, but got I4
		//IL_0170: Expected F4, but got I4
		//IL_0159: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj = default(object);
		object obj2;
		float num2;
		if (obj == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804B43D8h\"");
			Vector2 outputRange = _outputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyAnamorphicFlaresIntensityConnection)+34]");
			if ((object)outputRange == null)
			{
				obj2 = 0;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyAnamorphicFlaresIntensityConnection)+34]");
				object obj3 = 0 - _outputRange;
				float num = v - (float)_outputRange;
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
					goto IL_01c8;
				}
			}
			num2 = 0f;
			goto IL_01c8;
		}
		return (float)_inputRange;
		IL_01c8:
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2))
		{
			if (num2 > 1f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyAnamorphicFlaresIntensityConnection)+2C]");
				float num3 = 0f - (float)_inputRange;
				float num4 = num3 * 1f;
				return num4 + (float)_inputRange;
			}
		}
		else
		{
			num2 = 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.BeautifyAnamorphicFlaresIntensityConnection)+2C]");
		float num5 = 0f - (float)_inputRange;
		float num6 = num5 * num2;
		return num6 + (float)_inputRange;
	}
}
