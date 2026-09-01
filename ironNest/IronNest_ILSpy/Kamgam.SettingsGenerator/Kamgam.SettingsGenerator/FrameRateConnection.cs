using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class FrameRateConnection : ConnectionWithOptions<string>
{
	public bool RemoveUnlimited;

	public List<int> CustomFrameRates;

	public List<int> _values;

	public List<string> _labels;

	protected unsafe List<int> getFrameRates()
	{
		//IL_0237: Expected O, but got I4
		if (_values != null)
		{
			goto IL_02c7;
		}
		List<int> values = new List<int>();
		_values = values;
		List<int> values2;
		if (CustomFrameRates == null)
		{
			values2 = _values;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj = default(object);
			bool flag = obj != null;
			values2 = _values;
			if (flag)
			{
				if (_values != null)
				{
					_values.AddRange(CustomFrameRates);
					goto IL_0271;
				}
				goto IL_02ce;
			}
		}
		object obj2 = default(object);
		if (values2 != null)
		{
			values2.Add((int)(&obj2));
			if (_values != null)
			{
				_values.Add((int)(&obj2));
				if (_values != null)
				{
					_values.Add((int)(&obj2));
					if (_values != null)
					{
						_values.Add((int)(&obj2));
						if (_values != null)
						{
							_values.Add((int)(&obj2));
							if (_values != null)
							{
								_values.Add((int)(&obj2));
								if (_values != null)
								{
									_values.Add((int)(&obj2));
									if (_values != null)
									{
										_values.Add((int)(&obj2));
										if (_values != null)
										{
											_values.Add((int)(&obj2));
											if (_values != null)
											{
												_values.Add((int)(&obj2));
												obj2 = 240;
												goto IL_0271;
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_02ce;
		IL_02c7:
		return _values;
		IL_02ce:
		return (List<int>)(object)new NullReferenceException();
		IL_0271:
		if (RemoveUnlimited)
		{
			if (_values == null)
			{
				goto IL_02ce;
			}
			bool flag2 = _values.Remove((int)(&obj2));
		}
		goto IL_02c7;
	}

	public override List<string> GetOptionLabels()
	{
		if (_labels == null)
		{
			List<string> labels = new List<string>();
			_labels = labels;
			List<int> frameRates = getFrameRates();
			bool flag = frameRates == null;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (flag)
			{
				goto IL_0136;
			}
			object obj = default(object);
			int num5 = default(int);
			while (true)
			{
				int num4 = num3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v153 @ rax_v7 (System.Collections.Generic.List`1<System.Int32>)+18]");
				if ((nint)num4 >= (nint)0)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				List<string> labels2;
				string item;
				if ((nint)obj >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					string text = num2.ToString();
					string text2 = text + " fps";
					labels2 = _labels;
					if (_labels != null)
					{
						num2 = num5;
						item = text2;
						goto IL_0166;
					}
				}
				else
				{
					labels2 = _labels;
					if (_labels != null)
					{
						item = "Unlimited";
						goto IL_0166;
					}
				}
				goto IL_0136;
				IL_0166:
				labels2.Add(item);
				num++;
				num3 = num;
			}
		}
		return _labels;
		IL_0136:
		return (List<string>)(object)new NullReferenceException();
	}

	public override void SetOptionLabels(List<string> optionLabels)
	{
		List<int> frameRates = getFrameRates();
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
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.FrameRateConnection>)+2C8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.FrameRateConnection>)+2D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v10 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override int Get()
	{
		//IL_0094: Expected I4, but got O
		List<int> frameRates = getFrameRates();
		bool flag = frameRates == null;
		int num = 0;
		int num2 = 0;
		if (!flag)
		{
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
				int targetFrameRate = Application.targetFrameRate;
				if ((nint)obj != targetFrameRate)
				{
					num2++;
					num = num2;
					continue;
				}
				return num2;
			}
			return 0;
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public override void Set(int index)
	{
		//IL_0088: Expected I, but got O
		//IL_0098: Expected O, but got I
		//IL_00a8: Expected O, but got I
		int targetFrameRate = default(int);
		while (true)
		{
			List<int> frameRates = getFrameRates();
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
			Application.targetFrameRate = targetFrameRate;
			nint num3 = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v136 @ r8_v2 (Il2CppClass<Kamgam.SettingsGenerator.FrameRateConnection>)+258]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v136 @ r8_v2 (Il2CppClass<Kamgam.SettingsGenerator.FrameRateConnection>)+260]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v102 @ rax_v7 (should have been resolved before IL gen)");
		}
	}
}
