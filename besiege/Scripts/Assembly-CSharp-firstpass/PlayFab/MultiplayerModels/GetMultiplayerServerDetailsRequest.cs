using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class GetMultiplayerServerDetailsRequest : PlayFabRequestCommon
	{
		public string BuildId;

		public Dictionary<string, string> CustomTags;

		public string Region;

		public string SessionId;
	}
}
