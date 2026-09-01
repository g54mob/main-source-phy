using System;
using UnityEngine;

namespace BitCode.Logging
{
	public class UnityLogWriter : ILogWriter
	{
		public void Write(LogSeverity severity, string message)
		{
			if (severity <= LogSeverity.Info)
			{
				goto IL_0008;
			}
			goto IL_0108;
			IL_0008:
			int num = -788028792;
			goto IL_000d;
			IL_000d:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -1119957460)) % 18)
				{
				case 15u:
					break;
				case 0u:
				{
					int num5;
					int num6;
					if (severity == LogSeverity.Critical)
					{
						num5 = 2084003662;
						num6 = num5;
					}
					else
					{
						num5 = 732083814;
						num6 = num5;
					}
					num = num5 ^ (int)(num2 * 530902960);
					continue;
				}
				case 17u:
				{
					int num11;
					int num12;
					if (severity != LogSeverity.Warning)
					{
						num11 = 1080984662;
						num12 = num11;
					}
					else
					{
						num11 = 701073679;
						num12 = num11;
					}
					num = num11 ^ (int)(num2 * 1012631561);
					continue;
				}
				case 13u:
					Debug.LogError(message);
					return;
				case 5u:
					Debug.Log(message);
					return;
				case 12u:
					goto IL_00ce;
				case 7u:
				{
					int num9;
					int num10;
					if (severity == LogSeverity.Error)
					{
						num9 = -1005386732;
						num10 = num9;
					}
					else
					{
						num9 = -949103158;
						num10 = num9;
					}
					num = num9 ^ ((int)num2 * -577280299);
					continue;
				}
				case 6u:
					goto IL_0108;
				case 4u:
					Debug.LogError("CRITICAL: " + message);
					return;
				case 8u:
					num = ((int)num2 * -1838262459) ^ -1453624715;
					continue;
				case 14u:
				{
					int num13;
					int num14;
					if (severity == LogSeverity.Verbose)
					{
						num13 = -1121938723;
						num14 = num13;
					}
					else
					{
						num13 = -1610450451;
						num14 = num13;
					}
					num = num13 ^ (int)(num2 * 1939753060);
					continue;
				}
				case 3u:
				{
					int num7;
					int num8;
					if (severity == LogSeverity.Trace)
					{
						num7 = 480240284;
						num8 = num7;
					}
					else
					{
						num7 = 489445613;
						num8 = num7;
					}
					num = num7 ^ (int)(num2 * 574017665);
					continue;
				}
				case 1u:
					num = ((int)num2 * -1363971967) ^ -1742707070;
					continue;
				case 16u:
				{
					int num3;
					int num4;
					if (severity != LogSeverity.Info)
					{
						num3 = -1410650702;
						num4 = num3;
					}
					else
					{
						num3 = -1382214595;
						num4 = num3;
					}
					num = num3 ^ ((int)num2 * -1821614313);
					continue;
				}
				case 2u:
					num = (int)((num2 * 1160940247) ^ 0x69E26305);
					continue;
				case 10u:
					Debug.LogWarning(message);
					num = -635200907;
					continue;
				case 11u:
					return;
				default:
					throw new ArgumentOutOfRangeException("severity", severity, null);
				}
				break;
				IL_00ce:
				int num15;
				if (severity == LogSeverity.Assert)
				{
					num = -364375145;
					num15 = num;
				}
				else
				{
					num = -1717947262;
					num15 = num;
				}
			}
			goto IL_0008;
			IL_0108:
			int num16;
			if (severity > LogSeverity.Error)
			{
				num = -178666110;
				num16 = num;
			}
			else
			{
				num = -777352887;
				num16 = num;
			}
			goto IL_000d;
		}

		public void WriteException(Exception ex)
		{
			Debug.LogException(ex);
		}
	}
}
