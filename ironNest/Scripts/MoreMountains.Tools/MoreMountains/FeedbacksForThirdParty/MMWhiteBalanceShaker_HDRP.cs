using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/PostProcessing/MMWhiteBalanceShaker_HDRP")]
	public class MMWhiteBalanceShaker_HDRP : MMShaker
	{
		[Tooltip("whether or not to add to the initial value")]
		public bool RelativeValues;

		[MMInspectorGroup("Temperature", true, 47, false)]
		[Tooltip("the curve used to animate the temperature value on")]
		public AnimationCurve ShakeTemperature;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(-100f, 100f)]
		public float RemapTemperatureZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(-100f, 100f)]
		public float RemapTemperatureOne;

		[MMInspectorGroup("Tint", true, 48, false)]
		[Tooltip("the curve used to animate the tint value on")]
		public AnimationCurve ShakeTint;

		[Tooltip("the value to remap the curve's 0 to")]
		[Range(-100f, 100f)]
		public float RemapTintZero;

		[Tooltip("the value to remap the curve's 1 to")]
		[Range(-100f, 100f)]
		public float RemapTintOne;
	}
}
