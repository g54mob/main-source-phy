using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringImageAlpha")]
	public class MMSpringImageAlpha : MMSpringFloatComponent<Image>
	{
		protected Color _color;

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
	}
}
