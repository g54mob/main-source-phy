using UnityEngine;

public class FovController : MonoBehaviour
{
	public float defaultFov = 41f;

	public float fovToBe = 41f;

	public Camera cam;

	private void Update()
	{
		cam.fieldOfView = fovToBe;
	}
}
