using UnityEngine;
using UnityEngine.UI;

namespace BitCode.Debug
{
	public class DebugConsoleUI : ServicePrefab
	{
		[SerializeField]
		private KeyCode activateKey;

		[SerializeField]
		private KeyCode rawInputToggle;

		[SerializeField]
		private KeyCode submitKey = KeyCode.Return;

		[SerializeField]
		private Canvas uiCanvas;

		[SerializeField]
		private Text promptLabel;

		[SerializeField]
		private InputField inputBox;

		[SerializeField]
		private ScrollRect scrollRect;

		[SerializeField]
		private Transform entryList;

		[SerializeField]
		private RectTransform viewportTransform;

		[SerializeField]
		private RectTransform contentRootTransform;

		[SerializeField]
		private DebugConsoleUIOutputLine linePrefab;
	}
}
