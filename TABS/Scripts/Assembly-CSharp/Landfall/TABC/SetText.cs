using TMPro;
using UnityEngine;

namespace Landfall.TABC
{
	public class SetText : MonoBehaviour
	{
		public string[] texts;

		private TextMeshProUGUI textObject;

		public void SetTextByID(int textID)
		{
			if (!textObject)
			{
				textObject = GetComponent<TextMeshProUGUI>();
			}
			textObject.text = texts[textID];
		}
	}
}
