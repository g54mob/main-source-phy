using System;
using System.Threading.Tasks;
using BitCode.Extensions;
using BitCode.Users;
using Steamworks;

namespace BitCode.Platform.Steamworks
{
	public class SteamFriendManager : IPlatformService, IFriendManager
	{
		private readonly SteamService JvEZsMEbjtsmodyZFfToTIvzzHoC;

		private bool VwVaDdklZcwjDUKZLbrwYIhPAurOA;

		event Action<IPlatformService, Exception> IPlatformService.InternalErrorOccurred
		{
			add
			{
			}
			remove
			{
			}
		}

		public SteamFriendManager(SteamService steamService)
		{
			while (true)
			{
				int num = -1538590569;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -793378719)) % 3)
					{
					case 2u:
						break;
					default:
						return;
					case 1u:
						goto IL_0028;
					case 0u:
						return;
					}
					break;
					IL_0028:
					JvEZsMEbjtsmodyZFfToTIvzzHoC = steamService;
					num = (int)(num2 * 1583361477) ^ -159900798;
				}
			}
		}

		public void InitializeForUser(ILocalAccount user)
		{
			if (VwVaDdklZcwjDUKZLbrwYIhPAurOA)
			{
				while (true)
				{
					uint num;
					switch ((num = 1741222705u) % 3)
					{
					case 0u:
						continue;
					case 1u:
						throw new InvalidOperationException("Called InitializeForUser on a user that has already been initialized.");
					}
					break;
				}
			}
			VwVaDdklZcwjDUKZLbrwYIhPAurOA = true;
		}

		public void ReleaseForUser(ILocalAccount user)
		{
			if (!VwVaDdklZcwjDUKZLbrwYIhPAurOA)
			{
				while (true)
				{
					uint num;
					switch ((num = 813879775u) % 3)
					{
					case 0u:
						continue;
					case 1u:
						throw new InvalidOperationException("Trying to release a user that has never been initialized, or that was already released.");
					}
					break;
				}
			}
			VwVaDdklZcwjDUKZLbrwYIhPAurOA = false;
		}

		public bool IsInitializedForUser(ILocalAccount user)
		{
			return VwVaDdklZcwjDUKZLbrwYIhPAurOA;
		}

		public void GetFriendListAsync(ILocalAccount user, Action<IRemoteAccount[], Exception> callback)
		{
			NQEvRbguEcJeCvqpxkUwKbhFwoQR(user);
			IRemoteAccount[] arg = default(IRemoteAccount[]);
			while (true)
			{
				int num = 426461897;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x18F6F3C0)) % 4)
					{
					case 3u:
						break;
					default:
						return;
					case 1u:
					{
						arg = ILfveHzaDClkGsqVNVlpgfUZDgKG(user);
						int num3;
						int num4;
						if (callback != null)
						{
							num3 = 1554517595;
							num4 = num3;
						}
						else
						{
							num3 = 1508252741;
							num4 = num3;
						}
						num = num3 ^ ((int)num2 * -1138805585);
						continue;
					}
					case 0u:
						callback.SafelyInvoke(arg, null);
						num = ((int)num2 * -1532486970) ^ -811009014;
						continue;
					case 2u:
						return;
					}
					break;
				}
			}
		}

		public Task<IRemoteAccount[]> GetFriendListAsync(ILocalAccount user)
		{
			NQEvRbguEcJeCvqpxkUwKbhFwoQR(user);
			return Task.FromResult(ILfveHzaDClkGsqVNVlpgfUZDgKG(user));
		}

		private void NQEvRbguEcJeCvqpxkUwKbhFwoQR(ILocalAccount P_0)
		{
			if (VwVaDdklZcwjDUKZLbrwYIhPAurOA)
			{
				return;
			}
			while (true)
			{
				uint num;
				switch ((num = 1364417798u) % 3)
				{
				case 0u:
					break;
				default:
					return;
				case 2u:
					throw new InvalidOperationException("InitializeForUser needs to be called for the user before any other functions.");
				case 1u:
					return;
				}
			}
		}

		private IRemoteAccount[] ILfveHzaDClkGsqVNVlpgfUZDgKG(ILocalAccount P_0)
		{
			int friendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
			CSteamID friendByIndex = default(CSteamID);
			int num3 = default(int);
			IRemoteAccount[] array = default(IRemoteAccount[]);
			while (true)
			{
				int num = -184555504;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1725521700)) % 8)
					{
					case 0u:
						break;
					case 3u:
						friendByIndex = SteamFriends.GetFriendByIndex(num3, EFriendFlags.k_EFriendFlagImmediate);
						num = -2076844598;
						continue;
					case 7u:
						num3 = 0;
						num = ((int)num2 * -2010134180) ^ -1546801038;
						continue;
					case 4u:
						array = new IRemoteAccount[friendCount];
						num = ((int)num2 * -287157438) ^ 0x3A60062B;
						continue;
					case 1u:
						num3++;
						num = ((int)num2 * -149536579) ^ -1349556493;
						continue;
					case 6u:
						array[num3] = new SteamRemoteAccount(friendByIndex, JvEZsMEbjtsmodyZFfToTIvzzHoC);
						num = ((int)num2 * -882017822) ^ 0xA86F79;
						continue;
					case 2u:
					{
						int num4;
						if (num3 >= friendCount)
						{
							num = -912700999;
							num4 = num;
						}
						else
						{
							num = -1809538313;
							num4 = num;
						}
						continue;
					}
					default:
						return array;
					}
					break;
				}
			}
		}
	}
}
