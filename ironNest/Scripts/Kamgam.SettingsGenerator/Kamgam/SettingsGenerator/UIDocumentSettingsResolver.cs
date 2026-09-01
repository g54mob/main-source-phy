using System;
using System.Collections.Generic;
using Kamgam.LocalizationForSettings;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kamgam.SettingsGenerator
{
	public class UIDocumentSettingsResolver : MonoBehaviour
	{
		public delegate SettingResolverForVisualElement CreateResolverDelegate(UIDocumentSettingsResolver documentResolver, VisualElement element, List<string> uniqueClassNames);

		public SettingsProvider SettingsProvider;

		public LocalizationProvider LocalizationProvider;

		[NonSerialized]
		public CreateResolverDelegate CustomCreateResolverMethod;

		protected UIDocument _document;

		public UIDocument Document => null;

		public void CreateOrUpdateResolvers()
		{
		}

		public static UIDocumentSettingsResolver GetOrCreateResolversRoot(GameObject gameObjectWithUIDocument)
		{
			return null;
		}

		private int createOrUpdateResolvers<TVisualElement, TResolver>(List<string> uniqueClassNames)
		{
			return 0;
		}

		private int createOrUpdateCustomResolvers(List<string> uniqueClassNames)
		{
			return 0;
		}

		public TResolver CreateGameObjectWithResolver<TVisualElement, TResolver>(TVisualElement element)
		{
			return default(TResolver);
		}
	}
}
