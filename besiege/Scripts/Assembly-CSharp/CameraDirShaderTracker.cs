using UnityEngine;

[ExecuteInEditMode]
public class CameraDirShaderTracker : MonoBehaviour
{
	public Camera cam;

	private void Awake()
	{
		if (cam == null)
		{
			cam = Camera.main;
		}
	}

	private void Update()
	{
		if (cam != null)
		{
			Shader.SetGlobalVector("_CameraDir", cam.transform.forward);
		}
	}
}
