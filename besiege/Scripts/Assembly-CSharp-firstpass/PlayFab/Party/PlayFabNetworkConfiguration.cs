namespace PlayFab.Party
{
	public class PlayFabNetworkConfiguration
	{
		private const uint _MAX_SUPPORTED_PLAYER_COUNT = 32u;

		private const string _ErrorMessageMaxUserCountValueOutOfRange = "Value must be between 1 and {0}";

		private uint _maxPlayerCount;

		public uint MaxPlayerCount
		{
			get
			{
				return _maxPlayerCount;
			}
			set
			{
				if (value != 0 && value <= 32)
				{
					_maxPlayerCount = value;
				}
				else
				{
					PlayFabMultiplayerManager._LogError("Value must be between 1 and {0}".Replace("{0}", 32u.ToString()));
				}
			}
		}

		public PlayFabNetworkConfiguration()
		{
			_maxPlayerCount = 32u;
		}
	}
}
