using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringDepthOfFieldFocalLength_URP")]
	public class MMSpringDepthOfFieldFocalLength_URP : MMSpringFloatComponent<Volume>
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
