using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class FmodMasterVolumeConnection : Connection<float>
{
	private readonly Vector2 _inputRange;

	private readonly Vector2 _outputLinearRange;

	private readonly string _busPath;

	private Bus _bus;

	private bool _busResolved;

	public FmodMasterVolumeConnection(Vector2 inputRange, Vector2 outputLinearRange, string busPath)
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
		bool flag2 = System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref outputLinearRange) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2);
		Vector2 outputLinearRange2 = outputLinearRange;
		if (!flag2)
		{
			outputLinearRange2 = vector;
		}
		_inputRange = inputRange2;
		_outputLinearRange = outputLinearRange2;
		bool flag3 = string.IsNullOrWhiteSpace(busPath);
		bool flag4 = !flag3;
		string busPath2 = busPath;
		if (!flag4)
		{
			busPath2 = "bus:/";
		}
		_busPath = busPath2;
	}

	public new void Destroy()
	{
		//IL_0016: Expected O, but got I4
		_busResolved = false;
		_bus = (Bus)0;
	}

	public unsafe override float Get()
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Expected O, but got Unknown
		if (Application.isPlaying)
		{
			ResolveBusIfNeeded();
			if (!_busResolved)
			{
				return MapLinearToUi(1f);
			}
			Bus bus = (Bus)(this + 64);
			if (((Bus*)bus)->getVolume(out var volume) == RESULT.OK)
			{
				return MapLinearToUi(volume);
			}
		}
		return MapLinearToUi(1f);
	}

	public unsafe override void Set(float uiValue)
	{
		//IL_01d3: Expected F4, but got O
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Expected O, but got Unknown
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Expected O, but got Unknown
		//IL_015e: Invalid comparison between I4 and F4
		//IL_016d: Expected O, but got I4
		//IL_01e1: Expected I4, but got O
		//IL_02ad: Expected F4, but got I4
		//IL_00b2: Expected O, but got I4
		//IL_02ba: Invalid comparison between O and F4
		//IL_0196: Expected O, but got I4
		//IL_01c4: Expected F4, but got I4
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Expected O, but got Unknown
		//IL_01ad: Expected O, but got I4
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		if (!Application.isPlaying)
		{
			return;
		}
		ResolveBusIfNeeded();
		if (!_busResolved)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj = default(object);
		object obj2;
		float num2;
		if (obj == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804CF5DBh\"");
			Vector2 inputRange = _inputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnection)+2C]");
			if ((object)inputRange == null)
			{
				obj2 = 0;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnection)+2C]");
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
					goto IL_02b2;
				}
			}
			num2 = 0f;
			goto IL_02b2;
		}
		float volume = (float)_outputLinearRange;
		goto IL_026b;
		IL_02b2:
		float num4;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2))
		{
			if (num2 > 1f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnection)+34]");
				object obj4 = 0 - _outputLinearRange;
				float num3 = (float)obj4 * 1f;
				volume = num3 + (float)_outputLinearRange;
				num4 = 1f;
				goto IL_026b;
			}
		}
		else
		{
			num2 = 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnection)+34]");
		object obj5 = 0 - _outputLinearRange;
		float num5 = (float)obj5 * num2;
		volume = num5 + (float)_outputLinearRange;
		num4 = 1f;
		goto IL_026b;
		IL_026b:
		Bus bus = (Bus)(this + 64);
		if (((Bus*)bus)->setVolume(volume) != RESULT.OK)
		{
			object obj6 = default(object);
			object arg = (RESULT)obj6;
			string message = $"[FMOD] setVolume failed on '{_busPath}' with result: {arg}";
			UnityEngine.Debug.LogWarning(message);
		}
		else
		{
			base.NotifyListenersIfChanged(uiValue);
		}
	}

	private void ResolveBusIfNeeded()
	{
		if (!_busResolved && !string.IsNullOrWhiteSpace(_busPath))
		{
			Bus bus = RuntimeManager.GetBus(_busPath);
			_bus = bus;
			_busResolved = true;
		}
	}

	private float MapUiToLinear(float uiValue)
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnection)+2C]");
			if ((object)inputRange == null)
			{
				obj2 = 0;
			}
			else
			{
				float num = uiValue - (float)_inputRange;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnection)+2C]");
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
		return (float)_outputLinearRange;
		IL_01d1:
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2))
		{
			if (num2 > 1f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnection)+34]");
				object obj4 = 0 - _outputLinearRange;
				float num3 = (float)obj4 * 1f;
				return num3 + (float)_outputLinearRange;
			}
		}
		else
		{
			num2 = 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnection)+34]");
		object obj5 = 0 - _outputLinearRange;
		float num4 = (float)obj5 * num2;
		return num4 + (float)_outputLinearRange;
	}

	private float MapLinearToUi(float linear)
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
			Vector2 outputLinearRange = _outputLinearRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnection)+34]");
			if ((object)outputLinearRange == null)
			{
				obj2 = 0;
			}
			else
			{
				float num = linear - (float)_outputLinearRange;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnection)+34]");
				object obj3 = 0 - _outputLinearRange;
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
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnection)+2C]");
				object obj4 = 0 - _inputRange;
				float num3 = (float)obj4 * 1f;
				return num3 + (float)_inputRange;
			}
		}
		else
		{
			num2 = 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.FmodMasterVolumeConnection)+2C]");
		object obj5 = 0 - _inputRange;
		float num4 = (float)obj5 * num2;
		return num4 + (float)_inputRange;
	}

	private float DefaultUiValue()
	{
		return MapLinearToUi(1f);
	}
}
