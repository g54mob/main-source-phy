using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class MultiplayerEmptyRequest : PlayFabRequestCommon
	{
		public Dictionary<string, string> CustomTags;
	}
}
