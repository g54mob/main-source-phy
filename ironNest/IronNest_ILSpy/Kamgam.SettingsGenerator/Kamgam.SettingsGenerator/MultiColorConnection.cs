using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class MultiColorConnection : ConnectionWithOptions<Color>
{
	private List<Color> _colors;

	private int _selectedIndex;

	public unsafe MultiColorConnection(int selectedIndex)
	{
		//IL_0012: Expected O, but got Ref
		//IL_001f: Expected O, but got Ref
		//IL_0031: Expected O, but got Ref
		//IL_003e: Expected O, but got Ref
		//IL_0050: Expected O, but got Ref
		base._002Ector();
		object obj = default(object);
		object obj2 = default(object);
		object obj3 = default(object);
		object obj4 = default(object);
		object obj5 = default(object);
		_colors = new List<Color>
		{
			(Color)(&obj),
			(Color)(&obj2),
			(Color)(&obj3),
			(Color)(&obj4),
			(Color)(&obj5)
		};
		_selectedIndex = selectedIndex;
	}

	public override void SetOptionLabels(List<Color> colors)
	{
		if (colors == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		object obj = default(object);
		if (obj != null)
		{
			List<Color> colors2 = new List<Color>(colors);
			_colors = colors2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [colors @ rdx (System.Collections.Generic.List`1<UnityEngine.Color>)+18]");
			int num = (int)(-1);
			if (num >= _selectedIndex)
			{
				num = _selectedIndex;
			}
			_selectedIndex = num;
		}
	}

	public override void RefreshOptionLabels()
	{
		//IL_000c: Expected I, but got O
		//IL_001c: Expected O, but got I
		//IL_002c: Expected O, but got I
		_colors = null;
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.MultiColorConnection>)+2C8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.MultiColorConnection>)+2D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v10 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override int Get()
	{
		return _selectedIndex;
	}

	public override List<Color> GetOptionLabels()
	{
		return _colors;
	}

	public override void Set(int selectedIndex)
	{
		//IL_0005: Expected I, but got O
		//IL_001f: Expected O, but got I
		//IL_002f: Expected O, but got I
		nint num = (nint)this;
		_selectedIndex = selectedIndex;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.MultiColorConnection>)+258]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.MultiColorConnection>)+260]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v3 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}
}
