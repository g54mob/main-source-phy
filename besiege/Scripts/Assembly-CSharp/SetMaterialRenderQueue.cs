using UnityEngine;

public class SetMaterialRenderQueue : MonoBehaviour
{
	public int renderQueuey = 3000;

	private void Start()
	{
		GetComponent<Renderer>().material.renderQueue = renderQueuey;
	}
}
