using System.Linq;
using BitCode.Performance;

namespace dycJggssKJBbYomRwEcQasvEaFIib
{
	internal abstract class ioHvfLyWrTmuseRULrayklBvBDFh : PerformanceCounterBase<long, double>
	{
		private long lDyjPSJrpkNzpBpOBLImcqHYuSTX;

		protected ioHvfLyWrTmuseRULrayklBvBDFh(int P_0)
			: base(P_0)
		{
			base.Min = -1L;
			base.Max = -1L;
			base.Average = 0.0;
			lDyjPSJrpkNzpBpOBLImcqHYuSTX = 0L;
		}

		public virtual void BADtBjxeUMQDPWlTuNbLvnFRVTw()
		{
			if (!GetSample(out var retrievedSample))
			{
				goto IL_000d;
			}
			goto IL_01e9;
			IL_000d:
			int num = 1819195432;
			goto IL_0012;
			IL_0012:
			long head = default(long);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x4CDBE8AB)) % 20)
				{
				case 7u:
					break;
				default:
					return;
				case 19u:
					return;
				case 17u:
					base.Min = Samples.Min();
					num = (int)((num2 * 1448645850) ^ 0x75B00902);
					continue;
				case 10u:
					base.Max = -1L;
					num = ((int)num2 * -1560293948) ^ 0x789BB1A3;
					continue;
				case 3u:
				{
					int num5;
					int num6;
					if (base.Min >= 0)
					{
						num5 = 10705376;
						num6 = num5;
					}
					else
					{
						num5 = 2017724725;
						num6 = num5;
					}
					num = num5 ^ (int)(num2 * 1535556065);
					continue;
				}
				case 8u:
					Samples.PushBack(retrievedSample);
					num = 1799696673;
					continue;
				case 9u:
					goto IL_0100;
				case 12u:
					goto IL_011e;
				case 2u:
					lDyjPSJrpkNzpBpOBLImcqHYuSTX += retrievedSample;
					base.Average = (double)lDyjPSJrpkNzpBpOBLImcqHYuSTX / (double)Samples.Count;
					num = (int)((num2 * 1085464255) ^ 0x392CFC66);
					continue;
				case 6u:
					goto IL_0175;
				case 0u:
					base.Min = retrievedSample;
					num = ((int)num2 * -251078911) ^ 0x48D6D74A;
					continue;
				case 16u:
					return;
				case 11u:
					num = ((int)num2 * -1843831650) ^ -1128977296;
					continue;
				case 14u:
					base.Max = retrievedSample;
					num = ((int)num2 * -1492276154) ^ -235221946;
					continue;
				case 18u:
					goto IL_01e9;
				case 13u:
				{
					head = Samples.Head;
					lDyjPSJrpkNzpBpOBLImcqHYuSTX -= head;
					int num3;
					int num4;
					if (head != base.Min)
					{
						num3 = 289741159;
						num4 = num3;
					}
					else
					{
						num3 = 26564596;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 939290292);
					continue;
				}
				case 5u:
					base.Max = Samples.Max();
					num = ((int)num2 * -850062869) ^ -2012415492;
					continue;
				case 4u:
					goto IL_0277;
				case 15u:
					base.Min = -1L;
					num = ((int)num2 * -719911547) ^ 0x23F7E24;
					continue;
				case 1u:
					return;
				}
				break;
				IL_0277:
				int num7;
				if (head != base.Max)
				{
					num = 638230507;
					num7 = num;
				}
				else
				{
					num = 1268784921;
					num7 = num;
				}
				continue;
				IL_011e:
				int num8;
				if (retrievedSample >= base.Min)
				{
					num = 625548410;
					num8 = num;
				}
				else
				{
					num = 2054263707;
					num8 = num;
				}
				continue;
				IL_0100:
				int num9;
				if (base.Max < 0)
				{
					num = 746991430;
					num9 = num;
				}
				else
				{
					num = 1327493045;
					num9 = num;
				}
				continue;
				IL_0175:
				int num10;
				if (retrievedSample > base.Max)
				{
					num = 1655634421;
					num10 = num;
				}
				else
				{
					num = 2122918898;
					num10 = num;
				}
			}
			goto IL_000d;
			IL_01e9:
			int num11;
			if (Samples.Capacity != Samples.Count)
			{
				num = 638230507;
				num11 = num;
			}
			else
			{
				num = 1888987182;
				num11 = num;
			}
			goto IL_0012;
		}
	}
}
