using Localisation;
using UnityEngine;

namespace InternalModding.Misc
{
	public class DisableMP : MonoBehaviour
	{
		public GameObject BlackHole;

		public void Start()
		{
			if (SingleInstanceFindOnly<ModManager>.hasInstance() && ModManager.DisableMultiplayer)
			{
				DisableMultiverseButton();
			}
		}

		private void DisableMultiverseButton()
		{
			StartGameButton component = BlackHole.GetComponent<StartGameButton>();
			component.registerMouse = false;
			Transform transform = BlackHole.GetComponent<Tooltip>().tooltipParent.FindChild("S");
			transform.GetComponent<TextMesh>().text = LocalisationManager.GetTranslation(3582);
		}
	}
}
