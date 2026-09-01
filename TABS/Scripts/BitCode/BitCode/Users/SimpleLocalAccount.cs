using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BitCode.Graphics;

namespace BitCode.Users
{
	public class SimpleLocalAccount : ILocalAccount, IUserAccount
	{
		[CompilerGenerated]
		private readonly IUserAccountProperty<string> lYFHqpdsRBJgNTBlQappPiucOEoi;

		[CompilerGenerated]
		private readonly ulong OxcaRgGSDRPehuAfODGTeCmWQNto;

		public ulong? OnlineAccountId
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public IUserAccountProperty<string> Name
		{
			[CompilerGenerated]
			get
			{
				return lYFHqpdsRBJgNTBlQappPiucOEoi;
			}
		}

		public IUserAccountProperty<ImageData> AvatarImage
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public IUserAccountProperty<string> Presence
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public IUserAccountProperty<UserAccountOnlineStatus> OnlineStatus
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public ulong LocalId
		{
			[CompilerGenerated]
			get
			{
				return OxcaRgGSDRPehuAfODGTeCmWQNto;
			}
		}

		public bool PermittedToCreateUgc
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public bool PermittedToViewUgc
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public bool PermittedToCommunicate
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public bool PermittedToPurchaseContent
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		event Action<ILocalAccount> ILocalAccount.Left
		{
			add
			{
			}
			remove
			{
			}
		}

		internal SimpleLocalAccount(ulong P_0, string P_1)
		{
			OxcaRgGSDRPehuAfODGTeCmWQNto = P_0;
			lYFHqpdsRBJgNTBlQappPiucOEoi = new UserAccountAsyncProperty<string>("Name", this);
			Name.SetValue(P_1);
		}

		public void SetPresenceString(string presence)
		{
			throw new NotSupportedException();
		}

		public void CheckMultiplayerPermissionsAsync(MultiplayerMode multiplayerMode, CheckMultiplayerPermissionsEventHandler callback)
		{
			throw new NotSupportedException();
		}

		public void CheckGameInvitePermissionsAsync(CheckGameInvitePermissionsEventHandler callback)
		{
			throw new NotSupportedException();
		}

		public void CheckCrossNetworkPlayAsync(MultiplayerMode multiplayerMode, CheckCrossNetworkPlayEventHandler callback)
		{
			throw new NotSupportedException();
		}

		public Task<bool> CheckMultiplayerPermissionsAsync(MultiplayerMode multiplayerMode)
		{
			throw new NotSupportedException();
		}

		public Task<InvitePermissions> CheckGameInvitePermissionsAsync()
		{
			throw new NotSupportedException();
		}

		public Task<bool> CheckCrossNetworkPlayAsync(MultiplayerMode multiplayerMode)
		{
			throw new NotSupportedException();
		}
	}
}
