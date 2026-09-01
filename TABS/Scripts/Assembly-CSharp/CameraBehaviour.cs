using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
	public enum CameraMode
	{
		UpCloseFrontLine = 0,
		MidRangeFrontLine = 1,
		LongRangeFrontLine = 2,
		VaryingZoomFrontLine = 3,
		EntierMap = 4,
		Free = 5
	}

	private Vector3 velocity;

	private float cameraDistanceVelocity;

	private float cameraDistance = 60f;

	public float spring = 1f;

	public float drag = 1f;

	public CameraMode cameraMode;

	public bool rotateCamera;

	public AnimationCurve distanceAngleCurve;

	public Transform redCameraBase;

	public Transform blueCameraBase;

	private float lerpDistance;

	private float switchModeIn = 30f;

	private Transform m_camera;

	private float targetDistance = 100f;

	private Vector3 targetPosition;

	private float cappedTime;

	private void Start()
	{
		m_camera = base.transform.GetChild(0);
	}

	private void Update()
	{
		cappedTime = Mathf.Clamp(Time.deltaTime, 0f, 0.05f);
		DoCameraMode();
		FollowValues();
	}

	private void DoCameraMode()
	{
		switchModeIn -= Time.deltaTime;
		if (switchModeIn < 0f)
		{
			SetRandomCameraMode();
		}
		if (cameraMode == CameraMode.UpCloseFrontLine)
		{
			cameraDistance = 5f;
		}
		if (cameraMode == CameraMode.MidRangeFrontLine)
		{
			cameraDistance = 10f;
		}
		if (cameraMode == CameraMode.LongRangeFrontLine)
		{
			cameraDistance = 20f;
		}
		if (cameraMode == CameraMode.VaryingZoomFrontLine)
		{
			cameraDistance = 2f + Mathf.PerlinNoise(Time.time * 0.1f, Time.time * 0.15f) * 25f;
		}
	}

	private void SetRandomCameraMode()
	{
		switch (Random.Range(0, 3))
		{
		case 0:
			cameraMode = CameraMode.UpCloseFrontLine;
			break;
		case 1:
			cameraMode = CameraMode.MidRangeFrontLine;
			break;
		case 2:
			cameraMode = CameraMode.LongRangeFrontLine;
			break;
		case 3:
			cameraMode = CameraMode.VaryingZoomFrontLine;
			break;
		}
		switchModeIn = Random.Range(10, 50);
	}

	private void SetRotation()
	{
	}

	private void FollowValues()
	{
		velocity += (targetPosition - base.transform.position) * cappedTime * spring * 25f;
		velocity -= velocity * cappedTime * drag * 10f;
		base.transform.position += velocity * cappedTime;
		cameraDistanceVelocity += (0f - (targetDistance + cameraDistance * 0.7f) - m_camera.localPosition.z) * cappedTime * spring * 25f;
		cameraDistanceVelocity -= cameraDistanceVelocity * cappedTime * drag * 10f;
		m_camera.localPosition += Vector3.forward * cameraDistanceVelocity * cappedTime;
		lerpDistance = Mathf.Lerp(lerpDistance, Vector3.Distance(m_camera.position, targetPosition), Time.deltaTime * 0.5f);
		base.transform.rotation = Quaternion.Euler(distanceAngleCurve.Evaluate(lerpDistance), Time.time * 2f, 0f);
	}

	private void SetData()
	{
		_ = Vector3.left * 1000f;
		_ = Vector3.right * 1000f;
	}
}
