using SCPE;
using TFBGames;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ApplyPostSettings : MonoBehaviour
{
	private PostProcessProfile m_postProfile;

	private GlobalSettingsHandler settingsHandler;

	private MainCam m_mainCam;

	[SerializeField]
	[Tooltip("Check to force SSAO on in this scene on certain platforms where it's disabled by default")]
	private bool m_ssaoException;

	private void Awake()
	{
		settingsHandler = ServiceLocator.GetService<GlobalSettingsHandler>();
		PostProcessVolume component = GetComponent<PostProcessVolume>();
		m_postProfile = component.sharedProfile;
	}

	private void Start()
	{
		AssignMainCamera();
		ApplyGlobalSettings();
	}

	private void OnDestroy()
	{
		if (!(settingsHandler == null))
		{
			settingsHandler.DeregisterSettingsChangeHandler("VIDEO_AA", UpdateAA);
			settingsHandler.DeregisterSettingsChangeHandler("VIDEO_SSAO", UpdateAO);
			settingsHandler.DeregisterSettingsChangeHandler("VIDEO_BLOOM", UpdateBloom);
			settingsHandler.DeregisterSettingsChangeHandler("VIDEO_VIGNETTE", UpdateVignette);
			settingsHandler.DeregisterSettingsChangeHandler("VIDEO_MOTIONBLUR", UpdateBlur);
			settingsHandler.DeregisterSettingsChangeHandlerFloat("VIDEO_MOTIONBLURSHUTTER", UpdateBlurShutter);
			settingsHandler.DeregisterSettingsChangeHandlerFloat("VIDEO_MOTIONBLURSAMPLES", UpdateBlurSamples);
			settingsHandler.DeregisterSettingsChangeHandler("VIDEO_COLORCORRECTION", UpdateColorGrading);
			settingsHandler.DeregisterSettingsChangeHandler("VIDEO_AUTOEXPOSURE", UpdateAutoExposure);
			settingsHandler.DeregisterSettingsChangeHandler("VIDEO_DOF", UpdateDepthOfField);
			settingsHandler.DeregisterSettingsChangeHandler("VIDEO_AA_FASTMODE", UpdateAAFastMode);
			settingsHandler.DeregisterSettingsChangeHandler("VIDEO_BLOOM_FASTMODE", UpdateBloomFastMode);
			settingsHandler.DeregisterSettingsChangeHandler("VIDEO_FOG", UpdateFog);
		}
	}

	public void AssignSettingsToProfile(PostProcessProfile postProcessProfile)
	{
		m_postProfile = postProcessProfile;
		ApplyGlobalSettings();
	}

	private void ApplyGlobalSettings()
	{
		if (settingsHandler != null)
		{
			settingsHandler.RegisterSettingsChangeHandler("VIDEO_AA", UpdateAA);
			settingsHandler.RegisterSettingsChangeHandler("VIDEO_SSAO", UpdateAO);
			settingsHandler.RegisterSettingsChangeHandler("VIDEO_BLOOM", UpdateBloom);
			settingsHandler.RegisterSettingsChangeHandler("VIDEO_VIGNETTE", UpdateVignette);
			settingsHandler.RegisterSettingsChangeHandler("VIDEO_MOTIONBLUR", UpdateBlur);
			settingsHandler.RegisterSettingsChangeHandlerFloat("VIDEO_MOTIONBLURSHUTTER", UpdateBlurShutter);
			settingsHandler.RegisterSettingsChangeHandlerFloat("VIDEO_MOTIONBLURSAMPLES", UpdateBlurSamples);
			settingsHandler.RegisterSettingsChangeHandler("VIDEO_COLORCORRECTION", UpdateColorGrading);
			settingsHandler.RegisterSettingsChangeHandler("VIDEO_AUTOEXPOSURE", UpdateAutoExposure);
			settingsHandler.RegisterSettingsChangeHandler("VIDEO_DOF", UpdateDepthOfField);
			settingsHandler.RegisterSettingsChangeHandler("VIDEO_AA_FASTMODE", UpdateAAFastMode);
			settingsHandler.RegisterSettingsChangeHandler("VIDEO_BLOOM_FASTMODE", UpdateBloomFastMode);
			settingsHandler.RegisterSettingsChangeHandler("VIDEO_FOG", UpdateFog);
		}
	}

	private bool AssignMainCamera()
	{
		m_mainCam = ServiceLocator.GetService<PlayerCamerasManager>()?.GetMainCam(TFBGames.Player.One);
		if (m_mainCam == null)
		{
			Debug.LogError("MainCam is null, unable to update settings");
			return false;
		}
		return true;
	}

	private void UpdateAA(int value)
	{
		if (!(m_mainCam == null) || AssignMainCamera())
		{
			PostProcessLayer component = m_mainCam.GetComponent<PostProcessLayer>();
			PostProcessLayer.Antialiasing antialiasingMode = PostProcessLayer.Antialiasing.None;
			switch (value)
			{
			case 0:
				antialiasingMode = PostProcessLayer.Antialiasing.None;
				break;
			case 1:
				antialiasingMode = PostProcessLayer.Antialiasing.FastApproximateAntialiasing;
				break;
			case 2:
				antialiasingMode = PostProcessLayer.Antialiasing.SubpixelMorphologicalAntialiasing;
				break;
			}
			component.antialiasingMode = antialiasingMode;
			component.subpixelMorphologicalAntialiasing.quality = SubpixelMorphologicalAntialiasing.Quality.High;
		}
	}

	private void UpdateAAFastMode(int value)
	{
		if (!(m_mainCam == null) || AssignMainCamera())
		{
			m_mainCam.GetComponent<PostProcessLayer>().fastApproximateAntialiasing.fastMode = value == 1;
		}
	}

	private void UpdateAO(int value)
	{
		if (!m_postProfile.TryGetSettings<AmbientOcclusion>(out var outSetting))
		{
			return;
		}
		if (m_ssaoException)
		{
			SettingsProfileManager service = ServiceLocator.GetService<SettingsProfileManager>();
			if (service != null && service.CurrentSettingsProfile != null)
			{
				value = (service.CurrentSettingsProfile.AllowForcedSsao ? 1 : value);
			}
		}
		outSetting.enabled.value = value == 1;
	}

	private void UpdateBloom(int value)
	{
		if (m_postProfile.TryGetSettings<Bloom>(out var outSetting))
		{
			outSetting.enabled.value = value == 1;
		}
	}

	private void UpdateBloomFastMode(int value)
	{
		if (m_postProfile.TryGetSettings<Bloom>(out var outSetting))
		{
			outSetting.fastMode.value = value == 1;
		}
	}

	private void UpdateVignette(int value)
	{
		if (m_postProfile.TryGetSettings<Vignette>(out var outSetting))
		{
			outSetting.enabled.value = value == 1;
		}
	}

	private void UpdateBlur(int value)
	{
		if (m_postProfile.TryGetSettings<MotionBlur>(out var outSetting))
		{
			outSetting.enabled.value = value == 1;
			if (value == 0)
			{
				DisableMotionVectors();
			}
		}
	}

	private void UpdateBlurShutter(float value)
	{
		if (m_postProfile.TryGetSettings<MotionBlur>(out var outSetting))
		{
			outSetting.shutterAngle.value = (int)value;
		}
	}

	private void UpdateBlurSamples(float value)
	{
		if (m_postProfile.TryGetSettings<MotionBlur>(out var outSetting))
		{
			outSetting.sampleCount.value = (int)value;
		}
	}

	private void UpdateColorGrading(int value)
	{
		if (m_postProfile.TryGetSettings<ColorGrading>(out var outSetting))
		{
			outSetting.enabled.value = value == 1;
		}
	}

	private void UpdateAutoExposure(int value)
	{
		if (m_postProfile.TryGetSettings<AutoExposure>(out var outSetting))
		{
			outSetting.enabled.value = value == 1;
		}
	}

	private void UpdateDepthOfField(int value)
	{
		if (m_postProfile.TryGetSettings<DepthOfField>(out var outSetting))
		{
			outSetting.enabled.value = value == 1;
		}
	}

	private void UpdateFog(int value)
	{
		if (m_postProfile.TryGetSettings<SCPE.Fog>(out var outSetting))
		{
			outSetting.enabled.value = value == 1;
		}
	}

	private void DisableMotionVectors()
	{
		if (m_mainCam != null && m_mainCam.m_camera.depthTextureMode.HasFlag(DepthTextureMode.MotionVectors))
		{
			m_mainCam.m_camera.depthTextureMode &= ~DepthTextureMode.MotionVectors;
		}
	}
}
