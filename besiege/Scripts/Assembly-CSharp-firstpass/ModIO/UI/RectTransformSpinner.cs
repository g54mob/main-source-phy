using UnityEngine;

namespace ModIO.UI
{
	[RequireComponent(typeof(RectTransform))]
	public class RectTransformSpinner : MonoBehaviour
	{
		public float secondsPerRotation;

		private void Update()
		{
			float num = -360f / secondsPerRotation;
			base.transform.Rotate(new Vector3(0f, 0f, Time.unscaledDeltaTime * num));
		}
	}
}
