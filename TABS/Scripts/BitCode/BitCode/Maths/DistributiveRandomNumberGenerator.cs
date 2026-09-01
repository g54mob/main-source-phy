using System;

namespace BitCode.Maths
{
	public class DistributiveRandomNumberGenerator : IRandomNumberGenerator
	{
		public delegate double Distributor(double value);

		[Serializable]
		private sealed class XwWPjKkdTBaFtALtATnTIqerqCTh
		{
			public static readonly XwWPjKkdTBaFtALtATnTIqerqCTh _003C_003E9 = new XwWPjKkdTBaFtALtATnTIqerqCTh();

			internal double iCASAgFwZorYtiPStafATersKjHy(double P_0)
			{
				return P_0 * P_0;
			}

			internal double HNYDgnBoPoixgFsXjUCfCtasizZAA(double P_0)
			{
				return 1.0 - P_0 * P_0;
			}

			internal double FlVkiUjLLgkQGEPyJrraKkFcdXaBA(double P_0)
			{
				P_0 *= 2.0;
				P_0 -= 1.0;
				double num3 = default(double);
				while (true)
				{
					int num = -479220326;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -1971677664)) % 4)
						{
						case 0u:
							break;
						case 2u:
							num3 = Math.Abs(P_0);
							num = ((int)num2 * -512203176) ^ -648225291;
							continue;
						case 1u:
							P_0 *= num3;
							P_0 += 1.0;
							P_0 *= 0.5;
							num = ((int)num2 * -284472468) ^ -1250916781;
							continue;
						default:
							return P_0;
						}
						break;
					}
				}
			}
		}

		public static readonly Distributor SquareDistributor = XwWPjKkdTBaFtALtATnTIqerqCTh._003C_003E9.iCASAgFwZorYtiPStafATersKjHy;

		public static readonly Distributor InverseSquareDistributor = XwWPjKkdTBaFtALtATnTIqerqCTh._003C_003E9.HNYDgnBoPoixgFsXjUCfCtasizZAA;

		public static readonly Distributor SquareNormalDistributor;

		private readonly IRandomNumberGenerator MDcMmcQGFiJaKTTjwEygHTijSXvE;

		private readonly Distributor uamFQIgEEhMWGgjQxTKbzmELPHvwA;

		public DistributiveRandomNumberGenerator(Distributor distributor, IRandomNumberGenerator internalGenerator = null)
		{
			while (true)
			{
				int num = -1802227255;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1148139380)) % 4)
					{
					case 2u:
						break;
					default:
						return;
					case 1u:
						MDcMmcQGFiJaKTTjwEygHTijSXvE = internalGenerator ?? new DotNetRandomNumberGenerator();
						num = -1317037141;
						continue;
					case 3u:
						uamFQIgEEhMWGgjQxTKbzmELPHvwA = distributor ?? throw new ArgumentNullException("distributor");
						num = (int)(num2 * 525563189) ^ -1416589585;
						continue;
					case 0u:
						return;
					}
					break;
				}
			}
		}

		public double NextDouble()
		{
			double value = MDcMmcQGFiJaKTTjwEygHTijSXvE.NextDouble();
			return uamFQIgEEhMWGgjQxTKbzmELPHvwA(value);
		}

		static DistributiveRandomNumberGenerator()
		{
			while (true)
			{
				int num = -1418709368;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1236587580)) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 1u:
						goto IL_004c;
					case 2u:
						return;
					}
					break;
					IL_004c:
					SquareNormalDistributor = XwWPjKkdTBaFtALtATnTIqerqCTh._003C_003E9.FlVkiUjLLgkQGEPyJrraKkFcdXaBA;
					num = ((int)num2 * -129844868) ^ -1662289963;
				}
			}
		}
	}
}
