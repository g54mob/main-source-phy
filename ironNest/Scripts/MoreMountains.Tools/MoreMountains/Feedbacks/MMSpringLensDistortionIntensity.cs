using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringLensDistortionIntensity")]
	public class MMSpringLensDistortionIntensity : MMSpringFloatComponent<PostProcessVolume>
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
