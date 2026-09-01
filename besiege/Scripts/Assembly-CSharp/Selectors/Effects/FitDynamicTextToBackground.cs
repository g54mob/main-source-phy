using UnityEngine;

namespace Selectors.Effects
{
	public class FitDynamicTextToBackground : MonoBehaviour
	{
		public DynamicText text;

		public float margin = 0.1f;

		public void FitText()
		{
			float num = base.transform.localScale.x - margin * 2f;
			float x = text.bounds.size.x;
			text.transform.localScale = Vector3.one * Mathf.Min(1f, num / x);
		}
	}
}
