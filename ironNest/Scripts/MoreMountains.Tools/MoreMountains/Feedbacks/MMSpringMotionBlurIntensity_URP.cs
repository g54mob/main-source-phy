using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringMotionBlurIntensity_URP")]
	public class MMSpringMotionBlurIntensity_URP : MMSpringFloatComponent<Volume>
	{
		protected MotionBlur _motionBlur;

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
