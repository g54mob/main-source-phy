using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class FeelSpringsDemoSlider : MonoBehaviour
	{
		[Header("Bindings")]
		public Slider TargetSlider;

		public float value => 0f;

		public void UpdateText()
		{
		}
	}
}
