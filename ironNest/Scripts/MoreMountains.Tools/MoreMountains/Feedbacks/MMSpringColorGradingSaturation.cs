using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringColorGradingSaturation")]
	public class MMSpringColorGradingSaturation : MMSpringFloatComponent<PostProcessVolume>
	{
		protected ColorGrading _colorGrading;

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
