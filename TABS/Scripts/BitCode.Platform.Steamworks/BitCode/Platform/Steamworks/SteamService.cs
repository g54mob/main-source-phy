using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using AOT;
using BitCode.Extensions;
using JetBrains.Annotations;
using Steamworks;

namespace BitCode.Platform.Steamworks
{
	public class SteamService : IPlatformService, IUpdateableService, IDisposable
	{
		private Callback<PersonaStateChange_t> WCAagXVPNvcyjDqjwgltxtIimOhhb;

		private Callback<AvatarImageLoaded_t> AWDCFnNLmBzBPbqqoHIkpYRCKueH;

		private Callback<DlcInstalled_t> NUMhKPMglUXLtYezheeGaDlxYSkoA;

		private Callback<UserStatsReceived_t> zASBliCAlHSGmtAYZVuznXwEOXno;

		private Callback<UserAchievementStored_t> ylLttZAbgUZKpCXxkBiAhXeZrrQv;

		private Callback<UserStatsStored_t> CxzLbHkMtSDlfazJoqaIObxwlRYlA;

		private SteamAPIWarningMessageHook_t kvOvpdPimRjCubqwMIeFEfTchzih;

		private readonly IServiceUpdater UiqWRGkuDLeBUJrcKfNWbgaWARxT;

		private bool bBzFCPvegdcjojlzNVfWaJyAejlV;

		[CompilerGenerated]
		private Action<ulong> m_wibyoXdUyooAMfoIttKfmeojtQqM;

		[CompilerGenerated]
		private Action<CSteamID, int> m_brMXJPGlNBPaVzlNHNYLaCNsBIOjA;

		[CompilerGenerated]
		private Action<AppId_t> m_KoYBuZsLgJIySGTwfWiCHtQpPxJi;

		[CompilerGenerated]
		private Action<EResult> m_HrIKZKRBQoVhfFMPLlIaHyKVuyZv;

		[CompilerGenerated]
		private Action<UserAchievementStored_t> m_MKrFmoBXlKAuTQshDJNqgkNBHxBA;

		[CompilerGenerated]
		private Action<UserStatsStored_t> m_DPRKwlHJEMbmBgfJufKVpcmQhfFw;

		event Action<IPlatformService, Exception> IPlatformService.InternalErrorOccurred
		{
			add
			{
			}
			remove
			{
			}
		}

		internal event Action<ulong> wibyoXdUyooAMfoIttKfmeojtQqM
		{
			[CompilerGenerated]
			add
			{
				Action<ulong> action = this.m_wibyoXdUyooAMfoIttKfmeojtQqM;
				Action<ulong> action2 = default(Action<ulong>);
				Action<ulong> value2 = default(Action<ulong>);
				while (true)
				{
					int num = 1075571787;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x13C30057)) % 5)
						{
						case 2u:
							break;
						default:
							return;
						case 3u:
							action2 = action;
							num = 389590495;
							continue;
						case 1u:
						{
							action = Interlocked.CompareExchange(ref this.m_wibyoXdUyooAMfoIttKfmeojtQqM, value2, action2);
							int num3;
							int num4;
							if ((object)action != action2)
							{
								num3 = 1662070221;
								num4 = num3;
							}
							else
							{
								num3 = 366751596;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 579368034);
							continue;
						}
						case 4u:
							value2 = (Action<ulong>)Delegate.Combine(action2, b);
							num = ((int)num2 * -1676225200) ^ -803963932;
							continue;
						case 0u:
							return;
						}
						break;
					}
				}
			}
			[CompilerGenerated]
			remove
			{
				Action<ulong> action = this.m_wibyoXdUyooAMfoIttKfmeojtQqM;
				Action<ulong> value2 = default(Action<ulong>);
				Action<ulong> action2 = default(Action<ulong>);
				while (true)
				{
					int num = -1415009247;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -935401002)) % 6)
						{
						case 0u:
							break;
						default:
							return;
						case 4u:
							action = Interlocked.CompareExchange(ref this.m_wibyoXdUyooAMfoIttKfmeojtQqM, value2, action2);
							num = ((int)num2 * -164756981) ^ 0x7E77EB3B;
							continue;
						case 2u:
							value2 = (Action<ulong>)Delegate.Remove(action2, value3);
							num = ((int)num2 * -1486406699) ^ -2116200700;
							continue;
						case 1u:
							action2 = action;
							num = -55128872;
							continue;
						case 3u:
						{
							int num3;
							int num4;
							if ((object)action != action2)
							{
								num3 = 1357156775;
								num4 = num3;
							}
							else
							{
								num3 = 1240596797;
								num4 = num3;
							}
							num = num3 ^ ((int)num2 * -1955588314);
							continue;
						}
						case 5u:
							return;
						}
						break;
					}
				}
			}
		}

		internal event Action<CSteamID, int> brMXJPGlNBPaVzlNHNYLaCNsBIOjA
		{
			[CompilerGenerated]
			add
			{
				Action<CSteamID, int> action = this.m_brMXJPGlNBPaVzlNHNYLaCNsBIOjA;
				Action<CSteamID, int> value2 = default(Action<CSteamID, int>);
				Action<CSteamID, int> action2 = default(Action<CSteamID, int>);
				while (true)
				{
					int num = -838766701;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -1880474499)) % 5)
						{
						case 3u:
							break;
						default:
							return;
						case 2u:
						{
							action = Interlocked.CompareExchange(ref this.m_brMXJPGlNBPaVzlNHNYLaCNsBIOjA, value2, action2);
							int num3;
							int num4;
							if ((object)action == action2)
							{
								num3 = 1319585925;
								num4 = num3;
							}
							else
							{
								num3 = 365903657;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 1685221737);
							continue;
						}
						case 0u:
							value2 = (Action<CSteamID, int>)Delegate.Combine(action2, b);
							num = ((int)num2 * -1650534000) ^ 0x69F8BAA7;
							continue;
						case 1u:
							action2 = action;
							num = -503896462;
							continue;
						case 4u:
							return;
						}
						break;
					}
				}
			}
			[CompilerGenerated]
			remove
			{
				Action<CSteamID, int> action = this.m_brMXJPGlNBPaVzlNHNYLaCNsBIOjA;
				Action<CSteamID, int> action2 = default(Action<CSteamID, int>);
				Action<CSteamID, int> value2 = default(Action<CSteamID, int>);
				while (true)
				{
					int num = -1134495629;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -2053850137)) % 5)
						{
						case 3u:
							break;
						default:
							return;
						case 4u:
						{
							int num3;
							int num4;
							if ((object)action == action2)
							{
								num3 = 1905149565;
								num4 = num3;
							}
							else
							{
								num3 = 1831244049;
								num4 = num3;
							}
							num = num3 ^ ((int)num2 * -1650364854);
							continue;
						}
						case 2u:
							action = Interlocked.CompareExchange(ref this.m_brMXJPGlNBPaVzlNHNYLaCNsBIOjA, value2, action2);
							num = (int)((num2 * 1837284956) ^ 0x4D39658E);
							continue;
						case 1u:
							action2 = action;
							value2 = (Action<CSteamID, int>)Delegate.Remove(action2, value3);
							num = -729092372;
							continue;
						case 0u:
							return;
						}
						break;
					}
				}
			}
		}

		internal event Action<AppId_t> KoYBuZsLgJIySGTwfWiCHtQpPxJi
		{
			[CompilerGenerated]
			add
			{
				Action<AppId_t> action = this.m_KoYBuZsLgJIySGTwfWiCHtQpPxJi;
				Action<AppId_t> action2 = default(Action<AppId_t>);
				Action<AppId_t> value2 = default(Action<AppId_t>);
				while (true)
				{
					int num = -1799826022;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -395050754)) % 5)
						{
						case 3u:
							break;
						default:
							return;
						case 1u:
							action2 = action;
							num = -468266791;
							continue;
						case 4u:
						{
							action = Interlocked.CompareExchange(ref this.m_KoYBuZsLgJIySGTwfWiCHtQpPxJi, value2, action2);
							int num3;
							int num4;
							if ((object)action != action2)
							{
								num3 = 1266512970;
								num4 = num3;
							}
							else
							{
								num3 = 1521354940;
								num4 = num3;
							}
							num = num3 ^ ((int)num2 * -526682840);
							continue;
						}
						case 2u:
							value2 = (Action<AppId_t>)Delegate.Combine(action2, b);
							num = (int)(num2 * 243046335) ^ -1932167467;
							continue;
						case 0u:
							return;
						}
						break;
					}
				}
			}
			[CompilerGenerated]
			remove
			{
				Action<AppId_t> action = this.m_KoYBuZsLgJIySGTwfWiCHtQpPxJi;
				Action<AppId_t> action2 = default(Action<AppId_t>);
				Action<AppId_t> value2 = default(Action<AppId_t>);
				while (true)
				{
					int num = 875122770;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x5D055B4D)) % 5)
						{
						case 3u:
							break;
						default:
							return;
						case 1u:
							action2 = action;
							num = 1300535400;
							continue;
						case 0u:
						{
							action = Interlocked.CompareExchange(ref this.m_KoYBuZsLgJIySGTwfWiCHtQpPxJi, value2, action2);
							int num3;
							int num4;
							if ((object)action == action2)
							{
								num3 = 1915859475;
								num4 = num3;
							}
							else
							{
								num3 = 1777134999;
								num4 = num3;
							}
							num = num3 ^ ((int)num2 * -1624490063);
							continue;
						}
						case 2u:
							value2 = (Action<AppId_t>)Delegate.Remove(action2, value3);
							num = (int)((num2 * 797014731) ^ 0x1153234F);
							continue;
						case 4u:
							return;
						}
						break;
					}
				}
			}
		}

		internal event Action<EResult> HrIKZKRBQoVhfFMPLlIaHyKVuyZv
		{
			[CompilerGenerated]
			add
			{
				Action<EResult> action = this.m_HrIKZKRBQoVhfFMPLlIaHyKVuyZv;
				while (true)
				{
					int num = -409541774;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -945290983)) % 3)
						{
						case 2u:
							break;
						default:
							return;
						case 1u:
						{
							Action<EResult> action2 = action;
							Action<EResult> value2 = (Action<EResult>)Delegate.Combine(action2, b);
							action = Interlocked.CompareExchange(ref this.m_HrIKZKRBQoVhfFMPLlIaHyKVuyZv, value2, action2);
							int num3;
							if ((object)action == action2)
							{
								num = -893519626;
								num3 = num;
							}
							else
							{
								num = -409541774;
								num3 = num;
							}
							continue;
						}
						case 0u:
							return;
						}
						break;
					}
				}
			}
			[CompilerGenerated]
			remove
			{
				Action<EResult> action = this.m_HrIKZKRBQoVhfFMPLlIaHyKVuyZv;
				Action<EResult> value2 = default(Action<EResult>);
				Action<EResult> action2 = default(Action<EResult>);
				while (true)
				{
					int num = 1561358512;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x55D64237)) % 6)
						{
						case 0u:
							break;
						default:
							return;
						case 5u:
							action = Interlocked.CompareExchange(ref this.m_HrIKZKRBQoVhfFMPLlIaHyKVuyZv, value2, action2);
							num = ((int)num2 * -1495821952) ^ -880403882;
							continue;
						case 2u:
							value2 = (Action<EResult>)Delegate.Remove(action2, value3);
							num = (int)(num2 * 1378211530) ^ -1662123294;
							continue;
						case 1u:
							action2 = action;
							num = 267227579;
							continue;
						case 3u:
						{
							int num3;
							int num4;
							if ((object)action != action2)
							{
								num3 = -1044163345;
								num4 = num3;
							}
							else
							{
								num3 = -1284652302;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 1776433215);
							continue;
						}
						case 4u:
							return;
						}
						break;
					}
				}
			}
		}

		internal event Action<UserAchievementStored_t> MKrFmoBXlKAuTQshDJNqgkNBHxBA
		{
			[CompilerGenerated]
			add
			{
				Action<UserAchievementStored_t> action = this.m_MKrFmoBXlKAuTQshDJNqgkNBHxBA;
				Action<UserAchievementStored_t> action2 = default(Action<UserAchievementStored_t>);
				Action<UserAchievementStored_t> value2 = default(Action<UserAchievementStored_t>);
				while (true)
				{
					int num = -7861678;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -1742747981)) % 4)
						{
						case 2u:
							break;
						default:
							return;
						case 1u:
							action2 = action;
							value2 = (Action<UserAchievementStored_t>)Delegate.Combine(action2, b);
							num = -408087077;
							continue;
						case 0u:
						{
							action = Interlocked.CompareExchange(ref this.m_MKrFmoBXlKAuTQshDJNqgkNBHxBA, value2, action2);
							int num3;
							int num4;
							if ((object)action == action2)
							{
								num3 = 1303421724;
								num4 = num3;
							}
							else
							{
								num3 = 1585211410;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 179245160);
							continue;
						}
						case 3u:
							return;
						}
						break;
					}
				}
			}
			[CompilerGenerated]
			remove
			{
				Action<UserAchievementStored_t> action = this.m_MKrFmoBXlKAuTQshDJNqgkNBHxBA;
				Action<UserAchievementStored_t> action2 = default(Action<UserAchievementStored_t>);
				Action<UserAchievementStored_t> value2 = default(Action<UserAchievementStored_t>);
				while (true)
				{
					int num = -1111786118;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -1254706301)) % 5)
						{
						case 2u:
							break;
						default:
							return;
						case 3u:
						{
							int num3;
							int num4;
							if ((object)action != action2)
							{
								num3 = 1613546474;
								num4 = num3;
							}
							else
							{
								num3 = 92946826;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 1083558481);
							continue;
						}
						case 0u:
							action = Interlocked.CompareExchange(ref this.m_MKrFmoBXlKAuTQshDJNqgkNBHxBA, value2, action2);
							num = ((int)num2 * -1975531451) ^ 0x2596426E;
							continue;
						case 4u:
							action2 = action;
							value2 = (Action<UserAchievementStored_t>)Delegate.Remove(action2, value3);
							num = -572226214;
							continue;
						case 1u:
							return;
						}
						break;
					}
				}
			}
		}

		internal event Action<UserStatsStored_t> DPRKwlHJEMbmBgfJufKVpcmQhfFw
		{
			[CompilerGenerated]
			add
			{
				Action<UserStatsStored_t> action = this.m_DPRKwlHJEMbmBgfJufKVpcmQhfFw;
				Action<UserStatsStored_t> action2 = default(Action<UserStatsStored_t>);
				Action<UserStatsStored_t> value2 = default(Action<UserStatsStored_t>);
				while (true)
				{
					int num = -33847367;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -388404996)) % 4)
						{
						case 0u:
							break;
						default:
							return;
						case 1u:
							action2 = action;
							value2 = (Action<UserStatsStored_t>)Delegate.Combine(action2, b);
							num = -573665750;
							continue;
						case 2u:
						{
							action = Interlocked.CompareExchange(ref this.m_DPRKwlHJEMbmBgfJufKVpcmQhfFw, value2, action2);
							int num3;
							int num4;
							if ((object)action == action2)
							{
								num3 = 1217263131;
								num4 = num3;
							}
							else
							{
								num3 = 1098804777;
								num4 = num3;
							}
							num = num3 ^ ((int)num2 * -1869621160);
							continue;
						}
						case 3u:
							return;
						}
						break;
					}
				}
			}
			[CompilerGenerated]
			remove
			{
				Action<UserStatsStored_t> action = this.m_DPRKwlHJEMbmBgfJufKVpcmQhfFw;
				Action<UserStatsStored_t> action2 = default(Action<UserStatsStored_t>);
				while (true)
				{
					int num = 1025131472;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x75796AE4)) % 5)
						{
						case 3u:
							break;
						default:
							return;
						case 0u:
						{
							int num3;
							int num4;
							if ((object)action != action2)
							{
								num3 = -1449266774;
								num4 = num3;
							}
							else
							{
								num3 = -642679327;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 1937781078);
							continue;
						}
						case 1u:
						{
							Action<UserStatsStored_t> value2 = (Action<UserStatsStored_t>)Delegate.Remove(action2, value3);
							action = Interlocked.CompareExchange(ref this.m_DPRKwlHJEMbmBgfJufKVpcmQhfFw, value2, action2);
							num = (int)(num2 * 1143662463) ^ -1078077268;
							continue;
						}
						case 4u:
							action2 = action;
							num = 1572234597;
							continue;
						case 2u:
							return;
						}
						break;
					}
				}
			}
		}

		public SteamService([NotNull] IServiceUpdater serviceUpdater, AppId_t appId)
		{
			while (true)
			{
				int num = -194395878;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -889225761)) % 5)
					{
					case 3u:
						break;
					case 2u:
					{
						int num3;
						int num4;
						if (!kUHIEkpcdpgrcZWEaTXMYsKcJgiR(appId))
						{
							num3 = 241691042;
							num4 = num3;
						}
						else
						{
							num3 = 2075642772;
							num4 = num3;
						}
						num = num3 ^ ((int)num2 * -824287612);
						continue;
					}
					case 1u:
						throw new SteamNotInitializedException("Steam Service failed to initialize Steamworks API.");
					case 0u:
						UiqWRGkuDLeBUJrcKfNWbgaWARxT = serviceUpdater ?? throw new ArgumentNullException("serviceUpdater");
						num = -1324693397;
						continue;
					default:
						ptjpPgWHdgemSaUuxOyjySrdJVGcA();
						serviceUpdater.RegisterService(this);
						return;
					}
					break;
				}
			}
		}

		public void Update()
		{
			EfgdiGtxKrtefuGKEgjWCSAEbeND();
			SteamAPI.RunCallbacks();
		}

		~SteamService()
		{
			EBOFmlFXuTURaLShcWPnQiZUpheN(false);
		}

		public void Dispose()
		{
			EBOFmlFXuTURaLShcWPnQiZUpheN(true);
			GC.SuppressFinalize(this);
		}

		private void EfgdiGtxKrtefuGKEgjWCSAEbeND()
		{
			if (!bBzFCPvegdcjojlzNVfWaJyAejlV)
			{
				return;
			}
			while (true)
			{
				uint num;
				switch ((num = 1721263492u) % 3)
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

		private void EBOFmlFXuTURaLShcWPnQiZUpheN(bool P_0)
		{
			if (bBzFCPvegdcjojlzNVfWaJyAejlV)
			{
				goto IL_000b;
			}
			goto IL_0152;
			IL_000b:
			int num = -996646000;
			goto IL_0010;
			IL_0010:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -2124495258)) % 23)
				{
				case 12u:
					break;
				case 9u:
					UiqWRGkuDLeBUJrcKfNWbgaWARxT.DeregisterService(this);
					num = -568032553;
					continue;
				case 0u:
					goto IL_0097;
				case 18u:
					SteamClient.SetWarningMessageHook(null);
					num = (int)((num2 * 31881761) ^ 0x272A4F8E);
					continue;
				case 10u:
					kvOvpdPimRjCubqwMIeFEfTchzih = null;
					num = ((int)num2 * -166065051) ^ -1666199180;
					continue;
				case 4u:
					goto IL_00e4;
				case 13u:
					zASBliCAlHSGmtAYZVuznXwEOXno = null;
					num = (int)((num2 * 59900950) ^ 0x3599CA43);
					continue;
				case 7u:
					zASBliCAlHSGmtAYZVuznXwEOXno.Dispose();
					num = ((int)num2 * -1346915341) ^ -592191887;
					continue;
				case 22u:
					goto IL_0136;
				case 20u:
					goto IL_0152;
				case 5u:
					CxzLbHkMtSDlfazJoqaIObxwlRYlA.Dispose();
					num = (int)(num2 * 891635883) ^ -1482089405;
					continue;
				case 6u:
					WCAagXVPNvcyjDqjwgltxtIimOhhb = null;
					num = (int)(num2 * 1278377100) ^ -806595421;
					continue;
				case 14u:
					goto IL_01a4;
				case 15u:
					ylLttZAbgUZKpCXxkBiAhXeZrrQv.Dispose();
					ylLttZAbgUZKpCXxkBiAhXeZrrQv = null;
					num = ((int)num2 * -1630129183) ^ 0x19FA67B4;
					continue;
				case 2u:
					WCAagXVPNvcyjDqjwgltxtIimOhhb.Dispose();
					num = ((int)num2 * -131993131) ^ 0x4FFA59C8;
					continue;
				case 3u:
					NUMhKPMglUXLtYezheeGaDlxYSkoA.Dispose();
					NUMhKPMglUXLtYezheeGaDlxYSkoA = null;
					num = ((int)num2 * -1871038303) ^ 0x7C558F88;
					continue;
				case 16u:
					return;
				case 17u:
					AWDCFnNLmBzBPbqqoHIkpYRCKueH.Dispose();
					AWDCFnNLmBzBPbqqoHIkpYRCKueH = null;
					num = (int)(num2 * 302023355) ^ -2037130900;
					continue;
				case 11u:
					goto IL_025c;
				case 8u:
				{
					int num3;
					int num4;
					if (WCAagXVPNvcyjDqjwgltxtIimOhhb != null)
					{
						num3 = 406790995;
						num4 = num3;
					}
					else
					{
						num3 = 1071792993;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 153104110);
					continue;
				}
				case 21u:
					goto IL_029c;
				case 1u:
					CxzLbHkMtSDlfazJoqaIObxwlRYlA = null;
					num = ((int)num2 * -1367755325) ^ 0x15E14ACE;
					continue;
				default:
					bBzFCPvegdcjojlzNVfWaJyAejlV = true;
					return;
				}
				break;
				IL_029c:
				int num5;
				if (NUMhKPMglUXLtYezheeGaDlxYSkoA == null)
				{
					num = -1638725080;
					num5 = num;
				}
				else
				{
					num = -2021374010;
					num5 = num;
				}
				continue;
				IL_01a4:
				int num6;
				if (ylLttZAbgUZKpCXxkBiAhXeZrrQv != null)
				{
					num = -587807352;
					num6 = num;
				}
				else
				{
					num = -274876006;
					num6 = num;
				}
				continue;
				IL_00e4:
				SteamAPI.Shutdown();
				int num7;
				if (!P_0)
				{
					num = -568032553;
					num7 = num;
				}
				else
				{
					num = -1011799001;
					num7 = num;
				}
				continue;
				IL_025c:
				int num8;
				if (zASBliCAlHSGmtAYZVuznXwEOXno == null)
				{
					num = -1096097799;
					num8 = num;
				}
				else
				{
					num = -1402247290;
					num8 = num;
				}
				continue;
				IL_0097:
				int num9;
				if (AWDCFnNLmBzBPbqqoHIkpYRCKueH == null)
				{
					num = -607491815;
					num9 = num;
				}
				else
				{
					num = -1787914007;
					num9 = num;
				}
				continue;
				IL_0136:
				int num10;
				if (CxzLbHkMtSDlfazJoqaIObxwlRYlA == null)
				{
					num = -1629021953;
					num10 = num;
				}
				else
				{
					num = -1215828228;
					num10 = num;
				}
			}
			goto IL_000b;
			IL_0152:
			int num11;
			if (kvOvpdPimRjCubqwMIeFEfTchzih != null)
			{
				num = -1574216050;
				num11 = num;
			}
			else
			{
				num = -895439500;
				num11 = num;
			}
			goto IL_0010;
		}

		private bool kUHIEkpcdpgrcZWEaTXMYsKcJgiR(AppId_t P_0)
		{
			if (!Packsize.Test())
			{
				goto IL_0007;
			}
			goto IL_0053;
			IL_0007:
			int num = -2031497413;
			goto IL_000c;
			IL_000c:
			uint num2;
			switch ((num2 = (uint)(num ^ -1638646961)) % 5)
			{
			case 2u:
				break;
			case 3u:
				return false;
			case 1u:
				return false;
			case 4u:
				goto IL_0053;
			default:
				goto IL_006c;
			}
			goto IL_0007;
			IL_012b:
			bool result = default(bool);
			return result;
			IL_006c:
			try
			{
				if (SteamAPI.RestartAppIfNecessary(P_0))
				{
					while (true)
					{
						switch ((num2 = 163280792u) % 3)
						{
						case 0u:
							break;
						default:
							goto end_IL_0074;
						case 2u:
							result = false;
							goto IL_012b;
						case 1u:
							goto end_IL_0074;
						}
						continue;
						end_IL_0074:
						break;
					}
				}
			}
			catch (DllNotFoundException)
			{
				while (true)
				{
					IL_00af:
					int num3 = -555890887;
					while (true)
					{
						switch ((num2 = (uint)(num3 ^ -1638646961)) % 3)
						{
						case 0u:
							break;
						case 1u:
							goto IL_00d1;
						default:
							goto end_IL_00b4;
						}
						goto IL_00af;
						IL_00d1:
						result = false;
						num3 = ((int)num2 * -2099774973) ^ -536868874;
						continue;
						end_IL_00b4:
						break;
					}
					break;
				}
				goto IL_012b;
			}
			if (!SteamAPI.Init())
			{
				while (true)
				{
					switch ((num2 = 1817695354u) % 4)
					{
					case 0u:
						break;
					case 2u:
						return false;
					case 1u:
						goto end_IL_00eb;
					default:
						goto IL_012b;
					}
					continue;
					end_IL_00eb:
					break;
				}
			}
			return true;
			IL_0053:
			int num4;
			if (DllCheck.Test())
			{
				num = -1475230446;
				num4 = num;
			}
			else
			{
				num = -716526489;
				num4 = num;
			}
			goto IL_000c;
		}

		private void ptjpPgWHdgemSaUuxOyjySrdJVGcA()
		{
			WCAagXVPNvcyjDqjwgltxtIimOhhb = Callback<PersonaStateChange_t>.Create(SxLYbNPTXxrTGykxLNIvOzlBqnSx);
			AWDCFnNLmBzBPbqqoHIkpYRCKueH = Callback<AvatarImageLoaded_t>.Create(xgawRajbGhAopBOfdjwwBMuMdraNA);
			while (true)
			{
				int num = -1713784016;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -70411500)) % 7)
					{
					case 0u:
						break;
					default:
						return;
					case 5u:
						SteamClient.SetWarningMessageHook(kvOvpdPimRjCubqwMIeFEfTchzih);
						num = (int)((num2 * 297596711) ^ 0x72DC802F);
						continue;
					case 2u:
						kvOvpdPimRjCubqwMIeFEfTchzih = AHZSFkhlLEhWnuOPxrtNDBlHGYaG;
						num = ((int)num2 * -2124240061) ^ 0x5233388F;
						continue;
					case 6u:
						zASBliCAlHSGmtAYZVuznXwEOXno = Callback<UserStatsReceived_t>.Create(SYDwtiffRTvzRVyMSOEBWIyVcxAA);
						num = (int)((num2 * 1488789732) ^ 0xBFD5756);
						continue;
					case 3u:
						ylLttZAbgUZKpCXxkBiAhXeZrrQv = Callback<UserAchievementStored_t>.Create(QaUGAHcDXPWhviwFFfSQqcjjOROi);
						CxzLbHkMtSDlfazJoqaIObxwlRYlA = Callback<UserStatsStored_t>.Create(aFYpwgkoTsGiXTDsuARqbwnXeBQb);
						num = (int)(num2 * 944858510) ^ -1031903439;
						continue;
					case 1u:
						NUMhKPMglUXLtYezheeGaDlxYSkoA = Callback<DlcInstalled_t>.Create(gmNqSzocKjZRkReAHVvIOCOtfnSBA);
						num = (int)(num2 * 1663380489) ^ -551257591;
						continue;
					case 4u:
						return;
					}
					break;
				}
			}
		}

		private void SxLYbNPTXxrTGykxLNIvOzlBqnSx(PersonaStateChange_t P_0)
		{
			if (P_0.m_nChangeFlags != EPersonaChange.k_EPersonaChangeStatus)
			{
				return;
			}
			while (true)
			{
				int num = -144283446;
				while (true)
				{
					uint num2;
					Action<ulong> action;
					switch ((num2 = (uint)(num ^ -1295752164)) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 1u:
						action = this.wibyoXdUyooAMfoIttKfmeojtQqM;
						if (action != null)
						{
							goto IL_0036;
						}
						return;
					case 2u:
						return;
					}
					break;
					IL_0036:
					action.SafelyInvoke(P_0.m_ulSteamID);
					num = (int)(num2 * 525736145) ^ -1247699206;
				}
			}
		}

		private void xgawRajbGhAopBOfdjwwBMuMdraNA(AvatarImageLoaded_t P_0)
		{
			if (P_0.m_iImage <= 0)
			{
				goto IL_0009;
			}
			goto IL_003f;
			IL_0009:
			int num = 844732279;
			goto IL_000e;
			IL_000e:
			uint num2;
			switch ((num2 = (uint)(num ^ 0x51244675)) % 4)
			{
			case 3u:
				break;
			default:
				return;
			case 2u:
				return;
			case 1u:
				goto IL_003f;
			case 0u:
				return;
			}
			goto IL_0009;
			IL_003f:
			Action<CSteamID, int> action = this.brMXJPGlNBPaVzlNHNYLaCNsBIOjA;
			if (action == null)
			{
				return;
			}
			action.SafelyInvoke(P_0.m_steamID, P_0.m_iImage);
			num = 2018851193;
			goto IL_000e;
		}

		private void gmNqSzocKjZRkReAHVvIOCOtfnSBA(DlcInstalled_t P_0)
		{
			this.KoYBuZsLgJIySGTwfWiCHtQpPxJi?.SafelyInvoke(P_0.m_nAppID);
		}

		private void SYDwtiffRTvzRVyMSOEBWIyVcxAA(UserStatsReceived_t P_0)
		{
			this.HrIKZKRBQoVhfFMPLlIaHyKVuyZv?.SafelyInvoke(P_0.m_eResult);
		}

		private void QaUGAHcDXPWhviwFFfSQqcjjOROi(UserAchievementStored_t P_0)
		{
			this.MKrFmoBXlKAuTQshDJNqgkNBHxBA?.SafelyInvoke(P_0);
		}

		private void aFYpwgkoTsGiXTDsuARqbwnXeBQb(UserStatsStored_t P_0)
		{
			this.DPRKwlHJEMbmBgfJufKVpcmQhfFw?.SafelyInvoke(P_0);
		}

		[MonoPInvokeCallback(typeof(SteamAPIWarningMessageHook_t))]
		private static void AHZSFkhlLEhWnuOPxrtNDBlHGYaG(int P_0, StringBuilder P_1)
		{
			if (P_0 == 0)
			{
				return;
			}
			while (true)
			{
				int num = -930011790;
				while (true)
				{
					uint num2;
					int num3;
					switch ((num2 = (uint)(num ^ -1038499132)) % 4)
					{
					case 0u:
						break;
					default:
						return;
					case 2u:
					{
						int num4;
						if (P_0 != 1)
						{
							num3 = -2036528657;
							num4 = num3;
						}
						else
						{
							num3 = -1806151803;
							num4 = num3;
						}
						goto IL_003b;
					}
					case 3u:
						throw new ArgumentException("SteamApiMsgHook - received a message with an invalid severity level.");
					case 1u:
						return;
					}
					break;
					IL_003b:
					num = num3 ^ ((int)num2 * -2005607982);
				}
			}
		}
	}
}
