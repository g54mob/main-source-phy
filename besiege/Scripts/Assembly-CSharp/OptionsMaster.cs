using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class OptionsMaster : MonoBehaviour
{
	public enum Tier
	{
		VeryLow = 0,
		Low = 1,
		Medium = 2,
		High = 3,
		VeryHigh = 4
	}

	public class SSAOSetting
	{
		public SSAOPro.AOMode Mode;

		public float MaxIntensity;

		public SSAOPro.SampleCount Samples;

		public float Radius;

		public float Distance;

		public SSAOPro.BlurMode Blur;

		public int BlurPasses;

		public float Bias;

		public bool useNoiseTexture;

		public static SSAOSetting Original;

		public static SSAOSetting VeryLow;

		public static SSAOSetting Low;

		public static SSAOSetting Medium;

		public static SSAOSetting MediumWater;

		public static SSAOSetting High;

		public static SSAOSetting VeryHigh;

		public static SSAOSetting Cinematic4k;

		public static SSAOSetting Default
		{
			get
			{
				return Medium;
			}
		}

		public static void Setup()
		{
			SSAOSetting sSAOSetting = new SSAOSetting();
			sSAOSetting.Mode = SSAOPro.AOMode.V11;
			sSAOSetting.MaxIntensity = 15.1f;
			sSAOSetting.Samples = SSAOPro.SampleCount.Medium;
			sSAOSetting.Radius = 0.94f;
			sSAOSetting.Distance = 3.2f;
			sSAOSetting.Bias = 0.25f;
			sSAOSetting.Blur = SSAOPro.BlurMode.None;
			sSAOSetting.BlurPasses = 1;
			sSAOSetting.useNoiseTexture = false;
			Original = sSAOSetting;
			sSAOSetting = new SSAOSetting();
			sSAOSetting.Mode = SSAOPro.AOMode.V11;
			sSAOSetting.MaxIntensity = 16f;
			sSAOSetting.Samples = SSAOPro.SampleCount.VeryLow;
			sSAOSetting.Radius = 0.94f;
			sSAOSetting.Distance = 2.5f;
			sSAOSetting.Bias = 0.32f;
			sSAOSetting.Blur = SSAOPro.BlurMode.None;
			sSAOSetting.BlurPasses = 1;
			sSAOSetting.useNoiseTexture = false;
			VeryLow = sSAOSetting;
			sSAOSetting = new SSAOSetting();
			sSAOSetting.Mode = SSAOPro.AOMode.V11;
			sSAOSetting.MaxIntensity = 16f;
			sSAOSetting.Samples = SSAOPro.SampleCount.Low;
			sSAOSetting.Radius = 0.94f;
			sSAOSetting.Distance = 2.5f;
			sSAOSetting.Bias = 0.32f;
			sSAOSetting.Blur = SSAOPro.BlurMode.None;
			sSAOSetting.BlurPasses = 1;
			sSAOSetting.useNoiseTexture = false;
			Low = sSAOSetting;
			sSAOSetting = new SSAOSetting();
			sSAOSetting.Mode = SSAOPro.AOMode.V11;
			sSAOSetting.MaxIntensity = 16f;
			sSAOSetting.Samples = SSAOPro.SampleCount.Medium;
			sSAOSetting.Radius = 0.94f;
			sSAOSetting.Distance = 2.5f;
			sSAOSetting.Bias = 0.32f;
			sSAOSetting.Blur = SSAOPro.BlurMode.None;
			sSAOSetting.BlurPasses = 1;
			sSAOSetting.useNoiseTexture = false;
			Medium = sSAOSetting;
			sSAOSetting = new SSAOSetting();
			sSAOSetting.Mode = SSAOPro.AOMode.V12;
			sSAOSetting.MaxIntensity = Medium.MaxIntensity;
			sSAOSetting.Samples = Medium.Samples;
			sSAOSetting.Radius = Medium.Radius;
			sSAOSetting.Distance = Medium.Distance;
			sSAOSetting.Bias = Medium.Bias;
			sSAOSetting.Blur = Medium.Blur;
			sSAOSetting.BlurPasses = Medium.BlurPasses;
			sSAOSetting.useNoiseTexture = Medium.useNoiseTexture;
			MediumWater = sSAOSetting;
			sSAOSetting = new SSAOSetting();
			sSAOSetting.Mode = SSAOPro.AOMode.V12;
			sSAOSetting.MaxIntensity = 16f;
			sSAOSetting.Samples = SSAOPro.SampleCount.Ultra;
			sSAOSetting.Radius = 0.55f;
			sSAOSetting.Distance = 2.5f;
			sSAOSetting.Bias = 0.2f;
			sSAOSetting.Blur = SSAOPro.BlurMode.HighQualityBilateral;
			sSAOSetting.BlurPasses = 1;
			sSAOSetting.useNoiseTexture = true;
			High = sSAOSetting;
			sSAOSetting = new SSAOSetting();
			sSAOSetting.Mode = SSAOPro.AOMode.V12;
			sSAOSetting.MaxIntensity = 16f;
			sSAOSetting.Samples = SSAOPro.SampleCount.Ultra;
			sSAOSetting.Radius = 0.55f;
			sSAOSetting.Distance = 2.5f;
			sSAOSetting.Bias = 0.2f;
			sSAOSetting.Blur = SSAOPro.BlurMode.HighQualityBilateral;
			sSAOSetting.BlurPasses = 2;
			sSAOSetting.useNoiseTexture = true;
			VeryHigh = sSAOSetting;
			sSAOSetting = new SSAOSetting();
			sSAOSetting.Mode = SSAOPro.AOMode.V12;
			sSAOSetting.MaxIntensity = 16f;
			sSAOSetting.Samples = SSAOPro.SampleCount.Ultra;
			sSAOSetting.Radius = 0.6f;
			sSAOSetting.Distance = 2.5f;
			sSAOSetting.Bias = 0.2f;
			sSAOSetting.Blur = SSAOPro.BlurMode.HighQualityBilateral;
			sSAOSetting.BlurPasses = 4;
			sSAOSetting.useNoiseTexture = true;
			Cinematic4k = sSAOSetting;
		}

		public static void SetTo(Tier t)
		{
			switch (t)
			{
			case Tier.VeryLow:
				SetTo(VeryLow);
				break;
			case Tier.Low:
				SetTo(Low);
				break;
			case Tier.Medium:
				if (WaterController.Exist)
				{
					SetTo(MediumWater);
				}
				else
				{
					SetTo(Medium);
				}
				break;
			case Tier.High:
				SetTo(High);
				break;
			case Tier.VeryHigh:
				SetTo(VeryHigh);
				break;
			case (Tier)5:
				SetTo(Cinematic4k);
				break;
			default:
				Debug.LogWarning("SSAO set to missing tier " + t);
				BesiegeConfig.SSAOQuality = DefaultConfig.SSAOQuality;
				SetTo(Medium);
				break;
			}
		}

		public static void SetTo(SSAOSetting setting)
		{
			if (Camera.main == null)
			{
				return;
			}
			SSAOPro component = Camera.main.gameObject.GetComponent<SSAOPro>();
			if (!(component == null))
			{
				component.enabled = BesiegeConfig.ScreenSpaceAmbientOcclusion;
				if (!StatMaster.isMainMenu && BesiegeConfig.ScreenSpaceAmbientOcclusion)
				{
					component.Mode = setting.Mode;
					component.Intensity = BesiegeConfig.SSAOIntensity / 100f * setting.MaxIntensity;
					component.Samples = setting.Samples;
					component.Radius = setting.Radius;
					component.Distance = setting.Distance;
					component.Bias = setting.Bias;
					component.NoiseTexture = ((!setting.useNoiseTexture || Screen.height <= 2000) ? null : NoiseTex);
					component.Blur = setting.Blur;
					component.BlurPasses = setting.BlurPasses;
					component.CutoffDistance = 200f;
					component.BlurBilateralThreshold = 0.05f;
				}
			}
		}
	}

	public static BesiegeConfig BesiegeConfig = new BesiegeConfig();

	public static BesiegeConfig DefaultConfig = new BesiegeConfig();

	public static ControlScheme DefaultControls = new ControlScheme();

	public static ControlScheme CustomControls = new ControlScheme();

	public static AAMode FormerAntiAliasingMode = AAMode.FXAA3Console;

	public static bool isSandboxed = false;

	public static float minSendRate = 0.01f;

	public static float defaultSendRate = 0.1f;

	public static float maxSendRate = 2f;

	public static float minCamUpdateRate = 0.1f;

	public static float defaultCamUpdateRate = 0.05f;

	public static float maxCamUpdateRate = 1f;

	public static int minSkipChildCount = 0;

	public static int defaultSkipChildCount = 2;

	public static int maxSkipChildCount = 3;

	public static float minSmoothness = 0f;

	public static float defaultSmoothness = 1f;

	public static float maxSmoothness = 1f;

	public static float minVecThreshold = 0f;

	public static float defaultVecThreshold = 0f;

	public static float maxVecThreshold = 1f;

	public static float minRotThreshold = 0f;

	public static float defaultRotThreshold = 0f;

	public static float maxRotThreshold = 1f;

	public static bool negativeScaling = false;

	public static float minComponentUnit = 0.001f;

	public static float defaultTimeScale = 0.5f;

	public static int maxScoreboardPing = 999;

	public static float chokeWaitTime = 0.5f;

	public static bool firstRunAfterUpdate = false;

	public static float settingsMinBreakForce = 1f;

	public static float settingsMaxBreakForce = 20000f;

	public static float settingsMinDestroyThreshold = 1f;

	public static float settingsMaxDestroyThreshold = 20000f;

	public static bool clampMachineMiddleBlocksBelowFloor = true;

	public static bool scrollBindingEnabled = true;

	public static float scrollDisableTime = 0.5f;

	public static int linkDelayFrames = 2;

	public static int maxLogicTriggerCount = 36;

	public static int baseEventFrames = 10;

	public static int resendTransformFrames = 80;

	public static float sqrScaleUpdateThreshold = 0.002f;

	public static float networkTransformInterval = 0.1f;

	public static bool gatherTransformTargets = true;

	public static bool borderless = true;

	public static bool joinServer = false;

	public static bool spectatorEnabled = false;

	public static bool votingEnabled = false;

	public static bool allowExcessPlayers = true;

	public static bool limitPlayers = false;

	public static int maxPlayers = 10;

	public static int maxPlayersPerHost = 16;

	public static bool networkClusters = true;

	public static PlayerNetworkType networkType = PlayerNetworkType.DirectConnect;

	public Texture2D _noiseTex;

	public static Texture2D NoiseTex;

	private static OptionsMaster instance;

	protected Coroutine resolutionRoutine;

	public static bool skinsEnabled
	{
		get
		{
			return BesiegeConfig.SkinsEnabled;
		}
		set
		{
			BesiegeConfig.SkinsEnabled = value;
			BlockSkinLoader.ToggleSkins();
		}
	}

	public static float GetVerticalFOV()
	{
		return BesiegeConfig.FieldOfView * ((float)BesiegeConfig.ScreenHeight / (float)BesiegeConfig.ScreenWidth);
	}

	public IEnumerator Start()
	{
		NoiseTex = _noiseTex;
		QualitySettings.SetQualityLevel(3, true);
		SSAOSetting.Setup();
		SetShadowDefaults(350f);
		Resolution[] resolutions = Screen.resolutions;
		yield return null;
		yield return null;
		yield return null;
		if (BesiegeConfig.FirstTimePlaying)
		{
			BesiegeConfig.FirstTimePlaying = false;
			BesiegeConfig.ScreenWidth = resolutions[resolutions.Length - 1].width;
			BesiegeConfig.ScreenHeight = resolutions[resolutions.Length - 1].height;
			Screen.SetResolution(BesiegeConfig.ScreenWidth, BesiegeConfig.ScreenHeight, !BesiegeConfig.WindowedMode);
		}
		DefaultConfig.ScreenWidth = resolutions[resolutions.Length - 1].width;
		DefaultConfig.ScreenHeight = resolutions[resolutions.Length - 1].height;
		if (BesiegeConfig.SkinsEnabled)
		{
			BlockSkinLoader.ToggleSkins();
		}
		SceneManager.sceneLoaded += OnSceneLoaded;
		SetMasterVolume();
		SetTextureQuality();
		SetAnisoFilter();
		SetReflectionQuality();
		SetAntialiasing();
		SetSSAO();
		SetBloom();
		SetFPS();
		SetShaderDeform();
		SetShaderRippling();
		SetShadowDefaults(BesiegeConfig.ShadowRenderDistance);
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode newScene)
	{
		SetMasterVolume();
		SetAntialiasing();
		SetFPS();
		if (BesiegeEntryPoint.IsSPLevel(scene.name) || StatMaster.isMP)
		{
			SetSSAO();
			SetBloom();
			SetShaderRippling();
		}
	}

	protected void Awake()
	{
		instance = this;
		ReferenceMaster.onAdvancedBuildingToggled = (Action)Delegate.Combine(ReferenceMaster.onAdvancedBuildingToggled, new Action(SetAdvancedBuilding));
		ReferenceMaster.onFramerateChanged = (Action)Delegate.Combine(ReferenceMaster.onFramerateChanged, new Action(SetFPS));
		ReferenceMaster.onAAChanged = (Action)Delegate.Combine(ReferenceMaster.onAAChanged, new Action(SetAntialiasing));
		ReferenceMaster.onTextureQualityChanged = (Action)Delegate.Combine(ReferenceMaster.onTextureQualityChanged, new Action(SetTextureQuality));
		ReferenceMaster.onAnisoChanged = (Action)Delegate.Combine(ReferenceMaster.onAnisoChanged, new Action(SetAnisoFilter));
		ReferenceMaster.onReflectionQualityChanged = (Action)Delegate.Combine(ReferenceMaster.onReflectionQualityChanged, new Action(SetReflectionQuality));
		ReferenceMaster.onSSAOChanged = (Action)Delegate.Combine(ReferenceMaster.onSSAOChanged, new Action(SetSSAO));
		ReferenceMaster.onUIScaleChanged = (Action)Delegate.Combine(ReferenceMaster.onUIScaleChanged, new Action(ReferenceMaster.InvokeResolutionChange));
		ReferenceMaster.onBloomChanged = (Action)Delegate.Combine(ReferenceMaster.onBloomChanged, new Action(SetBloom));
		SetResolution();
		DetermineFirstRunAfterUpdate();
	}

	private void DetermineFirstRunAfterUpdate()
	{
		string lastVersion = BesiegeConfig.LastVersion;
		string versionString = VersionNumber.GetVersionString();
		if (string.IsNullOrEmpty(lastVersion) || !lastVersion.Equals(versionString))
		{
			Debug.Log("[GameVersion] Update detected: " + lastVersion + " > " + versionString);
			firstRunAfterUpdate = true;
			BesiegeConfig.LastVersion = versionString;
		}
	}

	public static void SetShaderDeform()
	{
		if (BesiegeConfig.DeformMeshes)
		{
			Shader.EnableKeyword("DISPLACE_VERTICES");
		}
		else
		{
			Shader.DisableKeyword("DISPLACE_VERTICES");
		}
	}

	public static void SetShaderRippling()
	{
		if (!StatMaster.isMainMenu && !(Camera.main == null))
		{
			RipplePostProcessing component = Camera.main.gameObject.GetComponent<RipplePostProcessing>();
			if (!(component == null))
			{
				component.enabled = BesiegeConfig.Rippling;
			}
		}
	}

	protected static void SetMasterVolume()
	{
		AudioListener.volume = BesiegeConfig.MasterVolume / 100f;
	}

	protected static void SetAntialiasing()
	{
		if (Camera.main == null)
		{
			return;
		}
		AntialiasingAsPostEffect component = Camera.main.gameObject.GetComponent<AntialiasingAsPostEffect>();
		if (!(component == null))
		{
			component.enabled = BesiegeConfig.AntiAliasingMode != AAMode.FXAA2;
			if (BesiegeConfig.AntiAliasingMode != AAMode.FXAA2)
			{
				component.SetMode(BesiegeConfig.AntiAliasingMode + 2);
			}
		}
	}

	protected static void SetUIBlur()
	{
		BesiegeConfig.UIBlur = !BesiegeConfig.UIBlur;
		SingleInstance<UIBlurManager>.Instance.ToggleUIBlur();
	}

	public static void SetShadowDefaults(float dist)
	{
		dist = Mathf.Max(350f, dist);
		QualitySettings.shadowCascade4Split = new Vector3(72f / dist, 160f / dist, 250f / dist);
		QualitySettings.shadowCascade2Split = 133f / dist;
	}

	public static void SetShadows(bool enabled, Light light = null)
	{
		BesiegeConfig besiegeConfig = BesiegeConfig;
		SetShadowDefaults(besiegeConfig.ShadowRenderDistance);
		if (enabled)
		{
			if (light != null)
			{
				light.shadows = (besiegeConfig.HardShadows ? LightShadows.Hard : LightShadows.Soft);
			}
			else
			{
				Shader.SetGlobalTexture("_ShadowMapTexture", Texture2D.whiteTexture);
			}
			QualitySettings.shadowResolution = besiegeConfig.ShadowRes;
			int num = besiegeConfig.ShadowCascades;
			if (WaterController.Exist && num == 2)
			{
				num = 0;
			}
			QualitySettings.shadowCascades = num;
			QualitySettings.shadowDistance = besiegeConfig.ShadowRenderDistance;
		}
		else
		{
			if (light != null)
			{
				light.shadows = LightShadows.None;
			}
			Shader.SetGlobalTexture("_ShadowMapTexture", Texture2D.whiteTexture);
		}
		if (besiegeConfig.ShadowCascades < 2)
		{
			if (!Shader.IsKeywordEnabled("_SHADOWS_SINGLE_CASCADE"))
			{
				Shader.EnableKeyword("_SHADOWS_SINGLE_CASCADE");
			}
			if (Shader.IsKeywordEnabled("_SHADOWS_TWO_CASCADES"))
			{
				Shader.DisableKeyword("_SHADOWS_TWO_CASCADES");
			}
		}
		else if (besiegeConfig.ShadowCascades == 2)
		{
			if (!Shader.IsKeywordEnabled("_SHADOWS_TWO_CASCADES"))
			{
				Shader.EnableKeyword("_SHADOWS_TWO_CASCADES");
			}
			if (Shader.IsKeywordEnabled("_SHADOWS_SINGLE_CASCADE"))
			{
				Shader.DisableKeyword("_SHADOWS_SINGLE_CASCADE");
			}
		}
		else
		{
			if (Shader.IsKeywordEnabled("_SHADOWS_SINGLE_CASCADE"))
			{
				Shader.DisableKeyword("_SHADOWS_SINGLE_CASCADE");
			}
			if (Shader.IsKeywordEnabled("_SHADOWS_TWO_CASCADES"))
			{
				Shader.DisableKeyword("_SHADOWS_TWO_CASCADES");
			}
		}
	}

	protected static void SetSSAO()
	{
		SSAOSetting.SetTo(BesiegeConfig.SSAOQuality);
	}

	public static void SetSSAO(int i)
	{
		SSAOSetting.SetTo((Tier)i);
	}

	public static void SetBloom()
	{
		if (StatMaster.isMainMenu || Camera.main == null)
		{
			return;
		}
		BloomAndLensFlares component = Camera.main.gameObject.GetComponent<BloomAndLensFlares>();
		if (component == null)
		{
			return;
		}
		if (BesiegeConfig.Bloom)
		{
			component.bloomBlurIterations = 2;
			component.hollyStretchWidth = 0.5f;
			component.hollywoodFlareBlurIterations = 2;
			component.bloomIntensity = Mathf.Lerp(0.5f, 0.6f, Mathf.Pow(BesiegeConfig.BloomIntensity / 100f, 0.1f));
			float b = 3f;
			float p = 2f;
			switch (StatMaster.GetCurrentIsland())
			{
			case Island.Ipsilon:
				b = 1.603f;
				p = 1f;
				break;
			case Island.Tolbrynd:
			case Island.Valfross:
			case Island.Krolmar:
				b = 2.5f;
				break;
			case Island.Water:
			case Island.WaterSandbox:
				b = 3.51f;
				break;
			case Island.None:
				b = 2f;
				break;
			}
			component.sepBlurSpread = Mathf.Lerp(0.25f, b, Mathf.Pow(BesiegeConfig.BloomIntensity / 100f, p));
		}
		else
		{
			component.bloomIntensity = 0.5f;
			component.sepBlurSpread = 0f;
			component.bloomBlurIterations = 1;
			component.hollyStretchWidth = 0f;
			component.hollywoodFlareBlurIterations = 1;
		}
	}

	protected static void SetTextureQuality()
	{
		QualitySettings.masterTextureLimit = Mathf.Abs(3 - BesiegeConfig.TextureQuality);
	}

	protected static void SetAnisoFilter()
	{
		QualitySettings.anisotropicFiltering = BesiegeConfig.AnisoFilter;
	}

	protected static void SetReflectionQuality()
	{
		PlanarReflections.UpdateReflectionQuality();
	}

	protected static void SetAdvancedBuilding()
	{
		AdvancedUIController advancedUIController = AdvancedUIController.Instance;
		if (advancedUIController != null)
		{
			AdvancedUIController.Instance.Toggle(BesiegeConfig.AdvancedBuilding);
		}
	}

	public static void SetResolution()
	{
		if (instance != null)
		{
			instance.ResetResolution();
		}
	}

	protected void ResetResolution()
	{
		if (resolutionRoutine != null)
		{
			StopCoroutine(resolutionRoutine);
			resolutionRoutine = null;
		}
		resolutionRoutine = StartCoroutine(RefreshResolutionIE());
	}

	public static int GetFPSLock()
	{
		return FrameRate.GetFPSLock(BesiegeConfig);
	}

	private IEnumerator RefreshResolutionIE()
	{
		BesiegeConfig currentConfig = BesiegeConfig;
		Screen.SetResolution(currentConfig.ScreenWidth, currentConfig.ScreenHeight, !currentConfig.WindowedMode);
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		if (ReferenceMaster.onResolutionChanged != null)
		{
			ReferenceMaster.onResolutionChanged();
		}
		if (ReferenceMaster.onFOVChanged != null)
		{
			ReferenceMaster.onFOVChanged();
		}
		resolutionRoutine = null;
	}

	public static void SetFPS()
	{
		CapFPS.SetTargetFrameRate((BesiegeConfig.VSync != 0) ? (-1) : GetFPSLock());
		QualitySettings.vSyncCount = Mathf.Clamp(BesiegeConfig.VSync, 0, 2);
		QualitySettings.maxQueuedFrames = ((BesiegeConfig.VSync > 0) ? 1 : 2);
	}
}
