using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class CastleFightClock : MonoBehaviour
	{
		private static CastleFightClock instance;

		public Image image;

		private void Awake()
		{
			image = GetComponent<Image>();
			instance = this;
		}

		public static void SetFillAmount(float value)
		{
			instance.image.fillAmount = value;
		}
	}
}
