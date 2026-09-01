using System;
using System.Runtime.CompilerServices;
using BitCode.Graphics;
using BitCode.Users;
using JetBrains.Annotations;
using Steamworks;

namespace BitCode.Platform.Steamworks
{
	public abstract class SteamUserAccount : IUserAccount, ISteamUserAccount, IDisposable
	{
		[CompilerGenerated]
		private readonly ulong? iaMgLFhwmQQNdvehAIMcdjQmrdEAb;

		[CompilerGenerated]
		private readonly IUserAccountProperty<string> xVfdYLUPhFesSaNvxMLsSZKaaHYgA;

		[CompilerGenerated]
		private readonly IUserAccountProperty<ImageData> JCAFCApzgWwlLWbuNjgtfzxFKuan;

		[CompilerGenerated]
		private readonly IUserAccountProperty<string> GnUBaiAfOKMpmidtqpMbHrfyhIugb;

		[CompilerGenerated]
		private readonly IUserAccountProperty<UserAccountOnlineStatus> YoKZqpQSfTrGdrDChjRCgxcefnscb;

		[CompilerGenerated]
		private readonly CSteamID CQlgUyGDyGuveYsZsiAuntWfLwkJ;

		private readonly SteamService JvEZsMEbjtsmodyZFfToTIvzzHoC;

		private bool bBzFCPvegdcjojlzNVfWaJyAejlV;

		public ulong? OnlineAccountId
		{
			[CompilerGenerated]
			get
			{
				return iaMgLFhwmQQNdvehAIMcdjQmrdEAb;
			}
		}

		[NotNull]
		public IUserAccountProperty<string> Name
		{
			[CompilerGenerated]
			get
			{
				return xVfdYLUPhFesSaNvxMLsSZKaaHYgA;
			}
		}

		[NotNull]
		public IUserAccountProperty<ImageData> AvatarImage
		{
			[CompilerGenerated]
			get
			{
				return JCAFCApzgWwlLWbuNjgtfzxFKuan;
			}
		}

		[NotNull]
		public IUserAccountProperty<string> Presence
		{
			[CompilerGenerated]
			get
			{
				return GnUBaiAfOKMpmidtqpMbHrfyhIugb;
			}
		}

		[NotNull]
		public IUserAccountProperty<UserAccountOnlineStatus> OnlineStatus
		{
			[CompilerGenerated]
			get
			{
				return YoKZqpQSfTrGdrDChjRCgxcefnscb;
			}
		}

		public CSteamID SteamId
		{
			[CompilerGenerated]
			get
			{
				return CQlgUyGDyGuveYsZsiAuntWfLwkJ;
			}
		}

		public SteamUserAccount(CSteamID steamId, SteamService steamService)
		{
			JvEZsMEbjtsmodyZFfToTIvzzHoC = steamService;
			CQlgUyGDyGuveYsZsiAuntWfLwkJ = steamId;
			iaMgLFhwmQQNdvehAIMcdjQmrdEAb = steamId.m_SteamID;
			xVfdYLUPhFesSaNvxMLsSZKaaHYgA = new UserAccountSyncProperty<string>("Name", this, qzodByLHlLVwRzvJCeVQgNBrtJyg);
			JCAFCApzgWwlLWbuNjgtfzxFKuan = new UserAccountAsyncProperty<ImageData>("Avatar Image", this, qzodByLHlLVwRzvJCeVQgNBrtJyg);
			GnUBaiAfOKMpmidtqpMbHrfyhIugb = new UserAccountSyncProperty<string>("Presence", this, qzodByLHlLVwRzvJCeVQgNBrtJyg);
			YoKZqpQSfTrGdrDChjRCgxcefnscb = new UserAccountSyncProperty<UserAccountOnlineStatus>("Online Status", this, qzodByLHlLVwRzvJCeVQgNBrtJyg);
			JvEZsMEbjtsmodyZFfToTIvzzHoC.wibyoXdUyooAMfoIttKfmeojtQqM += wibyoXdUyooAMfoIttKfmeojtQqM;
			JvEZsMEbjtsmodyZFfToTIvzzHoC.brMXJPGlNBPaVzlNHNYLaCNsBIOjA += brMXJPGlNBPaVzlNHNYLaCNsBIOjA;
		}

		public abstract void UpdateName();

		public abstract void UpdateOnlineStatus();

		public void UpdateAvatarImage(int imageHandle)
		{
			CheckDisposed();
			UserAccountAsyncProperty<ImageData> userAccountAsyncProperty = (UserAccountAsyncProperty<ImageData>)AvatarImage;
			try
			{
				userAccountAsyncProperty.SetValue(Utilities.CreateImageDataFromHandle(imageHandle));
			}
			catch (Exception error)
			{
				userAccountAsyncProperty.SetError(error);
			}
		}

		public void UpdatePresence()
		{
			CheckDisposed();
			if (SteamFriends.GetFriendRichPresenceKeyCount(SteamId) <= 0)
			{
				goto IL_0014;
			}
			goto IL_004e;
			IL_0014:
			int num = -1151608345;
			goto IL_0019;
			IL_0019:
			string friendRichPresenceKeyByIndex = default(string);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -1617930052)) % 5)
				{
				case 0u:
					break;
				default:
					return;
				case 4u:
					return;
				case 3u:
					goto IL_004e;
				case 2u:
					Presence.SetValue(SteamFriends.GetFriendRichPresence(SteamId, friendRichPresenceKeyByIndex));
					num = ((int)num2 * -148214791) ^ -573596913;
					continue;
				case 1u:
					return;
				}
				break;
			}
			goto IL_0014;
			IL_004e:
			friendRichPresenceKeyByIndex = SteamFriends.GetFriendRichPresenceKeyByIndex(SteamId, 0);
			num = -642001582;
			goto IL_0019;
		}

		public void Dispose()
		{
			if (bBzFCPvegdcjojlzNVfWaJyAejlV)
			{
				goto IL_0008;
			}
			goto IL_0046;
			IL_0008:
			int num = 1299772758;
			goto IL_000d;
			IL_000d:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x1CBA133F)) % 6)
				{
				case 4u:
					break;
				default:
					return;
				case 1u:
					return;
				case 2u:
					goto IL_0046;
				case 5u:
					GC.SuppressFinalize(this);
					num = ((int)num2 * -377080485) ^ 0x3BD86509;
					continue;
				case 0u:
					bBzFCPvegdcjojlzNVfWaJyAejlV = true;
					num = ((int)num2 * -1987969201) ^ 0x655B03E2;
					continue;
				case 3u:
					return;
				}
				break;
			}
			goto IL_0008;
			IL_0046:
			aeJZgeZsWzhdjsjMjfUEeaOyPUISA();
			num = 872722865;
			goto IL_000d;
		}

		protected void CheckDisposed()
		{
			if (!bBzFCPvegdcjojlzNVfWaJyAejlV)
			{
				return;
			}
			while (true)
			{
				uint num;
				switch ((num = 1303618543u) % 3)
				{
				case 2u:
					break;
				default:
					return;
				case 1u:
					throw new ObjectDisposedException(GetType().FullName);
				case 0u:
					return;
				}
			}
		}

		private void brMXJPGlNBPaVzlNHNYLaCNsBIOjA(CSteamID P_0, int P_1)
		{
			CSteamID steamId = SteamId;
			while (true)
			{
				int num = 262830860;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x5AD14F87)) % 5)
					{
					case 0u:
						break;
					default:
						return;
					case 4u:
					{
						int num5;
						int num6;
						if (steamId.Equals(P_0))
						{
							num5 = 376546379;
							num6 = num5;
						}
						else
						{
							num5 = 1128342316;
							num6 = num5;
						}
						num = num5 ^ (int)(num2 * 1191634698);
						continue;
					}
					case 2u:
						UpdateAvatarImage(P_1);
						num = ((int)num2 * -1396784547) ^ 0x2E48642B;
						continue;
					case 3u:
					{
						int num3;
						int num4;
						if (!AvatarImage.Tracked)
						{
							num3 = 1253179832;
							num4 = num3;
						}
						else
						{
							num3 = 1463834880;
							num4 = num3;
						}
						num = num3 ^ (int)(num2 * 520999917);
						continue;
					}
					case 1u:
						return;
					}
					break;
				}
			}
		}

		private void wibyoXdUyooAMfoIttKfmeojtQqM(ulong P_0)
		{
			if (SteamId.m_SteamID != P_0)
			{
				return;
			}
			while (true)
			{
				int num = -1394519018;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -63442831)) % 4)
					{
					case 0u:
						break;
					default:
						return;
					case 3u:
					{
						int num3;
						int num4;
						if (!OnlineStatus.Tracked)
						{
							num3 = 95251232;
							num4 = num3;
						}
						else
						{
							num3 = 1019620491;
							num4 = num3;
						}
						num = num3 ^ (int)(num2 * 1913953993);
						continue;
					}
					case 1u:
						UpdateOnlineStatus();
						num = ((int)num2 * -1660317282) ^ -1709794775;
						continue;
					case 2u:
						return;
					}
					break;
				}
			}
		}

		private void aeJZgeZsWzhdjsjMjfUEeaOyPUISA()
		{
			Name.TrackingStarted -= qzodByLHlLVwRzvJCeVQgNBrtJyg;
			AvatarImage.TrackingStarted -= qzodByLHlLVwRzvJCeVQgNBrtJyg;
			Presence.TrackingStarted -= qzodByLHlLVwRzvJCeVQgNBrtJyg;
			OnlineStatus.TrackingStarted -= qzodByLHlLVwRzvJCeVQgNBrtJyg;
			while (true)
			{
				int num = 275314451;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x4D900CDE)) % 4)
					{
					case 0u:
						break;
					default:
						return;
					case 1u:
						JvEZsMEbjtsmodyZFfToTIvzzHoC.wibyoXdUyooAMfoIttKfmeojtQqM -= wibyoXdUyooAMfoIttKfmeojtQqM;
						num = (int)((num2 * 832981512) ^ 0x521E605D);
						continue;
					case 3u:
						JvEZsMEbjtsmodyZFfToTIvzzHoC.brMXJPGlNBPaVzlNHNYLaCNsBIOjA -= brMXJPGlNBPaVzlNHNYLaCNsBIOjA;
						num = ((int)num2 * -834290051) ^ -766274789;
						continue;
					case 2u:
						return;
					}
					break;
				}
			}
		}

		private void qzodByLHlLVwRzvJCeVQgNBrtJyg()
		{
			if (Name.NeedsLoading())
			{
				goto IL_0010;
			}
			goto IL_00fe;
			IL_0010:
			int num = 1612802828;
			goto IL_0015;
			IL_0015:
			int largeFriendAvatar = default(int);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0xB20C6BB)) % 11)
				{
				case 7u:
					break;
				default:
					return;
				case 5u:
					goto IL_0056;
				case 2u:
				{
					largeFriendAvatar = SteamFriends.GetLargeFriendAvatar(SteamId);
					int num3;
					int num4;
					if (largeFriendAvatar > 0)
					{
						num3 = -1300147147;
						num4 = num3;
					}
					else
					{
						num3 = -1223184021;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 783245109);
					continue;
				}
				case 10u:
					UpdatePresence();
					num = (int)(num2 * 970361747) ^ -1637922826;
					continue;
				case 0u:
					UpdateOnlineStatus();
					num = (int)((num2 * 770415429) ^ 0x4B7DE6A4);
					continue;
				case 6u:
					AvatarImage.Status = UserAccountPropertyStatus.Loading;
					num = 1961124019;
					continue;
				case 3u:
					UpdateName();
					num = (int)((num2 * 645833375) ^ 0x3FE270B1);
					continue;
				case 4u:
					goto IL_00fe;
				case 8u:
					UpdateAvatarImage(largeFriendAvatar);
					return;
				case 9u:
					goto IL_0139;
				case 1u:
					return;
				}
				break;
				IL_0139:
				int num5;
				if (!OnlineStatus.NeedsLoading())
				{
					num = 1551538873;
					num5 = num;
				}
				else
				{
					num = 1612920898;
					num5 = num;
				}
				continue;
				IL_0056:
				int num6;
				if (!AvatarImage.NeedsLoading())
				{
					num = 1961124019;
					num6 = num;
				}
				else
				{
					num = 2133655420;
					num6 = num;
				}
			}
			goto IL_0010;
			IL_00fe:
			int num7;
			if (Presence.NeedsLoading())
			{
				num = 925857317;
				num7 = num;
			}
			else
			{
				num = 1302557516;
				num7 = num;
			}
			goto IL_0015;
		}
	}
}
