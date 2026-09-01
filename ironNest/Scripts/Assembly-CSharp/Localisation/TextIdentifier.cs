using System;

namespace Localisation
{
	[Serializable]
	public class TextIdentifier
	{
		public string Key;

		public string Raw;

		public TextIdentifier()
		{
		}

		public TextIdentifier(string rawText)
		{
		}

		public string Get()
		{
			return null;
		}

		public bool TryGet(out string text)
		{
			text = null;
			return false;
		}
	}
}
