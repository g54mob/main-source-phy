using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringChromaticAberrationIntensity_URP")]
	public class MMSpringChromaticAberrationIntensity_URP : MMSpringFloatComponent<Volume>
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
