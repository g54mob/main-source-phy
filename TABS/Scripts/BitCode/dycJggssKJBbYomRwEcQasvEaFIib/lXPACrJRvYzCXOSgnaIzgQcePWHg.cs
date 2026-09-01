using System.Linq;
using BitCode.Performance;

namespace dycJggssKJBbYomRwEcQasvEaFIib
{
	internal abstract class lXPACrJRvYzCXOSgnaIzgQcePWHg : PerformanceCounterBase<double, double>
	{
		private double lDyjPSJrpkNzpBpOBLImcqHYuSTX;

		protected lXPACrJRvYzCXOSgnaIzgQcePWHg(int P_0)
			: base(P_0)
		{
			while (true)
			{
				int num = 1846009434;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x100B6495)) % 4)
					{
					case 2u:
						break;
					default:
						return;
					case 3u:
						base.Min = -1.0;
						num = ((int)num2 * -1279253860) ^ 0x24FE32D1;
						continue;
					case 0u:
						base.Max = -1.0;
						base.Average = 0.0;
						lDyjPSJrpkNzpBpOBLImcqHYuSTX = 0.0;
						num = (int)((num2 * 563497449) ^ 0x7606E240);
						continue;
					case 1u:
						return;
					}
					break;
				}
			}
		}

		public virtual void BADtBjxeUMQDPWlTuNbLvnFRVTw()
		{
			if (!GetSample(out var retrievedSample))
			{
				goto IL_000d;
			}
			goto IL_01e2;
			IL_000d:
			int num = -1223829383;
			goto IL_0012;
			IL_0012:
			double head = default(double);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -741881950)) % 20)
				{
				case 19u:
					break;
				default:
					return;
				case 0u:
					base.Min = Samples.Min();
					num = ((int)num2 * -1700554340) ^ 0x79AF8436;
					continue;
				case 8u:
					return;
				case 15u:
					base.Max = -1.0;
					num = (int)((num2 * 608558003) ^ 0x51C9A7DD);
					continue;
				case 14u:
					base.Max = retrievedSample;
					num = (int)((num2 * 778905943) ^ 0x109D713E);
					continue;
				case 3u:
					goto IL_00e7;
				case 4u:
					goto IL_0104;
				case 11u:
					goto IL_0129;
				case 10u:
					base.Min = retrievedSample;
					num = ((int)num2 * -2041932478) ^ 0x1C040906;
					continue;
				case 17u:
					head = Samples.Head;
					lDyjPSJrpkNzpBpOBLImcqHYuSTX -= head;
					num = ((int)num2 * -1096870072) ^ -1819332057;
					continue;
				case 12u:
					base.Min = -1.0;
					num = (int)((num2 * 872556735) ^ 0x699C9C71);
					continue;
				case 16u:
					num = (int)(num2 * 654267602) ^ -2055229726;
					continue;
				case 18u:
					Samples.PushBack(retrievedSample);
					lDyjPSJrpkNzpBpOBLImcqHYuSTX += retrievedSample;
					num = -580243565;
					continue;
				case 9u:
					goto IL_01e2;
				case 13u:
				{
					base.Average = lDyjPSJrpkNzpBpOBLImcqHYuSTX / (double)Samples.Count;
					int num5;
					int num6;
					if (base.Min >= 0.0)
					{
						num5 = 66750783;
						num6 = num5;
					}
					else
					{
						num5 = 312755202;
						num6 = num5;
					}
					num = num5 ^ (int)(num2 * 1531477184);
					continue;
				}
				case 7u:
					return;
				case 1u:
				{
					int num3;
					int num4;
					if (head == base.Min)
					{
						num3 = -2100513170;
						num4 = num3;
					}
					else
					{
						num3 = -1533478835;
						num4 = num3;
					}
					num = num3 ^ ((int)num2 * -1022122620);
					continue;
				}
				case 6u:
					base.Max = Samples.Max();
					num = (int)((num2 * 2121857955) ^ 0x707A2EB8);
					continue;
				case 5u:
					goto IL_02af;
				case 2u:
					return;
				}
				break;
				IL_02af:
				int num7;
				if (retrievedSample < base.Min)
				{
					num = -691490652;
					num7 = num;
				}
				else
				{
					num = -973396854;
					num7 = num;
				}
				continue;
				IL_0104:
				int num8;
				if (base.Max < 0.0)
				{
					num = -1201166044;
					num8 = num;
				}
				else
				{
					num = -1812144711;
					num8 = num;
				}
				continue;
				IL_00e7:
				int num9;
				if (head == base.Max)
				{
					num = -804936603;
					num9 = num;
				}
				else
				{
					num = -1658607880;
					num9 = num;
				}
				continue;
				IL_0129:
				int num10;
				if (retrievedSample > base.Max)
				{
					num = -390822508;
					num10 = num;
				}
				else
				{
					num = -1638312092;
					num10 = num;
				}
			}
			goto IL_000d;
			IL_01e2:
			int num11;
			if (Samples.Capacity != Samples.Count)
			{
				num = -1658607880;
				num11 = num;
			}
			else
			{
				num = -2019544181;
				num11 = num;
			}
			goto IL_0012;
		}
	}
}
