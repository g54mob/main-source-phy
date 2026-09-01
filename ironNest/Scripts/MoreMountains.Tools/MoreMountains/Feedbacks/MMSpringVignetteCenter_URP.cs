using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringVignetteCenter_URP")]
	public class MMSpringVignetteCenter_URP : MMSpringVector2Component<Volume>
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
