using System.Collections.Generic;

namespace BitCode.Users
{
	public static class ILocalAccountManagerExtensions
	{
		public static ILocalAccount GetByLocalId(this ILocalAccountManager localAccountManager, ulong localId)
		{
			IEnumerator<ILocalAccount> enumerator = localAccountManager.GetEnumerator();
			try
			{
				ILocalAccount current = default(ILocalAccount);
				while (true)
				{
					IL_007a:
					int num;
					int num2;
					if (!enumerator.MoveNext())
					{
						num = -267500008;
						num2 = num;
					}
					else
					{
						num = -2139057801;
						num2 = num;
					}
					while (true)
					{
						uint num3;
						switch ((num3 = (uint)(num ^ -798538612)) % 6)
						{
						case 0u:
							num = -2139057801;
							continue;
						default:
							goto end_IL_000e;
						case 1u:
							current = enumerator.Current;
							num = -946740619;
							continue;
						case 3u:
						{
							int num4;
							int num5;
							if (current.LocalId != localId)
							{
								num4 = -1852343585;
								num5 = num4;
							}
							else
							{
								num4 = -1196648586;
								num5 = num4;
							}
							num = num4 ^ ((int)num3 * -1168851612);
							continue;
						}
						case 4u:
							return current;
						case 5u:
							break;
						case 2u:
							goto end_IL_000e;
						}
						goto IL_007a;
						continue;
						end_IL_000e:
						break;
					}
					break;
				}
			}
			finally
			{
				if (enumerator != null)
				{
					while (true)
					{
						IL_009b:
						int num6 = -427203099;
						while (true)
						{
							uint num3;
							switch ((num3 = (uint)(num6 ^ -798538612)) % 3)
							{
							case 2u:
								break;
							default:
								goto end_IL_00a0;
							case 1u:
								goto IL_00bd;
							case 0u:
								goto end_IL_00a0;
							}
							goto IL_009b;
							IL_00bd:
							enumerator.Dispose();
							num6 = ((int)num3 * -1365003229) ^ 0x352666B9;
							continue;
							end_IL_00a0:
							break;
						}
						break;
					}
				}
			}
			return null;
		}

		public static ILocalAccount GetByOnlineAccountId(this ILocalAccountManager localAccountManager, ulong onlineAccountId)
		{
			IEnumerator<ILocalAccount> enumerator = localAccountManager.GetEnumerator();
			try
			{
				ulong? onlineAccountId2 = default(ulong?);
				ILocalAccount current = default(ILocalAccount);
				ILocalAccount result = default(ILocalAccount);
				while (true)
				{
					IL_0083:
					int num;
					int num2;
					if (!enumerator.MoveNext())
					{
						num = -1853718157;
						num2 = num;
					}
					else
					{
						num = -801258462;
						num2 = num;
					}
					while (true)
					{
						uint num3;
						switch ((num3 = (uint)(num ^ -906459048)) % 9)
						{
						case 2u:
							num = -801258462;
							continue;
						default:
							goto end_IL_000e;
						case 1u:
							onlineAccountId2 = current.OnlineAccountId;
							num = ((int)num3 * -1528972732) ^ -633337459;
							continue;
						case 7u:
						{
							int num6;
							int num7;
							if (onlineAccountId2.Value == onlineAccountId)
							{
								num6 = 180545011;
								num7 = num6;
							}
							else
							{
								num6 = 1310179966;
								num7 = num6;
							}
							num = num6 ^ ((int)num3 * -2013397468);
							continue;
						}
						case 8u:
							break;
						case 0u:
							result = current;
							num = (int)(num3 * 1921653481) ^ -1596487578;
							continue;
						case 5u:
						{
							int num4;
							int num5;
							if (onlineAccountId2.HasValue)
							{
								num4 = -1632654317;
								num5 = num4;
							}
							else
							{
								num4 = -1043214614;
								num5 = num4;
							}
							num = num4 ^ ((int)num3 * -249998434);
							continue;
						}
						case 3u:
							current = enumerator.Current;
							onlineAccountId2 = current.OnlineAccountId;
							num = -122388256;
							continue;
						case 4u:
							goto end_IL_000e;
						case 6u:
							return result;
						}
						goto IL_0083;
						continue;
						end_IL_000e:
						break;
					}
					break;
				}
			}
			finally
			{
				if (enumerator != null)
				{
					while (true)
					{
						IL_010f:
						int num8 = -901030383;
						while (true)
						{
							uint num3;
							switch ((num3 = (uint)(num8 ^ -906459048)) % 3)
							{
							case 2u:
								break;
							default:
								goto end_IL_0114;
							case 1u:
								goto IL_0132;
							case 0u:
								goto end_IL_0114;
							}
							goto IL_010f;
							IL_0132:
							enumerator.Dispose();
							num8 = ((int)num3 * -1234900568) ^ 0x21714435;
							continue;
							end_IL_0114:
							break;
						}
						break;
					}
				}
			}
			return null;
		}

		public static ILocalAccount GetPrimaryAccount(this ILocalAccountManager localAccountManager)
		{
			if (localAccountManager.Count <= 0)
			{
				while (true)
				{
					uint num;
					switch ((num = 1811185894u) % 3)
					{
					case 2u:
						continue;
					case 1u:
						return null;
					}
					break;
				}
			}
			return localAccountManager[0];
		}
	}
}
