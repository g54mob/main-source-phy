using UnityEngine;

namespace Misc
{
	public class FloorMaterialGrabber : MonoBehaviour
	{
		private void Start()
		{
			GameObject gameObject = GameObject.Find("FloorBig");
			if (!gameObject)
			{
				Debug.LogWarning("Floor not found");
				return;
			}
			Renderer component = gameObject.GetComponent<Renderer>();
			MeshRenderer component2 = GetComponent<MeshRenderer>();
			component2.material.color = component.material.color;
		}

		private void OnDestroy()
		{
			Object.Destroy(GetComponent<MeshRenderer>().material);
		}
	}
}
