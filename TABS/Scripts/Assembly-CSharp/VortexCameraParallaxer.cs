using UnityEngine;

public class VortexCameraParallaxer : MonoBehaviour
{
	public Vector3 offset;

	private void Update()
	{
		base.transform.position = MainCam.instance.transform.position + offset;
	}
}
