using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringWhiteBalanceTint_URP")]
	public class MMSpringWhiteBalanceTint_URP : MMSpringFloatComponent<Volume>
	{
		protected WhiteBalance _whiteBalance;

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
