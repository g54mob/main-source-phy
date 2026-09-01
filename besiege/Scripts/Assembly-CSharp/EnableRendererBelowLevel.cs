using UnityEngine;

public class EnableRendererBelowLevel : MonoBehaviour
{
	public Camera cam;

	public Renderer ren;

	private void Start()
	{
		if (cam == null)
		{
			cam = Camera.main;
		}
	}

	private void Update()
	{
		ren.enabled = cam.transform.position.y < SingleInstanceFindOnly<AddPiece>.Instance.floorHeight + cam.nearClipPlane;
	}
}
