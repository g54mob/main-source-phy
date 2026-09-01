using System;
using System.Collections;
using System.Collections.Generic;
using BitCode.Users;
using Steamworks;

namespace BitCode.Platform.Steamworks
{
	public class SteamLocalAccountManager : IEnumerable<ILocalAccount>, IPlatformService, IDisposable, ILocalAccountManager, IEnumerable
	{
		private readonly SteamService JvEZsMEbjtsmodyZFfToTIvzzHoC;

		private readonly List<SteamLocalAccount> EoKOHYnVfZOmrNFpMKpknxjHtTFn;

		private bool bBzFCPvegdcjojlzNVfWaJyAejlV;

		public int Count => EoKOHYnVfZOmrNFpMKpknxjHtTFn.Count;

		public ILocalAccount this[int index] => EoKOHYnVfZOmrNFpMKpknxjHtTFn[index];

		event Action<ILocalAccount> ILocalAccountManager.AccountAdded
		{
			add
			{
			}
			remove
			{
			}
		}

		event Action<ILocalAccount> ILocalAccountManager.AccountLeft
		{
			add
			{
			}
			remove
			{
			}
		}

		event Action<Exception> ILocalAccountManager.AccountSignInFailed
		{
			add
			{
			}
			remove
			{
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

		public SteamLocalAccountManager(SteamService steamService)
		{
			JvEZsMEbjtsmodyZFfToTIvzzHoC = steamService;
			EoKOHYnVfZOmrNFpMKpknxjHtTFn = new List<SteamLocalAccount>();
			CSteamID steamID = SteamUser.GetSteamID();
			EoKOHYnVfZOmrNFpMKpknxjHtTFn.Add(new SteamLocalAccount(steamID, steamService));
		}

		~SteamLocalAccountManager()
		{
			EBOFmlFXuTURaLShcWPnQiZUpheN(false);
		}

		public void PromptSignIn(SignInPromptOptions options)
		{
			throw new NotSupportedException();
		}

		public void Dispose()
		{
			EBOFmlFXuTURaLShcWPnQiZUpheN(true);
			while (true)
			{
				int num = 2134420895;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x3D3869CA)) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 2u:
						goto IL_0029;
					case 1u:
						return;
					}
					break;
					IL_0029:
					GC.SuppressFinalize(this);
					num = (int)(num2 * 274757273) ^ -607966973;
				}
			}
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
				switch ((num = 139961168u) % 3)
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
			if (bBzFCPvegdcjojlzNVfWaJyAejlV)
			{
				goto IL_000b;
			}
			goto IL_0090;
			IL_000b:
			int num = 1417320534;
			goto IL_0010;
			IL_0010:
			int num3 = default(int);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x1C7350C3)) % 8)
				{
				case 4u:
					break;
				case 5u:
					return;
				case 6u:
					num3 = 0;
					num = (int)((num2 * 764217835) ^ 0x27AD8B01);
					continue;
				case 2u:
					EoKOHYnVfZOmrNFpMKpknxjHtTFn[num3].Dispose();
					num3++;
					num = 2057391228;
					continue;
				case 0u:
					num = ((int)num2 * -1827936347) ^ 0x7D217774;
					continue;
				case 1u:
					goto IL_0090;
				case 7u:
					goto IL_00a7;
				default:
					bBzFCPvegdcjojlzNVfWaJyAejlV = true;
					return;
				}
				break;
				IL_00a7:
				int num4;
				if (num3 < EoKOHYnVfZOmrNFpMKpknxjHtTFn.Count)
				{
					num = 1830858257;
					num4 = num;
				}
				else
				{
					num = 1395644104;
					num4 = num;
				}
			}
			goto IL_000b;
			IL_0090:
			int num5;
			if (P_0)
			{
				num = 1902649021;
				num5 = num;
			}
			else
			{
				num = 1395644104;
				num5 = num;
			}
			goto IL_0010;
		}

		public IEnumerator<ILocalAccount> GetEnumerator()
		{
			CheckDisposed();
			return EoKOHYnVfZOmrNFpMKpknxjHtTFn.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			CheckDisposed();
			return GetEnumerator();
		}
	}
}
