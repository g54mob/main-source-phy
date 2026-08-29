using UnityEngine;

namespace INab.Demo
{
	public class Simple3rdCameraFollow : MonoBehaviour
	{
		public bool manualControl;

		public Transform playerTransform;

		public Vector3 offset;

		public bool lookAtTarget = true;

		public float mouseSensitivity = 4f;

		private float currentX;

		private float currentY;

		public float yAngleMin = -50f;

		public float yAngleMax = 50f;

		private void Awake()
		{
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = 60;
		}

		private void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		private void LateUpdate()
		{
			if (!manualControl)
			{
				LateUpdateLogic();
			}
		}

		public void LateUpdateLogic()
		{
			offset.z -= Input.GetAxis("Mouse ScrollWheel") * 2f;
			offset.z = Mathf.Max(0.1f, offset.z);
			currentX += Input.GetAxis("Mouse X") * mouseSensitivity;
			currentY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
			currentY = Mathf.Clamp(currentY, yAngleMin, yAngleMax);
			if (playerTransform != null)
			{
				Vector3 vector = new Vector3(0f, 0f, 0f - offset.z);
				Quaternion quaternion = Quaternion.Euler(currentY, currentX, 0f);
				base.transform.position = playerTransform.position + quaternion * vector;
				if (lookAtTarget)
				{
					base.transform.LookAt(playerTransform.position);
				}
				else
				{
					base.transform.rotation = quaternion;
				}
			}
		}
	}
}
