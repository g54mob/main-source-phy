using TFBGames;
using UnityEngine;

public class ExposureVolume : MonoBehaviour
{
	public Collider[] colliders;

	public float exposure;

	public bool inVolume;

	private Transform m_cameraTransform;

	private void OnEnable()
	{
		ExposureVolumeManager.RegisterVolume(this);
	}

	private void OnDisable()
	{
		ExposureVolumeManager.UnregisterVolume(this);
	}

	private void FindCameraTransform()
	{
		MainCam mainCam = ServiceLocator.GetService<PlayerCamerasManager>()?.GetMainCam(TFBGames.Player.One);
		m_cameraTransform = ((mainCam != null) ? mainCam.transform : null);
	}

	private void Update()
	{
		if (m_cameraTransform == null)
		{
			FindCameraTransform();
			return;
		}
		inVolume = false;
		for (int i = 0; i < colliders.Length; i++)
		{
			if (!(colliders[i] == null) && Vector3.Distance(colliders[i].ClosestPoint(m_cameraTransform.position), m_cameraTransform.position) <= 0.2f)
			{
				inVolume = true;
				break;
			}
		}
	}
}
