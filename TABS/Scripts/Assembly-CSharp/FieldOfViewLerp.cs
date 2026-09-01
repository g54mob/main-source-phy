using UnityEngine;

public class FieldOfViewLerp : MonoBehaviour
{
	public float targetFOV = 60f;

	public float lerpFactor = 5f;

	private Camera cam;

	private void Awake()
	{
		cam = GetComponent<Camera>();
	}

	private void Update()
	{
		cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * lerpFactor);
	}
}
