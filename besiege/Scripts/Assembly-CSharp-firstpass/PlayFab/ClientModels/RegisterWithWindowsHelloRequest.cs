using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class RegisterWithWindowsHelloRequest : PlayFabRequestCommon
	{
		public Dictionary<string, string> CustomTags;

		public string DeviceName;

		public string EncryptedRequest;

		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		public string PlayerSecret;

		public string PublicKey;

		public string TitleId;

		public string UserName;
	}
}
