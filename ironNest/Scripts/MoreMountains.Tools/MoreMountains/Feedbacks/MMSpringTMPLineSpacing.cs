using TMPro;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringTMPLineSpacing")]
	public class MMSpringTMPLineSpacing : MMSpringFloatComponent<TMP_Text>
	{
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
