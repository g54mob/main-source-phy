using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringDepthOfFieldFocusDistance")]
	public class MMSpringDepthOfFieldFocusDistance : MMSpringFloatComponent<PostProcessVolume>
	{
		protected DepthOfField _depthOfField;

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
