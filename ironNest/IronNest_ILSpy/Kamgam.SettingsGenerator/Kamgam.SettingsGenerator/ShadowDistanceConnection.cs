using System;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator;

public class ShadowDistanceConnection : ConnectionWithOptions<string>
{
	public List<float> QualityDistances;

	public bool UseQualitySettingsAsFallback;

	protected List<float> _distancesFromSettings;

	protected List<string> _labels;

	public ShadowDistanceConnection(List<float> qualityDistances, bool useQualitySettingsAsFallback = true)
	{
		QualityDistances = qualityDistances;
		UseQualitySettingsAsFallback = useQualitySettingsAsFallback;
	}

	public override List<string> GetOptionLabels()
	{
		if (_labels == null)
		{
			string[] names = QualitySettings.names;
			List<string> labels = Enumerable.ToList(names);
			_labels = labels;
		}
		return _labels;
	}

	public override void SetOptionLabels(List<string> optionLabels)
	{
		List<float> distances = getDistances();
		if (optionLabels != null)
		{
			int size = optionLabels._size;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rax_v2 (System.Collections.Generic.List`1<System.Single>)+18]");
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
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.ShadowDistanceConnection>)+2C8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.ShadowDistanceConnection>)+2D0]");
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
		//IL_01cc: Expected I4, but got O
		//IL_01a0: Expected O, but got I4
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rax_v23+FFFFFFF8+v71 @ rax_v19*8]");
			bool flag = 0 == (nint)typeof(UniversalRenderPipelineAsset);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_01d6;
			}
		}
		obj4 = null;
		goto IL_01d6;
		IL_01d6:
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
			List<float> distances = getDistances();
			bool flag3 = distances == null;
			int num4 = 0;
			UnityEngine.Object obj5 = null;
			if (!flag3)
			{
				object obj7 = default(object);
				while (true)
				{
					UnityEngine.Object obj6 = obj5;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ rax_v8 (System.Collections.Generic.List`1<System.Single>)+18]");
					if ((nint)obj6 < 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						if ((object)obj == null)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ rdi_v1 (UnityEngine.Object)+BC]");
						bool flag4 = obj7 == null;
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180A37551h\"");
						if (!flag4)
						{
							num4++;
							obj5 = (UnityEngine.Object)num4;
							continue;
						}
						return num4;
					}
					return QualitySettings.GetQualityLevel();
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		return 0;
	}

	private unsafe List<float> getDistances()
	{
		//IL_011c: Expected I, but got O
		//IL_0124: Expected I, but got O
		//IL_0134: Expected O, but got I
		//IL_0170: Expected O, but got I
		//IL_01cc: Expected F4, but got Ref
		if (UseQualitySettingsAsFallback)
		{
			if (QualityDistances != null)
			{
				List<float> qualityDistances = QualityDistances;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v16 (System.Collections.Generic.List`1<System.Single>)+18]");
				if ((nint)0 > (nint)0)
				{
					goto IL_01ff;
				}
			}
			if (_distancesFromSettings == null)
			{
				List<float> distancesFromSettings = new List<float>();
				_distancesFromSettings = distancesFromSettings;
				string[] names = QualitySettings.names;
				if (names != null)
				{
					bool flag = names.Length <= 0;
					int num = 0;
					if (flag)
					{
						goto IL_01f8;
					}
					object obj3 = default(object);
					while (true)
					{
						RenderPipelineAsset renderPipelineAssetAt = QualitySettings.GetRenderPipelineAssetAt(num);
						if ((object)renderPipelineAssetAt == null)
						{
							break;
						}
						nint num2 = (nint)typeof(UniversalRenderPipelineAsset);
						nint num3 = (nint)renderPipelineAssetAt;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v237 @ r8_v4 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
						object obj = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r9_v4 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
						nint num4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v237 @ r8_v4 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
						if (num4 < 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r9_v4 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
						object obj2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v245 @ rcx_v11+FFFFFFF8+v244 @ rcx_v10*8]");
						if (0 != (nint)typeof(UniversalRenderPipelineAsset) || _distancesFromSettings == null)
						{
							break;
						}
						_distancesFromSettings.Add((nint)(&obj3));
						num++;
						if (num < names.Length)
						{
							continue;
						}
						goto IL_01f8;
					}
				}
				return (List<float>)(object)new NullReferenceException();
			}
			goto IL_01f8;
		}
		goto IL_01ff;
		IL_01ff:
		return QualityDistances;
		IL_01f8:
		return _distancesFromSettings;
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ r8_v10 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ r9_v6 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ r8_v10 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ r9_v6 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v125 @ rax_v21+FFFFFFF8+v70 @ rax_v17*8]");
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
		List<float> distances = getDistances();
		if (distances != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ rax_v8 (System.Collections.Generic.List`1<System.Single>)+18]");
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ rax_v8 (System.Collections.Generic.List`1<System.Single>)+18]");
				if ((nint)0 <= (nint)index)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ rax_v8 (System.Collections.Generic.List`1<System.Single>)+18]");
					int num4 = (int)(-1);
				}
				else
				{
					int num4 = index;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				float shadowDistance = default(float);
				((UniversalRenderPipelineAsset)obj).shadowDistance = shadowDistance;
			}
		}
		base.NotifyListenersIfChanged(index);
	}
}
