using UnityEngine;

public class VehicleNightLight
{
	public Light m_Light;

	public float m_OriginalIntensity;

	public VehicleNightLight(Light light)
	{
		m_Light = light;
		m_OriginalIntensity = light.intensity;
	}
}
