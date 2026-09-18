using DG.Tweening;
using UnityEngine;

public class CharacterCamerasController : SceneSingleton<CharacterCamerasController>
{
	[SerializeField]
	private Camera mainCamera;

	[SerializeField]
	private Camera movingCamera;

	[Space]
	[SerializeField]
	private float cameraMovementDuration;

	[SerializeField]
	private AnimationCurve cameraMovementDurationCurve;

	public void SetMovingPuzzleCameraTarget(Transform targetTrans, TweenCallback action)
	{
		movingCamera.enabled = true;
		mainCamera.enabled = false;
		TweenController.KillTweens(movingCamera.gameObject);
		TweenController.DOMove(movingCamera.transform, targetTrans.position, cameraMovementDuration, cameraMovementDurationCurve, action);
		TweenController.DORotation(movingCamera.transform, targetTrans.eulerAngles, cameraMovementDuration, cameraMovementDurationCurve);
	}

	public void ResetMovingCamera()
	{
		TweenController.KillTweens(movingCamera.gameObject);
		TweenController.DOLocalMove(movingCamera.transform, Vector3.zero, cameraMovementDuration, cameraMovementDurationCurve, DeactivateMovingCam);
		TweenController.DOLocalRotation(movingCamera.transform, Vector3.zero, cameraMovementDuration, cameraMovementDurationCurve);
	}

	private void DeactivateMovingCam()
	{
		movingCamera.enabled = false;
		mainCamera.enabled = true;
		Singleton<InputManager>.Instance.EnableInput();
	}
}
