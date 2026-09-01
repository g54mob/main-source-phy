using System;
using System.Collections.Generic;

namespace TFBGames
{
	[Serializable]
	public class PlayerPrefsData
	{
		public Dictionary<string, int> IntPrefs = new Dictionary<string, int>();

		public Dictionary<string, float> FloatPrefs = new Dictionary<string, float>();

		public Dictionary<string, string> StringPrefs = new Dictionary<string, string>();
	}
}
