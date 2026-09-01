using System;
using System.Threading.Tasks;
using BitCode.Users;
using JetBrains.Annotations;

namespace BitCode.Networking
{
	public interface IGameInvitationManager : IPlatformService
	{
		event Action<IGameInvitation, ILocalAccount> InvitationReceived;

		void SendGameInviteAsync([NotNull] ILocalAccount user, int maxInvitees, [NotNull] IGameInvitation invitation, Action<bool, Exception> sentCallback);

		Task<bool> SendGameInviteAsync([NotNull] ILocalAccount user, int maxInvitees, [NotNull] IGameInvitation invitation);

		void SendGameInviteAsync([NotNull] ILocalAccount user, [NotNull] IRemoteAccount[] invitees, [NotNull] IGameInvitation invitation, Action<bool, Exception> sentCallback);

		Task<bool> SendGameInviteAsync([NotNull] ILocalAccount user, [NotNull] IRemoteAccount[] invitees, [NotNull] IGameInvitation invitation);

		IGameInvitation CreateInviteToMultiplayerSession(IMultiplayerSession session, byte[] applicationData = null);
	}
}
