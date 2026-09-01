using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringVignetteColor_URP")]
	public class MMSpringVignetteColor_URP : MMSpringColorComponent<Volume>
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
