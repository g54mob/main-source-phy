using System;
using UnityEngine;

namespace INab.Demo
{
	[ExecuteAlways]
	public class EllipseFloat : MonoBehaviour
	{
		public Transform centralObject;

		public float rotationSpeed = 50f;

		public float horizontalRadius = 2f;

		public float verticalRadius = 1f;

		public float minVerticalOffset;

		public float maxVerticalOffset = 2f;

		public AnimationCurve scaleCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f), new Keyframe(1f, 0f));

		private float verticalOffset;

		public bool updateInEditor;

		private void Update()
		{
			if (updateInEditor || Application.isPlaying)
			{
				float num = Time.time * rotationSpeed;
				Vector3 positionOnEllipse = GetPositionOnEllipse(num);
				float num2 = Mathf.Repeat(num, 360f);
				num2 /= 360f;
				num2 = scaleCurve.Evaluate(num2);
				verticalOffset = Mathf.Lerp(minVerticalOffset, maxVerticalOffset, num2);
				base.transform.position = positionOnEllipse;
				base.transform.LookAt(centralObject);
			}
		}

		private Vector3 GetPositionOnEllipse(float angle)
		{
			float x = Mathf.Cos(MathF.PI / 180f * angle) * horizontalRadius;
			float z = Mathf.Sin(MathF.PI / 180f * angle) * verticalRadius;
			return centralObject.position + new Vector3(x, verticalOffset, z);
		}
	}
}
