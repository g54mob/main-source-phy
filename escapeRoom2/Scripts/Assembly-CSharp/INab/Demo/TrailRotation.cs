using UnityEngine;

namespace INab.Demo
{
	[ExecuteAlways]
	public class TrailRotation : MonoBehaviour
	{
		public Vector3 defaultRotation = new Vector3(0f, 0f, 0f);

		public bool enableRotation = true;

		public float rotationSpeed = 700f;

		public Vector3 rotationAxis = Vector3.up;

		public float pauseDuration = 1f;

		public float smoothDamp = 2f;

		private float anglePassed;

		private float pauseTimer;

		private bool isRotating = true;

		private float currentVelocity;

		private float currentRotationSpeed;

		public bool updateInEditor;

		private void Update()
		{
			if (!enableRotation)
			{
				base.transform.eulerAngles = defaultRotation;
				anglePassed = 0f;
			}
			else
			{
				if (!updateInEditor && !Application.isPlaying)
				{
					return;
				}
				if (isRotating)
				{
					currentRotationSpeed = Mathf.SmoothDamp(currentRotationSpeed, rotationSpeed, ref currentVelocity, smoothDamp, 1000f);
					float num = currentRotationSpeed * Time.deltaTime;
					anglePassed += num;
					base.transform.Rotate(rotationAxis, num);
					if (anglePassed >= 360f)
					{
						currentRotationSpeed = 0f;
						anglePassed %= 360f;
						isRotating = false;
					}
				}
				else
				{
					pauseTimer += Time.deltaTime;
					if (pauseTimer >= pauseDuration)
					{
						isRotating = true;
						pauseTimer = 0f;
					}
				}
			}
		}

		public void ToggleRotation()
		{
			enableRotation = !enableRotation;
			isRotating = enableRotation;
			pauseTimer = 0f;
		}
	}
}
