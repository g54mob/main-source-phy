using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringImageColor")]
	public class MMSpringImageColor : MMSpringColorComponent<Image>
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
