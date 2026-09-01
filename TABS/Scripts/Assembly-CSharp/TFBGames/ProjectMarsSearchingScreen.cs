using GamepadUI.StateManager.Core;
using TMPro;
using UnityEngine;

namespace TFBGames
{
	public class ProjectMarsSearchingScreen : UISubMenu
	{
		[SerializeField]
		protected TMP_Text searchingText;

		public void SetSearchingText(string message)
		{
			if (searchingText != null)
			{
				searchingText.text = message;
			}
		}
	}
}
