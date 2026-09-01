using System;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;

namespace Localisation;

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

		public FontData()
		{
			List<FontOverride> overrides = new List<FontOverride>();
			Overrides = overrides;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<FontData, string> _003C_003E9__3_0;

		public static Func<FontData.FontOverride, string> _003C_003E9__3_2;

		public static Func<FontData.FontOverride, TMP_FontAsset> _003C_003E9__3_3;

		public static Func<FontData, Dictionary<string, TMP_FontAsset>> _003C_003E9__3_1;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal string _003CInit_003Eb__3_0(FontData x)
		{
			if (x != null && (object)x.BaseFont != null)
			{
				return x.BaseFont.name;
			}
			return (string)(object)new NullReferenceException();
		}

		internal Dictionary<string, TMP_FontAsset> _003CInit_003Eb__3_1(FontData x)
		{
			if (x != null)
			{
				Func<FontData.FontOverride, string> keySelector = _003C_003E9__3_2;
				if (_003C_003E9__3_2 == null)
				{
					keySelector = (_003C_003E9__3_2 = (FontData.FontOverride fontOverride) => (string)((fontOverride != null) ? ((object)fontOverride.Language) : ((object)new NullReferenceException())));
				}
				Func<FontData.FontOverride, TMP_FontAsset> elementSelector = _003C_003E9__3_3;
				if (_003C_003E9__3_3 == null)
				{
					elementSelector = (_003C_003E9__3_3 = (FontData.FontOverride fontOverride) => (TMP_FontAsset)((fontOverride != null) ? ((object)fontOverride.Font) : ((object)new NullReferenceException())));
				}
				return Enumerable.ToDictionary(x.Overrides, keySelector, elementSelector);
			}
			return (Dictionary<string, TMP_FontAsset>)(object)new NullReferenceException();
		}

		internal string _003CInit_003Eb__3_2(FontData.FontOverride x)
		{
			if (x != null)
			{
				return x.Language;
			}
			return (string)(object)new NullReferenceException();
		}

		internal TMP_FontAsset _003CInit_003Eb__3_3(FontData.FontOverride x)
		{
			if (x != null)
			{
				return x.Font;
			}
			return (TMP_FontAsset)(object)new NullReferenceException();
		}
	}

	public List<FontData> Fonts;

	[NonSerialized]
	public Dictionary<string, Dictionary<string, TMP_FontAsset>> Runtime;

	public void Init()
	{
		Func<FontData, string> keySelector = _003C_003Ec._003C_003E9__3_0;
		if (_003C_003Ec._003C_003E9__3_0 == null)
		{
			keySelector = (_003C_003Ec._003C_003E9__3_0 = (FontData x) => (string)((x != null && (object)x.BaseFont != null) ? ((object)x.BaseFont.name) : ((object)new NullReferenceException())));
		}
		Func<FontData, Dictionary<string, TMP_FontAsset>> elementSelector = _003C_003Ec._003C_003E9__3_1;
		if (_003C_003Ec._003C_003E9__3_1 == null)
		{
			elementSelector = (_003C_003Ec._003C_003E9__3_1 = delegate(FontData x)
			{
				if (x != null)
				{
					Func<FontData.FontOverride, string> keySelector2 = _003C_003Ec._003C_003E9__3_2;
					if (_003C_003Ec._003C_003E9__3_2 == null)
					{
						keySelector2 = (_003C_003Ec._003C_003E9__3_2 = (FontData.FontOverride fontOverride) => (string)((fontOverride != null) ? ((object)fontOverride.Language) : ((object)new NullReferenceException())));
					}
					Func<FontData.FontOverride, TMP_FontAsset> elementSelector2 = _003C_003Ec._003C_003E9__3_3;
					if (_003C_003Ec._003C_003E9__3_3 == null)
					{
						elementSelector2 = (_003C_003Ec._003C_003E9__3_3 = (FontData.FontOverride fontOverride) => (TMP_FontAsset)((fontOverride != null) ? ((object)fontOverride.Font) : ((object)new NullReferenceException())));
					}
					return Enumerable.ToDictionary(x.Overrides, keySelector2, elementSelector2);
				}
				return (Dictionary<string, TMP_FontAsset>)(object)new NullReferenceException();
			});
		}
		Dictionary<string, Dictionary<string, TMP_FontAsset>> runtime = Enumerable.ToDictionary(Fonts, keySelector, elementSelector);
		Runtime = runtime;
	}

	public LocalisationFontData()
	{
		List<FontData> fonts = new List<FontData>();
		Fonts = fonts;
		Runtime = new Dictionary<string, Dictionary<string, TMP_FontAsset>>();
		base._002Ector();
	}
}
