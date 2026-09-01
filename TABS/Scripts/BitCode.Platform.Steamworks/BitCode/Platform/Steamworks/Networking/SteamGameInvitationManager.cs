using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using BitCode.Networking;
using BitCode.Threading;
using BitCode.Users;
using JetBrains.Annotations;
using Steamworks;
using UBbHXiGzYeAJkDzmQNPeJCmcysxTA;

namespace BitCode.Platform.Steamworks.Networking
{
	public class SteamGameInvitationManager : IPlatformService, IDisposable, IGameInvitationManager
	{
		private sealed class uXIuiIoLsHZLzmzGOgKAMpCeydnP
		{
			public SteamGameInvitationManager fzmOHwCVFAORQTdOFaeSSiVyGDdjA;

			public ILocalAccount ctVEnxnyPJNLbwwfBSndAcMzIxuP;

			public IRemoteAccount[] BWvkeUdUAXZdQdrRnUuRRYBfsZZC;

			public IGameInvitation fUobkqhVXyuCUPgIlqCOHsTIdZJGc;

			public Action<bool, Exception> zzHgJwzXHJcKWEVhIkkLZzZtwrcM;

			internal bool CFGIOnXZHttteugfBnelxyhmpLiU()
			{
				return fzmOHwCVFAORQTdOFaeSSiVyGDdjA.VxEUHokJncBSzQhIXzCwAtRmiGwF(ctVEnxnyPJNLbwwfBSndAcMzIxuP, BWvkeUdUAXZdQdrRnUuRRYBfsZZC, fUobkqhVXyuCUPgIlqCOHsTIdZJGc);
			}

			internal void OuBvbhlwAazqMWxSdXOvcAkfQwxg(bool P_0)
			{
				zzHgJwzXHJcKWEVhIkkLZzZtwrcM?.Invoke(arg1: true, null);
			}

			internal void TQhPjZCIhoahoLNOCiPQbcOOdGNM(Exception P_0)
			{
				zzHgJwzXHJcKWEVhIkkLZzZtwrcM?.Invoke(arg1: false, P_0);
			}
		}

		private sealed class PKAGIlwuPAhdWmxJZiCgkoVqnmhhA
		{
			public SteamGameInvitationManager fzmOHwCVFAORQTdOFaeSSiVyGDdjA;

			public ILocalAccount ctVEnxnyPJNLbwwfBSndAcMzIxuP;

			public IRemoteAccount[] BWvkeUdUAXZdQdrRnUuRRYBfsZZC;

			public IGameInvitation fUobkqhVXyuCUPgIlqCOHsTIdZJGc;

			internal bool CFGIOnXZHttteugfBnelxyhmpLiU()
			{
				return fzmOHwCVFAORQTdOFaeSSiVyGDdjA.VxEUHokJncBSzQhIXzCwAtRmiGwF(ctVEnxnyPJNLbwwfBSndAcMzIxuP, BWvkeUdUAXZdQdrRnUuRRYBfsZZC, fUobkqhVXyuCUPgIlqCOHsTIdZJGc);
			}
		}

		private const string cBRimfLElaadyitwdeIVBZHaFyCg = "CmdArgs.txt";

		private Callback<GameRichPresenceJoinRequested_t> FgbzqITuvQbQQUdgcAulRYogdIih;

		private readonly SteamService JvEZsMEbjtsmodyZFfToTIvzzHoC;

		private readonly SteamLocalAccountManager krdklyPInffabTJcsVYAeGdWXOoI;

		private bool bBzFCPvegdcjojlzNVfWaJyAejlV;

		private bool uagUCpiskYUgDzypBzdEKlIvHILE;

		[CompilerGenerated]
		private Action<IGameInvitation, ILocalAccount> jMvrYzdZlrUwGJCjRWPYpxtuJBDk;

		public event Action<IGameInvitation, ILocalAccount> InvitationReceived
		{
			[CompilerGenerated]
			add
			{
				Action<IGameInvitation, ILocalAccount> action = jMvrYzdZlrUwGJCjRWPYpxtuJBDk;
				Action<IGameInvitation, ILocalAccount> action2 = default(Action<IGameInvitation, ILocalAccount>);
				while (true)
				{
					int num = 11306696;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x3C78DF02)) % 4)
						{
						case 3u:
							break;
						default:
							return;
						case 2u:
							action2 = action;
							num = 1207968211;
							continue;
						case 1u:
						{
							Action<IGameInvitation, ILocalAccount> value2 = (Action<IGameInvitation, ILocalAccount>)Delegate.Combine(action2, value);
							action = Interlocked.CompareExchange(ref jMvrYzdZlrUwGJCjRWPYpxtuJBDk, value2, action2);
							int num3;
							int num4;
							if ((object)action == action2)
							{
								num3 = -750510182;
								num4 = num3;
							}
							else
							{
								num3 = -981485952;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 1585679816);
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
				Action<IGameInvitation, ILocalAccount> action = jMvrYzdZlrUwGJCjRWPYpxtuJBDk;
				Action<IGameInvitation, ILocalAccount> action2 = default(Action<IGameInvitation, ILocalAccount>);
				while (true)
				{
					int num = 1582399614;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x5F0B325)) % 5)
						{
						case 2u:
							break;
						default:
							return;
						case 4u:
							action2 = action;
							num = 324532049;
							continue;
						case 0u:
						{
							int num3;
							int num4;
							if ((object)action != action2)
							{
								num3 = 389958582;
								num4 = num3;
							}
							else
							{
								num3 = 1978664273;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 873237076);
							continue;
						}
						case 3u:
						{
							Action<IGameInvitation, ILocalAccount> value2 = (Action<IGameInvitation, ILocalAccount>)Delegate.Remove(action2, value);
							action = Interlocked.CompareExchange(ref jMvrYzdZlrUwGJCjRWPYpxtuJBDk, value2, action2);
							num = (int)(num2 * 2004274775) ^ -1533205277;
							continue;
						}
						case 1u:
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

		public SteamGameInvitationManager([NotNull] SteamService steamService, [NotNull] SteamLocalAccountManager localAccountManager)
		{
			while (true)
			{
				int num = -1509925610;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1436244803)) % 5)
					{
					case 0u:
						break;
					default:
						return;
					case 2u:
						JvEZsMEbjtsmodyZFfToTIvzzHoC = steamService ?? throw new ArgumentNullException("steamService");
						num = ((int)num2 * -800588989) ^ -1125652194;
						continue;
					case 1u:
						krdklyPInffabTJcsVYAeGdWXOoI = localAccountManager ?? throw new ArgumentNullException("localAccountManager");
						num = ((int)num2 * -822231248) ^ -190729475;
						continue;
					case 4u:
						FgbzqITuvQbQQUdgcAulRYogdIih = Callback<GameRichPresenceJoinRequested_t>.Create(zdQpVMTVXpuQDlHdHGetzSVAIoAv);
						num = (int)(num2 * 825605363) ^ -1901743042;
						continue;
					case 3u:
						return;
					}
					break;
				}
			}
		}

		public void SendGameInviteAsync(ILocalAccount user, int maxInvitees, IGameInvitation invitation, Action<bool, Exception> sentCallback)
		{
			throw new NotSupportedException();
		}

		public Task<bool> SendGameInviteAsync(ILocalAccount user, int maxInvitees, IGameInvitation invitation)
		{
			throw new NotSupportedException();
		}

		public void SendGameInviteAsync(ILocalAccount user, IRemoteAccount[] invitees, IGameInvitation invitation, Action<bool, Exception> sentCallback)
		{
			uXIuiIoLsHZLzmzGOgKAMpCeydnP uXIuiIoLsHZLzmzGOgKAMpCeydnP2 = new uXIuiIoLsHZLzmzGOgKAMpCeydnP();
			while (true)
			{
				int num = -428868329;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -335130916)) % 8)
					{
					case 4u:
						break;
					default:
						return;
					case 6u:
						uXIuiIoLsHZLzmzGOgKAMpCeydnP2.BWvkeUdUAXZdQdrRnUuRRYBfsZZC = invitees;
						num = (int)((num2 * 1084722818) ^ 0x5E1C10E7);
						continue;
					case 0u:
						uXIuiIoLsHZLzmzGOgKAMpCeydnP2.ctVEnxnyPJNLbwwfBSndAcMzIxuP = user;
						num = ((int)num2 * -645840368) ^ -63671966;
						continue;
					case 3u:
						uXIuiIoLsHZLzmzGOgKAMpCeydnP2.fzmOHwCVFAORQTdOFaeSSiVyGDdjA = this;
						num = ((int)num2 * -884203838) ^ 0x79F7D1A2;
						continue;
					case 1u:
						uXIuiIoLsHZLzmzGOgKAMpCeydnP2.zzHgJwzXHJcKWEVhIkkLZzZtwrcM = sentCallback;
						EfgdiGtxKrtefuGKEgjWCSAEbeND();
						num = ((int)num2 * -417340922) ^ -1166895985;
						continue;
					case 7u:
						uXIuiIoLsHZLzmzGOgKAMpCeydnP2.fUobkqhVXyuCUPgIlqCOHsTIdZJGc = invitation;
						num = ((int)num2 * -2128575367) ^ 0x37E5212;
						continue;
					case 5u:
						AsyncHelper.InvokeAsync(uXIuiIoLsHZLzmzGOgKAMpCeydnP2.CFGIOnXZHttteugfBnelxyhmpLiU, uXIuiIoLsHZLzmzGOgKAMpCeydnP2.OuBvbhlwAazqMWxSdXOvcAkfQwxg, uXIuiIoLsHZLzmzGOgKAMpCeydnP2.TQhPjZCIhoahoLNOCiPQbcOOdGNM);
						num = (int)(num2 * 821845858) ^ -934613892;
						continue;
					case 2u:
						return;
					}
					break;
				}
			}
		}

		public Task<bool> SendGameInviteAsync(ILocalAccount user, IRemoteAccount[] invitees, IGameInvitation invitation)
		{
			PKAGIlwuPAhdWmxJZiCgkoVqnmhhA obj = new PKAGIlwuPAhdWmxJZiCgkoVqnmhhA
			{
				fzmOHwCVFAORQTdOFaeSSiVyGDdjA = this,
				ctVEnxnyPJNLbwwfBSndAcMzIxuP = user,
				BWvkeUdUAXZdQdrRnUuRRYBfsZZC = invitees,
				fUobkqhVXyuCUPgIlqCOHsTIdZJGc = invitation
			};
			EfgdiGtxKrtefuGKEgjWCSAEbeND();
			return Task.Run((Func<bool>)obj.CFGIOnXZHttteugfBnelxyhmpLiU);
		}

		public IGameInvitation CreateInviteToMultiplayerSession(IMultiplayerSession session, byte[] applicationData = null)
		{
			SteamMultiplayerSession obj = (session as SteamMultiplayerSession) ?? throw new ArgumentException(zTycDwuNuvbAsEiUylpUmPhKeHTh.msPEYTwjnnntHvmoAhKLaJgUMEunA);
			if (!session.CanSendInvites || session.SessionInfo == null)
			{
				throw new InvalidOperationException("Multiplayer game session not ready to send invites.");
			}
			return new SteamGameInvitation(obj.SteamSessionInfo, applicationData);
		}

		public void CheckForLaunchInvite(bool fakeCommandLineArgs = false)
		{
			if (uagUCpiskYUgDzypBzdEKlIvHILE)
			{
				goto IL_0008;
			}
			goto IL_0081;
			IL_0008:
			int num = 964514251;
			goto IL_000d;
			IL_000d:
			uint num2;
			switch ((num2 = (uint)(num ^ 0x190697C0)) % 6)
			{
			case 5u:
				break;
			case 1u:
				throw new InvalidOperationException("Called CheckForLaunchInvite more than once.");
			case 3u:
				goto IL_0052;
			case 0u:
				goto IL_006e;
			case 2u:
				goto IL_0081;
			default:
				goto IL_0096;
			}
			goto IL_0008;
			IL_006e:
			string[] array = default(string[]);
			int num3 = default(int);
			string text = array[num3];
			if (!string.IsNullOrEmpty(text))
			{
				num = 1631623606;
				goto IL_000d;
			}
			goto IL_00f7;
			IL_0081:
			uagUCpiskYUgDzypBzdEKlIvHILE = true;
			string[] array2;
			if (fakeCommandLineArgs)
			{
				array2 = ALmjlZsDPdLTZqpsXcgtjFTpyfYiA();
				goto IL_005f;
			}
			num = 1590882781;
			goto IL_000d;
			IL_011e:
			if (num3 < array.Length)
			{
				goto IL_006e;
			}
			int num4 = 1790914130;
			goto IL_0100;
			IL_0100:
			switch ((num2 = (uint)(num4 ^ 0x190697C0)) % 3)
			{
			case 0u:
				break;
			default:
				return;
			case 2u:
				goto IL_011e;
			case 1u:
				return;
			}
			goto IL_00fb;
			IL_0052:
			array2 = Environment.GetCommandLineArgs();
			goto IL_005f;
			IL_005f:
			array = array2;
			num3 = 0;
			goto IL_011e;
			IL_0096:
			try
			{
				SteamGameInvitation invitation = SteamGameInvitation.FromString(text);
				jMvrYzdZlrUwGJCjRWPYpxtuJBDk?.SafelyInvoke(invitation, krdklyPInffabTJcsVYAeGdWXOoI.GetPrimaryAccount());
			}
			catch (Exception)
			{
				if (fakeCommandLineArgs)
				{
					while (true)
					{
						switch ((num2 = 2139575630u) % 3)
						{
						case 0u:
							break;
						default:
							goto end_IL_00c0;
						case 2u:
							throw;
						case 1u:
							goto end_IL_00c0;
						}
						continue;
						end_IL_00c0:
						break;
					}
				}
			}
			goto IL_00f7;
			IL_00f7:
			num3++;
			goto IL_00fb;
			IL_00fb:
			num4 = 880916625;
			goto IL_0100;
		}

		public void Dispose()
		{
			EBOFmlFXuTURaLShcWPnQiZUpheN(true);
			GC.SuppressFinalize(this);
		}

		~SteamGameInvitationManager()
		{
			EBOFmlFXuTURaLShcWPnQiZUpheN(false);
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
				switch ((num = 2058164366u) % 3)
				{
				case 0u:
					break;
				default:
					return;
				case 2u:
					throw new ObjectDisposedException(GetType().FullName);
				case 1u:
					return;
				}
			}
		}

		private void EBOFmlFXuTURaLShcWPnQiZUpheN(bool P_0)
		{
			if (FgbzqITuvQbQQUdgcAulRYogdIih != null)
			{
				while (true)
				{
					int num = -141304164;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -2117187359)) % 4)
						{
						case 0u:
							break;
						case 1u:
							FgbzqITuvQbQQUdgcAulRYogdIih.Dispose();
							num = ((int)num2 * -1456303771) ^ 0x108D9FCB;
							continue;
						case 3u:
							FgbzqITuvQbQQUdgcAulRYogdIih = null;
							num = ((int)num2 * -345704036) ^ -1371982621;
							continue;
						default:
							goto end_IL_0008;
						}
						break;
					}
					continue;
					end_IL_0008:
					break;
				}
			}
			bBzFCPvegdcjojlzNVfWaJyAejlV = true;
		}

		private void zdQpVMTVXpuQDlHdHGetzSVAIoAv(GameRichPresenceJoinRequested_t P_0)
		{
			SteamGameInvitation invitation = SteamGameInvitation.FromString(P_0.m_rgchConnect);
			while (true)
			{
				int num = -2000938795;
				while (true)
				{
					uint num2;
					Action<IGameInvitation, ILocalAccount> action;
					switch ((num2 = (uint)(num ^ -1334777411)) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 2u:
						action = jMvrYzdZlrUwGJCjRWPYpxtuJBDk;
						if (action != null)
						{
							goto IL_0039;
						}
						return;
					case 1u:
						return;
					}
					break;
					IL_0039:
					action.SafelyInvoke(invitation, krdklyPInffabTJcsVYAeGdWXOoI.GetPrimaryAccount());
					num = ((int)num2 * -674370374) ^ 0x44CB07C2;
				}
			}
		}

		private bool VxEUHokJncBSzQhIXzCwAtRmiGwF([NotNull] ILocalAccount P_0, [NotNull] IRemoteAccount[] P_1, [NotNull] IGameInvitation P_2)
		{
			EfgdiGtxKrtefuGKEgjWCSAEbeND();
			if (P_0 == null)
			{
				goto IL_000c;
			}
			goto IL_0122;
			IL_000c:
			int num = -1459676981;
			goto IL_0011;
			IL_0011:
			string text = default(string);
			SteamRemoteAccount steamRemoteAccount = default(SteamRemoteAccount);
			int num5 = default(int);
			IRemoteAccount[] array = default(IRemoteAccount[]);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -1824247406)) % 25)
				{
				case 6u:
					break;
				case 17u:
					text = text + steamRemoteAccount.SteamId.m_SteamID + " ";
					num = ((int)num2 * -1889308666) ^ 0x15FE1EBF;
					continue;
				case 15u:
					throw new ArgumentException(zTycDwuNuvbAsEiUylpUmPhKeHTh.iuiAEtKWhwsCVKGWoBnFvPagRPCYA, "user");
				case 18u:
					goto IL_00e1;
				case 20u:
					goto IL_0108;
				case 16u:
					goto IL_0122;
				case 11u:
					throw new ArgumentNullException("user");
				case 5u:
					goto IL_0157;
				case 12u:
					goto IL_0173;
				case 13u:
					goto IL_0194;
				case 1u:
					throw new ArgumentException(zTycDwuNuvbAsEiUylpUmPhKeHTh.BsWAuZvcZBCaHknVhFqduSAUuihT);
				case 24u:
					goto IL_01ce;
				case 8u:
				{
					int num6;
					int num7;
					if (text.Length > 0)
					{
						num6 = 165415281;
						num7 = num6;
					}
					else
					{
						num6 = 2028653860;
						num7 = num6;
					}
					num = num6 ^ ((int)num2 * -1039135137);
					continue;
				}
				case 22u:
					throw new ArgumentException(zTycDwuNuvbAsEiUylpUmPhKeHTh.YGqrxwkoYfsnfumZvTiNyXtovOPe, "invitation");
				case 7u:
					num = ((int)num2 * -74207041) ^ -1847763522;
					continue;
				case 9u:
					num5++;
					num = -1283385476;
					continue;
				case 3u:
					num5 = 0;
					num = ((int)num2 * -248322984) ^ 0xA192144;
					continue;
				case 10u:
					text = "";
					array = P_1;
					num = -274243979;
					continue;
				case 14u:
					throw new ArgumentNullException("invitees");
				case 4u:
					goto IL_0295;
				case 19u:
				{
					int num3;
					int num4;
					if (steamRemoteAccount != null)
					{
						num3 = 32892566;
						num4 = num3;
					}
					else
					{
						num3 = 912989231;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 1152734636);
					continue;
				}
				case 23u:
					throw new ArgumentNullException("invitation");
				case 21u:
					throw new SteamApiException("Failed sending game invite to users.: " + text);
				case 2u:
					throw new ArgumentException("Value cannot be an empty collection.", "invitees");
				default:
					return true;
				}
				break;
				IL_0295:
				int num8;
				if (P_2 == null)
				{
					num = -1057697725;
					num8 = num;
				}
				else
				{
					num = -1589682608;
					num8 = num;
				}
				continue;
				IL_00e1:
				int num9;
				if (SteamFriends.InviteUserToGame(steamRemoteAccount.SteamId, P_2.ToString()))
				{
					num = -1609021721;
					num9 = num;
				}
				else
				{
					num = -457103114;
					num9 = num;
				}
				continue;
				IL_0157:
				int num10;
				if (!(P_2 is SteamGameInvitation))
				{
					num = -1687554309;
					num10 = num;
				}
				else
				{
					num = -1968998854;
					num10 = num;
				}
				continue;
				IL_01ce:
				int num11;
				if (P_1.Length != 0)
				{
					num = -2044072034;
					num11 = num;
				}
				else
				{
					num = -197554722;
					num11 = num;
				}
				continue;
				IL_0173:
				steamRemoteAccount = (array[num5] ?? throw new ArgumentException("Null user passed in.")) as SteamRemoteAccount;
				num = -1668369732;
				continue;
				IL_0108:
				int num12;
				if (num5 < array.Length)
				{
					num = -568500881;
					num12 = num;
				}
				else
				{
					num = -1537853193;
					num12 = num;
				}
				continue;
				IL_0194:
				int num13;
				if (!(P_0 is SteamLocalAccount))
				{
					num = -1573854361;
					num13 = num;
				}
				else
				{
					num = -1863125510;
					num13 = num;
				}
			}
			goto IL_000c;
			IL_0122:
			int num14;
			if (P_1 != null)
			{
				num = -495322068;
				num14 = num;
			}
			else
			{
				num = -1231866431;
				num14 = num;
			}
			goto IL_0011;
		}

		private string[] ALmjlZsDPdLTZqpsXcgtjFTpyfYiA()
		{
			if (!File.Exists("CmdArgs.txt"))
			{
				goto IL_000c;
			}
			goto IL_006f;
			IL_000c:
			int num = 2022202247;
			goto IL_0011;
			IL_0011:
			char[] separator = default(char[]);
			string text = default(string);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x4D69E963)) % 5)
				{
				case 2u:
					break;
				case 4u:
					return new string[1] { string.Empty };
				case 1u:
					separator = new char[1] { ' ' };
					num = ((int)num2 * -1599539127) ^ 0x5E5B61B9;
					continue;
				case 0u:
					goto IL_006f;
				default:
					return text.Split(separator);
				}
				break;
			}
			goto IL_000c;
			IL_006f:
			string text2 = File.ReadAllText("CmdArgs.txt");
			text = string.Empty;
			text += text2;
			num = 1710381731;
			goto IL_0011;
		}
	}
}
