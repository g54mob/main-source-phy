using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringChromaticAberrationIntensity")]
	public class MMSpringChromaticAberrationIntensity : MMSpringFloatComponent<PostProcessVolume>
	{
		protected ChromaticAberration _chromaticAberration;

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
