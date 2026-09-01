using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostFX : MonoBehaviour
{
	[Header("Foward Renderers")]
	public UniversalRendererData m_ForwardRenderData;

	public UniversalRendererData m_ForwardRenderDataBuildMode;

	[Header("Volumes")]
	public Volume m_Volume;

	public VolumeProfile m_BuildModeVolumeProfile;

	public VolumeProfile m_SandboxVolumeProfile;

	public VolumeProfile m_DecorVolumeProfile;

	[Header("Build Mode")]
	[ColorUsage(true, true)]
	public Color m_BuildModeNoCollideTint;

	[ColorUsage(true, true)]
	public Color m_BuildModeSupportCollideTint;

	[ColorUsage(true, true)]
	public Color m_BuildModeCollideTint;

	[ColorUsage(true, true)]
	public Color m_BuildModeCustomShapeNoCollide;

	[Header("Abmient Lighting")]
	[ColorUsage(true, true)]
	public Color m_BuildAmbientLightColor;

	[ColorUsage(true, true)]
	public Color m_SandboxAmbientLightColor;

	public static PostFX m_Instance;

	public void Awake()
	{
		m_Instance = this;
	}

	public void SetForBuildMode()
	{
		Cameras.m_Instance.m_Main.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = false;
		Cameras.m_Instance.m_RenderLast.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = true;
		m_Volume.profile = m_BuildModeVolumeProfile;
	}

	public void SetForSandbox()
	{
		Set(m_SandboxVolumeProfile);
	}

	public void SetForDecor()
	{
		Set(m_DecorVolumeProfile);
	}

	public void Set(VolumeProfile volumeProfile)
	{
		Cameras.m_Instance.m_Main.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = false;
		Cameras.m_Instance.m_RenderLast.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = true;
		m_Volume.profile = volumeProfile;
	}

	public void PopupBackgroundFXOn()
	{
		if (GameStateManager.GetState() == GameState.BUILD)
		{
			Cameras.m_Instance.m_Main.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = false;
			Cameras.m_Instance.m_RenderLast.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = true;
		}
		m_Volume.profile.TryGet<DepthOfField>(out var component);
		if (component != null)
		{
			component.active = true;
		}
	}

	public void PopupBackgroundFXOff()
	{
		if (GameStateManager.GetState() == GameState.BUILD)
		{
			Cameras.m_Instance.m_Main.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = true;
			Cameras.m_Instance.m_RenderLast.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = false;
		}
		m_Volume.profile.TryGet<DepthOfField>(out var component);
		if (component != null)
		{
			component.active = false;
		}
	}
}
