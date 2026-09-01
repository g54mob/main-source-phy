using UnityEngine;

public class SortRenderQueue : MonoBehaviour
{
	private Renderer currentMesh;

	[Range(-100f, 100f)]
	public int renderQueue;

	private void Start()
	{
		currentMesh = GetComponent<Renderer>();
		if (currentMesh == null)
		{
			Debug.LogError("Object doesn't contains a mesh renderer component");
		}
		currentMesh.sortingOrder = renderQueue;
	}

	private void Update()
	{
		if (currentMesh.sortingOrder != renderQueue)
		{
			currentMesh.sortingOrder = renderQueue;
		}
	}
}
