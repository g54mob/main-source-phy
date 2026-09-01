using System;
using JetBrains.Annotations;
using UnityEngine;
using dycJggssKJBbYomRwEcQasvEaFIib;

namespace BitCode.Performance
{
	internal class FrameTimingPerformanceCounterBase : lXPACrJRvYzCXOSgnaIzgQcePWHg
	{
		private readonly DFaEhMkrGEEOwIAPDVnDUgbLXWaeB timingWrapper;

		private readonly Func<FrameTiming, double> sample;

		protected FrameTimingPerformanceCounterBase(int historySize, [NotNull] DFaEhMkrGEEOwIAPDVnDUgbLXWaeB timingWrapper, Func<FrameTiming, double> sample)
			: base(historySize)
		{
			this.timingWrapper = timingWrapper ?? throw new ArgumentNullException("timingWrapper");
			this.sample = sample;
		}

		protected override bool GetSample(out double retrievedSample)
		{
			if (!timingWrapper.fZddJOAHbxABIWmFfYFDKAMKNTlzA.HasValue)
			{
				goto IL_0015;
			}
			goto IL_0057;
			IL_0015:
			int num = 802829622;
			goto IL_001a;
			IL_001a:
			uint num2;
			switch ((num2 = (uint)(num ^ 0x7F2F1F83)) % 4)
			{
			case 0u:
				break;
			case 1u:
				retrievedSample = 0.0;
				return false;
			case 3u:
				goto IL_0057;
			default:
				return true;
			}
			goto IL_0015;
			IL_0057:
			retrievedSample = sample(timingWrapper.fZddJOAHbxABIWmFfYFDKAMKNTlzA.Value);
			num = 1739383393;
			goto IL_001a;
		}
	}
}
