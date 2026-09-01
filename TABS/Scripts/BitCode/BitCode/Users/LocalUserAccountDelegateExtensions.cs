using System;

namespace BitCode.Users
{
	public static class LocalUserAccountDelegateExtensions
	{
		public static void SafelyInvoke(this CheckMultiplayerPermissionsEventHandler self, ILocalAccount user, bool permitted)
		{
			try
			{
				self(user, permitted);
			}
			catch (Exception)
			{
			}
		}

		public static void SafelyInvoke(this CheckGameInvitePermissionsEventHandler self, ILocalAccount user, InvitePermissions permissions)
		{
			try
			{
				self(user, permissions);
			}
			catch (Exception)
			{
			}
		}

		public static void SafelyInvoke(this CheckCrossNetworkPlayEventHandler self, ILocalAccount user, bool permitted)
		{
			try
			{
				self(user, permitted);
			}
			catch (Exception)
			{
			}
		}
	}
}
