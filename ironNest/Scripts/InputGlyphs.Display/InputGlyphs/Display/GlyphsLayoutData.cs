using System;

namespace InputGlyphs.Display
{
	[Serializable]
	public struct GlyphsLayoutData
	{
		public GlyphsLayout Layout;

		public int Index;

		public int MaxCount;

		public static GlyphsLayoutData Default => default(GlyphsLayoutData);
	}
}
