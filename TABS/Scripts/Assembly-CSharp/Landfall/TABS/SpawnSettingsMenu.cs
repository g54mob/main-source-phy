using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class SpawnSettingsMenu : MonoBehaviour
	{
		public GameObject settingsMenu;

		public Button button;

		public SafeAreaMarker safeAreaMarkers;

		private bool done;

		private void Awake()
		{
			if (!done)
			{
				done = true;
				settingsMenu = Object.Instantiate(settingsMenu, base.transform.position, base.transform.rotation, base.transform);
				if (safeAreaMarkers != null)
				{
					settingsMenu.GetComponent<SettingsHandler>().AddSafeAreaMarkers(safeAreaMarkers);
				}
				settingsMenu.transform.localScale = Vector3.one;
				EscapeMenuHandler escapeMenuHandler = GetComponentInParent<EscapeMenuHandler>();
				if (escapeMenuHandler == null)
				{
					escapeMenuHandler = base.gameObject.transform.parent.GetComponentInChildren<EscapeMenuHandler>();
				}
				button.onClick.AddListener(escapeMenuHandler.GoToSettings);
				settingsMenu.GetComponent<SettingsHandler>().ButtonBack.onClick.AddListener(escapeMenuHandler.GoToEscapeMenu);
				escapeMenuHandler.settingsAnim = settingsMenu.GetComponent<CodeAnimation>();
			}
		}
	}
}
