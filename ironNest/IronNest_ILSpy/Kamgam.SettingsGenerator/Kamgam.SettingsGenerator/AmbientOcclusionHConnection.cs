using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using HTraceAO.Scripts.Globals;
using HTraceAO.Scripts.Infrastructure.URP;
using UnityEngine;
using UnityEngine.Rendering;

namespace Kamgam.SettingsGenerator;

public class AmbientOcclusionHConnection : ConnectionWithOptions<string>
{
	protected List<string> _labels;

	public override List<string> GetOptionLabels()
	{
		if (_labels == null)
		{
			List<string> labels = new List<string>();
			_labels = labels;
			if (_labels != null)
			{
				_labels.Add("High");
				if (_labels != null)
				{
					_labels.Add("Medium");
					if (_labels != null)
					{
						_labels.Add("Low");
						if (_labels != null)
						{
							_labels.Add("Off");
							goto IL_00df;
						}
					}
				}
			}
			return (List<string>)(object)new NullReferenceException();
		}
		goto IL_00df;
		IL_00df:
		return _labels;
	}

	public override void SetOptionLabels(List<string> optionLabels)
	{
		if (optionLabels != null && optionLabels._size == 4)
		{
			_labels = optionLabels;
		}
		else
		{
			Debug.LogError("Invalid new labels. Need to be four (high, medium, low, off).");
		}
	}

	public override void RefreshOptionLabels()
	{
		//IL_000c: Expected I, but got O
		//IL_001c: Expected O, but got I
		//IL_002c: Expected O, but got I
		_labels = null;
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.AmbientOcclusionHConnection>)+2C8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.AmbientOcclusionHConnection>)+2D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v10 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public unsafe override int Get()
	{
		//IL_0146: Expected I4, but got O
		HTraceAOVolume volume = GetVolume();
		if (volume != null)
		{
			if ((object)volume == null || volume.Enable == null)
			{
				goto IL_0138;
			}
			if (!volume.Enable.value)
			{
				return 3;
			}
			object obj = default(object);
			if (!(volume.AmbientOcclusionMode == (AmbientOcclusionMode)(int)(&obj)))
			{
				if (volume.AmbientOcclusionMode == (AmbientOcclusionMode)(int)(&obj))
				{
					return 2;
				}
			}
			else
			{
				if (volume.FullResolution == null)
				{
					goto IL_0138;
				}
				if (!volume.FullResolution.value)
				{
					return 1;
				}
			}
		}
		return 0;
		IL_0138:
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public override void Set(int index)
	{
		HTraceAOVolume volume = GetVolume();
		if (!(volume != null))
		{
			return;
		}
		BoolParameter boolParameter = new BoolParameter(value: true, overrideState: true);
		if (index != 4)
		{
			volume.Enable = boolParameter;
			BoolParameter fullResolution;
			if (index != 0)
			{
				AmbientOcclusionModeParameter ambientOcclusionMode;
				if (index != 1)
				{
					if (index != 2 && index != 3)
					{
						goto IL_01a6;
					}
					AmbientOcclusionMode value = default(AmbientOcclusionMode);
					ambientOcclusionMode = new AmbientOcclusionModeParameter(value, overrideState: true);
					value = AmbientOcclusionMode.SSAO;
				}
				else
				{
					ambientOcclusionMode = null;
					AmbientOcclusionMode value = AmbientOcclusionMode.GTAO;
				}
				volume.AmbientOcclusionMode = ambientOcclusionMode;
				fullResolution = null;
				bool flag = false;
				bool flag2 = true;
			}
			else
			{
				AmbientOcclusionModeParameter ambientOcclusionMode2 = new AmbientOcclusionModeParameter(AmbientOcclusionMode.GTAO, overrideState: true);
				volume.AmbientOcclusionMode = ambientOcclusionMode2;
				bool flag = default(bool);
				bool flag2 = default(bool);
				fullResolution = new BoolParameter(flag, flag2);
				flag = true;
				flag2 = true;
			}
			volume.FullResolution = fullResolution;
		}
		else
		{
			boolParameter._002Ector(value: false, overrideState: true);
			volume.Enable = boolParameter;
		}
		goto IL_01a6;
		IL_01a6:
		base.NotifyListenersIfChanged(index);
	}

	public override void OnQualityChanged(int qualityLevel)
	{
		Set(qualityLevel);
		base.OnQualityChanged(qualityLevel);
	}

	public HTraceAOVolume GetVolume()
	{
		if (Application.isPlaying)
		{
			VolumeManager instance = VolumeManager.instance;
			if (instance == null)
			{
				return (HTraceAOVolume)(object)new NullReferenceException();
			}
			if (instance._003Cstack_003Ek__BackingField != null)
			{
				return instance._003Cstack_003Ek__BackingField.GetComponent<HTraceAOVolume>();
			}
		}
		return null;
	}
}
