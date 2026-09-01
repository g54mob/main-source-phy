using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringVignetteIntensity")]
	public class MMSpringVignetteIntensity : MMSpringFloatComponent<PostProcessVolume>
	{
		protected Vignette _vignette;

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
