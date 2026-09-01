using System;
using BitCode.Maths;
using JetBrains.Annotations;
using UnityEngine;

namespace BitCode.Pooling
{
	public class CompoundComponentPool<T> : IPool<T>, IPool where T : IPoolable, Component
	{
		protected readonly T[] prefabs;

		protected readonly Action<T> initalize;

		private readonly AutoComponentPrefabPool<T>[] KofKsDjfjKspNsOBifOeGXxDdNUz;

		private readonly IRandomNumberGenerator dAJcEeBeJUvrUWOStMstGdERkZMGA;

		public AutoComponentPrefabPool<T> this[int index] => KofKsDjfjKspNsOBifOeGXxDdNUz[index];

		public int PoolCount => KofKsDjfjKspNsOBifOeGXxDdNUz.Length;

		public int TotalCount
		{
			get
			{
				int num = 0;
				AutoComponentPrefabPool<T>[] kofKsDjfjKspNsOBifOeGXxDdNUz = KofKsDjfjKspNsOBifOeGXxDdNUz;
				int num4 = default(int);
				AutoComponentPrefabPool<T> autoComponentPrefabPool = default(AutoComponentPrefabPool<T>);
				while (true)
				{
					int num2 = -562567301;
					while (true)
					{
						uint num3;
						switch ((num3 = (uint)(num2 ^ -402388264)) % 7)
						{
						case 4u:
							break;
						case 6u:
							num4++;
							num2 = (int)((num3 * 1990211345) ^ 0x2DF0172D);
							continue;
						case 0u:
							num += autoComponentPrefabPool.TotalCount;
							num2 = ((int)num3 * -1058684421) ^ 0x32FBE073;
							continue;
						case 1u:
							autoComponentPrefabPool = kofKsDjfjKspNsOBifOeGXxDdNUz[num4];
							num2 = -1577729374;
							continue;
						case 3u:
						{
							int num5;
							if (num4 >= kofKsDjfjKspNsOBifOeGXxDdNUz.Length)
							{
								num2 = -339553855;
								num5 = num2;
							}
							else
							{
								num2 = -694446186;
								num5 = num2;
							}
							continue;
						}
						case 2u:
							num4 = 0;
							num2 = ((int)num3 * -1910591655) ^ -1846877181;
							continue;
						default:
							return num;
						}
						break;
					}
				}
			}
		}

		public int AvailableCount
		{
			get
			{
				int num = 0;
				AutoComponentPrefabPool<T>[] kofKsDjfjKspNsOBifOeGXxDdNUz = KofKsDjfjKspNsOBifOeGXxDdNUz;
				int num4 = default(int);
				AutoComponentPrefabPool<T> autoComponentPrefabPool = default(AutoComponentPrefabPool<T>);
				while (true)
				{
					int num2 = 1636586488;
					while (true)
					{
						uint num3;
						switch ((num3 = (uint)(num2 ^ 0x13563D3E)) % 8)
						{
						case 0u:
							break;
						case 3u:
						{
							int num5;
							if (num4 < kofKsDjfjKspNsOBifOeGXxDdNUz.Length)
							{
								num2 = 294244714;
								num5 = num2;
							}
							else
							{
								num2 = 2096920255;
								num5 = num2;
							}
							continue;
						}
						case 7u:
							num2 = ((int)num3 * -1390182392) ^ -1736041563;
							continue;
						case 4u:
							autoComponentPrefabPool = kofKsDjfjKspNsOBifOeGXxDdNUz[num4];
							num2 = 158796708;
							continue;
						case 6u:
							num4 = 0;
							num2 = (int)(num3 * 13201516) ^ -167325551;
							continue;
						case 5u:
							num4++;
							num2 = (int)(num3 * 152303944) ^ -1558560331;
							continue;
						case 2u:
							num += autoComponentPrefabPool.AvailableCount;
							num2 = ((int)num3 * -31520023) ^ 0x5FFFA4C9;
							continue;
						default:
							return num;
						}
						break;
					}
				}
			}
		}

		public void ReturnAll()
		{
			AutoComponentPrefabPool<T>[] kofKsDjfjKspNsOBifOeGXxDdNUz = KofKsDjfjKspNsOBifOeGXxDdNUz;
			int num = 0;
			while (true)
			{
				int num2 = -1948623946;
				while (true)
				{
					uint num3;
					switch ((num3 = (uint)(num2 ^ -1360487194)) % 5)
					{
					case 4u:
						break;
					default:
						return;
					case 1u:
						num2 = (int)((num3 * 108765014) ^ 0x6E4720A0);
						continue;
					case 3u:
					{
						int num4;
						if (num >= kofKsDjfjKspNsOBifOeGXxDdNUz.Length)
						{
							num2 = -951734802;
							num4 = num2;
						}
						else
						{
							num2 = -1101970613;
							num4 = num2;
						}
						continue;
					}
					case 2u:
						kofKsDjfjKspNsOBifOeGXxDdNUz[num].ReturnAll();
						num++;
						num2 = -1039236032;
						continue;
					case 0u:
						return;
					}
					break;
				}
			}
		}

		public T Get(Action<T> resetOverride = null)
		{
			int num = dAJcEeBeJUvrUWOStMstGdERkZMGA.Next(KofKsDjfjKspNsOBifOeGXxDdNUz.Length);
			return KofKsDjfjKspNsOBifOeGXxDdNUz[num].Get(resetOverride);
		}

		public bool Contains(T pooledItem)
		{
			AutoComponentPrefabPool<T>[] kofKsDjfjKspNsOBifOeGXxDdNUz = KofKsDjfjKspNsOBifOeGXxDdNUz;
			int num3 = default(int);
			while (true)
			{
				int num = 1355317181;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x1601EC34)) % 8)
					{
					case 4u:
						break;
					case 1u:
						num3 = 0;
						num = ((int)num2 * -1698461547) ^ 0x4A4300FE;
						continue;
					case 6u:
						num3++;
						num = 917651550;
						continue;
					case 5u:
					{
						int num5;
						if (kofKsDjfjKspNsOBifOeGXxDdNUz[num3].Contains(pooledItem))
						{
							num = 1675311652;
							num5 = num;
						}
						else
						{
							num = 1098260378;
							num5 = num;
						}
						continue;
					}
					case 2u:
					{
						int num4;
						if (num3 < kofKsDjfjKspNsOBifOeGXxDdNUz.Length)
						{
							num = 2102589697;
							num4 = num;
						}
						else
						{
							num = 2056695207;
							num4 = num;
						}
						continue;
					}
					case 7u:
						num = ((int)num2 * -1917942538) ^ -618354172;
						continue;
					case 0u:
						return true;
					default:
						return false;
					}
					break;
				}
			}
		}

		public void Return(T pooledItem)
		{
			pooledItem.Owner.Return(pooledItem);
		}

		public CompoundComponentPool([NotNull] T[] prefabs, IRandomNumberGenerator numberGenerator, Action<T> initialize = null, Action<T> reset = null, int initialCapacity = 0)
		{
			int num3 = default(int);
			while (true)
			{
				int num = 1867589797;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x477505A)) % 12)
					{
					case 4u:
						break;
					default:
						return;
					case 7u:
					{
						int num5;
						int num6;
						if (prefabs.Length != 0)
						{
							num5 = -875328739;
							num6 = num5;
						}
						else
						{
							num5 = -1387906188;
							num6 = num5;
						}
						num = num5 ^ (int)(num2 * 1989329174);
						continue;
					}
					case 8u:
						throw new ArgumentException("Value cannot be an empty collection.", "prefabs");
					case 6u:
						num3++;
						num = ((int)num2 * -1018678329) ^ 0x12C7A408;
						continue;
					case 2u:
						num = ((int)num2 * -1112874727) ^ -1481845704;
						continue;
					case 0u:
					{
						int num4;
						if (num3 >= KofKsDjfjKspNsOBifOeGXxDdNUz.Length)
						{
							num = 157181903;
							num4 = num;
						}
						else
						{
							num = 162834189;
							num4 = num;
						}
						continue;
					}
					case 1u:
						this.prefabs = prefabs ?? throw new ArgumentNullException("prefabs");
						num = 1614080712;
						continue;
					case 10u:
						initalize = initialize;
						num = ((int)num2 * -42652056) ^ 0x57055917;
						continue;
					case 5u:
						dAJcEeBeJUvrUWOStMstGdERkZMGA = numberGenerator;
						num = ((int)num2 * -1105330145) ^ -102094838;
						continue;
					case 11u:
						KofKsDjfjKspNsOBifOeGXxDdNUz[num3] = new AutoComponentPrefabPool<T>(prefabs[num3], initialize, reset, initialCapacity);
						num = 108454116;
						continue;
					case 3u:
						KofKsDjfjKspNsOBifOeGXxDdNUz = new AutoComponentPrefabPool<T>[prefabs.Length];
						num3 = 0;
						num = ((int)num2 * -1527587494) ^ -1002083162;
						continue;
					case 9u:
						return;
					}
					break;
				}
			}
		}

		IPoolable IPool.Get(Action<IPoolable> resetOverride)
		{
			int num = dAJcEeBeJUvrUWOStMstGdERkZMGA.Next(KofKsDjfjKspNsOBifOeGXxDdNUz.Length);
			return ((IPool<T>)KofKsDjfjKspNsOBifOeGXxDdNUz[num]).Get((Action<T>)resetOverride);
		}

		bool IPool.Contains(IPoolable pooledItem)
		{
			return Contains((T)pooledItem);
		}

		void IPool.Return(IPoolable pooledItem)
		{
			Return((T)pooledItem);
		}
	}
}
