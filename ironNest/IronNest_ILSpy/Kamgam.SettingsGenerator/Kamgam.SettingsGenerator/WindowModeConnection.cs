using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class WindowModeConnection : ConnectionWithOptions<string>
{
	protected List<FullScreenMode> _values;

	protected List<string> _labels;

	protected FullScreenMode? lastKnownMode;

	protected int lastSetFrame;

	public override List<string> GetOptionLabels()
	{
		if (CollectionExtensions.IsNullOrEmpty(_labels))
		{
			List<string> labels = new List<string>();
			_labels = labels;
			if (_labels != null)
			{
				_labels.Add("Full Screen");
				if (_labels != null)
				{
					_labels.Add("Window");
					if (_labels != null)
					{
						_labels.Add("Exclusive");
						goto IL_00b0;
					}
				}
			}
			return (List<string>)(object)new NullReferenceException();
		}
		goto IL_00b0;
		IL_00b0:
		return _labels;
	}

	public override void SetOptionLabels(List<string> optionLabels)
	{
		List<FullScreenMode> windowOptions = getWindowOptions();
		if (optionLabels != null)
		{
			int size = optionLabels._size;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rax_v3 (System.Collections.Generic.List`1<UnityEngine.FullScreenMode>)+18]");
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
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.WindowModeConnection>)+2C8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.WindowModeConnection>)+2D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v10 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	protected unsafe virtual List<FullScreenMode> getWindowOptions()
	{
		if (CollectionExtensions.IsNullOrEmpty(_values))
		{
			List<FullScreenMode> values = new List<FullScreenMode>();
			_values = values;
			if (_values != null)
			{
				object obj = default(object);
				_values.Add((FullScreenMode)(int)(&obj));
				if (_values != null)
				{
					_values.Add((FullScreenMode)(int)(&obj));
					if (_values != null)
					{
						_values.Add((FullScreenMode)(int)(&obj));
						goto IL_00ad;
					}
				}
			}
			return (List<FullScreenMode>)(object)new NullReferenceException();
		}
		goto IL_00ad;
		IL_00ad:
		return _values;
	}

	public override int Get()
	{
		//IL_0110: Expected O, but got I4
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0010: Expected O, but got I4
		//IL_00f8: Expected I4, but got O
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		int frameCount = Time.frameCount;
		object obj = frameCount - lastSetFrame;
		if ((nint)obj > 3)
		{
			lastKnownMode = (FullScreenMode?)(object)0;
		}
		FullScreenMode fullScreenMode = Screen.fullScreenMode;
		object obj2 = this + 56;
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj3 = default(object);
		bool flag = obj3 == null;
		FullScreenMode fullScreenMode2 = fullScreenMode;
		FullScreenMode fullScreenMode3 = default(FullScreenMode);
		if (!flag)
		{
			object obj4 = this + 56;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
			fullScreenMode2 = fullScreenMode3;
		}
		List<FullScreenMode> windowOptions = getWindowOptions();
		bool flag2 = windowOptions == null;
		int num2 = 0;
		int num3 = 0;
		if (!flag2)
		{
			while (true)
			{
				int num4 = num2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rax_v12 (System.Collections.Generic.List`1<UnityEngine.FullScreenMode>)+18]");
				if ((nint)num4 >= (nint)0)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if (fullScreenMode3 != fullScreenMode2)
				{
					num3++;
					num2 = num3;
					continue;
				}
				return num3;
			}
			return 0;
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public unsafe override void Set(int index)
	{
		//IL_0190: Expected O, but got I
		//IL_0156: Expected O, but got I4
		List<FullScreenMode> windowOptions = getWindowOptions();
		int num2;
		if (index >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rax_v3 (System.Collections.Generic.List`1<UnityEngine.FullScreenMode>)+18]");
			int num = (int)(-1);
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
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rax_v3 (System.Collections.Generic.List`1<UnityEngine.FullScreenMode>)+18]");
		object obj = -1;
		ScreenOrchestrator instance;
		FullScreenMode fullScreenMode;
		FullScreenMode fullScreenMode2 = default(FullScreenMode);
		if (num2 != (nint)obj)
		{
			instance = ScreenOrchestrator.Instance;
			fullScreenMode = fullScreenMode2;
		}
		else
		{
			RuntimePlatform platform = Application.platform;
			if (platform != RuntimePlatform.OSXPlayer)
			{
				RuntimePlatform platform2 = Application.platform;
				if (platform2 != RuntimePlatform.WindowsPlayer)
				{
					goto IL_0126;
				}
				instance = ScreenOrchestrator.Instance;
				fullScreenMode = FullScreenMode.ExclusiveFullScreen;
			}
			else
			{
				instance = ScreenOrchestrator.Instance;
				fullScreenMode = FullScreenMode.MaximizedWindow;
			}
		}
		instance.RequestFullScreenMode(fullScreenMode);
		goto IL_0126;
		IL_0126:
		int frameCount = Time.frameCount;
		lastSetFrame = frameCount;
		FullScreenMode? fullScreenMode3 = (FullScreenMode)(int)(&fullScreenMode2);
		lastKnownMode = (FullScreenMode?)(object)0;
		base.NotifyListenersIfChanged(num2);
	}
}
