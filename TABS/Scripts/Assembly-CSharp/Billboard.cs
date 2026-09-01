using TFBGames;
using UnityEngine;

public class Billboard : MonoBehaviour
{
	private Transform m_mainCamTransform;

	private void Awake()
	{
		LateUpdate();
	}

	private void Start()
	{
		GetCamera();
		LateUpdate();
	}

	private void GetCamera()
	{
		MainCam mainCam = ServiceLocator.GetService<PlayerCamerasManager>()?.GetMainCam(TFBGames.Player.One);
		m_mainCamTransform = ((mainCam != null) ? mainCam.transform : null);
	}

	private void LateUpdate()
	{
		if (m_mainCamTransform == null)
		{
			GetCamera();
		}
		if (m_mainCamTransform != null)
		{
			base.transform.LookAt(m_mainCamTransform.position);
		}
	}
}
