using UnityEngine;

namespace LevelCreator
{
	public class LightController : MonoBehaviour
	{
		private Light lightComponent;

		public float destroyDelay;

		public float lerpSpeed;

		[Space]
		public AnimationCurve rangeInitial;

		private AnimationCurve range;

		public float rangeMultiplier = 1f;

		public AnimationCurve intensityInitial;

		private AnimationCurve intensity;

		public float intensityMultiplier = 1f;

		[Space]
		public Color colorA = Color.white;

		public Color colorB = Color.white;

		private float lerpValue;

		private void Start()
		{
			range = rangeInitial;
			intensity = intensityInitial;
			lightComponent = GetComponent<Light>();
			if (destroyDelay > 0f)
			{
				Object.Destroy(base.gameObject, destroyDelay);
			}
		}

		private void Update()
		{
			lerpValue += Time.deltaTime * lerpSpeed;
			lightComponent.range = range.Evaluate(lerpValue) * rangeMultiplier;
			lightComponent.intensity = intensity.Evaluate(lerpValue) * intensityMultiplier;
			lightComponent.color = Color.Lerp(colorA, colorB, lerpValue);
		}
	}
}
