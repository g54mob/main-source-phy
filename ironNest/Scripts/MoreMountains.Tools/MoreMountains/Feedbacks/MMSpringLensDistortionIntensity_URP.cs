using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringLensDistortionIntensity_URP")]
	public class MMSpringLensDistortionIntensity_URP : MMSpringFloatComponent<Volume>
	{
		protected LensDistortion _lensDistortion;

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
