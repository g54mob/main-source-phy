using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace INab.ToonDetailer.URP;

public class ToonDetailer : ScriptableRendererFeature
{
	public class TextureRefData : ContextItem
	{
		public TextureHandle depthMaskTexture = TextureHandle.s_NullHandle;

		public override void Reset()
		{
			depthMaskTexture = TextureHandle.s_NullHandle;
		}
	}

	private ToonDetailerSettings m_Settings;

	private Shader m_Shader;

	private Shader m_DepthShader;

	private Material m_ToonDetailerMaterial;

	private Material m_DepthMaterial;

	private ToonDetailerPass m_ToonDetailerPass;

	private DepthMaskPass m_DepthMaskPass;

	public const string k_UseContours = "_USE_CONTOURS";

	public const string k_UseCavity = "_USE_CAVITY";

	public const string k_Orthographic = "_ORTHOGRAPHIC";

	public const string k_FadeContoursOnly = "_FADE_COUNTOURS_ONLY";

	public const string k_FadeOn = "_FADE_ON";

	public override void Create()
	{
		ToonDetailerPass toonDetailerPass = (ToonDetailerPass)new ScriptableRenderPass();
		ProfilingSampler profilingSampler = new ProfilingSampler("Toon Detailer");
		((ScriptableRenderPass)toonDetailerPass).profilingSampler = profilingSampler;
		m_ToonDetailerPass = toonDetailerPass;
		ToonDetailerSettings settings = m_Settings;
		if (settings._MaskUse != ToonDetailerSettings.MaskUse.None)
		{
			DepthMaskPass depthMaskPass = (DepthMaskPass)new ScriptableRenderPass();
			ProfilingSampler profilingSampler2 = new ProfilingSampler("Toon Detailer Depth Mask");
			((ScriptableRenderPass)depthMaskPass).profilingSampler = profilingSampler2;
			m_DepthMaskPass = depthMaskPass;
		}
	}

	public unsafe override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
	{
		//IL_0013: Expected O, but got Ref
		//IL_0050: Expected O, but got Ref
		//IL_008d: Expected O, but got Ref
		if (m_ToonDetailerPass == null)
		{
			return;
		}
		CameraData cameraData = (CameraData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref renderingData, 8));
		if (((CameraData*)cameraData)->cameraType == CameraType.Preview)
		{
			return;
		}
		CameraData cameraData2 = (CameraData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref renderingData, 8));
		if (((CameraData*)cameraData2)->cameraType == CameraType.Reflection)
		{
			return;
		}
		CameraData cameraData3 = (CameraData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref renderingData, 8));
		if (!((CameraData*)cameraData3)->postProcessEnabled)
		{
			return;
		}
		if (m_ToonDetailerMaterial == null)
		{
			Shader shader = Shader.Find("Hidden/INabStudio/ToonDetailer");
			Material toonDetailerMaterial = CoreUtils.CreateEngineMaterial(shader);
			m_ToonDetailerMaterial = toonDetailerMaterial;
		}
		if (m_DepthMaterial == null)
		{
			Shader shader2 = Shader.Find("Hidden/INabStudio/ToonDetailer/DepthMask");
			Material depthMaterial = CoreUtils.CreateEngineMaterial(shader2);
			m_DepthMaterial = depthMaterial;
		}
		ToonDetailerSettings settings = m_Settings;
		if (settings._ControlViaVolumes)
		{
			VolumeManager instance = VolumeManager.instance;
			ToonDetailerVolumeComponent toonDetailerVolumeComponent;
			if (instance._003Cstack_003Ek__BackingField != null)
			{
				ToonDetailerVolumeComponent component = instance._003Cstack_003Ek__BackingField.GetComponent<ToonDetailerVolumeComponent>();
				toonDetailerVolumeComponent = component;
			}
			else
			{
				toonDetailerVolumeComponent = null;
			}
			if (!(toonDetailerVolumeComponent != null) || !toonDetailerVolumeComponent.IsActive())
			{
				return;
			}
		}
		ToonDetailerSettings settings2 = m_Settings;
		if (settings2._MaskUse != ToonDetailerSettings.MaskUse.None)
		{
			DepthMaskPass depthMaskPass = m_DepthMaskPass;
			((ScriptableRenderPass)depthMaskPass)._003CrenderPassEvent_003Ek__BackingField = RenderPassEvent.AfterRenderingOpaques;
		}
		ToonDetailerPass toonDetailerPass = m_ToonDetailerPass;
		((ScriptableRenderPass)toonDetailerPass)._003CrenderPassEvent_003Ek__BackingField = RenderPassEvent.BeforeRenderingTransparents;
		ToonDetailerPass toonDetailerPass2 = m_ToonDetailerPass;
		((ScriptableRenderPass)toonDetailerPass2).m_Input = (ScriptableRenderPassInput)3;
		ToonDetailerSettings settings3 = m_Settings;
		if (settings3._MaskUse != ToonDetailerSettings.MaskUse.None)
		{
			DepthMaskPass depthMaskPass2 = m_DepthMaskPass;
			depthMaskPass2.m_Material = m_DepthMaterial;
			depthMaskPass2.m_LayerMask = settings3._MaskLayer;
		}
		ToonDetailerPass toonDetailerPass3 = m_ToonDetailerPass;
		toonDetailerPass3.m_Material = m_ToonDetailerMaterial;
		toonDetailerPass3.m_Settings = m_Settings;
		ToonDetailerSettings settings4 = m_Settings;
		if (settings4._MaskUse != ToonDetailerSettings.MaskUse.None)
		{
			renderer.EnqueuePass(m_DepthMaskPass);
		}
		renderer.EnqueuePass(m_ToonDetailerPass);
	}

	protected override void Dispose(bool disposing)
	{
		m_DepthMaskPass = null;
		m_ToonDetailerPass = null;
		CoreUtils.Destroy(m_ToonDetailerMaterial);
		CoreUtils.Destroy(m_DepthMaterial);
	}

	public ToonDetailer()
	{
		//IL_002a: Expected O, but got I
		ToonDetailerSettings toonDetailerSettings = new ToonDetailerSettings();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206D80]");
		toonDetailerSettings._ColorHue = (Color)0;
		toonDetailerSettings._FadeStart = 40f;
		toonDetailerSettings._FadeEnd = 60f;
		toonDetailerSettings._BlackOffset = 0.5f;
		toonDetailerSettings._ContoursIntensity = 0.5f;
		toonDetailerSettings._ContoursThickness = 1f;
		toonDetailerSettings._ContoursElevationStrength = 1f;
		toonDetailerSettings._ContoursDepressionStrength = 2f;
		toonDetailerSettings._CavityIntensity = 1f;
		toonDetailerSettings._CavityRadius = 0.5f;
		toonDetailerSettings._CavityStrength = 1.25f;
		toonDetailerSettings._CavitySamples = 12;
		m_Settings = toonDetailerSettings;
		base._002Ector();
	}
}
