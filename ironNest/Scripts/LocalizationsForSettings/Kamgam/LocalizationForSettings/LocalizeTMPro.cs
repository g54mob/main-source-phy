using TMPro;
using UnityEngine;

namespace Kamgam.LocalizationForSettings
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class LocalizeTMPro : LocalizeBase
	{
		public TextMeshProUGUI Textfield;

		public override void Awake()
		{
		}

		public override string GetText()
		{
			return null;
		}

		public override void SetText(string text)
		{
		}
	}
}
