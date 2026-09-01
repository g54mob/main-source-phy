using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator;

public class AmbientOcclusionConnection : Connection<bool>
{
	public const float OFF_INTENSITY = 0.001f;

	public static bool UseActiveStateToDisable;

	protected Dictionary<UniversalRenderPipelineAsset, float> _lastKnownIntensities;

	protected ScriptableRenderer getRenderer()
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ r8_v3 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ r9_v2 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ r8_v3 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ r9_v2 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rax_v16+FFFFFFF8+v60 @ rax_v12*8]");
				bool flag2 = 0 == (nint)typeof(UniversalRenderPipelineAsset);
				obj4 = (UnityEngine.Object)1;
				if (flag2)
				{
					goto IL_014c;
				}
			}
			obj4 = null;
			goto IL_014c;
		}
		goto IL_00dc;
		IL_014c:
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
			if ((object)obj != null)
			{
				return ((UniversalRenderPipelineAsset)obj).scriptableRenderer;
			}
			return (ScriptableRenderer)(object)new NullReferenceException();
		}
		return null;
	}

	private static float getIntensity(UniversalRenderPipelineAsset rpAsset)
	{
		//IL_0052: Expected F4, but got I4
		if (!(rpAsset != null))
		{
			return 0f;
		}
		ScriptableRendererFeature rendererFeature = UniversalRenderPipelineUtils.GetRendererFeature("ScreenSpaceAmbientOcclusion", rpAsset);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180770A50");
		float result = default(float);
		return result;
	}

	private static void setIntensity(UniversalRenderPipelineAsset rpAsset, float intensity)
	{
		if (rpAsset != null)
		{
			ScriptableRendererFeature rendererFeature = UniversalRenderPipelineUtils.GetRendererFeature("ScreenSpaceAmbientOcclusion", rpAsset);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807710C0");
		}
	}

	public override bool Get()
	{
		//IL_0038: Expected I, but got O
		//IL_0046: Expected I, but got O
		//IL_0056: Expected O, but got I
		//IL_0092: Expected O, but got I
		//IL_00b7: Expected O, but got I4
		//IL_01dd: Expected I4, but got O
		//IL_0174: Invalid comparison between F4 and I4
		RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
		bool flag = (object)currentRenderPipeline == null;
		UnityEngine.Object obj = null;
		UnityEngine.Object obj4;
		if (!flag)
		{
			nint num = (nint)currentRenderPipeline;
			nint num2 = (nint)typeof(UniversalRenderPipelineAsset);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r8_v7 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ r9_v2 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r8_v7 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ r9_v2 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v124 @ rax_v22+FFFFFFF8+v67 @ rax_v18*8]");
				bool flag2 = 0 == (nint)typeof(UniversalRenderPipelineAsset);
				obj4 = (UnityEngine.Object)1;
				if (flag2)
				{
					goto IL_01e7;
				}
			}
			obj4 = null;
			goto IL_01e7;
		}
		goto IL_00dc;
		IL_01e7:
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
			ScriptableRendererFeature rendererFeature = UniversalRenderPipelineUtils.GetRendererFeature("ScreenSpaceAmbientOcclusion", (UniversalRenderPipelineAsset)obj);
			if (!UseActiveStateToDisable)
			{
				updateLastKnownIntensity((UniversalRenderPipelineAsset)obj);
				float intensity = getIntensity((UniversalRenderPipelineAsset)obj);
				bool flag4 = intensity < 0.001f;
				float num4 = intensity - 0.001f;
				bool flag5 = num4 == 0f;
				bool flag6 = !flag4;
				bool flag7 = !flag5;
				return flag7 & flag6;
			}
			if ((object)rendererFeature != null)
			{
				return rendererFeature.m_Active;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return false;
	}

	public override void Set(bool enable)
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
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ r8_v17 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ r9_v8 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ r8_v17 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ r9_v8 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v127 @ rax_v43+FFFFFFF8+v70 @ rax_v39*8]");
				bool flag2 = 0 == (nint)typeof(UniversalRenderPipelineAsset);
				obj4 = (UnityEngine.Object)1;
				if (flag2)
				{
					goto IL_01fb;
				}
			}
			obj4 = null;
			goto IL_01fb;
		}
		goto IL_00dc;
		IL_01fb:
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
		ScriptableRendererFeature rendererFeature = UniversalRenderPipelineUtils.GetRendererFeature("ScreenSpaceAmbientOcclusion", (UniversalRenderPipelineAsset)obj);
		if (rendererFeature != null)
		{
			if (!UseActiveStateToDisable)
			{
				updateLastKnownIntensity((UniversalRenderPipelineAsset)obj);
				if (enable)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311C0");
				}
				if (obj != null)
				{
					ScriptableRendererFeature rendererFeature2 = UniversalRenderPipelineUtils.GetRendererFeature("ScreenSpaceAmbientOcclusion", (UniversalRenderPipelineAsset)obj);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807710C0");
				}
			}
			else
			{
				rendererFeature.m_Active = enable;
			}
		}
		base.NotifyListenersIfChanged(enable);
	}

	protected unsafe void updateLastKnownIntensity(UniversalRenderPipelineAsset rpAsset)
	{
		//IL_004c: Expected F4, but got Ref
		//IL_0038: Expected F4, but got Ref
		float intensity = getIntensity(rpAsset);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
		object obj = default(object);
		object obj2 = default(object);
		if (obj == null)
		{
			_lastKnownIntensities.set_Item(rpAsset, (float)(nint)(&obj2));
		}
		else
		{
			bool flag = _lastKnownIntensities.TryAdd(rpAsset, (nint)(&obj2));
		}
	}

	public AmbientOcclusionConnection()
	{
		Dictionary<UniversalRenderPipelineAsset, float> lastKnownIntensities = new Dictionary<UniversalRenderPipelineAsset, float>();
		_lastKnownIntensities = lastKnownIntensities;
		base._002Ector();
	}
}
