using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringPaniniProjectionDistance_URP")]
	public class MMSpringPaniniProjectionDistance_URP : MMSpringFloatComponent<Volume>
	{
		protected PaniniProjection _paniniProjection;

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
