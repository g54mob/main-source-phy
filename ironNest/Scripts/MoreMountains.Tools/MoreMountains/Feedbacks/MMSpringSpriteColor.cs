using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringSpriteColor")]
	public class MMSpringSpriteColor : MMSpringColorComponent<SpriteRenderer>
	{
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
	}
}
