using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator;

public class QualityPreset
{
	public int particleRaycastBudget;

	public bool softVegetation;

	public int vSyncCount;

	public int antiAliasing;

	public int asyncUploadTimeSlice;

	public int asyncUploadBufferSize;

	public bool asyncUploadPersistentBuffer;

	public bool realtimeReflectionProbes;

	public bool billboardsFaceCameraPosition;

	public float resolutionScalingFixedDPIFactor;

	public bool softParticles;

	public RenderPipelineAsset renderPipeline;

	public SkinWeights skinWeights;

	public bool streamingMipmapsActive;

	public float streamingMipmapsMemoryBudget;

	public int streamingMipmapsRenderersPerFrame;

	public int streamingMipmapsMaxLevelReduction;

	public bool streamingMipmapsAddAllCameras;

	public int streamingMipmapsMaxFileIORequests;

	public int maxQueuedFrames;

	public ColorSpace desiredColorSpace;

	public ColorSpace activeColorSpace;

	public int globalTextureMipmapLimit;

	public int pixelLightCount;

	public int maximumLODLevel;

	public ShadowProjection shadowProjection;

	public int shadowCascades;

	public float shadowDistance;

	public UnityEngine.ShadowQuality shadows;

	public ShadowmaskMode shadowmaskMode;

	public float shadowNearPlaneOffset;

	public float shadowCascade2Split;

	public Vector3 shadowCascade4Split;

	public float lodBias;

	public AnisotropicFiltering anisotropicFiltering;

	public UnityEngine.ShadowResolution shadowResolution;

	public static QualityPreset CreateFromCurrentLevel()
	{
		//IL_0329: Expected O, but got F4
		QualityPreset qualityPreset = new QualityPreset();
		int num = QualitySettings.particleRaycastBudget;
		if (qualityPreset != null)
		{
			qualityPreset.particleRaycastBudget = num;
			bool flag = QualitySettings.softVegetation;
			qualityPreset.softVegetation = flag;
			int num2 = QualitySettings.vSyncCount;
			qualityPreset.vSyncCount = num2;
			int num3 = QualitySettings.antiAliasing;
			qualityPreset.antiAliasing = num3;
			int num4 = QualitySettings.asyncUploadTimeSlice;
			qualityPreset.asyncUploadTimeSlice = num4;
			int num5 = QualitySettings.asyncUploadBufferSize;
			qualityPreset.asyncUploadBufferSize = num5;
			bool flag2 = QualitySettings.asyncUploadPersistentBuffer;
			qualityPreset.asyncUploadPersistentBuffer = flag2;
			bool flag3 = QualitySettings.realtimeReflectionProbes;
			qualityPreset.realtimeReflectionProbes = flag3;
			bool flag4 = QualitySettings.billboardsFaceCameraPosition;
			qualityPreset.billboardsFaceCameraPosition = flag4;
			float num6 = QualitySettings.resolutionScalingFixedDPIFactor;
			qualityPreset.resolutionScalingFixedDPIFactor = num6;
			bool flag5 = QualitySettings.softParticles;
			qualityPreset.softParticles = flag5;
			SkinWeights skinWeights = QualitySettings.skinWeights;
			qualityPreset.skinWeights = skinWeights;
			bool flag6 = QualitySettings.streamingMipmapsActive;
			qualityPreset.streamingMipmapsActive = flag6;
			float num7 = QualitySettings.streamingMipmapsMemoryBudget;
			qualityPreset.streamingMipmapsMemoryBudget = num7;
			int num8 = QualitySettings.streamingMipmapsRenderersPerFrame;
			qualityPreset.streamingMipmapsRenderersPerFrame = num8;
			int num9 = QualitySettings.streamingMipmapsMaxLevelReduction;
			qualityPreset.streamingMipmapsMaxLevelReduction = num9;
			bool flag7 = QualitySettings.streamingMipmapsAddAllCameras;
			qualityPreset.streamingMipmapsAddAllCameras = flag7;
			int num10 = QualitySettings.streamingMipmapsMaxFileIORequests;
			qualityPreset.streamingMipmapsMaxFileIORequests = num10;
			int num11 = QualitySettings.maxQueuedFrames;
			qualityPreset.maxQueuedFrames = num11;
			ColorSpace colorSpace = QualitySettings.desiredColorSpace;
			qualityPreset.desiredColorSpace = colorSpace;
			ColorSpace colorSpace2 = QualitySettings.activeColorSpace;
			qualityPreset.activeColorSpace = colorSpace2;
			int num12 = QualitySettings.globalTextureMipmapLimit;
			qualityPreset.globalTextureMipmapLimit = num12;
			int num13 = QualitySettings.pixelLightCount;
			qualityPreset.pixelLightCount = num13;
			int num14 = QualitySettings.maximumLODLevel;
			qualityPreset.maximumLODLevel = num14;
			ShadowProjection shadowProjection = QualitySettings.shadowProjection;
			qualityPreset.shadowProjection = shadowProjection;
			int num15 = QualitySettings.shadowCascades;
			qualityPreset.shadowCascades = num15;
			float num16 = QualitySettings.shadowDistance;
			qualityPreset.shadowDistance = num16;
			UnityEngine.ShadowQuality shadowQuality = QualitySettings.shadows;
			qualityPreset.shadows = shadowQuality;
			ShadowmaskMode shadowmaskMode = QualitySettings.shadowmaskMode;
			qualityPreset.shadowmaskMode = shadowmaskMode;
			float num17 = QualitySettings.shadowNearPlaneOffset;
			qualityPreset.shadowNearPlaneOffset = num17;
			float num18 = QualitySettings.shadowCascade2Split;
			qualityPreset.shadowCascade2Split = num18;
			Vector3 vector = QualitySettings.shadowCascade4Split;
			qualityPreset.shadowCascade4Split = (Vector3)vector.x;
			_ = vector.z;
			float num19 = QualitySettings.lodBias;
			qualityPreset.lodBias = num19;
			AnisotropicFiltering anisotropicFiltering = QualitySettings.anisotropicFiltering;
			qualityPreset.anisotropicFiltering = anisotropicFiltering;
			UnityEngine.ShadowResolution shadowResolution = QualitySettings.shadowResolution;
			qualityPreset.shadowResolution = shadowResolution;
			RenderPipelineAsset renderPipelineAsset = QualitySettings.renderPipeline;
			RenderPipelineAsset renderPipelineAsset2;
			if (renderPipelineAsset == null)
			{
				renderPipelineAsset2 = null;
			}
			else
			{
				RenderPipelineAsset original = QualitySettings.renderPipeline;
				renderPipelineAsset2 = UnityEngine.Object.Instantiate(original);
			}
			qualityPreset.renderPipeline = renderPipelineAsset2;
			return qualityPreset;
		}
		return (QualityPreset)(object)new NullReferenceException();
	}

	public unsafe void ApplyToCurrentLevel()
	{
		//IL_016d: Expected O, but got Ref
		QualitySettings.particleRaycastBudget = particleRaycastBudget;
		QualitySettings.softVegetation = softVegetation;
		QualitySettings.vSyncCount = vSyncCount;
		QualitySettings.antiAliasing = antiAliasing;
		QualitySettings.asyncUploadTimeSlice = asyncUploadTimeSlice;
		QualitySettings.asyncUploadBufferSize = asyncUploadBufferSize;
		QualitySettings.asyncUploadPersistentBuffer = asyncUploadPersistentBuffer;
		QualitySettings.realtimeReflectionProbes = realtimeReflectionProbes;
		QualitySettings.billboardsFaceCameraPosition = billboardsFaceCameraPosition;
		QualitySettings.resolutionScalingFixedDPIFactor = resolutionScalingFixedDPIFactor;
		QualitySettings.softParticles = softParticles;
		QualitySettings.skinWeights = skinWeights;
		QualitySettings.streamingMipmapsActive = streamingMipmapsActive;
		QualitySettings.streamingMipmapsMemoryBudget = streamingMipmapsMemoryBudget;
		QualitySettings.streamingMipmapsRenderersPerFrame = streamingMipmapsRenderersPerFrame;
		QualitySettings.streamingMipmapsMaxLevelReduction = streamingMipmapsMaxLevelReduction;
		QualitySettings.streamingMipmapsAddAllCameras = streamingMipmapsAddAllCameras;
		QualitySettings.streamingMipmapsMaxFileIORequests = streamingMipmapsMaxFileIORequests;
		QualitySettings.maxQueuedFrames = maxQueuedFrames;
		QualitySettings.globalTextureMipmapLimit = globalTextureMipmapLimit;
		QualitySettings.pixelLightCount = pixelLightCount;
		QualitySettings.maximumLODLevel = maximumLODLevel;
		QualitySettings.shadowProjection = shadowProjection;
		QualitySettings.shadowCascades = shadowCascades;
		QualitySettings.shadowDistance = shadowDistance;
		QualitySettings.shadows = shadows;
		QualitySettings.shadowmaskMode = shadowmaskMode;
		QualitySettings.shadowNearPlaneOffset = shadowNearPlaneOffset;
		QualitySettings.shadowCascade2Split = shadowCascade2Split;
		object obj = default(object);
		QualitySettings.shadowCascade4Split = (Vector3)(&obj);
		QualitySettings.lodBias = lodBias;
		QualitySettings.anisotropicFiltering = anisotropicFiltering;
		QualitySettings.shadowResolution = shadowResolution;
		RenderPipelineAsset renderPipelineAsset = QualitySettings.renderPipeline;
		if (renderPipelineAsset != null && renderPipeline != null)
		{
			applyToCurrentLevelURP();
		}
	}

	protected unsafe void applyToCurrentLevelURP()
	{
		//IL_0656: Expected I, but got O
		//IL_009d: Expected I, but got O
		//IL_000d: Expected I, but got O
		//IL_001d: Expected O, but got I
		//IL_00c2: Expected I, but got O
		//IL_00d2: Expected O, but got I
		//IL_00fe: Expected I, but got O
		//IL_05fc: Expected I, but got O
		//IL_0059: Expected O, but got I
		//IL_0124: Expected O, but got I
		//IL_0151: Expected I, but got O
		//IL_01d7: Expected I, but got O
		//IL_01e4: Expected I, but got O
		//IL_0206: Expected I, but got O
		//IL_0213: Expected I, but got O
		//IL_027f: Expected F4, but got I
		//IL_02cd: Expected F4, but got I
		//IL_02fc: Expected F4, but got I
		//IL_036c: Expected I, but got O
		//IL_0458: Expected I, but got O
		//IL_0468: Expected O, but got I
		//IL_049c: Expected I, but got O
		//IL_06e7: Expected I, but got O
		//IL_04c2: Expected O, but got I
		//IL_04f7: Expected I, but got O
		//IL_0406: Expected I, but got O
		//IL_0522: Expected I, but got O
		//IL_0532: Expected O, but got I
		//IL_055e: Expected I, but got O
		//IL_074d: Expected I, but got O
		//IL_058c: Expected O, but got I
		//IL_05b9: Expected I, but got O
		UnityEngine.Object obj = renderPipeline;
		nint num = (nint)typeof(UniversalRenderPipelineAsset);
		if ((object)renderPipeline == null)
		{
			goto IL_0086;
		}
		nint num2 = (nint)obj;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rdx_v1 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ r8_v31 (Il2CppClass<UnityEngine.Object>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rdx_v1 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ r8_v31 (Il2CppClass<UnityEngine.Object>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v67 @ rax_v95+FFFFFFF8+v52 @ rax_v92*8]");
			if (0 == (nint)typeof(UniversalRenderPipelineAsset))
			{
				goto IL_0086;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		nint num4 = (nint)typeof(UniversalRenderPipelineAsset);
		nint num5 = num2;
		goto IL_075b;
		IL_016c:
		RenderPipelineAsset renderPipelineAsset;
		if (!(renderPipeline != null) || !(renderPipelineAsset != null))
		{
			return;
		}
		bool flag = (object)renderPipeline == null;
		nint num6 = unchecked((nint)null);
		RenderPipelineAsset renderPipelineAsset2 = renderPipelineAsset;
		nint num7 = unchecked((nint)null);
		RenderPipelineAsset obj4;
		if (!flag)
		{
			bool flag2 = (object)renderPipelineAsset == null;
			num6 = unchecked((nint)null);
			renderPipelineAsset2 = renderPipelineAsset;
			num7 = unchecked((nint)null);
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v2 (UnityEngine.Object)+10C]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v2 (UnityEngine.Object)+114]");
				((UniversalRenderPipelineAsset)renderPipelineAsset).colorGradingLutSize = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v2 (UnityEngine.Object)+110]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v2 (UnityEngine.Object)+FC]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v2 (UnityEngine.Object)+FD]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v2 (UnityEngine.Object)+E0]");
				((UniversalRenderPipelineAsset)renderPipelineAsset).shadowDepthBias = 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v2 (UnityEngine.Object)+4C]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v2 (UnityEngine.Object)+4D]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v2 (UnityEngine.Object)+55]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v2 (UnityEngine.Object)+5C]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v2 (UnityEngine.Object)+60]");
				((UniversalRenderPipelineAsset)renderPipelineAsset).renderScale = 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v2 (UnityEngine.Object)+C0]");
				((UniversalRenderPipelineAsset)renderPipelineAsset).shadowCascadeCount = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v2 (UnityEngine.Object)+BC]");
				((UniversalRenderPipelineAsset)renderPipelineAsset).shadowDistance = 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdi_v2 (UnityEngine.Object)+A0]");
				((UniversalRenderPipelineAsset)renderPipelineAsset).maxAdditionalLightsCount = 0;
				bool flag3 = renderPipelineAsset == null;
				bool flag4 = !flag3;
				obj4 = renderPipelineAsset;
				if (!flag4)
				{
					RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
					nint num8 = (nint)typeof(UniversalRenderPipelineAsset);
					bool flag5 = (object)currentRenderPipeline != null;
					obj4 = currentRenderPipeline;
					if (flag5)
					{
						num5 = (nint)currentRenderPipeline;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v128 @ rdx_v28 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
						object obj5 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v335 @ r8_v5 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
						nint num9 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v128 @ rdx_v28 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
						bool flag6 = num9 < 0;
						RenderPipelineAsset renderPipelineAsset3 = currentRenderPipeline;
						num4 = (nint)typeof(UniversalRenderPipelineAsset);
						renderPipelineAsset2 = renderPipelineAsset;
						if (!flag6)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v335 @ r8_v5 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
							object obj6 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v134 @ rax_v80+FFFFFFF8+v133 @ rax_v79*8]");
							bool flag7 = 0 != (nint)typeof(UniversalRenderPipelineAsset);
							renderPipelineAsset3 = currentRenderPipeline;
							num4 = (nint)typeof(UniversalRenderPipelineAsset);
							renderPipelineAsset2 = renderPipelineAsset;
							if (!flag7)
							{
								obj4 = currentRenderPipeline;
								goto IL_0691;
							}
						}
						goto IL_075b;
					}
				}
				goto IL_0691;
			}
		}
		throw new NullReferenceException();
		IL_06fa:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
		object obj7 = default(object);
		if (obj7 == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		bool flag8 = (object)UniversalRenderPipelineUtils.AdditionalLightShadowmapResolution_FieldInfo == null;
		object obj8 = default(object);
		num4 = (nint)(&obj8);
		num7 = unchecked((nint)null);
		if (!flag8)
		{
			object value = default(object);
			UniversalRenderPipelineUtils.AdditionalLightShadowmapResolution_FieldInfo.SetValue(renderPipelineAsset2, value);
			return;
		}
		goto IL_062b;
		IL_0691:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
		object obj9 = default(object);
		if (obj9 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			bool flag9 = (object)UniversalRenderPipelineUtils.MainLightShadowmapResolution_FieldInfo == null;
			num4 = (nint)(&obj8);
			renderPipelineAsset2 = renderPipelineAsset;
			num5 = unchecked((nint)null);
			if (flag9)
			{
				goto IL_0609;
			}
			object value2 = default(object);
			UniversalRenderPipelineUtils.MainLightShadowmapResolution_FieldInfo.SetValue(obj4, value2);
		}
		bool flag10 = renderPipelineAsset == null;
		bool flag11 = !flag10;
		renderPipelineAsset2 = renderPipelineAsset;
		if (!flag11)
		{
			RenderPipelineAsset currentRenderPipeline2 = GraphicsSettings.currentRenderPipeline;
			nint num10 = (nint)typeof(UniversalRenderPipelineAsset);
			bool flag12 = (object)currentRenderPipeline2 != null;
			renderPipelineAsset2 = currentRenderPipeline2;
			if (flag12)
			{
				nint num11 = (nint)currentRenderPipeline2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v332 @ rdx_v25 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
				object obj10 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v336 @ r8_v27 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
				nint num12 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v332 @ rdx_v25 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
				bool flag13 = num12 < 0;
				num4 = (nint)typeof(UniversalRenderPipelineAsset);
				renderPipelineAsset2 = currentRenderPipeline2;
				num5 = num11;
				if (!flag13)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v336 @ r8_v27 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
					object obj11 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v339 @ rax_v62+FFFFFFF8+v338 @ rax_v61*8]");
					bool flag14 = 0 != (nint)typeof(UniversalRenderPipelineAsset);
					num4 = (nint)typeof(UniversalRenderPipelineAsset);
					renderPipelineAsset2 = currentRenderPipeline2;
					num5 = num11;
					if (!flag14)
					{
						renderPipelineAsset2 = currentRenderPipeline2;
						goto IL_06fa;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				num7 = num5;
				goto IL_062b;
			}
		}
		goto IL_06fa;
		IL_0086:
		renderPipelineAsset = QualitySettings.renderPipeline;
		nint num13 = (nint)typeof(UniversalRenderPipelineAsset);
		if ((object)renderPipelineAsset != null)
		{
			num7 = (nint)renderPipelineAsset;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ rdx_v8 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			object obj12 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ r8_v3 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
			nint num14 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ rdx_v8 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			bool flag15 = num14 < 0;
			num6 = (nint)typeof(UniversalRenderPipelineAsset);
			renderPipelineAsset2 = renderPipelineAsset;
			if (!flag15)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ r8_v3 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
				object obj13 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v189 @ rax_v91+FFFFFFF8+v174 @ rax_v90*8]");
				bool flag16 = 0 != (nint)typeof(UniversalRenderPipelineAsset);
				num6 = (nint)typeof(UniversalRenderPipelineAsset);
				renderPipelineAsset2 = renderPipelineAsset;
				if (!flag16)
				{
					goto IL_016c;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			return;
		}
		goto IL_016c;
		IL_075b:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_0609;
		IL_062b:
		num6 = num4;
		throw new NullReferenceException();
		IL_0609:
		throw new NullReferenceException();
	}
}
