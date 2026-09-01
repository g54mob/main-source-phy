using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[RequireComponent(typeof(ScrollRect))]
	public class ScrollViewReset : MonoBehaviour
	{
		public float verticalPosition = 1f;

		public float horizontalPosition = 1f;

		private void OnEnable()
		{
			GetComponent<ScrollRect>().verticalNormalizedPosition = 1f;
			GetComponent<ScrollRect>().horizontalNormalizedPosition = 1f;
		}
	}
}
