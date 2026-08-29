using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class EditorWater : MonoBehaviour
{
	public enum WaterGeometryType
	{
		Plane = 0,
		Circle = 1
	}

	public WaterSurface waterSurface;

	public MeshRenderer plane;

	public MeshRenderer circle;

	public WaterData data;

	public void syncVisuals()
	{
		waterSurface.timeMultiplier = data.timeScale;
		waterSurface.ripplesCurrentSpeedValue = data.rippleSpeed;
		waterSurface.ripplesChaos = data.chaos;
		waterSurface.ripplesWindSpeed = data.localWindSpeed;
		waterSurface.refractionColor = data.refractionColor;
		waterSurface.scatteringColor = data.waterColor;
		waterSurface.ambientScattering = data.ambientIntensity;
		waterSurface.meshRenderers.Clear();
		if (data.geometryType == WaterGeometryType.Plane)
		{
			waterSurface.meshRenderers.Add(plane);
		}
		if (data.geometryType == WaterGeometryType.Circle)
		{
			waterSurface.meshRenderers.Add(circle);
		}
	}
}
