using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class TextureResolutionConnection : ConnectionWithOptions<string>
{
	protected List<string> _labels;

	protected List<int> _values;

	protected unsafe List<int> getValues()
	{
		if (!CollectionExtensions.IsNullOrEmpty(_values))
		{
			goto IL_012c;
		}
		List<int> values = new List<int>();
		_values = values;
		if (_values != null)
		{
			object obj = default(object);
			_values.Add((int)(&obj));
			if (_values != null)
			{
				_values.Add((int)(&obj));
				if (_values != null)
				{
					_values.Add((int)(&obj));
					if (_values != null)
					{
						_values.Add((int)(&obj));
						if (QualitySettingUtils.AreQualitiesOrderedLowToHigh())
						{
							if (_values == null)
							{
								goto IL_0133;
							}
							_values.Reverse();
						}
						goto IL_012c;
					}
				}
			}
		}
		goto IL_0133;
		IL_0133:
		return (List<int>)(object)new NullReferenceException();
		IL_012c:
		return _values;
	}

	public override List<string> GetOptionLabels()
	{
		if (!CollectionExtensions.IsNullOrEmpty(_labels))
		{
			goto IL_0130;
		}
		List<string> labels = new List<string>();
		_labels = labels;
		if (_labels != null)
		{
			_labels.Add("Full Resolution");
			if (_labels != null)
			{
				_labels.Add("Half Resolution");
				if (_labels != null)
				{
					_labels.Add("Quater Resolution");
					if (_labels != null)
					{
						_labels.Add("Eighth Resolution");
						if (QualitySettingUtils.AreQualitiesOrderedLowToHigh())
						{
							if (_labels == null)
							{
								goto IL_0137;
							}
							_labels.Reverse();
						}
						goto IL_0130;
					}
				}
			}
		}
		goto IL_0137;
		IL_0137:
		return (List<string>)(object)new NullReferenceException();
		IL_0130:
		return _labels;
	}

	public override void SetOptionLabels(List<string> optionLabels)
	{
		List<int> values = getValues();
		if (optionLabels != null)
		{
			int size = optionLabels._size;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rax_v2 (System.Collections.Generic.List`1<System.Int32>)+18]");
			if ((nint)size == 0)
			{
				List<string> labels = new List<string>(optionLabels);
				_labels = labels;
				return;
			}
		}
		int num = default(int);
		string text = num.ToString();
		string message = "Invalid new labels. Need to be " + text + ".";
		Debug.LogError(message);
	}

	public override void RefreshOptionLabels()
	{
		//IL_000c: Expected I, but got O
		//IL_001c: Expected O, but got I
		//IL_002c: Expected O, but got I
		_labels = null;
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.TextureResolutionConnection>)+2C8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.TextureResolutionConnection>)+2D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v10 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override int Get()
	{
		//IL_006d: Expected I4, but got O
		List<int> values = getValues();
		bool flag = values == null;
		int num = 0;
		int num2 = 0;
		if (flag)
		{
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		object obj = default(object);
		while (true)
		{
			int num3 = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rax_v2 (System.Collections.Generic.List`1<System.Int32>)+18]");
			if ((nint)num3 >= (nint)0)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			int globalTextureMipmapLimit = QualitySettings.globalTextureMipmapLimit;
			if ((nint)obj != globalTextureMipmapLimit)
			{
				num2++;
				num = num2;
				continue;
			}
			return num2;
		}
		return 0;
	}

	public override void Set(int index)
	{
		//IL_00a1: Expected I, but got O
		//IL_00b1: Expected O, but got I
		//IL_00c1: Expected O, but got I
		List<int> values = getValues();
		if (index >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v34 @ rax_v2 (System.Collections.Generic.List`1<System.Int32>)+18]");
			int num = (int)(-1);
			bool flag = index <= num;
			int num2 = index;
			if (!flag)
			{
				num2 = num;
			}
		}
		else
		{
			int num2 = 0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		int globalTextureMipmapLimit = default(int);
		QualitySettings.globalTextureMipmapLimit = globalTextureMipmapLimit;
		nint num3 = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ r8_v2 (Il2CppClass<Kamgam.SettingsGenerator.TextureResolutionConnection>)+258]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ r8_v2 (Il2CppClass<Kamgam.SettingsGenerator.TextureResolutionConnection>)+260]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v100 @ rax_v6 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}
}
