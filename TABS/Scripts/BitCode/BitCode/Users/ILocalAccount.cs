using System;
using System.Threading.Tasks;

namespace BitCode.Users
{
	public interface ILocalAccount : IUserAccount
	{
		ulong LocalId { get; }

		bool PermittedToCreateUgc { get; }

		bool PermittedToViewUgc { get; }

		bool PermittedToCommunicate { get; }

		bool PermittedToPurchaseContent { get; }

		event Action<ILocalAccount> Left;

		void SetPresenceString(string presence);

		void CheckMultiplayerPermissionsAsync(MultiplayerMode multiplayerMode, CheckMultiplayerPermissionsEventHandler callback);

		void CheckGameInvitePermissionsAsync(CheckGameInvitePermissionsEventHandler callback);

		void CheckCrossNetworkPlayAsync(MultiplayerMode multiplayerMode, CheckCrossNetworkPlayEventHandler callback);

		Task<bool> CheckMultiplayerPermissionsAsync(MultiplayerMode multiplayerMode);

		Task<InvitePermissions> CheckGameInvitePermissionsAsync();

		Task<bool> CheckCrossNetworkPlayAsync(MultiplayerMode multiplayerMode);
	}
}
