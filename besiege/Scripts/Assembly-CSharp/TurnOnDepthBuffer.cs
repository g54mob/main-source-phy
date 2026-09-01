using UnityEngine;

public class TurnOnDepthBuffer : MonoBehaviour
{
	private void Start()
	{
		GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
	}
}
