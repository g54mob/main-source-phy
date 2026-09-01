using UnityEngine;
using UnityEngine.Rendering;

public class SetSceneSettings : MonoBehaviour
{
	public Color color;

	private void Start()
	{
		RenderSettings.ambientEquatorColor = color;
		RenderSettings.ambientGroundColor = color;
		RenderSettings.ambientSkyColor = color;
		RenderSettings.ambientMode = AmbientMode.Trilight;
	}
}
