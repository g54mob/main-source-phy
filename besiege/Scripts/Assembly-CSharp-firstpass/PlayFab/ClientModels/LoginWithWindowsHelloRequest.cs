using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class LoginWithWindowsHelloRequest : PlayFabRequestCommon
	{
		public string ChallengeSignature;

		public Dictionary<string, string> CustomTags;

		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		public string PublicKeyHint;

		public string TitleId;
	}
}
