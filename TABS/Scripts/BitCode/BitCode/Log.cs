using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using BitCode.Logging;
using JetBrains.Annotations;

namespace BitCode
{
	public static class Log
	{
		public static class Trace
		{
			[StructLayout(LayoutKind.Auto)]
			private struct pIYWguDmZCloYILqODpXfIyPIkhOA
			{
				public StringBuilder WMXiSAgttNuytVDQGTKPcktHQFkG;
			}

			[Serializable]
			private sealed class cxvKGedwrzWugfSXrjKsfQLkqTju<_0001>
			{
				public static readonly cxvKGedwrzWugfSXrjKsfQLkqTju<_0001> _003C_003E9 = new cxvKGedwrzWugfSXrjKsfQLkqTju<_0001>();

				public static Func<PropertyInfo, bool> _003C_003E9__10_0;

				internal bool goCMGCznISCFnKsRkaYVyufuACOFA(PropertyInfo P_0)
				{
					return P_0.CanRead;
				}
			}

			private sealed class YPkJlOGgBEObnyABSocjgFgpdYRT<_0001>
			{
				public bool YhVNsWPfFneJmahzKxabibzeEhXfA;

				internal string vRqxzLFcPDxRjUnJZPhSyQiXFlSy(object P_0)
				{
					return iqklzKRxIqVchztPwUeDFUYOuJDg(P_0, YhVNsWPfFneJmahzKxabibzeEhXfA);
				}
			}

			public const BindingFlags PublicInstances = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField | BindingFlags.GetProperty;

			public const BindingFlags AllInstances = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.GetProperty;

			private const bool ypHmdeZqJzgVDGUJLSFascvtvFcx = true;

			[StringFormatMethod("message")]
			[Conditional("ENABLE_LOGGING")]
			public static void Write(string message, params object[] args)
			{
				if (Verbosity > LogSeverity.Trace)
				{
					while (true)
					{
						uint num;
						switch ((num = 1144221400u) % 3)
						{
						case 2u:
							continue;
						case 1u:
							return;
						}
						break;
					}
				}
				IEnumerator<ILogWriter> enumerator = RTtlvMkXoYEtJspIFYPopSMCoxAm.GetEnumerator();
				try
				{
					while (true)
					{
						int num2;
						int num3;
						if (enumerator.MoveNext())
						{
							num2 = -1433819216;
							num3 = num2;
						}
						else
						{
							num2 = -1732357989;
							num3 = num2;
						}
						while (true)
						{
							uint num;
							switch ((num = (uint)(num2 ^ -1152464350)) % 4)
							{
							case 0u:
								num2 = -1433819216;
								continue;
							default:
								return;
							case 2u:
								enumerator.Current.Write(LogSeverity.Trace, string.Format(message, args));
								num2 = -267644759;
								continue;
							case 3u:
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
							IL_00a5:
							int num4 = -379216658;
							while (true)
							{
								uint num;
								switch ((num = (uint)(num4 ^ -1152464350)) % 3)
								{
								case 0u:
									break;
								default:
									goto end_IL_00aa;
								case 1u:
									goto IL_00c7;
								case 2u:
									goto end_IL_00aa;
								}
								goto IL_00a5;
								IL_00c7:
								enumerator.Dispose();
								num4 = ((int)num * -744579027) ^ -945972775;
								continue;
								end_IL_00aa:
								break;
							}
							break;
						}
					}
				}
			}

			[Conditional("ENABLE_LOGGING")]
			public static void Write(string message)
			{
				if (Verbosity > LogSeverity.Trace)
				{
					while (true)
					{
						uint num;
						switch ((num = 894431719u) % 3)
						{
						case 0u:
							continue;
						case 1u:
							return;
						}
						break;
					}
				}
				IEnumerator<ILogWriter> enumerator = RTtlvMkXoYEtJspIFYPopSMCoxAm.GetEnumerator();
				try
				{
					while (true)
					{
						int num2;
						int num3;
						if (enumerator.MoveNext())
						{
							num2 = -1167047822;
							num3 = num2;
						}
						else
						{
							num2 = -28704237;
							num3 = num2;
						}
						while (true)
						{
							uint num;
							switch ((num = (uint)(num2 ^ -1281112277)) % 4)
							{
							case 2u:
								num2 = -1167047822;
								continue;
							default:
								return;
							case 1u:
								enumerator.Current.Write(LogSeverity.Trace, message);
								num2 = -1337352284;
								continue;
							case 3u:
								break;
							case 0u:
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
							IL_009f:
							int num4 = -701428527;
							while (true)
							{
								uint num;
								switch ((num = (uint)(num4 ^ -1281112277)) % 3)
								{
								case 2u:
									break;
								default:
									goto end_IL_00a4;
								case 1u:
									goto IL_00c1;
								case 0u:
									goto end_IL_00a4;
								}
								goto IL_009f;
								IL_00c1:
								enumerator.Dispose();
								num4 = ((int)num * -1864760063) ^ 0x3889F5B2;
								continue;
								end_IL_00a4:
								break;
							}
							break;
						}
					}
				}
			}

			public static void Method()
			{
			}

			[StringFormatMethod("message")]
			public static void Method(string message, params object[] args)
			{
			}

			[Conditional("ENABLE_LOGGING")]
			public static void List(IEnumerable list, string name = null, bool suppressExceptions = true)
			{
			}

			[Conditional("ENABLE_LOGGING")]
			public static void Dictionary(IDictionary dictionary, string name = null, bool suppressExceptions = true)
			{
			}

			[Conditional("ENABLE_LOGGING")]
			public static void ExceptionDetail(Exception exception)
			{
				pIYWguDmZCloYILqODpXfIyPIkhOA pIYWguDmZCloYILqODpXfIyPIkhOA2 = default(pIYWguDmZCloYILqODpXfIyPIkhOA);
				pIYWguDmZCloYILqODpXfIyPIkhOA2.WMXiSAgttNuytVDQGTKPcktHQFkG = new StringBuilder();
				ySzsbjgWgzLkEDsaqzRLQtGzGSNs(exception, 0, ref pIYWguDmZCloYILqODpXfIyPIkhOA2);
			}

			[Conditional("ENABLE_LOGGING")]
			public static void ObjectDetail<T>([CanBeNull] T obj, [CanBeNull] string name = null, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField | BindingFlags.GetProperty, bool suppressExceptions = true)
			{
				string text = KYVyzcvBaDeJPQMRqcfHBGBsJDUSA(name);
				string text3 = default(string);
				FieldInfo[] fields = default(FieldInfo[]);
				Type type = default(Type);
				int num6 = default(int);
				FieldInfo fieldInfo = default(FieldInfo);
				StringBuilder stringBuilder = default(StringBuilder);
				PropertyInfo current = default(PropertyInfo);
				string text2 = default(string);
				while (true)
				{
					int num = -870145787;
					while (true)
					{
						Type type2;
						uint num2;
						switch ((num2 = (uint)(num ^ -1677143126)) % 10)
						{
						case 8u:
							break;
						case 1u:
							text3 = iqklzKRxIqVchztPwUeDFUYOuJDg(obj, suppressExceptions);
							if (obj != null)
							{
								num = ((int)num2 * -1968146254) ^ 0x40AC23A3;
								continue;
							}
							type2 = typeof(T);
							goto IL_017b;
						case 9u:
							fields = type.GetFields(bindingFlags);
							num6 = 0;
							num = ((int)num2 * -309962280) ^ 0x2118B71D;
							continue;
						case 3u:
						{
							int num7;
							if (num6 < fields.Length)
							{
								num = -1818414133;
								num7 = num;
							}
							else
							{
								num = -1278381998;
								num7 = num;
							}
							continue;
						}
						case 5u:
							fieldInfo = fields[num6];
							num = -2050895978;
							continue;
						case 4u:
						{
							string text4 = iqklzKRxIqVchztPwUeDFUYOuJDg(fieldInfo.GetValue(obj), suppressExceptions);
							stringBuilder.KYwPDKVyPbNDcLtELthjLAdBYgKD().OKJDdkEzclsnfAEkvqwoCGdGXadOB(fieldInfo.Name, text4);
							num6++;
							num = ((int)num2 * -2019347218) ^ -2073980915;
							continue;
						}
						case 2u:
							if (obj != null)
							{
								num = (int)(num2 * 676127127) ^ -64936323;
								continue;
							}
							return;
						case 6u:
							stringBuilder = new StringBuilder().AppendLine(text + " (" + type.FullName + ") = " + text3);
							num = ((int)num2 * -1151177225) ^ -587108814;
							continue;
						case 7u:
							type2 = obj.GetType();
							goto IL_017b;
						default:
							{
								IEnumerator<PropertyInfo> enumerator = type.GetProperties(bindingFlags).Where(cxvKGedwrzWugfSXrjKsfQLkqTju<T>._003C_003E9.goCMGCznISCFnKsRkaYVyufuACOFA).GetEnumerator();
								try
								{
									while (true)
									{
										int num3;
										int num4;
										if (!enumerator.MoveNext())
										{
											num3 = -7656862;
											num4 = num3;
										}
										else
										{
											num3 = -547728801;
											num4 = num3;
										}
										while (true)
										{
											switch ((num2 = (uint)(num3 ^ -1677143126)) % 6)
											{
											case 2u:
												num3 = -547728801;
												continue;
											default:
												return;
											case 0u:
												stringBuilder.KYwPDKVyPbNDcLtELthjLAdBYgKD().OKJDdkEzclsnfAEkvqwoCGdGXadOB(current.Name, text2);
												num3 = (int)((num2 * 313218695) ^ 0x4BE9E53B);
												continue;
											case 5u:
												text2 = STSgoOgABHhbDrwoEcGjhtMQRIDkA(obj, suppressExceptions, current);
												num3 = ((int)num2 * -2138594035) ^ 0x2D242A61;
												continue;
											case 3u:
												break;
											case 1u:
												current = enumerator.Current;
												num3 = -496738921;
												continue;
											case 4u:
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
											IL_0260:
											int num5 = -1922004065;
											while (true)
											{
												switch ((num2 = (uint)(num5 ^ -1677143126)) % 3)
												{
												case 0u:
													break;
												default:
													goto end_IL_0265;
												case 2u:
													goto IL_0283;
												case 1u:
													goto end_IL_0265;
												}
												goto IL_0260;
												IL_0283:
												enumerator.Dispose();
												num5 = (int)((num2 * 353266626) ^ 0x4337B274);
												continue;
												end_IL_0265:
												break;
											}
											break;
										}
									}
								}
							}
							IL_017b:
							type = type2;
							num = -81593468;
							continue;
						}
						break;
					}
				}
			}

			private static string STSgoOgABHhbDrwoEcGjhtMQRIDkA<_0001>(_0001 P_0, bool P_1, PropertyInfo P_2)
			{
				ParameterInfo[] indexParameters = P_2.GetIndexParameters();
				string result = default(string);
				try
				{
					result = (indexParameters.Any() ? "[]" : iqklzKRxIqVchztPwUeDFUYOuJDg(P_2.GetValue(P_0), P_1));
				}
				catch (Exception ex)
				{
					while (true)
					{
						IL_002c:
						int num = 234767964;
						while (true)
						{
							uint num2;
							switch ((num2 = (uint)(num ^ 0x64F93988)) % 6)
							{
							case 5u:
								break;
							default:
								goto end_IL_0031;
							case 4u:
							{
								int num3;
								int num4;
								if (!P_1)
								{
									num3 = -125078647;
									num4 = num3;
								}
								else
								{
									num3 = -320179100;
									num4 = num3;
								}
								num = num3 ^ (int)(num2 * 1264282024);
								continue;
							}
							case 0u:
								result = sZzkqPLtzkiGJpliYzPxqoJxqwLN(ex);
								num = ((int)num2 * -1551834771) ^ -893443731;
								continue;
							case 3u:
								throw;
							case 1u:
								num = ((int)num2 * -702541889) ^ -1632038437;
								continue;
							case 2u:
								goto end_IL_0031;
							}
							goto IL_002c;
							continue;
							end_IL_0031:
							break;
						}
						break;
					}
				}
				return result;
			}

			[Conditional("ENABLE_LOGGING")]
			public static void Stack()
			{
				StackFrame[] frames = new StackTrace().GetFrames();
				int num5 = default(int);
				while (true)
				{
					int num = -885484659;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -983613121)) % 8)
						{
						case 0u:
							break;
						default:
							return;
						case 4u:
							num5++;
							num = -564097494;
							continue;
						case 7u:
							return;
						case 6u:
							num = (int)(num2 * 417447568) ^ -1670384310;
							continue;
						case 3u:
							num5 = 1;
							num = -432167047;
							continue;
						case 5u:
						{
							int num6;
							if (num5 < frames.Length)
							{
								num = -974365277;
								num6 = num;
							}
							else
							{
								num = -505670954;
								num6 = num;
							}
							continue;
						}
						case 2u:
						{
							int num3;
							int num4;
							if (frames == null)
							{
								num3 = -579137988;
								num4 = num3;
							}
							else
							{
								num3 = -1399892160;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 735979026);
							continue;
						}
						case 1u:
							return;
						}
						break;
					}
				}
			}

			private static string iqklzKRxIqVchztPwUeDFUYOuJDg<_0001>(_0001 P_0, bool P_1)
			{
				YPkJlOGgBEObnyABSocjgFgpdYRT<_0001> yPkJlOGgBEObnyABSocjgFgpdYRT = new YPkJlOGgBEObnyABSocjgFgpdYRT<_0001>();
				IEnumerable enumerable = default(IEnumerable);
				string text = default(string);
				string result = default(string);
				while (true)
				{
					int num = 1480715537;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x6BEC3457)) % 11)
						{
						case 0u:
							break;
						case 5u:
							num = ((int)num2 * -1917897278) ^ 0x1D4BE4D1;
							continue;
						case 9u:
						{
							int num6;
							int num7;
							if (enumerable != null)
							{
								num6 = -821674042;
								num7 = num6;
							}
							else
							{
								num6 = -1554321233;
								num7 = num6;
							}
							num = num6 ^ ((int)num2 * -693257846);
							continue;
						}
						case 10u:
							text = P_0 as string;
							num = 643121942;
							continue;
						case 7u:
							return hhsmxILKFTzAhZXHRzOdohumZUAG<_0001>(string.Join(", ", enumerable.Cast<object>().Select(yPkJlOGgBEObnyABSocjgFgpdYRT.vRqxzLFcPDxRjUnJZPhSyQiXFlSy)), "[{0}]");
						case 8u:
						{
							int num10;
							int num11;
							if (text != null)
							{
								num10 = 622352722;
								num11 = num10;
							}
							else
							{
								num10 = 1455233821;
								num11 = num10;
							}
							num = num10 ^ ((int)num2 * -992747635);
							continue;
						}
						case 4u:
							return "null";
						case 3u:
						{
							yPkJlOGgBEObnyABSocjgFgpdYRT.YhVNsWPfFneJmahzKxabibzeEhXfA = P_1;
							int num8;
							int num9;
							if (P_0 == null)
							{
								num8 = 183807091;
								num9 = num8;
							}
							else
							{
								num8 = 1625676644;
								num9 = num8;
							}
							num = num8 ^ (int)(num2 * 1586543219);
							continue;
						}
						case 2u:
							return text;
						case 1u:
							enumerable = P_0 as IEnumerable;
							num = ((int)num2 * -1428483523) ^ -1264300820;
							continue;
						default:
							try
							{
								result = P_0.ToString();
							}
							catch (Exception ex)
							{
								while (true)
								{
									IL_0169:
									int num3 = 860873371;
									while (true)
									{
										switch ((num2 = (uint)(num3 ^ 0x6BEC3457)) % 5)
										{
										case 2u:
											break;
										case 4u:
										{
											int num4;
											int num5;
											if (yPkJlOGgBEObnyABSocjgFgpdYRT.YhVNsWPfFneJmahzKxabibzeEhXfA)
											{
												num4 = 114453559;
												num5 = num4;
											}
											else
											{
												num4 = 1332498803;
												num5 = num4;
											}
											num3 = num4 ^ (int)(num2 * 1220604453);
											continue;
										}
										case 0u:
											result = sZzkqPLtzkiGJpliYzPxqoJxqwLN(ex);
											num3 = (int)(num2 * 2133876180) ^ -1785353066;
											continue;
										case 1u:
											goto end_IL_016e;
										default:
											throw;
										}
										goto IL_0169;
										continue;
										end_IL_016e:
										break;
									}
									break;
								}
							}
							return result;
						}
						break;
					}
				}
			}

			private static string SNyaMyFPDdWvICEwhYDQfuDiivT()
			{
				StackFrame frame = new StackTrace().GetFrame(2);
				Type declaringType = default(Type);
				MethodBase method = default(MethodBase);
				string text = default(string);
				while (true)
				{
					int num = -1002790445;
					while (true)
					{
						uint num2;
						object obj;
						switch ((num2 = (uint)(num ^ -212734007)) % 5)
						{
						case 2u:
							break;
						case 3u:
							obj = declaringType.FullName;
							goto IL_0044;
						case 4u:
							declaringType = method.DeclaringType;
							if (declaringType == null)
							{
								obj = "[No declaring type]";
								goto IL_0044;
							}
							num = (int)((num2 * 1634865908) ^ 0x436CF6D9);
							continue;
						case 1u:
							method = frame.GetMethod();
							num = (int)(num2 * 205919932) ^ -664800025;
							continue;
						default:
							{
								return $"{text}.{method.Name} in {frame.GetFileName()}[{frame.GetFileLineNumber()}]";
							}
							IL_0044:
							text = (string)obj;
							num = -1824260710;
							continue;
						}
						break;
					}
				}
			}

			internal static string djJrnTBLvPLsuocnbpCJaDSmmhpF<_0001, _0002>(_0001 P_0, _0002 P_1, bool P_2 = true)
			{
				return iqklzKRxIqVchztPwUeDFUYOuJDg(P_0, P_2) + " = " + iqklzKRxIqVchztPwUeDFUYOuJDg(P_1, P_2);
			}

			[CompilerGenerated]
			internal static void ySzsbjgWgzLkEDsaqzRLQtGzGSNs(Exception P_0, int P_1, ref pIYWguDmZCloYILqODpXfIyPIkhOA P_2)
			{
				P_2.WMXiSAgttNuytVDQGTKPcktHQFkG.KYwPDKVyPbNDcLtELthjLAdBYgKD(P_1).Append(P_0.GetType().FullName).Append(": ")
					.AppendLine(P_0.Message);
				AggregateException ex = default(AggregateException);
				while (true)
				{
					int num = 1926009683;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x6A4F2BA5)) % 7)
						{
						case 0u:
							break;
						case 1u:
						{
							int num7;
							int num8;
							if (P_0.InnerException == null)
							{
								num7 = 319787098;
								num8 = num7;
							}
							else
							{
								num7 = 1009955519;
								num8 = num7;
							}
							num = num7 ^ ((int)num2 * -1250565680);
							continue;
						}
						case 3u:
							ex = P_0 as AggregateException;
							num = 867940353;
							continue;
						case 5u:
							P_2.WMXiSAgttNuytVDQGTKPcktHQFkG.KYwPDKVyPbNDcLtELthjLAdBYgKD(P_1 + 1).AppendLine("with inner exception:");
							ySzsbjgWgzLkEDsaqzRLQtGzGSNs(P_0.InnerException, P_1 + 1, ref P_2);
							num = ((int)num2 * -1723568749) ^ 0x7FEF0034;
							continue;
						case 2u:
							P_2.WMXiSAgttNuytVDQGTKPcktHQFkG.KYwPDKVyPbNDcLtELthjLAdBYgKD(P_1 + 1).AppendLine("with inner exceptions:");
							num = (int)(num2 * 1863587164) ^ -1692199068;
							continue;
						case 6u:
							if (ex != null)
							{
								num = ((int)num2 * -1291607857) ^ -1983936993;
								continue;
							}
							goto IL_01ee;
						default:
							{
								int num3 = 0;
								IEnumerator<Exception> enumerator = ex.InnerExceptions.GetEnumerator();
								try
								{
									while (true)
									{
										IL_0195:
										int num4;
										int num5;
										if (!enumerator.MoveNext())
										{
											num4 = 480013361;
											num5 = num4;
										}
										else
										{
											num4 = 575743190;
											num5 = num4;
										}
										while (true)
										{
											switch ((num2 = (uint)(num4 ^ 0x6A4F2BA5)) % 5)
											{
											case 4u:
												num4 = 575743190;
												continue;
											default:
												goto end_IL_0128;
											case 2u:
											{
												Exception current = enumerator.Current;
												P_2.WMXiSAgttNuytVDQGTKPcktHQFkG.KYwPDKVyPbNDcLtELthjLAdBYgKD(P_1 + 1).Append(num3).Append(": ");
												ySzsbjgWgzLkEDsaqzRLQtGzGSNs(current, P_1 + 1, ref P_2);
												num4 = 1847907002;
												continue;
											}
											case 1u:
												num3++;
												num4 = (int)((num2 * 1679605173) ^ 0x1FA8BDA);
												continue;
											case 0u:
												break;
											case 3u:
												goto end_IL_0128;
											}
											goto IL_0195;
											continue;
											end_IL_0128:
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
											IL_01b6:
											int num6 = 94986946;
											while (true)
											{
												switch ((num2 = (uint)(num6 ^ 0x6A4F2BA5)) % 3)
												{
												case 2u:
													break;
												default:
													goto end_IL_01bb;
												case 1u:
													goto IL_01d8;
												case 0u:
													goto end_IL_01bb;
												}
												goto IL_01b6;
												IL_01d8:
												enumerator.Dispose();
												num6 = (int)((num2 * 1038288240) ^ 0x1D4D0172);
												continue;
												end_IL_01bb:
												break;
											}
											break;
										}
									}
								}
								goto IL_01ee;
							}
							IL_01ee:
							P_2.WMXiSAgttNuytVDQGTKPcktHQFkG.AppendLine("Source: " + P_0.Source).AppendLine(P_0.StackTrace);
							return;
						}
						break;
					}
				}
			}

			[CompilerGenerated]
			internal static string hhsmxILKFTzAhZXHRzOdohumZUAG<_0001>(string P_0, string P_1)
			{
				return string.Format(P_1, P_0);
			}
		}

		public const string EnableLoggingDefine = "ENABLE_LOGGING";

		private const string XwxhZMybFQpViYQPlsPwyWcSokvb = "\t";

		private const string NdKgzGdLgPPFvpKDszEshkmNtNdW = "null";

		private const string ktikZBxNYawcJmBMEEoDVGifLyUq = "???";

		private static readonly IList<ILogWriter> RTtlvMkXoYEtJspIFYPopSMCoxAm = new List<ILogWriter>();

		[CompilerGenerated]
		private static LogSeverity WTfRWkyPZEijyBMVuOVuFbVMTuNB = LogSeverity.Info;

		public static LogSeverity Verbosity
		{
			[CompilerGenerated]
			get
			{
				return WTfRWkyPZEijyBMVuOVuFbVMTuNB;
			}
			[CompilerGenerated]
			set
			{
				WTfRWkyPZEijyBMVuOVuFbVMTuNB = value;
			}
		}

		public static void RegisterLogWriter(ILogWriter writer)
		{
			RTtlvMkXoYEtJspIFYPopSMCoxAm.Add(writer);
		}

		public static void DeregisterLogWriter(ILogWriter writer)
		{
			RTtlvMkXoYEtJspIFYPopSMCoxAm.Remove(writer);
		}

		[Conditional("ENABLE_LOGGING")]
		public static void Info(string message)
		{
			if (Verbosity > LogSeverity.Info)
			{
				while (true)
				{
					uint num;
					switch ((num = 1676768392u) % 3)
					{
					case 0u:
						continue;
					case 1u:
						return;
					}
					break;
				}
			}
			IEnumerator<ILogWriter> enumerator = RTtlvMkXoYEtJspIFYPopSMCoxAm.GetEnumerator();
			try
			{
				while (true)
				{
					int num2;
					int num3;
					if (!enumerator.MoveNext())
					{
						num2 = 1275534826;
						num3 = num2;
					}
					else
					{
						num2 = 1662250724;
						num3 = num2;
					}
					while (true)
					{
						uint num;
						switch ((num = (uint)(num2 ^ 0x3F44B819)) % 4)
						{
						case 2u:
							num2 = 1662250724;
							continue;
						default:
							return;
						case 1u:
							enumerator.Current.Write(LogSeverity.Info, message);
							num2 = 147936065;
							continue;
						case 0u:
							break;
						case 3u:
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
						IL_00a1:
						int num4 = 2120207376;
						while (true)
						{
							uint num;
							switch ((num = (uint)(num4 ^ 0x3F44B819)) % 3)
							{
							case 0u:
								break;
							default:
								goto end_IL_00a6;
							case 1u:
								goto IL_00c3;
							case 2u:
								goto end_IL_00a6;
							}
							goto IL_00a1;
							IL_00c3:
							enumerator.Dispose();
							num4 = (int)((num * 1008161080) ^ 0x38CF4DED);
							continue;
							end_IL_00a6:
							break;
						}
						break;
					}
				}
			}
		}

		[Conditional("ENABLE_LOGGING")]
		[StringFormatMethod("message")]
		public static void Info(string message, params object[] args)
		{
			if (Verbosity > LogSeverity.Info)
			{
				while (true)
				{
					uint num;
					switch ((num = 2025981352u) % 3)
					{
					case 0u:
						continue;
					case 1u:
						return;
					}
					break;
				}
			}
			IEnumerator<ILogWriter> enumerator = RTtlvMkXoYEtJspIFYPopSMCoxAm.GetEnumerator();
			try
			{
				while (true)
				{
					int num2;
					int num3;
					if (!enumerator.MoveNext())
					{
						num2 = -1154142705;
						num3 = num2;
					}
					else
					{
						num2 = -1327228382;
						num3 = num2;
					}
					while (true)
					{
						uint num;
						switch ((num = (uint)(num2 ^ -1514960748)) % 4)
						{
						case 0u:
							num2 = -1327228382;
							continue;
						default:
							return;
						case 2u:
							enumerator.Current.Write(LogSeverity.Info, string.Format(message, args));
							num2 = -1463768843;
							continue;
						case 1u:
							break;
						case 3u:
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
						IL_00a7:
						int num4 = -305072746;
						while (true)
						{
							uint num;
							switch ((num = (uint)(num4 ^ -1514960748)) % 3)
							{
							case 0u:
								break;
							default:
								goto end_IL_00ac;
							case 1u:
								goto IL_00c9;
							case 2u:
								goto end_IL_00ac;
							}
							goto IL_00a7;
							IL_00c9:
							enumerator.Dispose();
							num4 = (int)(num * 901068308) ^ -746250895;
							continue;
							end_IL_00ac:
							break;
						}
						break;
					}
				}
			}
		}

		[Conditional("ENABLE_LOGGING")]
		public static void Warning(string message)
		{
			if (Verbosity > LogSeverity.Warning)
			{
				while (true)
				{
					uint num;
					switch ((num = 2061745630u) % 3)
					{
					case 0u:
						continue;
					case 1u:
						return;
					}
					break;
				}
			}
			IEnumerator<ILogWriter> enumerator = RTtlvMkXoYEtJspIFYPopSMCoxAm.GetEnumerator();
			try
			{
				while (true)
				{
					int num2;
					int num3;
					if (enumerator.MoveNext())
					{
						num2 = 369721059;
						num3 = num2;
					}
					else
					{
						num2 = 982495353;
						num3 = num2;
					}
					while (true)
					{
						uint num;
						switch ((num = (uint)(num2 ^ 0x14B22F5E)) % 4)
						{
						case 0u:
							num2 = 369721059;
							continue;
						default:
							return;
						case 1u:
							enumerator.Current.Write(LogSeverity.Warning, message);
							num2 = 1188807260;
							continue;
						case 2u:
							break;
						case 3u:
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
						IL_00a1:
						int num4 = 833958464;
						while (true)
						{
							uint num;
							switch ((num = (uint)(num4 ^ 0x14B22F5E)) % 3)
							{
							case 0u:
								break;
							default:
								goto end_IL_00a6;
							case 1u:
								goto IL_00c3;
							case 2u:
								goto end_IL_00a6;
							}
							goto IL_00a1;
							IL_00c3:
							enumerator.Dispose();
							num4 = ((int)num * -596328976) ^ -474211270;
							continue;
							end_IL_00a6:
							break;
						}
						break;
					}
				}
			}
		}

		[Conditional("ENABLE_LOGGING")]
		[StringFormatMethod("message")]
		public static void Warning(string message, params object[] args)
		{
			if (Verbosity > LogSeverity.Warning)
			{
				while (true)
				{
					uint num;
					switch ((num = 188893864u) % 3)
					{
					case 0u:
						continue;
					case 1u:
						return;
					}
					break;
				}
			}
			IEnumerator<ILogWriter> enumerator = RTtlvMkXoYEtJspIFYPopSMCoxAm.GetEnumerator();
			try
			{
				while (true)
				{
					int num2;
					int num3;
					if (enumerator.MoveNext())
					{
						num2 = -1987663181;
						num3 = num2;
					}
					else
					{
						num2 = -827085540;
						num3 = num2;
					}
					while (true)
					{
						uint num;
						switch ((num = (uint)(num2 ^ -1639627126)) % 4)
						{
						case 0u:
							num2 = -1987663181;
							continue;
						default:
							return;
						case 1u:
							enumerator.Current.Write(LogSeverity.Warning, string.Format(message, args));
							num2 = -1856367907;
							continue;
						case 3u:
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
						IL_00a7:
						int num4 = -1820713799;
						while (true)
						{
							uint num;
							switch ((num = (uint)(num4 ^ -1639627126)) % 3)
							{
							case 2u:
								break;
							default:
								goto end_IL_00ac;
							case 1u:
								goto IL_00c9;
							case 0u:
								goto end_IL_00ac;
							}
							goto IL_00a7;
							IL_00c9:
							enumerator.Dispose();
							num4 = ((int)num * -258850839) ^ 0x5FBFF327;
							continue;
							end_IL_00ac:
							break;
						}
						break;
					}
				}
			}
		}

		[Conditional("ENABLE_LOGGING")]
		public static void Error(string message)
		{
			if (Verbosity > LogSeverity.Error)
			{
				while (true)
				{
					uint num;
					switch ((num = 1941803716u) % 3)
					{
					case 0u:
						continue;
					case 1u:
						return;
					}
					break;
				}
			}
			IEnumerator<ILogWriter> enumerator = RTtlvMkXoYEtJspIFYPopSMCoxAm.GetEnumerator();
			try
			{
				while (true)
				{
					int num2;
					int num3;
					if (!enumerator.MoveNext())
					{
						num2 = -244541219;
						num3 = num2;
					}
					else
					{
						num2 = -626208134;
						num3 = num2;
					}
					while (true)
					{
						uint num;
						switch ((num = (uint)(num2 ^ -1808469573)) % 4)
						{
						case 3u:
							num2 = -626208134;
							continue;
						default:
							return;
						case 1u:
							enumerator.Current.Write(LogSeverity.Error, message);
							num2 = -1955748029;
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
						IL_00a1:
						int num4 = -917510158;
						while (true)
						{
							uint num;
							switch ((num = (uint)(num4 ^ -1808469573)) % 3)
							{
							case 0u:
								break;
							default:
								goto end_IL_00a6;
							case 2u:
								goto IL_00c3;
							case 1u:
								goto end_IL_00a6;
							}
							goto IL_00a1;
							IL_00c3:
							enumerator.Dispose();
							num4 = (int)(num * 1830165931) ^ -866550849;
							continue;
							end_IL_00a6:
							break;
						}
						break;
					}
				}
			}
		}

		[StringFormatMethod("message")]
		[Conditional("ENABLE_LOGGING")]
		public static void Error(string message, params object[] args)
		{
			if (Verbosity > LogSeverity.Error)
			{
				while (true)
				{
					uint num;
					switch ((num = 748978849u) % 3)
					{
					case 0u:
						continue;
					case 1u:
						return;
					}
					break;
				}
			}
			IEnumerator<ILogWriter> enumerator = RTtlvMkXoYEtJspIFYPopSMCoxAm.GetEnumerator();
			try
			{
				while (true)
				{
					int num2;
					int num3;
					if (!enumerator.MoveNext())
					{
						num2 = 597742624;
						num3 = num2;
					}
					else
					{
						num2 = 314091331;
						num3 = num2;
					}
					while (true)
					{
						uint num;
						switch ((num = (uint)(num2 ^ 0x25AC53C6)) % 4)
						{
						case 3u:
							num2 = 314091331;
							continue;
						default:
							return;
						case 1u:
							enumerator.Current.Write(LogSeverity.Error, string.Format(message, args));
							num2 = 615148718;
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
						IL_00a7:
						int num4 = 66808708;
						while (true)
						{
							uint num;
							switch ((num = (uint)(num4 ^ 0x25AC53C6)) % 3)
							{
							case 2u:
								break;
							default:
								goto end_IL_00ac;
							case 1u:
								goto IL_00c9;
							case 0u:
								goto end_IL_00ac;
							}
							goto IL_00a7;
							IL_00c9:
							enumerator.Dispose();
							num4 = (int)(num * 1719746944) ^ -23568310;
							continue;
							end_IL_00ac:
							break;
						}
						break;
					}
				}
			}
		}

		[Conditional("ENABLE_LOGGING")]
		public static void Exception(Exception ex)
		{
			if (Verbosity > LogSeverity.Error)
			{
				while (true)
				{
					uint num;
					switch ((num = 345943643u) % 3)
					{
					case 0u:
						continue;
					case 2u:
						return;
					}
					break;
				}
			}
			IEnumerator<ILogWriter> enumerator = RTtlvMkXoYEtJspIFYPopSMCoxAm.GetEnumerator();
			try
			{
				while (true)
				{
					int num2;
					int num3;
					if (!enumerator.MoveNext())
					{
						num2 = -1408348654;
						num3 = num2;
					}
					else
					{
						num2 = -1825353605;
						num3 = num2;
					}
					while (true)
					{
						uint num;
						switch ((num = (uint)(num2 ^ -1517332107)) % 4)
						{
						case 0u:
							num2 = -1825353605;
							continue;
						default:
							return;
						case 2u:
							enumerator.Current.WriteException(ex);
							num2 = -1591046936;
							continue;
						case 1u:
							break;
						case 3u:
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
						IL_009f:
						int num4 = -761979611;
						while (true)
						{
							uint num;
							switch ((num = (uint)(num4 ^ -1517332107)) % 3)
							{
							case 0u:
								break;
							default:
								goto end_IL_00a4;
							case 2u:
								goto IL_00c1;
							case 1u:
								goto end_IL_00a4;
							}
							goto IL_009f;
							IL_00c1:
							enumerator.Dispose();
							num4 = ((int)num * -2099517122) ^ 0x26E370DC;
							continue;
							end_IL_00a4:
							break;
						}
						break;
					}
				}
			}
		}

		private static StringBuilder KYwPDKVyPbNDcLtELthjLAdBYgKD(this StringBuilder P_0, int P_1 = 1)
		{
			int num = 0;
			while (true)
			{
				int num2;
				int num3;
				if (num < P_1)
				{
					num2 = 1650593703;
					num3 = num2;
				}
				else
				{
					num2 = 1667280466;
					num3 = num2;
				}
				while (true)
				{
					uint num4;
					switch ((num4 = (uint)(num2 ^ 0x7B7DBB24)) % 4)
					{
					case 0u:
						num2 = 1650593703;
						continue;
					case 3u:
						P_0.Append("\t");
						num++;
						num2 = 1727503129;
						continue;
					case 1u:
						break;
					default:
						return P_0;
					}
					break;
				}
			}
		}

		private static StringBuilder OKJDdkEzclsnfAEkvqwoCGdGXadOB<_0001>(this StringBuilder P_0, string P_1, _0001 P_2)
		{
			return P_0.AppendLine(Trace.djJrnTBLvPLsuocnbpCJaDSmmhpF(P_1, P_2));
		}

		private static string KYVyzcvBaDeJPQMRqcfHBGBsJDUSA(string P_0)
		{
			if (!string.IsNullOrEmpty(P_0))
			{
				while (true)
				{
					uint num;
					switch ((num = 825960910u) % 3)
					{
					case 0u:
						continue;
					case 1u:
						return P_0;
					}
					break;
				}
			}
			return "???";
		}

		private static string sZzkqPLtzkiGJpliYzPxqoJxqwLN(Exception P_0)
		{
			return "Exception: " + P_0.Message;
		}
	}
}
