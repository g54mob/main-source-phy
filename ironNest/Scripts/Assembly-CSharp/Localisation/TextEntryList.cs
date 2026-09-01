using System;
using System.Collections.Generic;

namespace Localisation
{
	[Serializable]
	public class TextEntryList
	{
		public List<TextEntry> Items;

		public static TextEntryList Deserialise(byte[] data)
		{
			return null;
		}
	}
}
