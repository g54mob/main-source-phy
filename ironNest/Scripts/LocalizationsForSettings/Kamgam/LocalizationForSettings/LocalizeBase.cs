using UnityEngine;

namespace Kamgam.LocalizationForSettings
{
	public abstract class LocalizeBase : MonoBehaviour
	{
		public LocalizationProvider LocalizationProvider;

		public string Term;

		[Tooltip("Translates the given term and sets the text of the TMPro Textfield.\n\nA string.Format(format) string can be specified. The translated text will always be appended as an additional LAST parameter to the parameters list.\n\nExample Format: {0} %")]
		public string Format;

		[Tooltip("EDITOR ONLY: If enabled then the inspector will try to find the term based on the content of the textfield.\nThis is done ONLY at edit-time NOT at runtime. It's a convenience feature only.\nIf you want to dynamically update the localization then please set the 'Term' property and then call 'Localize()'.")]
		public bool UpdateTermFromText;

		protected object[] _lastUsedParameters;

		protected string _lastUsedFormat;

		public virtual void Awake()
		{
		}

		protected virtual void onLanguageChanged(string language)
		{
		}

		public virtual void OnEnable()
		{
		}

		public virtual void OnDisable()
		{
		}

		public abstract string GetText();

		public abstract void SetText(string text);

		public virtual void Clear()
		{
		}

		public virtual void Localize()
		{
		}

		public virtual void Localize(string term, string format = null, params object[] parameters)
		{
		}
	}
}
