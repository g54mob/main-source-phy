using System.Collections;
using UnityEngine;

namespace RuntimeSceneGizmo
{
	public class CameraGizmoListener : MonoBehaviour
	{
		[SerializeField]
		private float cameraAdjustmentSpeed = 3f;

		[SerializeField]
		private float projectionTransitionSpeed = 2f;

		private Camera mainCamera;

		private Transform mainCamParent;

		private IEnumerator cameraRotateCoroutine;

		private IEnumerator projectionChangeCoroutine;

		private void Awake()
		{
			mainCamera = Camera.main;
			mainCamParent = mainCamera.transform.parent;
		}

		private void OnDisable()
		{
			cameraRotateCoroutine = (projectionChangeCoroutine = null);
		}

		public void OnGizmoComponentClicked(GizmoComponent component)
		{
			switch (component)
			{
			case GizmoComponent.Center:
				SwitchOrthographicMode();
				break;
			case GizmoComponent.XNegative:
				RotateCameraInDirection(Vector3.right);
				break;
			case GizmoComponent.XPositive:
				RotateCameraInDirection(-Vector3.right);
				break;
			case GizmoComponent.YNegative:
				RotateCameraInDirection(Vector3.up);
				break;
			case GizmoComponent.YPositive:
				RotateCameraInDirection(-Vector3.up);
				break;
			case GizmoComponent.ZNegative:
				RotateCameraInDirection(Vector3.forward);
				break;
			default:
				RotateCameraInDirection(-Vector3.forward);
				break;
			}
		}

		public void SwitchOrthographicMode()
		{
			if (projectionChangeCoroutine == null)
			{
				projectionChangeCoroutine = SwitchProjection();
				StartCoroutine(projectionChangeCoroutine);
			}
		}

		public void RotateCameraInDirection(Vector3 direction)
		{
			if (cameraRotateCoroutine == null)
			{
				cameraRotateCoroutine = SetCameraRotation(direction);
				StartCoroutine(cameraRotateCoroutine);
			}
		}

		private IEnumerator SwitchProjection()
		{
			bool isOrthographic = mainCamera.orthographic;
			Matrix4x4 src = mainCamera.projectionMatrix;
			Matrix4x4 dest;
			if (isOrthographic)
			{
				dest = Matrix4x4.Perspective(mainCamera.fieldOfView, mainCamera.aspect, mainCamera.nearClipPlane, mainCamera.farClipPlane);
			}
			else
			{
				float orthographicSize = mainCamera.orthographicSize;
				float aspect = mainCamera.aspect;
				dest = Matrix4x4.Ortho((0f - orthographicSize) * aspect, orthographicSize * aspect, 0f - orthographicSize, orthographicSize, mainCamera.nearClipPlane, mainCamera.farClipPlane);
			}
			for (float t = 0f; t < 1f; t += Time.unscaledDeltaTime * projectionTransitionSpeed)
			{
				float t2 = (isOrthographic ? (t * t) : Mathf.Pow(t, 0.2f));
				Matrix4x4 projectionMatrix = default(Matrix4x4);
				for (int i = 0; i < 16; i++)
				{
					projectionMatrix[i] = Mathf.LerpUnclamped(src[i], dest[i], t2);
				}
				mainCamera.projectionMatrix = projectionMatrix;
				yield return null;
			}
			mainCamera.orthographic = !isOrthographic;
			mainCamera.ResetProjectionMatrix();
			projectionChangeCoroutine = null;
		}

		private IEnumerator SetCameraRotation(Vector3 targetForward)
		{
			Quaternion initialRotation = mainCamParent.localRotation;
			Quaternion targetRotation;
			if (Mathf.Abs(targetForward.y) < 0.99f)
			{
				targetRotation = Quaternion.LookRotation(targetForward);
			}
			else
			{
				Vector3 vector = mainCamParent.forward;
				if (vector.x == 0f && vector.z == 0f)
				{
					vector.y = 1f;
				}
				else if (Mathf.Abs(vector.x) > Mathf.Abs(vector.z))
				{
					vector.x = Mathf.Sign(vector.x);
					vector.y = 0f;
					vector.z = 0f;
				}
				else
				{
					vector.x = 0f;
					vector.y = 0f;
					vector.z = Mathf.Sign(vector.z);
				}
				if (targetForward.y > 0f)
				{
					vector = -vector;
				}
				targetRotation = Quaternion.LookRotation(targetForward, vector);
			}
			for (float t = 0f; t < 1f; t += Time.unscaledDeltaTime * cameraAdjustmentSpeed)
			{
				mainCamParent.localRotation = Quaternion.LerpUnclamped(initialRotation, targetRotation, t);
				yield return null;
			}
			mainCamParent.localRotation = targetRotation;
			cameraRotateCoroutine = null;
		}
	}
}
