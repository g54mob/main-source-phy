using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringMotionBlurShutterAngle")]
	public class MMSpringMotionBlurShutterAngle : MMSpringFloatComponent<PostProcessVolume>
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
