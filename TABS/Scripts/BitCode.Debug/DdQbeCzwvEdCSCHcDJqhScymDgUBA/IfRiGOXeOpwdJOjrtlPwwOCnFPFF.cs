using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace DdQbeCzwvEdCSCHcDJqhScymDgUBA
{
	internal static class IfRiGOXeOpwdJOjrtlPwwOCnFPFF
	{
		[Serializable]
		private sealed class zhzuCEXyYSJokcxwneoOptcywCwm
		{
			public static readonly zhzuCEXyYSJokcxwneoOptcywCwm _003C_003E9 = new zhzuCEXyYSJokcxwneoOptcywCwm();

			public static Func<Type, bool> _003C_003E9__1_0;

			public static Func<Type, bool> _003C_003E9__1_1;

			public static Func<Type, bool> _003C_003E9__3_0;

			public static Func<Type, bool> _003C_003E9__3_1;

			internal bool GZKFdsdSMgwRkxzkGTpHWbtuKgHW(Type P_0)
			{
				return P_0.IsConstructedGenericType;
			}

			internal bool yXxIrrJxSGHVSxnoxAqhzVUGpazQ(Type P_0)
			{
				return P_0.GetGenericTypeDefinition() == typeof(IEnumerable<>);
			}

			internal bool yKFOzqsaJwnuIOIPpEldjOYFyJZG(Type P_0)
			{
				return P_0.IsConstructedGenericType;
			}

			internal bool HzHixkuSlYMUuJhOVdYmboJgeDEw(Type P_0)
			{
				return P_0.GetGenericTypeDefinition() == typeof(ICollection<>);
			}
		}

		internal static int ctdEYIimdWRLVraecBsvmlqRdPgg([NotNull] Type P_0, [NotNull] Type P_1)
		{
			int num = 0;
			while (true)
			{
				int num2 = -1170405958;
				while (true)
				{
					uint num3;
					switch ((num3 = (uint)(num2 ^ -1667726221)) % 8)
					{
					case 7u:
						break;
					case 0u:
					{
						int num7;
						int num8;
						if (tVHozYRxcsSRXXCNmQKwhxuWsvkt(P_0).Contains(P_1))
						{
							num7 = 1576918351;
							num8 = num7;
						}
						else
						{
							num7 = 874244280;
							num8 = num7;
						}
						num2 = num7 ^ ((int)num3 * -2125939616);
						continue;
					}
					case 5u:
					{
						int num6;
						if (!(P_0 != P_1))
						{
							num2 = -354350257;
							num6 = num2;
						}
						else
						{
							num2 = -955565597;
							num6 = num2;
						}
						continue;
					}
					case 2u:
						throw new InvalidOperationException($"Ancestor type {P_1} is not assignable from {P_0}.");
					case 6u:
						P_0 = P_0.BaseType;
						num2 = (int)(num3 * 1946615964) ^ -488665138;
						continue;
					case 3u:
						num++;
						num2 = -1980372795;
						continue;
					case 1u:
					{
						int num4;
						int num5;
						if (!P_1.IsAssignableFrom(P_0))
						{
							num4 = 1088211363;
							num5 = num4;
						}
						else
						{
							num4 = 1529874196;
							num5 = num4;
						}
						num2 = num4 ^ (int)(num3 * 1535208866);
						continue;
					}
					default:
						return num;
					}
					break;
				}
			}
		}

		internal static Type NmKsMyoHUcTBNVdDINorfTzMqwWJ(Type P_0)
		{
			return P_0.GetInterfaces().Where(zhzuCEXyYSJokcxwneoOptcywCwm._003C_003E9.GZKFdsdSMgwRkxzkGTpHWbtuKgHW).FirstOrDefault(zhzuCEXyYSJokcxwneoOptcywCwm._003C_003E9.yXxIrrJxSGHVSxnoxAqhzVUGpazQ);
		}

		internal static Type eFHRywyZLBLcaVAtPVdEzuzhidup(Type P_0)
		{
			Type type = NmKsMyoHUcTBNVdDINorfTzMqwWJ(P_0);
			if (type != null)
			{
				while (true)
				{
					uint num;
					switch ((num = 8935919u) % 3)
					{
					case 0u:
						continue;
					case 2u:
						return type.GenericTypeArguments[0];
					}
					break;
				}
			}
			return null;
		}

		internal static Type GFgfqNjJDhNErXepPYImmVnskJsDb(Type P_0)
		{
			return P_0.GetInterfaces().Where(zhzuCEXyYSJokcxwneoOptcywCwm._003C_003E9.yKFOzqsaJwnuIOIPpEldjOYFyJZG).FirstOrDefault(zhzuCEXyYSJokcxwneoOptcywCwm._003C_003E9.HzHixkuSlYMUuJhOVdYmboJgeDEw);
		}

		private static IEnumerable<Type> tVHozYRxcsSRXXCNmQKwhxuWsvkt(Type P_0)
		{
			if (!(P_0.BaseType == null))
			{
				goto IL_000e;
			}
			goto IL_005a;
			IL_000e:
			int num = -1797717512;
			goto IL_0013;
			IL_0013:
			uint num2;
			IEnumerable<Type> interfaces = default(IEnumerable<Type>);
			switch ((num2 = (uint)(num ^ -1478685202)) % 4)
			{
			case 0u:
				break;
			case 2u:
				return P_0.GetInterfaces().Except(P_0.BaseType.GetInterfaces());
			case 3u:
				goto IL_005a;
			default:
				return interfaces;
			}
			goto IL_000e;
			IL_005a:
			interfaces = P_0.GetInterfaces();
			num = -1743270025;
			goto IL_0013;
		}
	}
}
