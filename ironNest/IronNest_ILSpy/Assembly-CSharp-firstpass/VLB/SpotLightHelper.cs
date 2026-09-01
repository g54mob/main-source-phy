using UnityEngine;

namespace VLB;

public static class SpotLightHelper
{
	public static float GetIntensity(Light light)
	{
		//IL_0044: Expected F4, but got I4
		if (light != null)
		{
			return light.intensity;
		}
		return 0f;
	}

	public static float GetSpotAngle(Light light)
	{
		//IL_0044: Expected F4, but got I4
		if (light != null)
		{
			return light.spotAngle;
		}
		return 0f;
	}

	public static float GetFallOffEnd(Light light)
	{
		//IL_0044: Expected F4, but got I4
		if (light != null)
		{
			return light.range;
		}
		return 0f;
	}
}
