using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABC
{
	public class PopUpLevelUI : MonoBehaviour
	{
		public TextMeshProUGUI mainText;

		public TextMeshProUGUI numberText;

		public Image outline;

		public Image background;

		private void Start()
		{
		}

		internal void Init(AllianceBonus bonus, Alliance alliance, bool isUnlocked, int unitsRequired)
		{
			mainText.text = bonus.description;
			numberText.text = unitsRequired.ToString();
			mainText.color = alliance.color;
			numberText.color = alliance.color;
			outline.color = alliance.color;
			background.color = alliance.color;
			FadeMultiplier[] componentsInChildren = GetComponentsInChildren<FadeMultiplier>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].multiplier = (isUnlocked ? 1f : 0.2f);
			}
		}
	}
}
