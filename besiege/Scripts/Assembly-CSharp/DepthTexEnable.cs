using UnityEngine;

public class DepthTexEnable : MonoBehaviour
{
	public Camera cam;

	private void OnEnable()
	{
		Camera.main.depthTextureMode |= DepthTextureMode.Depth;
	}
}
