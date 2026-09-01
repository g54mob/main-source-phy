using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator;

public class ShadowResolutionConnection : ConnectionWithOptions<string>
{
	public static bool SetAdditionalLightResolution = true;

	public static int AdditionalToMainResolutionFactor = 4;

	protected List<int> _values;

	protected List<string> _labels;

	private static void setResolution(UniversalRenderPipelineAsset asset, int resolution)
	{
		UniversalRenderPipelineUtils.SetMainLightShadowResolution(resolution, asset);
		if (SetAdditionalLightResolution)
		{
			int num = resolution / AdditionalToMainResolutionFactor;
			bool flag = num <= 256;
			int value = 256;
			if (!flag)
			{
				value = num;
			}
			UniversalRenderPipelineUtils.SetAdditionalLightShadowResolution(value, asset);
		}
	}

	public override List<string> GetOptionLabels()
	{
		if (_labels == null)
		{
			List<string> list = new List<string>();
			if (list == null)
			{
				return (List<string>)(object)new NullReferenceException();
			}
			list.Add("Low");
			list.Add("Mid");
			list.Add("High");
			list.Add("Very High");
			list.Add("Ultra");
			_labels = list;
		}
		return _labels;
	}

	public override void SetOptionLabels(List<string> optionLabels)
	{
		List<int> resolutions = getResolutions();
		string text;
		string text2;
		if (optionLabels != null)
		{
			int size = optionLabels._size;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v34 @ rax_v2 (System.Collections.Generic.List`1<System.Int32>)+18]");
			if ((nint)size == 0)
			{
				_labels = optionLabels;
				return;
			}
		}
		else if (resolutions == null)
		{
			text = null;
			text2 = "Invalid new labels. Need to be ";
			goto IL_00b8;
		}
		text = resolutions.ToString();
		text2 = "Invalid new labels. Need to be ";
		goto IL_00b8;
		IL_00b8:
		string message = text2 + text + ".";
		Debug.LogError(message);
	}

	public override void RefreshOptionLabels()
	{
		//IL_000c: Expected I, but got O
		//IL_001c: Expected O, but got I
		//IL_002c: Expected O, but got I
		_labels = null;
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.ShadowResolutionConnection>)+2C8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.ShadowResolutionConnection>)+2D0]");
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
		//IL_0201: Expected I4, but got O
		//IL_0196: Expected O, but got I4
		RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
		UnityEngine.Object obj;
		if ((object)currentRenderPipeline == null)
		{
			obj = null;
			goto IL_00e4;
		}
		nint num = (nint)currentRenderPipeline;
		nint num2 = (nint)typeof(UniversalRenderPipelineAsset);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ r8_v7 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ r9_v6 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ r8_v7 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ r9_v6 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rax_v25+FFFFFFF8+v71 @ rax_v21*8]");
			bool flag = 0 == (nint)typeof(UniversalRenderPipelineAsset);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_020b;
			}
		}
		obj4 = null;
		goto IL_020b;
		IL_020b:
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
			List<int> resolutions = getResolutions();
			bool flag3 = resolutions == null;
			int num4 = 0;
			UnityEngine.Object obj5 = null;
			if (!flag3)
			{
				object obj7 = default(object);
				while (true)
				{
					UnityEngine.Object obj6 = obj5;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ rax_v8 (System.Collections.Generic.List`1<System.Int32>)+18]");
					if ((nint)obj6 < 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						if ((object)obj == null)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ rdi_v1 (UnityEngine.Object)+98]");
						if (obj7 != null)
						{
							num4++;
							obj5 = (UnityEngine.Object)num4;
							continue;
						}
						return num4;
					}
					int qualityLevel = QualitySettings.GetQualityLevel();
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ rax_v8 (System.Collections.Generic.List`1<System.Int32>)+18]");
					int result = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ rax_v8 (System.Collections.Generic.List`1<System.Int32>)+18]");
					if ((nint)qualityLevel < (nint)0)
					{
						result = qualityLevel;
					}
					return result;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		return 0;
	}

	private unsafe List<int> getResolutions()
	{
		if (_values == null)
		{
			List<int> list = new List<int>();
			if (list == null)
			{
				return (List<int>)(object)new NullReferenceException();
			}
			object obj = default(object);
			list.Add((int)(&obj));
			list.Add((int)(&obj));
			list.Add((int)(&obj));
			list.Add((int)(&obj));
			list.Add((int)(&obj));
			_values = list;
		}
		return _values;
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ r8_v9 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ r9_v5 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ r8_v9 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ r9_v5 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rax_v21+FFFFFFF8+v70 @ rax_v17*8]");
				bool flag2 = 0 == (nint)typeof(UniversalRenderPipelineAsset);
				obj4 = (UnityEngine.Object)1;
				if (flag2)
				{
					goto IL_01be;
				}
			}
			obj4 = null;
			goto IL_01be;
		}
		goto IL_00dc;
		IL_01be:
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
		List<int> resolutions = getResolutions();
		if (resolutions != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ rax_v8 (System.Collections.Generic.List`1<System.Int32>)+18]");
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ rax_v8 (System.Collections.Generic.List`1<System.Int32>)+18]");
				if ((nint)0 <= (nint)index)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ rax_v8 (System.Collections.Generic.List`1<System.Int32>)+18]");
					int num4 = (int)(-1);
				}
				else
				{
					int num4 = index;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				int resolution = default(int);
				setResolution((UniversalRenderPipelineAsset)obj, resolution);
			}
		}
		base.NotifyListenersIfChanged(index);
	}
}
