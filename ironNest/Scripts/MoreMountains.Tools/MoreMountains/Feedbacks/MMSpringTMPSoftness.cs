using TMPro;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Springs/MMSpringTMPSoftness")]
	public class MMSpringTMPSoftness : MMSpringFloatComponent<TMP_Text>
	{
		protected override void ApplyValue(float newValue)
		{
		}

		protected override void GrabCurrentValue()
		{
		}
	}
}
