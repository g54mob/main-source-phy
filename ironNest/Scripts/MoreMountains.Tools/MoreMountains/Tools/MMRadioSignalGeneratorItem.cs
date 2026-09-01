using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	[Serializable]
	public class MMRadioSignalGeneratorItem
	{
		public enum GeneratorItemModes
		{
			Multiply = 0,
			Additive = 1
		}

		public bool Active;

		public MMSignal.SignalType SignalType;

		[MMEnumCondition("SignalType", new int[] { 9 })]
		public AnimationCurve Curve;

		[MMEnumCondition("SignalType", new int[] { 10 })]
		public MMTween.MMTweenCurve TweenCurve;

		public GeneratorItemModes Mode;

		[Range(-1f, 1f)]
		public float Phase;

		[Range(0f, 10f)]
		public float Frequency;

		[Range(0f, 1f)]
		public float Amplitude;

		[Range(-1f, 1f)]
		public float Offset;

		public bool Invert;
	}
}
