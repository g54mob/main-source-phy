using System;
using System.Collections.Generic;
using BitCode.Users;
using JetBrains.Annotations;
using Steamworks;
using UnityEngine;
using qKHVznygoYkvheyhtfJODBaLRROp;

namespace BitCode.Platform.Steamworks
{
	public class SteamAchievementManager : IAchievementManager, IPlatformService, IUpdateableService, IDisposable
	{
		private struct ZnueFaBZWTjUoLSpuQCMFNNiCNTrA
		{
			public bool VAXVhKGLKsjVpUuVMZvtoWIFimTQ;

			public ISteamAchievement QiPYsOJGvYbWsVEtlaJgsnUKCpwT;

			public SteamLocalAccount KxYDxIGtXJpzZJYWdYxNudQkHswBA;

			public float zoINveUFTaJbyWeOfZBzFehRrYbm;

			public AchievementEventHandler bwtNlcoxnnQpGutbhbpqPmEkdhEY;
		}

		private readonly Queue<ZnueFaBZWTjUoLSpuQCMFNNiCNTrA> YRPIxXWJDEsTcTLpegoqjzFmzvzrA = new Queue<ZnueFaBZWTjUoLSpuQCMFNNiCNTrA>();

		private readonly SteamService JvEZsMEbjtsmodyZFfToTIvzzHoC;

		private readonly IServiceUpdater UiqWRGkuDLeBUJrcKfNWbgaWARxT;

		private readonly float aAHhMVXtJWgCeKRpqmxpzoxHILFc;

		private bool rRzuUJJnpLtRdsgQnjZxcYRhFNmI;

		private bool bBzFCPvegdcjojlzNVfWaJyAejlV;

		private bool InSgerYqMxiOsmSklCsiDNMkthYR;

		private bool ltJelMpGkoiOOdSzWBkeDDwfRlAqA;

		private bool JvBIxaXnPypdXElcHsKPmhCzAJVo;

		private float cgUVYklihmYntrBSygJIMqJeUUYI;

		public bool Initialized => rRzuUJJnpLtRdsgQnjZxcYRhFNmI;

		event Action<IPlatformService, Exception> IPlatformService.InternalErrorOccurred
		{
			add
			{
			}
			remove
			{
			}
		}

		public SteamAchievementManager([NotNull] SteamService steamService, [NotNull] IServiceUpdater serviceUpdater, float storeStatsInterval = 5f)
		{
			while (true)
			{
				int num = -1933920512;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -729063283)) % 7)
					{
					case 0u:
						break;
					default:
						return;
					case 4u:
						JvEZsMEbjtsmodyZFfToTIvzzHoC = steamService ?? throw new ArgumentNullException("steamService");
						UiqWRGkuDLeBUJrcKfNWbgaWARxT = serviceUpdater ?? throw new ArgumentNullException("serviceUpdater");
						if (!(storeStatsInterval > 0f))
						{
							throw new ArgumentOutOfRangeException("storeStatsInterval");
						}
						aAHhMVXtJWgCeKRpqmxpzoxHILFc = storeStatsInterval;
						steamService.HrIKZKRBQoVhfFMPLlIaHyKVuyZv += HrIKZKRBQoVhfFMPLlIaHyKVuyZv;
						num = ((int)num2 * -1591350395) ^ -1901856068;
						continue;
					case 1u:
					{
						int num3;
						int num4;
						if (!SteamUserStats.RequestCurrentStats())
						{
							num3 = -1794781679;
							num4 = num3;
						}
						else
						{
							num3 = -989144802;
							num4 = num3;
						}
						num = num3 ^ ((int)num2 * -2002180329);
						continue;
					}
					case 6u:
						steamService.DPRKwlHJEMbmBgfJufKVpcmQhfFw += DPRKwlHJEMbmBgfJufKVpcmQhfFw;
						serviceUpdater.RegisterService(this);
						num = ((int)num2 * -465060639) ^ 0x3781870B;
						continue;
					case 2u:
						throw new SteamApiException("Could not request current user stats. User is possibly not signed in!");
					case 5u:
						cgUVYklihmYntrBSygJIMqJeUUYI = 0f;
						num = ((int)num2 * -910695734) ^ 0x251E7AF7;
						continue;
					case 3u:
						return;
					}
					break;
				}
			}
		}

		public void UpdateAchievementAsync(IAchievement achievement, ILocalAccount account, float progress, AchievementEventHandler eventHandler = null)
		{
			EfgdiGtxKrtefuGKEgjWCSAEbeND();
			uint num17 = default(uint);
			ISteamAchievement steamAchievement = default(ISteamAchievement);
			ZnueFaBZWTjUoLSpuQCMFNNiCNTrA znueFaBZWTjUoLSpuQCMFNNiCNTrA = default(ZnueFaBZWTjUoLSpuQCMFNNiCNTrA);
			SteamLocalAccount steamLocalAccount = default(SteamLocalAccount);
			while (true)
			{
				int num = 933996016;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x4EAAE4EA)) % 33)
					{
					case 3u:
						break;
					case 10u:
						num17 = (uint)(progress * (float)steamAchievement.MaxProgressValue);
						num = 135838905;
						continue;
					case 15u:
						throw new ArgumentNullException("account");
					case 19u:
					{
						int num18;
						int num19;
						if (steamAchievement != null)
						{
							num18 = 62057717;
							num19 = num18;
						}
						else
						{
							num18 = 385695448;
							num19 = num18;
						}
						num = num18 ^ ((int)num2 * -787536395);
						continue;
					}
					case 28u:
					{
						int num9;
						if (!steamAchievement.ShowProgressOverlay)
						{
							num = 1228203342;
							num9 = num;
						}
						else
						{
							num = 233361584;
							num9 = num;
						}
						continue;
					}
					case 4u:
						throw new ArgumentOutOfRangeException("progress", $"Progress for achievement {steamAchievement.AchievementId} must be between 0 and 1 inclusive.");
					case 2u:
					{
						int num24;
						if (!(progress >= 0f - Mathf.Epsilon))
						{
							num = 760852799;
							num24 = num;
						}
						else
						{
							num = 1244263395;
							num24 = num;
						}
						continue;
					}
					case 24u:
						znueFaBZWTjUoLSpuQCMFNNiCNTrA = default(ZnueFaBZWTjUoLSpuQCMFNNiCNTrA);
						num = (int)(num2 * 1173317987) ^ -1324344074;
						continue;
					case 12u:
					{
						int num6;
						int num7;
						if (SteamUserStats.SetAchievement(steamAchievement.AchievementId))
						{
							num6 = -988552657;
							num7 = num6;
						}
						else
						{
							num6 = -436722307;
							num7 = num6;
						}
						num = num6 ^ (int)(num2 * 106899707);
						continue;
					}
					case 17u:
						throw new ArgumentNullException("achievement");
					case 25u:
						throw new ArgumentException(seVdBpEtNsxiBntiSfTwSSbsnzbv.rrhjgAJEoIHWUdoPwGIKgWslUdGJ, "achievement");
					case 14u:
						return;
					case 20u:
					{
						int num20;
						int num21;
						if (num17 % steamAchievement.DisplayOverlayInterval == 0L)
						{
							num20 = 1558913414;
							num21 = num20;
						}
						else
						{
							num20 = 503423116;
							num21 = num20;
						}
						num = num20 ^ ((int)num2 * -870473723);
						continue;
					}
					case 8u:
						throw new InvalidOperationException("An initialization error has occurred. See SteamAchievementManager documentation for more information.");
					case 11u:
					{
						int num12;
						int num13;
						if (progress == 1f)
						{
							num12 = -1316133876;
							num13 = num12;
						}
						else
						{
							num12 = -2141398516;
							num13 = num12;
						}
						num = num12 ^ (int)(num2 * 1571381749);
						continue;
					}
					case 13u:
						eventHandler.SafelyInvoke(steamAchievement, 1f, hasBeenAwarded: true, null);
						num = 1228203342;
						continue;
					case 7u:
					{
						int num5;
						if (!rRzuUJJnpLtRdsgQnjZxcYRhFNmI)
						{
							num = 169477512;
							num5 = num;
						}
						else
						{
							num = 1689331035;
							num5 = num;
						}
						continue;
					}
					case 29u:
						throw new ArgumentException(seVdBpEtNsxiBntiSfTwSSbsnzbv.rWFMxAOejtQAafsFTTHAQjRBhlMV, "account");
					case 1u:
						steamAchievement = achievement as ISteamAchievement;
						num = 1703639447;
						continue;
					case 22u:
					{
						int num22;
						int num23;
						if (SteamUserStats.IndicateAchievementProgress(steamAchievement.AchievementId, num17, steamAchievement.MaxProgressValue))
						{
							num22 = 1505674014;
							num23 = num22;
						}
						else
						{
							num22 = 409405455;
							num23 = num22;
						}
						num = num22 ^ ((int)num2 * -1482265320);
						continue;
					}
					case 23u:
					{
						int num15;
						int num16;
						if (progress <= 1f + Mathf.Epsilon)
						{
							num15 = 1966966810;
							num16 = num15;
						}
						else
						{
							num15 = 473057428;
							num16 = num15;
						}
						num = num15 ^ ((int)num2 * -774626285);
						continue;
					}
					case 16u:
					{
						int num14;
						if (account == null)
						{
							num = 392520464;
							num14 = num;
						}
						else
						{
							num = 756254701;
							num14 = num;
						}
						continue;
					}
					case 27u:
					{
						int num10;
						int num11;
						if (steamLocalAccount != null)
						{
							num10 = -537430959;
							num11 = num10;
						}
						else
						{
							num10 = -1323629193;
							num11 = num10;
						}
						num = num10 ^ (int)(num2 * 1765120403);
						continue;
					}
					case 0u:
						steamLocalAccount = account as SteamLocalAccount;
						num = 1563934502;
						continue;
					case 30u:
						znueFaBZWTjUoLSpuQCMFNNiCNTrA.QiPYsOJGvYbWsVEtlaJgsnUKCpwT = steamAchievement;
						znueFaBZWTjUoLSpuQCMFNNiCNTrA.KxYDxIGtXJpzZJYWdYxNudQkHswBA = steamLocalAccount;
						num = ((int)num2 * -1859128313) ^ 0x4C58E85A;
						continue;
					case 32u:
					{
						znueFaBZWTjUoLSpuQCMFNNiCNTrA.bwtNlcoxnnQpGutbhbpqPmEkdhEY = eventHandler;
						ZnueFaBZWTjUoLSpuQCMFNNiCNTrA item = znueFaBZWTjUoLSpuQCMFNNiCNTrA;
						YRPIxXWJDEsTcTLpegoqjzFmzvzrA.Enqueue(item);
						num = (int)((num2 * 692504304) ^ 0x76568E5D);
						continue;
					}
					case 9u:
						znueFaBZWTjUoLSpuQCMFNNiCNTrA.zoINveUFTaJbyWeOfZBzFehRrYbm = progress;
						num = ((int)num2 * -726164290) ^ -1919104715;
						continue;
					case 6u:
					{
						int num8;
						if (achievement != null)
						{
							num = 764335783;
							num8 = num;
						}
						else
						{
							num = 1462671481;
							num8 = num;
						}
						continue;
					}
					case 5u:
						throw new SteamApiException($"Failed to show achievement progress for {steamAchievement.AchievementId}.");
					case 21u:
					{
						int num3;
						int num4;
						if (InSgerYqMxiOsmSklCsiDNMkthYR)
						{
							num3 = -468310727;
							num4 = num3;
						}
						else
						{
							num3 = -1327256937;
							num4 = num3;
						}
						num = num3 ^ (int)(num2 * 563405572);
						continue;
					}
					case 26u:
						znueFaBZWTjUoLSpuQCMFNNiCNTrA.VAXVhKGLKsjVpUuVMZvtoWIFimTQ = true;
						num = ((int)num2 * -981052270) ^ -733140525;
						continue;
					case 31u:
						throw new SteamApiException($"Failed to award achievement {steamAchievement.AchievementId}.");
					default:
						ltJelMpGkoiOOdSzWBkeDDwfRlAqA = true;
						return;
					}
					break;
				}
			}
		}

		public void GetAchievementAsync(IAchievement achievement, ILocalAccount account, AchievementEventHandler eventHandler)
		{
			EfgdiGtxKrtefuGKEgjWCSAEbeND();
			if (InSgerYqMxiOsmSklCsiDNMkthYR)
			{
				goto IL_0011;
			}
			goto IL_0173;
			IL_0011:
			int num = 798950817;
			goto IL_0016;
			IL_0016:
			ZnueFaBZWTjUoLSpuQCMFNNiCNTrA znueFaBZWTjUoLSpuQCMFNNiCNTrA = default(ZnueFaBZWTjUoLSpuQCMFNNiCNTrA);
			ISteamAchievement steamAchievement = default(ISteamAchievement);
			SteamLocalAccount steamLocalAccount = default(SteamLocalAccount);
			bool pbAchieved = default(bool);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x5A1851BF)) % 20)
				{
				case 19u:
					break;
				default:
					return;
				case 9u:
					znueFaBZWTjUoLSpuQCMFNNiCNTrA = new ZnueFaBZWTjUoLSpuQCMFNNiCNTrA
					{
						QiPYsOJGvYbWsVEtlaJgsnUKCpwT = steamAchievement
					};
					num = ((int)num2 * -1450704468) ^ -1689586895;
					continue;
				case 4u:
					goto IL_009f;
				case 18u:
					znueFaBZWTjUoLSpuQCMFNNiCNTrA.KxYDxIGtXJpzZJYWdYxNudQkHswBA = steamLocalAccount;
					num = (int)((num2 * 1747196311) ^ 0x798393D9);
					continue;
				case 15u:
					goto IL_00dd;
				case 13u:
					throw new ArgumentException(seVdBpEtNsxiBntiSfTwSSbsnzbv.rWFMxAOejtQAafsFTTHAQjRBhlMV, "account");
				case 17u:
					goto IL_0117;
				case 1u:
				{
					ZnueFaBZWTjUoLSpuQCMFNNiCNTrA item = znueFaBZWTjUoLSpuQCMFNNiCNTrA;
					YRPIxXWJDEsTcTLpegoqjzFmzvzrA.Enqueue(item);
					num = ((int)num2 * -1340728025) ^ 0x6E6353F6;
					continue;
				}
				case 2u:
					throw new ArgumentNullException("account");
				case 5u:
					goto IL_0173;
				case 3u:
					steamLocalAccount = account as SteamLocalAccount;
					num = 401829267;
					continue;
				case 14u:
					return;
				case 0u:
				{
					int num5;
					int num6;
					if (steamAchievement != null)
					{
						num5 = -902907304;
						num6 = num5;
					}
					else
					{
						num5 = -250972447;
						num6 = num5;
					}
					num = num5 ^ ((int)num2 * -806941061);
					continue;
				}
				case 6u:
					throw new InvalidOperationException("An initialization error has occurred. See SteamAchievementManager documentation for more information.");
				case 12u:
					throw new SteamApiException($"Achievement {steamAchievement.AchievementId} does not exist.");
				case 8u:
					znueFaBZWTjUoLSpuQCMFNNiCNTrA.zoINveUFTaJbyWeOfZBzFehRrYbm = 0f;
					znueFaBZWTjUoLSpuQCMFNNiCNTrA.bwtNlcoxnnQpGutbhbpqPmEkdhEY = eventHandler;
					num = ((int)num2 * -124734910) ^ 0x58045F5A;
					continue;
				case 10u:
					throw new ArgumentException(seVdBpEtNsxiBntiSfTwSSbsnzbv.rrhjgAJEoIHWUdoPwGIKgWslUdGJ, "achievement");
				case 16u:
				{
					int num3;
					int num4;
					if (steamLocalAccount == null)
					{
						num3 = -769561158;
						num4 = num3;
					}
					else
					{
						num3 = -1527029366;
						num4 = num3;
					}
					num = num3 ^ ((int)num2 * -1214793079);
					continue;
				}
				case 7u:
					eventHandler.SafelyInvoke(achievement, pbAchieved ? 1f : 0f, pbAchieved, null);
					num = 543383460;
					continue;
				case 11u:
					return;
				}
				break;
				IL_0117:
				int num7;
				if (rRzuUJJnpLtRdsgQnjZxcYRhFNmI)
				{
					num = 1147966183;
					num7 = num;
				}
				else
				{
					num = 2093134486;
					num7 = num;
				}
				continue;
				IL_00dd:
				int num8;
				if (account != null)
				{
					num = 881881604;
					num8 = num;
				}
				else
				{
					num = 2012496513;
					num8 = num;
				}
				continue;
				IL_009f:
				int num9;
				if (!SteamUserStats.GetAchievement(steamAchievement.AchievementId, out pbAchieved))
				{
					num = 876699763;
					num9 = num;
				}
				else
				{
					num = 1550015428;
					num9 = num;
				}
			}
			goto IL_0011;
			IL_0173:
			steamAchievement = achievement as ISteamAchievement;
			num = 146371823;
			goto IL_0016;
		}

		public void Update()
		{
			if (!rRzuUJJnpLtRdsgQnjZxcYRhFNmI)
			{
				return;
			}
			while (true)
			{
				int num = 622999533;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x185638AF)) % 4)
					{
					case 3u:
						break;
					case 2u:
						if (InSgerYqMxiOsmSklCsiDNMkthYR)
						{
							goto IL_0039;
						}
						goto IL_0107;
					case 1u:
						return;
					default:
						{
							ZnueFaBZWTjUoLSpuQCMFNNiCNTrA znueFaBZWTjUoLSpuQCMFNNiCNTrA = YRPIxXWJDEsTcTLpegoqjzFmzvzrA.Dequeue();
							try
							{
								if (znueFaBZWTjUoLSpuQCMFNNiCNTrA.VAXVhKGLKsjVpUuVMZvtoWIFimTQ)
								{
									goto IL_0064;
								}
								goto IL_00ca;
								IL_0064:
								int num10 = 765397148;
								goto IL_0069;
								IL_0069:
								while (true)
								{
									switch ((num2 = (uint)(num10 ^ 0x185638AF)) % 5)
									{
									case 2u:
										break;
									default:
										goto end_IL_005c;
									case 3u:
										UpdateAchievementAsync(znueFaBZWTjUoLSpuQCMFNNiCNTrA.QiPYsOJGvYbWsVEtlaJgsnUKCpwT, znueFaBZWTjUoLSpuQCMFNNiCNTrA.KxYDxIGtXJpzZJYWdYxNudQkHswBA, znueFaBZWTjUoLSpuQCMFNNiCNTrA.zoINveUFTaJbyWeOfZBzFehRrYbm, znueFaBZWTjUoLSpuQCMFNNiCNTrA.bwtNlcoxnnQpGutbhbpqPmEkdhEY);
										num10 = ((int)num2 * -1469280507) ^ 0x7993079C;
										continue;
									case 4u:
										num10 = (int)(num2 * 1888721418) ^ -1356328850;
										continue;
									case 1u:
										goto IL_00ca;
									case 0u:
										goto end_IL_005c;
									}
									break;
								}
								goto IL_0064;
								IL_00ca:
								GetAchievementAsync(znueFaBZWTjUoLSpuQCMFNNiCNTrA.QiPYsOJGvYbWsVEtlaJgsnUKCpwT, znueFaBZWTjUoLSpuQCMFNNiCNTrA.KxYDxIGtXJpzZJYWdYxNudQkHswBA, znueFaBZWTjUoLSpuQCMFNNiCNTrA.bwtNlcoxnnQpGutbhbpqPmEkdhEY);
								num10 = 1277944726;
								goto IL_0069;
								end_IL_005c:;
							}
							catch (Exception exception)
							{
								znueFaBZWTjUoLSpuQCMFNNiCNTrA.bwtNlcoxnnQpGutbhbpqPmEkdhEY.SafelyInvoke(znueFaBZWTjUoLSpuQCMFNNiCNTrA.QiPYsOJGvYbWsVEtlaJgsnUKCpwT, znueFaBZWTjUoLSpuQCMFNNiCNTrA.zoINveUFTaJbyWeOfZBzFehRrYbm, hasBeenAwarded: false, exception);
							}
							goto IL_0107;
						}
						IL_0107:
						if (YRPIxXWJDEsTcTLpegoqjzFmzvzrA.Count <= 0)
						{
							while (true)
							{
								int num3 = 612950958;
								while (true)
								{
									switch ((num2 = (uint)(num3 ^ 0x185638AF)) % 7)
									{
									case 0u:
										break;
									default:
										return;
									case 2u:
									{
										cgUVYklihmYntrBSygJIMqJeUUYI -= Time.unscaledDeltaTime;
										int num6;
										int num7;
										if (JvBIxaXnPypdXElcHsKPmhCzAJVo)
										{
											num6 = -944934663;
											num7 = num6;
										}
										else
										{
											num6 = -221537687;
											num7 = num6;
										}
										num3 = num6 ^ (int)(num2 * 1839075893);
										continue;
									}
									case 3u:
										SteamUserStats.StoreStats();
										num3 = (int)(num2 * 440657240) ^ -1900477500;
										continue;
									case 4u:
										JvBIxaXnPypdXElcHsKPmhCzAJVo = true;
										ltJelMpGkoiOOdSzWBkeDDwfRlAqA = false;
										num3 = ((int)num2 * -606907531) ^ 0x9DB1662;
										continue;
									case 6u:
									{
										int num8;
										int num9;
										if (cgUVYklihmYntrBSygJIMqJeUUYI > 0f)
										{
											num8 = -949317280;
											num9 = num8;
										}
										else
										{
											num8 = -634513131;
											num9 = num8;
										}
										num3 = num8 ^ (int)(num2 * 683202138);
										continue;
									}
									case 5u:
									{
										int num4;
										int num5;
										if (ltJelMpGkoiOOdSzWBkeDDwfRlAqA)
										{
											num4 = 175078653;
											num5 = num4;
										}
										else
										{
											num4 = 406466288;
											num5 = num4;
										}
										num3 = num4 ^ (int)(num2 * 334620372);
										continue;
									}
									case 1u:
										return;
									}
									break;
								}
							}
						}
						goto default;
					}
					break;
					IL_0039:
					num = (int)(num2 * 76254299) ^ -988409956;
				}
			}
		}

		public void ResetAchievement(ISteamAchievement achievement)
		{
			if (!rRzuUJJnpLtRdsgQnjZxcYRhFNmI)
			{
				goto IL_0008;
			}
			goto IL_007c;
			IL_0008:
			int num = -744857667;
			goto IL_000d;
			IL_000d:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -797511606)) % 7)
				{
				case 6u:
					break;
				default:
					return;
				case 1u:
					throw new InvalidOperationException("Requested stats have not yet been returned.");
				case 4u:
					throw new SteamApiException($"Failed to clear progress for achievement {achievement.AchievementId}.");
				case 0u:
					goto IL_007c;
				case 5u:
					throw new InvalidOperationException("An initialization error has occurred. See SteamAchievementManager documentation for more information.");
				case 3u:
					goto IL_00b5;
				case 2u:
					return;
				}
				break;
				IL_00b5:
				int num3;
				if (!SteamUserStats.ClearAchievement(achievement.AchievementId))
				{
					num = -1930290238;
					num3 = num;
				}
				else
				{
					num = -207349473;
					num3 = num;
				}
			}
			goto IL_0008;
			IL_007c:
			int num4;
			if (InSgerYqMxiOsmSklCsiDNMkthYR)
			{
				num = -772413822;
				num4 = num;
			}
			else
			{
				num = -975377644;
				num4 = num;
			}
			goto IL_000d;
		}

		public void ResetAllStatsAndAchievements()
		{
			if (!rRzuUJJnpLtRdsgQnjZxcYRhFNmI)
			{
				goto IL_0008;
			}
			goto IL_004c;
			IL_0008:
			int num = 1195593991;
			goto IL_000d;
			IL_000d:
			uint num2;
			switch ((num2 = (uint)(num ^ 0x8ABFEB7)) % 5)
			{
			case 0u:
				break;
			case 4u:
				throw new InvalidOperationException("An initialization error has occurred. See SteamAchievementManager documentation for more information.");
			case 3u:
				goto IL_004c;
			case 2u:
				throw new InvalidOperationException("Requested stats have not yet been returned.");
			default:
				SteamUserStats.ResetAllStats(bAchievementsToo: true);
				return;
			}
			goto IL_0008;
			IL_004c:
			int num3;
			if (InSgerYqMxiOsmSklCsiDNMkthYR)
			{
				num = 1697490573;
				num3 = num;
			}
			else
			{
				num = 1912505695;
				num3 = num;
			}
			goto IL_000d;
		}

		public void EnqueueStoreStats()
		{
			ltJelMpGkoiOOdSzWBkeDDwfRlAqA = true;
		}

		private void HrIKZKRBQoVhfFMPLlIaHyKVuyZv(EResult P_0)
		{
			if (P_0 != EResult.k_EResultOK)
			{
				ZnueFaBZWTjUoLSpuQCMFNNiCNTrA znueFaBZWTjUoLSpuQCMFNNiCNTrA = default(ZnueFaBZWTjUoLSpuQCMFNNiCNTrA);
				InvalidOperationException exception = default(InvalidOperationException);
				while (true)
				{
					int num = -810274862;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -25266394)) % 9)
						{
						case 2u:
							break;
						case 0u:
							num = (int)((num2 * 647841977) ^ 0x14ADB236);
							continue;
						case 4u:
							znueFaBZWTjUoLSpuQCMFNNiCNTrA = YRPIxXWJDEsTcTLpegoqjzFmzvzrA.Dequeue();
							num = -1389649794;
							continue;
						case 1u:
							return;
						case 3u:
							goto IL_0077;
						case 8u:
							exception = new InvalidOperationException("Requesting stats failed.");
							num = (int)((num2 * 1329687016) ^ 0x124452BE);
							continue;
						case 6u:
							znueFaBZWTjUoLSpuQCMFNNiCNTrA.bwtNlcoxnnQpGutbhbpqPmEkdhEY.SafelyInvoke(znueFaBZWTjUoLSpuQCMFNNiCNTrA.QiPYsOJGvYbWsVEtlaJgsnUKCpwT, znueFaBZWTjUoLSpuQCMFNNiCNTrA.zoINveUFTaJbyWeOfZBzFehRrYbm, hasBeenAwarded: false, exception);
							num = ((int)num2 * -48916717) ^ -1450209634;
							continue;
						case 7u:
							InSgerYqMxiOsmSklCsiDNMkthYR = true;
							num = ((int)num2 * -1856032489) ^ -2141966135;
							continue;
						default:
							goto end_IL_0007;
						}
						break;
						IL_0077:
						int num3;
						if (YRPIxXWJDEsTcTLpegoqjzFmzvzrA.Count > 0)
						{
							num = -961519097;
							num3 = num;
						}
						else
						{
							num = -1856671636;
							num3 = num;
						}
					}
					continue;
					end_IL_0007:
					break;
				}
			}
			rRzuUJJnpLtRdsgQnjZxcYRhFNmI = true;
		}

		private void DPRKwlHJEMbmBgfJufKVpcmQhfFw(UserStatsStored_t P_0)
		{
			cgUVYklihmYntrBSygJIMqJeUUYI = aAHhMVXtJWgCeKRpqmxpzoxHILFc;
			while (true)
			{
				int num = 565600904;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x7A69C7D4)) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 1u:
						goto IL_002e;
					case 2u:
						return;
					}
					break;
					IL_002e:
					JvBIxaXnPypdXElcHsKPmhCzAJVo = false;
					num = (int)((num2 * 1143195423) ^ 0x14BF13D);
				}
			}
		}

		public void Dispose()
		{
			if (bBzFCPvegdcjojlzNVfWaJyAejlV)
			{
				goto IL_0008;
			}
			goto IL_006c;
			IL_0008:
			int num = -1682223863;
			goto IL_000d;
			IL_000d:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -220407678)) % 6)
				{
				case 5u:
					break;
				case 1u:
					return;
				case 3u:
					JvEZsMEbjtsmodyZFfToTIvzzHoC.HrIKZKRBQoVhfFMPLlIaHyKVuyZv -= HrIKZKRBQoVhfFMPLlIaHyKVuyZv;
					num = (int)(num2 * 575406457) ^ -308120123;
					continue;
				case 2u:
					goto IL_006c;
				case 0u:
					JvEZsMEbjtsmodyZFfToTIvzzHoC.DPRKwlHJEMbmBgfJufKVpcmQhfFw -= DPRKwlHJEMbmBgfJufKVpcmQhfFw;
					num = ((int)num2 * -1682679843) ^ 0x514007FC;
					continue;
				default:
					bBzFCPvegdcjojlzNVfWaJyAejlV = true;
					return;
				}
				break;
			}
			goto IL_0008;
			IL_006c:
			YRPIxXWJDEsTcTLpegoqjzFmzvzrA.Clear();
			UiqWRGkuDLeBUJrcKfNWbgaWARxT.DeregisterService(this);
			num = -2134685465;
			goto IL_000d;
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
				switch ((num = 1538259535u) % 3)
				{
				case 0u:
					break;
				default:
					return;
				case 1u:
					throw new ObjectDisposedException(GetType().FullName);
				case 2u:
					return;
				}
			}
		}
	}
}
