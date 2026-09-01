using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringVignetteColor")]
	public class MMSpringVignetteColor : MMSpringColorComponent<PostProcessVolume>
	{
		protected Vignette _vignette;

		public override Color TargetColor
		{
			get
			{
				return default(Color);
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
