using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using dycJggssKJBbYomRwEcQasvEaFIib;

namespace BitCode.Performance
{
	public class PerformanceCounters : IDisposable, IUpdateableService
	{
		[CompilerGenerated]
		private readonly IPerformanceCounter<double, double> FaCGxWRXvKgCRoBbuAGFRmXiddkC;

		[CompilerGenerated]
		private readonly IPerformanceCounter<double, double> VfvQgTxZfkQnfzHoSQoteGgHnKtl;

		[CompilerGenerated]
		private readonly IPerformanceCounter<double, double> TzwterTfLgbDRgUPmIljTrhMqTDEb;

		[CompilerGenerated]
		private readonly IPerformanceCounter<long, double> DAzaIhGpCQEhzJGLmWFhXIKZiSBz;

		[CompilerGenerated]
		private readonly IPerformanceCounter<long, double> jLGJOnrwSwYKTPGzfCGZBthkvCGU;

		[CompilerGenerated]
		private readonly IPerformanceCounter<long, double> GRweYRmbOliZHcHYaLBEUflbMZMaA;

		private readonly IPerformanceCounter[] otgkbjLbtGaWvNoRPyvKCcNYScVl;

		private readonly IServiceUpdater UeSkHcBnUDZDTHkgfGCVbjKIjUXtA;

		private bool tlRiOzSchvnYldbxynOFisSYraBV;

		[CanBeNull]
		public IPerformanceCounter<double, double> FrameTimeCounter
		{
			[CompilerGenerated]
			get
			{
				return FaCGxWRXvKgCRoBbuAGFRmXiddkC;
			}
		}

		[CanBeNull]
		public IPerformanceCounter<double, double> CpuCounter
		{
			[CompilerGenerated]
			get
			{
				return VfvQgTxZfkQnfzHoSQoteGgHnKtl;
			}
		}

		[CanBeNull]
		public IPerformanceCounter<double, double> GpuCounter
		{
			[CompilerGenerated]
			get
			{
				return TzwterTfLgbDRgUPmIljTrhMqTDEb;
			}
		}

		[CanBeNull]
		public IPerformanceCounter<long, double> AllocatedMemoryCounter
		{
			[CompilerGenerated]
			get
			{
				return DAzaIhGpCQEhzJGLmWFhXIKZiSBz;
			}
		}

		[CanBeNull]
		public IPerformanceCounter<long, double> ReservedMemoryCounter
		{
			[CompilerGenerated]
			get
			{
				return jLGJOnrwSwYKTPGzfCGZBthkvCGU;
			}
		}

		[CanBeNull]
		public IPerformanceCounter<long, double> GcMemoryCounter
		{
			[CompilerGenerated]
			get
			{
				return GRweYRmbOliZHcHYaLBEUflbMZMaA;
			}
		}

		private PerformanceCounters([NotNull] IServiceUpdater P_0, [CanBeNull] IPerformanceCounter<double, double> P_1, [CanBeNull] IPerformanceCounter<double, double> P_2, [CanBeNull] IPerformanceCounter<double, double> P_3, [CanBeNull] IPerformanceCounter<long, double> P_4, [CanBeNull] IPerformanceCounter<long, double> P_5, [CanBeNull] IPerformanceCounter<long, double> P_6)
		{
			UeSkHcBnUDZDTHkgfGCVbjKIjUXtA = P_0 ?? throw new ArgumentNullException("serviceUpdater");
			P_0.RegisterService(this);
			FaCGxWRXvKgCRoBbuAGFRmXiddkC = P_1;
			VfvQgTxZfkQnfzHoSQoteGgHnKtl = P_2;
			TzwterTfLgbDRgUPmIljTrhMqTDEb = P_3;
			DAzaIhGpCQEhzJGLmWFhXIKZiSBz = P_4;
			jLGJOnrwSwYKTPGzfCGZBthkvCGU = P_5;
			GRweYRmbOliZHcHYaLBEUflbMZMaA = P_6;
			otgkbjLbtGaWvNoRPyvKCcNYScVl = new IPerformanceCounter[6] { FrameTimeCounter, CpuCounter, GpuCounter, AllocatedMemoryCounter, ReservedMemoryCounter, GcMemoryCounter };
		}

		public void Dispose()
		{
			if (tlRiOzSchvnYldbxynOFisSYraBV)
			{
				goto IL_0008;
			}
			goto IL_003e;
			IL_0008:
			int num = -707766302;
			goto IL_000d;
			IL_000d:
			uint num2;
			switch ((num2 = (uint)(num ^ -1029884917)) % 4)
			{
			case 3u:
				break;
			case 1u:
				return;
			case 2u:
				goto IL_003e;
			default:
				UeSkHcBnUDZDTHkgfGCVbjKIjUXtA.DeregisterService(this);
				return;
			}
			goto IL_0008;
			IL_003e:
			tlRiOzSchvnYldbxynOFisSYraBV = true;
			num = -372661713;
			goto IL_000d;
		}

		void IUpdateableService.Update()
		{
			if (tlRiOzSchvnYldbxynOFisSYraBV)
			{
				goto IL_000b;
			}
			goto IL_009d;
			IL_000b:
			int num = -305015605;
			goto IL_0010;
			IL_0010:
			int num3 = default(int);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -584140347)) % 8)
				{
				case 2u:
					break;
				default:
					return;
				case 7u:
					goto IL_0044;
				case 1u:
					num = ((int)num2 * -1953513547) ^ -129968337;
					continue;
				case 5u:
					num3++;
					num = -1714586110;
					continue;
				case 6u:
					throw new ObjectDisposedException(GetType().FullName);
				case 0u:
					goto IL_009d;
				case 4u:
				{
					IPerformanceCounter obj = otgkbjLbtGaWvNoRPyvKCcNYScVl[num3];
					if (obj == null)
					{
						goto case 5u;
					}
					obj.Tick();
					num = -50666736;
					continue;
				}
				case 3u:
					return;
				}
				break;
				IL_0044:
				int num4;
				if (num3 < otgkbjLbtGaWvNoRPyvKCcNYScVl.Length)
				{
					num = -1058455447;
					num4 = num;
				}
				else
				{
					num = -587864026;
					num4 = num;
				}
			}
			goto IL_000b;
			IL_009d:
			num3 = 0;
			num = -135542948;
			goto IL_0010;
		}

		public static PerformanceCounters CreateForFrameTimingSystem(IServiceUpdater serviceUpdater, int historySize)
		{
			DFaEhMkrGEEOwIAPDVnDUgbLXWaeB timingWrapper = new DFaEhMkrGEEOwIAPDVnDUgbLXWaeB(serviceUpdater);
			return new PerformanceCounters(serviceUpdater, new FrameTimeCounter(historySize), new CpuFrameTimingPerformanceCounter(historySize, timingWrapper), new GpuFrameTimingPerformanceCounter(historySize, timingWrapper), new TotalAllocatedMemoryPerformanceCounter(historySize), new TotalReservedMemoryPerformanceCounter(historySize), new TotalGcMemoryPerformanceCounter(historySize));
		}
	}
}
