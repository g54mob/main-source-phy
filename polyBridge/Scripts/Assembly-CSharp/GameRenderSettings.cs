using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameRenderSettings
{
	private static UniversalRenderPipelineAsset m_UniversalRenderPipelineAsset;

	public static void Init()
	{
		m_UniversalRenderPipelineAsset = (UniversalRenderPipelineAsset)GraphicsSettings.defaultRenderPipeline;
	}

	public static void SetShadows(bool on)
	{
		m_UniversalRenderPipelineAsset.shadowDistance = (on ? (GameSettings.CamDistFromPivot() + 50f) : 0f);
	}

	public static void ResetSetShadowsOnExit()
	{
		try
		{
			m_UniversalRenderPipelineAsset.shadowDistance = GameSettings.CamDistFromPivot() + 50f;
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Caught exception in ResetSetShadowsOnExit(): " + ex.Message);
		}
	}

	public static void SetShadows_OverrideDistance(bool on, float cameraZDistance)
	{
		try
		{
			m_UniversalRenderPipelineAsset.shadowDistance = (on ? (cameraZDistance + 50f) : 0f);
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Caught exception in SetShadows_OverrideDistance(): " + ex.Message);
		}
	}

	public static void SetQualitySettings(bool vsync, int vsyncInterval, ShadowResolution shadowResolution)
	{
		QualitySettings.SetQualityLevel((int)shadowResolution, applyExpensiveChanges: true);
		if (GameStateManager.GetState() == GameState.SIM || GameStateManager.GetState() == GameState.MAIN_MENU)
		{
			SetShadows((Profiles.m_ActiveProfile.m_ShadowResolution != ShadowResolution.OFF) ? true : false);
		}
		if (vsyncInterval > 0)
		{
			QualitySettings.vSyncCount = vsyncInterval;
		}
		else
		{
			QualitySettings.vSyncCount = (vsync ? 1 : 0);
		}
	}

	public static void SetPostFXSettings(bool ssao, bool bloom, bool vignette, AntiAliasingQuality antiAliasingQuality)
	{
		if (PostFX.m_Instance.m_ForwardRenderData.rendererFeatures.Count > 0)
		{
			PostFX.m_Instance.m_ForwardRenderData.rendererFeatures[0].SetActive(ssao);
		}
		PostFX.m_Instance.m_Volume.profile.TryGet<Bloom>(out var component);
		if (component != null)
		{
			component.active = bloom;
		}
		PostFX.m_Instance.m_Volume.profile.TryGet<Vignette>(out var component2);
		if (component2 != null)
		{
			component2.active = vignette;
		}
		QualitySettings.antiAliasing = 0;
		switch (antiAliasingQuality)
		{
		case AntiAliasingQuality.EXTREME_PERFORMANCE:
			Cameras.MainCamera().GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.None;
			break;
		case AntiAliasingQuality.PERFORMANCE:
			Cameras.MainCamera().GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.FastApproximateAntialiasing;
			break;
		case AntiAliasingQuality.DEFAULT:
			Cameras.MainCamera().GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.SubpixelMorphologicalAntiAliasing;
			Cameras.MainCamera().GetComponent<UniversalAdditionalCameraData>().antialiasingQuality = AntialiasingQuality.Low;
			break;
		case AntiAliasingQuality.QUALITY:
			Cameras.MainCamera().GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.SubpixelMorphologicalAntiAliasing;
			Cameras.MainCamera().GetComponent<UniversalAdditionalCameraData>().antialiasingQuality = AntialiasingQuality.Medium;
			break;
		case AntiAliasingQuality.EXTREME_QUALITY:
			Cameras.MainCamera().GetComponent<UniversalAdditionalCameraData>().antialiasing = AntialiasingMode.SubpixelMorphologicalAntiAliasing;
			Cameras.MainCamera().GetComponent<UniversalAdditionalCameraData>().antialiasingQuality = AntialiasingQuality.High;
			break;
		}
	}
}
