using System;
using System.Collections.Generic;
using System.Linq;
using BitCode.Attributes;
using IfbHfNncEbjZtVkjvFPZBBYcpxLmA;

namespace BitCode.Debug.Commands
{
	public static class DebugConsoleCommands
	{
		[Serializable]
		private sealed class zhzuCEXyYSJokcxwneoOptcywCwm
		{
			public static readonly zhzuCEXyYSJokcxwneoOptcywCwm _003C_003E9 = new zhzuCEXyYSJokcxwneoOptcywCwm();

			public static Func<kpOqttWobXfHSksnDwvNsfAmsgdlA, Type> _003C_003E9__0_0;

			internal Type fLocwGJPbrlvkhtXAlSnvusxfNUE(kpOqttWobXfHSksnDwvNsfAmsgdlA P_0)
			{
				return P_0.MzGbjYSINAlUiDjmRZYCXhbDKeTh;
			}
		}

		[DebugCommand(Description = "Display this help message.")]
		public static void Help(DebugConsole console, IDebugConsoleWriter writer, bool all = false)
		{
			writer.AppendLine("24 Bit Games Debug Console");
			kpOqttWobXfHSksnDwvNsfAmsgdlA current2 = default(kpOqttWobXfHSksnDwvNsfAmsgdlA);
			while (true)
			{
				int num = 1900422763;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x2E1BBDF0)) % 4)
					{
					case 0u:
						break;
					case 3u:
						writer.AppendLine("Current context: " + console.FormatContextAsPrettyString());
						num = ((int)num2 * -1745054961) ^ -1302875217;
						continue;
					case 2u:
						writer.AppendLine("---");
						num = (int)((num2 * 736412490) ^ 0x1427C089);
						continue;
					default:
					{
						writer.AppendLine("Available commands for given context:");
						IEnumerator<IGrouping<Type, kpOqttWobXfHSksnDwvNsfAmsgdlA>> enumerator = console.RBTZeEcYCyedGrhpvgXrfqvmjDzW.GroupBy(zhzuCEXyYSJokcxwneoOptcywCwm._003C_003E9.fLocwGJPbrlvkhtXAlSnvusxfNUE).GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								while (true)
								{
									IGrouping<Type, kpOqttWobXfHSksnDwvNsfAmsgdlA> current = enumerator.Current;
									int num3 = 595224097;
									while (true)
									{
										switch ((num2 = (uint)(num3 ^ 0x2E1BBDF0)) % 8)
										{
										case 3u:
											num3 = 643069189;
											continue;
										case 0u:
										{
											int num6;
											int num7;
											if (!all)
											{
												num6 = 881882391;
												num7 = num6;
											}
											else
											{
												num6 = 1572814034;
												num7 = num6;
											}
											num3 = num6 ^ ((int)num2 * -913652924);
											continue;
										}
										case 4u:
											writer.AppendLine($"\n{current.Key}\n---");
											num3 = 1164028094;
											continue;
										case 7u:
											break;
										case 5u:
											goto end_IL_00b8;
										case 2u:
											writer.AppendLine("\nGlobal commands\n---");
											num3 = 1164028094;
											continue;
										case 1u:
										{
											int num4;
											int num5;
											if (current.Key == typeof(void))
											{
												num4 = 354062934;
												num5 = num4;
											}
											else
											{
												num4 = 593866258;
												num5 = num4;
											}
											num3 = num4 ^ (int)(num2 * 1117185246);
											continue;
										}
										default:
											goto IL_019f;
										}
										if (console.Context != null)
										{
											goto end_IL_0145;
										}
										num3 = ((int)num2 * -1495628972) ^ 0x3A5CFDFE;
										continue;
										IL_019f:
										IEnumerator<kpOqttWobXfHSksnDwvNsfAmsgdlA> enumerator2 = current.GetEnumerator();
										try
										{
											while (true)
											{
												IL_01e1:
												int num8;
												int num9;
												if (enumerator2.MoveNext())
												{
													num8 = 136060669;
													num9 = num8;
												}
												else
												{
													num8 = 307567684;
													num9 = num8;
												}
												while (true)
												{
													switch ((num2 = (uint)(num8 ^ 0x2E1BBDF0)) % 5)
													{
													case 0u:
														num8 = 136060669;
														continue;
													default:
														goto end_IL_01ad;
													case 1u:
														current2 = enumerator2.Current;
														num8 = 472632579;
														continue;
													case 2u:
														break;
													case 3u:
														writer.AppendLine(current2.ToString());
														num8 = (int)((num2 * 1734451907) ^ 0x4D8BDE66);
														continue;
													case 4u:
														goto end_IL_01ad;
													}
													goto IL_01e1;
													continue;
													end_IL_01ad:
													break;
												}
												break;
											}
										}
										finally
										{
											if (enumerator2 != null)
											{
												while (true)
												{
													IL_0222:
													int num10 = 252806909;
													while (true)
													{
														switch ((num2 = (uint)(num10 ^ 0x2E1BBDF0)) % 3)
														{
														case 2u:
															break;
														default:
															goto end_IL_0227;
														case 1u:
															goto IL_0245;
														case 0u:
															goto end_IL_0227;
														}
														goto IL_0222;
														IL_0245:
														enumerator2.Dispose();
														num10 = ((int)num2 * -800632043) ^ -214043983;
														continue;
														end_IL_0227:
														break;
													}
													break;
												}
											}
										}
										goto end_IL_0145;
										continue;
										end_IL_00b8:
										break;
									}
									continue;
									end_IL_0145:
									break;
								}
							}
							return;
						}
						finally
						{
							if (enumerator != null)
							{
								while (true)
								{
									IL_026c:
									int num11 = 443423333;
									while (true)
									{
										switch ((num2 = (uint)(num11 ^ 0x2E1BBDF0)) % 3)
										{
										case 0u:
											break;
										default:
											goto end_IL_0271;
										case 2u:
											goto IL_028f;
										case 1u:
											goto end_IL_0271;
										}
										goto IL_026c;
										IL_028f:
										enumerator.Dispose();
										num11 = ((int)num2 * -72263639) ^ -1171981464;
										continue;
										end_IL_0271:
										break;
									}
									break;
								}
							}
						}
					}
					}
					break;
				}
			}
		}

		[DebugCommand(Description = "Print a message to the debug console.")]
		public static string Echo(string message)
		{
			return message;
		}

		[DebugCommand(Description = "Pop the current context from the context stack.")]
		public static void Pop(DebugConsole console, IDebugConsoleWriter writer)
		{
			if (console.Context == null)
			{
				goto IL_000b;
			}
			goto IL_00b4;
			IL_000b:
			int num = -426425961;
			goto IL_0010;
			IL_0010:
			object context = default(object);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -831012438)) % 7)
				{
				case 3u:
					break;
				default:
					return;
				case 6u:
				{
					int num3;
					int num4;
					if (console.Context != null)
					{
						num3 = -1775516425;
						num4 = num3;
					}
					else
					{
						num3 = -1310770116;
						num4 = num3;
					}
					num = num3 ^ ((int)num2 * -1771912020);
					continue;
				}
				case 2u:
					writer.AppendLine(console.FormatContextAsPrettyString());
					num = ((int)num2 * -1503841819) ^ 0x18A8BF65;
					continue;
				case 1u:
					return;
				case 4u:
					writer.AppendLine("Previous context was " + DebugConsole.FormatContextAsPrettyString(context));
					num = (int)((num2 * 1237673163) ^ 0x654CF730);
					continue;
				case 5u:
					goto IL_00b4;
				case 0u:
					return;
				}
				break;
			}
			goto IL_000b;
			IL_00b4:
			context = console.PopContext();
			num = -1813330699;
			goto IL_0010;
		}

		[DebugCommand(Description = "Prints out the current context.")]
		public static void PrintContext(DebugConsole console, IDebugConsoleWriter writer)
		{
			writer.AppendLine(console.FormatContextAsPrettyString());
		}

		[DebugCommand(Description = "Clear entire context stack.")]
		public static void ClearContext(DebugConsole console)
		{
			console.ClearContext();
		}

		[DebugCommand(Description = "Pushes the given type onto the context stack.")]
		public static Type FindType(Type type)
		{
			return type;
		}
	}
}
