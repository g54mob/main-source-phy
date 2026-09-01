using Landfall.TABS;
using UnityEngine;

public class ApplyMapGroundMaterial : MonoBehaviour
{
	private void Start()
	{
		Material material = null;
		if ((bool)MapSettings.Instance)
		{
			material = MapSettings.Instance.mapGroundMaterial;
		}
		if ((bool)material)
		{
			GetComponent<Renderer>().sharedMaterial = material;
		}
	}
}
