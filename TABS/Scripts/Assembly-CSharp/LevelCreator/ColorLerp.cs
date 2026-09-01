using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class ColorLerp : MonoBehaviour
	{
		public Image image;

		public Color imageTargetColor;

		public Text text;

		public TMP_Text tmpText;

		public Color textTargetColor;

		public Color tmpTextTargetColor;

		public AnimationCurve transitionCurve;

		public bool destroyOnFinishedLerp;

		private float t;

		private float elapsedTime;

		private Color imageStartColor;

		private Color textStartColor;

		private Color tmpTextStartColor;

		private void Start()
		{
			if ((bool)image)
			{
				imageStartColor = image.color;
			}
			if ((bool)text)
			{
				textStartColor = text.color;
			}
			if ((bool)tmpText)
			{
				tmpTextStartColor = tmpText.color;
			}
		}

		private void Update()
		{
			if ((bool)image)
			{
				image.color = Color.Lerp(imageStartColor, imageTargetColor, t);
			}
			if ((bool)text)
			{
				text.color = Color.Lerp(textStartColor, textTargetColor, t);
			}
			if ((bool)tmpText)
			{
				tmpText.color = Color.Lerp(tmpTextStartColor, tmpTextTargetColor, t);
			}
			t = Mathf.Clamp01((transitionCurve == null) ? elapsedTime : transitionCurve.Evaluate(elapsedTime));
			if (t >= 1f && destroyOnFinishedLerp)
			{
				Object.Destroy(base.gameObject);
			}
			elapsedTime += Time.deltaTime;
		}
	}
}
