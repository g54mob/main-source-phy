using System.Collections.Generic;
using UnityEngine;

namespace TFBGames
{
	public class DisableInUIMode : MonoBehaviour
	{
		[SerializeField]
		private List<GameObject> disableOnTraditionalUI;

		[SerializeField]
		private List<GameObject> disableOnRadialUI;

		private void Start()
		{
			if (ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("UI_INPUT_MODE").currentValue == 0)
			{
				SetEnabled(disableOnRadialUI, isEnabled: false);
				SetEnabled(disableOnTraditionalUI, isEnabled: true);
			}
			else
			{
				SetEnabled(disableOnTraditionalUI, isEnabled: false);
				SetEnabled(disableOnRadialUI, isEnabled: true);
			}
		}

		private void SetEnabled(List<GameObject> gameObjects, bool isEnabled)
		{
			if (gameObjects != null)
			{
				for (int i = 0; i < gameObjects.Count; i++)
				{
					gameObjects[i].SetActive(isEnabled);
				}
			}
		}
	}
}
