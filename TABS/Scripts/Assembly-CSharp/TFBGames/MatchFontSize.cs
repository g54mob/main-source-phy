using TMPro;
using UnityEngine;

namespace TFBGames
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class MatchFontSize : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI targetToMatch;

		private TextMeshProUGUI myTextComponent;

		private void Awake()
		{
			myTextComponent = GetComponent<TextMeshProUGUI>();
		}

		private void Update()
		{
			if (!(targetToMatch == null))
			{
				myTextComponent.fontSize = targetToMatch.fontSize;
			}
		}
	}
}
