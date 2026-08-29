using UnityEngine;

public class Controller : MonoBehaviour
{
	public Camera _camera;

	public void Update()
	{
		if (Input.GetButton("Fire1"))
		{
			base.transform.Rotate(0f, Input.GetAxis("Mouse X"), 0f);
		}
		if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
		{
			_camera.transform.Translate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f);
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0f)
		{
			_camera.transform.Translate(0f, 0f, -5f);
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0f)
		{
			_camera.transform.Translate(0f, 0f, 5f);
		}
	}
}
