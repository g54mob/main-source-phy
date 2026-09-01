using System;
using BitCode.Users;

namespace BitCode.Networking
{
	public static class GameInvitationManagerDelegateExtensions
	{
		public static void SafelyInvoke(this Action<bool, Exception> self, bool invitationWasSent, Exception exception)
		{
			try
			{
				self(invitationWasSent, exception);
			}
			catch (Exception)
			{
			}
		}

		public static void SafelyInvoke(this Action<IGameInvitation, ILocalAccount> self, IGameInvitation invitation, ILocalAccount recipient)
		{
			try
			{
				self(invitation, recipient);
			}
			catch (Exception)
			{
			}
		}
	}
}
