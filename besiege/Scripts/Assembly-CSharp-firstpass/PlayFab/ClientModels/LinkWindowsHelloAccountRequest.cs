using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class LinkWindowsHelloAccountRequest : PlayFabRequestCommon
	{
		public Dictionary<string, string> CustomTags;

		public string DeviceName;

		public bool? ForceLink;

		public string PublicKey;

		public string UserName;
	}
}
