using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringColorAdjustmentsHueShift_URP")]
	public class MMSpringColorAdjustmentsHueShift_URP : MMSpringFloatComponent<Volume>
	{
		protected ColorAdjustments _colorAdjustments;

		public override float TargetFloat
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		protected override void Initialization()
		{
		}
	}
}
