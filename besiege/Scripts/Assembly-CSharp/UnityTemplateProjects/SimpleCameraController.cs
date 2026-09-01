using System.Collections;
using UnityEngine;

namespace UnityTemplateProjects
{
	public class SimpleCameraController : MonoBehaviour
	{
		private class CameraState
		{
			public float yaw;

			public float pitch;

			public float roll;

			public float x;

			public float y;

			public float z;

			public void SetFromTransform(Transform t)
			{
				pitch = t.eulerAngles.x;
				yaw = t.eulerAngles.y;
				roll = t.eulerAngles.z;
				x = t.position.x;
				y = t.position.y;
				z = t.position.z;
			}

			public void Translate(Vector3 translation)
			{
				Vector3 vector = Quaternion.Euler(pitch, yaw, roll) * translation;
				x += vector.x;
				y += vector.y;
				z += vector.z;
			}

			public void LerpTowards(CameraState target, float positionLerpPct, float rotationLerpPct)
			{
				yaw = Mathf.Lerp(yaw, target.yaw, rotationLerpPct);
				pitch = Mathf.Lerp(pitch, target.pitch, rotationLerpPct);
				roll = Mathf.Lerp(roll, target.roll, rotationLerpPct);
				x = Mathf.Lerp(x, target.x, positionLerpPct);
				y = Mathf.Lerp(y, target.y, positionLerpPct);
				z = Mathf.Lerp(z, target.z, positionLerpPct);
			}

			public void UpdateTransform(Transform t)
			{
				t.eulerAngles = new Vector3(pitch, yaw, roll);
				t.position = new Vector3(x, y, z);
			}
		}

		private CameraState m_TargetCameraState = new CameraState();

		private CameraState m_InterpolatingCameraState = new CameraState();

		[Tooltip("Exponential boost factor on translation, controllable by mouse wheel.")]
		[Header("Movement Settings")]
		public float boost = 3.5f;

		[Tooltip("Time it takes to interpolate camera position 99% of the way to the target.")]
		[Range(0.001f, 3f)]
		public float positionLerpTime = 0.2f;

		[Header("Rotation Settings")]
		[Tooltip("X = Change in mouse position.\nY = Multiplicative factor for camera rotation.")]
		public AnimationCurve mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

		[Range(0.001f, 1f)]
		[Tooltip("Time it takes to interpolate camera rotation 99% of the way to the target.")]
		public float rotationLerpTime = 0.01f;

		[Tooltip("Whether or not to invert our Y axis for mouse input to rotation.")]
		public bool invertY;

		private float speed;

		private void OnEnable()
		{
			m_TargetCameraState.SetFromTransform(base.transform);
			m_InterpolatingCameraState.SetFromTransform(base.transform);
		}

		private void Start()
		{
			if (Application.isPlaying)
			{
				StopAllCoroutines();
				StartCoroutine(AnimateTimeScale(0f, 0.1f, 4f));
				StartCoroutine(AnimateRoll(20f));
			}
		}

		private IEnumerator AnimateTimeScale(float start, float end, float duration)
		{
			Time.timeScale = 0f;
			yield return new WaitForSecondsRealtime(1f);
			for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
			{
				float pct = t / duration;
				Time.timeScale = Mathf.Lerp(start, end, pct);
				yield return null;
			}
			Time.timeScale = end;
			speed = 1f;
		}

		private IEnumerator AnimateRoll(float duration)
		{
			float start = base.transform.eulerAngles.z;
			yield return new WaitForSecondsRealtime(1f);
			float end = 0f;
			for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
			{
				float pct = t / duration;
				base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, base.transform.eulerAngles.y, Mathf.Lerp(start, end, Mathf.Pow(pct, 3f)));
				speed = Mathf.Clamp01(Mathf.Pow(pct + pct, 2f));
				yield return null;
			}
			while (true)
			{
				base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, base.transform.eulerAngles.y, end);
				yield return null;
			}
		}

		private Vector3 GetInputTranslationDirection()
		{
			Vector3 vector = default(Vector3);
			if (Input.GetKey(KeyCode.W))
			{
				vector += Vector3.forward;
			}
			if (Input.GetKey(KeyCode.S))
			{
				vector += Vector3.back;
			}
			if (Input.GetKey(KeyCode.A))
			{
				vector += Vector3.left * 0.7f;
			}
			if (Input.GetKey(KeyCode.D))
			{
				vector += Vector3.right * 0.7f;
			}
			if (Input.GetKey(KeyCode.Q))
			{
				vector += Vector3.down * 0.7f;
			}
			if (Input.GetKey(KeyCode.E))
			{
				vector += Vector3.up * 0.7f;
			}
			return vector * speed;
		}

		private void Update()
		{
			Vector3 zero = Vector3.zero;
			if (Input.GetKey(KeyCode.Escape))
			{
				Application.Quit();
			}
			if (Input.GetMouseButtonDown(1))
			{
				Cursor.lockState = CursorLockMode.Locked;
			}
			if (Input.GetMouseButtonUp(1))
			{
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
			}
			if (Input.GetMouseButton(1))
			{
				Vector2 vector = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (float)(invertY ? 1 : (-1)));
				float num = mouseSensitivityCurve.Evaluate(vector.magnitude);
				m_TargetCameraState.yaw += vector.x * num;
				m_TargetCameraState.pitch += vector.y * num;
			}
			zero = GetInputTranslationDirection() * Time.unscaledDeltaTime;
			if (Input.GetKey(KeyCode.LeftShift))
			{
				zero *= 10f;
			}
			boost += Input.mouseScrollDelta.y * 0.2f;
			zero *= Mathf.Pow(2f, boost);
			m_TargetCameraState.Translate(zero);
			float positionLerpPct = 1f - Mathf.Exp(Mathf.Log(0.00999999f) / positionLerpTime * Time.unscaledDeltaTime);
			float rotationLerpPct = 1f - Mathf.Exp(Mathf.Log(0.00999999f) / rotationLerpTime * Time.unscaledDeltaTime);
			m_InterpolatingCameraState.LerpTowards(m_TargetCameraState, positionLerpPct, rotationLerpPct);
			m_InterpolatingCameraState.UpdateTransform(base.transform);
		}
	}
}
