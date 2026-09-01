using System;

namespace TMPSelection
{
	public static class TMP_RichTextSelectionUtility
	{
		[Flags]
		public enum StyleFlags
		{
			None = 0,
			Bold = 1,
			Italic = 2,
			Underline = 4,
			SmallCaps = 8
		}

		private struct Mapping
		{
			public string Raw;

			public int PlainLength;

			public int[] PlainToRaw;

			public StyleFlags[] PlainStyles;
		}

		private enum TagKind
		{
			Open = 0,
			Close = 1
		}

		private static readonly (string tag, int len, StyleFlags style)[] s_OpenTags;

		private static readonly (string tag, int len, StyleFlags style)[] s_CloseTags;

		public static string ExtractRichSubstringByPlainRange(string raw, int plainStart, int plainEndInclusive, bool trimResult)
		{
			return null;
		}

		public static bool RemoveRichTextByPlainRange(ref string raw, int plainStart, int plainEndInclusive, bool trimOuterNewlines, bool preserveVisualSeparation)
		{
			return false;
		}

		public static string SanitizeRichText(string s)
		{
			return null;
		}

		private static int ConsumeLeadingOpenTagsBackward(string raw, int rawPos, StyleFlags stylesMask)
		{
			return 0;
		}

		private static int ConsumeTrailingCloseTags(string raw, int rawPos, StyleFlags stylesMask)
		{
			return 0;
		}

		private static string BalanceTags(string s)
		{
			return null;
		}

		private static string StripEmptyTagPairs(string s)
		{
			return null;
		}

		private static Mapping BuildMapping(string raw)
		{
			return default(Mapping);
		}

		private static bool TryParseSupportedTag(string raw, int startIndex, out TagKind kind, out StyleFlags style, out int length)
		{
			kind = default(TagKind);
			style = default(StyleFlags);
			length = default(int);
			return false;
		}

		private static string BuildOpenTags(StyleFlags flags)
		{
			return null;
		}

		private static string BuildCloseTags(StyleFlags flags)
		{
			return null;
		}

		private static bool StringMatchAt(string source, int startIndex, string pattern, int patternLen)
		{
			return false;
		}

		private static bool NormalizePlainRange(int plainLen, ref int plainStart, ref int plainEndInclusive)
		{
			return false;
		}

		private static bool ContainsAnyNewline(string s)
		{
			return false;
		}

		private static bool ContainsAnyClosingTags(string s)
		{
			return false;
		}

		private static bool ContainsMatchingOpeningTags(string s, StyleFlags flag)
		{
			return false;
		}

		private static bool EndsWithNewline(string s)
		{
			return false;
		}

		private static bool StartsWithNewline(string s)
		{
			return false;
		}

		private static string TrimNewlinesNormalize(string s)
		{
			return null;
		}
	}
}
