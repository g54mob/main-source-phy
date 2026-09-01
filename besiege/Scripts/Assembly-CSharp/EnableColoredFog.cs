using UnityEngine;

public class EnableColoredFog : MonoBehaviour
{
	public ColorfulFog fog;

	public ColorfulFog sourceValues;

	private Camera Cam;

	private void Start()
	{
		Cam = Camera.main;
		if (fog == null)
		{
			fog = Cam.GetComponent<ColorfulFog>();
		}
		fog.enabled = true;
		if ((bool)sourceValues)
		{
			fog.useCustomDepthTexture = sourceValues.useCustomDepthTexture;
			fog.distanceFog = sourceValues.distanceFog;
			fog.heightFog = sourceValues.heightFog;
			fog.height = sourceValues.height;
			fog.heightDensity = sourceValues.heightDensity;
			fog.fogMode = sourceValues.fogMode;
			fog.fogDensity = sourceValues.fogDensity;
			fog.useRadialDistance = sourceValues.useRadialDistance;
			fog.startDistance = sourceValues.startDistance;
			fog.coloringMode = sourceValues.coloringMode;
			fog.fogCube = sourceValues.fogCube;
			fog.skyColor = sourceValues.skyColor;
			fog.equatorColor = sourceValues.equatorColor;
			fog.groundColor = sourceValues.groundColor;
			fog.solidColor = sourceValues.solidColor;
			fog.gradient = sourceValues.gradient;
			fog.gradientTexture = sourceValues.gradientTexture;
			if (base.gameObject.tag == "Special")
			{
				Cam.renderingPath = RenderingPath.Forward;
			}
			Object.Destroy(sourceValues.gameObject);
		}
		if (WaterController.Exist)
		{
			return;
		}
		FogVolume[] array = Object.FindObjectsOfType<FogVolume>();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].enabled && array[i].gameObject.activeInHierarchy)
			{
				return;
			}
		}
		Color color = Color.black;
		float num = 500f;
		float value = 1000f;
		if (fog.distanceFog)
		{
			switch (fog.coloringMode)
			{
			case ColorfulFog.ColoringMode.Solid:
				color = fog.solidColor;
				break;
			case ColorfulFog.ColoringMode.Cube:
				color = Color.gray;
				break;
			}
			num = fog.startDistance;
			value = num + 250f;
		}
		Shader.SetGlobalColor("_FogVolumeColor", color);
		Shader.SetGlobalColor("_FogInscatteringColor", Color.black);
		Shader.SetGlobalFloat("_FogVolumeMin", num);
		Shader.SetGlobalFloat("_FogVolumeMax", value);
		Shader.SetGlobalVector("_FogLightDir", Vector3.forward);
	}
}
