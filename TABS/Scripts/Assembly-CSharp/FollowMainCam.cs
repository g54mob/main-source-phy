using TFBGames;
using UnityEngine;

public class FollowMainCam : MonoBehaviour
{
	public Vector3 offset;

	public bool m_followPosition = true;

	public bool m_followRotation = true;

	private PlayerCamerasManager playerCameras;

	private Transform mainCamTransform;

	private void Start()
	{
		GetMainCamera();
	}

	private void GetMainCamera()
	{
		playerCameras = ServiceLocator.GetService<PlayerCamerasManager>();
		MainCam mainCam = playerCameras?.GetMainCam(TFBGames.Player.One);
		mainCamTransform = ((mainCam != null) ? mainCam.transform : null);
	}

	private void LateUpdate()
	{
		if (!mainCamTransform)
		{
			GetMainCamera();
			return;
		}
		if (m_followPosition)
		{
			base.transform.position = mainCamTransform.TransformPoint(offset);
		}
		if (m_followRotation)
		{
			base.transform.rotation = mainCamTransform.rotation;
		}
	}
}
