using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class PopUpTextManager : MonoBehaviour
	{
		public Text displayText;

		public float displayTime;

		public float fadeTime;

		private IEnumerator fadeAlpha;

		private static PopUpTextManager popUpTextManager;

		public static PopUpTextManager Instance()
		{
			if (!popUpTextManager)
			{
				popUpTextManager = Object.FindObjectOfType(typeof(PopUpTextManager)) as PopUpTextManager;
				if (!popUpTextManager)
				{
					Debug.LogError("There needs to be one active PopUpTextManager script on a GameObject in your scene.");
				}
			}
			return popUpTextManager;
		}

		public void DisplayMessage(string message)
		{
			displayText.text = message;
			SetAlpha();
		}

		private void SetAlpha()
		{
			if (fadeAlpha != null)
			{
				StopCoroutine(fadeAlpha);
			}
			fadeAlpha = FadeAlpha();
			StartCoroutine(fadeAlpha);
		}

		private IEnumerator FadeAlpha()
		{
			Color color = displayText.color;
			color.a = 1f;
			displayText.color = color;
			yield return new WaitForSeconds(displayTime);
			while (displayText.color.a > 0f)
			{
				Color color2 = displayText.color;
				color2.a -= Time.deltaTime / fadeTime;
				displayText.color = color2;
				yield return null;
			}
			yield return null;
		}
	}
}
