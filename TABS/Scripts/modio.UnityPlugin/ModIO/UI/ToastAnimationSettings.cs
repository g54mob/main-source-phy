using UnityEngine;

namespace ModIO.UI
{
	[RequireComponent(typeof(RectTransform))]
	public class ToastAnimationSettings : MonoBehaviour
	{
		public Vector2 offset = new Vector2(0f, 100f);

		public float duration = 0.5f;
	}
}
