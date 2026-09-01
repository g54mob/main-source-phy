using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringBloomIntensity")]
	public class MMSpringBloomIntensity : MMSpringFloatComponent<PostProcessVolume>
	{
		protected Bloom _bloom;

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
