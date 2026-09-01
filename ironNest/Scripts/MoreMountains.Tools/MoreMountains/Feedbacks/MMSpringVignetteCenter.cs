using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringVignetteCenter")]
	public class MMSpringVignetteCenter : MMSpringVector2Component<PostProcessVolume>
	{
		protected Vignette _vignette;

		public override Vector2 TargetVector2
		{
			get
			{
				return default(Vector2);
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
