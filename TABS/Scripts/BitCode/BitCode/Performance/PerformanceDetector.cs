using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace BitCode.Performance
{
	public class PerformanceDetector : IDisposable, IUpdateableService, IPerformanceDetector
	{
		[CompilerGenerated]
		private MeasuredPerformanceState mBcihcpJKRCDOWPPEGyNlEKQWROh;

		[CompilerGenerated]
		private float VtGdPhdhYBsThNGDGcjwQeGnKpZv;

		[CompilerGenerated]
		private float FgxhMItZXqDXqLVDfdJuglLlvDNv;

		[CompilerGenerated]
		private float mXDDIfJxJRyGDnrFcztHuSkTnAizA;

		[CompilerGenerated]
		private float OMfFnSaDxwOvdFmZHXqlzWrUArtJA;

		[NotNull]
		private readonly IPerformanceCounter<double, double> YQpdMiKpnjdloTjzoKeswtgQVZoSA;

		[NotNull]
		private readonly IPerformanceCounter<double, double> OdLxJxShQhdKSdScniQLumknSHJz;

		[NotNull]
		private readonly IPerformanceCounter<double, double> LsxmmGnQSJeygaJTzkfZNSJuiMhn;

		private bool UcgvKdaLGYARNuIkgRpqVRONKkQL;

		private bool vCIVmHHyOKqmtitvqkRIOSLqntPW;

		private readonly IServiceUpdater UeSkHcBnUDZDTHkgfGCVbjKIjUXtA;

		private bool tlRiOzSchvnYldbxynOFisSYraBV;

		public MeasuredPerformanceState State
		{
			[CompilerGenerated]
			get
			{
				return mBcihcpJKRCDOWPPEGyNlEKQWROh;
			}
			[CompilerGenerated]
			private set
			{
				mBcihcpJKRCDOWPPEGyNlEKQWROh = measuredPerformanceState;
			}
		}

		public float FrameTimeTarget
		{
			[CompilerGenerated]
			get
			{
				return VtGdPhdhYBsThNGDGcjwQeGnKpZv;
			}
			[CompilerGenerated]
			set
			{
				VtGdPhdhYBsThNGDGcjwQeGnKpZv = value;
			}
		}

		public float CpuTimeTarget
		{
			[CompilerGenerated]
			get
			{
				return FgxhMItZXqDXqLVDfdJuglLlvDNv;
			}
			[CompilerGenerated]
			set
			{
				FgxhMItZXqDXqLVDfdJuglLlvDNv = value;
			}
		}

		public float GpuTimeTarget
		{
			[CompilerGenerated]
			get
			{
				return mXDDIfJxJRyGDnrFcztHuSkTnAizA;
			}
			[CompilerGenerated]
			set
			{
				mXDDIfJxJRyGDnrFcztHuSkTnAizA = value;
			}
		}

		public float SurplusThreshold
		{
			[CompilerGenerated]
			get
			{
				return OMfFnSaDxwOvdFmZHXqlzWrUArtJA;
			}
			[CompilerGenerated]
			set
			{
				OMfFnSaDxwOvdFmZHXqlzWrUArtJA = value;
			}
		}

		public PerformanceDetector([NotNull] IServiceUpdater serviceUpdater, [NotNull] IPerformanceCounter<double, double> frameTimeCounter, [NotNull] IPerformanceCounter<double, double> cpuTimeCounter, [NotNull] IPerformanceCounter<double, double> gpuTimeCounter, float frameTimeTarget = 16.4f, float gpuTimeTarget = 16f, float cpuTimeTarget = 16f, float surplusThreshold = 1f)
		{
			if (frameTimeTarget <= 0f)
			{
				throw new ArgumentOutOfRangeException("frameTimeTarget", "Parameter must be positive and nonzero.");
			}
			if (gpuTimeTarget <= 0f)
			{
				throw new ArgumentOutOfRangeException("gpuTimeTarget", "Parameter must be positive and nonzero.");
			}
			if (cpuTimeTarget <= 0f)
			{
				throw new ArgumentOutOfRangeException("cpuTimeTarget", "Parameter must be positive and nonzero.");
			}
			if (surplusThreshold < 0f)
			{
				throw new ArgumentOutOfRangeException("surplusThreshold", "Parameter must be positive.");
			}
			UeSkHcBnUDZDTHkgfGCVbjKIjUXtA = serviceUpdater ?? throw new ArgumentNullException("serviceUpdater");
			serviceUpdater.RegisterService(this);
			YQpdMiKpnjdloTjzoKeswtgQVZoSA = frameTimeCounter ?? throw new ArgumentNullException("frameTimeCounter");
			OdLxJxShQhdKSdScniQLumknSHJz = cpuTimeCounter ?? throw new ArgumentNullException("cpuTimeCounter");
			LsxmmGnQSJeygaJTzkfZNSJuiMhn = gpuTimeCounter ?? throw new ArgumentNullException("gpuTimeCounter");
			GpuTimeTarget = gpuTimeTarget;
			CpuTimeTarget = cpuTimeTarget;
			FrameTimeTarget = frameTimeTarget;
			SurplusThreshold = surplusThreshold;
			State = MeasuredPerformanceState.Inconclusive;
			UcgvKdaLGYARNuIkgRpqVRONKkQL = false;
			vCIVmHHyOKqmtitvqkRIOSLqntPW = false;
		}

		public void Update()
		{
			UrWwwkEVqlsCwuqAxyNaOnyUzodO();
			if (YQpdMiKpnjdloTjzoKeswtgQVZoSA.Count >= 2)
			{
				goto IL_0017;
			}
			goto IL_025b;
			IL_0017:
			int num = -1040030157;
			goto IL_001c;
			IL_001c:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -623072809)) % 19)
				{
				case 0u:
					break;
				default:
					return;
				case 15u:
				{
					bool num5 = YQpdMiKpnjdloTjzoKeswtgQVZoSA.Current > (double)FrameTimeTarget;
					if (num5 && OdLxJxShQhdKSdScniQLumknSHJz.Current > (double)CpuTimeTarget)
					{
						if (UcgvKdaLGYARNuIkgRpqVRONKkQL)
						{
							State |= MeasuredPerformanceState.CpuConstrained;
						}
						UcgvKdaLGYARNuIkgRpqVRONKkQL = true;
					}
					else
					{
						UcgvKdaLGYARNuIkgRpqVRONKkQL = false;
						if (OdLxJxShQhdKSdScniQLumknSHJz.Average <= (double)(CpuTimeTarget - SurplusThreshold))
						{
							State |= MeasuredPerformanceState.CpuSurplus;
						}
					}
					if (num5)
					{
						num = -1041875416;
						continue;
					}
					goto case 9u;
				}
				case 11u:
				{
					int num14;
					int num15;
					if (LsxmmGnQSJeygaJTzkfZNSJuiMhn.Count >= 2)
					{
						num14 = 914148320;
						num15 = num14;
					}
					else
					{
						num14 = 1660706024;
						num15 = num14;
					}
					num = num14 ^ ((int)num2 * -1751811240);
					continue;
				}
				case 3u:
				{
					int num6;
					int num7;
					if (LsxmmGnQSJeygaJTzkfZNSJuiMhn.Current <= (double)GpuTimeTarget)
					{
						num6 = 1967680984;
						num7 = num6;
					}
					else
					{
						num6 = 536433145;
						num7 = num6;
					}
					num = num6 ^ ((int)num2 * -1898515679);
					continue;
				}
				case 9u:
					vCIVmHHyOKqmtitvqkRIOSLqntPW = false;
					num = -1287118586;
					continue;
				case 4u:
					return;
				case 16u:
				{
					int num12;
					int num13;
					if (YQpdMiKpnjdloTjzoKeswtgQVZoSA.Current > (double)FrameTimeTarget)
					{
						num12 = -517783289;
						num13 = num12;
					}
					else
					{
						num12 = -1736320741;
						num13 = num12;
					}
					num = num12 ^ (int)(num2 * 82565674);
					continue;
				}
				case 6u:
				{
					int num8;
					int num9;
					if (OdLxJxShQhdKSdScniQLumknSHJz.Count >= 2)
					{
						num8 = 1708393908;
						num9 = num8;
					}
					else
					{
						num8 = 1987201304;
						num9 = num8;
					}
					num = num8 ^ ((int)num2 * -501113506);
					continue;
				}
				case 1u:
				{
					int num10;
					int num11;
					if (LsxmmGnQSJeygaJTzkfZNSJuiMhn.Average > (double)(GpuTimeTarget - SurplusThreshold))
					{
						num10 = 159757747;
						num11 = num10;
					}
					else
					{
						num10 = 1361099215;
						num11 = num10;
					}
					num = num10 ^ (int)(num2 * 371093523);
					continue;
				}
				case 13u:
					goto IL_0213;
				case 17u:
					State |= MeasuredPerformanceState.GpuConstrained;
					num = ((int)num2 * -1769863680) ^ -851807640;
					continue;
				case 18u:
					goto IL_025b;
				case 5u:
					num = (int)(num2 * 265620478) ^ -974402752;
					continue;
				case 10u:
					State |= MeasuredPerformanceState.GpuSurplus;
					num = ((int)num2 * -1764185130) ^ -891959646;
					continue;
				case 12u:
					vCIVmHHyOKqmtitvqkRIOSLqntPW = true;
					num = -1251863905;
					continue;
				case 14u:
				{
					int num3;
					int num4;
					if (vCIVmHHyOKqmtitvqkRIOSLqntPW)
					{
						num3 = 659434663;
						num4 = num3;
					}
					else
					{
						num3 = 1661661238;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 1348490302);
					continue;
				}
				case 8u:
					State = MeasuredPerformanceState.Inconclusive;
					num = -205338589;
					continue;
				case 7u:
					State |= MeasuredPerformanceState.Adequate;
					num = (int)((num2 * 1954338007) ^ 0x4DC01CA7);
					continue;
				case 2u:
					return;
				}
				break;
				IL_0213:
				int num16;
				if (YQpdMiKpnjdloTjzoKeswtgQVZoSA.Average > (double)FrameTimeTarget)
				{
					num = -1349314655;
					num16 = num;
				}
				else
				{
					num = -567424752;
					num16 = num;
				}
			}
			goto IL_0017;
			IL_025b:
			State = MeasuredPerformanceState.Inconclusive;
			num = -2040613203;
			goto IL_001c;
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
				switch ((num = 2004004325u) % 3)
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

		public void Dispose()
		{
			if (tlRiOzSchvnYldbxynOFisSYraBV)
			{
				goto IL_0008;
			}
			goto IL_003e;
			IL_0008:
			int num = 1532172774;
			goto IL_000d;
			IL_000d:
			uint num2;
			switch ((num2 = (uint)(num ^ 0x4A86B824)) % 4)
			{
			case 0u:
				break;
			case 2u:
				return;
			case 3u:
				goto IL_003e;
			default:
				UeSkHcBnUDZDTHkgfGCVbjKIjUXtA.DeregisterService(this);
				return;
			}
			goto IL_0008;
			IL_003e:
			tlRiOzSchvnYldbxynOFisSYraBV = true;
			num = 268058197;
			goto IL_000d;
		}
	}
}
