using BitCode.Users;
using Steamworks;

namespace BitCode.Platform.Steamworks
{
	public interface ISteamUserAccount : IUserAccount
	{
		CSteamID SteamId { get; }

		void UpdateName();

		void UpdateOnlineStatus();

		void UpdateAvatarImage(int imageHandle);

		void UpdatePresence();
	}
}
