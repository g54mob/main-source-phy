using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	[Serializable]
	public class MMTweenType
	{
		public MMTweenDefinitionTypes MMTweenDefinitionType;

		public MMTween.MMTweenCurve MMTweenCurve;

		public AnimationCurve Curve;

		public bool Initialized;

		public static MMTweenType DefaultEaseInCubic { get; }

		public MMTweenType(MMTween.MMTweenCurve newCurve)
		{
		}

		public MMTweenType(AnimationCurve newCurve)
		{
		}

		public float Evaluate(float t)
		{
			return 0f;
		}
	}
}
