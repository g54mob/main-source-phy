namespace PlayFab.Party
{
	public class PlayFabLocalPlayer : PlayFabPlayer
	{
		internal string _preferredLanguageCode;

		public bool IsChatControlAvailable
		{
			get
			{
				return _chatControlHandle != null;
			}
		}

		public string LanguageCode
		{
			get
			{
				PlayFabMultiplayerManager playFabMultiplayerManager = PlayFabMultiplayerManager.Get();
				return playFabMultiplayerManager._GetLanguageCode(base.EntityKey, true);
			}
			set
			{
				if (!IsChatControlAvailable)
				{
					_preferredLanguageCode = value;
				}
			}
		}

		public string PlatformSpecificUserId
		{
			get
			{
				return _platformSpecificUserId;
			}
		}

		public PlayFabLocalPlayer()
		{
			_isLocal = true;
		}
	}
}
