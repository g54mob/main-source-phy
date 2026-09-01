using TMPro;
using UnityEngine;

namespace TFBGames
{
	[RequireComponent(typeof(TMP_InputField))]
	public class TextMeshProInputFieldPlatformSpecificOverride : PlatformSpecificOverride
	{
		[SerializeField]
		private float overrideFontSize = 24f;

		private TMP_InputField tmpInputField;

		protected override void ApplyPlatformOverride()
		{
			tmpInputField = GetComponent<TMP_InputField>();
			tmpInputField.pointSize = overrideFontSize;
		}
	}
}
