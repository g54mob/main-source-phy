using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMRadioSignalGenerator : MMRadioSignal
	{
		public bool AnimatedPreview;

		public bool BackAndForth;

		[MMCondition("BackAndForth", true)]
		public float BackAndForthMirrorPoint;

		public MMRadioSignalGeneratorItemList SignalList;

		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 Clamps;

		[Range(0f, 1f)]
		public float Bias;

		private void Reset()
		{
		}

		public virtual float Evaluate(float time)
		{
			return 0f;
		}

		protected override void Shake()
		{
		}

		protected override void ShakeComplete()
		{
		}

		public override float GraphValue(float time)
		{
			return 0f;
		}
	}
}
