using UnityEngine;

namespace INab.Demo
{
	[ExecuteAlways]
	public class ScaleOscillate : MonoBehaviour
	{
		public AnimationCurve scaleCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f), new Keyframe(1f, 0f));

		public Vector3 minScale = new Vector3(0.3f, 0.3f, 0.3f);

		public Vector3 maxScale = new Vector3(1f, 1f, 1f);

		public float oscillationSpeed = 0.4f;

		private float elapsedTime;

		public bool updateInEditor;

		private void Update()
		{
			if (updateInEditor || Application.isPlaying)
			{
				elapsedTime += Time.deltaTime * oscillationSpeed;
				elapsedTime = Mathf.Repeat(elapsedTime, 1f);
				float t = scaleCurve.Evaluate(elapsedTime);
				base.transform.localScale = Vector3.Lerp(minScale, maxScale, t);
			}
		}
	}
}
