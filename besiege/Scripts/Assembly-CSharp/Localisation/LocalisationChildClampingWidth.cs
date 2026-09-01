using UnityEngine;

namespace Localisation
{
	[RequireComponent(typeof(DynamicText))]
	public class LocalisationChildClampingWidth : LocalisationChild
	{
		public int characterLengthForMin = 10;

		public int characterLengthForMax = 7;

		public float minLetterSpacing;

		public float maxLetterSpacing = 0.1f;

		public bool exponentialEvaluation;

		public override void Recaption()
		{
			string translation = LocalisationManager.GetTranslation(translationID);
			if (!(translation != string.Empty))
			{
				return;
			}
			dynamicText = base.gameObject.GetComponent<DynamicText>();
			if (dynamicText != null)
			{
				ReferenceMaster.SetDynamicText(dynamicText, translation);
				float num = Mathf.InverseLerp(1f * (float)characterLengthForMin, 1f * (float)characterLengthForMax, 1f * (float)translation.Length);
				if (exponentialEvaluation)
				{
					num *= num;
				}
				float letterSpacing = Mathf.Lerp(minLetterSpacing, maxLetterSpacing, num);
				dynamicText.letterSpacing = letterSpacing;
			}
		}
	}
}
