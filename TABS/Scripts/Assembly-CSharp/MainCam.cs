using UnityEngine;

public class MainCam : MonoBehaviour
{
	public static MainCam instance;

	public bool isPlacementCam = true;

	[HideInInspector]
	public Camera m_camera;

	public Transform m_cameraRootTransform;

	private void Awake()
	{
		if (instance != null)
		{
			Object.Destroy(this);
			return;
		}
		instance = this;
		m_camera = GetComponent<Camera>();
	}

	private void OnDestroy()
	{
		if (instance == this)
		{
			instance = null;
		}
	}
}
