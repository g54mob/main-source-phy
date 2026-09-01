using InControl;
using UnityEngine;

namespace LevelCreator
{
	public class CameraScript : MonoBehaviour
	{
		public float maxCameraLean = 89.9f;

		private Vector3 targetPosition;

		private Quaternion targetRotation;

		private float velocityLerpSpeed;

		private float rotationLerpSpeed;

		private float shakeDuration = 0.5f;

		private float shakeTimer = float.PositiveInfinity;

		private float shakeForce = 1f;

		private float currentForce;

		private InControlInputModule inputModule;

		private Transform cameraRoot;

		private void Start()
		{
			inputModule = Object.FindObjectOfType<InControlInputModule>();
			cameraRoot = base.transform.parent.parent;
			targetPosition = cameraRoot.position;
			targetRotation = cameraRoot.rotation;
		}

		private void Update()
		{
			shakeTimer += Time.deltaTime;
			currentForce = Mathf.Lerp(shakeForce, 0f, shakeTimer / shakeDuration);
			if (currentForce > 0f)
			{
				inputModule.Device.Vibrate(currentForce);
			}
			cameraRoot.position = Vector3.Lerp(targetPosition + Random.insideUnitSphere * currentForce, cameraRoot.position, Mathf.Pow(velocityLerpSpeed, Time.deltaTime));
			cameraRoot.rotation = Quaternion.Lerp(targetRotation, cameraRoot.rotation, Mathf.Pow(rotationLerpSpeed, Time.deltaTime));
		}

		public void SetTarget(Vector3 position, Quaternion rotation, float velocityLerpSpeed, float rotationLerpSpeed)
		{
			targetPosition = position;
			targetRotation = rotation;
			this.velocityLerpSpeed = velocityLerpSpeed;
			this.rotationLerpSpeed = rotationLerpSpeed;
		}

		public void SetTarget(Transform transform, float velocityLerpSpeed, float rotationLerpSpeed)
		{
			SetTarget(transform.position, transform.rotation, velocityLerpSpeed, rotationLerpSpeed);
		}

		public void SetTarget(Vector3 position, Vector3 eulerAngles, float velocityLerpSpeed, float rotationLerpSpeed)
		{
			SetTarget(position, Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z), velocityLerpSpeed, rotationLerpSpeed);
		}

		private void OnPostRender()
		{
			LandscapeShaper[] componentsInChildren = GetComponentsInChildren<LandscapeShaper>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].OnPostRender();
			}
		}
	}
}
