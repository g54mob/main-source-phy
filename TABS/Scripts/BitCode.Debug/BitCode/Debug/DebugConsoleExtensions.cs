using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BitCode.Attributes;
using BitCode.Debug.TokenResolvers;

namespace BitCode.Debug
{
	public static class DebugConsoleExtensions
	{
		private struct PvEgDSbcBcrDBvbsawCrclhIBGNBA
		{
			public readonly MethodInfo HlGwOAKaoNMxiiutGrpPqeWSOgBM;

			public readonly DebugCommandAttribute qvkIJCdPVJSCVOYjPFfnzNAwIYFh;

			public readonly string YBLwrmnGpuNNorfGBIOCDXiTtNWV;

			public readonly string RPFxjSkyEPTyMTlnykcAETQhIstB;

			public PvEgDSbcBcrDBvbsawCrclhIBGNBA(MethodInfo P_0)
			{
				this = default(PvEgDSbcBcrDBvbsawCrclhIBGNBA);
				HlGwOAKaoNMxiiutGrpPqeWSOgBM = P_0;
				qvkIJCdPVJSCVOYjPFfnzNAwIYFh = HlGwOAKaoNMxiiutGrpPqeWSOgBM.GetCustomAttribute<DebugCommandAttribute>();
				YBLwrmnGpuNNorfGBIOCDXiTtNWV = qvkIJCdPVJSCVOYjPFfnzNAwIYFh?.Name ?? P_0.Name;
				RPFxjSkyEPTyMTlnykcAETQhIstB = qvkIJCdPVJSCVOYjPFfnzNAwIYFh?.Description;
			}
		}

		private sealed class GnecnrhPfsbptcHKeDdfrKHVwyJLA
		{
			public Type fFBbqLtnuQHQmJcbEZakxTzolnGB;

			public Func<Type, bool> JMcudtKaYFaqKlvOBFtBhCiAwINH;

			internal bool kKppCGDWaVHhLAEwWAebCbpvOGUwA(Type P_0)
			{
				return P_0.GetInterfaces().Any(HVwPBKFuBlrPJPWRPgjcmuKzELjD);
			}

			internal bool HVwPBKFuBlrPJPWRPgjcmuKzELjD(Type P_0)
			{
				return P_0 == fFBbqLtnuQHQmJcbEZakxTzolnGB;
			}
		}

		[Serializable]
		private sealed class zhzuCEXyYSJokcxwneoOptcywCwm
		{
			public static readonly zhzuCEXyYSJokcxwneoOptcywCwm _003C_003E9 = new zhzuCEXyYSJokcxwneoOptcywCwm();

			public static Func<Type, bool> _003C_003E9__4_1;

			public static Func<Type, IEnumerable<MethodInfo>> _003C_003E9__5_0;

			public static Func<MethodInfo, PvEgDSbcBcrDBvbsawCrclhIBGNBA> _003C_003E9__5_1;

			public static Func<PvEgDSbcBcrDBvbsawCrclhIBGNBA, bool> _003C_003E9__5_2;

			public static Func<MethodInfo, PvEgDSbcBcrDBvbsawCrclhIBGNBA> _003C_003E9__6_0;

			public static Func<PvEgDSbcBcrDBvbsawCrclhIBGNBA, bool> _003C_003E9__6_1;

			internal bool uvXENNFYjZBAtaAxetTEuKkhocMGc(Type P_0)
			{
				return P_0.GetConstructor(Type.EmptyTypes) != null;
			}

			internal IEnumerable<MethodInfo> WsagPiAlENSygovDhriTGBVGNVhn(Type P_0)
			{
				return P_0.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}

			internal PvEgDSbcBcrDBvbsawCrclhIBGNBA xioxSOSfvGArDPZfrcAyZEjkYEtS(MethodInfo P_0)
			{
				return new PvEgDSbcBcrDBvbsawCrclhIBGNBA(P_0);
			}

			internal bool OUMORgzamGaIYOmQeVdKphUXgpbO(PvEgDSbcBcrDBvbsawCrclhIBGNBA P_0)
			{
				return P_0.qvkIJCdPVJSCVOYjPFfnzNAwIYFh != null;
			}

			internal PvEgDSbcBcrDBvbsawCrclhIBGNBA AzaIfKCrIeibeHhFwnTUoftOdCBgA(MethodInfo P_0)
			{
				return new PvEgDSbcBcrDBvbsawCrclhIBGNBA(P_0);
			}

			internal bool JLqgrlAvmEPmQwizAcOfwhxVfsBF(PvEgDSbcBcrDBvbsawCrclhIBGNBA P_0)
			{
				return P_0.qvkIJCdPVJSCVOYjPFfnzNAwIYFh != null;
			}
		}

		public static void RegisterDefaultResolvers(this DebugConsole console)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			while (true)
			{
				int num = 154048894;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x7C9EBD17)) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 2u:
						goto IL_0028;
					case 1u:
						return;
					}
					break;
					IL_0028:
					console.RegisterResolversFromAssembly(executingAssembly);
					num = ((int)num2 * -1913779143) ^ 0xA465374;
				}
			}
		}

		public static void RegisterDefaultCommands(this DebugConsole console)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			console.RegisterCommandsFromAssembly(executingAssembly);
		}

		public static void RegisterDefaults(this DebugConsole console)
		{
			console.RegisterDefaultResolvers();
			console.RegisterDefaultCommands();
		}

		public static void RegisterResolversFromAssembly(this DebugConsole console, Assembly assembly)
		{
			GnecnrhPfsbptcHKeDdfrKHVwyJLA gnecnrhPfsbptcHKeDdfrKHVwyJLA = new GnecnrhPfsbptcHKeDdfrKHVwyJLA();
			object obj = default(object);
			while (true)
			{
				int num = -1743523429;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -704006277)) % 3)
					{
					case 0u:
						break;
					case 2u:
						goto IL_0028;
					default:
					{
						IEnumerator<Type> enumerator = assembly.GetTypes().Where(gnecnrhPfsbptcHKeDdfrKHVwyJLA.kKppCGDWaVHhLAEwWAebCbpvOGUwA).Where(zhzuCEXyYSJokcxwneoOptcywCwm._003C_003E9.uvXENNFYjZBAtaAxetTEuKkhocMGc)
							.GetEnumerator();
						try
						{
							while (true)
							{
								int num3;
								int num4;
								if (enumerator.MoveNext())
								{
									num3 = -270902237;
									num4 = num3;
								}
								else
								{
									num3 = -736280080;
									num4 = num3;
								}
								while (true)
								{
									switch ((num2 = (uint)(num3 ^ -704006277)) % 5)
									{
									case 3u:
										num3 = -270902237;
										continue;
									default:
										return;
									case 1u:
										obj = Activator.CreateInstance(enumerator.Current);
										num3 = -2001038105;
										continue;
									case 4u:
										console.RegisterTokenResolver((ITokenResolver)obj);
										num3 = (int)((num2 * 995481335) ^ 0x5B5B5F0F);
										continue;
									case 0u:
										break;
									case 2u:
										return;
									}
									break;
								}
							}
						}
						finally
						{
							if (enumerator != null)
							{
								while (true)
								{
									IL_0100:
									int num5 = -948260378;
									while (true)
									{
										switch ((num2 = (uint)(num5 ^ -704006277)) % 3)
										{
										case 0u:
											break;
										default:
											goto end_IL_0105;
										case 1u:
											goto IL_0122;
										case 2u:
											goto end_IL_0105;
										}
										goto IL_0100;
										IL_0122:
										enumerator.Dispose();
										num5 = ((int)num2 * -891337047) ^ 0x7F12C16D;
										continue;
										end_IL_0105:
										break;
									}
									break;
								}
							}
						}
					}
					}
					break;
					IL_0028:
					gnecnrhPfsbptcHKeDdfrKHVwyJLA.fFBbqLtnuQHQmJcbEZakxTzolnGB = typeof(ITokenResolver);
					num = (int)(num2 * 199446326) ^ -2014527957;
				}
			}
		}

		public static void RegisterCommandsFromAssembly(this DebugConsole console, Assembly assembly)
		{
			IEnumerator<PvEgDSbcBcrDBvbsawCrclhIBGNBA> enumerator = assembly.GetExportedTypes().SelectMany(zhzuCEXyYSJokcxwneoOptcywCwm._003C_003E9.WsagPiAlENSygovDhriTGBVGNVhn).Select(zhzuCEXyYSJokcxwneoOptcywCwm._003C_003E9.xioxSOSfvGArDPZfrcAyZEjkYEtS)
				.Where(zhzuCEXyYSJokcxwneoOptcywCwm._003C_003E9.OUMORgzamGaIYOmQeVdKphUXgpbO)
				.GetEnumerator();
			try
			{
				PvEgDSbcBcrDBvbsawCrclhIBGNBA current = default(PvEgDSbcBcrDBvbsawCrclhIBGNBA);
				while (true)
				{
					int num;
					int num2;
					if (enumerator.MoveNext())
					{
						num = 1252822574;
						num2 = num;
					}
					else
					{
						num = 678676556;
						num2 = num;
					}
					while (true)
					{
						uint num3;
						switch ((num3 = (uint)(num ^ 0x53039E55)) % 5)
						{
						case 2u:
							num = 1252822574;
							continue;
						default:
							return;
						case 4u:
							current = enumerator.Current;
							num = 1561823137;
							continue;
						case 3u:
							console.RegisterCommand(current.YBLwrmnGpuNNorfGBIOCDXiTtNWV, current.HlGwOAKaoNMxiiutGrpPqeWSOgBM, current.RPFxjSkyEPTyMTlnykcAETQhIstB);
							num = ((int)num3 * -1921961546) ^ -1502350007;
							continue;
						case 0u:
							break;
						case 1u:
							return;
						}
						break;
					}
				}
			}
			finally
			{
				if (enumerator != null)
				{
					while (true)
					{
						IL_00f7:
						int num4 = 493670688;
						while (true)
						{
							uint num3;
							switch ((num3 = (uint)(num4 ^ 0x53039E55)) % 3)
							{
							case 2u:
								break;
							default:
								goto end_IL_00fc;
							case 1u:
								goto IL_0119;
							case 0u:
								goto end_IL_00fc;
							}
							goto IL_00f7;
							IL_0119:
							enumerator.Dispose();
							num4 = (int)((num3 * 540067826) ^ 0x486BD412);
							continue;
							end_IL_00fc:
							break;
						}
						break;
					}
				}
			}
		}

		public static void RegisterCommandsFromType(this DebugConsole console, Type type)
		{
			IEnumerator<PvEgDSbcBcrDBvbsawCrclhIBGNBA> enumerator = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).Select(zhzuCEXyYSJokcxwneoOptcywCwm._003C_003E9.AzaIfKCrIeibeHhFwnTUoftOdCBgA).Where(zhzuCEXyYSJokcxwneoOptcywCwm._003C_003E9.JLqgrlAvmEPmQwizAcOfwhxVfsBF)
				.GetEnumerator();
			try
			{
				while (true)
				{
					int num;
					int num2;
					if (!enumerator.MoveNext())
					{
						num = 1590048492;
						num2 = num;
					}
					else
					{
						num = 1385048875;
						num2 = num;
					}
					while (true)
					{
						uint num3;
						switch ((num3 = (uint)(num ^ 0x1094E02E)) % 4)
						{
						case 3u:
							num = 1385048875;
							continue;
						default:
							return;
						case 1u:
						{
							PvEgDSbcBcrDBvbsawCrclhIBGNBA current = enumerator.Current;
							console.RegisterCommand(current.YBLwrmnGpuNNorfGBIOCDXiTtNWV, current.HlGwOAKaoNMxiiutGrpPqeWSOgBM);
							num = 1120062702;
							continue;
						}
						case 0u:
							break;
						case 2u:
							return;
						}
						break;
					}
				}
			}
			finally
			{
				if (enumerator != null)
				{
					while (true)
					{
						IL_00bd:
						int num4 = 1856932149;
						while (true)
						{
							uint num3;
							switch ((num3 = (uint)(num4 ^ 0x1094E02E)) % 3)
							{
							case 0u:
								break;
							default:
								goto end_IL_00c2;
							case 1u:
								goto IL_00df;
							case 2u:
								goto end_IL_00c2;
							}
							goto IL_00bd;
							IL_00df:
							enumerator.Dispose();
							num4 = ((int)num3 * -65987546) ^ -246260619;
							continue;
							end_IL_00c2:
							break;
						}
						break;
					}
				}
			}
		}
	}
}
