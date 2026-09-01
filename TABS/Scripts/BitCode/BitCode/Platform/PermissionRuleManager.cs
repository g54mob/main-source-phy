using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BitCode.Users;

namespace BitCode.Platform
{
	public sealed class PermissionRuleManager<TGameFeature, TPlatformPermission> : IPlatformService, IPlatformPermissionRuleManager<TPlatformPermission>, IPermissionRuleManager<TGameFeature>
	{
		[StructLayout(LayoutKind.Auto)]
		private struct AikoJGDayZfRNoNRQpRVTcRTCFii : IAsyncStateMachine
		{
			public int dagGWVjAonzlEQhnHtJbFGTLQUwi;

			public AsyncTaskMethodBuilder<IPermissionResult> VVEGRPiIETvwaByBfDgZFHzJPzZZ;

			public PermissionRuleManager<TGameFeature, TPlatformPermission> vvKNDIxiYKTrRTKAccPPCIdmGSFtA;

			public TGameFeature cyvZaxOTuDemSudcbwVoIQTtEEuS;

			public ILocalAccount sKxkSYTWUSIsbkXngRDdQfwEBOKM;

			private IEnumerator<TPlatformPermission> BVCdlUXHezocQErtLHwjfwmllzYH;

			private TaskAwaiter<PermissionResult<TPlatformPermission>> ZXmTiyGvYGuRDpIPfYHtBgPoRgDC;

			private void MoveNext()
			{
				int num = dagGWVjAonzlEQhnHtJbFGTLQUwi;
				PermissionRuleManager<TGameFeature, TPlatformPermission> permissionRuleManager = vvKNDIxiYKTrRTKAccPPCIdmGSFtA;
				IPermissionResult result = default(IPermissionResult);
				try
				{
					if (num == 0)
					{
						goto IL_0100;
					}
					TPlatformPermission permission = default(TPlatformPermission);
					while (true)
					{
						int num2 = -2072509343;
						while (true)
						{
							uint num3;
							switch ((num3 = (uint)(num2 ^ -592859539)) % 7)
							{
							case 5u:
								break;
							case 3u:
								goto end_IL_0014;
							case 0u:
								BVCdlUXHezocQErtLHwjfwmllzYH = permissionRuleManager.LRzYhZbvCyyQNXaWopDzXgzgDxDN.RfSBXdiNhBODkVNLxjedXNtmrUVoA(cyvZaxOTuDemSudcbwVoIQTtEEuS).GetEnumerator();
								num2 = -945856780;
								continue;
							case 1u:
							{
								int num4;
								int num5;
								if (permissionRuleManager.LRzYhZbvCyyQNXaWopDzXgzgDxDN.oWMHqILhVIdphjksFHygeMZEzmvxA(cyvZaxOTuDemSudcbwVoIQTtEEuS))
								{
									num4 = 1367169882;
									num5 = num4;
								}
								else
								{
									num4 = 1418737420;
									num5 = num4;
								}
								num2 = num4 ^ ((int)num3 * -1178795485);
								continue;
							}
							case 6u:
								result = new PermissionResult<TPlatformPermission>(permission, PermissionState.Granted, PermissionDetail.OhHJMYtkCasjeedEqCuNlufHZFXS(cyvZaxOTuDemSudcbwVoIQTtEEuS), sKxkSYTWUSIsbkXngRDdQfwEBOKM);
								num2 = (int)(num3 * 222374520) ^ -975193358;
								continue;
							case 4u:
								permission = default(TPlatformPermission);
								num2 = (int)((num3 * 268513827) ^ 0x4182AB7E);
								continue;
							default:
								goto IL_0100;
							}
							break;
						}
						continue;
						end_IL_0014:
						break;
					}
					goto end_IL_000e;
					IL_0100:
					try
					{
						if (num != 0)
						{
							goto IL_0106;
						}
						goto IL_01a7;
						IL_0106:
						int num6 = -347257336;
						goto IL_010b;
						IL_010b:
						PermissionResult<TPlatformPermission> result2 = default(PermissionResult<TPlatformPermission>);
						TaskAwaiter<PermissionResult<TPlatformPermission>> awaiter = default(TaskAwaiter<PermissionResult<TPlatformPermission>>);
						TPlatformPermission current = default(TPlatformPermission);
						while (true)
						{
							uint num3;
							switch ((num3 = (uint)(num6 ^ -592859539)) % 15)
							{
							case 5u:
								break;
							default:
								goto end_IL_0100;
							case 1u:
								num6 = ((int)num3 * -1032269713) ^ 0x38947492;
								continue;
							case 4u:
								num = (dagGWVjAonzlEQhnHtJbFGTLQUwi = -1);
								num6 = (int)(num3 * 2022294947) ^ -876101012;
								continue;
							case 9u:
								goto IL_0186;
							case 7u:
								goto IL_01a7;
							case 14u:
								goto end_IL_000e;
							case 8u:
								ZXmTiyGvYGuRDpIPfYHtBgPoRgDC = default(TaskAwaiter<PermissionResult<TPlatformPermission>>);
								num6 = ((int)num3 * -534750590) ^ -554843741;
								continue;
							case 11u:
								result = result2;
								num6 = (int)(num3 * 313517510) ^ -1486726073;
								continue;
							case 13u:
								num = (dagGWVjAonzlEQhnHtJbFGTLQUwi = 0);
								num6 = ((int)num3 * -976291612) ^ 0x878EBB3;
								continue;
							case 6u:
								VVEGRPiIETvwaByBfDgZFHzJPzZZ.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							case 0u:
								ZXmTiyGvYGuRDpIPfYHtBgPoRgDC = awaiter;
								num6 = ((int)num3 * -522377131) ^ 0x50C22A2;
								continue;
							case 3u:
							{
								awaiter = permissionRuleManager.mwAmFHLRUnLLaGqPQhrBbAAhHdzDA.HasPermissionAsync(sKxkSYTWUSIsbkXngRDdQfwEBOKM, current).GetAwaiter();
								int num7;
								int num8;
								if (awaiter.IsCompleted)
								{
									num7 = 1329708837;
									num8 = num7;
								}
								else
								{
									num7 = 85921094;
									num8 = num7;
								}
								num6 = num7 ^ (int)(num3 * 149149503);
								continue;
							}
							case 12u:
								goto IL_02a8;
							case 2u:
								current = BVCdlUXHezocQErtLHwjfwmllzYH.Current;
								num6 = -1917816214;
								continue;
							case 10u:
								goto end_IL_0100;
							}
							break;
							IL_02a8:
							result2 = awaiter.GetResult();
							int num9;
							if (!result2.HasPermission())
							{
								num6 = -1453000512;
								num9 = num6;
							}
							else
							{
								num6 = -1963022503;
								num9 = num6;
							}
							continue;
							IL_0186:
							int num10;
							if (BVCdlUXHezocQErtLHwjfwmllzYH.MoveNext())
							{
								num6 = -213042453;
								num10 = num6;
							}
							else
							{
								num6 = -1431299072;
								num10 = num6;
							}
						}
						goto IL_0106;
						IL_01a7:
						awaiter = ZXmTiyGvYGuRDpIPfYHtBgPoRgDC;
						num6 = -609392158;
						goto IL_010b;
						end_IL_0100:;
					}
					finally
					{
						if (num < 0)
						{
							while (true)
							{
								IL_02f0:
								int num11 = -237098238;
								while (true)
								{
									uint num3;
									switch ((num3 = (uint)(num11 ^ -592859539)) % 4)
									{
									case 0u:
										break;
									default:
										goto end_IL_02f5;
									case 3u:
									{
										int num12;
										int num13;
										if (BVCdlUXHezocQErtLHwjfwmllzYH == null)
										{
											num12 = 1621778401;
											num13 = num12;
										}
										else
										{
											num12 = 638567306;
											num13 = num12;
										}
										num11 = num12 ^ (int)(num3 * 445809442);
										continue;
									}
									case 1u:
										BVCdlUXHezocQErtLHwjfwmllzYH.Dispose();
										num11 = ((int)num3 * -220965025) ^ -2070764968;
										continue;
									case 2u:
										goto end_IL_02f5;
									}
									goto IL_02f0;
									continue;
									end_IL_02f5:
									break;
								}
								break;
							}
						}
					}
					BVCdlUXHezocQErtLHwjfwmllzYH = null;
					while (true)
					{
						IL_035c:
						int num14 = -686063904;
						while (true)
						{
							uint num3;
							switch ((num3 = (uint)(num14 ^ -592859539)) % 4)
							{
							case 3u:
								break;
							default:
								goto end_IL_0361;
							case 1u:
								permission = default(TPlatformPermission);
								num14 = (int)((num3 * 78300889) ^ 0x66BFAE1C);
								continue;
							case 0u:
								result = new PermissionResult<TPlatformPermission>(permission, PermissionState.Granted, sKxkSYTWUSIsbkXngRDdQfwEBOKM);
								num14 = ((int)num3 * -883805824) ^ 0x7B3D0853;
								continue;
							case 2u:
								goto end_IL_0361;
							}
							goto IL_035c;
							continue;
							end_IL_0361:
							break;
						}
						break;
					}
					end_IL_000e:;
				}
				catch (Exception exception)
				{
					while (true)
					{
						int num15 = -1617414312;
						while (true)
						{
							uint num3;
							switch ((num3 = (uint)(num15 ^ -592859539)) % 3)
							{
							case 2u:
								break;
							case 1u:
								goto IL_03e6;
							default:
								VVEGRPiIETvwaByBfDgZFHzJPzZZ.SetException(exception);
								return;
							}
							break;
							IL_03e6:
							dagGWVjAonzlEQhnHtJbFGTLQUwi = -2;
							num15 = ((int)num3 * -2137847120) ^ 0x58924930;
						}
					}
				}
				dagGWVjAonzlEQhnHtJbFGTLQUwi = -2;
				while (true)
				{
					int num16 = -1743616961;
					while (true)
					{
						uint num3;
						switch ((num3 = (uint)(num16 ^ -592859539)) % 3)
						{
						case 2u:
							break;
						default:
							return;
						case 1u:
							goto IL_0438;
						case 0u:
							return;
						}
						break;
						IL_0438:
						VVEGRPiIETvwaByBfDgZFHzJPzZZ.SetResult(result);
						num16 = ((int)num3 * -829834888) ^ -668127602;
					}
				}
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				VVEGRPiIETvwaByBfDgZFHzJPzZZ.SetStateMachine(stateMachine);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout(LayoutKind.Auto)]
		private struct IFrOAAItOnacBsBEtAySDuarPORbA : IAsyncStateMachine
		{
			public int dagGWVjAonzlEQhnHtJbFGTLQUwi;

			public AsyncTaskMethodBuilder<IPermissionResult> VVEGRPiIETvwaByBfDgZFHzJPzZZ;

			public PermissionRuleManager<TGameFeature, TPlatformPermission> vvKNDIxiYKTrRTKAccPPCIdmGSFtA;

			public TGameFeature zIctoCBCLEIlqCVGvkypEGazzmQBA;

			public ILocalAccount sKxkSYTWUSIsbkXngRDdQfwEBOKM;

			public IRemoteAccount ZZyzxirGbCGzXEkNGxNvjLWelQwE;

			private IEnumerator<TPlatformPermission> BVCdlUXHezocQErtLHwjfwmllzYH;

			private TaskAwaiter<PermissionResult<TPlatformPermission>> ZXmTiyGvYGuRDpIPfYHtBgPoRgDC;

			private void MoveNext()
			{
				int num = dagGWVjAonzlEQhnHtJbFGTLQUwi;
				PermissionRuleManager<TGameFeature, TPlatformPermission> permissionRuleManager = vvKNDIxiYKTrRTKAccPPCIdmGSFtA;
				IPermissionResult result = default(IPermissionResult);
				try
				{
					if (num == 0)
					{
						goto IL_0103;
					}
					TPlatformPermission permission = default(TPlatformPermission);
					while (true)
					{
						int num2 = 1486268672;
						while (true)
						{
							uint num3;
							switch ((num3 = (uint)(num2 ^ 0x41021362)) % 7)
							{
							case 0u:
								break;
							case 1u:
							{
								int num4;
								int num5;
								if (!permissionRuleManager.LRzYhZbvCyyQNXaWopDzXgzgDxDN.oWMHqILhVIdphjksFHygeMZEzmvxA(zIctoCBCLEIlqCVGvkypEGazzmQBA))
								{
									num4 = -1098751392;
									num5 = num4;
								}
								else
								{
									num4 = -375614205;
									num5 = num4;
								}
								num2 = num4 ^ (int)(num3 * 1011450339);
								continue;
							}
							case 5u:
								result = new PermissionResult<TPlatformPermission>(permission, PermissionState.Granted, PermissionDetail.OhHJMYtkCasjeedEqCuNlufHZFXS(zIctoCBCLEIlqCVGvkypEGazzmQBA), sKxkSYTWUSIsbkXngRDdQfwEBOKM);
								num2 = (int)(num3 * 622274607) ^ -970490210;
								continue;
							case 6u:
								BVCdlUXHezocQErtLHwjfwmllzYH = permissionRuleManager.LRzYhZbvCyyQNXaWopDzXgzgDxDN.RfSBXdiNhBODkVNLxjedXNtmrUVoA(zIctoCBCLEIlqCVGvkypEGazzmQBA).GetEnumerator();
								num2 = 2073168626;
								continue;
							case 4u:
								goto end_IL_0014;
							case 3u:
								permission = default(TPlatformPermission);
								num2 = (int)((num3 * 1574356368) ^ 0x30050758);
								continue;
							default:
								goto IL_0103;
							}
							break;
						}
						continue;
						end_IL_0014:
						break;
					}
					goto end_IL_000e;
					IL_0103:
					try
					{
						if (num != 0)
						{
							goto IL_0109;
						}
						goto IL_027c;
						IL_0109:
						int num6 = 879724872;
						goto IL_010e;
						IL_010e:
						TaskAwaiter<PermissionResult<TPlatformPermission>> awaiter = default(TaskAwaiter<PermissionResult<TPlatformPermission>>);
						PermissionResult<TPlatformPermission> result2 = default(PermissionResult<TPlatformPermission>);
						TPlatformPermission current = default(TPlatformPermission);
						while (true)
						{
							uint num3;
							switch ((num3 = (uint)(num6 ^ 0x41021362)) % 15)
							{
							case 0u:
								break;
							default:
								goto end_IL_0103;
							case 3u:
								ZXmTiyGvYGuRDpIPfYHtBgPoRgDC = awaiter;
								num6 = ((int)num3 * -1852464773) ^ -638049379;
								continue;
							case 10u:
								goto end_IL_000e;
							case 1u:
								VVEGRPiIETvwaByBfDgZFHzJPzZZ.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							case 7u:
							{
								int num9;
								int num10;
								if (!result2.HasPermission())
								{
									num9 = -1044750835;
									num10 = num9;
								}
								else
								{
									num9 = -240274093;
									num10 = num9;
								}
								num6 = num9 ^ (int)(num3 * 520332204);
								continue;
							}
							case 6u:
								result = result2;
								num6 = (int)(num3 * 895336874) ^ -1539986755;
								continue;
							case 8u:
								goto IL_01f9;
							case 5u:
							{
								awaiter = permissionRuleManager.mwAmFHLRUnLLaGqPQhrBbAAhHdzDA.HasPermissionWithTargetUserAsync(sKxkSYTWUSIsbkXngRDdQfwEBOKM, current, ZZyzxirGbCGzXEkNGxNvjLWelQwE).GetAwaiter();
								int num7;
								int num8;
								if (awaiter.IsCompleted)
								{
									num7 = 628192148;
									num8 = num7;
								}
								else
								{
									num7 = 1849891698;
									num8 = num7;
								}
								num6 = num7 ^ (int)(num3 * 1416790123);
								continue;
							}
							case 14u:
								num = (dagGWVjAonzlEQhnHtJbFGTLQUwi = -1);
								num6 = (int)(num3 * 1027118976) ^ -154306666;
								continue;
							case 9u:
								goto IL_027c;
							case 13u:
								current = BVCdlUXHezocQErtLHwjfwmllzYH.Current;
								num6 = 2115516132;
								continue;
							case 2u:
								num6 = (int)((num3 * 1683262212) ^ 0x4D0005DB);
								continue;
							case 11u:
								result2 = awaiter.GetResult();
								num6 = 1224774138;
								continue;
							case 4u:
								num = (dagGWVjAonzlEQhnHtJbFGTLQUwi = 0);
								num6 = ((int)num3 * -32133499) ^ -1091164633;
								continue;
							case 12u:
								goto end_IL_0103;
							}
							break;
							IL_01f9:
							int num11;
							if (!BVCdlUXHezocQErtLHwjfwmllzYH.MoveNext())
							{
								num6 = 701577060;
								num11 = num6;
							}
							else
							{
								num6 = 1127717005;
								num11 = num6;
							}
						}
						goto IL_0109;
						IL_027c:
						awaiter = ZXmTiyGvYGuRDpIPfYHtBgPoRgDC;
						ZXmTiyGvYGuRDpIPfYHtBgPoRgDC = default(TaskAwaiter<PermissionResult<TPlatformPermission>>);
						num6 = 1332457960;
						goto IL_010e;
						end_IL_0103:;
					}
					finally
					{
						if (num < 0)
						{
							while (true)
							{
								IL_02f9:
								int num12 = 147276389;
								while (true)
								{
									uint num3;
									switch ((num3 = (uint)(num12 ^ 0x41021362)) % 4)
									{
									case 2u:
										break;
									default:
										goto end_IL_02fe;
									case 3u:
									{
										int num13;
										int num14;
										if (BVCdlUXHezocQErtLHwjfwmllzYH == null)
										{
											num13 = 423688134;
											num14 = num13;
										}
										else
										{
											num13 = 1145501767;
											num14 = num13;
										}
										num12 = num13 ^ (int)(num3 * 2108755563);
										continue;
									}
									case 0u:
										BVCdlUXHezocQErtLHwjfwmllzYH.Dispose();
										num12 = ((int)num3 * -1126743634) ^ 0x3D85AFDB;
										continue;
									case 1u:
										goto end_IL_02fe;
									}
									goto IL_02f9;
									continue;
									end_IL_02fe:
									break;
								}
								break;
							}
						}
					}
					BVCdlUXHezocQErtLHwjfwmllzYH = null;
					while (true)
					{
						IL_0365:
						int num15 = 801076241;
						while (true)
						{
							uint num3;
							switch ((num3 = (uint)(num15 ^ 0x41021362)) % 3)
							{
							case 2u:
								break;
							default:
								goto end_IL_036a;
							case 1u:
								goto IL_0388;
							case 0u:
								goto end_IL_036a;
							}
							goto IL_0365;
							IL_0388:
							result = new PermissionResult<TPlatformPermission>(default(TPlatformPermission), PermissionState.Granted, sKxkSYTWUSIsbkXngRDdQfwEBOKM, ZZyzxirGbCGzXEkNGxNvjLWelQwE);
							num15 = (int)(num3 * 848916331) ^ -261862170;
							continue;
							end_IL_036a:
							break;
						}
						break;
					}
					end_IL_000e:;
				}
				catch (Exception exception)
				{
					dagGWVjAonzlEQhnHtJbFGTLQUwi = -2;
					VVEGRPiIETvwaByBfDgZFHzJPzZZ.SetException(exception);
					return;
				}
				dagGWVjAonzlEQhnHtJbFGTLQUwi = -2;
				while (true)
				{
					int num16 = 60589797;
					while (true)
					{
						uint num3;
						switch ((num3 = (uint)(num16 ^ 0x41021362)) % 3)
						{
						case 2u:
							break;
						default:
							return;
						case 1u:
							goto IL_03ff;
						case 0u:
							return;
						}
						break;
						IL_03ff:
						VVEGRPiIETvwaByBfDgZFHzJPzZZ.SetResult(result);
						num16 = ((int)num3 * -108591193) ^ -1410938374;
					}
				}
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				VVEGRPiIETvwaByBfDgZFHzJPzZZ.SetStateMachine(stateMachine);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout(LayoutKind.Auto)]
		private struct lbgwMPhIrLeYZonGfASReLVwiHfZA : IAsyncStateMachine
		{
			public int dagGWVjAonzlEQhnHtJbFGTLQUwi;

			public AsyncTaskMethodBuilder<IPermissionResult> VVEGRPiIETvwaByBfDgZFHzJPzZZ;

			public IPermissionResult LsXIvTTlDPiPgBuGOzrHSiXOHHwDb;

			public PermissionRuleManager<TGameFeature, TPlatformPermission> vvKNDIxiYKTrRTKAccPPCIdmGSFtA;

			public ILocalAccount sKxkSYTWUSIsbkXngRDdQfwEBOKM;

			private TaskAwaiter<PermissionResult<TPlatformPermission>> ZXmTiyGvYGuRDpIPfYHtBgPoRgDC;

			private void MoveNext()
			{
				int num = dagGWVjAonzlEQhnHtJbFGTLQUwi;
				PermissionRuleManager<TGameFeature, TPlatformPermission> permissionRuleManager = vvKNDIxiYKTrRTKAccPPCIdmGSFtA;
				IPermissionResult result = default(IPermissionResult);
				try
				{
					if (num != 0)
					{
						goto IL_0014;
					}
					goto IL_0167;
					IL_0014:
					int num2 = -1888680543;
					goto IL_0019;
					IL_0019:
					TaskAwaiter<PermissionResult<TPlatformPermission>> awaiter = default(TaskAwaiter<PermissionResult<TPlatformPermission>>);
					TPlatformPermission platformPermission = default(TPlatformPermission);
					while (true)
					{
						uint num3;
						switch ((num3 = (uint)(num2 ^ -629790571)) % 11)
						{
						case 0u:
							break;
						default:
							goto end_IL_000e;
						case 2u:
							result = awaiter.GetResult();
							num2 = -1230533826;
							continue;
						case 5u:
							return;
						case 8u:
							platformPermission = ((PermissionResult<TPlatformPermission>)LsXIvTTlDPiPgBuGOzrHSiXOHHwDb).PlatformPermission;
							num2 = (int)((num3 * 1154950464) ^ 0x6E36B13F);
							continue;
						case 6u:
							VVEGRPiIETvwaByBfDgZFHzJPzZZ.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							num2 = ((int)num3 * -432229624) ^ -733728258;
							continue;
						case 10u:
							num = (dagGWVjAonzlEQhnHtJbFGTLQUwi = -1);
							num2 = (int)((num3 * 1471483421) ^ 0x45FE7D23);
							continue;
						case 4u:
							ZXmTiyGvYGuRDpIPfYHtBgPoRgDC = default(TaskAwaiter<PermissionResult<TPlatformPermission>>);
							num2 = (int)(num3 * 508599189) ^ -67825050;
							continue;
						case 7u:
							num = (dagGWVjAonzlEQhnHtJbFGTLQUwi = 0);
							ZXmTiyGvYGuRDpIPfYHtBgPoRgDC = awaiter;
							num2 = (int)(num3 * 235681882) ^ -1550886120;
							continue;
						case 9u:
						{
							awaiter = permissionRuleManager.mwAmFHLRUnLLaGqPQhrBbAAhHdzDA.ResolvePermission(sKxkSYTWUSIsbkXngRDdQfwEBOKM, platformPermission).GetAwaiter();
							int num4;
							int num5;
							if (awaiter.IsCompleted)
							{
								num4 = 91333774;
								num5 = num4;
							}
							else
							{
								num4 = 782140583;
								num5 = num4;
							}
							num2 = num4 ^ (int)(num3 * 566201621);
							continue;
						}
						case 3u:
							goto IL_0167;
						case 1u:
							goto end_IL_000e;
						}
						break;
					}
					goto IL_0014;
					IL_0167:
					awaiter = ZXmTiyGvYGuRDpIPfYHtBgPoRgDC;
					num2 = -699829395;
					goto IL_0019;
					end_IL_000e:;
				}
				catch (Exception exception)
				{
					dagGWVjAonzlEQhnHtJbFGTLQUwi = -2;
					VVEGRPiIETvwaByBfDgZFHzJPzZZ.SetException(exception);
					return;
				}
				dagGWVjAonzlEQhnHtJbFGTLQUwi = -2;
				VVEGRPiIETvwaByBfDgZFHzJPzZZ.SetResult(result);
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				VVEGRPiIETvwaByBfDgZFHzJPzZZ.SetStateMachine(stateMachine);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		private readonly IPermissionManager<TPlatformPermission> mwAmFHLRUnLLaGqPQhrBbAAhHdzDA;

		private readonly PermissionRules<TGameFeature, TPlatformPermission> LRzYhZbvCyyQNXaWopDzXgzgDxDN;

		public event Action<IPlatformService, Exception> InternalErrorOccurred
		{
			add
			{
			}
			remove
			{
			}
		}

		internal PermissionRuleManager(IPermissionManager<TPlatformPermission> P_0, PermissionRules<TGameFeature, TPlatformPermission> P_1)
		{
			while (true)
			{
				int num = -355624844;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1933261954)) % 3)
					{
					case 0u:
						break;
					case 2u:
						goto IL_0028;
					default:
						LRzYhZbvCyyQNXaWopDzXgzgDxDN = P_1;
						return;
					}
					break;
					IL_0028:
					mwAmFHLRUnLLaGqPQhrBbAAhHdzDA = P_0;
					num = ((int)num2 * -1061122909) ^ 0x435F3530;
				}
			}
		}

		[AsyncStateMachine(typeof(_003CHasPermission_003Ed__6<, >))]
		public Task<IPermissionResult> HasPermission(ILocalAccount localAccount, TGameFeature feature)
		{
			AikoJGDayZfRNoNRQpRVTcRTCFii stateMachine = default(AikoJGDayZfRNoNRQpRVTcRTCFii);
			stateMachine.VVEGRPiIETvwaByBfDgZFHzJPzZZ = AsyncTaskMethodBuilder<IPermissionResult>.Create();
			stateMachine.vvKNDIxiYKTrRTKAccPPCIdmGSFtA = this;
			stateMachine.sKxkSYTWUSIsbkXngRDdQfwEBOKM = localAccount;
			stateMachine.cyvZaxOTuDemSudcbwVoIQTtEEuS = feature;
			while (true)
			{
				int num = 480651878;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x4D17EE67)) % 3)
					{
					case 0u:
						break;
					case 2u:
						goto IL_0046;
					default:
						return stateMachine.VVEGRPiIETvwaByBfDgZFHzJPzZZ.Task;
					}
					break;
					IL_0046:
					stateMachine.dagGWVjAonzlEQhnHtJbFGTLQUwi = -1;
					stateMachine.VVEGRPiIETvwaByBfDgZFHzJPzZZ.Start(ref stateMachine);
					num = (int)((num2 * 40117034) ^ 0x22298409);
				}
			}
		}

		[AsyncStateMachine(typeof(_003CHasPermissionWithTargetUser_003Ed__7<, >))]
		public Task<IPermissionResult> HasPermissionWithTargetUser(ILocalAccount localAccount, TGameFeature permission, IRemoteAccount targetUser)
		{
			IFrOAAItOnacBsBEtAySDuarPORbA stateMachine = default(IFrOAAItOnacBsBEtAySDuarPORbA);
			stateMachine.VVEGRPiIETvwaByBfDgZFHzJPzZZ = AsyncTaskMethodBuilder<IPermissionResult>.Create();
			while (true)
			{
				int num = 533429311;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x420928DC)) % 6)
					{
					case 3u:
						break;
					case 5u:
						stateMachine.ZZyzxirGbCGzXEkNGxNvjLWelQwE = targetUser;
						stateMachine.dagGWVjAonzlEQhnHtJbFGTLQUwi = -1;
						stateMachine.VVEGRPiIETvwaByBfDgZFHzJPzZZ.Start(ref stateMachine);
						num = ((int)num2 * -1214379122) ^ -1385162578;
						continue;
					case 0u:
						stateMachine.sKxkSYTWUSIsbkXngRDdQfwEBOKM = localAccount;
						num = ((int)num2 * -1300090758) ^ -520774870;
						continue;
					case 1u:
						stateMachine.vvKNDIxiYKTrRTKAccPPCIdmGSFtA = this;
						num = (int)((num2 * 786614388) ^ 0x5AEA4128);
						continue;
					case 2u:
						stateMachine.zIctoCBCLEIlqCVGvkypEGazzmQBA = permission;
						num = ((int)num2 * -2088056455) ^ -130446811;
						continue;
					default:
						return stateMachine.VVEGRPiIETvwaByBfDgZFHzJPzZZ.Task;
					}
					break;
				}
			}
		}

		public Task NotifyWithUI(ILocalAccount localAccount, IPermissionResult result)
		{
			TPlatformPermission platformPermission = ((PermissionResult<TPlatformPermission>)result).PlatformPermission;
			return mwAmFHLRUnLLaGqPQhrBbAAhHdzDA.NotifyWithUI(localAccount, platformPermission);
		}

		[AsyncStateMachine(typeof(_003CResolvePermission_003Ed__9<, >))]
		public Task<IPermissionResult> ResolvePermission(ILocalAccount localAccount, IPermissionResult result)
		{
			lbgwMPhIrLeYZonGfASReLVwiHfZA stateMachine = default(lbgwMPhIrLeYZonGfASReLVwiHfZA);
			stateMachine.VVEGRPiIETvwaByBfDgZFHzJPzZZ = AsyncTaskMethodBuilder<IPermissionResult>.Create();
			stateMachine.vvKNDIxiYKTrRTKAccPPCIdmGSFtA = this;
			while (true)
			{
				int num = 742455431;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x41927689)) % 4)
					{
					case 0u:
						break;
					case 2u:
						stateMachine.sKxkSYTWUSIsbkXngRDdQfwEBOKM = localAccount;
						num = (int)(num2 * 1229462369) ^ -793556388;
						continue;
					case 3u:
						stateMachine.LsXIvTTlDPiPgBuGOzrHSiXOHHwDb = result;
						num = (int)((num2 * 1997649469) ^ 0x12EA91AF);
						continue;
					default:
						stateMachine.dagGWVjAonzlEQhnHtJbFGTLQUwi = -1;
						stateMachine.VVEGRPiIETvwaByBfDgZFHzJPzZZ.Start(ref stateMachine);
						return stateMachine.VVEGRPiIETvwaByBfDgZFHzJPzZZ.Task;
					}
					break;
				}
			}
		}
	}
}
