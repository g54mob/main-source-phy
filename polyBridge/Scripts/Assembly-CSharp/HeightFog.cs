using AtmosphericHeightFog;
using UnityEngine;

public class HeightFog
{
	public static HeightFogGlobal m_HeightFogGlobal;

	public static readonly float MIN_FOG_HEIGHT_END_RELATIVE_Y = 0.1f;

	public static readonly int MAX_HEIGHT = 150;

	public static float DEFAULT_FOG_HEIGHT_START_MIN_WORLD_Y = 0f;

	public static float DEFAULT_FOG_HEIGHT_START_MAX_RELATIVE_Y = -0.5f;

	public static float DEFAULT_FOG_HEIGHT_END_RELATIVE_Y = 0.5f;

	public static int FOG_RENDER_PRIORITY = -1;

	public static bool m_AutomaticFogHeightStart;

	public static void Create(HeightFogGlobal prefab)
	{
		Destroy();
		m_HeightFogGlobal = Object.Instantiate<HeightFogGlobal>(prefab);
		m_HeightFogGlobal.mainCamera = Cameras.MainCamera();
		m_HeightFogGlobal.renderPriority = FOG_RENDER_PRIORITY;
		Enable(on: false);
	}

	public static void Enable(bool on)
	{
		if ((Object)(object)m_HeightFogGlobal != null)
		{
			((Component)(object)m_HeightFogGlobal).gameObject.SetActive(on);
			m_HeightFogGlobal.renderPriority = FOG_RENDER_PRIORITY;
		}
	}

	public static void Destroy()
	{
		if ((Object)(object)m_HeightFogGlobal != null)
		{
			((Component)(object)m_HeightFogGlobal).gameObject.SetActive(value: false);
			Object.Destroy(((Component)(object)m_HeightFogGlobal).gameObject);
			m_HeightFogGlobal = null;
		}
	}

	public static void SetDirectionalLight(Light light)
	{
		if ((Object)(object)m_HeightFogGlobal != null)
		{
			m_HeightFogGlobal.mainDirectional = light;
		}
	}

	public static void ManualUpdate()
	{
		if ((Object)(object)m_HeightFogGlobal != null)
		{
			m_HeightFogGlobal.UpdateManual();
		}
	}

	public static Color GetStartColor()
	{
		if ((Object)(object)m_HeightFogGlobal != null)
		{
			return m_HeightFogGlobal.fogColorStart;
		}
		return Color.grey;
	}

	public static void UpdateProperties()
	{
		if ((Object)(object)m_HeightFogGlobal != null)
		{
			float pitch = Cameras.GetPitch();
			if (SandboxSettings.m_NoWater)
			{
				SetHeightStart(0f);
			}
			else if (m_AutomaticFogHeightStart)
			{
				float t = Mathf.Clamp01(pitch / Cameras.GetMaxPitch());
				float value = ((WaterBlocks.GetHeight() > 1.001f) ? (WaterBlocks.GetHeight() - 1f) : (WaterBlocks.GetHeight() / 2f));
				SetHeightStart(Mathf.SmoothStep(0f, Mathf.Clamp(value, 0f, WaterBlocks.GetHeight()), t));
			}
			else
			{
				float t2 = Mathf.Clamp01(pitch / Cameras.GetMaxPitch());
				float to = Mathf.Clamp(SandboxSettings.m_FogHeightMaxWorldY, SandboxSettings.m_FogHeightMinWorldY, WaterBlocks.GetHeight());
				SetHeightStart(Mathf.SmoothStep(SandboxSettings.m_FogHeightMinWorldY, to, t2));
			}
			float num = (SandboxSettings.m_NoWater ? 0f : WaterBlocks.GetHeight());
			float num2 = Mathf.Clamp(SandboxSettings.m_FogHeightEndRelativeY, MIN_FOG_HEIGHT_END_RELATIVE_Y, float.MaxValue);
			SetHeightEnd(Mathf.Clamp(num + num2, MIN_FOG_HEIGHT_END_RELATIVE_Y, float.MaxValue));
			float t3 = Mathf.Clamp01(pitch / 5f);
			SetIntensity(Mathf.Lerp(0.7f, 1f, t3));
		}
	}

	private static void SetHeightStart(float height)
	{
		if ((Object)(object)m_HeightFogGlobal != null)
		{
			m_HeightFogGlobal.fogHeightStart = height;
		}
	}

	private static void SetHeightEnd(float height)
	{
		if ((Object)(object)m_HeightFogGlobal != null)
		{
			m_HeightFogGlobal.fogHeightEnd = height;
		}
	}

	private static void SetIntensity(float normalizedIntensity)
	{
		if ((Object)(object)m_HeightFogGlobal != null)
		{
			m_HeightFogGlobal.fogIntensity = normalizedIntensity;
		}
	}
}
