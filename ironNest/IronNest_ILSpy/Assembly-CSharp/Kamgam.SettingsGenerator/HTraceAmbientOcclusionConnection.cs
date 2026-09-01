using System;
using Cpp2ILInjected;
using HTraceAO.Scripts.Infrastructure.URP;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator;

public class HTraceAmbientOcclusionConnection : Connection<bool>
{
	public override bool Get()
	{
		//IL_0038: Expected I, but got O
		//IL_0046: Expected I, but got O
		//IL_0056: Expected O, but got I
		//IL_0092: Expected O, but got I
		//IL_00b7: Expected O, but got I4
		//IL_0171: Expected I4, but got O
		RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
		bool flag = (object)currentRenderPipeline == null;
		UnityEngine.Object obj = null;
		UnityEngine.Object obj4;
		if (!flag)
		{
			nint num = (nint)currentRenderPipeline;
			nint num2 = (nint)typeof(UniversalRenderPipelineAsset);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ r8_v6 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ r9_v2 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ r8_v6 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ r9_v2 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ rax_v23+FFFFFFF8+v64 @ rax_v19*8]");
				bool flag2 = 0 == (nint)typeof(UniversalRenderPipelineAsset);
				obj4 = (UnityEngine.Object)1;
				if (flag2)
				{
					goto IL_017b;
				}
			}
			obj4 = null;
			goto IL_017b;
		}
		goto IL_00dc;
		IL_017b:
		bool flag3 = (object)obj4 == null;
		obj = null;
		if (!flag3)
		{
			obj = currentRenderPipeline;
		}
		goto IL_00dc;
		IL_00dc:
		if (obj != null)
		{
			HTraceAORendererFeature rendererFeature = UniversalRenderPipelineUtils.GetRendererFeature<HTraceAORendererFeature>((UniversalRenderPipelineAsset)obj);
			if (rendererFeature != null)
			{
				if ((object)rendererFeature != null)
				{
					return ((ScriptableRendererFeature)rendererFeature).m_Active;
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
		}
		return false;
	}

	public override void Set(bool value)
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r8_v6 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ r9_v2 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r8_v6 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ r9_v2 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ rax_v21+FFFFFFF8+v67 @ rax_v17*8]");
				bool flag2 = 0 == (nint)typeof(UniversalRenderPipelineAsset);
				obj4 = (UnityEngine.Object)1;
				if (flag2)
				{
					goto IL_0152;
				}
			}
			obj4 = null;
			goto IL_0152;
		}
		goto IL_00dc;
		IL_0152:
		bool flag3 = (object)obj4 == null;
		obj = null;
		if (!flag3)
		{
			obj = currentRenderPipeline;
		}
		goto IL_00dc;
		IL_00dc:
		if (obj != null)
		{
			HTraceAORendererFeature rendererFeature = UniversalRenderPipelineUtils.GetRendererFeature<HTraceAORendererFeature>((UniversalRenderPipelineAsset)obj);
			if (rendererFeature != null)
			{
				((ScriptableRendererFeature)rendererFeature).m_Active = value;
			}
		}
	}
}
