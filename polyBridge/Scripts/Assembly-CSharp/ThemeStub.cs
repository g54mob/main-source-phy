using System;
using AtmosphericHeightFog;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
[CreateAssetMenu(fileName = "ThemeStub", menuName = "Game/ThemeStub", order = 2)]
public class ThemeStub : ScriptableObject
{
	public string m_ID;

	[Header("Terrain")]
	public float m_DefaultTerrainHeight;

	public Color m_BuildModeHoleColor;

	[Header("Bookend Prefabs")]
	public GameObject[] m_BookendPrefabs;

	[Header("Middle Island Prefabs")]
	public GameObject[] m_MiddleIslandPrefabs;

	[Header("Camera Framing")]
	public bool m_IgnoreDecor;

	public float m_TerrainBoundsScaleX = 1f;

	[Header("New Water")]
	public float m_SpecMin;

	public float m_SpecMax;

	public float m_RadiusInner;

	public float m_RadiusOuter;

	public Color m_WaterColor;

	[ColorUsage(true, true)]
	public Color m_SpecularColor;

	public Color m_FoamColor;

	public Color m_ShadowColor;

	public float m_ShadowPower;

	[Header("Water")]
	public bool m_NoWaterDefault;

	public GameObject m_WaterPrefab;

	public float m_WaterScaleZ;

	public float m_WaterOffsetZ;

	public float m_LeftEdgeOffsetX;

	public float m_RightEdgeOffsetX;

	public Color m_BuildModeWaterColor;

	[Header("Sky")]
	public GameObject m_GradientSky;

	[Header("Fog")]
	public HeightFogGlobal m_HeightFogGlobalPrefab;

	public float m_FogHeightStart = HeightFog.DEFAULT_FOG_HEIGHT_START_MAX_RELATIVE_Y;

	public float m_FogHeightEnd = HeightFog.DEFAULT_FOG_HEIGHT_END_RELATIVE_Y;

	[Header("Visual FX")]
	public VolumeProfile m_VolumeProfile;

	[Range(0f, 1f)]
	public float m_WindowsNoiseThreshold;

	[Header("Lighting")]
	[ColorUsage(true, true)]
	public Color m_AmbientLightColor;

	public Light m_SunLight;

	public Light m_SecondaryLight;

	public Light m_WaterFillLight;

	public Light m_BridgeLight;

	[Header("Audio")]
	public ThemeAudioClip m_AmbientAudio;

	[Header("Time Of Day")]
	public ThemeTimeOfDay m_ThemeTimeOfDay;

	public float m_VehicleNightSpotLightsScale;

	public float m_VehicleNightPointLightsScale;
}
