using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using BitCode.Debug.MemberWrappers;
using BitCode.Debug.TokenResolvers;
using DdQbeCzwvEdCSCHcDJqhScymDgUBA;
using IfbHfNncEbjZtVkjvFPZBBYcpxLmA;
using JetBrains.Annotations;

namespace BitCode.Debug
{
	public class DebugConsole
	{
		private struct WwYyVFZaXzykaQdWUmoZZoAHZoPx
		{
			public readonly List<kpOqttWobXfHSksnDwvNsfAmsgdlA> jDUzYeBuTXnMjCJgreCCcLjGOaE;

			public readonly int yLbGQwLIqgdnnnNSoaNUwijyOnro;

			public WwYyVFZaXzykaQdWUmoZZoAHZoPx(List<kpOqttWobXfHSksnDwvNsfAmsgdlA> P_0, int P_1)
			{
				yLbGQwLIqgdnnnNSoaNUwijyOnro = P_1;
				jDUzYeBuTXnMjCJgreCCcLjGOaE = P_0;
			}
		}

		private sealed class wECsozKlnLbEXuvcHskRiPOjPWLM
		{
			public string ujaBylKryXcTFfKZBuxuEHEakPzeA;

			public DebugConsole XjfEdSSApFhbGfqzDRkWbLdpquul;

			internal bool QzuphlZEyDWZIYpwiphMxNpltwKA(kpOqttWobXfHSksnDwvNsfAmsgdlA P_0)
			{
				return string.Equals(P_0.YBLwrmnGpuNNorfGBIOCDXiTtNWV, ujaBylKryXcTFfKZBuxuEHEakPzeA, XjfEdSSApFhbGfqzDRkWbLdpquul.CccFWUBFKPdDOQSwkUizAdgNPkkg);
			}
		}

		[Serializable]
		private sealed class zhzuCEXyYSJokcxwneoOptcywCwm
		{
			public static readonly zhzuCEXyYSJokcxwneoOptcywCwm _003C_003E9 = new zhzuCEXyYSJokcxwneoOptcywCwm();

			public static Func<KeyValuePair<Type, List<kpOqttWobXfHSksnDwvNsfAmsgdlA>>, IEnumerable<kpOqttWobXfHSksnDwvNsfAmsgdlA>> _003C_003E9__42_0;

			public static Func<WwYyVFZaXzykaQdWUmoZZoAHZoPx, int> _003C_003E9__50_2;

			public static Func<WwYyVFZaXzykaQdWUmoZZoAHZoPx, IEnumerable<kpOqttWobXfHSksnDwvNsfAmsgdlA>> _003C_003E9__50_3;

			internal IEnumerable<kpOqttWobXfHSksnDwvNsfAmsgdlA> IKjlJliHwJHoSEYfOrvQoBmkOcHP(KeyValuePair<Type, List<kpOqttWobXfHSksnDwvNsfAmsgdlA>> P_0)
			{
				return P_0.Value;
			}

			internal int hveDIWTaDJhTcuOzZMxyhMUTalWu(WwYyVFZaXzykaQdWUmoZZoAHZoPx P_0)
			{
				return P_0.yLbGQwLIqgdnnnNSoaNUwijyOnro;
			}

			internal IEnumerable<kpOqttWobXfHSksnDwvNsfAmsgdlA> HWNHbCHnukuOiIVznstqhbkExEmS(WwYyVFZaXzykaQdWUmoZZoAHZoPx P_0)
			{
				return P_0.jDUzYeBuTXnMjCJgreCCcLjGOaE;
			}
		}

		internal const string lJQpgWZjsORrqOWxzEcCnhHcdVkCA = "24 Bit Games Debug Console";

		private readonly Stack<object> BSzOQFkMsjYVCmQniVliDzfGYJEJ = new Stack<object>();

		private readonly Dictionary<Type, List<kpOqttWobXfHSksnDwvNsfAmsgdlA>> CwpqVicEiBjmplmwKuvdekMpeqKM = new Dictionary<Type, List<kpOqttWobXfHSksnDwvNsfAmsgdlA>>();

		private readonly List<kpOqttWobXfHSksnDwvNsfAmsgdlA> BwbiKvwDrrefyiDfeMmhhQBAlCSgb = new List<kpOqttWobXfHSksnDwvNsfAmsgdlA>();

		private readonly StringComparison CccFWUBFKPdDOQSwkUizAdgNPkkg;

		private readonly QEaanedCObJjKPrcXjEAOXtRjqeO mmLSzUqVTcMGsUmmXXncQLusiNCg = new BfQDjKlCtjVjTdbJCDBiEejLgcxN();

		[CompilerGenerated]
		private Exception MvuBOPqfTHSuUZJsVfnUcQmeVmKJ;

		[CompilerGenerated]
		private Type ukKUFpfkWKLFDAjGeGGMGkqRbvFC;

		[CompilerGenerated]
		private bool QEorgnppTbQnENanYGZiHBCxgCBVA;

		internal readonly List<Assembly> voBRoFFzKynOBYQnDLxHHntxPWlf = new List<Assembly>();

		internal readonly StringBuilder LpGYZHLDdDpelSxCYmKYgtKIpsCy = new StringBuilder();

		[CanBeNull]
		public Exception LastCommandException
		{
			[CompilerGenerated]
			get
			{
				return MvuBOPqfTHSuUZJsVfnUcQmeVmKJ;
			}
			[CompilerGenerated]
			private set
			{
				MvuBOPqfTHSuUZJsVfnUcQmeVmKJ = mvuBOPqfTHSuUZJsVfnUcQmeVmKJ;
			}
		}

		[CanBeNull]
		public object Context
		{
			get
			{
				if (BSzOQFkMsjYVCmQniVliDzfGYJEJ.Count <= 0)
				{
					while (true)
					{
						uint num;
						switch ((num = 879658945u) % 3)
						{
						case 0u:
							continue;
						case 1u:
							return null;
						}
						break;
					}
				}
				return BSzOQFkMsjYVCmQniVliDzfGYJEJ.Peek();
			}
		}

		public int ContextCount => BSzOQFkMsjYVCmQniVliDzfGYJEJ.Count;

		[CanBeNull]
		public Type ContextType
		{
			[CompilerGenerated]
			get
			{
				return ukKUFpfkWKLFDAjGeGGMGkqRbvFC;
			}
			[CompilerGenerated]
			private set
			{
				ukKUFpfkWKLFDAjGeGGMGkqRbvFC = type;
			}
		}

		public bool IsEnumerableContext
		{
			[CompilerGenerated]
			get
			{
				return QEorgnppTbQnENanYGZiHBCxgCBVA;
			}
			[CompilerGenerated]
			private set
			{
				QEorgnppTbQnENanYGZiHBCxgCBVA = qEorgnppTbQnENanYGZiHBCxgCBVA;
			}
		}

		internal List<kpOqttWobXfHSksnDwvNsfAmsgdlA> RBTZeEcYCyedGrhpvgXrfqvmjDzW => BwbiKvwDrrefyiDfeMmhhQBAlCSgb;

		internal QEaanedCObJjKPrcXjEAOXtRjqeO BfQDjKlCtjVjTdbJCDBiEejLgcxN => mmLSzUqVTcMGsUmmXXncQLusiNCg;

		public DebugConsole(StringComparison commandNameMatchingMode = StringComparison.InvariantCultureIgnoreCase)
		{
			CccFWUBFKPdDOQSwkUizAdgNPkkg = commandNameMatchingMode;
		}

		public void RegisterTokenResolver([NotNull] ITokenResolver resolver)
		{
			mmLSzUqVTcMGsUmmXXncQLusiNCg.QSIoYPRpaNAbzpKoxxBOddnQfdoB(resolver);
			while (true)
			{
				int num = 2047462057;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x4FD0AE9F)) % 3)
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
					resolver.Register(this);
					num = (int)((num2 * 2845320) ^ 0x6397A1C7);
				}
			}
		}

		public void RegisterCommand([NotNull] string name, [NotNull] MethodInfo method, [CanBeNull] string description = null)
		{
			if (method.IsGenericMethod)
			{
				goto IL_0008;
			}
			goto IL_006b;
			IL_0008:
			int num = 56103836;
			goto IL_000d;
			IL_000d:
			kpOqttWobXfHSksnDwvNsfAmsgdlA item = default(kpOqttWobXfHSksnDwvNsfAmsgdlA);
			List<kpOqttWobXfHSksnDwvNsfAmsgdlA> value = default(List<kpOqttWobXfHSksnDwvNsfAmsgdlA>);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x5E04A5D3)) % 7)
				{
				case 0u:
					break;
				default:
					return;
				case 4u:
				{
					int num3;
					int num4;
					if (!CwpqVicEiBjmplmwKuvdekMpeqKM.TryGetValue(item.MzGbjYSINAlUiDjmRZYCXhbDKeTh, out value))
					{
						num3 = 1364905845;
						num4 = num3;
					}
					else
					{
						num3 = 1580706619;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 300862703);
					continue;
				}
				case 3u:
					goto IL_006b;
				case 6u:
					throw new ArgumentException("Provided method must not be generic.");
				case 1u:
					value = new List<kpOqttWobXfHSksnDwvNsfAmsgdlA>();
					CwpqVicEiBjmplmwKuvdekMpeqKM.Add(item.MzGbjYSINAlUiDjmRZYCXhbDKeTh, value);
					num = ((int)num2 * -2030992582) ^ -1398895310;
					continue;
				case 5u:
					value.Add(item);
					num = 585509819;
					continue;
				case 2u:
					return;
				}
				break;
			}
			goto IL_0008;
			IL_006b:
			item = new kpOqttWobXfHSksnDwvNsfAmsgdlA(this, name, method, description);
			num = 1412659334;
			goto IL_000d;
		}

		public void RegisterAssemblyForTypeResolution([NotNull] Assembly assembly)
		{
			voBRoFFzKynOBYQnDLxHHntxPWlf.Add(assembly);
		}

		public string InvokeCommand([NotNull] string command)
		{
			LastCommandException = null;
			if (BwbiKvwDrrefyiDfeMmhhQBAlCSgb.Count == 0)
			{
				while (true)
				{
					int num = 1829918283;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x7D0E3811)) % 3)
						{
						case 2u:
							break;
						case 1u:
							tKQmAhfhkuJyRRRciSwPSusxsuax();
							num = ((int)num2 * -1145141621) ^ 0x11487DD6;
							continue;
						default:
							goto end_IL_0014;
						}
						break;
					}
					continue;
					end_IL_0014:
					break;
				}
			}
			LpGYZHLDdDpelSxCYmKYgtKIpsCy.Clear();
			try
			{
				List<string> list = czRCxkFEAQcOTxcWtAUstYqQqnLr.LeFUloyjiVvyikIvaNNflmcQgPpk(command);
				if (list.Count == 0)
				{
					while (true)
					{
						uint num2;
						switch ((num2 = 1792273052u) % 3)
						{
						case 0u:
							continue;
						case 2u:
							return string.Empty;
						}
						break;
					}
				}
				string text = list[0];
				kpOqttWobXfHSksnDwvNsfAmsgdlA? kpOqttWobXfHSksnDwvNsfAmsgdlA2 = null;
				using (List<kpOqttWobXfHSksnDwvNsfAmsgdlA>.Enumerator enumerator = BwbiKvwDrrefyiDfeMmhhQBAlCSgb.GetEnumerator())
				{
					kpOqttWobXfHSksnDwvNsfAmsgdlA current = default(kpOqttWobXfHSksnDwvNsfAmsgdlA);
					while (true)
					{
						IL_016c:
						int num3;
						int num4;
						if (!enumerator.MoveNext())
						{
							num3 = 242915820;
							num4 = num3;
						}
						else
						{
							num3 = 1982418055;
							num4 = num3;
						}
						while (true)
						{
							uint num2;
							switch ((num2 = (uint)(num3 ^ 0x7D0E3811)) % 7)
							{
							case 6u:
								num3 = 1982418055;
								continue;
							default:
								goto end_IL_00ce;
							case 4u:
								kpOqttWobXfHSksnDwvNsfAmsgdlA2 = current;
								num3 = (int)(num2 * 1366295389) ^ -2031551308;
								continue;
							case 2u:
							{
								int num5;
								int num6;
								if (!string.Equals(current.YBLwrmnGpuNNorfGBIOCDXiTtNWV, text, CccFWUBFKPdDOQSwkUizAdgNPkkg))
								{
									num5 = 404473059;
									num6 = num5;
								}
								else
								{
									num5 = 1694359329;
									num6 = num5;
								}
								num3 = num5 ^ (int)(num2 * 2070297788);
								continue;
							}
							case 3u:
								current = enumerator.Current;
								num3 = 1877618864;
								continue;
							case 1u:
								num3 = ((int)num2 * -760376264) ^ -405824620;
								continue;
							case 0u:
								break;
							case 5u:
								goto end_IL_00ce;
							}
							goto IL_016c;
							continue;
							end_IL_00ce:
							break;
						}
						break;
					}
				}
				if (!kpOqttWobXfHSksnDwvNsfAmsgdlA2.HasValue)
				{
					while (true)
					{
						uint num2;
						switch ((num2 = 145156936u) % 3)
						{
						case 0u:
							continue;
						case 1u:
							throw new CommandInvocationException("Couldn't find command " + text + ".");
						}
						break;
					}
				}
				udjzGEbVIadxnOLYqlEindvjCNdW(kpOqttWobXfHSksnDwvNsfAmsgdlA2.Value, list);
			}
			catch (TokenizationException ex)
			{
				LpGYZHLDdDpelSxCYmKYgtKIpsCy.AppendLine("Parse error.");
				while (true)
				{
					IL_0211:
					int num7 = 892676880;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num7 ^ 0x7D0E3811)) % 3)
						{
						case 2u:
							break;
						default:
							goto end_IL_0216;
						case 1u:
							goto IL_0234;
						case 0u:
							goto end_IL_0216;
						}
						goto IL_0211;
						IL_0234:
						LpGYZHLDdDpelSxCYmKYgtKIpsCy.AppendLine(ex.ToString());
						LastCommandException = ex;
						num7 = (int)((num2 * 1033572886) ^ 0x5E9C6223);
						continue;
						end_IL_0216:
						break;
					}
					break;
				}
			}
			catch (ParameterResolverException ex2)
			{
				LpGYZHLDdDpelSxCYmKYgtKIpsCy.AppendLine($"Error resolving parameter {ex2.ParameterInfo.Name} of type {ex2.ParameterInfo.ParameterType}.");
				LpGYZHLDdDpelSxCYmKYgtKIpsCy.AppendLine(ex2.ToString());
				LastCommandException = ex2;
			}
			catch (TokenResolutionException ex3)
			{
				LpGYZHLDdDpelSxCYmKYgtKIpsCy.AppendLine($"Error resolving token '{ex3.Token}' for type {ex3.Type}.");
				while (true)
				{
					IL_02da:
					int num8 = 73180089;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num8 ^ 0x7D0E3811)) % 3)
						{
						case 2u:
							break;
						case 1u:
							goto IL_02fd;
						default:
							LastCommandException = ex3;
							goto end_IL_02df;
						}
						goto IL_02da;
						IL_02fd:
						LpGYZHLDdDpelSxCYmKYgtKIpsCy.AppendLine(ex3.ToString());
						num8 = ((int)num2 * -199813472) ^ 0x57A020F1;
						continue;
						end_IL_02df:
						break;
					}
					break;
				}
			}
			catch (CommandExecutionException ex4)
			{
				LpGYZHLDdDpelSxCYmKYgtKIpsCy.AppendLine("Error running command.");
				while (true)
				{
					IL_0340:
					int num9 = 100412333;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num9 ^ 0x7D0E3811)) % 3)
						{
						case 0u:
							break;
						default:
							goto end_IL_0345;
						case 2u:
							goto IL_0363;
						case 1u:
							goto end_IL_0345;
						}
						goto IL_0340;
						IL_0363:
						LpGYZHLDdDpelSxCYmKYgtKIpsCy.AppendLine(ex4.ToString());
						LastCommandException = ex4;
						num9 = (int)(num2 * 515896126) ^ -1755494944;
						continue;
						end_IL_0345:
						break;
					}
					break;
				}
			}
			catch (CommandInvocationException ex5)
			{
				while (true)
				{
					IL_0395:
					int num10 = 1568664888;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num10 ^ 0x7D0E3811)) % 4)
						{
						case 2u:
							break;
						default:
							goto end_IL_039a;
						case 1u:
							LpGYZHLDdDpelSxCYmKYgtKIpsCy.AppendLine("Error attempting to invoke command.");
							num10 = ((int)num2 * -1428413039) ^ 0x6A602730;
							continue;
						case 0u:
							LpGYZHLDdDpelSxCYmKYgtKIpsCy.AppendLine(ex5.ToString());
							LastCommandException = ex5;
							num10 = ((int)num2 * -1331036584) ^ 0x6A7A1172;
							continue;
						case 3u:
							goto end_IL_039a;
						}
						goto IL_0395;
						continue;
						end_IL_039a:
						break;
					}
					break;
				}
			}
			catch (Exception ex6)
			{
				while (true)
				{
					IL_040c:
					int num11 = 1837151071;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num11 ^ 0x7D0E3811)) % 4)
						{
						case 0u:
							break;
						default:
							goto end_IL_0411;
						case 2u:
							LpGYZHLDdDpelSxCYmKYgtKIpsCy.AppendLine("Command threw a general exception while running.");
							num11 = (int)(num2 * 1036047385) ^ -436198070;
							continue;
						case 1u:
							LpGYZHLDdDpelSxCYmKYgtKIpsCy.AppendLine(ex6.ToString());
							LastCommandException = ex6;
							num11 = (int)((num2 * 1301923366) ^ 0x921DE1C);
							continue;
						case 3u:
							goto end_IL_0411;
						}
						goto IL_040c;
						continue;
						end_IL_0411:
						break;
					}
					break;
				}
			}
			return LpGYZHLDdDpelSxCYmKYgtKIpsCy.ToString();
		}

		public void ClearContext()
		{
			BSzOQFkMsjYVCmQniVliDzfGYJEJ.Clear();
			ContextType = null;
			while (true)
			{
				int num = -1573732215;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1251425817)) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 2u:
						goto IL_0034;
					case 1u:
						return;
					}
					break;
					IL_0034:
					IsEnumerableContext = false;
					tKQmAhfhkuJyRRRciSwPSusxsuax();
					num = (int)((num2 * 896925406) ^ 0x58567A61);
				}
			}
		}

		public void SetContext<T>([NotNull] IEnumerable<T> context)
		{
			BSzOQFkMsjYVCmQniVliDzfGYJEJ.Clear();
			PushContext(context);
		}

		public void SetContext([NotNull] object context)
		{
			BSzOQFkMsjYVCmQniVliDzfGYJEJ.Clear();
			PushContext(context);
		}

		public void PushContext<T>([NotNull] IEnumerable<T> context)
		{
			aNhMkKeHsooKjSyhANIgxWiBSUSg(context, true, typeof(T));
		}

		public void PushContext([NotNull] object context)
		{
			Type type = context.GetType();
			Type type2 = default(Type);
			while (true)
			{
				int num = -1417449160;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -127440485)) % 6)
					{
					case 5u:
						break;
					case 3u:
						type2 = IfRiGOXeOpwdJOjrtlPwwOCnFPFF.eFHRywyZLBLcaVAtPVdEzuzhidup(type);
						num = ((int)num2 * -2059869584) ^ -84427989;
						continue;
					case 1u:
						return;
					case 2u:
					{
						int num3;
						int num4;
						if (!(type2 != null))
						{
							num3 = -1776490021;
							num4 = num3;
						}
						else
						{
							num3 = -1185547567;
							num4 = num3;
						}
						num = num3 ^ ((int)num2 * -1944744229);
						continue;
					}
					case 4u:
						aNhMkKeHsooKjSyhANIgxWiBSUSg(context, true, type2);
						num = (int)(num2 * 165917454) ^ -610359270;
						continue;
					default:
						aNhMkKeHsooKjSyhANIgxWiBSUSg(context, false, type);
						return;
					}
					break;
				}
			}
		}

		public object PopContext()
		{
			object result = BSzOQFkMsjYVCmQniVliDzfGYJEJ.Pop();
			object context = Context;
			if (context == null)
			{
				ContextType = null;
				IsEnumerableContext = false;
			}
			else
			{
				Type type = context.GetType();
				Type type2 = IfRiGOXeOpwdJOjrtlPwwOCnFPFF.eFHRywyZLBLcaVAtPVdEzuzhidup(type);
				if (type2 == null)
				{
					ContextType = type;
					IsEnumerableContext = false;
				}
				else
				{
					ContextType = type2;
					IsEnumerableContext = true;
				}
			}
			tKQmAhfhkuJyRRRciSwPSusxsuax();
			return result;
		}

		public bool HasResolverForType([NotNull] Type type)
		{
			return mmLSzUqVTcMGsUmmXXncQLusiNCg.HasResolverForType(type);
		}

		public bool HasResolverForType<T>()
		{
			return mmLSzUqVTcMGsUmmXXncQLusiNCg.HasResolverForType<T>();
		}

		public bool HasCommand([NotNull] string commandName)
		{
			wECsozKlnLbEXuvcHskRiPOjPWLM wECsozKlnLbEXuvcHskRiPOjPWLM2 = new wECsozKlnLbEXuvcHskRiPOjPWLM();
			wECsozKlnLbEXuvcHskRiPOjPWLM2.ujaBylKryXcTFfKZBuxuEHEakPzeA = commandName;
			while (true)
			{
				int num = -296139949;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -110802172)) % 3)
					{
					case 2u:
						break;
					case 1u:
						goto IL_002f;
					default:
						return CwpqVicEiBjmplmwKuvdekMpeqKM.SelectMany(zhzuCEXyYSJokcxwneoOptcywCwm._003C_003E9.IKjlJliHwJHoSEYfOrvQoBmkOcHP).Any(wECsozKlnLbEXuvcHskRiPOjPWLM2.QzuphlZEyDWZIYpwiphMxNpltwKA);
					}
					break;
					IL_002f:
					wECsozKlnLbEXuvcHskRiPOjPWLM2.XjfEdSSApFhbGfqzDRkWbLdpquul = this;
					num = ((int)num2 * -1587779817) ^ 0x1A6B4831;
				}
			}
		}

		public string FormatContextAsPrettyString()
		{
			return FormatContextAsPrettyString(Context);
		}

		public static string FormatContextAsPrettyString(object context)
		{
			if (context == null)
			{
				goto IL_0008;
			}
			goto IL_015a;
			IL_0008:
			int num = 1478732238;
			goto IL_000d;
			IL_000d:
			int? num3 = default(int?);
			Type type2 = default(Type);
			Type type3 = default(Type);
			ICollection collection = default(ICollection);
			IEnumerable enumerable = default(IEnumerable);
			StringBuilder stringBuilder = default(StringBuilder);
			Type type = default(Type);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x633BBC19)) % 16)
				{
				case 3u:
					break;
				case 12u:
					goto IL_0063;
				case 15u:
					num3 = null;
					num = ((int)num2 * -1833706738) ^ -389434957;
					continue;
				case 13u:
					return context.ToString();
				case 11u:
					type2 = type3;
					num = (int)((num2 * 1807050261) ^ 0x3CE5DADB);
					continue;
				case 8u:
					type2 = null;
					num = (int)((num2 * 2088158896) ^ 0x31BA0757);
					continue;
				case 5u:
					goto IL_00e8;
				case 14u:
				{
					collection = enumerable as ICollection;
					int num4;
					int num5;
					if (collection == null)
					{
						num4 = -11971123;
						num5 = num4;
					}
					else
					{
						num4 = -1519549813;
						num5 = num4;
					}
					num = num4 ^ ((int)num2 * -1659267132);
					continue;
				}
				case 1u:
					goto IL_015a;
				case 9u:
					stringBuilder.Append($"({type})");
					num = (int)(num2 * 1499852773) ^ -1396445104;
					continue;
				case 10u:
					num3 = collection.Count;
					num = ((int)num2 * -65511522) ^ -1224918119;
					continue;
				case 6u:
					type = context.GetType();
					num = 1156020486;
					continue;
				case 0u:
					goto IL_01d0;
				case 7u:
					return string.Empty;
				case 2u:
					stringBuilder.Append(num3);
					num = (int)((num2 * 778689301) ^ 0x3B726F3);
					continue;
				default:
					return stringBuilder.ToString();
				}
				break;
				IL_01d0:
				stringBuilder.Append(']');
				int num6;
				if (!(type2 == null))
				{
					num = 497852381;
					num6 = num;
				}
				else
				{
					num = 18303632;
					num6 = num;
				}
				continue;
				IL_00e8:
				stringBuilder = new StringBuilder();
				stringBuilder.Append((type2 != null) ? $"{type2}[" : "???[");
				int num7;
				if (!num3.HasValue)
				{
					num = 226836281;
					num7 = num;
				}
				else
				{
					num = 1036550459;
					num7 = num;
				}
				continue;
				IL_0063:
				type3 = IfRiGOXeOpwdJOjrtlPwwOCnFPFF.eFHRywyZLBLcaVAtPVdEzuzhidup(type);
				int num8;
				if (!(type3 != null))
				{
					num = 944362508;
					num8 = num;
				}
				else
				{
					num = 1982518306;
					num8 = num;
				}
			}
			goto IL_0008;
			IL_015a:
			enumerable = context as IEnumerable;
			int num9;
			if (enumerable != null)
			{
				num = 311043631;
				num9 = num;
			}
			else
			{
				num = 1194839236;
				num9 = num;
			}
			goto IL_000d;
		}

		private void aNhMkKeHsooKjSyhANIgxWiBSUSg(object P_0, bool P_1, Type P_2)
		{
			BSzOQFkMsjYVCmQniVliDzfGYJEJ.Push(P_0);
			IsEnumerableContext = P_1;
			while (true)
			{
				int num = 2111936607;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x4497391A)) % 4)
					{
					case 0u:
						break;
					default:
						return;
					case 1u:
						ContextType = P_2;
						num = (int)((num2 * 911703576) ^ 0x32582DB8);
						continue;
					case 2u:
						tKQmAhfhkuJyRRRciSwPSusxsuax();
						num = (int)(num2 * 826475802) ^ -271449923;
						continue;
					case 3u:
						return;
					}
					break;
				}
			}
		}

		private void udjzGEbVIadxnOLYqlEindvjCNdW(kpOqttWobXfHSksnDwvNsfAmsgdlA P_0, IReadOnlyList<string> P_1)
		{
			object[] array = new object[P_0.UUIgfIwPJCwnlxaFzoYVszeDotDn];
			int num = -1;
			object obj = default(object);
			int lastUsedTokenIndex = default(int);
			IEnumerable enumerable = default(IEnumerable);
			bool flag = default(bool);
			int num7 = default(int);
			Type type = default(Type);
			ParameterInfo parameterInfo = default(ParameterInfo);
			IList list = default(IList);
			object current = default(object);
			while (true)
			{
				int num2 = -1410530603;
				while (true)
				{
					int num12;
					int num13;
					uint num3;
					switch ((num3 = (uint)(num2 ^ -728553865)) % 25)
					{
					case 5u:
						break;
					case 6u:
						if (obj != null)
						{
							num2 = (int)((num3 * 1695107390) ^ 0x5CEB6270);
							continue;
						}
						goto IL_042a;
					case 1u:
					{
						int num16;
						int num17;
						if (num >= 0)
						{
							num16 = -2004581598;
							num17 = num16;
						}
						else
						{
							num16 = -1961970749;
							num17 = num16;
						}
						num2 = num16 ^ ((int)num3 * -2058265181);
						continue;
					}
					case 21u:
						lastUsedTokenIndex = 0;
						num2 = ((int)num3 * -862967634) ^ -155792388;
						continue;
					case 0u:
					{
						enumerable = Context as IEnumerable;
						int num20;
						if (enumerable != null)
						{
							num2 = -2097553332;
							num20 = num2;
						}
						else
						{
							num2 = -1437895483;
							num20 = num2;
						}
						continue;
					}
					case 14u:
						flag = true;
						num2 = (int)((num3 * 832098014) ^ 0x15BB2DE5);
						continue;
					case 20u:
						num = num7;
						num2 = ((int)num3 * -928865764) ^ -359316305;
						continue;
					case 19u:
					{
						int num9;
						int num10;
						if (!(type != typeof(string)))
						{
							num9 = 1335683496;
							num10 = num9;
						}
						else
						{
							num9 = 1651150113;
							num10 = num9;
						}
						num2 = num9 ^ (int)(num3 * 1075284034);
						continue;
					}
					case 3u:
						array[num7] = mmLSzUqVTcMGsUmmXXncQLusiNCg.ResolveParameter(parameterInfo, P_1, ref lastUsedTokenIndex);
						num2 = -578867186;
						continue;
					case 16u:
						if (type != typeof(void))
						{
							num2 = -1418258828;
							continue;
						}
						goto IL_042a;
					case 10u:
						aNhMkKeHsooKjSyhANIgxWiBSUSg(obj, flag, type);
						num2 = ((int)num3 * -1965172989) ^ -657616194;
						continue;
					case 8u:
						num2 = (int)((num3 * 1505739421) ^ 0x5520AB36);
						continue;
					case 24u:
					{
						int num18;
						int num19;
						if (type.IsClass)
						{
							num18 = 588948634;
							num19 = num18;
						}
						else
						{
							num18 = 118184902;
							num19 = num18;
						}
						num2 = num18 ^ (int)(num3 * 177795855);
						continue;
					}
					case 12u:
						flag = false;
						num2 = -841041408;
						continue;
					case 22u:
					{
						int num14;
						int num15;
						if (P_0.WfndoufkaPaQMggxgUSCHTGYHHRPB)
						{
							num14 = -720974030;
							num15 = num14;
						}
						else
						{
							num14 = -1481277386;
							num15 = num14;
						}
						num2 = num14 ^ ((int)num3 * -1795546527);
						continue;
					}
					case 13u:
						num2 = (int)(num3 * 2098403640) ^ -523092754;
						continue;
					case 7u:
					{
						parameterInfo = P_0.bkncIrHDIQIvZUDikxserJxasceeA[num7];
						int num11;
						if (mmLSzUqVTcMGsUmmXXncQLusiNCg.VbMKGqtUAZLbQXJHWXHrppnVIlLz(parameterInfo))
						{
							num2 = -1944538394;
							num11 = num2;
						}
						else
						{
							num2 = -571417551;
							num11 = num2;
						}
						continue;
					}
					case 11u:
					{
						int num8;
						if (num7 < array.Length)
						{
							num2 = -443771415;
							num8 = num2;
						}
						else
						{
							num2 = -1803203146;
							num8 = num2;
						}
						continue;
					}
					case 2u:
						obj = NMwIuAnVGocBJwOWjuOJUXjXHMRj(P_0, P_1, Context, array, num, ref lastUsedTokenIndex, out type);
						num2 = (int)(num3 * 1752976039) ^ -1616117268;
						continue;
					case 15u:
						list = obj as IList;
						if (list != null)
						{
							num2 = -15222372;
							continue;
						}
						LpGYZHLDdDpelSxCYmKYgtKIpsCy.AppendLine(obj.ToString());
						goto IL_0403;
					case 18u:
						num7++;
						num2 = -1647269985;
						continue;
					case 23u:
						obj = DRjVjltAnRtDlrfQZCIuuEhuHTwW(P_0, P_1, enumerable, array, num, ref lastUsedTokenIndex, out type);
						num2 = (int)(num3 * 404486170) ^ -1038065411;
						continue;
					case 9u:
						num7 = 0;
						num2 = ((int)num3 * -2105770856) ^ -54223454;
						continue;
					default:
					{
						IEnumerator enumerator = list.GetEnumerator();
						try
						{
							while (true)
							{
								IL_0369:
								int num4;
								int num5;
								if (!enumerator.MoveNext())
								{
									num4 = -921285331;
									num5 = num4;
								}
								else
								{
									num4 = -1022732301;
									num5 = num4;
								}
								while (true)
								{
									switch ((num3 = (uint)(num4 ^ -728553865)) % 5)
									{
									case 0u:
										num4 = -1022732301;
										continue;
									default:
										goto end_IL_0333;
									case 1u:
										current = enumerator.Current;
										num4 = -1551779940;
										continue;
									case 3u:
										break;
									case 2u:
										LpGYZHLDdDpelSxCYmKYgtKIpsCy.AppendLine(current?.ToString());
										num4 = -1345725629;
										continue;
									case 4u:
										goto end_IL_0333;
									}
									goto IL_0369;
									continue;
									end_IL_0333:
									break;
								}
								break;
							}
						}
						finally
						{
							if (enumerator is IDisposable disposable)
							{
								while (true)
								{
									IL_03b6:
									int num6 = -28356720;
									while (true)
									{
										switch ((num3 = (uint)(num6 ^ -728553865)) % 3)
										{
										case 0u:
											break;
										default:
											goto end_IL_03bb;
										case 1u:
											goto IL_03d9;
										case 2u:
											goto end_IL_03bb;
										}
										goto IL_03b6;
										IL_03d9:
										disposable.Dispose();
										num6 = (int)((num3 * 449005649) ^ 0xD5BF1DF);
										continue;
										end_IL_03bb:
										break;
									}
									break;
								}
							}
						}
						goto IL_042a;
					}
					case 17u:
						goto IL_042a;
						IL_042a:
						if (lastUsedTokenIndex < P_1.Count - 1)
						{
							num12 = -563947030;
							num13 = num12;
						}
						else
						{
							num12 = -2105853776;
							num13 = num12;
						}
						goto IL_0408;
						IL_0408:
						while (true)
						{
							switch ((num3 = (uint)(num12 ^ -728553865)) % 4)
							{
							case 0u:
								break;
							default:
								return;
							case 2u:
								goto IL_042a;
							case 1u:
								LpGYZHLDdDpelSxCYmKYgtKIpsCy.AppendLine("Too many parameters provided to command " + P_0.YBLwrmnGpuNNorfGBIOCDXiTtNWV + ". Ignoring excess.");
								num12 = ((int)num3 * -470655144) ^ -401729464;
								continue;
							case 3u:
								return;
							}
							break;
						}
						goto IL_0403;
						IL_0403:
						num12 = -623876115;
						goto IL_0408;
					}
					break;
				}
			}
		}

		private object DRjVjltAnRtDlrfQZCIuuEhuHTwW(kpOqttWobXfHSksnDwvNsfAmsgdlA P_0, IReadOnlyList<string> P_1, IEnumerable P_2, object[] P_3, int P_4, ref int P_5, out Type P_6)
		{
			ICollection collection = Context as ICollection;
			IList<object> list = default(IList<object>);
			object current = default(object);
			while (true)
			{
				int num = -123933330;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -186978668)) % 7)
					{
					case 4u:
						break;
					case 1u:
					{
						int num6;
						int num7;
						if (collection != null)
						{
							num6 = 631208535;
							num7 = num6;
						}
						else
						{
							num6 = 1489452962;
							num7 = num6;
						}
						num = num6 ^ (int)(num2 * 1004588510);
						continue;
					}
					case 5u:
						P_6 = null;
						num = -215527784;
						continue;
					case 6u:
						list = new List<object>(collection.Count);
						num = (int)(num2 * 487804358) ^ -1335413227;
						continue;
					case 2u:
						num = ((int)num2 * -1190188339) ^ -639557623;
						continue;
					case 0u:
						list = new List<object>();
						num = -899407466;
						continue;
					default:
					{
						IEnumerator enumerator = P_2.GetEnumerator();
						try
						{
							while (true)
							{
								IL_00da:
								int num3;
								int num4;
								if (enumerator.MoveNext())
								{
									num3 = -571713321;
									num4 = num3;
								}
								else
								{
									num3 = -710175108;
									num4 = num3;
								}
								while (true)
								{
									switch ((num2 = (uint)(num3 ^ -186978668)) % 5)
									{
									case 2u:
										num3 = -571713321;
										continue;
									default:
										goto end_IL_00b4;
									case 1u:
										break;
									case 0u:
									{
										object item = NMwIuAnVGocBJwOWjuOJUXjXHMRj(P_0, P_1, current, P_3, P_4, ref P_5, out P_6);
										list.Add(item);
										num3 = (int)((num2 * 1886745211) ^ 0x6A30445F);
										continue;
									}
									case 4u:
										current = enumerator.Current;
										num3 = -821727974;
										continue;
									case 3u:
										goto end_IL_00b4;
									}
									goto IL_00da;
									continue;
									end_IL_00b4:
									break;
								}
								break;
							}
						}
						finally
						{
							if (enumerator is IDisposable disposable)
							{
								while (true)
								{
									IL_013a:
									int num5 = -1480079315;
									while (true)
									{
										switch ((num2 = (uint)(num5 ^ -186978668)) % 3)
										{
										case 0u:
											break;
										default:
											goto end_IL_013f;
										case 2u:
											goto IL_015d;
										case 1u:
											goto end_IL_013f;
										}
										goto IL_013a;
										IL_015d:
										disposable.Dispose();
										num5 = ((int)num2 * -525903954) ^ 0x5B8E5D9C;
										continue;
										end_IL_013f:
										break;
									}
									break;
								}
							}
						}
						return list;
					}
					}
					break;
				}
			}
		}

		private object NMwIuAnVGocBJwOWjuOJUXjXHMRj(kpOqttWobXfHSksnDwvNsfAmsgdlA P_0, IReadOnlyList<string> P_1, object P_2, object[] P_3, int P_4, ref int P_5, out Type P_6)
		{
			if (P_4 >= 0)
			{
				goto IL_0005;
			}
			goto IL_0071;
			IL_0005:
			int num = 1995718664;
			goto IL_000a;
			IL_000a:
			object obj = default(object);
			IMemberWrapper memberWrapper = default(IMemberWrapper);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x3105A44B)) % 7)
				{
				case 4u:
					break;
				case 1u:
					obj = XGqvyUXbaDQoUEDsXflYJPsfJMpWA(memberWrapper, P_1, ref P_5);
					P_6 = memberWrapper.MemberType;
					num = 1956904311;
					continue;
				case 0u:
				{
					int num3;
					int num4;
					if (memberWrapper == null)
					{
						num3 = 1056860501;
						num4 = num3;
					}
					else
					{
						num3 = 1069067649;
						num4 = num3;
					}
					num = num3 ^ ((int)num2 * -1187461081);
					continue;
				}
				case 2u:
					goto IL_0071;
				case 3u:
					return obj;
				case 6u:
					P_3[P_4] = P_2;
					num = (int)((num2 * 739013850) ^ 0x475756B7);
					continue;
				default:
					return obj;
				}
				break;
			}
			goto IL_0005;
			IL_0071:
			obj = P_0.HlGwOAKaoNMxiiutGrpPqeWSOgBM.Invoke(P_0.WfndoufkaPaQMggxgUSCHTGYHHRPB ? null : P_2, P_3);
			P_6 = P_0.trZaBdfbWuyVlkBeEHjhbgibLyXpB;
			memberWrapper = obj as IMemberWrapper;
			num = 2041827619;
			goto IL_000a;
		}

		private object XGqvyUXbaDQoUEDsXflYJPsfJMpWA(IMemberWrapper P_0, IReadOnlyList<string> P_1, ref int P_2)
		{
			IInvokableMember invokableMember = P_0 as IInvokableMember;
			IReadableMember readableMember = default(IReadableMember);
			IWriteableMember writeableMember = default(IWriteableMember);
			while (true)
			{
				int num = -1834238869;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -118772852)) % 15)
					{
					case 0u:
						break;
					case 1u:
					{
						int num7;
						int num8;
						if (!readableMember.CanRead)
						{
							num7 = 1512158426;
							num8 = num7;
						}
						else
						{
							num7 = 971495676;
							num8 = num7;
						}
						num = num7 ^ (int)(num2 * 849722987);
						continue;
					}
					case 5u:
						return null;
					case 4u:
					{
						int num9;
						int num10;
						if (invokableMember != null)
						{
							num9 = 547577396;
							num10 = num9;
						}
						else
						{
							num9 = 1831165728;
							num10 = num9;
						}
						num = num9 ^ ((int)num2 * -1461890165);
						continue;
					}
					case 12u:
						return invokableMember.Invoke(mmLSzUqVTcMGsUmmXXncQLusiNCg, P_1, ref P_2);
					case 10u:
						LpGYZHLDdDpelSxCYmKYgtKIpsCy.AppendLine($"{writeableMember.Member} is not writeable. Skipping write attempt.");
						num = ((int)num2 * -173564646) ^ -308931818;
						continue;
					case 7u:
						LpGYZHLDdDpelSxCYmKYgtKIpsCy.AppendLine($"{readableMember.Member} is not readable. Doing nothing.");
						return null;
					case 14u:
					{
						int num4;
						int num5;
						if (!writeableMember.CanWrite)
						{
							num4 = 1438225349;
							num5 = num4;
						}
						else
						{
							num4 = 659831064;
							num5 = num4;
						}
						num = num4 ^ ((int)num2 * -752377302);
						continue;
					}
					case 6u:
					{
						int num11;
						int num12;
						if (P_2 >= P_1.Count - 1)
						{
							num11 = -770704196;
							num12 = num11;
						}
						else
						{
							num11 = -68971655;
							num12 = num11;
						}
						num = num11 ^ ((int)num2 * -1527076268);
						continue;
					}
					case 9u:
					{
						string token = P_1[++P_2];
						writeableMember.SetValue(mmLSzUqVTcMGsUmmXXncQLusiNCg, token);
						num = -12685171;
						continue;
					}
					case 2u:
					{
						writeableMember = P_0 as IWriteableMember;
						int num6;
						if (writeableMember == null)
						{
							num = -221082792;
							num6 = num;
						}
						else
						{
							num = -1129178279;
							num6 = num;
						}
						continue;
					}
					case 8u:
						LpGYZHLDdDpelSxCYmKYgtKIpsCy.AppendLine("We got an IMemberWrapper of an unexpected type. Doing nothing with it.");
						num = -1175422663;
						continue;
					case 13u:
						return readableMember.GetValue();
					case 11u:
					{
						readableMember = P_0 as IReadableMember;
						int num3;
						if (readableMember != null)
						{
							num = -173650365;
							num3 = num;
						}
						else
						{
							num = -341207778;
							num3 = num;
						}
						continue;
					}
					default:
						return null;
					}
					break;
				}
			}
		}

		private void tKQmAhfhkuJyRRRciSwPSusxsuax()
		{
			BwbiKvwDrrefyiDfeMmhhQBAlCSgb.Clear();
			List<kpOqttWobXfHSksnDwvNsfAmsgdlA> value = default(List<kpOqttWobXfHSksnDwvNsfAmsgdlA>);
			if (ContextType != null)
			{
				while (true)
				{
					int num = 541812991;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x40FD908A)) % 4)
						{
						case 3u:
							break;
						case 1u:
						{
							int num3;
							int num4;
							if (!CwpqVicEiBjmplmwKuvdekMpeqKM.TryGetValue(ContextType, out value))
							{
								num3 = -628109451;
								num4 = num3;
							}
							else
							{
								num3 = -902159081;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 2084731887);
							continue;
						}
						case 2u:
							BwbiKvwDrrefyiDfeMmhhQBAlCSgb.AddRange(value);
							num = (int)((num2 * 1262121595) ^ 0x26195D8C);
							continue;
						default:
							goto end_IL_001c;
						}
						break;
					}
					continue;
					end_IL_001c:
					break;
				}
				IEnumerator<kpOqttWobXfHSksnDwvNsfAmsgdlA> enumerator = (from P_0 in CwpqVicEiBjmplmwKuvdekMpeqKM.Where(delegate(KeyValuePair<Type, List<kpOqttWobXfHSksnDwvNsfAmsgdlA>> P_0)
					{
						if (P_0.Key.IsAssignableFrom(ContextType))
						{
							while (true)
							{
								uint num9;
								switch ((num9 = 1113971261u) % 3)
								{
								case 0u:
									continue;
								case 2u:
									return ContextType != P_0.Key;
								}
								break;
							}
						}
						return false;
					})
					select new WwYyVFZaXzykaQdWUmoZZoAHZoPx(P_0.Value, IfRiGOXeOpwdJOjrtlPwwOCnFPFF.ctdEYIimdWRLVraecBsvmlqRdPgg(ContextType, P_0.Key))).OrderBy(zhzuCEXyYSJokcxwneoOptcywCwm._003C_003E9.hveDIWTaDJhTcuOzZMxyhMUTalWu).SelectMany(zhzuCEXyYSJokcxwneoOptcywCwm._003C_003E9.HWNHbCHnukuOiIVznstqhbkExEmS).GetEnumerator();
				try
				{
					kpOqttWobXfHSksnDwvNsfAmsgdlA current = default(kpOqttWobXfHSksnDwvNsfAmsgdlA);
					while (true)
					{
						IL_0156:
						int num5;
						int num6;
						if (enumerator.MoveNext())
						{
							num5 = 804012478;
							num6 = num5;
						}
						else
						{
							num5 = 1225211302;
							num6 = num5;
						}
						while (true)
						{
							uint num2;
							switch ((num2 = (uint)(num5 ^ 0x40FD908A)) % 5)
							{
							case 3u:
								num5 = 804012478;
								continue;
							default:
								goto end_IL_0108;
							case 4u:
								current = enumerator.Current;
								num5 = 1302169025;
								continue;
							case 0u:
								BwbiKvwDrrefyiDfeMmhhQBAlCSgb.Add(current);
								num5 = ((int)num2 * -595733709) ^ -1514807285;
								continue;
							case 2u:
								break;
							case 1u:
								goto end_IL_0108;
							}
							goto IL_0156;
							continue;
							end_IL_0108:
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
							IL_0174:
							int num7 = 1882815094;
							while (true)
							{
								uint num2;
								switch ((num2 = (uint)(num7 ^ 0x40FD908A)) % 3)
								{
								case 0u:
									break;
								default:
									goto end_IL_0179;
								case 1u:
									goto IL_0196;
								case 2u:
									goto end_IL_0179;
								}
								goto IL_0174;
								IL_0196:
								enumerator.Dispose();
								num7 = ((int)num2 * -1095175572) ^ -1285386554;
								continue;
								end_IL_0179:
								break;
							}
							break;
						}
					}
				}
			}
			if (!CwpqVicEiBjmplmwKuvdekMpeqKM.TryGetValue(typeof(void), out value))
			{
				return;
			}
			while (true)
			{
				int num8 = 626817492;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num8 ^ 0x40FD908A)) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 1u:
						goto IL_01e7;
					case 2u:
						return;
					}
					break;
					IL_01e7:
					BwbiKvwDrrefyiDfeMmhhQBAlCSgb.AddRange(value);
					num8 = (int)(num2 * 669571083) ^ -166849708;
				}
			}
		}

		[CompilerGenerated]
		private bool tIEIWZfwMABTniKydbVTLdbBehjU(KeyValuePair<Type, List<kpOqttWobXfHSksnDwvNsfAmsgdlA>> P_0)
		{
			if (P_0.Key.IsAssignableFrom(ContextType))
			{
				while (true)
				{
					uint num;
					switch ((num = 1113971261u) % 3)
					{
					case 0u:
						continue;
					case 2u:
						return ContextType != P_0.Key;
					}
					break;
				}
			}
			return false;
		}

		[CompilerGenerated]
		private WwYyVFZaXzykaQdWUmoZZoAHZoPx WnrsHlCjtDbeBTnAlPhAIVbHvvWF(KeyValuePair<Type, List<kpOqttWobXfHSksnDwvNsfAmsgdlA>> P_0)
		{
			return new WwYyVFZaXzykaQdWUmoZZoAHZoPx(P_0.Value, IfRiGOXeOpwdJOjrtlPwwOCnFPFF.ctdEYIimdWRLVraecBsvmlqRdPgg(ContextType, P_0.Key));
		}
	}
}
