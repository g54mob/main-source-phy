using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.LocalizationForSettings
{
	[Serializable]
	public class Translation : ITranslation
	{
		[SerializeField]
		[HideInInspector]
		protected string _term;

		[SerializeField]
		[HideInInspector]
		protected List<string> _texts;

		public Translation(string term, int languageCount)
		{
		}

		public Translation(string term, List<string> texts)
		{
		}

		public string GetTerm()
		{
			return null;
		}

		public bool HasText(int languageIndex)
		{
			return false;
		}

		public string GetText(int languageIndex)
		{
			return null;
		}

		public void SetText(int languageIndex, string text)
		{
		}

		public void ClearTexts()
		{
		}
	}
}
