using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class MeshQualityConnection : ConnectionWithOptions<string>
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
				_labels.Add("Ultra");
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
								_labels.Add("VeryLow");
								goto IL_010e;
							}
						}
					}
				}
			}
			return (List<string>)(object)new NullReferenceException();
		}
		goto IL_010e;
		IL_010e:
		return _labels;
	}

	public override void SetOptionLabels(List<string> optionLabels)
	{
		if (optionLabels != null && optionLabels._size == 5)
		{
			_labels = optionLabels;
		}
		else
		{
			Debug.LogError("Invalid new labels. Need to be five (ultra, high, medium, low, veryLow).");
		}
	}

	public override void RefreshOptionLabels()
	{
		//IL_000c: Expected I, but got O
		//IL_001c: Expected O, but got I
		//IL_002c: Expected O, but got I
		_labels = null;
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.MeshQualityConnection>)+2C8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.MeshQualityConnection>)+2D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v10 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override int Get()
	{
		if (QualitySettings.maximumLODLevel != 0)
		{
			float lodBias = QualitySettings.lodBias;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj = default(object);
			if (obj != null)
			{
				return 3;
			}
			float lodBias2 = QualitySettings.lodBias;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj2 = default(object);
			if (obj2 != null)
			{
				return 4;
			}
		}
		else
		{
			float lodBias3 = QualitySettings.lodBias;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
			object obj3 = default(object);
			if (obj3 == null)
			{
				float lodBias4 = QualitySettings.lodBias;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
				object obj4 = default(object);
				if (obj4 != null)
				{
					return 1;
				}
				float lodBias5 = QualitySettings.lodBias;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
				object obj5 = default(object);
				if (obj5 != null)
				{
					return 2;
				}
			}
		}
		return 0;
	}

	public override void Set(int index)
	{
		//IL_002b: Expected O, but got I4
		//IL_0132: Expected O, but got I4
		//IL_00ed: Expected I, but got O
		//IL_00fd: Expected O, but got I
		//IL_010d: Expected O, but got I
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected I4, but got Unknown
		bool flag = index == 0;
		int maximumLODLevel = default(int);
		float lodBias = default(float);
		if (!flag)
		{
			object obj = index - 1;
			if (!flag)
			{
				object obj2 = obj - 1;
				if (!flag)
				{
					int num = obj2 - 1;
					if (!flag)
					{
						if (num != 1)
						{
							goto IL_00e8;
						}
						maximumLODLevel = num;
						lodBias = 0.4f;
					}
					else
					{
						maximumLODLevel = 1;
						lodBias = 0.8f;
					}
					goto IL_0117;
				}
				lodBias = 1f;
			}
			else
			{
				lodBias = 1.5f;
			}
		}
		else
		{
			lodBias = 2f;
		}
		maximumLODLevel = 0;
		goto IL_0117;
		IL_00e8:
		nint num2 = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r8_v6 (Il2CppClass<Kamgam.SettingsGenerator.MeshQualityConnection>)+258]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r8_v6 (Il2CppClass<Kamgam.SettingsGenerator.MeshQualityConnection>)+260]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v83 @ rax_v3 (should have been resolved before IL gen)");
		goto IL_0117;
		IL_0117:
		QualitySettings.SetLODSettings(lodBias, maximumLODLevel);
		object obj5 = 0;
		goto IL_00e8;
	}

	public override void OnQualityChanged(int qualityLevel)
	{
		Set(qualityLevel);
		base.OnQualityChanged(qualityLevel);
	}
}
