using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator;

public class RenderScaleConnection : Connection<float>
{
	public bool ReapplyOnQualityChange;

	public float DefaultRenderScale = 1f;

	[NonSerialized]
	protected float scale = -1f;

	public UniversalRenderPipelineAsset QualityRenderAsset
	{
		get
		{
			//IL_000d: Expected I, but got O
			//IL_001b: Expected I, but got O
			//IL_002b: Expected O, but got I
			//IL_0067: Expected O, but got I
			//IL_008c: Expected O, but got I4
			int qualityLevel = QualitySettings.GetQualityLevel();
			RenderPipelineAsset renderPipelineAssetAt = QualitySettings.GetRenderPipelineAssetAt(qualityLevel);
			bool flag = (object)renderPipelineAssetAt == null;
			UniversalRenderPipelineAsset result = null;
			if (flag)
			{
				goto IL_00e9;
			}
			nint num = (nint)renderPipelineAssetAt;
			nint num2 = (nint)typeof(UniversalRenderPipelineAsset);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ r8_v2 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r10_v2 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v42 @ r8_v2 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			UniversalRenderPipelineAsset universalRenderPipelineAsset;
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ r10_v2 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v88 @ rax_v10+FFFFFFF8+v43 @ rax_v6*8]");
				bool flag2 = 0 == (nint)typeof(UniversalRenderPipelineAsset);
				universalRenderPipelineAsset = (UniversalRenderPipelineAsset)1;
				if (flag2)
				{
					goto IL_00ee;
				}
			}
			universalRenderPipelineAsset = null;
			goto IL_00ee;
			IL_00ee:
			bool flag3 = (object)universalRenderPipelineAsset == null;
			result = null;
			if (!flag3)
			{
				result = (UniversalRenderPipelineAsset)renderPipelineAssetAt;
			}
			goto IL_00e9;
			IL_00e9:
			return result;
		}
	}

	public override float Get()
	{
		//IL_006b: Invalid comparison between I4 and F4
		if (0f > scale)
		{
			scale = DefaultRenderScale;
		}
		UniversalRenderPipelineAsset qualityRenderAsset = QualityRenderAsset;
		if (qualityRenderAsset != null)
		{
			UniversalRenderPipelineAsset qualityRenderAsset2 = QualityRenderAsset;
			scale = qualityRenderAsset2.m_RenderScale;
		}
		return scale;
	}

	public override void Set(float scale)
	{
		UniversalRenderPipelineAsset qualityRenderAsset = QualityRenderAsset;
		if (qualityRenderAsset != null)
		{
			UniversalRenderPipelineAsset qualityRenderAsset2 = QualityRenderAsset;
			qualityRenderAsset2.renderScale = scale;
		}
		this.scale = scale;
	}

	public override void OnQualityChanged(int qualityLevel)
	{
		if (ReapplyOnQualityChange)
		{
			Set(scale);
		}
		base.OnQualityChanged(qualityLevel);
	}
}
