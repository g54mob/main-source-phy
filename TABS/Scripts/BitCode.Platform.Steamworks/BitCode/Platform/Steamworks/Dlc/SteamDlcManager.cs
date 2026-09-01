using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using BitCode.Dlc;
using BitCode.Users;
using Steamworks;

namespace BitCode.Platform.Steamworks.Dlc
{
	public class SteamDlcManager : IPlatformService, IDlcManager
	{
		private SteamService JvEZsMEbjtsmodyZFfToTIvzzHoC;

		[CompilerGenerated]
		private Action<IDlc> sucYuzvvXYotBJKHhHxpfktumRpUA;

		public event Action<IDlc> InstalledDlc
		{
			[CompilerGenerated]
			add
			{
				Action<IDlc> action = sucYuzvvXYotBJKHhHxpfktumRpUA;
				while (true)
				{
					int num = -1423482124;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -941785677)) % 3)
						{
						case 0u:
							break;
						default:
							return;
						case 1u:
						{
							Action<IDlc> action2 = action;
							Action<IDlc> value2 = (Action<IDlc>)Delegate.Combine(action2, value);
							action = Interlocked.CompareExchange(ref sucYuzvvXYotBJKHhHxpfktumRpUA, value2, action2);
							int num3;
							if ((object)action == action2)
							{
								num = -1360103147;
								num3 = num;
							}
							else
							{
								num = -1423482124;
								num3 = num;
							}
							continue;
						}
						case 2u:
							return;
						}
						break;
					}
				}
			}
			[CompilerGenerated]
			remove
			{
				Action<IDlc> action = sucYuzvvXYotBJKHhHxpfktumRpUA;
				Action<IDlc> action2 = default(Action<IDlc>);
				while (true)
				{
					int num = 413050077;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x4B871990)) % 4)
						{
						case 2u:
							break;
						default:
							return;
						case 1u:
						{
							action2 = action;
							Action<IDlc> value2 = (Action<IDlc>)Delegate.Remove(action2, value);
							action = Interlocked.CompareExchange(ref sucYuzvvXYotBJKHhHxpfktumRpUA, value2, action2);
							num = 1150849040;
							continue;
						}
						case 0u:
						{
							int num3;
							int num4;
							if ((object)action == action2)
							{
								num3 = -85301373;
								num4 = num3;
							}
							else
							{
								num3 = -1088628771;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 2010150466);
							continue;
						}
						case 3u:
							return;
						}
						break;
					}
				}
			}
		}

		event Action<IPlatformService, Exception> IPlatformService.InternalErrorOccurred
		{
			add
			{
			}
			remove
			{
			}
		}

		public SteamDlcManager(SteamService steamService)
		{
			while (true)
			{
				int num = -798689134;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1730975199)) % 3)
					{
					case 2u:
						break;
					case 1u:
						goto IL_0028;
					default:
						steamService.KoYBuZsLgJIySGTwfWiCHtQpPxJi += YjSGJFaSPhZwgHfqPrMpDYKqVxkBA;
						return;
					}
					break;
					IL_0028:
					JvEZsMEbjtsmodyZFfToTIvzzHoC = steamService;
					num = ((int)num2 * -520676567) ^ -1326486321;
				}
			}
		}

		public void Initialize()
		{
		}

		public bool CheckDlcInstalled(AppId_t dlcId)
		{
			return SteamApps.BIsDlcInstalled(dlcId);
		}

		public void GetDlcForUserAsync(ILocalAccount userAccount, Action<IDlc[], Exception> doneCallback)
		{
			if (doneCallback == null)
			{
				return;
			}
			while (true)
			{
				int num = 2132036457;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x66955873)) % 3)
					{
					case 2u:
						break;
					default:
						return;
					case 1u:
						goto IL_0025;
					case 0u:
						return;
					}
					break;
					IL_0025:
					doneCallback(EWXBRbeafGCcVauADlFDUeVDSYhPB(), null);
					num = ((int)num2 * -698632006) ^ -440345304;
				}
			}
		}

		public Task<IDlc[]> GetDlcForUserAsync(ILocalAccount userAccount)
		{
			return Task.FromResult(EWXBRbeafGCcVauADlFDUeVDSYhPB());
		}

		private IDlc[] EWXBRbeafGCcVauADlFDUeVDSYhPB()
		{
			int dLCCount = SteamApps.GetDLCCount();
			IDlc[] array = new IDlc[dLCCount];
			int num3 = default(int);
			AppId_t pAppID = default(AppId_t);
			while (true)
			{
				int num = 13345673;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x437E3319)) % 10)
					{
					case 0u:
						break;
					case 3u:
						num3++;
						num = 1573879586;
						continue;
					case 7u:
					{
						int num5;
						if (num3 >= dLCCount)
						{
							num = 14622805;
							num5 = num;
						}
						else
						{
							num = 2062598390;
							num5 = num;
						}
						continue;
					}
					case 5u:
						array[num3] = new SteamDlc(pAppID);
						num = ((int)num2 * -1899595966) ^ 0x36C8D54B;
						continue;
					case 6u:
						num3 = 0;
						num = ((int)num2 * -394571703) ^ 0x17642FC4;
						continue;
					case 1u:
					{
						int num4;
						if (!SteamApps.BGetDLCDataByIndex(num3, out pAppID, out var _, out var _, 128))
						{
							num = 683066785;
							num4 = num;
						}
						else
						{
							num = 834372450;
							num4 = num;
						}
						continue;
					}
					case 4u:
						num = (int)(num2 * 404923846) ^ -48874570;
						continue;
					case 2u:
						throw new SteamApiException($"Failed to retrieve DLC[{num3}]'s details from Steam.");
					case 9u:
						num = ((int)num2 * -657025500) ^ 0x544211F6;
						continue;
					default:
						return array;
					}
					break;
				}
			}
		}

		private void YjSGJFaSPhZwgHfqPrMpDYKqVxkBA(AppId_t P_0)
		{
			SteamDlc obj = new SteamDlc(P_0);
			while (true)
			{
				int num = -130426527;
				while (true)
				{
					uint num2;
					Action<IDlc> action;
					switch ((num2 = (uint)(num ^ -1517842116)) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 1u:
						action = sucYuzvvXYotBJKHhHxpfktumRpUA;
						if (action != null)
						{
							goto IL_0034;
						}
						return;
					case 2u:
						return;
					}
					break;
					IL_0034:
					action(obj);
					num = ((int)num2 * -202689023) ^ -506992795;
				}
			}
		}
	}
}
