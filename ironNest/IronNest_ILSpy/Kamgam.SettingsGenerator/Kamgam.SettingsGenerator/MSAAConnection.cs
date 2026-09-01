using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator;

public class MSAAConnection : ConnectionWithOptions<string>
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
				_labels.Add("Disabled");
				if (_labels != null)
				{
					_labels.Add("2x");
					if (_labels != null)
					{
						_labels.Add("4x");
						if (_labels != null)
						{
							_labels.Add("8x");
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
			Debug.LogError("Invalid new labels. Need to be four (disabled, 2, 4, 8).");
		}
	}

	public override void RefreshOptionLabels()
	{
		//IL_000c: Expected I, but got O
		//IL_001c: Expected O, but got I
		//IL_002c: Expected O, but got I
		_labels = null;
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.MSAAConnection>)+2C8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.MSAAConnection>)+2D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v10 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override int Get()
	{
		//IL_0040: Expected I, but got O
		//IL_004e: Expected I, but got O
		//IL_005e: Expected O, but got I
		//IL_009a: Expected O, but got I
		//IL_00bf: Expected O, but got I4
		//IL_01ee: Expected I4, but got O
		RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
		UnityEngine.Object obj;
		if ((object)currentRenderPipeline == null)
		{
			obj = null;
			goto IL_00e4;
		}
		nint num = (nint)currentRenderPipeline;
		nint num2 = (nint)typeof(UniversalRenderPipelineAsset);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ r8_v3 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ r9_v2 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ r8_v3 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ r9_v2 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ rax_v20+FFFFFFF8+v64 @ rax_v16*8]");
			bool flag = 0 == (nint)typeof(UniversalRenderPipelineAsset);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_01f8;
			}
		}
		obj4 = null;
		goto IL_01f8;
		IL_01f8:
		bool flag2 = (object)obj4 == null;
		obj = null;
		if (!flag2)
		{
			obj = currentRenderPipeline;
		}
		goto IL_00e4;
		IL_00e4:
		if (obj != null)
		{
			if ((object)obj == null)
			{
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rbx_v1 (UnityEngine.Object)+5C]");
			if ((nint)0 > (nint)1)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rbx_v1 (UnityEngine.Object)+5C]");
				if ((nint)0 != 2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rbx_v1 (UnityEngine.Object)+5C]");
					if ((nint)0 != 4)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rbx_v1 (UnityEngine.Object)+5C]");
						bool flag3 = (nint)0 >= (nint)8;
						int result = 3;
						if (!flag3)
						{
							result = 0;
						}
						return result;
					}
					return 2;
				}
				return 1;
			}
		}
		return 0;
	}

	public override void Set(int index)
	{
		//IL_0038: Expected I, but got O
		//IL_0046: Expected I, but got O
		//IL_0056: Expected O, but got I
		//IL_0092: Expected O, but got I
		//IL_00b7: Expected O, but got I4
		RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
		bool flag = (object)currentRenderPipeline == null;
		UnityEngine.Object obj = null;
		UnityEngine.Object obj4;
		if (!flag)
		{
			nint num = (nint)currentRenderPipeline;
			nint num2 = (nint)typeof(UniversalRenderPipelineAsset);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ r8_v7 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ r9_v2 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ r8_v7 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ r9_v2 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v121 @ rax_v17+FFFFFFF8+v66 @ rax_v13*8]");
				bool flag2 = 0 == (nint)typeof(UniversalRenderPipelineAsset);
				obj4 = (UnityEngine.Object)1;
				if (flag2)
				{
					goto IL_01a5;
				}
			}
			obj4 = null;
			goto IL_01a5;
		}
		goto IL_00dc;
		IL_01a5:
		bool flag3 = (object)obj4 == null;
		obj = null;
		if (!flag3)
		{
			obj = currentRenderPipeline;
		}
		goto IL_00dc;
		IL_00dc:
		if (!(obj != null))
		{
			return;
		}
		if (index > 0)
		{
			if (index != 1)
			{
				if (index != 2)
				{
					if (index >= 3)
					{
						_ = 8;
					}
				}
				else
				{
					_ = 4;
				}
			}
			else
			{
				_ = 2;
			}
		}
		else
		{
			_ = 1;
		}
		base.NotifyListenersIfChanged(index);
	}
}
