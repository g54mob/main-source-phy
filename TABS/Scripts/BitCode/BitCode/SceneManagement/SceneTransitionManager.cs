using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BitCode.AssetManagement;
using BitCode.Extensions;
using CySSvAMKspxRBYRzSLeYjkLjCHJQ;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

namespace BitCode.SceneManagement
{
	public class SceneTransitionManager : IDisposable, IUpdateableService
	{
		private const int wCBcCotPQbGJnvFgaSHwVoFVEeqP = 4;

		private readonly string rQEhrSRVNzNvuqRHrRZuXDALGtqY;

		private readonly ISceneTransition OsWFcMAIWbOgEAcEmKjVDFGFERudc;

		private readonly IResourceManager hzPfAnSIEgQZmSOzKlgkfDGKCDnC;

		private readonly bool NnygTNPycIIXnrPQVprWpEjWikIdA;

		private readonly Queue<ILoadTask> zRxKICWtnMXwEGiTQjbbZXVifbWK;

		private readonly IServiceUpdater UeSkHcBnUDZDTHkgfGCVbjKIjUXtA;

		private ILoadTask QidrGbebPwbtcylSqujGSUsmmFkM;

		private ILoadTask SYvzBbKSRXYTQEJIDDAeHMRAtedPA;

		private string hZRHCAmiDMAtwWoFDxVQjBZguuAp;

		private Action gztllmiFMZkmmZGhEgwngiXIKNKsA;

		private Action XOzrcUrxXdAAQnlvKhBihiTdirNP;

		private bool yXFBYdvoqFhsJpMHbrdEbMLTdtiv;

		private bool tlRiOzSchvnYldbxynOFisSYraBV;

		[CompilerGenerated]
		private bool dzHDDyeGzGUWoImefOhKiircodftA;

		[CompilerGenerated]
		private bool HAGFxbxRSjZKhZxSGrpLzLhWFDqg;

		[CompilerGenerated]
		private bool qeKBGPNjLtnUHfkfIBsLGBCodsrh;

		public bool Transitioning
		{
			[CompilerGenerated]
			get
			{
				return dzHDDyeGzGUWoImefOhKiircodftA;
			}
			[CompilerGenerated]
			private set
			{
				dzHDDyeGzGUWoImefOhKiircodftA = flag;
			}
		}

		public bool InLoadingScene
		{
			[CompilerGenerated]
			get
			{
				return HAGFxbxRSjZKhZxSGrpLzLhWFDqg;
			}
			[CompilerGenerated]
			private set
			{
				HAGFxbxRSjZKhZxSGrpLzLhWFDqg = hAGFxbxRSjZKhZxSGrpLzLhWFDqg;
			}
		}

		public bool HasStartedLoadingFinalScene
		{
			[CompilerGenerated]
			get
			{
				return qeKBGPNjLtnUHfkfIBsLGBCodsrh;
			}
			[CompilerGenerated]
			private set
			{
				qeKBGPNjLtnUHfkfIBsLGBCodsrh = flag;
			}
		}

		public int LoadTaskCount => zRxKICWtnMXwEGiTQjbbZXVifbWK.Count;

		public SceneTransitionManager([NotNull] IServiceUpdater serviceUpdater, [NotNull] string loadingSceneName, [NotNull] ISceneTransition transition, [CanBeNull] IResourceManager resourceManager = null, bool syncLoadEvenWithLoadingScene = false, int initialLoadTaskQueueSize = 4)
		{
			while (true)
			{
				int num = -1920947948;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -967735357)) % 9)
					{
					case 7u:
						break;
					default:
						return;
					case 2u:
					{
						int num3;
						int num4;
						if (!string.IsNullOrEmpty(loadingSceneName))
						{
							num3 = -931692498;
							num4 = num3;
						}
						else
						{
							num3 = -1745558119;
							num4 = num3;
						}
						num = num3 ^ (int)(num2 * 2104009192);
						continue;
					}
					case 0u:
						UeSkHcBnUDZDTHkgfGCVbjKIjUXtA = serviceUpdater ?? throw new ArgumentNullException("serviceUpdater");
						num = (int)(num2 * 1286300902) ^ -875554914;
						continue;
					case 4u:
						SceneManager.sceneLoaded += wvawQrtbsLXfmuzlfcvJiYdJLKuD;
						num = ((int)num2 * -464981758) ^ 0x10CFB86;
						continue;
					case 1u:
						serviceUpdater.RegisterService(this);
						num = ((int)num2 * -464420022) ^ 0x6E8AFF6D;
						continue;
					case 5u:
						NnygTNPycIIXnrPQVprWpEjWikIdA = syncLoadEvenWithLoadingScene;
						hzPfAnSIEgQZmSOzKlgkfDGKCDnC = resourceManager;
						zRxKICWtnMXwEGiTQjbbZXVifbWK = new Queue<ILoadTask>(initialLoadTaskQueueSize);
						num = ((int)num2 * -1356923097) ^ 0x210BAA64;
						continue;
					case 6u:
						throw new ArgumentException("Value cannot be null or empty.", "loadingSceneName");
					case 3u:
						rQEhrSRVNzNvuqRHrRZuXDALGtqY = loadingSceneName;
						OsWFcMAIWbOgEAcEmKjVDFGFERudc = transition ?? throw new ArgumentNullException("transition");
						num = -1412092847;
						continue;
					case 8u:
						return;
					}
					break;
				}
			}
		}

		public void EnqueueLoadTask([NotNull] ILoadTask task)
		{
			UrWwwkEVqlsCwuqAxyNaOnyUzodO();
			if (task == null)
			{
				goto IL_0009;
			}
			goto IL_0058;
			IL_0009:
			int num = 1008298289;
			goto IL_000e;
			IL_000e:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0xABA55C3)) % 7)
				{
				case 4u:
					break;
				default:
					return;
				case 5u:
					throw new InvalidOperationException("Can't queue more load tasks once the final scene load has started.");
				case 3u:
					goto IL_0058;
				case 0u:
					zRxKICWtnMXwEGiTQjbbZXVifbWK.Enqueue(task);
					num = 589953327;
					continue;
				case 1u:
					throw new ArgumentNullException("task");
				case 2u:
				{
					int num3;
					int num4;
					if (!HasStartedLoadingFinalScene)
					{
						num3 = 317440705;
						num4 = num3;
					}
					else
					{
						num3 = 2074938591;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 1802625985);
					continue;
				}
				case 6u:
					return;
				}
				break;
			}
			goto IL_0009;
			IL_0058:
			int num5;
			if (!Transitioning)
			{
				num = 1261368486;
				num5 = num;
			}
			else
			{
				num = 440848868;
				num5 = num;
			}
			goto IL_000e;
		}

		public void ChangeScene([NotNull] string sceneName, bool useLoadingScene = true, [CanBeNull] Action transitionCompleted = null, [CanBeNull] Action loadingSceneTransitionCompleted = null)
		{
			UrWwwkEVqlsCwuqAxyNaOnyUzodO();
			Action sceneSwitch = default(Action);
			while (true)
			{
				int num = -1244320643;
				while (true)
				{
					uint num2;
					Action action;
					switch ((num2 = (uint)(num ^ -1717701006)) % 15)
					{
					case 13u:
						break;
					default:
						return;
					case 5u:
						Transitioning = true;
						hZRHCAmiDMAtwWoFDxVQjBZguuAp = sceneName;
						num = -1512032277;
						continue;
					case 11u:
						sceneSwitch = OAWrOBYXkyrApCbDxjuJRAcnNaKf;
						num = -670925373;
						continue;
					case 9u:
					{
						XOzrcUrxXdAAQnlvKhBihiTdirNP = loadingSceneTransitionCompleted;
						int num6;
						int num7;
						if (!useLoadingScene)
						{
							num6 = 2017352481;
							num7 = num6;
						}
						else
						{
							num6 = 996272392;
							num7 = num6;
						}
						num = num6 ^ (int)(num2 * 1701926720);
						continue;
					}
					case 1u:
						if (!NnygTNPycIIXnrPQVprWpEjWikIdA)
						{
							num = (int)(num2 * 2001407853) ^ -1151675866;
							continue;
						}
						action = rjZqkwMENRloNYmckRHXEyRgvXGm;
						goto IL_01bd;
					case 4u:
					{
						int num4;
						int num5;
						if (!string.IsNullOrEmpty(sceneName))
						{
							num4 = 925418787;
							num5 = num4;
						}
						else
						{
							num4 = 1293876583;
							num5 = num4;
						}
						num = num4 ^ ((int)num2 * -1633465419);
						continue;
					}
					case 14u:
						num = (int)((num2 * 1961852598) ^ 0x17D22B01);
						continue;
					case 12u:
						gztllmiFMZkmmZGhEgwngiXIKNKsA = transitionCompleted;
						num = ((int)num2 * -340367940) ^ 0x23B5E6FE;
						continue;
					case 3u:
						throw new InvalidOperationException("Can't load a scene while a transition is underway.");
					case 10u:
						throw new ArgumentException("Value cannot be null or empty.", "sceneName");
					case 6u:
						yXFBYdvoqFhsJpMHbrdEbMLTdtiv = false;
						num = (int)(num2 * 876782727) ^ -1386208859;
						continue;
					case 0u:
						OsWFcMAIWbOgEAcEmKjVDFGFERudc.StartTransition(sceneSwitch, useLoadingScene);
						num = -405049105;
						continue;
					case 2u:
					{
						int num3;
						if (Transitioning)
						{
							num = -2010172732;
							num3 = num;
						}
						else
						{
							num = -1575432791;
							num3 = num;
						}
						continue;
					}
					case 8u:
						action = fTseJsYqMfPEWinSyjZSDDPbwymR;
						goto IL_01bd;
					case 7u:
						return;
						IL_01bd:
						sceneSwitch = action;
						num = -1192563071;
						continue;
					}
					break;
				}
			}
		}

		private void wvawQrtbsLXfmuzlfcvJiYdJLKuD(Scene P_0, LoadSceneMode P_1)
		{
			if (string.Equals(P_0.name, rQEhrSRVNzNvuqRHrRZuXDALGtqY, StringComparison.InvariantCultureIgnoreCase))
			{
				goto IL_0015;
			}
			goto IL_0069;
			IL_0015:
			int num = -1004615833;
			goto IL_001a;
			IL_001a:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -836293466)) % 9)
				{
				case 2u:
					break;
				default:
					return;
				case 3u:
					InLoadingScene = true;
					num = (int)(num2 * 577078547) ^ -1603462903;
					continue;
				case 6u:
					goto IL_0069;
				case 8u:
					OsWFcMAIWbOgEAcEmKjVDFGFERudc.EnteredFinalScene(NGoGnwulIFyMwADQReDsbiOgfkuW);
					num = ((int)num2 * -1090661244) ^ 0x2D130840;
					continue;
				case 7u:
					HasStartedLoadingFinalScene = false;
					InLoadingScene = false;
					num = -389989901;
					continue;
				case 1u:
					QidrGbebPwbtcylSqujGSUsmmFkM.Complete();
					QidrGbebPwbtcylSqujGSUsmmFkM = null;
					num = (int)((num2 * 2039648784) ^ 0x232EDCD5);
					continue;
				case 0u:
					OsWFcMAIWbOgEAcEmKjVDFGFERudc.EnteredLoadingScene(izFUzniKigoAzLgzPLdfGzuktxDg);
					return;
				case 4u:
				{
					int num3;
					int num4;
					if (QidrGbebPwbtcylSqujGSUsmmFkM != null)
					{
						num3 = 1548085397;
						num4 = num3;
					}
					else
					{
						num3 = 1856254837;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 605107816);
					continue;
				}
				case 5u:
					return;
				}
				break;
			}
			goto IL_0015;
			IL_0069:
			int num5;
			if (!string.Equals(P_0.name, hZRHCAmiDMAtwWoFDxVQjBZguuAp, StringComparison.InvariantCultureIgnoreCase))
			{
				num = -295867500;
				num5 = num;
			}
			else
			{
				num = -90871012;
				num5 = num;
			}
			goto IL_001a;
		}

		private void NGoGnwulIFyMwADQReDsbiOgfkuW()
		{
			Transitioning = false;
			Action action = gztllmiFMZkmmZGhEgwngiXIKNKsA;
			gztllmiFMZkmmZGhEgwngiXIKNKsA = null;
			action?.SafelyInvoke();
		}

		private void izFUzniKigoAzLgzPLdfGzuktxDg()
		{
			Action xOzrcUrxXdAAQnlvKhBihiTdirNP = XOzrcUrxXdAAQnlvKhBihiTdirNP;
			XOzrcUrxXdAAQnlvKhBihiTdirNP = null;
			xOzrcUrxXdAAQnlvKhBihiTdirNP?.SafelyInvoke();
			ILoadTask sYvzBbKSRXYTQEJIDDAeHMRAtedPA;
			if (hzPfAnSIEgQZmSOzKlgkfDGKCDnC != null)
			{
				ILoadTask loadTask = new UnDqFhpmJCYmxlmZblDWIHWOKkFK(hzPfAnSIEgQZmSOzKlgkfDGKCDnC, hZRHCAmiDMAtwWoFDxVQjBZguuAp);
				sYvzBbKSRXYTQEJIDDAeHMRAtedPA = loadTask;
			}
			else
			{
				ILoadTask loadTask = new pPRNDTtcgXIHRSOpCFfdUCDtqZIJ(hZRHCAmiDMAtwWoFDxVQjBZguuAp);
				sYvzBbKSRXYTQEJIDDAeHMRAtedPA = loadTask;
			}
			SYvzBbKSRXYTQEJIDDAeHMRAtedPA = sYvzBbKSRXYTQEJIDDAeHMRAtedPA;
		}

		private void OAWrOBYXkyrApCbDxjuJRAcnNaKf()
		{
			while (true)
			{
				int num;
				int num2;
				if (zRxKICWtnMXwEGiTQjbbZXVifbWK.Count <= 0)
				{
					num = -598545769;
					num2 = num;
				}
				else
				{
					num = -1756100406;
					num2 = num;
				}
				while (true)
				{
					uint num3;
					switch ((num3 = (uint)(num ^ -566299913)) % 7)
					{
					case 0u:
						num = -1756100406;
						continue;
					case 3u:
						return;
					case 5u:
						break;
					case 2u:
					{
						ILoadTask loadTask = zRxKICWtnMXwEGiTQjbbZXVifbWK.Dequeue();
						loadTask.Start(async: false);
						loadTask.Complete();
						num = -977431659;
						continue;
					}
					case 1u:
						hzPfAnSIEgQZmSOzKlgkfDGKCDnC.LoadScene(hZRHCAmiDMAtwWoFDxVQjBZguuAp, async: false);
						num = ((int)num3 * -904323893) ^ 0x3ED3CF19;
						continue;
					case 4u:
					{
						int num4;
						int num5;
						if (hzPfAnSIEgQZmSOzKlgkfDGKCDnC == null)
						{
							num4 = -1011860117;
							num5 = num4;
						}
						else
						{
							num4 = -1205840538;
							num5 = num4;
						}
						num = num4 ^ (int)(num3 * 698223502);
						continue;
					}
					default:
						SceneManager.LoadScene(hZRHCAmiDMAtwWoFDxVQjBZguuAp);
						return;
					}
					break;
				}
			}
		}

		private void rjZqkwMENRloNYmckRHXEyRgvXGm()
		{
			if (hzPfAnSIEgQZmSOzKlgkfDGKCDnC != null)
			{
				goto IL_0008;
			}
			goto IL_0054;
			IL_0008:
			int num = -373923287;
			goto IL_000d;
			IL_000d:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -736985752)) % 5)
				{
				case 0u:
					break;
				default:
					return;
				case 1u:
					hzPfAnSIEgQZmSOzKlgkfDGKCDnC.LoadScene(rQEhrSRVNzNvuqRHrRZuXDALGtqY, async: false);
					num = ((int)num2 * -2047970490) ^ -1023936329;
					continue;
				case 2u:
					goto IL_0054;
				case 4u:
					return;
				case 3u:
					return;
				}
				break;
			}
			goto IL_0008;
			IL_0054:
			SceneManager.LoadScene(rQEhrSRVNzNvuqRHrRZuXDALGtqY);
			num = -1183519832;
			goto IL_000d;
		}

		private void fTseJsYqMfPEWinSyjZSDDPbwymR()
		{
			if (hzPfAnSIEgQZmSOzKlgkfDGKCDnC != null)
			{
				while (true)
				{
					uint num;
					switch ((num = 1158948466u) % 3)
					{
					case 0u:
						continue;
					case 1u:
						hzPfAnSIEgQZmSOzKlgkfDGKCDnC.LoadScene(rQEhrSRVNzNvuqRHrRZuXDALGtqY);
						return;
					}
					break;
				}
			}
			SceneManager.LoadSceneAsync(rQEhrSRVNzNvuqRHrRZuXDALGtqY);
		}

		void IUpdateableService.Update()
		{
			if (!InLoadingScene)
			{
				goto IL_000b;
			}
			goto IL_0148;
			IL_000b:
			int num = 1009628444;
			goto IL_0010;
			IL_0010:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x6F741137)) % 21)
				{
				case 14u:
					break;
				default:
					return;
				case 20u:
					EnqueueLoadTask(SYvzBbKSRXYTQEJIDDAeHMRAtedPA);
					num = (int)((num2 * 157713067) ^ 0x7362028B);
					continue;
				case 8u:
				{
					int num5;
					int num6;
					if (QidrGbebPwbtcylSqujGSUsmmFkM is UnDqFhpmJCYmxlmZblDWIHWOKkFK)
					{
						num5 = 233026295;
						num6 = num5;
					}
					else
					{
						num5 = 1649541729;
						num6 = num5;
					}
					num = num5 ^ ((int)num2 * -1126131444);
					continue;
				}
				case 7u:
					SYvzBbKSRXYTQEJIDDAeHMRAtedPA = null;
					num = (int)((num2 * 1177825679) ^ 0xD89EF01);
					continue;
				case 17u:
					QidrGbebPwbtcylSqujGSUsmmFkM.Complete();
					QidrGbebPwbtcylSqujGSUsmmFkM = null;
					num = (int)(num2 * 1281843900) ^ -1741878779;
					continue;
				case 3u:
				{
					int num13;
					int num14;
					if (!(QidrGbebPwbtcylSqujGSUsmmFkM is pPRNDTtcgXIHRSOpCFfdUCDtqZIJ))
					{
						num13 = -149769000;
						num14 = num13;
					}
					else
					{
						num13 = -472936881;
						num14 = num13;
					}
					num = num13 ^ ((int)num2 * -594237174);
					continue;
				}
				case 4u:
					goto IL_0126;
				case 10u:
					goto IL_0148;
				case 5u:
				{
					int num9;
					int num10;
					if (SYvzBbKSRXYTQEJIDDAeHMRAtedPA != null)
					{
						num9 = -2057423827;
						num10 = num9;
					}
					else
					{
						num9 = -1982090757;
						num10 = num9;
					}
					num = num9 ^ (int)(num2 * 1228625701);
					continue;
				}
				case 6u:
					HasStartedLoadingFinalScene = true;
					num = (int)((num2 * 1869989054) ^ 0x19FD5F3C);
					continue;
				case 2u:
				{
					int num11;
					int num12;
					if (zRxKICWtnMXwEGiTQjbbZXVifbWK.Count > 0)
					{
						num11 = -842117214;
						num12 = num11;
					}
					else
					{
						num11 = -1799567946;
						num12 = num11;
					}
					num = num11 ^ ((int)num2 * -1358306817);
					continue;
				}
				case 19u:
					_ = yXFBYdvoqFhsJpMHbrdEbMLTdtiv;
					num = 1356729112;
					continue;
				case 13u:
				{
					int num7;
					int num8;
					if (zRxKICWtnMXwEGiTQjbbZXVifbWK.Count != 0)
					{
						num7 = 1682880810;
						num8 = num7;
					}
					else
					{
						num7 = 1907750490;
						num8 = num7;
					}
					num = num7 ^ ((int)num2 * -1858000600);
					continue;
				}
				case 12u:
					zRxKICWtnMXwEGiTQjbbZXVifbWK.Clear();
					num = ((int)num2 * -1897716977) ^ 0x1A2AC29;
					continue;
				case 11u:
				{
					int num3;
					int num4;
					if (!QidrGbebPwbtcylSqujGSUsmmFkM.IsDone)
					{
						num3 = -816870837;
						num4 = num3;
					}
					else
					{
						num3 = -475401539;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 1875467419);
					continue;
				}
				case 0u:
					goto IL_024b;
				case 1u:
					return;
				case 18u:
					QidrGbebPwbtcylSqujGSUsmmFkM = zRxKICWtnMXwEGiTQjbbZXVifbWK.Dequeue();
					QidrGbebPwbtcylSqujGSUsmmFkM.Start(!NnygTNPycIIXnrPQVprWpEjWikIdA);
					num = ((int)num2 * -614664002) ^ -1182084459;
					continue;
				case 9u:
					goto IL_02b1;
				case 16u:
					return;
				case 15u:
					return;
				}
				break;
				IL_02b1:
				int num15;
				if (QidrGbebPwbtcylSqujGSUsmmFkM == null)
				{
					num = 675143290;
					num15 = num;
				}
				else
				{
					num = 1523707653;
					num15 = num;
				}
				continue;
				IL_024b:
				int num16;
				if (QidrGbebPwbtcylSqujGSUsmmFkM == null)
				{
					num = 992417668;
					num16 = num;
				}
				else
				{
					num = 796040350;
					num16 = num;
				}
				continue;
				IL_0126:
				int num17;
				if (zRxKICWtnMXwEGiTQjbbZXVifbWK.Count <= 0)
				{
					num = 1523707653;
					num17 = num;
				}
				else
				{
					num = 385813283;
					num17 = num;
				}
			}
			goto IL_000b;
			IL_0148:
			int num18;
			if (QidrGbebPwbtcylSqujGSUsmmFkM == null)
			{
				num = 513679070;
				num18 = num;
			}
			else
			{
				num = 671708226;
				num18 = num;
			}
			goto IL_0010;
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
				switch ((num = 737728597u) % 3)
				{
				case 0u:
					break;
				default:
					return;
				case 1u:
					throw new ObjectDisposedException(GetType().FullName);
				case 2u:
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
				int num = -855039905;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -615373757)) % 3)
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
					SceneManager.sceneLoaded -= wvawQrtbsLXfmuzlfcvJiYdJLKuD;
					num = (int)(num2 * 1998882098) ^ -1397349237;
				}
			}
		}

		public void Dispose()
		{
			UeSkHcBnUDZDTHkgfGCVbjKIjUXtA.DeregisterService(this);
			while (true)
			{
				int num = -1086048955;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -487806630)) % 3)
					{
					case 0u:
						break;
					case 1u:
						goto IL_002e;
					default:
						tlRiOzSchvnYldbxynOFisSYraBV = true;
						return;
					}
					break;
					IL_002e:
					FMrXgrmCIGmTIAmtWxyMsbxVOWEF();
					GC.SuppressFinalize(this);
					num = ((int)num2 * -1266010757) ^ 0x72667801;
				}
			}
		}

		~SceneTransitionManager()
		{
			FMrXgrmCIGmTIAmtWxyMsbxVOWEF();
		}
	}
}
