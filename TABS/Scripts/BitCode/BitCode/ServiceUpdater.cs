using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using BitCode.Extensions;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace BitCode
{
	public class ServiceUpdater : IDisposable, IServiceUpdater
	{
		private readonly HashSet<IUpdateableService> QqaJShxYBNzTMlIWBirUluGCcpDU = new HashSet<IUpdateableService>();

		private readonly PlayerLoopSystem xflqxzTQpQCfMqjOtdDxaUrGkMSSA;

		private bool tlRiOzSchvnYldbxynOFisSYraBV;

		[CompilerGenerated]
		private Action<Exception, IUpdateableService> djOYuJQddqopZpVLawymzdufLAIO;

		public event Action<Exception, IUpdateableService> InternalErrorOccurred
		{
			[CompilerGenerated]
			add
			{
				Action<Exception, IUpdateableService> action = djOYuJQddqopZpVLawymzdufLAIO;
				Action<Exception, IUpdateableService> action2 = default(Action<Exception, IUpdateableService>);
				while (true)
				{
					int num = -208855888;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -264884366)) % 4)
						{
						case 0u:
							break;
						default:
							return;
						case 2u:
							action2 = action;
							num = -982992755;
							continue;
						case 3u:
						{
							Action<Exception, IUpdateableService> value2 = (Action<Exception, IUpdateableService>)Delegate.Combine(action2, value);
							action = Interlocked.CompareExchange(ref djOYuJQddqopZpVLawymzdufLAIO, value2, action2);
							int num3;
							int num4;
							if ((object)action != action2)
							{
								num3 = 1225428417;
								num4 = num3;
							}
							else
							{
								num3 = 101176926;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 1147098767);
							continue;
						}
						case 1u:
							return;
						}
						break;
					}
				}
			}
			[CompilerGenerated]
			remove
			{
				Action<Exception, IUpdateableService> action = djOYuJQddqopZpVLawymzdufLAIO;
				Action<Exception, IUpdateableService> action2 = default(Action<Exception, IUpdateableService>);
				while (true)
				{
					int num = 1761722335;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x5C47653B)) % 5)
						{
						case 0u:
							break;
						default:
							return;
						case 4u:
							action2 = action;
							num = 1838279971;
							continue;
						case 3u:
						{
							Action<Exception, IUpdateableService> value2 = (Action<Exception, IUpdateableService>)Delegate.Remove(action2, value);
							action = Interlocked.CompareExchange(ref djOYuJQddqopZpVLawymzdufLAIO, value2, action2);
							num = ((int)num2 * -213798053) ^ 0x74053C67;
							continue;
						}
						case 1u:
						{
							int num3;
							int num4;
							if ((object)action == action2)
							{
								num3 = -390864688;
								num4 = num3;
							}
							else
							{
								num3 = -1984443277;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 1169319903);
							continue;
						}
						case 2u:
							return;
						}
						break;
					}
				}
			}
		}

		public ServiceUpdater()
			: this(PlayerLoop.GetCurrentPlayerLoop())
		{
		}

		public ServiceUpdater(PlayerLoopSystem basePlayerLoop)
		{
			xflqxzTQpQCfMqjOtdDxaUrGkMSSA = basePlayerLoop;
		}

		public void RegisterService(IUpdateableService service)
		{
			UrWwwkEVqlsCwuqAxyNaOnyUzodO();
			if (QqaJShxYBNzTMlIWBirUluGCcpDU.Count == 0)
			{
				while (true)
				{
					int num = 915370252;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x228D4140)) % 3)
						{
						case 0u:
							break;
						case 2u:
							SAIWKNSzvZbejbObihvKesVoxEJG();
							num = (int)((num2 * 1002844022) ^ 0x1F58A76B);
							continue;
						default:
							goto end_IL_0013;
						}
						break;
					}
					continue;
					end_IL_0013:
					break;
				}
			}
			QqaJShxYBNzTMlIWBirUluGCcpDU.Add(service);
		}

		public void DeregisterService(IUpdateableService service)
		{
			UrWwwkEVqlsCwuqAxyNaOnyUzodO();
			if (!QqaJShxYBNzTMlIWBirUluGCcpDU.Contains(service))
			{
				goto IL_0014;
			}
			goto IL_007e;
			IL_0014:
			int num = -1892485984;
			goto IL_0019;
			IL_0019:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -952938804)) % 6)
				{
				case 2u:
					break;
				default:
					return;
				case 1u:
					XgeDFmfYXeHKymlMCcwsGqubVUCxb();
					num = ((int)num2 * -118481411) ^ -1020616568;
					continue;
				case 5u:
				{
					int num3;
					int num4;
					if (QqaJShxYBNzTMlIWBirUluGCcpDU.Count <= 0)
					{
						num3 = 563928258;
						num4 = num3;
					}
					else
					{
						num3 = 1997539186;
						num4 = num3;
					}
					num = num3 ^ ((int)num2 * -194110299);
					continue;
				}
				case 0u:
					goto IL_007e;
				case 4u:
					throw new InvalidOperationException("Service not registered.");
				case 3u:
					return;
				}
				break;
			}
			goto IL_0014;
			IL_007e:
			QqaJShxYBNzTMlIWBirUluGCcpDU.Remove(service);
			num = -1607844387;
			goto IL_0019;
		}

		private void SAIWKNSzvZbejbObihvKesVoxEJG()
		{
			PlayerLoopSystem playerLoop = xflqxzTQpQCfMqjOtdDxaUrGkMSSA;
			PlayerLoopSystem playerLoopSystem = default(PlayerLoopSystem);
			PlayerLoopSystem item = default(PlayerLoopSystem);
			int num3 = default(int);
			PlayerLoopSystem playerLoopSystem2 = default(PlayerLoopSystem);
			List<PlayerLoopSystem> list = default(List<PlayerLoopSystem>);
			int num4 = default(int);
			while (true)
			{
				int num = -624019795;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -383032564)) % 16)
					{
					case 9u:
						break;
					case 1u:
						playerLoopSystem.type = typeof(ServiceUpdater);
						playerLoopSystem.updateDelegate = kCTtkGZWKYNRqFSpEsKcJOaFpgdb;
						item = playerLoopSystem;
						num3 = -1;
						num = ((int)num2 * -275002588) ^ -1411847773;
						continue;
					case 0u:
						playerLoopSystem2.subSystemList = list.ToArray();
						playerLoop.subSystemList[num3] = playerLoopSystem2;
						num = (int)(num2 * 1661416977) ^ -754738280;
						continue;
					case 15u:
						num = (int)((num2 * 870924515) ^ 0x623C5A93);
						continue;
					case 12u:
						throw new InvalidOperationException("Couldn't find PreUpdate in player loop. Was it removed?");
					case 13u:
						num = (int)(num2 * 687343132) ^ -1521697675;
						continue;
					case 7u:
						list.AddRange(playerLoopSystem2.subSystemList);
						num = ((int)num2 * -1718435777) ^ -427453293;
						continue;
					case 2u:
					{
						int num7;
						if (num4 < playerLoop.subSystemList.Length)
						{
							num = -1876301276;
							num7 = num;
						}
						else
						{
							num = -587302631;
							num7 = num;
						}
						continue;
					}
					case 5u:
					{
						int num6;
						if (num3 != -1)
						{
							num = -873502385;
							num6 = num;
						}
						else
						{
							num = -1717658736;
							num6 = num;
						}
						continue;
					}
					case 8u:
					{
						int num5;
						if (!(playerLoop.subSystemList[num4].type == typeof(PreUpdate)))
						{
							num = -1817561674;
							num5 = num;
						}
						else
						{
							num = -661269470;
							num5 = num;
						}
						continue;
					}
					case 11u:
						num4 = 0;
						num = (int)((num2 * 1513711209) ^ 0x2B546B20);
						continue;
					case 10u:
						num4++;
						num = -1190920002;
						continue;
					case 3u:
						playerLoopSystem2 = playerLoop.subSystemList[num3];
						list = new List<PlayerLoopSystem>(playerLoopSystem2.subSystemList.Length + 1);
						num = -1051120853;
						continue;
					case 14u:
						num3 = num4;
						num = ((int)num2 * -1469727582) ^ 0x69B9069D;
						continue;
					case 6u:
						list.Add(item);
						num = ((int)num2 * -2123734378) ^ 0x790639A8;
						continue;
					default:
						PlayerLoop.SetPlayerLoop(playerLoop);
						return;
					}
					break;
				}
			}
		}

		private void kCTtkGZWKYNRqFSpEsKcJOaFpgdb()
		{
			foreach (IUpdateableService item in QqaJShxYBNzTMlIWBirUluGCcpDU)
			{
				try
				{
					if (item == null)
					{
						continue;
					}
					while (true)
					{
						IL_0019:
						int num = 571720058;
						while (true)
						{
							uint num2;
							switch ((num2 = (uint)(num ^ 0x3463B1F2)) % 3)
							{
							case 2u:
								break;
							default:
								goto end_IL_001e;
							case 1u:
								goto IL_003b;
							case 0u:
								goto end_IL_001e;
							}
							goto IL_0019;
							IL_003b:
							item.Update();
							num = ((int)num2 * -1143103393) ^ 0x663F65A6;
							continue;
							end_IL_001e:
							break;
						}
						break;
					}
				}
				catch (Exception arg)
				{
					djOYuJQddqopZpVLawymzdufLAIO.SafelyInvoke(arg, item);
				}
			}
		}

		private void XgeDFmfYXeHKymlMCcwsGqubVUCxb()
		{
			PlayerLoop.SetPlayerLoop(xflqxzTQpQCfMqjOtdDxaUrGkMSSA);
		}

		private void UrWwwkEVqlsCwuqAxyNaOnyUzodO()
		{
			if (!tlRiOzSchvnYldbxynOFisSYraBV)
			{
				return;
			}
			while (true)
			{
				uint num;
				switch ((num = 1026089069u) % 3)
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

		private void FMrXgrmCIGmTIAmtWxyMsbxVOWEF()
		{
			if (tlRiOzSchvnYldbxynOFisSYraBV)
			{
				return;
			}
			while (true)
			{
				int num = 1495735086;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x6D95F9EF)) % 3)
					{
					case 2u:
						break;
					default:
						return;
					case 1u:
						goto IL_002a;
					case 0u:
						return;
					}
					break;
					IL_002a:
					XgeDFmfYXeHKymlMCcwsGqubVUCxb();
					num = (int)(num2 * 1630581344) ^ -6125846;
				}
			}
		}

		public void Dispose()
		{
			HashSet<IUpdateableService> qqaJShxYBNzTMlIWBirUluGCcpDU = QqaJShxYBNzTMlIWBirUluGCcpDU;
			if (qqaJShxYBNzTMlIWBirUluGCcpDU == null)
			{
				goto IL_0033;
			}
			qqaJShxYBNzTMlIWBirUluGCcpDU.Clear();
			goto IL_0011;
			IL_0011:
			int num = 150280854;
			goto IL_0016;
			IL_0033:
			FMrXgrmCIGmTIAmtWxyMsbxVOWEF();
			GC.SuppressFinalize(this);
			num = 704514699;
			goto IL_0016;
			IL_0016:
			uint num2;
			switch ((num2 = (uint)(num ^ 0x56995590)) % 3)
			{
			case 0u:
				break;
			case 2u:
				goto IL_0033;
			default:
				tlRiOzSchvnYldbxynOFisSYraBV = true;
				return;
			}
			goto IL_0011;
		}

		~ServiceUpdater()
		{
			FMrXgrmCIGmTIAmtWxyMsbxVOWEF();
		}
	}
}
