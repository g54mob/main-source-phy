using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BitCode.ErrorHandling
{
	public class ExceptionHandlingService : IPlatformService
	{
		[StructLayout(LayoutKind.Auto)]
		private struct CoVVQDGOWcaVOLhcfkAjhbdrgnuY : IAsyncStateMachine
		{
			public int dagGWVjAonzlEQhnHtJbFGTLQUwi;

			public AsyncTaskMethodBuilder<ExceptionResolution> VVEGRPiIETvwaByBfDgZFHzJPzZZ;

			public ExceptionHandlingService vvKNDIxiYKTrRTKAccPPCIdmGSFtA;

			public Exception oWrDNzJREYSpjItEKqgnTyPMJeoYb;

			private List<IExceptionHandler>.Enumerator BVCdlUXHezocQErtLHwjfwmllzYH;

			private TaskAwaiter<ExceptionResolution> ZXmTiyGvYGuRDpIPfYHtBgPoRgDC;

			private void MoveNext()
			{
				int num = dagGWVjAonzlEQhnHtJbFGTLQUwi;
				ExceptionHandlingService exceptionHandlingService = vvKNDIxiYKTrRTKAccPPCIdmGSFtA;
				ExceptionResolution result = default(ExceptionResolution);
				try
				{
					if (num != 0)
					{
						while (true)
						{
							int num2 = 700116702;
							while (true)
							{
								uint num3;
								switch ((num3 = (uint)(num2 ^ 0x10C4296F)) % 3)
								{
								case 2u:
									break;
								case 1u:
									BVCdlUXHezocQErtLHwjfwmllzYH = exceptionHandlingService.cWDYEyMkeeUoEQrBujnlbpbuCGqp.GetEnumerator();
									num2 = ((int)num3 * -541042637) ^ -1097942317;
									continue;
								default:
									goto end_IL_0011;
								}
								break;
							}
							continue;
							end_IL_0011:
							break;
						}
					}
					try
					{
						if (num != 0)
						{
							goto IL_005c;
						}
						goto IL_0114;
						IL_005c:
						int num4 = 367108745;
						goto IL_0061;
						IL_0061:
						IExceptionHandler current = default(IExceptionHandler);
						TaskAwaiter<ExceptionResolution> awaiter = default(TaskAwaiter<ExceptionResolution>);
						while (true)
						{
							uint num3;
							switch ((num3 = (uint)(num4 ^ 0x10C4296F)) % 12)
							{
							case 4u:
								break;
							default:
								goto end_IL_0056;
							case 2u:
								goto IL_00a7;
							case 1u:
							{
								int num7;
								int num8;
								if (!current.CanHandleException(oWrDNzJREYSpjItEKqgnTyPMJeoYb))
								{
									num7 = 113610241;
									num8 = num7;
								}
								else
								{
									num7 = 1607372539;
									num8 = num7;
								}
								num4 = num7 ^ ((int)num3 * -599640184);
								continue;
							}
							case 7u:
								num = (dagGWVjAonzlEQhnHtJbFGTLQUwi = 0);
								ZXmTiyGvYGuRDpIPfYHtBgPoRgDC = awaiter;
								num4 = ((int)num3 * -801575095) ^ 0x3B852AAE;
								continue;
							case 11u:
								goto IL_0114;
							case 3u:
								current = BVCdlUXHezocQErtLHwjfwmllzYH.Current;
								num4 = 469813038;
								continue;
							case 9u:
								ZXmTiyGvYGuRDpIPfYHtBgPoRgDC = default(TaskAwaiter<ExceptionResolution>);
								num = (dagGWVjAonzlEQhnHtJbFGTLQUwi = -1);
								num4 = (int)((num3 * 176678925) ^ 0x354CF023);
								continue;
							case 8u:
							{
								awaiter = current.TryHandleExceptionAsync(oWrDNzJREYSpjItEKqgnTyPMJeoYb).GetAwaiter();
								int num5;
								int num6;
								if (!awaiter.IsCompleted)
								{
									num5 = -247833720;
									num6 = num5;
								}
								else
								{
									num5 = -501950902;
									num6 = num5;
								}
								num4 = num5 ^ ((int)num3 * -1817706850);
								continue;
							}
							case 6u:
								num4 = (int)(num3 * 498233788) ^ -932559519;
								continue;
							case 5u:
								result = awaiter.GetResult();
								goto end_IL_000e;
							case 10u:
								VVEGRPiIETvwaByBfDgZFHzJPzZZ.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							case 0u:
								goto end_IL_0056;
							}
							break;
							IL_00a7:
							int num9;
							if (BVCdlUXHezocQErtLHwjfwmllzYH.MoveNext())
							{
								num4 = 1208856552;
								num9 = num4;
							}
							else
							{
								num4 = 1937560683;
								num9 = num4;
							}
						}
						goto IL_005c;
						IL_0114:
						awaiter = ZXmTiyGvYGuRDpIPfYHtBgPoRgDC;
						num4 = 917401354;
						goto IL_0061;
						end_IL_0056:;
					}
					finally
					{
						if (num < 0)
						{
							while (true)
							{
								IL_01f3:
								int num10 = 64433439;
								while (true)
								{
									uint num3;
									switch ((num3 = (uint)(num10 ^ 0x10C4296F)) % 3)
									{
									case 2u:
										break;
									default:
										goto end_IL_01f8;
									case 1u:
										goto IL_0216;
									case 0u:
										goto end_IL_01f8;
									}
									goto IL_01f3;
									IL_0216:
									((IDisposable)BVCdlUXHezocQErtLHwjfwmllzYH/*cast due to .constrained prefix*/).Dispose();
									num10 = ((int)num3 * -1855220645) ^ 0x402DE831;
									continue;
									end_IL_01f8:
									break;
								}
								break;
							}
						}
					}
					BVCdlUXHezocQErtLHwjfwmllzYH = default(List<IExceptionHandler>.Enumerator);
					while (true)
					{
						IL_0244:
						int num11 = 480680331;
						while (true)
						{
							uint num3;
							switch ((num3 = (uint)(num11 ^ 0x10C4296F)) % 3)
							{
							case 2u:
								break;
							default:
								goto end_IL_0249;
							case 1u:
								goto IL_0267;
							case 0u:
								goto end_IL_0249;
							}
							goto IL_0244;
							IL_0267:
							result = ExceptionResolution.HandleException;
							num11 = ((int)num3 * -773426532) ^ -277802567;
							continue;
							end_IL_0249:
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
						int num12 = 1982366864;
						while (true)
						{
							uint num3;
							switch ((num3 = (uint)(num12 ^ 0x10C4296F)) % 3)
							{
							case 0u:
								break;
							default:
								return;
							case 1u:
								goto IL_02a0;
							case 2u:
								return;
							}
							break;
							IL_02a0:
							dagGWVjAonzlEQhnHtJbFGTLQUwi = -2;
							VVEGRPiIETvwaByBfDgZFHzJPzZZ.SetException(exception);
							num12 = ((int)num3 * -871154840) ^ 0x4E27591E;
						}
					}
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

		private readonly List<IExceptionHandler> cWDYEyMkeeUoEQrBujnlbpbuCGqp = new List<IExceptionHandler>();

		event Action<IPlatformService, Exception> IPlatformService.InternalErrorOccurred
		{
			add
			{
			}
			remove
			{
			}
		}

		public void RegisterExceptionHandler(IExceptionHandler handler)
		{
			cWDYEyMkeeUoEQrBujnlbpbuCGqp.Add(handler);
		}

		[AsyncStateMachine(typeof(_003CTryHandleExceptionAsync_003Ed__5))]
		public Task<ExceptionResolution> TryHandleExceptionAsync(Exception exception)
		{
			CoVVQDGOWcaVOLhcfkAjhbdrgnuY stateMachine = default(CoVVQDGOWcaVOLhcfkAjhbdrgnuY);
			stateMachine.VVEGRPiIETvwaByBfDgZFHzJPzZZ = AsyncTaskMethodBuilder<ExceptionResolution>.Create();
			while (true)
			{
				int num = 1743981986;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x4DB8DE01)) % 4)
					{
					case 2u:
						break;
					case 3u:
						stateMachine.vvKNDIxiYKTrRTKAccPPCIdmGSFtA = this;
						stateMachine.oWrDNzJREYSpjItEKqgnTyPMJeoYb = exception;
						num = (int)(num2 * 283305857) ^ -1515189406;
						continue;
					case 0u:
						stateMachine.dagGWVjAonzlEQhnHtJbFGTLQUwi = -1;
						num = (int)(num2 * 1066341787) ^ -1249310360;
						continue;
					default:
						stateMachine.VVEGRPiIETvwaByBfDgZFHzJPzZZ.Start(ref stateMachine);
						return stateMachine.VVEGRPiIETvwaByBfDgZFHzJPzZZ.Task;
					}
					break;
				}
			}
		}
	}
}
