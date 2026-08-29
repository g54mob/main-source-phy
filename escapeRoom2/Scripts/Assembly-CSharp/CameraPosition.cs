using System;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
	private float horizontalFoV = 60f;

	private Camera _camera;

	private void Start()
	{
		_camera = GetComponent<Camera>();
	}

	private void Update()
	{
		float f = Mathf.Tan(0.856217f * horizontalFoV * (MathF.PI / 180f)) * (float)Screen.height / (float)Screen.width;
		float fieldOfView = 2f * Mathf.Atan(f) * 57.29578f;
		_camera.fieldOfView = fieldOfView;
	}
}
