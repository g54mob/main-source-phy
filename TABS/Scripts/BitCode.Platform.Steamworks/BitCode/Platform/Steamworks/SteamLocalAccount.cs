using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BitCode.Users;
using Steamworks;

namespace BitCode.Platform.Steamworks
{
	public class SteamLocalAccount : SteamUserAccount, IUserAccount, ILocalAccount
	{
		[CompilerGenerated]
		private ulong EFCHbCnMUXDamfexhtnKtmAYfALMA;

		[CompilerGenerated]
		private bool WJTWrKPsgECTFJHSdghskGyigCrz;

		[CompilerGenerated]
		private bool qKUFXlEqDoaRkGChgOpLxauMANIyb;

		[CompilerGenerated]
		private bool GriVZswjfBFwCWYzbRNEBNsyfVbN;

		[CompilerGenerated]
		private bool JETYgsGMUfJvHeUNqINcKOzUQcRi;

		public ulong LocalId
		{
			[CompilerGenerated]
			get
			{
				return EFCHbCnMUXDamfexhtnKtmAYfALMA;
			}
			[CompilerGenerated]
			protected set
			{
				EFCHbCnMUXDamfexhtnKtmAYfALMA = value;
			}
		}

		public bool PermittedToCreateUgc
		{
			[CompilerGenerated]
			get
			{
				return WJTWrKPsgECTFJHSdghskGyigCrz;
			}
			[CompilerGenerated]
			protected set
			{
				WJTWrKPsgECTFJHSdghskGyigCrz = value;
			}
		}

		public bool PermittedToViewUgc
		{
			[CompilerGenerated]
			get
			{
				return qKUFXlEqDoaRkGChgOpLxauMANIyb;
			}
			[CompilerGenerated]
			protected set
			{
				qKUFXlEqDoaRkGChgOpLxauMANIyb = value;
			}
		}

		public bool PermittedToCommunicate
		{
			[CompilerGenerated]
			get
			{
				return GriVZswjfBFwCWYzbRNEBNsyfVbN;
			}
			[CompilerGenerated]
			protected set
			{
				GriVZswjfBFwCWYzbRNEBNsyfVbN = value;
			}
		}

		public bool PermittedToPurchaseContent
		{
			[CompilerGenerated]
			get
			{
				return JETYgsGMUfJvHeUNqINcKOzUQcRi;
			}
			[CompilerGenerated]
			protected set
			{
				JETYgsGMUfJvHeUNqINcKOzUQcRi = value;
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

		public SteamLocalAccount(CSteamID steamId, SteamService steamService)
			: base(steamId, steamService)
		{
		}

		public void SetPresenceString(string presence)
		{
			throw new NotImplementedException();
		}

		public void CheckMultiplayerPermissionsAsync(MultiplayerMode multiplayerMode, CheckMultiplayerPermissionsEventHandler callback)
		{
			CheckDisposed();
			while (true)
			{
				int num = -1832782956;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -490853491)) % 4)
					{
					case 0u:
						break;
					default:
						return;
					case 1u:
					{
						int num3;
						int num4;
						if (callback == null)
						{
							num3 = -794694992;
							num4 = num3;
						}
						else
						{
							num3 = -1992621179;
							num4 = num3;
						}
						num = num3 ^ ((int)num2 * -310322542);
						continue;
					}
					case 2u:
						callback(this, permitted: true);
						num = (int)((num2 * 517065748) ^ 0x32421F3A);
						continue;
					case 3u:
						return;
					}
					break;
				}
			}
		}

		public void CheckGameInvitePermissionsAsync(CheckGameInvitePermissionsEventHandler callback)
		{
			CheckDisposed();
			while (true)
			{
				int num = 812358008;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x6B43D2E9)) % 4)
					{
					case 2u:
						break;
					default:
						return;
					case 1u:
					{
						int num3;
						int num4;
						if (callback != null)
						{
							num3 = 877016932;
							num4 = num3;
						}
						else
						{
							num3 = 1261211651;
							num4 = num3;
						}
						num = num3 ^ ((int)num2 * -2087335115);
						continue;
					}
					case 0u:
						callback.SafelyInvoke(this, InvitePermissions.All);
						num = (int)(num2 * 1194140565) ^ -154722258;
						continue;
					case 3u:
						return;
					}
					break;
				}
			}
		}

		public void CheckCrossNetworkPlayAsync(MultiplayerMode multiplayerMode, CheckCrossNetworkPlayEventHandler callback)
		{
			throw new NotImplementedException();
		}

		public Task<bool> CheckMultiplayerPermissionsAsync(MultiplayerMode multiplayerMode)
		{
			throw new NotImplementedException();
		}

		public Task<InvitePermissions> CheckGameInvitePermissionsAsync()
		{
			CheckDisposed();
			return Task.FromResult(InvitePermissions.All);
		}

		public Task<bool> CheckCrossNetworkPlayAsync(MultiplayerMode multiplayerMode)
		{
			throw new NotImplementedException();
		}

		public override void UpdateName()
		{
			CheckDisposed();
			while (true)
			{
				int num = 34905809;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x49F3E1A1)) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 2u:
						goto IL_0028;
					case 1u:
						return;
					}
					break;
					IL_0028:
					base.Name.SetValue(SteamFriends.GetPersonaName());
					num = (int)((num2 * 1068244214) ^ 0x5054DD49);
				}
			}
		}

		public override void UpdateOnlineStatus()
		{
			CheckDisposed();
			base.OnlineStatus.SetValue(Utilities.ConvertToOnlineStatus(SteamFriends.GetPersonaState()));
		}
	}
}
