using TMPro;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringTMPTextColor")]
	public class MMSpringTMPTextColor : MMSpringColorComponent<TMP_Text>
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
