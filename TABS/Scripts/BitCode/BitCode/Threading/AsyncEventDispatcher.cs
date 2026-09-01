using System;
using System.Collections.Concurrent;
using BitCode.Extensions;
using JetBrains.Annotations;

namespace BitCode.Threading
{
	public class AsyncEventDispatcher : IDisposable, IUpdateableService
	{
		private sealed class urThmvUQMrBrmbWpTLAspYVIiCct
		{
			public Action FHgBHyavfNdkhWMqoCudhMJnWCgE;

			internal void xkMROJooiIZWxwfMqjcyEDhMRmaJ()
			{
				FHgBHyavfNdkhWMqoCudhMJnWCgE?.SafelyInvoke();
			}
		}

		private sealed class WhgqmVPNKQDOtGuysXpXhBxWInee<_0001>
		{
			public Action<_0001> FHgBHyavfNdkhWMqoCudhMJnWCgE;

			public _0001 lhuAiwnCeBNlNexPeZDjBlbXolZj;

			internal void xkMROJooiIZWxwfMqjcyEDhMRmaJ()
			{
				FHgBHyavfNdkhWMqoCudhMJnWCgE?.SafelyInvoke(lhuAiwnCeBNlNexPeZDjBlbXolZj);
			}
		}

		private sealed class ZgpEGROawSeTOwGhoUpWgmONKEpF<_0001, _0002>
		{
			public Action<_0001, _0002> FHgBHyavfNdkhWMqoCudhMJnWCgE;

			public _0001 NpvDUQnzFgAeQOOPFZhPmdTITTBH;

			public _0002 rBBzqDrIlGcHLlcqimEOcRrmPwEc;

			internal void xkMROJooiIZWxwfMqjcyEDhMRmaJ()
			{
				FHgBHyavfNdkhWMqoCudhMJnWCgE?.SafelyInvoke(NpvDUQnzFgAeQOOPFZhPmdTITTBH, rBBzqDrIlGcHLlcqimEOcRrmPwEc);
			}
		}

		private sealed class iGxKHhjXaZlfnmkEjHatbAVmeLNd<_0001, _0002, _0003>
		{
			public Action<_0001, _0002, _0003> FHgBHyavfNdkhWMqoCudhMJnWCgE;

			public _0001 NpvDUQnzFgAeQOOPFZhPmdTITTBH;

			public _0002 rBBzqDrIlGcHLlcqimEOcRrmPwEc;

			public _0003 cFRuPWNIeCmbYBVHuJUMMwLdasBL;

			internal void xkMROJooiIZWxwfMqjcyEDhMRmaJ()
			{
				FHgBHyavfNdkhWMqoCudhMJnWCgE?.SafelyInvoke(NpvDUQnzFgAeQOOPFZhPmdTITTBH, rBBzqDrIlGcHLlcqimEOcRrmPwEc, cFRuPWNIeCmbYBVHuJUMMwLdasBL);
			}
		}

		private readonly ConcurrentQueue<Action> tJZHJjUlJouCbLPkpEWRALzTQaptA = new ConcurrentQueue<Action>();

		private readonly IServiceUpdater UeSkHcBnUDZDTHkgfGCVbjKIjUXtA;

		private volatile bool tlRiOzSchvnYldbxynOFisSYraBV;

		public AsyncEventDispatcher([NotNull] IServiceUpdater serviceUpdater)
		{
			while (true)
			{
				int num = 297528583;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x24573071)) % 4)
					{
					case 0u:
						break;
					default:
						return;
					case 2u:
						UeSkHcBnUDZDTHkgfGCVbjKIjUXtA = serviceUpdater ?? throw new ArgumentNullException("serviceUpdater");
						num = (int)((num2 * 1337216374) ^ 0x2CC1F3F4);
						continue;
					case 1u:
						serviceUpdater.RegisterService(this);
						num = ((int)num2 * -975680057) ^ -1559574083;
						continue;
					case 3u:
						return;
					}
					break;
				}
			}
		}

		public void Enqueue(Action callback)
		{
			urThmvUQMrBrmbWpTLAspYVIiCct urThmvUQMrBrmbWpTLAspYVIiCct2 = new urThmvUQMrBrmbWpTLAspYVIiCct();
			urThmvUQMrBrmbWpTLAspYVIiCct2.FHgBHyavfNdkhWMqoCudhMJnWCgE = callback;
			UrWwwkEVqlsCwuqAxyNaOnyUzodO();
			tJZHJjUlJouCbLPkpEWRALzTQaptA.Enqueue(urThmvUQMrBrmbWpTLAspYVIiCct2.xkMROJooiIZWxwfMqjcyEDhMRmaJ);
		}

		public void Enqueue<T>(Action<T> callback, T obj)
		{
			WhgqmVPNKQDOtGuysXpXhBxWInee<T> whgqmVPNKQDOtGuysXpXhBxWInee = new WhgqmVPNKQDOtGuysXpXhBxWInee<T>();
			whgqmVPNKQDOtGuysXpXhBxWInee.FHgBHyavfNdkhWMqoCudhMJnWCgE = callback;
			whgqmVPNKQDOtGuysXpXhBxWInee.lhuAiwnCeBNlNexPeZDjBlbXolZj = obj;
			UrWwwkEVqlsCwuqAxyNaOnyUzodO();
			tJZHJjUlJouCbLPkpEWRALzTQaptA.Enqueue(whgqmVPNKQDOtGuysXpXhBxWInee.xkMROJooiIZWxwfMqjcyEDhMRmaJ);
		}

		public void Enqueue<T1, T2>(Action<T1, T2> callback, T1 obj1, T2 obj2)
		{
			ZgpEGROawSeTOwGhoUpWgmONKEpF<T1, T2> zgpEGROawSeTOwGhoUpWgmONKEpF = new ZgpEGROawSeTOwGhoUpWgmONKEpF<T1, T2>();
			while (true)
			{
				int num = -218744839;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1977379693)) % 5)
					{
					case 3u:
						break;
					case 2u:
						zgpEGROawSeTOwGhoUpWgmONKEpF.FHgBHyavfNdkhWMqoCudhMJnWCgE = callback;
						num = ((int)num2 * -1836017443) ^ 0x49F22EC8;
						continue;
					case 0u:
						zgpEGROawSeTOwGhoUpWgmONKEpF.rBBzqDrIlGcHLlcqimEOcRrmPwEc = obj2;
						UrWwwkEVqlsCwuqAxyNaOnyUzodO();
						num = (int)((num2 * 2000219927) ^ 0x6BDFCC87);
						continue;
					case 1u:
						zgpEGROawSeTOwGhoUpWgmONKEpF.NpvDUQnzFgAeQOOPFZhPmdTITTBH = obj1;
						num = (int)(num2 * 1985184079) ^ -1939510520;
						continue;
					default:
						tJZHJjUlJouCbLPkpEWRALzTQaptA.Enqueue(zgpEGROawSeTOwGhoUpWgmONKEpF.xkMROJooiIZWxwfMqjcyEDhMRmaJ);
						return;
					}
					break;
				}
			}
		}

		public void Enqueue<T1, T2, T3>(Action<T1, T2, T3> callback, T1 obj1, T2 obj2, T3 obj3)
		{
			iGxKHhjXaZlfnmkEjHatbAVmeLNd<T1, T2, T3> iGxKHhjXaZlfnmkEjHatbAVmeLNd2 = new iGxKHhjXaZlfnmkEjHatbAVmeLNd<T1, T2, T3>();
			while (true)
			{
				int num = 1340277440;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x2D8B55D5)) % 3)
					{
					case 0u:
						break;
					case 1u:
						goto IL_0028;
					default:
						iGxKHhjXaZlfnmkEjHatbAVmeLNd2.cFRuPWNIeCmbYBVHuJUMMwLdasBL = obj3;
						UrWwwkEVqlsCwuqAxyNaOnyUzodO();
						tJZHJjUlJouCbLPkpEWRALzTQaptA.Enqueue(iGxKHhjXaZlfnmkEjHatbAVmeLNd2.xkMROJooiIZWxwfMqjcyEDhMRmaJ);
						return;
					}
					break;
					IL_0028:
					iGxKHhjXaZlfnmkEjHatbAVmeLNd2.FHgBHyavfNdkhWMqoCudhMJnWCgE = callback;
					iGxKHhjXaZlfnmkEjHatbAVmeLNd2.NpvDUQnzFgAeQOOPFZhPmdTITTBH = obj1;
					iGxKHhjXaZlfnmkEjHatbAVmeLNd2.rBBzqDrIlGcHLlcqimEOcRrmPwEc = obj2;
					num = (int)(num2 * 546109093) ^ -1239124086;
				}
			}
		}

		void IUpdateableService.Update()
		{
			UrWwwkEVqlsCwuqAxyNaOnyUzodO();
			Action result = default(Action);
			while (true)
			{
				int num = 615559649;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x6FB0DC72)) % 6)
					{
					case 0u:
						break;
					default:
						return;
					case 5u:
						result();
						num = (int)((num2 * 1064100967) ^ 0x67783295);
						continue;
					case 4u:
						num = (int)((num2 * 79390534) ^ 0x1A15EEF5);
						continue;
					case 1u:
					{
						int num5;
						if (!tJZHJjUlJouCbLPkpEWRALzTQaptA.TryDequeue(out result))
						{
							num = 991523762;
							num5 = num;
						}
						else
						{
							num = 969676351;
							num5 = num;
						}
						continue;
					}
					case 3u:
					{
						int num3;
						int num4;
						if (result == null)
						{
							num3 = -1491308080;
							num4 = num3;
						}
						else
						{
							num3 = -896671156;
							num4 = num3;
						}
						num = num3 ^ ((int)num2 * -1119553675);
						continue;
					}
					case 2u:
						return;
					}
					break;
				}
			}
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
				switch ((num = 328519936u) % 3)
				{
				case 2u:
					break;
				default:
					return;
				case 1u:
					throw new ObjectDisposedException(GetType().FullName);
				case 0u:
					return;
				}
			}
		}

		public void Dispose()
		{
			if (tlRiOzSchvnYldbxynOFisSYraBV)
			{
				while (true)
				{
					uint num;
					switch ((num = 414839333u) % 3)
					{
					case 0u:
						continue;
					case 2u:
						return;
					}
					break;
				}
			}
			tlRiOzSchvnYldbxynOFisSYraBV = true;
			UeSkHcBnUDZDTHkgfGCVbjKIjUXtA.DeregisterService(this);
		}
	}
}
