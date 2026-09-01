using UnityEngine;

public class Theme : MonoBehaviour
{
	public ThemeStub m_ThemeStub;

	[Header("FX")]
	public Light m_SunLight;

	public Light m_SecondaryLight;

	public Light m_BridgeLight;

	public Light m_WaterFillLight;

	[Header("Build Mode")]
	public Light m_BuildModeLight;

	public Light m_SandboxModeLight;

	public GameObject m_BuildZoneDuck;

	[Header("Water")]
	public GameObject m_WaterPlane;

	public float m_MinWidth;

	public float m_MaxWidth;

	public static Theme m_Instance;

	private MeshRenderer m_WaterPlaneMeshRenderer;

	private MaterialPropertyBlock m_WaterPlaneMaterialPropertyBlock;

	private static string m_LastLoadedHeightFogThemeID;

	private void Awake()
	{
		m_Instance = this;
		m_BuildZoneDuck.SetActive(value: false);
		m_WaterPlaneMeshRenderer = m_WaterPlane.GetComponent<MeshRenderer>();
		m_WaterPlaneMaterialPropertyBlock = new MaterialPropertyBlock();
	}

	private void Start()
	{
		GameUI.m_Instance.m_SandboxTheme.PopulateThemes();
	}

	private void Update()
	{
	}

	public float GetDefaultTerrainHeight()
	{
		if (Mathf.Approximately(m_Instance.m_ThemeStub.m_DefaultTerrainHeight, 0f))
		{
			return TerrainIslands.DEFAULT_HEIGHT;
		}
		return m_Instance.m_ThemeStub.m_DefaultTerrainHeight;
	}

	public int GetTerrainPrefabIndex(TerrainIslandType islandType, string name)
	{
		switch (islandType)
		{
		case TerrainIslandType.Bookend:
		{
			for (int j = 0; j < m_ThemeStub.m_BookendPrefabs.Length; j++)
			{
				if (m_ThemeStub.m_BookendPrefabs[j].name == name)
				{
					return j;
				}
			}
			break;
		}
		case TerrainIslandType.Middle:
		{
			for (int i = 0; i < m_ThemeStub.m_MiddleIslandPrefabs.Length; i++)
			{
				if (m_ThemeStub.m_MiddleIslandPrefabs[i].name == name)
				{
					return i;
				}
			}
			break;
		}
		}
		return -1;
	}

	public int GetNumTerrainIslandPrefabs(TerrainIslandType islandType)
	{
		return islandType switch
		{
			TerrainIslandType.Bookend => m_ThemeStub.m_BookendPrefabs.Length, 
			TerrainIslandType.Middle => m_ThemeStub.m_MiddleIslandPrefabs.Length, 
			_ => 0, 
		};
	}

	public GameObject GetTerrainIslandPrefab(TerrainIslandType islandType, int variantIndex)
	{
		if (variantIndex == -1)
		{
			return null;
		}
		switch (islandType)
		{
		case TerrainIslandType.Bookend:
			if (variantIndex < m_ThemeStub.m_BookendPrefabs.Length)
			{
				return m_ThemeStub.m_BookendPrefabs[variantIndex];
			}
			break;
		case TerrainIslandType.Middle:
			if (variantIndex < m_ThemeStub.m_MiddleIslandPrefabs.Length)
			{
				return m_ThemeStub.m_MiddleIslandPrefabs[variantIndex];
			}
			break;
		}
		return null;
	}

	public void OnLayoutLoaded()
	{
		UpdateGradientSkyFromThemeStub();
		UpdateHeightFogFromThemeStub();
		UpdateWaterPlaneFromThemeStub();
		WaterLine.Generate();
		if (GameStateManager.GetState() != GameState.BUILD)
		{
			UpdateLightsFromThemeStub();
		}
		if (GameStateManager.GetState() == GameState.SIM)
		{
			BridgeEdges.EnableJointCaps();
			Pistons.EnablePinions();
		}
		if (GameStateManager.GetState() != GameState.BUILD)
		{
			Pistons.HideAllUI();
			BridgeSprings.HideAllUI();
		}
		if (GameStateManager.GetState() == GameState.SANDBOX)
		{
			BridgeJoints.MakeGreyScale();
		}
		else
		{
			BridgeJoints.MakeDefaultColor();
		}
	}

	public void PlayAmbientAudio()
	{
		ThemeAudio.Play(m_ThemeStub.m_AmbientAudio);
	}

	public void StopAmbientAudio()
	{
		ThemeAudio.Stop();
	}

	public string GetLocalizedDisplayName()
	{
		return ThemeStubs.m_Instance.GetLocalizedDisplayName(m_ThemeStub.m_ID);
	}

	public GameObject GetBestTerrainIslandPrefabMatch(TerrainIslandType islandType, int variantIndex)
	{
		GameObject terrainIslandPrefab = GetTerrainIslandPrefab(islandType, variantIndex);
		if (!terrainIslandPrefab)
		{
			terrainIslandPrefab = GetTerrainIslandPrefab(islandType, 0);
		}
		return terrainIslandPrefab;
	}

	public int GetRandomBookendVariant()
	{
		int numTerrainIslandPrefabs = m_Instance.GetNumTerrainIslandPrefabs(TerrainIslandType.Bookend);
		if (numTerrainIslandPrefabs == 0)
		{
			return -1;
		}
		return Random.Range(0, numTerrainIslandPrefabs);
	}

	public int GetRandomBookendVariantWithExclusion(int excludeVariantIndex)
	{
		int numTerrainIslandPrefabs = m_Instance.GetNumTerrainIslandPrefabs(TerrainIslandType.Bookend);
		if (numTerrainIslandPrefabs < 2)
		{
			return -1;
		}
		int num;
		for (num = Random.Range(0, numTerrainIslandPrefabs); num == excludeVariantIndex; num = Random.Range(0, numTerrainIslandPrefabs))
		{
		}
		return num;
	}

	public void EnableBuildModeVolumeProfile()
	{
	}

	public void DisableModeLighting()
	{
		m_BuildModeLight.gameObject.SetActive(value: false);
		m_SandboxModeLight.gameObject.SetActive(value: false);
		m_SecondaryLight.gameObject.SetActive(value: false);
		m_SunLight.gameObject.SetActive(value: false);
		m_BridgeLight.gameObject.SetActive(value: false);
		m_WaterFillLight.gameObject.SetActive(value: false);
	}

	public void EnableSandboxModeLighting()
	{
		m_SandboxModeLight.gameObject.SetActive(value: true);
		m_BuildModeLight.gameObject.SetActive(value: false);
		m_SunLight.gameObject.SetActive(value: false);
		m_SecondaryLight.gameObject.SetActive(value: false);
		m_BridgeLight.gameObject.SetActive(value: false);
		m_WaterFillLight.gameObject.SetActive(value: false);
	}

	public void EnableBuildModeLighting()
	{
		m_BuildModeLight.gameObject.SetActive(value: true);
		m_SandboxModeLight.gameObject.SetActive(value: false);
		m_SecondaryLight.gameObject.SetActive(value: false);
		m_SunLight.gameObject.SetActive(value: false);
		m_BridgeLight.gameObject.SetActive(value: false);
		m_WaterFillLight.gameObject.SetActive(value: false);
	}

	public void EnableSimModeLighting()
	{
		UpdateLightsFromThemeStub();
		m_BuildModeLight.gameObject.SetActive(value: false);
		m_SandboxModeLight.gameObject.SetActive(value: false);
		m_SunLight.gameObject.SetActive(m_ThemeStub.m_SunLight != null);
		m_SecondaryLight.gameObject.SetActive(m_ThemeStub.m_SecondaryLight != null);
		m_BridgeLight.gameObject.SetActive(m_ThemeStub.m_BridgeLight != null);
	}

	public void SetThemeVolume()
	{
		Main.m_Instance.m_PostFX.Set(m_Instance.m_ThemeStub.m_VolumeProfile);
		GameRenderSettings.SetPostFXSettings(Profiles.m_ActiveProfile.m_SSAO, Profiles.m_ActiveProfile.m_Bloom, Profiles.m_ActiveProfile.m_Vignette, Profiles.m_ActiveProfile.m_AntiAliasingQuality);
	}

	public void UpdateGradientSkyFromThemeStub()
	{
		if (Cameras.m_Instance.m_GradientSky != null)
		{
			Object.Destroy(Cameras.m_Instance.m_GradientSky.gameObject);
			Cameras.m_Instance.m_GradientSky = null;
		}
		if (m_ThemeStub.m_GradientSky != null)
		{
			Cameras.m_Instance.m_GradientSky = Object.Instantiate(m_ThemeStub.m_GradientSky, Cameras.m_Instance.m_Main.transform).GetComponent<GradientSky>();
			Cameras.m_Instance.m_GradientSky.m_ThemeStubID = m_ThemeStub.m_ID;
			Cameras.m_Instance.m_GradientSky.Update();
			Cameras.m_Instance.m_GradientSky.gameObject.SetActive(GameStateManager.GetState() == GameState.SIM || GameStateManager.GetState() == GameState.MAIN_MENU);
		}
	}

	public bool FogIsZeroHeight()
	{
		if (m_ThemeStub == null)
		{
			return false;
		}
		if (Mathf.Approximately(m_ThemeStub.m_FogHeightStart, 0f))
		{
			return Mathf.Approximately(m_ThemeStub.m_FogHeightEnd, 0f);
		}
		return false;
	}

	public float GetFogHeightStart()
	{
		if (!(m_ThemeStub == null))
		{
			return m_ThemeStub.m_FogHeightStart;
		}
		return HeightFog.DEFAULT_FOG_HEIGHT_START_MAX_RELATIVE_Y;
	}

	public float GetFogHeightEnd()
	{
		if (!(m_ThemeStub == null))
		{
			return m_ThemeStub.m_FogHeightEnd;
		}
		return HeightFog.DEFAULT_FOG_HEIGHT_END_RELATIVE_Y;
	}

	public void PositionWaterPlane()
	{
		EnableWaterPlane(!SandboxSettings.m_NoWater);
		Vector3 averagePositionOfBookendSpawnPoints = TerrainIslands.GetAveragePositionOfBookendSpawnPoints();
		m_WaterPlane.transform.position = new Vector3(averagePositionOfBookendSpawnPoints.x, WaterBlocks.GetHeight(), 0f);
		UpdateWaterPlaneWidth();
	}

	public void EnableWaterPlane(bool on)
	{
		m_WaterPlane.SetActive(on);
	}

	private void UpdateHeightFogFromThemeStub()
	{
		if ((Object)(object)m_ThemeStub.m_HeightFogGlobalPrefab != null && m_ThemeStub.m_ID != m_LastLoadedHeightFogThemeID)
		{
			HeightFog.Create(m_ThemeStub.m_HeightFogGlobalPrefab);
			HeightFog.ManualUpdate();
			m_LastLoadedHeightFogThemeID = m_ThemeStub.m_ID;
		}
	}

	private void UpdateWaterPlaneFromThemeStub()
	{
		m_WaterPlaneMaterialPropertyBlock.SetFloat("_SpecMin", m_ThemeStub.m_SpecMin);
		m_WaterPlaneMaterialPropertyBlock.SetFloat("_SpecMax", m_ThemeStub.m_SpecMax);
		m_WaterPlaneMaterialPropertyBlock.SetFloat("_Radius_Inner", m_ThemeStub.m_RadiusInner);
		m_WaterPlaneMaterialPropertyBlock.SetFloat("_Radius_Outer", m_ThemeStub.m_RadiusOuter);
		m_WaterPlaneMaterialPropertyBlock.SetColor("_WaterColor", m_ThemeStub.m_WaterColor);
		m_WaterPlaneMaterialPropertyBlock.SetVector("_Specularcolor", m_ThemeStub.m_SpecularColor);
		m_WaterPlaneMaterialPropertyBlock.SetColor("_FoamColor", m_ThemeStub.m_FoamColor);
		m_WaterPlaneMaterialPropertyBlock.SetColor("_ShadowColor", m_ThemeStub.m_ShadowColor);
		m_WaterPlaneMaterialPropertyBlock.SetFloat("_ShadowPower", m_ThemeStub.m_ShadowPower);
		m_WaterPlaneMeshRenderer.SetPropertyBlock(m_WaterPlaneMaterialPropertyBlock);
	}

	public void UpdateWaterPlaneWidth()
	{
		float num = TerrainIslands.DistanceBetweenBookends();
		float value = Mathf.Lerp(m_MinWidth, m_MaxWidth, num / TerrainIslands.MAX_SEPARATION_X);
		m_WaterPlaneMaterialPropertyBlock.SetFloat("_WaterWidth", value);
		m_WaterPlaneMeshRenderer.SetPropertyBlock(m_WaterPlaneMaterialPropertyBlock);
	}

	private void UpdateLightsFromThemeStub()
	{
		UpdateSunLightFromStub();
		UpdateSecondaryLightFromStub();
		UpdateBridgeLightFromStub();
		UpdateWaterFillLightFromStub();
	}

	private void UpdateSunLightFromStub()
	{
		if (m_SunLight != null)
		{
			if (m_ThemeStub.m_SunLight != null)
			{
				CopyLight(m_SunLight, m_ThemeStub.m_SunLight);
				m_SunLight.transform.position = m_ThemeStub.m_SunLight.transform.position;
				m_SunLight.transform.rotation = m_ThemeStub.m_SunLight.transform.rotation;
			}
			m_SunLight.cullingMask |= Utils.DECOR_LAYER_MASK;
			m_SunLight.gameObject.SetActive(value: false);
		}
	}

	private void UpdateSecondaryLightFromStub()
	{
		if (m_SecondaryLight != null)
		{
			if (m_ThemeStub.m_SecondaryLight != null)
			{
				CopyLight(m_SecondaryLight, m_ThemeStub.m_SecondaryLight);
				m_SecondaryLight.transform.position = m_ThemeStub.m_SecondaryLight.transform.position;
				m_SecondaryLight.transform.rotation = m_ThemeStub.m_SecondaryLight.transform.rotation;
			}
			m_SecondaryLight.cullingMask = Utils.TERRAIN_LAYER_MASK;
			m_SecondaryLight.shadows = LightShadows.None;
			m_SecondaryLight.gameObject.SetActive(value: false);
		}
	}

	private void UpdateBridgeLightFromStub()
	{
		if (m_BridgeLight != null)
		{
			if (m_ThemeStub.m_BridgeLight != null)
			{
				CopyLight(m_BridgeLight, m_ThemeStub.m_BridgeLight);
			}
			m_BridgeLight.cullingMask = Utils.SPRING_LAYER_MASK | Utils.JOINT_LAYER_MASK | Utils.EDGE_LAYER_MASK | Utils.PISTON_LAYER_MASK | Utils.VEHICLE_LAYER_MASK;
			m_BridgeLight.shadows = LightShadows.None;
			m_BridgeLight.gameObject.SetActive(value: false);
		}
	}

	private void UpdateWaterFillLightFromStub()
	{
		if (m_WaterFillLight != null)
		{
			if (m_ThemeStub.m_WaterFillLight != null)
			{
				CopyLight(m_WaterFillLight, m_ThemeStub.m_WaterFillLight);
			}
			m_WaterFillLight.cullingMask = Utils.NO_RENDER_LAYER_MASK;
			m_WaterFillLight.shadows = LightShadows.None;
			m_WaterFillLight.gameObject.SetActive(value: false);
		}
	}

	private void CopyLight(Light dest, Light src)
	{
		dest.transform.position = src.transform.position;
		dest.transform.rotation = src.transform.rotation;
		dest.bounceIntensity = src.bounceIntensity;
		dest.color = src.color;
		dest.cookie = src.cookie;
		dest.cookieSize = src.cookieSize;
		dest.cullingMask = src.cullingMask;
		dest.flare = src.flare;
		dest.intensity = src.intensity;
		dest.range = src.range;
		dest.renderMode = src.renderMode;
		dest.shadowBias = src.shadowBias;
		dest.shadowCustomResolution = src.shadowCustomResolution;
		dest.shadowNearPlane = src.shadowNearPlane;
		dest.shadowNormalBias = src.shadowNormalBias;
		dest.shadowResolution = src.shadowResolution;
		dest.shadows = src.shadows;
		dest.shadowStrength = src.shadowStrength;
		dest.spotAngle = src.spotAngle;
		dest.type = src.type;
	}
}
