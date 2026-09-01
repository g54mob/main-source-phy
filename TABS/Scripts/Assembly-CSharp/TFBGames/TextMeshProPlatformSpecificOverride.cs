using TMPro;
using UnityEngine;

namespace TFBGames
{
	[RequireComponent(typeof(TMP_Text))]
	public class TextMeshProPlatformSpecificOverride : PlatformSpecificOverride
	{
		[SerializeField]
		private float overrideFontSize = 24f;

		[SerializeField]
		private bool autoSize;

		private TMP_Text tmpText;

		protected override void ApplyPlatformOverride()
		{
			tmpText = GetComponent<TMP_Text>();
			tmpText.enableAutoSizing = autoSize;
			tmpText.fontSize = overrideFontSize;
		}
	}
}
