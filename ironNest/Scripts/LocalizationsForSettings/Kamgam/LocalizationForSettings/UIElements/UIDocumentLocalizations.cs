using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kamgam.LocalizationForSettings.UIElements
{
	public class UIDocumentLocalizations : MonoBehaviour
	{
		public static int ParentLevelsToSearch;

		public LocalizationProvider LocalizationProvider;

		protected UIDocument _document;

		public UIDocument Document => null;

		public void CreateOrUpdateLocalizers()
		{
		}

		public static UIDocumentLocalizations GetOrCreateLocalizationsRoot(GameObject gameObjectWithUIDocument)
		{
			return null;
		}

		private int createOrUpdateLocalizer<TVisualElement, TLocalizer>(List<string> uniqueClassNames)
		{
			return 0;
		}

		public TLocalizer CreateGameObjectWithLocalizer<TVisualElement, TLocalizer>(VisualElement element)
		{
			return default(TLocalizer);
		}
	}
}
