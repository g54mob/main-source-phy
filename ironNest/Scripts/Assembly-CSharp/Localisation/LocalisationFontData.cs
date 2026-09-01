using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Localisation
{
	[CreateAssetMenu(fileName = "LocalisationFontData", menuName = "Localisation/Font Data")]
	public class LocalisationFontData : ScriptableObject
	{
		[Serializable]
		public class FontData
		{
			[Serializable]
			public class FontOverride
			{
				public string Language;

				public TMP_FontAsset Font;
			}

			public TMP_FontAsset BaseFont;

			public List<FontOverride> Overrides;
		}

		public List<FontData> Fonts;

		[NonSerialized]
		[HideInInspector]
		public Dictionary<string, Dictionary<string, TMP_FontAsset>> Runtime;

		public void Init()
		{
		}
	}
}
