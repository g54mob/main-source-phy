using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringColorAdjustmentsSaturation_URP")]
	public class MMSpringColorAdjustmentsSaturation_URP : MMSpringFloatComponent<Volume>
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
