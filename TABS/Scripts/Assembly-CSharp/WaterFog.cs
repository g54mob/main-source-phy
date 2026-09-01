using UnityEngine;

[ExecuteInEditMode]
public class WaterFog : MonoBehaviour
{
	private Material mat;

	private void OnEnable()
	{
		if (mat == null)
		{
			mat = GetComponent<Renderer>().sharedMaterial;
		}
		mat.EnableKeyword("OBJECT_FOG");
	}

	private void OnDisable()
	{
		mat.DisableKeyword("OBJECT_FOG");
	}
}
