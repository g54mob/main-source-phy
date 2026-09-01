using PartyCSharpSDK;
using PlayFab.ClientModels;

namespace PlayFab.Party
{
	public class PlayFabPlayer
	{
		private const string _ErrorMessageVoiceLevelOutOfRange = "Value {0} is out of range. Value must be between 0 and 1.";

		private ChatState _chatState;

		private float _voiceLevel;

		private bool _isMuted;

		internal string _entityToken;

		internal bool _isLocal;

		internal string _platformSpecificUserId;

		internal PARTY_ENDPOINT_HANDLE _endPointHandle;

		internal PARTY_CHAT_CONTROL_HANDLE _chatControlHandle;

		internal bool _mutedByPlatform;

		public ulong uniquePlatformId
		{
			get
			{
				return 0uL;
			}
		}

		public ChatState ChatState
		{
			get
			{
				PlayFabMultiplayerManager playFabMultiplayerManager = PlayFabMultiplayerManager.Get();
				_chatState = playFabMultiplayerManager._GetChatState(EntityKey, _isLocal);
				return _chatState;
			}
		}

		public EntityKey EntityKey { get; private set; }

		public bool IsLocal
		{
			get
			{
				return _isLocal;
			}
		}

		public bool IsMuted
		{
			get
			{
				return _isMuted;
			}
			set
			{
				PlayFabMultiplayerManager playFabMultiplayerManager = PlayFabMultiplayerManager.Get();
				playFabMultiplayerManager._SetMuted(EntityKey, value, _isLocal);
				_isMuted = value;
			}
		}

		public float VoiceLevel
		{
			get
			{
				PlayFabMultiplayerManager playFabMultiplayerManager = PlayFabMultiplayerManager.Get();
				if (_isLocal)
				{
					return _voiceLevel;
				}
				_voiceLevel = playFabMultiplayerManager._GetVoiceLevel(EntityKey);
				return _voiceLevel;
			}
			set
			{
				if (value >= 0f && value <= 1f)
				{
					PlayFabMultiplayerManager playFabMultiplayerManager = PlayFabMultiplayerManager.Get();
					playFabMultiplayerManager._SetVoiceLevel(EntityKey, value, _isLocal);
					_voiceLevel = value;
				}
				else
				{
					PlayFabMultiplayerManager._LogError("Value {0} is out of range. Value must be between 0 and 1.".Replace("{0}", value.ToString()));
				}
			}
		}

		public PlayFabPlayer()
		{
			_platformSpecificUserId = string.Empty;
			_mutedByPlatform = false;
		}

		internal void _SetEntityKey(EntityKey entityKey)
		{
			EntityKey = entityKey;
		}
	}
}
