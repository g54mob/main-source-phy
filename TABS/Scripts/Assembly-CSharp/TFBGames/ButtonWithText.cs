using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	public class ButtonWithText : MonoBehaviour
	{
		[SerializeField]
		private Button button;

		[SerializeField]
		private TextMeshProUGUI textMesh;

		[SerializeField]
		private LocalizeText localizeText;

		public void Init(string label, Action onClick)
		{
			textMesh.text = label;
			button.onClick.AddListener(onClick.Invoke);
		}

		public void InitLocalized(string localizedKey, Action onClick)
		{
			localizeText.LocaleID = localizedKey;
			button.onClick.AddListener(onClick.Invoke);
		}
	}
}
