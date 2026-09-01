using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class StartGameRequest : PlayFabRequestCommon
	{
		public string BuildVersion;

		public string CharacterId;

		public string CustomCommandLineData;

		public Dictionary<string, string> CustomTags;

		public string GameMode;

		public Region Region;

		public string StatisticName;
	}
}
